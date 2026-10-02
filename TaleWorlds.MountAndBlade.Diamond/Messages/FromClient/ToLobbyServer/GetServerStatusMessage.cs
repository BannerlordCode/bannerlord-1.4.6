using System;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A8 RID: 168
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetServerStatusMessage : Message
	{
	}
}
