using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu
{
	// Token: 0x0200004B RID: 75
	public class InitialMenuOptionVM : ViewModel
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x000179A2 File Offset: 0x00015BA2
		public InitialMenuOptionVM(InitialStateOption initialStateOption)
		{
			this.InitialStateOption = initialStateOption;
			this.DisabledHint = new HintViewModel(initialStateOption.IsDisabledAndReason().Item2, null);
			this.EnabledHint = new HintViewModel(initialStateOption.EnabledHint, null);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000179E0 File Offset: 0x00015BE0
		public void ExecuteAction()
		{
			InitialState initialState = GameStateManager.Current.ActiveState as InitialState;
			if (initialState != null)
			{
				initialState.OnExecutedInitialStateOption(this.InitialStateOption);
				this.InitialStateOption.DoAction();
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00017A17 File Offset: 0x00015C17
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DisabledHint.HintText = this.InitialStateOption.IsDisabledAndReason().Item2;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00017A3F File Offset: 0x00015C3F
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00017A47 File Offset: 0x00015C47
		[DataSourceProperty]
		public HintViewModel DisabledHint
		{
			get
			{
				return this._disabledHint;
			}
			set
			{
				if (value != this._disabledHint)
				{
					this._disabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledHint");
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00017A65 File Offset: 0x00015C65
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00017A6D File Offset: 0x00015C6D
		[DataSourceProperty]
		public HintViewModel EnabledHint
		{
			get
			{
				return this._enabledHint;
			}
			set
			{
				if (value != this._enabledHint)
				{
					this._enabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EnabledHint");
				}
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00017A8B File Offset: 0x00015C8B
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this.InitialStateOption.Name.ToString();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00017A9D File Offset: 0x00015C9D
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this.InitialStateOption.IsDisabledAndReason().Item1;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00017AB4 File Offset: 0x00015CB4
		[DataSourceProperty]
		public bool IsHidden
		{
			get
			{
				Func<bool> isHidden = this.InitialStateOption.IsHidden;
				return isHidden != null && isHidden();
			}
		}

		// Token: 0x040002D2 RID: 722
		public readonly InitialStateOption InitialStateOption;

		// Token: 0x040002D3 RID: 723
		private HintViewModel _disabledHint;

		// Token: 0x040002D4 RID: 724
		private HintViewModel _enabledHint;
	}
}
