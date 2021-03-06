﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EindprojectEngine.Screens
{

	public class PlayerIndexEventArgs : EventArgs
	{

		private PlayerIndex playerIndex;

		public PlayerIndexEventArgs(PlayerIndex playerIndex)
		{
			this.playerIndex = playerIndex;
		}

		public PlayerIndex PlayerIndex
		{
			get
			{
				return playerIndex;
			}
		}
	}
}
