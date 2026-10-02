using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000FD RID: 253
	public class PawnSeega : PawnBase
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0005E110 File Offset: 0x0005C310
		public override bool IsPlaced
		{
			get
			{
				return this.X >= 0 && this.X < BoardGameSeega.BoardWidth && this.Y >= 0 && this.Y < BoardGameSeega.BoardHeight;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0005E140 File Offset: 0x0005C340
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0005E148 File Offset: 0x0005C348
		public bool MovedThisTurn { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0005E151 File Offset: 0x0005C351
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0005E159 File Offset: 0x0005C359
		public int PrevX
		{
			get
			{
				return this._prevX;
			}
			set
			{
				this._prevX = value;
				if (value >= 0)
				{
					this.MovedThisTurn = true;
					return;
				}
				this.MovedThisTurn = false;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0005E175 File Offset: 0x0005C375
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0005E17D File Offset: 0x0005C37D
		public int PrevY
		{
			get
			{
				return this._prevY;
			}
			set
			{
				this._prevY = value;
				if (value >= 0)
				{
					this.MovedThisTurn = true;
					return;
				}
				this.MovedThisTurn = false;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0005E199 File Offset: 0x0005C399
		public PawnSeega(GameEntity entity, bool playerOne)
			: base(entity, playerOne)
		{
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
			this.MovedThisTurn = false;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0005E1C6 File Offset: 0x0005C3C6
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
			this.MovedThisTurn = false;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0005E1F1 File Offset: 0x0005C3F1
		public void UpdateMoveBackAvailable()
		{
			if (this.MovedThisTurn)
			{
				this.MovedThisTurn = false;
				return;
			}
			this.PrevX = -1;
			this.PrevY = -1;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0005E211 File Offset: 0x0005C411
		public void AISetMovedThisTurn(bool moved)
		{
			this.MovedThisTurn = moved;
		}

		// Token: 0x0400058D RID: 1421
		public int X;

		// Token: 0x0400058E RID: 1422
		public int Y;

		// Token: 0x0400058F RID: 1423
		private int _prevX;

		// Token: 0x04000590 RID: 1424
		private int _prevY;
	}
}
