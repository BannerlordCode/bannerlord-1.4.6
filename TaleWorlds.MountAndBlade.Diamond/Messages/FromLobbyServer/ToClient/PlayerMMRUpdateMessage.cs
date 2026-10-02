using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Ranked;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000057 RID: 87
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class PlayerMMRUpdateMessage : Message
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000033C0 File Offset: 0x000015C0
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000033C8 File Offset: 0x000015C8
		[JsonProperty]
		public RankBarInfo OldInfo { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000033D1 File Offset: 0x000015D1
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000033D9 File Offset: 0x000015D9
		[JsonProperty]
		public RankBarInfo NewInfo { get; private set; }

		// Token: 0x060001C1 RID: 449 RVA: 0x000033E2 File Offset: 0x000015E2
		public PlayerMMRUpdateMessage()
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000033EA File Offset: 0x000015EA
		public PlayerMMRUpdateMessage(RankBarInfo oldInfo, RankBarInfo newInfo)
		{
			this.OldInfo = oldInfo;
			this.NewInfo = newInfo;
		}
	}
}
