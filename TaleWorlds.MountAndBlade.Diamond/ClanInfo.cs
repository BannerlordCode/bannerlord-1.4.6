using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public class ClanInfo
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000070FD File Offset: 0x000052FD
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00007105 File Offset: 0x00005305
		[JsonProperty]
		public Guid ClanId { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000710E File Offset: 0x0000530E
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00007116 File Offset: 0x00005316
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000711F File Offset: 0x0000531F
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00007127 File Offset: 0x00005327
		[JsonProperty]
		public string Tag { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00007130 File Offset: 0x00005330
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00007138 File Offset: 0x00005338
		[JsonProperty]
		public string Faction { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00007141 File Offset: 0x00005341
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x00007149 File Offset: 0x00005349
		[JsonProperty]
		public string Sigil { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00007152 File Offset: 0x00005352
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000715A File Offset: 0x0000535A
		[JsonProperty]
		public string InformationText { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00007163 File Offset: 0x00005363
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000716B File Offset: 0x0000536B
		[JsonProperty]
		public ClanPlayer[] Players { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00007174 File Offset: 0x00005374
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000717C File Offset: 0x0000537C
		[JsonProperty]
		public ClanAnnouncement[] Announcements { get; private set; }

		// Token: 0x060005A8 RID: 1448 RVA: 0x00007188 File Offset: 0x00005388
		public ClanInfo(Guid clanId, string name, string tag, string faction, string sigil, string information, ClanPlayer[] players, ClanAnnouncement[] announcements)
		{
			this.ClanId = clanId;
			this.Name = name;
			this.Tag = tag;
			this.Faction = faction;
			this.Sigil = sigil;
			this.Players = players;
			this.InformationText = information;
			this.Announcements = announcements;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000071D8 File Offset: 0x000053D8
		public static ClanInfo CreateUnavailableClanInfo()
		{
			return new ClanInfo(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new ClanPlayer[0], new ClanAnnouncement[0]);
		}
	}
}
