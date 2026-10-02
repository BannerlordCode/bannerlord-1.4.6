using System;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Map.Cheat
{
	// Token: 0x0200004F RID: 79
	public class CheatGroupItemVM : CheatItemBaseVM
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00012CB3 File Offset: 0x00010EB3
		public CheatGroupItemVM(GameplayCheatGroup cheatGroup, Action<CheatGroupItemVM> onSelectCheatGroup)
		{
			this.CheatGroup = cheatGroup;
			this._onSelectCheatGroup = onSelectCheatGroup;
			this.RefreshValues();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00012CCF File Offset: 0x00010ECF
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject name = this.CheatGroup.GetName();
			base.Name = ((name != null) ? name.ToString() : null);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public override void ExecuteAction()
		{
			Action<CheatGroupItemVM> onSelectCheatGroup = this._onSelectCheatGroup;
			if (onSelectCheatGroup == null)
			{
				return;
			}
			onSelectCheatGroup(this);
		}

		// Token: 0x0400026B RID: 619
		public readonly GameplayCheatGroup CheatGroup;

		// Token: 0x0400026C RID: 620
		private readonly Action<CheatGroupItemVM> _onSelectCheatGroup;
	}
}
