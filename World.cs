using Godot;
using System;

public class World : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void _on_Player_spawnLaser(PackedScene Laser, Vector2 location) 
	{
		var laser = Laser.Instance<Area2D>();
		laser.GlobalPosition = location;
		AddChild(laser);
	}
	
	public void _on_ShootingEnemy_spawnLaser(PackedScene Laser, Vector2 location) 
	{
		var laser = Laser.Instance<Area2D>();
		laser.GlobalPosition = location;
		AddChild(laser);
	}

}
