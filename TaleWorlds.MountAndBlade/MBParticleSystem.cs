using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D7 RID: 471
	public struct MBParticleSystem
	{
		// Token: 0x06001C06 RID: 7174 RVA: 0x00060D9D File Offset: 0x0005EF9D
		internal MBParticleSystem(int i)
		{
			this.index = i;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00060DA6 File Offset: 0x0005EFA6
		public bool Equals(MBParticleSystem a)
		{
			return this.index == a.index;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00060DB6 File Offset: 0x0005EFB6
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x0400096E RID: 2414
		private int index;
	}
}
