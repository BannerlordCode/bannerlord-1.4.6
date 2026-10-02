using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class PlayerRemovedFromMatchmakerGame : Message
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003430 File Offset: 0x00001630
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00003438 File Offset: 0x00001638
		[JsonProperty]
		public DisconnectType DisconnectType { get; private set; }

		// Token: 0x060001CA RID: 458 RVA: 0x00003441 File Offset: 0x00001641
		public PlayerRemovedFromMatchmakerGame()
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00003449 File Offset: 0x00001649
		public PlayerRemovedFromMatchmakerGame(DisconnectType disconnectType)
		{
			this.DisconnectType = disconnectType;
		}
	}
}
