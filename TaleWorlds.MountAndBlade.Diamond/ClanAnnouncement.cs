using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class ClanAnnouncement
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00006E36 File Offset: 0x00005036
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00006E3E File Offset: 0x0000503E
		[JsonProperty]
		public int Id { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00006E47 File Offset: 0x00005047
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00006E4F File Offset: 0x0000504F
		[JsonProperty]
		public string Announcement { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00006E58 File Offset: 0x00005058
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00006E60 File Offset: 0x00005060
		[JsonProperty]
		public PlayerId AuthorId { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00006E69 File Offset: 0x00005069
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00006E71 File Offset: 0x00005071
		[JsonProperty]
		public DateTime CreationTime { get; private set; }

		// Token: 0x0600056A RID: 1386 RVA: 0x00006E7A File Offset: 0x0000507A
		public ClanAnnouncement(int id, string announcement, PlayerId authorId, DateTime creationTime)
		{
			this.Id = id;
			this.Announcement = announcement;
			this.AuthorId = authorId;
			this.CreationTime = creationTime;
		}
	}
}
