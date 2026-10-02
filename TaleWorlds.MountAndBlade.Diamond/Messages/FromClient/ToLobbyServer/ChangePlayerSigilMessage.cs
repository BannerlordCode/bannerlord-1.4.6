using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007E RID: 126
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ChangePlayerSigilMessage : Message
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00003AC9 File Offset: 0x00001CC9
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00003AD1 File Offset: 0x00001CD1
		[JsonProperty]
		public string SigilId { get; private set; }

		// Token: 0x06000269 RID: 617 RVA: 0x00003ADA File Offset: 0x00001CDA
		public ChangePlayerSigilMessage()
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00003AE2 File Offset: 0x00001CE2
		public ChangePlayerSigilMessage(string sigilId)
		{
			this.SigilId = sigilId;
		}
	}
}
