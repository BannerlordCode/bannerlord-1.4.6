using System;

namespace TaleWorlds.CampaignSystem.Party
{
	// Token: 0x02000301 RID: 769
	public struct TroopTradeDifference
	{
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000BEEF4 File Offset: 0x000BD0F4
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x000BEEFC File Offset: 0x000BD0FC
		public CharacterObject Troop { get; set; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000BEF05 File Offset: 0x000BD105
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x000BEF0D File Offset: 0x000BD10D
		public bool IsPrisoner { get; set; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x000BEF16 File Offset: 0x000BD116
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x000BEF1E File Offset: 0x000BD11E
		public int FromCount { get; set; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000BEF27 File Offset: 0x000BD127
		// (set) Token: 0x06002D2F RID: 11567 RVA: 0x000BEF2F File Offset: 0x000BD12F
		public int ToCount { get; set; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000BEF38 File Offset: 0x000BD138
		public int DifferenceCount
		{
			get
			{
				return this.FromCount - this.ToCount;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000BEF47 File Offset: 0x000BD147
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x000BEF4F File Offset: 0x000BD14F
		public bool IsEmpty { get; private set; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000BEF58 File Offset: 0x000BD158
		public static TroopTradeDifference Empty
		{
			get
			{
				return new TroopTradeDifference
				{
					IsEmpty = true
				};
			}
		}
	}
}
