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

	public override void _Ready()
	{

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
		if (hp <=0 )
		{
			QueueFree();
		}
	}
}
