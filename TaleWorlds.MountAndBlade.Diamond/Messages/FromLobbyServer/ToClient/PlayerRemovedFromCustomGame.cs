using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class PlayerRemovedFromCustomGame : Message
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00003408 File Offset: 0x00001608
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00003410 File Offset: 0x00001610
		[JsonProperty]
		public DisconnectType DisconnectType { get; private set; }

		// Token: 0x060001C6 RID: 454 RVA: 0x00003419 File Offset: 0x00001619
		public PlayerRemovedFromCustomGame()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00003421 File Offset: 0x00001621
		public PlayerRemovedFromCustomGame(DisconnectType disconnectType)
		{
			this.DisconnectType = disconnectType;
		}
	}
}
