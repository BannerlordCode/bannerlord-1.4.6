using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Conversation.Persuasion
{
	// Token: 0x020002A4 RID: 676
	public class PersuasionTask
	{
		// Token: 0x06002438 RID: 9272 RVA: 0x0009C4BF File Offset: 0x0009A6BF
		public PersuasionTask(int reservationType)
		{
			this.Options = new MBList<PersuasionOptionArgs>();
			this.ReservationType = reservationType;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0009C4D9 File Offset: 0x0009A6D9
		public void AddOptionToTask(PersuasionOptionArgs option)
		{
			this.Options.Add(option);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0009C4E8 File Offset: 0x0009A6E8
		public void BlockAllOptions()
		{
			foreach (PersuasionOptionArgs persuasionOptionArgs in this.Options)
			{
				persuasionOptionArgs.BlockTheOption(true);
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x0009C53C File Offset: 0x0009A73C
		public void UnblockAllOptions()
		{
			foreach (PersuasionOptionArgs persuasionOptionArgs in this.Options)
			{
				persuasionOptionArgs.BlockTheOption(false);
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0009C590 File Offset: 0x0009A790
		public void ApplyEffects(float moveToNextStageChance, float blockRandomOptionChance)
		{
			if (moveToNextStageChance > MBRandom.RandomFloat)
			{
				this.BlockAllOptions();
				return;
			}
			if (blockRandomOptionChance > MBRandom.RandomFloat)
			{
				PersuasionOptionArgs randomElementWithPredicate = this.Options.GetRandomElementWithPredicate<PersuasionOptionArgs>((PersuasionOptionArgs x) => !x.IsBlocked);
				if (randomElementWithPredicate == null)
				{
					return;
				}
				randomElementWithPredicate.BlockTheOption(true);
			}
		}

		// Token: 0x04000B06 RID: 2822
		public readonly MBList<PersuasionOptionArgs> Options;

		// Token: 0x04000B07 RID: 2823
		public TextObject SpokenLine;

		// Token: 0x04000B08 RID: 2824
		public TextObject ImmediateFailLine;

		// Token: 0x04000B09 RID: 2825
		public TextObject FinalFailLine;

		// Token: 0x04000B0A RID: 2826
		public TextObject TryLaterLine;

		// Token: 0x04000B0B RID: 2827
		public readonly int ReservationType;
	}
}
