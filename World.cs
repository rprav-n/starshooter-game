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
	
	public void _on_EnemySpawner_spawnEnemy(PackedScene Enemy, Vector2 location) 
	{
		var enemy = Enemy.Instance<Area2D>();
		enemy.GlobalPosition = location;
		AddChild(enemy);
		if (enemy.HasSignal("spawnLaser"))  
		{
			enemy.Connect("spawnLaser", this, "_on_ShootingEnemy_spawnLaser");
		}
		
	}

}
