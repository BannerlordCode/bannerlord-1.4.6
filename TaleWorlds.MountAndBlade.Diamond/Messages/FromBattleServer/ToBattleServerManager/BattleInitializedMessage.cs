using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D0 RID: 208
	[MessageDescription("BattleServer", "BattleServerManager", true)]
	[Serializable]
	public class BattleInitializedMessage : Message
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00004984 File Offset: 0x00002B84
		[JsonProperty]
		public string GameType { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000498C File Offset: 0x00002B8C
		[JsonProperty]
		public List<PlayerId> AssignedPlayers { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00004994 File Offset: 0x00002B94
		[JsonProperty]
		public string Faction1 { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000499C File Offset: 0x00002B9C
		[JsonProperty]
		public string Faction2 { get; }

		// Token: 0x060003C9 RID: 969 RVA: 0x000049A4 File Offset: 0x00002BA4
		public BattleInitializedMessage()
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000049AC File Offset: 0x00002BAC
		public BattleInitializedMessage(string gameType, List<PlayerId> assignedPlayers, string faction1, string faction2)
		{
			this.GameType = gameType;
			this.AssignedPlayers = assignedPlayers;
			this.Faction1 = faction1;
			this.Faction2 = faction2;
		}
	}
}
