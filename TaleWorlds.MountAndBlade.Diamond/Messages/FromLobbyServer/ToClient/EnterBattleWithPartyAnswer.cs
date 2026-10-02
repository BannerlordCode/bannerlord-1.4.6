using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002C RID: 44
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class EnterBattleWithPartyAnswer : Message
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002B1C File Offset: 0x00000D1C
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002B24 File Offset: 0x00000D24
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002B2D File Offset: 0x00000D2D
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00002B35 File Offset: 0x00000D35
		[JsonProperty]
		public string[] SelectedAndEnabledGameTypes { get; private set; }

		// Token: 0x060000EF RID: 239 RVA: 0x00002B3E File Offset: 0x00000D3E
		public EnterBattleWithPartyAnswer()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002B46 File Offset: 0x00000D46
		public EnterBattleWithPartyAnswer(bool successful, string[] selectedAndEnabledGameTypes)
		{
			this.Successful = successful;
			this.SelectedAndEnabledGameTypes = selectedAndEnabledGameTypes;
		}
	}
}
