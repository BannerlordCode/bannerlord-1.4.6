using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x0200000B RID: 11
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager", false)]
	[Serializable]
	public class UpdateCustomGameData : Message
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002530 File Offset: 0x00000730
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002538 File Offset: 0x00000738
		[JsonProperty]
		public string NewGameType { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002541 File Offset: 0x00000741
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002549 File Offset: 0x00000749
		[JsonProperty]
		public string NewMap { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002552 File Offset: 0x00000752
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000255A File Offset: 0x0000075A
		[JsonProperty]
		public int NewMaxNumberOfPlayers { get; private set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00002563 File Offset: 0x00000763
		public UpdateCustomGameData()
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000256B File Offset: 0x0000076B
		public UpdateCustomGameData(string newGameType, string newMap, int newMaxNumberOfPlayers)
		{
			this.NewGameType = newGameType;
			this.NewMap = newMap;
			this.NewMaxNumberOfPlayers = newMaxNumberOfPlayers;
		}
	}
}
