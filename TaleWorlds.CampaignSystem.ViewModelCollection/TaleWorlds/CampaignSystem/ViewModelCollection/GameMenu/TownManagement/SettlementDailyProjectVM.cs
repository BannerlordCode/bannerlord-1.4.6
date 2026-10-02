using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x020000A3 RID: 163
	public class SettlementDailyProjectVM : SettlementProjectVM
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x00041803 File Offset: 0x0003FA03
		public SettlementDailyProjectVM(Action<SettlementProjectVM, bool> onSelection, Action<SettlementProjectVM> onSetAsCurrent, Action onResetCurrent, Building building, Settlement settlement)
			: base(onSelection, onSetAsCurrent, onResetCurrent, building, settlement)
		{
			base.IsDaily = true;
			this.RefreshValues();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0004181F File Offset: 0x0003FA1F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DefaultText = GameTexts.FindText("str_default", null).ToString();
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0004183D File Offset: 0x0003FA3D
		public override void RefreshProductionText()
		{
			base.RefreshProductionText();
			base.ProductionText = new TextObject("{=bd7oAQq6}Daily", null).ToString();
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0004185B File Offset: 0x0003FA5B
		public override void ExecuteAddRemoveToQueue()
		{
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0004185D File Offset: 0x0003FA5D
		public override void ExecuteSetAsActiveDevelopment()
		{
			this._onSelection(this, false);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004186C File Offset: 0x0003FA6C
		public override void ExecuteSetAsCurrent()
		{
			Action<SettlementProjectVM> onSetAsCurrent = this._onSetAsCurrent;
			if (onSetAsCurrent == null)
			{
				return;
			}
			onSetAsCurrent(this);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0004187F File Offset: 0x0003FA7F
		public override void ExecuteResetCurrent()
		{
			Action onResetCurrent = this._onResetCurrent;
			if (onResetCurrent == null)
			{
				return;
			}
			onResetCurrent();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00041891 File Offset: 0x0003FA91
		public override void ExecuteToggleSelected()
		{
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00041893 File Offset: 0x0003FA93
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x0004189B File Offset: 0x0003FA9B
		[DataSourceProperty]
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
			set
			{
				if (value != this._isDefault)
				{
					this._isDefault = value;
					base.OnPropertyChangedWithValue(value, "IsDefault");
				}
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000418B9 File Offset: 0x0003FAB9
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x000418C1 File Offset: 0x0003FAC1
		[DataSourceProperty]
		public string DefaultText
		{
			get
			{
				return this._defaultText;
			}
			set
			{
				if (value != this._defaultText)
				{
					this._defaultText = value;
					base.OnPropertyChangedWithValue<string>(value, "DefaultText");
				}
			}
		}

		// Token: 0x04000741 RID: 1857
		private bool _isDefault;

		// Token: 0x04000742 RID: 1858
		private string _defaultText;
	}
}
