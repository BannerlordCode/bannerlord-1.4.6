using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D6 RID: 470
	public struct MBMusicTrack
	{
		// Token: 0x06001C01 RID: 7169 RVA: 0x00060D60 File Offset: 0x0005EF60
		public MBMusicTrack(MBMusicTrack obj)
		{
			this.index = obj.index;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00060D6E File Offset: 0x0005EF6E
		internal MBMusicTrack(int i)
		{
			this.index = i;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00060D77 File Offset: 0x0005EF77
		private bool IsValid
		{
			get
			{
				return this.index >= 0;
			}
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00060D85 File Offset: 0x0005EF85
		public bool Equals(MBMusicTrack obj)
		{
			return this.index == obj.index;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00060D95 File Offset: 0x0005EF95
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x0400096D RID: 2413
		private int index;
	}
}
