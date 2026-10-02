using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B7 RID: 183
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RegisterCustomGameMessage : Message
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00004321 File Offset: 0x00002521
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00004329 File Offset: 0x00002529
		[JsonProperty]
		public string GameModule { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00004332 File Offset: 0x00002532
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000433A File Offset: 0x0000253A
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00004343 File Offset: 0x00002543
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000434B File Offset: 0x0000254B
		[JsonProperty]
		public string ServerName { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00004354 File Offset: 0x00002554
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000435C File Offset: 0x0000255C
		[JsonProperty]
		public int MaxPlayerCount { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00004365 File Offset: 0x00002565
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000436D File Offset: 0x0000256D
		[JsonProperty]
		public string Map { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00004376 File Offset: 0x00002576
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000437E File Offset: 0x0000257E
		[JsonProperty]
		public string UniqueMapId { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00004387 File Offset: 0x00002587
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000438F File Offset: 0x0000258F
		[JsonProperty]
		public int Port { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00004398 File Offset: 0x00002598
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000043A0 File Offset: 0x000025A0
		[JsonProperty]
		public string GamePassword { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000043A9 File Offset: 0x000025A9
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000043B1 File Offset: 0x000025B1
		[JsonProperty]
		public string AdminPassword { get; private set; }

		// Token: 0x06000348 RID: 840 RVA: 0x000043BA File Offset: 0x000025BA
		public RegisterCustomGameMessage()
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000043C4 File Offset: 0x000025C4
		public RegisterCustomGameMessage(string gameModule, string gameType, string serverName, int maxPlayerCount, string map, string uniqueMapId, string gamePassword, string adminPassword, int port)
		{
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.ServerName = serverName;
			this.MaxPlayerCount = maxPlayerCount;
			this.Map = map;
			this.UniqueMapId = uniqueMapId;
			this.GamePassword = gamePassword;
			this.AdminPassword = adminPassword;
			this.Port = port;
		}
	}
}
