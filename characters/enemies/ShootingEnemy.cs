using Godot;
using System;

public class ShootingEnemy : Enemy
{
	private PackedScene Laser;
	private Position2D muzzle;
	
	[Signal]
	public delegate void spawnLaser(PackedScene packedScene, Vector2 location);

	private AudioStreamPlayer laserSound;
	
	public override void _Ready()
	{
		base._Ready();
		laserSound = GetNode<AudioStreamPlayer>("LaserSound");
		muzzle = GetNode<Position2D>("Muzzle");
		Laser = GD.Load<PackedScene>("res://projectiles/EnemyLaser.tscn");
	}
	
	public void _on_ShootTimer_timeout() 
	{
		laserSound.Play();
		this.EmitSignal("spawnLaser", Laser, muzzle.GlobalPosition);		
	}
}
