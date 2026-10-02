using System;
using Helpers;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x020000A2 RID: 162
	public class SettlementBuildingProjectVM : SettlementProjectVM
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x000413D0 File Offset: 0x0003F5D0
		public SettlementBuildingProjectVM(Action<SettlementProjectVM, bool> onSelection, Action<SettlementProjectVM> onSetAsCurrent, Action onResetCurrent, Building building, Settlement settlement)
			: base(onSelection, onSetAsCurrent, onResetCurrent, building, settlement)
		{
			this.Level = building.CurrentLevel;
			this.MaxLevel = 3;
			this.DevelopmentLevelText = building.CurrentLevel.ToString();
			this.CanBuild = this.Level < 3;
			base.IsDaily = false;
			this.RefreshValues();
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00041436 File Offset: 0x0003F636
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.AlreadyAtMaxText = new TextObject("{=ybLA7ZXp}Already at Max", null).ToString();
			this.UpdateProjectHints();
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0004145C File Offset: 0x0003F65C
		private void UpdateProjectHints()
		{
			if (this.AddRemoveHint == null)
			{
				this.AddRemoveHint = new HintViewModel();
			}
			if (this.SetAsActiveHint == null)
			{
				this.SetAsActiveHint = new HintViewModel();
			}
			this.AddRemoveHint.HintText = (this.IsInQueue ? new TextObject("{=faDegful}Remove from queue", null) : new TextObject("{=SFebv4hH}Add to queue", null));
			this.SetAsActiveHint.HintText = ((this.DevelopmentQueueIndex == 0) ? new TextObject("{=cD1HTdYJ}Already active development", null) : new TextObject("{=PcLGc2bM}Set as active development", null));
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000414E8 File Offset: 0x0003F6E8
		public override void RefreshProductionText()
		{
			base.RefreshProductionText();
			if (this.DevelopmentQueueIndex == 0)
			{
				GameTexts.SetVariable("LEFT", GameTexts.FindText("str_completion", null));
				int daysToComplete = BuildingHelper.GetDaysToComplete(base.Building, this._settlement.Town);
				TextObject textObject;
				if (daysToComplete != -1)
				{
					textObject = new TextObject("{=c5eYzHaM}{DAYS} {?DAY_IS_PLURAL}Days{?}Day{\\?} ({PERCENTAGE}%)", null);
					textObject.SetTextVariable("DAYS", daysToComplete);
					GameTexts.SetVariable("DAY_IS_PLURAL", (daysToComplete > 1) ? 1 : 0);
				}
				else
				{
					textObject = new TextObject("{=0TauthlH}Never ({PERCENTAGE}%)", null);
				}
				textObject.SetTextVariable("PERCENTAGE", (int)(BuildingHelper.GetProgressOfBuilding(base.Building, this._settlement.Town) * 100f));
				GameTexts.SetVariable("RIGHT", textObject);
				base.ProductionText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				return;
			}
			if (this.DevelopmentQueueIndex > 0)
			{
				GameTexts.SetVariable("NUMBER", this.DevelopmentQueueIndex);
				base.ProductionText = GameTexts.FindText("str_in_queue_with_number", null).ToString();
				return;
			}
			base.ProductionText = " ";
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000415F5 File Offset: 0x0003F7F5
		public override void ExecuteAddRemoveToQueue()
		{
			if (this._onSelection != null && this.CanBuild)
			{
				this._onSelection(this, false);
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00041614 File Offset: 0x0003F814
		public override void ExecuteSetAsActiveDevelopment()
		{
			if (this._onSelection != null && this.CanBuild)
			{
				this._onSelection(this, true);
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00041633 File Offset: 0x0003F833
		public override void ExecuteSetAsCurrent()
		{
			Action<SettlementProjectVM> onSetAsCurrent = this._onSetAsCurrent;
			if (onSetAsCurrent == null)
			{
				return;
			}
			onSetAsCurrent(this);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00041646 File Offset: 0x0003F846
		public override void ExecuteResetCurrent()
		{
			Action onResetCurrent = this._onResetCurrent;
			if (onResetCurrent == null)
			{
				return;
			}
			onResetCurrent();
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00041658 File Offset: 0x0003F858
		public override void ExecuteToggleSelected()
		{
			if (this.CanBuild)
			{
				this.IsSelected = !this.IsSelected;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00041671 File Offset: 0x0003F871
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00041679 File Offset: 0x0003F879
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00041697 File Offset: 0x0003F897
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x0004169F File Offset: 0x0003F89F
		[DataSourceProperty]
		public string DevelopmentLevelText
		{
			get
			{
				return this._developmentLevelText;
			}
			set
			{
				if (value != this._developmentLevelText)
				{
					this._developmentLevelText = value;
					base.OnPropertyChangedWithValue<string>(value, "DevelopmentLevelText");
				}
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x000416C2 File Offset: 0x0003F8C2
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x000416CA File Offset: 0x0003F8CA
		[DataSourceProperty]
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value != this._level)
				{
					this._level = value;
					base.OnPropertyChangedWithValue(value, "Level");
				}
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000416E8 File Offset: 0x0003F8E8
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x000416F0 File Offset: 0x0003F8F0
		[DataSourceProperty]
		public int MaxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				if (value != this._maxLevel)
				{
					this._maxLevel = value;
					base.OnPropertyChangedWithValue(value, "MaxLevel");
				}
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0004170E File Offset: 0x0003F90E
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x00041716 File Offset: 0x0003F916
		[DataSourceProperty]
		public int DevelopmentQueueIndex
		{
			get
			{
				return this._developmentQueueIndex;
			}
			set
			{
				if (value != this._developmentQueueIndex)
				{
					this._developmentQueueIndex = value;
					base.OnPropertyChangedWithValue(value, "DevelopmentQueueIndex");
					this.UpdateProjectHints();
				}
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0004173A File Offset: 0x0003F93A
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00041742 File Offset: 0x0003F942
		[DataSourceProperty]
		public bool IsInQueue
		{
			get
			{
				return this._isInQueue;
			}
			set
			{
				if (value != this._isInQueue)
				{
					this._isInQueue = value;
					base.OnPropertyChangedWithValue(value, "IsInQueue");
					this.UpdateProjectHints();
				}
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00041766 File Offset: 0x0003F966
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0004176E File Offset: 0x0003F96E
		[DataSourceProperty]
		public string AlreadyAtMaxText
		{
			get
			{
				return this._alreadyAtMaxText;
			}
			set
			{
				if (value != this._alreadyAtMaxText)
				{
					this._alreadyAtMaxText = value;
					base.OnPropertyChangedWithValue<string>(value, "AlreadyAtMaxText");
				}
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00041791 File Offset: 0x0003F991
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00041799 File Offset: 0x0003F999
		[DataSourceProperty]
		public bool CanBuild
		{
			get
			{
				return this._canBuild;
			}
			set
			{
				if (value != this._canBuild)
				{
					this._canBuild = value;
					base.OnPropertyChangedWithValue(value, "CanBuild");
				}
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000417B7 File Offset: 0x0003F9B7
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x000417BF File Offset: 0x0003F9BF
		[DataSourceProperty]
		public HintViewModel AddRemoveHint
		{
			get
			{
				return this._addRemoveHint;
			}
			set
			{
				if (value != this._addRemoveHint)
				{
					this._addRemoveHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AddRemoveHint");
				}
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000417DD File Offset: 0x0003F9DD
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x000417E5 File Offset: 0x0003F9E5
		[DataSourceProperty]
		public HintViewModel SetAsActiveHint
		{
			get
			{
				return this._setAsActiveHint;
			}
			set
			{
				if (value != this._setAsActiveHint)
				{
					this._setAsActiveHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SetAsActiveHint");
				}
			}
		}

		// Token: 0x04000737 RID: 1847
		private bool _isSelected;

		// Token: 0x04000738 RID: 1848
		private string _alreadyAtMaxText;

		// Token: 0x04000739 RID: 1849
		private string _developmentLevelText;

		// Token: 0x0400073A RID: 1850
		private int _level;

		// Token: 0x0400073B RID: 1851
		private int _maxLevel;

		// Token: 0x0400073C RID: 1852
		private int _developmentQueueIndex = -1;

		// Token: 0x0400073D RID: 1853
		private bool _canBuild;

		// Token: 0x0400073E RID: 1854
		private bool _isInQueue;

		// Token: 0x0400073F RID: 1855
		private HintViewModel _addRemoveHint;

		// Token: 0x04000740 RID: 1856
		private HintViewModel _setAsActiveHint;
	}
}
