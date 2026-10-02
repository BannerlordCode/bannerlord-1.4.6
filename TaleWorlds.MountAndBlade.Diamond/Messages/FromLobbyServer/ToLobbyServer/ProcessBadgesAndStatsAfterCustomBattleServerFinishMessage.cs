using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToLobbyServer
{
	// Token: 0x0200006D RID: 109
	[MessageDescription("CustomBattleServerManager", "CustomBattleServerManager", true)]
	[Serializable]
	public class ProcessBadgesAndStatsAfterCustomBattleServerFinishMessage : Message
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00003869 File Offset: 0x00001A69
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00003871 File Offset: 0x00001A71
		[JsonProperty]
		public List<BadgeDataEntry> BadgeDataEntries { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000387A File Offset: 0x00001A7A
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00003882 File Offset: 0x00001A82
		[JsonProperty]
		public PlayerId[] PlayerIds { get; private set; }

		// Token: 0x0600022F RID: 559 RVA: 0x0000388B File Offset: 0x00001A8B
		public ProcessBadgesAndStatsAfterCustomBattleServerFinishMessage()
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00003893 File Offset: 0x00001A93
		public ProcessBadgesAndStatsAfterCustomBattleServerFinishMessage(List<BadgeDataEntry> badgeDataEntries, PlayerId[] playerIds)
		{
			this.BadgeDataEntries = badgeDataEntries;
			this.PlayerIds = playerIds;
		}
	}
}
