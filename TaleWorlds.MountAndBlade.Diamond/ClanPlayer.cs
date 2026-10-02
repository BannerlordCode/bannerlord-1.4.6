using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public class ClanPlayer
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x000072FE File Offset: 0x000054FE
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00007306 File Offset: 0x00005506
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000730F File Offset: 0x0000550F
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00007317 File Offset: 0x00005517
		[JsonProperty]
		public Guid ClanId { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00007320 File Offset: 0x00005520
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00007328 File Offset: 0x00005528
		[JsonProperty]
		public ClanPlayerRole Role { get; private set; }

		// Token: 0x060005C5 RID: 1477 RVA: 0x00007331 File Offset: 0x00005531
		public ClanPlayer(PlayerId playerId, Guid clanId, ClanPlayerRole role)
		{
			this.PlayerId = playerId;
			this.ClanId = clanId;
			this.Role = role;
		}
	}
}
