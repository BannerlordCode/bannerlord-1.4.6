using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class ClanPlayerInfo
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00007094 File Offset: 0x00005294
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0000709C File Offset: 0x0000529C
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x000070A5 File Offset: 0x000052A5
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x000070AD File Offset: 0x000052AD
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000070B6 File Offset: 0x000052B6
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000070BE File Offset: 0x000052BE
		[JsonProperty]
		public AnotherPlayerState State { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000070C7 File Offset: 0x000052C7
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000070CF File Offset: 0x000052CF
		[JsonProperty]
		public string ActiveBadgeId { get; private set; }

		// Token: 0x06000597 RID: 1431 RVA: 0x000070D8 File Offset: 0x000052D8
		public ClanPlayerInfo(PlayerId playerId, string playerName, AnotherPlayerState anotherPlayerState, string activeBadgeId)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.ActiveBadgeId = activeBadgeId;
			this.State = anotherPlayerState;
		}
	}
}
