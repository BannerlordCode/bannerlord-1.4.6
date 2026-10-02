using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class GetUserCosmeticsInfoMessageResult : FunctionResult
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00002F12 File Offset: 0x00001112
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00002F1A File Offset: 0x0000111A
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002F23 File Offset: 0x00001123
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00002F2B File Offset: 0x0000112B
		[JsonProperty]
		public List<string> OwnedCosmetics { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002F34 File Offset: 0x00001134
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002F3C File Offset: 0x0000113C
		[JsonProperty]
		public Dictionary<string, List<string>> UsedCosmetics { get; private set; }

		// Token: 0x06000155 RID: 341 RVA: 0x00002F45 File Offset: 0x00001145
		public GetUserCosmeticsInfoMessageResult()
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002F4D File Offset: 0x0000114D
		public GetUserCosmeticsInfoMessageResult(bool successful, List<string> ownedCosmetics, Dictionary<string, List<string>> usedCosmetics)
		{
			this.Successful = successful;
			this.OwnedCosmetics = ownedCosmetics;
			this.UsedCosmetics = usedCosmetics;
		}
	}
}
