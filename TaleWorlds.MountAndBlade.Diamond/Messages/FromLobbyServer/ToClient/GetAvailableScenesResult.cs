using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class GetAvailableScenesResult : FunctionResult
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00002C42 File Offset: 0x00000E42
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00002C4A File Offset: 0x00000E4A
		[JsonProperty]
		public AvailableScenes AvailableScenes { get; private set; }

		// Token: 0x06000109 RID: 265 RVA: 0x00002C53 File Offset: 0x00000E53
		public GetAvailableScenesResult()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002C5B File Offset: 0x00000E5B
		public GetAvailableScenesResult(AvailableScenes scenes)
		{
			this.AvailableScenes = scenes;
		}
	}
}
