using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002A RID: 42
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class CustomGameServerListResponse : FunctionResult
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002AEC File Offset: 0x00000CEC
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002AF4 File Offset: 0x00000CF4
		[JsonProperty]
		public AvailableCustomGames AvailableCustomGames { get; private set; }

		// Token: 0x060000E8 RID: 232 RVA: 0x00002AFD File Offset: 0x00000CFD
		public CustomGameServerListResponse()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002B05 File Offset: 0x00000D05
		public CustomGameServerListResponse(AvailableCustomGames availableCustomGames)
		{
			this.AvailableCustomGames = availableCustomGames;
		}
	}
}
