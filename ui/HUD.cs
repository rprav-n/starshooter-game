using Godot;
using System;

public class HUD : Control
{
	private Label scoreLabel;	
	private TextureRect livesTexture;

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("ScoreLabel");
		livesTexture = GetNode<TextureRect>("LivesTexture");
	}
	
	public void updateScore(int score) 
	{
		scoreLabel.Text = "SCORE: " + score.ToString();
	}
	
	public void UpdateLives(int value) 
	{
		Vector2 newRectSize = livesTexture.RectSize;
		newRectSize.x = value * 37;
		livesTexture.RectSize = newRectSize;
	}
}
