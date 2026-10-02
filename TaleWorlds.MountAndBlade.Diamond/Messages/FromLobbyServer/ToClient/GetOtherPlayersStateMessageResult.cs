using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class GetOtherPlayersStateMessageResult : FunctionResult
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002D5A File Offset: 0x00000F5A
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002D62 File Offset: 0x00000F62
		[JsonProperty]
		public List<ValueTuple<PlayerId, AnotherPlayerData>> States { get; private set; }

		// Token: 0x06000125 RID: 293 RVA: 0x00002D6B File Offset: 0x00000F6B
		public GetOtherPlayersStateMessageResult()
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00002D73 File Offset: 0x00000F73
		public GetOtherPlayersStateMessageResult(List<ValueTuple<PlayerId, AnotherPlayerData>> states)
		{
			this.States = states;
		}
	}
}
