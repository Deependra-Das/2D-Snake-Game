# 2D-Snake-Game
 Classic 2D-Snake Game with Singleplayer & Co-op Mode. Developed with Unity using Singleton Design Pattern

 # Features
Gameplay:
-Two Gameplay Modes : Single Player & Co-Op Mode
-Snake Movement can be controlled in four directions using WASD Keys (Player 1) and Arrow keys (Player 2)
-Snake wraps around the playable area when it reaches the bounds
-Snake can grow or reduce its size upon eating certain type of food
-When Snake collides with itself or the other Snake (in Co-Op), the Snake dies & its Game Over
-Snake can collect powerups like Shield to avoid collision, ScoreMultipler to 2X Score & SpeedBoost to increase movement speed for certain amount of time

Foods:
-Mass Gainer: Increases the length of the Snake with a flexible amount
-Mass Burner: Decreases the length of the Snake with a flexible amount but does not decrease length if the Snake doesn't have a body
-Food spawns are randomized & they despawn after a certain amount of time if not eaten by Snake

PowerUps:
-Shield: Snake can avoid collision & death for certain amount of time
-Score Boost: Snake gets 2X Score for each Mass Gainer food it eats
-Speed Up: Snake Movement Speed is increased for a short duration
-Powerups are randomized & they despawn after a certain amount of time if not collected by Snake

Co-Op Mode:
-Two-player Gameplay mode with separate controls for 2 Snakes.
-If Snake A collides with Snake B's body, the Snake A dies & gets their score to 0, resulting in GameOver with Snake A as Winner
-If Snake A collides with Snake B's head i.e., Head to Head Collision, both Snakes die, resulting in GameOver with whichever Snake has higher score

Score & High Score Management:
-Score increases with Mass Gainer food
-Score decreases with Mass Burner food
-Everytime a Player Scores Higher Score than existing High Score for the game mode, the High Score is set

UI:
-Lobby UI with Single Player, Co-Op Mode & Quit Buttons
-Lobby UI also displays Highscores for Single Player & Co-Op Modes
-Gameplay UI to display Score(s) for Snake(s) & their active powerups, 
-Pause Menu (using Escape Button) with Resume and Back to Menu buttons along with Highscore for respective game Modes.
-Game Over Menu with Restart and Back to Menu buttons along with Winner & Loser Score, Highscore for respective game Modes.

Audio:
-Separate Audio Sources for Menu SFX, Powerups, Foods & Background Music
-Whenever Snake eats a food or collects a powerup, corresponding SFX is played

