using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DF RID: 479
	public struct MBTeam
	{
		// Token: 0x06001C3F RID: 7231 RVA: 0x00061043 File Offset: 0x0005F243
		internal MBTeam(Mission mission, int index)
		{
			this._mission = mission;
			this.Index = index;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00061053 File Offset: 0x0005F253
		public static MBTeam InvalidTeam
		{
			get
			{
				return new MBTeam(null, -1);
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0006105C File Offset: 0x0005F25C
		public override int GetHashCode()
		{
			return this.Index;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00061064 File Offset: 0x0005F264
		public override bool Equals(object obj)
		{
			return ((MBTeam)obj).Index == this.Index;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00061079 File Offset: 0x0005F279
		public static bool operator ==(MBTeam team1, MBTeam team2)
		{
			return team1.Index == team2.Index;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00061089 File Offset: 0x0005F289
		public static bool operator !=(MBTeam team1, MBTeam team2)
		{
			return team1.Index != team2.Index;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0006109C File Offset: 0x0005F29C
		public bool IsValid
		{
			get
			{
				return this.Index >= 0;
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000610AA File Offset: 0x0005F2AA
		public bool IsEnemyOf(MBTeam otherTeam)
		{
			return MBAPI.IMBTeam.IsEnemy(this._mission.Pointer, this.Index, otherTeam.Index);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000610CD File Offset: 0x0005F2CD
		public void SetIsEnemyOf(MBTeam otherTeam, bool isEnemyOf)
		{
			MBAPI.IMBTeam.SetIsEnemy(this._mission.Pointer, this.Index, otherTeam.Index, isEnemyOf);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000610F1 File Offset: 0x0005F2F1
		public override string ToString()
		{
			return "Mission Team: " + this.Index;
		}

		// Token: 0x04000986 RID: 2438
		public readonly int Index;

		// Token: 0x04000987 RID: 2439
		private readonly Mission _mission;
	}
}
