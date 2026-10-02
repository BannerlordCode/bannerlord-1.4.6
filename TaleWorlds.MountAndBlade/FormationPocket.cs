using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000220 RID: 544
	public class FormationPocket
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x00072471 File Offset: 0x00070671
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x00072479 File Offset: 0x00070679
		public Func<Agent, int> PriorityFunction { get; private set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00072482 File Offset: 0x00070682
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x0007248A File Offset: 0x0007068A
		public int MaxValue { get; private set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x00072493 File Offset: 0x00070693
		// (set) Token: 0x06002083 RID: 8323 RVA: 0x0007249B File Offset: 0x0007069B
		public int TroopCount { get; private set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x000724A4 File Offset: 0x000706A4
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x000724AC File Offset: 0x000706AC
		public int Index { get; private set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000724B5 File Offset: 0x000706B5
		// (set) Token: 0x06002087 RID: 8327 RVA: 0x000724BD File Offset: 0x000706BD
		public int AddedTroopCount { get; private set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x000724C6 File Offset: 0x000706C6
		// (set) Token: 0x06002089 RID: 8329 RVA: 0x000724CE File Offset: 0x000706CE
		public int ScoreToSeek { get; private set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x000724D7 File Offset: 0x000706D7
		// (set) Token: 0x0600208B RID: 8331 RVA: 0x000724DF File Offset: 0x000706DF
		public int BestScoreSoFar { get; private set; }

		// Token: 0x0600208C RID: 8332 RVA: 0x000724E8 File Offset: 0x000706E8
		public FormationPocket(Func<Agent, int> priorityFunction, int maxValue, int troopCount, int index)
		{
			this.PriorityFunction = priorityFunction;
			this.MaxValue = maxValue;
			this.TroopCount = troopCount;
			this.Index = index;
			this.AddedTroopCount = 0;
			this.ScoreToSeek = maxValue;
			this.BestScoreSoFar = 0;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00072524 File Offset: 0x00070724
		public void AddTroop()
		{
			int addedTroopCount = this.AddedTroopCount;
			this.AddedTroopCount = addedTroopCount + 1;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00072541 File Offset: 0x00070741
		public bool IsFormationPocketFilled()
		{
			return this.AddedTroopCount >= this.TroopCount;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00072554 File Offset: 0x00070754
		public void UpdateScoreToSeek()
		{
			this.ScoreToSeek = this.BestScoreSoFar;
			this.BestScoreSoFar = 0;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00072569 File Offset: 0x00070769
		public void SetBestScoreSoFar(int bestScoreSoFar)
		{
			this.BestScoreSoFar = bestScoreSoFar;
		}
	}
}
