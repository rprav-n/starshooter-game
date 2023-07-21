using Godot;
using System;

public class Enemy : Area2D
{
	[Export]
	private int speed = 150;
	
	[Export]
	private int hp = 1;
	
	[Export]
	private int damage = 1;

	[Export]
	private int points = 0;

	[Signal]
	private delegate void enemyDied(int points);
	private AudioStreamPlayer hitSound;

	public override void _Ready()
	{
		hitSound = GetNode<AudioStreamPlayer>("HitSound");
	}


	public override void _PhysicsProcess(float delta)
	{
		var newPosition = Vector2.Zero;

		newPosition.y = speed * delta;
		
		this.GlobalPosition += newPosition;
	}
	
	public void _on_Enemy_area_entered(Area2D area) 
	{
		if (area is Player player) 
		{
			player.takeDamage(damage);
		}
	}
	
	public void takeDamage(int damage) 
	{
		hp -= damage;
		hitSound.Play();
		if (hp <=0 )
		{
			QueueFree();
			this.EmitSignal("enemyDied", points);
		}
	}
}
