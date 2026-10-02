using System;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000172 RID: 370
	public class PlayerInfo
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00010C0D File Offset: 0x0000EE0D
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00010C15 File Offset: 0x0000EE15
		public string PlayerId { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00010C1E File Offset: 0x0000EE1E
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00010C26 File Offset: 0x0000EE26
		public string Username { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00010C2F File Offset: 0x0000EE2F
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00010C37 File Offset: 0x0000EE37
		public int ForcedIndex { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00010C40 File Offset: 0x0000EE40
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00010C48 File Offset: 0x0000EE48
		public int TeamNo { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00010C51 File Offset: 0x0000EE51
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00010C59 File Offset: 0x0000EE59
		public int Kill { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00010C62 File Offset: 0x0000EE62
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00010C6A File Offset: 0x0000EE6A
		public int Death { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00010C73 File Offset: 0x0000EE73
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00010C7B File Offset: 0x0000EE7B
		public int Assist { get; set; }

		// Token: 0x06000A6C RID: 2668 RVA: 0x00010C84 File Offset: 0x0000EE84
		public bool HasSameContentWith(PlayerInfo other)
		{
			return this.PlayerId == other.PlayerId && this.Username == other.Username && this.ForcedIndex == other.ForcedIndex && this.TeamNo == other.TeamNo && this.Kill == other.Kill && this.Death == other.Death && this.Assist == other.Assist;
		}
	}
}
