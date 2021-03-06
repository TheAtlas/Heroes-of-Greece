﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EindprojectEngine.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EindprojectEngine.Screens
{

	public class LoadingScreen : GameScreen
	{

		private bool loadingIsSlow;
		private bool otherScreensAreGone;
		private GameScreen[] screensToLoad;

		private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow, GameScreen[] screensToLoad)
		{
			this.loadingIsSlow = loadingIsSlow;
			this.screensToLoad = screensToLoad;
			TransitionOnTime = TimeSpan.FromSeconds(0.5);
		}

		public static void Load(ScreenManager screenManager, bool loadingIsSlow, PlayerIndex? controllingPlayer, params GameScreen[] screensToLoad)
		{
			foreach(GameScreen screen in screenManager.GetScreens())
			{
				screen.ExitScreen();
			}
			LoadingScreen loadingScreen = new LoadingScreen(screenManager, loadingIsSlow, screensToLoad);
			screenManager.AddScreen(loadingScreen, controllingPlayer);
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			if(otherScreensAreGone)
			{
				ScreenManager.RemoveScreen(this);
				foreach(GameScreen screen in screensToLoad)
				{
					if(screen != null)
					{
						ScreenManager.AddScreen(screen, ControllingPlayer);
					}
				}
				ScreenManager.Game.ResetElapsedTime();
			}
		}

		public override void Draw(GameTime gameTime)
		{
			if(ScreenState == ScreenState.Active && ScreenManager.GetScreens().Length == 1)
			{
				otherScreensAreGone = true;
			}
			if(loadingIsSlow)
			{
				SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
				SpriteFont font = ScreenManager.Font;
				const string message = "Loading...";
				Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
				Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
				Vector2 textSize = font.MeasureString(message);
				Vector2 textPosition = (viewportSize - textSize) / 2;
				Color color = Color.White * TransitionAlpha;
				spriteBatch.Begin();
				spriteBatch.DrawString(font, message, textPosition, color);
				spriteBatch.End();
			}
		}
	}
}
