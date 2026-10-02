using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000301 RID: 769
	public class IntermissionVoteItem
	{
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000A8495 File Offset: 0x000A6695
		// (set) Token: 0x06002BCF RID: 11215 RVA: 0x000A849D File Offset: 0x000A669D
		public int VoteCount { get; private set; }

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000A84A6 File Offset: 0x000A66A6
		public IntermissionVoteItem(string id, int index)
		{
			this.Id = id;
			this.Index = index;
			this.VoteCount = 0;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000A84C3 File Offset: 0x000A66C3
		public void SetVoteCount(int voteCount)
		{
			this.VoteCount = voteCount;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000A84CC File Offset: 0x000A66CC
		public void IncreaseVoteCount(int incrementAmount)
		{
			this.VoteCount += incrementAmount;
		}

		// Token: 0x04001146 RID: 4422
		public readonly string Id;

		// Token: 0x04001147 RID: 4423
		public readonly int Index;
	}
}
