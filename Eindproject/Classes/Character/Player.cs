using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EindprojectEngine.GameState;

namespace EindprojectEngine.Character
{

	public class Player : Entity
	{

		public enum PlayerState
		{
			Idle,
			Moving,
			Jumping,
			Attacking,
			InDialog,
			InCinematic,
			InMenu
		}

		private PlayerState playerState;
		private InputAction jumpControl;
		private InputAction pauseAction;
		private InputAction moveLeftControl;
		private InputAction moveRightControl;

		public Player(Game game) : base(game)
		{
			Id = 1;
			Name = "Player";
			MaxHealth = 100;
			Health = 100;
			AttackDamage = 10;
			AttackSpeed = 1.0;
			MovementSpeed = 3;
			Dead = false;
			Visible = true;
			Enabled = true;
			playerState = PlayerState.Idle;
			jumpControl = new InputAction(Buttons.X, Keys.Space, true);
			moveLeftControl = new InputAction(Buttons.LeftThumbstickLeft, Keys.Left, false);
			moveRightControl = new InputAction(Buttons.LeftThumbstickRight, Keys.Right, false);
			pauseAction = new InputAction(new Buttons[] { Buttons.Start, Buttons.Back }, new Keys[] { Keys.Escape }, true);
		}

		public PlayerState State
		{
			get
			{
				return playerState;
			}
			set
			{
				playerState = value;
			}
		}

		public override void Initialize()
		{

		}

		public override void LoadContent()
		{
			Texture = Game.Content.Load<Texture2D>("Textures/character");
			Position = new Vector2((float) (Game.GraphicsDevice.Viewport.Width - (Texture.Width * 1.5)), (float) Game.GraphicsDevice.Viewport.Height - 125);
		}

		public override void UnloadContent()
		{
		}

		public override void Update(GameTime gameTime)
		{
			if(Enabled)
			{
				foreach(Entity entity in EntityList.GetInstance(Game).GetEntities())
				{
					if(entity is NPC)
					{
						NPC npc = entity as NPC;
						if(npc.Position.X - (npc.Texture.Width / 2) >= (Position.X - 130) && npc.Position.X + (npc.Texture.Width / 2) <= (Position.X + 130))
						{
							npc.ReadyForDialog = true;
						} else
						{
							npc.ReadyForDialog = false;
						}
					}
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(Visible)
			{
				spriteBatch.Begin();
				SpriteEffects effects = Direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

				spriteBatch.Draw(Texture, new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height), Texture.Bounds, Color.White, 0.0f, new Vector2(0, 0), effects, 0.0f);

				//spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height), Color.Black);
				spriteBatch.End();
			}
		}

		public override void Dispose()
		{

		}

		public void HandleInput(GameTime gameTime, InputState input, PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			if(State == PlayerState.InDialog || State == PlayerState.InCinematic || State == PlayerState.InMenu)
			{
			} else
			{
				if(jumpControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					Console.WriteLine("[" + DateTime.Now.ToString("o") + "]: " + "Jump!");
				}
				if(moveLeftControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					State = PlayerState.Moving;
					Direction = 0;
					Position.X -= MovementSpeed;
				}

				if(moveRightControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					State = PlayerState.Moving;
					Direction = 1;
					Position.X += MovementSpeed;
				}
				if(!moveLeftControl.Evaluate(input, controllingPlayer, out playerIndex) && !moveRightControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					State = PlayerState.Idle;
				}
			}
		}
	}
}
