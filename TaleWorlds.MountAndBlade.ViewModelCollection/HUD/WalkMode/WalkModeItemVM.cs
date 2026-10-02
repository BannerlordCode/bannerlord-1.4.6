using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.WalkMode
{
	// Token: 0x0200005B RID: 91
	public class WalkModeItemVM : ViewModel
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public WalkModeItemVM(string typeId, TextObject description, MissionMainAgentWalkModeControllerVM.GetIsWalkModeActivatedDelegate getIsActive, MissionMainAgentWalkModeControllerVM.SetIsWalkModeActivatedDelegate setIsActive, MissionMainAgentWalkModeControllerVM.GetCanChangeWalkModeActivatedDelegate canChangeActive, Action<WalkModeItemVM> onToggle)
		{
			this._getIsActive = getIsActive;
			this._setIsActive = setIsActive;
			this._canChangeActive = canChangeActive;
			this._descriptionTextObj = description;
			this._onToggle = onToggle;
			this.IsActive = this._getIsActive();
			this.TypeId = typeId;
			this.RefreshValues();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001AB37 File Offset: 0x00018D37
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Description = this._descriptionTextObj.ToString();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001AB50 File Offset: 0x00018D50
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM toggleInputKey = this.ToggleInputKey;
			if (toggleInputKey == null)
			{
				return;
			}
			toggleInputKey.OnFinalize();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001AB68 File Offset: 0x00018D68
		public void OnEnabled()
		{
			this.IsActive = this._getIsActive();
			this.IsDisabled = !this._canChangeActive();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001AB90 File Offset: 0x00018D90
		public void ToggleState()
		{
			this.IsDisabled = !this._canChangeActive();
			if (!this.IsDisabled)
			{
				this.IsActive = !this.IsActive;
				this._setIsActive(this.IsActive);
				this._onToggle(this);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001ABED File Offset: 0x00018DED
		[DataSourceProperty]
		public InputKeyItemVM ToggleInputKey
		{
			get
			{
				return this._toggleInputKey;
			}
			set
			{
				if (value != this._toggleInputKey)
				{
					this._toggleInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ToggleInputKey");
				}
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001AC0B File Offset: 0x00018E0B
		public void SetToggleInputKey(HotKey hotKey, bool isHotKeyConsoleOnly)
		{
			InputKeyItemVM toggleInputKey = this.ToggleInputKey;
			if (toggleInputKey != null)
			{
				toggleInputKey.OnFinalize();
			}
			this.ToggleInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, isHotKeyConsoleOnly);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001AC2B File Offset: 0x00018E2B
		public void SetToggleInputKey(GameKey gameKey, bool isHotKeyConsoleOnly)
		{
			InputKeyItemVM toggleInputKey = this.ToggleInputKey;
			if (toggleInputKey != null)
			{
				toggleInputKey.OnFinalize();
			}
			this.ToggleInputKey = InputKeyItemVM.CreateFromGameKey(gameKey, isHotKeyConsoleOnly);
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001AC4B File Offset: 0x00018E4B
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0001AC53 File Offset: 0x00018E53
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001AC71 File Offset: 0x00018E71
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0001AC79 File Offset: 0x00018E79
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001AC97 File Offset: 0x00018E97
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001AC9F File Offset: 0x00018E9F
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001ACC2 File Offset: 0x00018EC2
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0001ACCA File Offset: 0x00018ECA
		[DataSourceProperty]
		public string TypeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				if (value != this._typeId)
				{
					this._typeId = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeId");
				}
			}
		}

		// Token: 0x04000347 RID: 839
		private readonly MissionMainAgentWalkModeControllerVM.GetIsWalkModeActivatedDelegate _getIsActive;

		// Token: 0x04000348 RID: 840
		private readonly MissionMainAgentWalkModeControllerVM.SetIsWalkModeActivatedDelegate _setIsActive;

		// Token: 0x04000349 RID: 841
		private readonly MissionMainAgentWalkModeControllerVM.GetCanChangeWalkModeActivatedDelegate _canChangeActive;

		// Token: 0x0400034A RID: 842
		private readonly Action<WalkModeItemVM> _onToggle;

		// Token: 0x0400034B RID: 843
		private readonly TextObject _descriptionTextObj;

		// Token: 0x0400034C RID: 844
		private InputKeyItemVM _toggleInputKey;

		// Token: 0x0400034D RID: 845
		private bool _isActive;

		// Token: 0x0400034E RID: 846
		private bool _isDisabled;

		// Token: 0x0400034F RID: 847
		private string _description;

		// Token: 0x04000350 RID: 848
		private string _typeId;
	}
}
