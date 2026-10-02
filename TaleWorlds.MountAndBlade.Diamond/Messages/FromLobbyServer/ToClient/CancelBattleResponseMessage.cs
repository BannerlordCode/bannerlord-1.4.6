using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000019 RID: 25
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class CancelBattleResponseMessage : Message
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002803 File Offset: 0x00000A03
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000280B File Offset: 0x00000A0B
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060000A0 RID: 160 RVA: 0x00002814 File Offset: 0x00000A14
		public CancelBattleResponseMessage()
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000281C File Offset: 0x00000A1C
		public CancelBattleResponseMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
