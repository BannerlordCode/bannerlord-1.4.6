using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class Announcement
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000050AA File Offset: 0x000032AA
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000050B2 File Offset: 0x000032B2
		public int Id { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000050BB File Offset: 0x000032BB
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000050C3 File Offset: 0x000032C3
		public Guid BattleId { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000050CC File Offset: 0x000032CC
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000050D4 File Offset: 0x000032D4
		public AnnouncementType Type { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000050DD File Offset: 0x000032DD
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x000050E5 File Offset: 0x000032E5
		public string Text { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000050EE File Offset: 0x000032EE
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x000050F6 File Offset: 0x000032F6
		public bool IsEnabled { get; set; }

		// Token: 0x0600046F RID: 1135 RVA: 0x000050FF File Offset: 0x000032FF
		public Announcement()
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00005107 File Offset: 0x00003307
		public Announcement(int id, Guid battleId, AnnouncementType type, string text, bool isEnabled)
		{
			this.Id = id;
			this.BattleId = battleId;
			this.Type = type;
			this.Text = text;
			this.IsEnabled = isEnabled;
		}
	}
}
