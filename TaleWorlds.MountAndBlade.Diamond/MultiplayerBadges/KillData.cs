using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000168 RID: 360
	public struct KillData
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0000FF50 File Offset: 0x0000E150
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0000FF58 File Offset: 0x0000E158
		public PlayerId KillerId { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0000FF61 File Offset: 0x0000E161
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0000FF69 File Offset: 0x0000E169
		public PlayerId VictimId { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0000FF72 File Offset: 0x0000E172
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0000FF7A File Offset: 0x0000E17A
		public string KillerFaction { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000FF83 File Offset: 0x0000E183
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0000FF8B File Offset: 0x0000E18B
		public string VictimFaction { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000FF94 File Offset: 0x0000E194
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0000FF9C File Offset: 0x0000E19C
		public string KillerTroop { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0000FFA5 File Offset: 0x0000E1A5
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0000FFAD File Offset: 0x0000E1AD
		public string VictimTroop { get; set; }
	}
}
