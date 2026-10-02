using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000255 RID: 597
	public class CompassMarker
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000767C6 File Offset: 0x000749C6
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x000767CE File Offset: 0x000749CE
		public string Id { get; private set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000767D7 File Offset: 0x000749D7
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x000767DF File Offset: 0x000749DF
		public float Angle { get; private set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x000767E8 File Offset: 0x000749E8
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x000767F0 File Offset: 0x000749F0
		public bool IsPrimary { get; private set; }

		// Token: 0x060021CC RID: 8652 RVA: 0x000767F9 File Offset: 0x000749F9
		public CompassMarker(string id, float angle, bool isPrimary)
		{
			this.Id = id;
			this.Angle = angle % 360f;
			this.IsPrimary = isPrimary;
		}
	}
}
