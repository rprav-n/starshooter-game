using Godot;
using System;

public class PlayerLaser : Laser
{
	public void _on_PlayerLaser_area_entered(Area2D area) 
	{
		if (area.IsInGroup("enemies") && area is Enemy enemy) 
		{
			enemy.takeDamage(damage);
			QueueFree();
		}
	}
}
