using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000151 RID: 337
	public class PublishedLobbyNewsArticle
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public string Title { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0000DBCD File Offset: 0x0000BDCD
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		public int Type { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0000DBDE File Offset: 0x0000BDDE
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0000DBE6 File Offset: 0x0000BDE6
		public string Description { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0000DBEF File Offset: 0x0000BDEF
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		public string DateStart { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0000DC00 File Offset: 0x0000BE00
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public string DateEnd { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0000DC11 File Offset: 0x0000BE11
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x0000DC19 File Offset: 0x0000BE19
		public bool Pinned { get; set; }
	}
}
