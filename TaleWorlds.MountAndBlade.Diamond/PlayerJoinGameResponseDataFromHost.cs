using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class PlayerJoinGameResponseDataFromHost
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0000B868 File Offset: 0x00009A68
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0000B870 File Offset: 0x00009A70
		public PlayerId PlayerId { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000B879 File Offset: 0x00009A79
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0000B881 File Offset: 0x00009A81
		public int PeerIndex { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000B88A File Offset: 0x00009A8A
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0000B892 File Offset: 0x00009A92
		public int SessionKey { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000B89B File Offset: 0x00009A9B
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		public bool IsAdmin { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000B8AC File Offset: 0x00009AAC
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		public CustomGameJoinResponse CustomGameJoinResponse { get; set; }
	}
}
