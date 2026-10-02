using System;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000054 RID: 84
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PendingBattleRejoinMessage : Message
	{
	}
}
