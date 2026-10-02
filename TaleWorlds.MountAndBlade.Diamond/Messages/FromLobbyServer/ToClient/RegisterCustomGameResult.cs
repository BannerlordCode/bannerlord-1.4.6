using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class RegisterCustomGameResult : FunctionResult
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000035D3 File Offset: 0x000017D3
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000035DB File Offset: 0x000017DB
		[JsonProperty]
		public bool Success { get; private set; }

		// Token: 0x060001EE RID: 494 RVA: 0x000035E4 File Offset: 0x000017E4
		public RegisterCustomGameResult()
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000035EC File Offset: 0x000017EC
		public RegisterCustomGameResult(bool success)
		{
			this.Success = success;
		}
	}
}
