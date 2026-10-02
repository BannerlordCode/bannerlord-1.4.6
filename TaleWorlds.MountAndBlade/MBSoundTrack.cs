using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DD RID: 477
	public struct MBSoundTrack
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x00060FDD File Offset: 0x0005F1DD
		internal MBSoundTrack(int i)
		{
			this.index = i;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00060FE6 File Offset: 0x0005F1E6
		public bool Equals(MBSoundTrack a)
		{
			return this.index == a.index;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00060FF6 File Offset: 0x0005F1F6
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x04000985 RID: 2437
		private int index;
	}
}
