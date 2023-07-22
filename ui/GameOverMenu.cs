using Godot;
using System;

public class GameOverMenu : Control
{

	private Label ScoreLabel;

	public override void _Ready()
	{
		ScoreLabel = GetNode<Label>("ScoreLabel");
	}

	public override void _Process(float delta)
	{
		
	}
	
	public void _on_Retry_pressed() 
	{
		GetTree().ChangeScene("res://World.tscn");
	}
	
	public void _on_Back_pressed() 
	{
		GetTree().ChangeScene("res://ui/MainMenu.tscn");
	}
	
	public void setScore(int score) 
	{
		ScoreLabel.Text = "Score = " + score.ToString();
	}
}
