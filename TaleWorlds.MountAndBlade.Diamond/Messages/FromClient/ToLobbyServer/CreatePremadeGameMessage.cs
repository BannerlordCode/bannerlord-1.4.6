using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000086 RID: 134
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class CreatePremadeGameMessage : Message
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00003C32 File Offset: 0x00001E32
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00003C3A File Offset: 0x00001E3A
		[JsonProperty]
		public string PremadeGameName { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00003C43 File Offset: 0x00001E43
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00003C4B File Offset: 0x00001E4B
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00003C54 File Offset: 0x00001E54
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00003C5C File Offset: 0x00001E5C
		[JsonProperty]
		public string MapName { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00003C65 File Offset: 0x00001E65
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00003C6D File Offset: 0x00001E6D
		[JsonProperty]
		public string FactionA { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003C76 File Offset: 0x00001E76
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00003C7E File Offset: 0x00001E7E
		[JsonProperty]
		public string FactionB { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00003C87 File Offset: 0x00001E87
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00003C8F File Offset: 0x00001E8F
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00003C98 File Offset: 0x00001E98
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00003CA0 File Offset: 0x00001EA0
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x06000298 RID: 664 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public CreatePremadeGameMessage()
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00003CB1 File Offset: 0x00001EB1
		public CreatePremadeGameMessage(string premadeGameName, string gameType, string mapName, string factionA, string factionB, string password, PremadeGameType premadeGameType)
		{
			this.PremadeGameName = premadeGameName;
			this.GameType = gameType;
			this.MapName = mapName;
			this.FactionA = factionA;
			this.FactionB = factionB;
			this.Password = password;
			this.PremadeGameType = premadeGameType;
		}
	}
}
