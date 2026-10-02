using System;
using TaleWorlds.MountAndBlade.Missions.Hints;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Missions.Interaction.InteractionItems
{
	// Token: 0x02000042 RID: 66
	internal class MissionHintInteractionItemVM : MissionInteractionItemBaseVM
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00016265 File Offset: 0x00014465
		public MissionHintInteractionItemVM(MissionHint hint)
		{
			this.Hint = hint;
			this.RefreshValues();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001627A File Offset: 0x0001447A
		public override void RefreshValues()
		{
			base.RefreshValues();
			base.Message = this.Hint.Description.ToString();
		}

		// Token: 0x0400029B RID: 667
		public readonly MissionHint Hint;
	}
}
