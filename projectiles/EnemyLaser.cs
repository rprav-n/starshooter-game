using Godot;
using System;

public class EnemyLaser : Laser
{
	public void _on_EnemyLaser_area_entered(Area2D area) 
	{
		if (area is Player player) 
		{
			player.takeDamage(damage);
			QueueFree();
		}
	}

}
