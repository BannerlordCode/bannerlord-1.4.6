using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C8 RID: 200
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class UpdateUsingClanSigil : Message
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00004786 File Offset: 0x00002986
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000478E File Offset: 0x0000298E
		[JsonProperty]
		public bool IsUsed { get; private set; }

		// Token: 0x0600039F RID: 927 RVA: 0x00004797 File Offset: 0x00002997
		public UpdateUsingClanSigil()
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000479F File Offset: 0x0000299F
		public UpdateUsingClanSigil(bool isUsed)
		{
			this.IsUsed = isUsed;
		}
	}
}
