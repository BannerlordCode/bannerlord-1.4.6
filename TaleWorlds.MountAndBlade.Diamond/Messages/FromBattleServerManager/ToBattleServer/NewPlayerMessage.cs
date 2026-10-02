using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E0 RID: 224
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class NewPlayerMessage : Message
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00004CEF File Offset: 0x00002EEF
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00004CF7 File Offset: 0x00002EF7
		[JsonProperty]
		public PlayerBattleInfo PlayerBattleInfo { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00004D00 File Offset: 0x00002F00
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x00004D08 File Offset: 0x00002F08
		[JsonProperty]
		public PlayerData PlayerData { get; private set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00004D11 File Offset: 0x00002F11
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00004D19 File Offset: 0x00002F19
		[JsonProperty]
		public Guid PlayerParty { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00004D22 File Offset: 0x00002F22
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00004D2A File Offset: 0x00002F2A
		[JsonProperty]
		public Dictionary<string, List<string>> UsedCosmetics { get; private set; }

		// Token: 0x0600041C RID: 1052 RVA: 0x00004D33 File Offset: 0x00002F33
		public NewPlayerMessage()
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00004D3B File Offset: 0x00002F3B
		public NewPlayerMessage(PlayerData playerData, PlayerBattleInfo playerBattleInfo, Guid playerParty, Dictionary<string, List<string>> usedCosmetics)
		{
			this.PlayerBattleInfo = playerBattleInfo;
			this.PlayerData = playerData;
			this.PlayerParty = playerParty;
			this.UsedCosmetics = usedCosmetics;
		}
	}
}
