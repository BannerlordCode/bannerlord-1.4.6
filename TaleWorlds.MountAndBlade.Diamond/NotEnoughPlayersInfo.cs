using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class NotEnoughPlayersInfo
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00007024 File Offset: 0x00005224
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000702C File Offset: 0x0000522C
		[JsonProperty]
		public int CurrentPlayerCount { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00007035 File Offset: 0x00005235
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000703D File Offset: 0x0000523D
		[JsonProperty]
		public int RequiredPlayerCount { get; private set; }

		// Token: 0x06000589 RID: 1417 RVA: 0x00007046 File Offset: 0x00005246
		public NotEnoughPlayersInfo(int currentPlayerCount, int requiredPlayerCount)
		{
			this.CurrentPlayerCount = currentPlayerCount;
			this.RequiredPlayerCount = requiredPlayerCount;
		}
	}
}
