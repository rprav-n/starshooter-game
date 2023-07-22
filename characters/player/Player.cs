using Godot;
using System;

public class Player : Area2D
{
	[Export]
	private int speed = 300;
	
	[Export]
	private int hp = 3;
	
	[Export]
	private int damage = 1;
	
	private Vector2 inputVector = Vector2.Zero;
	private PackedScene Laser;
	
	[Signal]
	public delegate void spawnLaser(PackedScene packedScene, Vector2 location);
	
	[Signal]
	public delegate void playerTookDamage(int hpLeft);
	
	private Position2D muzzle;
	private AudioStreamPlayer hitSound;
	private AudioStreamPlayer laserSound;

	[Signal]
	public delegate void playerDied();

	public override void _Ready()
	{
		muzzle = GetNode<Position2D>("Muzzle");
		Laser = GD.Load<PackedScene>("res://projectiles/PlayerLaser.tscn");
		
		hitSound = GetNode<AudioStreamPlayer>("HitSound");
		laserSound = GetNode<AudioStreamPlayer>("LaserSound");
	}
	

	public override void _PhysicsProcess(float delta)
	{
		// Screen Dimension: 540w, 960h
		inputVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		inputVector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
		
		// normalize: the diagonal movement wont be faster
		inputVector = inputVector.Normalized();
		
		var newPosition = new Vector2();
		newPosition.x = inputVector.x * speed * delta;
		newPosition.y = inputVector.y * speed * delta;
		
		this.GlobalPosition += newPosition;
		
		// clamp: so that player wont move beyong screen boundaries
		this.GlobalPosition = new Vector2(Mathf.Clamp(this.GlobalPosition.x, 0, 540), Mathf.Clamp(this.GlobalPosition.y, 0, 960));
		
		if (Input.IsActionJustPressed("shoot")) 
		{
			// var laser = Laser.Instance<Area2D>();
			// laser.GlobalPosition = this.GlobalPosition;
			// AddChild(laser);
			laserSound.Play();
			this.EmitSignal("spawnLaser", Laser, muzzle.GlobalPosition);
		}
	}
	
	public void _on_Player_area_entered(Area2D area) 
	{
		if (area.IsInGroup("enemies") && area is Enemy enemy) 
		{
			enemy.takeDamage(damage);
		}
	}
	
	public void takeDamage(int damage) 
	{
		hp -= damage;
		hitSound.Play();
		this.EmitSignal("playerTookDamage", hp);
		if (hp <=0 )
		{
			QueueFree();
			this.EmitSignal("playerDied");
		}
	}
}
