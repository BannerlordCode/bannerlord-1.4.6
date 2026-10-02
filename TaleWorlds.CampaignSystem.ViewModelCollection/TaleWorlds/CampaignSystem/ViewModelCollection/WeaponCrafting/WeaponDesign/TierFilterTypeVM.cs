using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000103 RID: 259
	public class TierFilterTypeVM : ViewModel
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005A88E File Offset: 0x00058A8E
		public WeaponDesignVM.CraftingPieceTierFilter FilterType { get; }

		// Token: 0x06001787 RID: 6023 RVA: 0x0005A896 File Offset: 0x00058A96
		public TierFilterTypeVM(WeaponDesignVM.CraftingPieceTierFilter filterType, Action<WeaponDesignVM.CraftingPieceTierFilter> onSelect, string tierName)
		{
			this.FilterType = filterType;
			this._onSelect = onSelect;
			this.TierName = tierName;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0005A8B3 File Offset: 0x00058AB3
		public void ExecuteSelectTier()
		{
			Action<WeaponDesignVM.CraftingPieceTierFilter> onSelect = this._onSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect(this.FilterType);
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0005A8CB File Offset: 0x00058ACB
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x0005A8D3 File Offset: 0x00058AD3
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0005A8F1 File Offset: 0x00058AF1
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x0005A8F9 File Offset: 0x00058AF9
		[DataSourceProperty]
		public string TierName
		{
			get
			{
				return this._tierName;
			}
			set
			{
				if (value != this._tierName)
				{
					this._tierName = value;
					base.OnPropertyChangedWithValue<string>(value, "TierName");
				}
			}
		}

		// Token: 0x04000AC4 RID: 2756
		private readonly Action<WeaponDesignVM.CraftingPieceTierFilter> _onSelect;

		// Token: 0x04000AC5 RID: 2757
		private bool _isSelected;

		// Token: 0x04000AC6 RID: 2758
		private string _tierName;
	}
}
