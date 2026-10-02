using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000045 RID: 69
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class InitializeSessionResponse : LoginResultObject
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002F6A File Offset: 0x0000116A
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00002F72 File Offset: 0x00001172
		[JsonProperty]
		public PlayerData PlayerData { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00002F7B File Offset: 0x0000117B
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00002F83 File Offset: 0x00001183
		[JsonProperty]
		public ServerStatus ServerStatus { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002F8C File Offset: 0x0000118C
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00002F94 File Offset: 0x00001194
		[JsonProperty]
		public AvailableScenes AvailableScenes { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002F9D File Offset: 0x0000119D
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002FA5 File Offset: 0x000011A5
		[JsonProperty]
		public SupportedFeatures SupportedFeatures { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002FAE File Offset: 0x000011AE
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002FB6 File Offset: 0x000011B6
		[JsonProperty]
		public bool HasPendingRejoin { get; private set; }

		// Token: 0x06000161 RID: 353 RVA: 0x00002FBF File Offset: 0x000011BF
		public InitializeSessionResponse()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00002FC7 File Offset: 0x000011C7
		public InitializeSessionResponse(PlayerData playerData, ServerStatus serverStatus, AvailableScenes availableScenes, SupportedFeatures supportedFeatures, bool hasPendingRejoin)
		{
			this.PlayerData = playerData;
			this.ServerStatus = serverStatus;
			this.AvailableScenes = availableScenes;
			this.SupportedFeatures = supportedFeatures;
			this.HasPendingRejoin = hasPendingRejoin;
		}
	}
}
