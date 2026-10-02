using System;
using TaleWorlds.Core.ViewModelCollection.Selector;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000E0 RID: 224
	public class EncyclopediaListSelectorVM : SelectorVM<EncyclopediaListSelectorItemVM>
	{
		// Token: 0x0600155F RID: 5471 RVA: 0x000547C3 File Offset: 0x000529C3
		public EncyclopediaListSelectorVM(int selectedIndex, Action<SelectorVM<EncyclopediaListSelectorItemVM>> onChange, Action onActivate)
			: base(selectedIndex, onChange)
		{
			this._onActivate = onActivate;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000547D4 File Offset: 0x000529D4
		public void ExecuteOnDropdownActivated()
		{
			Action onActivate = this._onActivate;
			if (onActivate == null)
			{
				return;
			}
			onActivate();
		}

		// Token: 0x040009BD RID: 2493
		private Action _onActivate;
	}
}
