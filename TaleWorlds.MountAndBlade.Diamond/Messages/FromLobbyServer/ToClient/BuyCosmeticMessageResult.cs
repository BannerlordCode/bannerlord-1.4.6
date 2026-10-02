using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class BuyCosmeticMessageResult : FunctionResult
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000027C3 File Offset: 0x000009C3
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000027CB File Offset: 0x000009CB
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000027DC File Offset: 0x000009DC
		[JsonProperty]
		public int Gold { get; private set; }

		// Token: 0x0600009C RID: 156 RVA: 0x000027E5 File Offset: 0x000009E5
		public BuyCosmeticMessageResult()
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000027ED File Offset: 0x000009ED
		public BuyCosmeticMessageResult(bool successful, int gold)
		{
			this.Successful = successful;
			this.Gold = gold;
		}
	}
}
