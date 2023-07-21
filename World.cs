using Godot;
using System;

public class World : Node2D
{
	private int score = 0;
	private PackedScene HUDScene;
	private HUD hud;

	public override void _Ready()
	{
		score = 0;
		HUDScene = GD.Load<PackedScene>("res://ui/HUD.tscn");
		hud = GetNode<HUD>("HUD");
		updateScoreAndHUD(0);
		hud.UpdateLives(3);
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
		enemy.Connect("enemyDied", this, "_on_EnemyDied");
		
	}
	
	public void _on_DeadZone_area_entered(Area2D area) 
	{
		//GD.Print("Deleting " + area.Name);
		area.QueueFree();
	}
	
	public void _on_EnemyDied(int points) 
	{
		updateScoreAndHUD(points);
	}
	
	public void updateScoreAndHUD(int points) 
	{
		score += points;
		GD.Print("hud", hud == null);
		hud.updateScore(score);
		
	}
	
	public void _on_Player_playerTookDamage(int hpLeft) 
	{
		hud.UpdateLives(hpLeft);
	}

}
