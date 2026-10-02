using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000049 RID: 73
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class JoinBattleMessage : Message
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000030CD File Offset: 0x000012CD
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000030D5 File Offset: 0x000012D5
		[JsonProperty]
		public BattleServerInformationForClient BattleServerInformation { get; private set; }

		// Token: 0x06000179 RID: 377 RVA: 0x000030DE File Offset: 0x000012DE
		public JoinBattleMessage()
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000030E6 File Offset: 0x000012E6
		public JoinBattleMessage(BattleServerInformationForClient battleServerInformation)
		{
			this.BattleServerInformation = battleServerInformation;
		}
	}
}
