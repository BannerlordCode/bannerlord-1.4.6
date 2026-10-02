using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public class ClanCreationPlayerData
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00006F1D File Offset: 0x0000511D
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00006F25 File Offset: 0x00005125
		public PlayerSessionId PlayerSessionId { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00006F2E File Offset: 0x0000512E
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00006F36 File Offset: 0x00005136
		public ClanCreationAnswer ClanCreationAnswer { get; private set; }

		// Token: 0x06000573 RID: 1395 RVA: 0x00006F3F File Offset: 0x0000513F
		public ClanCreationPlayerData(PlayerSessionId playerSessionId, ClanCreationAnswer answer)
		{
			this.PlayerSessionId = playerSessionId;
			this.ClanCreationAnswer = answer;
		}
	}
}
