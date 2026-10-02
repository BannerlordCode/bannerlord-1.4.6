using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000392 RID: 914
	public class GameOverState : GameState
	{
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000D968F File Offset: 0x000D788F
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000D9692 File Offset: 0x000D7892
		// (set) Token: 0x060034FE RID: 13566 RVA: 0x000D969A File Offset: 0x000D789A
		public IGameOverStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x000D96A3 File Offset: 0x000D78A3
		// (set) Token: 0x06003500 RID: 13568 RVA: 0x000D96AB File Offset: 0x000D78AB
		public GameOverState.GameOverReason Reason { get; private set; }

		// Token: 0x06003501 RID: 13569 RVA: 0x000D96B4 File Offset: 0x000D78B4
		public GameOverState()
		{
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000D96BC File Offset: 0x000D78BC
		public GameOverState(GameOverState.GameOverReason reason)
		{
			this.Reason = reason;
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000D96CB File Offset: 0x000D78CB
		public static GameOverState CreateForVictory()
		{
			Game game = Game.Current;
			if (game == null)
			{
				return null;
			}
			return game.GameStateManager.CreateState<GameOverState>(new object[] { GameOverState.GameOverReason.Victory });
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000D96F1 File Offset: 0x000D78F1
		public static GameOverState CreateForRetirement()
		{
			Game game = Game.Current;
			if (game == null)
			{
				return null;
			}
			return game.GameStateManager.CreateState<GameOverState>(new object[] { GameOverState.GameOverReason.Retirement });
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000D9717 File Offset: 0x000D7917
		public static GameOverState CreateForClanDestroyed()
		{
			Game game = Game.Current;
			if (game == null)
			{
				return null;
			}
			return game.GameStateManager.CreateState<GameOverState>(new object[] { GameOverState.GameOverReason.ClanDestroyed });
		}

		// Token: 0x04000F27 RID: 3879
		private IGameOverStateHandler _handler;

		// Token: 0x0200076F RID: 1903
		public enum GameOverReason
		{
			// Token: 0x04001EA0 RID: 7840
			Retirement,
			// Token: 0x04001EA1 RID: 7841
			ClanDestroyed,
			// Token: 0x04001EA2 RID: 7842
			Victory
		}
	}
}
