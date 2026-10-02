using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class UpdateUsedCosmeticItemsMessageResult : FunctionResult
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000037E9 File Offset: 0x000019E9
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000037F1 File Offset: 0x000019F1
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x06000221 RID: 545 RVA: 0x000037FA File Offset: 0x000019FA
		public UpdateUsedCosmeticItemsMessageResult()
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00003802 File Offset: 0x00001A02
		public UpdateUsedCosmeticItemsMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
