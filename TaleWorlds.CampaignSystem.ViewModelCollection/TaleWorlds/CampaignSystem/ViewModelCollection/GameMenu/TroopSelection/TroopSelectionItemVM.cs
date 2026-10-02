using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TroopSelection
{
	// Token: 0x020000A0 RID: 160
	public class TroopSelectionItemVM : ViewModel
	{
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00040F1E File Offset: 0x0003F11E
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x00040F26 File Offset: 0x0003F126
		public TroopRosterElement Troop { get; private set; }

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00040F30 File Offset: 0x0003F130
		public TroopSelectionItemVM(TroopRosterElement troop, Action<TroopSelectionItemVM> onAdd, Action<TroopSelectionItemVM> onRemove)
		{
			this._onAdd = onAdd;
			this._onRemove = onRemove;
			this.Troop = troop;
			this.MaxAmount = this.Troop.Number - this.Troop.WoundedNumber;
			this.Visual = new CharacterImageIdentifierVM(CampaignUIHelper.GetCharacterCode(troop.Character, false));
			this.Name = troop.Character.Name.ToString();
			this.TierIconData = CampaignUIHelper.GetCharacterTierData(this.Troop.Character, false);
			this.TypeIconData = CampaignUIHelper.GetCharacterTypeData(this.Troop.Character, false);
			this.IsTroopHero = this.Troop.Character.IsHero;
			this.HeroHealthPercent = (this.Troop.Character.IsHero ? MathF.Ceiling((float)this.Troop.Character.HeroObject.HitPoints / (float)this.Troop.Character.MaxHitPoints() * 100f) : 0);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004103A File Offset: 0x0003F23A
		public void ExecuteAdd()
		{
			Action<TroopSelectionItemVM> onAdd = this._onAdd;
			if (onAdd == null)
			{
				return;
			}
			onAdd.DynamicInvokeWithLog(new object[] { this });
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00041057 File Offset: 0x0003F257
		public void ExecuteRemove()
		{
			Action<TroopSelectionItemVM> onRemove = this._onRemove;
			if (onRemove == null)
			{
				return;
			}
			onRemove.DynamicInvokeWithLog(new object[] { this });
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00041074 File Offset: 0x0003F274
		private void UpdateAmountText()
		{
			GameTexts.SetVariable("LEFT", this.CurrentAmount);
			GameTexts.SetVariable("RIGHT", this.MaxAmount);
			this.AmountText = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000410AC File Offset: 0x0003F2AC
		public void ExecuteLink()
		{
			if (this.Troop.Character != null)
			{
				EncyclopediaManager encyclopediaManager = Campaign.Current.EncyclopediaManager;
				Hero heroObject = this.Troop.Character.HeroObject;
				encyclopediaManager.GoToLink(((heroObject != null) ? heroObject.EncyclopediaLink : null) ?? this.Troop.Character.EncyclopediaLink);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00041105 File Offset: 0x0003F305
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0004110D File Offset: 0x0003F30D
		[DataSourceProperty]
		public int MaxAmount
		{
			get
			{
				return this._maxAmount;
			}
			set
			{
				if (value != this._maxAmount)
				{
					this._maxAmount = value;
					base.OnPropertyChangedWithValue(value, "MaxAmount");
					this.UpdateAmountText();
				}
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00041131 File Offset: 0x0003F331
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00041139 File Offset: 0x0003F339
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

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00041157 File Offset: 0x0003F357
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0004115F File Offset: 0x0003F35F
		[DataSourceProperty]
		public bool IsRosterFull
		{
			get
			{
				return this._isRosterFull;
			}
			set
			{
				if (value != this._isRosterFull)
				{
					this._isRosterFull = value;
					base.OnPropertyChangedWithValue(value, "IsRosterFull");
				}
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0004117D File Offset: 0x0003F37D
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00041185 File Offset: 0x0003F385
		[DataSourceProperty]
		public bool IsTroopHero
		{
			get
			{
				return this._isTroopHero;
			}
			set
			{
				if (value != this._isTroopHero)
				{
					this._isTroopHero = value;
					base.OnPropertyChangedWithValue(value, "IsTroopHero");
				}
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000411A3 File Offset: 0x0003F3A3
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x000411AB File Offset: 0x0003F3AB
		[DataSourceProperty]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (value != this._isLocked)
				{
					this._isLocked = value;
					base.OnPropertyChangedWithValue(value, "IsLocked");
				}
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000411C9 File Offset: 0x0003F3C9
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x000411D1 File Offset: 0x0003F3D1
		[DataSourceProperty]
		public int CurrentAmount
		{
			get
			{
				return this._currentAmount;
			}
			set
			{
				if (value != this._currentAmount)
				{
					this._currentAmount = value;
					base.OnPropertyChangedWithValue(value, "CurrentAmount");
					this.IsSelected = value > 0;
					this.UpdateAmountText();
				}
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000411FF File Offset: 0x0003F3FF
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00041207 File Offset: 0x0003F407
		[DataSourceProperty]
		public int HeroHealthPercent
		{
			get
			{
				return this._heroHealthPercent;
			}
			set
			{
				if (value != this._heroHealthPercent)
				{
					this._heroHealthPercent = value;
					base.OnPropertyChangedWithValue(value, "HeroHealthPercent");
				}
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00041225 File Offset: 0x0003F425
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x0004122D File Offset: 0x0003F42D
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00041250 File Offset: 0x0003F450
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00041258 File Offset: 0x0003F458
		[DataSourceProperty]
		public string AmountText
		{
			get
			{
				return this._amountText;
			}
			set
			{
				if (value != this._amountText)
				{
					this._amountText = value;
					base.OnPropertyChangedWithValue<string>(value, "AmountText");
				}
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0004127B File Offset: 0x0003F47B
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00041283 File Offset: 0x0003F483
		[DataSourceProperty]
		public CharacterImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000412A1 File Offset: 0x0003F4A1
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x000412A9 File Offset: 0x0003F4A9
		[DataSourceProperty]
		public StringItemWithHintVM TierIconData
		{
			get
			{
				return this._tierIconData;
			}
			set
			{
				if (value != this._tierIconData)
				{
					this._tierIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TierIconData");
				}
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x000412C7 File Offset: 0x0003F4C7
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x000412CF File Offset: 0x0003F4CF
		[DataSourceProperty]
		public StringItemWithHintVM TypeIconData
		{
			get
			{
				return this._typeIconData;
			}
			set
			{
				if (value != this._typeIconData)
				{
					this._typeIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TypeIconData");
				}
			}
		}

		// Token: 0x04000729 RID: 1833
		private readonly Action<TroopSelectionItemVM> _onAdd;

		// Token: 0x0400072A RID: 1834
		private readonly Action<TroopSelectionItemVM> _onRemove;

		// Token: 0x0400072B RID: 1835
		private int _currentAmount;

		// Token: 0x0400072C RID: 1836
		private int _maxAmount;

		// Token: 0x0400072D RID: 1837
		private int _heroHealthPercent;

		// Token: 0x0400072E RID: 1838
		private CharacterImageIdentifierVM _visual;

		// Token: 0x0400072F RID: 1839
		private bool _isSelected;

		// Token: 0x04000730 RID: 1840
		private bool _isRosterFull;

		// Token: 0x04000731 RID: 1841
		private bool _isLocked;

		// Token: 0x04000732 RID: 1842
		private bool _isTroopHero;

		// Token: 0x04000733 RID: 1843
		private string _name;

		// Token: 0x04000734 RID: 1844
		private string _amountText;

		// Token: 0x04000735 RID: 1845
		private StringItemWithHintVM _tierIconData;

		// Token: 0x04000736 RID: 1846
		private StringItemWithHintVM _typeIconData;
	}
}
