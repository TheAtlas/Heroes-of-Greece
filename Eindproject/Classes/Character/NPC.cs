﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EindprojectEngine.Character.Dialogs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EindprojectEngine.Character
{

	public class NPC : Entity
	{

		private Dialog dialog;
		private string textureAssetName;
		private bool readyForDialog;
		private Texture2D dialogTexture;

		public NPC(int id, string name, Vector2 position, string textureAssetName, Dialog dialog, bool enabled, bool visible, Game game) : base(game)
		{
			Id = id;
			Name = name;
			Position = position;
			this.textureAssetName = textureAssetName;
			Visible = visible;
			Enabled = enabled;
			readyForDialog = false;
			this.dialog = dialog;
		}

		public Dialog Dialog
		{
			get
			{
				return dialog;
			}
			set
			{
				dialog = value;
			}
		}

		public bool ReadyForDialog
		{
			get
			{
				return readyForDialog;
			}
			set
			{
				readyForDialog = value;
			}
		}

		public override void Initialize()
		{
		}

		public override void LoadContent()
		{
			Texture = Game.Content.Load<Texture2D>(textureAssetName);
			dialogTexture = Game.Content.Load<Texture2D>("Textures/XboxController/Xbox360_Button_A");
		}

		public override void UnloadContent()
		{
		}

		public override void Update(GameTime gameTime)
		{
			if(Enabled)
			{
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(Visible)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(Texture, new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height), Color.White);
				if(readyForDialog)
				{
					spriteBatch.Draw(dialogTexture, new Vector2(Position.X + (Texture.Width / 2), Position.Y - 20), dialogTexture.Bounds, Color.White, 0.0f, new Vector2(dialogTexture.Width / 2, dialogTexture.Height / 2), 0.5f, SpriteEffects.None, 0.0f);
				}
				spriteBatch.End();
			}
		}

		public override void Dispose()
		{
		}
	}
}
