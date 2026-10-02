using System;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000017 RID: 23
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class BattleServerLostMessage : Message
	{
	}
}
