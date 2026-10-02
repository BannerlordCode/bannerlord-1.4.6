using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200012A RID: 298
	public class ClanPartyBehaviorSelectorVM : SelectorVM<SelectorItemVM>
	{
		// Token: 0x06001C05 RID: 7173 RVA: 0x00067CD8 File Offset: 0x00065ED8
		public ClanPartyBehaviorSelectorVM(int selectedIndex, Action<SelectorVM<SelectorItemVM>> onChange)
			: base(selectedIndex, onChange)
		{
			this.ActionsDisabledHint = new HintViewModel();
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00067CED File Offset: 0x00065EED
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x00067CF5 File Offset: 0x00065EF5
		[DataSourceProperty]
		public bool CanUseActions
		{
			get
			{
				return this._canUseActions;
			}
			set
			{
				if (value != this._canUseActions)
				{
					this._canUseActions = value;
					base.OnPropertyChangedWithValue(value, "CanUseActions");
				}
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00067D13 File Offset: 0x00065F13
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x00067D1B File Offset: 0x00065F1B
		[DataSourceProperty]
		public HintViewModel ActionsDisabledHint
		{
			get
			{
				return this._actionsDisabledHint;
			}
			set
			{
				if (value != this._actionsDisabledHint)
				{
					this._actionsDisabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ActionsDisabledHint");
				}
			}
		}

		// Token: 0x04000D14 RID: 3348
		private bool _canUseActions;

		// Token: 0x04000D15 RID: 3349
		private HintViewModel _actionsDisabledHint;
	}
}
