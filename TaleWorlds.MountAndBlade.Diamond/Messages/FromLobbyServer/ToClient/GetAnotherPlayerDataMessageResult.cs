using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	public class GetAnotherPlayerDataMessageResult : FunctionResult
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002BEC File Offset: 0x00000DEC
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00002BF4 File Offset: 0x00000DF4
		[JsonProperty]
		public PlayerData AnotherPlayerData { get; private set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00002BFD File Offset: 0x00000DFD
		public GetAnotherPlayerDataMessageResult()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002C05 File Offset: 0x00000E05
		public GetAnotherPlayerDataMessageResult(PlayerData playerData)
		{
			this.AnotherPlayerData = playerData;
		}
	}
}
