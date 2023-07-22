using System.Threading.Tasks;
using Godot;
using System;

public class World : Node2D
{
	private int score = 0;
	private PackedScene HUDScene;
	private HUD hud;
	private EnemySpawner enemySpawner;
	private PackedScene GameOverScene;
	private CanvasLayer uiLayer;

	private Explosion explosion;

	public override void _Ready()
	{
		score = 0;
		HUDScene = GD.Load<PackedScene>("res://ui/HUD.tscn");
		hud = GetNode<HUD>("HUD");
		updateScoreAndHUD(0);
		hud.UpdateLives(3);
		
		enemySpawner = GetNode<EnemySpawner>("EnemySpawner");
		GameOverScene = GD.Load<PackedScene>("res://ui/GameOverMenu.tscn");
		uiLayer = GetNode<CanvasLayer>("UILayer");
		
		explosion = GetNode<Explosion>("EffectsLayer/Explosion");
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
		area.QueueFree();
	}
	
	public void _on_EnemyDied(int points, Vector2 location) 
	{
		updateScoreAndHUD(points);
		createExplosion(location);
	}
	
	public void updateScoreAndHUD(int points) 
	{
		score += points;
		hud.updateScore(score);
		
	}
	
	public void _on_Player_playerTookDamage(int hpLeft) 
	{
		hud.UpdateLives(hpLeft);
	}

	// Player died, show Game over screen
	public void _on_Player_playerDied(Vector2 location) 
	{	
		createExplosion(location);
		var timer = GetTree().CreateTimer(3);
		timer.Connect("timeout", this, "_on_TimerComplete");
	}
	
	public void _on_TimerComplete() 
	{
		gameOver();
	}
	
	public void gameOver() 
	{

		enemySpawner.StopSpwaning();
		var gameOverMenu = GameOverScene.Instance<GameOverMenu>();
		uiLayer.AddChild(gameOverMenu);
		gameOverMenu.setScore(score);
	}
	
	public void createExplosion(Vector2 location) 
	{
		explosion.GlobalPosition = location;
		explosion.start();
	}
}
