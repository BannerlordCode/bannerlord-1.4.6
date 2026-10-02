using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000FC RID: 252
	public class PawnPuluc : PawnBase
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0005DD21 File Offset: 0x0005BF21
		public float Height
		{
			get
			{
				if (PawnPuluc._height == 0f)
				{
					PawnPuluc._height = (base.Entity.GetBoundingBoxMax() - base.Entity.GetBoundingBoxMin()).z;
				}
				return PawnPuluc._height;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0005DD59 File Offset: 0x0005BF59
		public override Vec3 PosBeforeMoving
		{
			get
			{
				return this.PosBeforeMovingBase - new Vec3(0f, 0f, this.Height * (float)this.PawnsBelow.Count, -1f);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0005DD8D File Offset: 0x0005BF8D
		public override bool IsPlaced
		{
			get
			{
				return (this.InPlay || this.IsInSpawn) && this.IsTopPawn;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0005DDA7 File Offset: 0x0005BFA7
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0005DDAF File Offset: 0x0005BFAF
		public int X
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
				if (value >= 0 && value < 11)
				{
					this.IsInSpawn = false;
					return;
				}
				this.IsInSpawn = true;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0005DDD0 File Offset: 0x0005BFD0
		public List<PawnPuluc> PawnsBelow { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0005DDD8 File Offset: 0x0005BFD8
		public bool InPlay
		{
			get
			{
				return this.X >= 0 && this.X < 11;
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0005DDEF File Offset: 0x0005BFEF
		public PawnPuluc(GameEntity entity, bool playerOne)
			: base(entity, playerOne)
		{
			this.PawnsBelow = new List<PawnPuluc>();
			this.SpawnPos = base.CurrentPos;
			this.X = -1;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0005DE25 File Offset: 0x0005C025
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.State = PawnPuluc.MovementState.MovingForward;
			this.IsTopPawn = true;
			this.IsInSpawn = true;
			this.CapturedBy = null;
			this.PawnsBelow.Clear();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0005DE5C File Offset: 0x0005C05C
		public override void AddGoalPosition(Vec3 goal)
		{
			if (this.IsTopPawn)
			{
				goal.z += this.Height * (float)this.PawnsBelow.Count;
				int count = this.PawnsBelow.Count;
				for (int i = 0; i < count; i++)
				{
					this.PawnsBelow[i].AddGoalPosition(goal - new Vec3(0f, 0f, (float)(i + 1) * this.Height, -1f));
				}
			}
			base.GoalPositions.Add(goal);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0005DEEC File Offset: 0x0005C0EC
		public override void MovePawnToGoalPositions(bool instantMove, float speed, bool dragged = false)
		{
			if (base.GoalPositions.Count == 0)
			{
				return;
			}
			base.MovePawnToGoalPositions(instantMove, speed, dragged);
			if (this.IsTopPawn)
			{
				foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
				{
					pawnPuluc.MovePawnToGoalPositions(instantMove, speed, dragged);
				}
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0005DF60 File Offset: 0x0005C160
		public override void SetPawnAtPosition(Vec3 position)
		{
			base.SetPawnAtPosition(position);
			if (this.IsTopPawn)
			{
				int num = 1;
				foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
				{
					pawnPuluc.SetPawnAtPosition(new Vec3(position.x, position.y, position.z - this.Height * (float)num, -1f));
					num++;
				}
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0005DFEC File Offset: 0x0005C1EC
		public override void EnableCollisionBody()
		{
			base.EnableCollisionBody();
			foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
			{
				pawnPuluc.Entity.BodyFlag &= ~BodyFlags.Disabled;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0005E050 File Offset: 0x0005C250
		public override void DisableCollisionBody()
		{
			base.DisableCollisionBody();
			foreach (PawnPuluc pawnPuluc in this.PawnsBelow)
			{
				pawnPuluc.Entity.BodyFlag |= BodyFlags.Disabled;
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0005E0B4 File Offset: 0x0005C2B4
		public void MovePawnBackToSpawn(bool instantMove, float speed, bool fake = false)
		{
			this.X = -1;
			this.State = PawnPuluc.MovementState.MovingForward;
			this.IsTopPawn = true;
			this.IsInSpawn = true;
			base.Captured = false;
			this.CapturedBy = null;
			this.PawnsBelow.Clear();
			if (!fake)
			{
				this.AddGoalPosition(this.SpawnPos);
				this.MovePawnToGoalPositions(instantMove, speed, false);
			}
		}

		// Token: 0x04000585 RID: 1413
		public PawnPuluc.MovementState State;

		// Token: 0x04000586 RID: 1414
		public PawnPuluc CapturedBy;

		// Token: 0x04000587 RID: 1415
		public Vec3 SpawnPos;

		// Token: 0x04000588 RID: 1416
		public bool IsInSpawn = true;

		// Token: 0x04000589 RID: 1417
		public bool IsTopPawn = true;

		// Token: 0x0400058A RID: 1418
		private static float _height;

		// Token: 0x0400058B RID: 1419
		private int _x;

		// Token: 0x0200022A RID: 554
		public enum MovementState
		{
			// Token: 0x040009CC RID: 2508
			MovingForward,
			// Token: 0x040009CD RID: 2509
			MovingBackward,
			// Token: 0x040009CE RID: 2510
			ChangingDirection
		}
	}
}
