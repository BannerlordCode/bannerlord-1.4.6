using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	public class PlayerLeaderboardData
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0000D14F File Offset: 0x0000B34F
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0000D157 File Offset: 0x0000B357
		public PlayerId PlayerId { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0000D160 File Offset: 0x0000B360
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0000D168 File Offset: 0x0000B368
		public string RankId { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0000D171 File Offset: 0x0000B371
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0000D179 File Offset: 0x0000B379
		public int Rating { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0000D182 File Offset: 0x0000B382
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0000D18A File Offset: 0x0000B38A
		public string Name { get; set; }

		// Token: 0x060008ED RID: 2285 RVA: 0x0000D193 File Offset: 0x0000B393
		public PlayerLeaderboardData(PlayerId playerId, string rankId, int rating, string name)
		{
			this.PlayerId = playerId;
			this.RankId = rankId;
			this.Rating = rating;
			this.Name = name;
		}
	}
}
