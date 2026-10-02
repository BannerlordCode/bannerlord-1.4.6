using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public class PlayerBattleServerInformation
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000CA62 File Offset: 0x0000AC62
		public int PeerIndex { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0000CA73 File Offset: 0x0000AC73
		public int SessionKey { get; set; }

		// Token: 0x06000896 RID: 2198 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public PlayerBattleServerInformation()
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public PlayerBattleServerInformation(int peerIndex, int sessionKey)
		{
			this.PeerIndex = peerIndex;
			this.SessionKey = sessionKey;
		}
	}
}
