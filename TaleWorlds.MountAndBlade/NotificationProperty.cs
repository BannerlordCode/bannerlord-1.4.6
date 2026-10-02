using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000320 RID: 800
	[AttributeUsage(AttributeTargets.Field)]
	public class NotificationProperty : Attribute
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000AFF11 File Offset: 0x000AE111
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x000AFF19 File Offset: 0x000AE119
		public string StringId { get; private set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000AFF22 File Offset: 0x000AE122
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x000AFF2A File Offset: 0x000AE12A
		public string SoundIdOne { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x000AFF33 File Offset: 0x000AE133
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x000AFF3B File Offset: 0x000AE13B
		public string SoundIdTwo { get; private set; }

		// Token: 0x06002D8C RID: 11660 RVA: 0x000AFF44 File Offset: 0x000AE144
		public NotificationProperty(string stringId, string soundIdOne, string soundIdTwo = "")
		{
			this.StringId = stringId;
			this.SoundIdOne = soundIdOne;
			this.SoundIdTwo = soundIdTwo;
		}
	}
}
