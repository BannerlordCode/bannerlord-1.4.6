using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200000D RID: 13
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer", true)]
	[Serializable]
	public class ClientQuitFromCustomGameMessage : Message
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000025E0 File Offset: 0x000007E0
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000025E8 File Offset: 0x000007E8
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x0600006B RID: 107 RVA: 0x000025F1 File Offset: 0x000007F1
		public ClientQuitFromCustomGameMessage()
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000025F9 File Offset: 0x000007F9
		public ClientQuitFromCustomGameMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
