using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C4 RID: 196
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class UpdateCustomGameData : Message
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000389 RID: 905 RVA: 0x000046B6 File Offset: 0x000028B6
		// (set) Token: 0x0600038A RID: 906 RVA: 0x000046BE File Offset: 0x000028BE
		[JsonProperty]
		public string NewGameType { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000046C7 File Offset: 0x000028C7
		// (set) Token: 0x0600038C RID: 908 RVA: 0x000046CF File Offset: 0x000028CF
		[JsonProperty]
		public string NewMap { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000046D8 File Offset: 0x000028D8
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000046E0 File Offset: 0x000028E0
		[JsonProperty]
		public int NewMaxNumberOfPlayers { get; private set; }

		// Token: 0x0600038F RID: 911 RVA: 0x000046E9 File Offset: 0x000028E9
		public UpdateCustomGameData()
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000046F1 File Offset: 0x000028F1
		public UpdateCustomGameData(string newGameType, string newMap, int newMaxNumberOfPlayers)
		{
			this.NewGameType = newGameType;
			this.NewMap = newMap;
			this.NewMaxNumberOfPlayers = newMaxNumberOfPlayers;
		}
	}
}
