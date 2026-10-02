using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x0200000C RID: 12
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager", false)]
	[Serializable]
	public class UpdateGamePropertiesMessage : Message
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002588 File Offset: 0x00000788
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002590 File Offset: 0x00000790
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002599 File Offset: 0x00000799
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000025A1 File Offset: 0x000007A1
		[JsonProperty]
		public string Scene { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000025AA File Offset: 0x000007AA
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000025B2 File Offset: 0x000007B2
		[JsonProperty]
		public string UniqueSceneId { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x000025BB File Offset: 0x000007BB
		public UpdateGamePropertiesMessage()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000025C3 File Offset: 0x000007C3
		public UpdateGamePropertiesMessage(string gameType, string scene, string uniqueSceneId)
		{
			this.GameType = gameType;
			this.Scene = scene;
			this.UniqueSceneId = uniqueSceneId;
		}
	}
}
