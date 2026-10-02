using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012A RID: 298
	[Serializable]
	public class JoinGameData
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0000BAFF File Offset: 0x00009CFF
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0000BB07 File Offset: 0x00009D07
		public GameServerProperties GameServerProperties { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0000BB10 File Offset: 0x00009D10
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0000BB18 File Offset: 0x00009D18
		public int PeerIndex { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0000BB21 File Offset: 0x00009D21
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0000BB29 File Offset: 0x00009D29
		public int SessionKey { get; set; }

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000BB32 File Offset: 0x00009D32
		public JoinGameData()
		{
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000BB3A File Offset: 0x00009D3A
		public JoinGameData(GameServerProperties gameServerProperties, int peerIndex, int sessionKey)
		{
			this.GameServerProperties = gameServerProperties;
			this.PeerIndex = peerIndex;
			this.SessionKey = sessionKey;
		}
	}
}
