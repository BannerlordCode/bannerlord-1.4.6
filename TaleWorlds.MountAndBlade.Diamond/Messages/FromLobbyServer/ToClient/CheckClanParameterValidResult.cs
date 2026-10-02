using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class CheckClanParameterValidResult : FunctionResult
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002833 File Offset: 0x00000A33
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000283B File Offset: 0x00000A3B
		[JsonProperty]
		public bool IsValid { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002844 File Offset: 0x00000A44
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000284C File Offset: 0x00000A4C
		[JsonProperty]
		public StringValidationError Error { get; private set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00002855 File Offset: 0x00000A55
		public CheckClanParameterValidResult()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000285D File Offset: 0x00000A5D
		public CheckClanParameterValidResult(bool isValid, StringValidationError error)
		{
			this.IsValid = isValid;
			this.Error = error;
		}
	}
}
