using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class GetAnotherPlayerStateMessageResult : FunctionResult
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002C14 File Offset: 0x00000E14
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002C1C File Offset: 0x00000E1C
		[JsonProperty]
		public AnotherPlayerData AnotherPlayerData { get; private set; }

		// Token: 0x06000105 RID: 261 RVA: 0x00002C25 File Offset: 0x00000E25
		public GetAnotherPlayerStateMessageResult()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002C2D File Offset: 0x00000E2D
		public GetAnotherPlayerStateMessageResult(AnotherPlayerState anotherPlayerState, int anotherPlayerExperience)
		{
			this.AnotherPlayerData = new AnotherPlayerData(anotherPlayerState, anotherPlayerExperience);
		}
	}
}
