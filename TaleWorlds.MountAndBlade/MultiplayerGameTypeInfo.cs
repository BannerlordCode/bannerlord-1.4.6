using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031B RID: 795
	public class MultiplayerGameTypeInfo
	{
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000AF7D0 File Offset: 0x000AD9D0
		// (set) Token: 0x06002D39 RID: 11577 RVA: 0x000AF7D8 File Offset: 0x000AD9D8
		public string GameModule { get; private set; }

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000AF7E1 File Offset: 0x000AD9E1
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x000AF7E9 File Offset: 0x000AD9E9
		public string GameType { get; private set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000AF7F2 File Offset: 0x000AD9F2
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x000AF7FA File Offset: 0x000AD9FA
		public List<string> Scenes { get; private set; }

		// Token: 0x06002D3E RID: 11582 RVA: 0x000AF803 File Offset: 0x000ADA03
		public MultiplayerGameTypeInfo(string gameModule, string gameType)
		{
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.Scenes = new List<string>();
		}
	}
}
