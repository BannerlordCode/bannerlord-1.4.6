using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.WalkMode
{
	// Token: 0x0200005A RID: 90
	public class MissionMainAgentWalkModeControllerVM : ViewModel
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x0001A955 File Offset: 0x00018B55
		public MissionMainAgentWalkModeControllerVM()
		{
			this.ControlModes = new MBBindingList<WalkModeItemVM>();
			this.RefreshValues();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001A96E File Offset: 0x00018B6E
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.ControlModes.ApplyActionOnAllItems(delegate(WalkModeItemVM o)
			{
				o.OnFinalize();
			});
			this.ControlModes.Clear();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001A9AC File Offset: 0x00018BAC
		public void AddWalkMode(string typeId, TextObject name, MissionMainAgentWalkModeControllerVM.GetIsWalkModeActivatedDelegate getIsActive, MissionMainAgentWalkModeControllerVM.SetIsWalkModeActivatedDelegate setIsActive, MissionMainAgentWalkModeControllerVM.GetCanChangeWalkModeActivatedDelegate canChangeActive, HotKey hotKey, bool isHotkeyConsoleOnly)
		{
			WalkModeItemVM walkModeItemVM = new WalkModeItemVM(typeId, name, getIsActive, setIsActive, canChangeActive, new Action<WalkModeItemVM>(this.OnItemToggled));
			walkModeItemVM.SetToggleInputKey(hotKey, isHotkeyConsoleOnly);
			this.ControlModes.Add(walkModeItemVM);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001A9E8 File Offset: 0x00018BE8
		public void AddWalkMode(string typeId, TextObject name, MissionMainAgentWalkModeControllerVM.GetIsWalkModeActivatedDelegate getIsActive, MissionMainAgentWalkModeControllerVM.SetIsWalkModeActivatedDelegate setIsActive, MissionMainAgentWalkModeControllerVM.GetCanChangeWalkModeActivatedDelegate canChangeActive, GameKey hotKey, bool isHotkeyConsoleOnly)
		{
			WalkModeItemVM walkModeItemVM = new WalkModeItemVM(typeId, name, getIsActive, setIsActive, canChangeActive, new Action<WalkModeItemVM>(this.OnItemToggled));
			walkModeItemVM.SetToggleInputKey(hotKey, isHotkeyConsoleOnly);
			this.ControlModes.Add(walkModeItemVM);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001AA24 File Offset: 0x00018C24
		private void OnItemToggled(WalkModeItemVM item)
		{
			this.LastUsedItem = item;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001AA30 File Offset: 0x00018C30
		public void SetEnabled(bool isEnabled)
		{
			this.IsEnabled = isEnabled;
			if (isEnabled)
			{
				for (int i = 0; i < this.ControlModes.Count; i++)
				{
					this.ControlModes[i].OnEnabled();
				}
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001AA6E File Offset: 0x00018C6E
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001AA76 File Offset: 0x00018C76
		[DataSourceProperty]
		public MBBindingList<WalkModeItemVM> ControlModes
		{
			get
			{
				return this._controlModes;
			}
			set
			{
				if (value != this._controlModes)
				{
					this._controlModes = value;
					base.OnPropertyChangedWithValue<MBBindingList<WalkModeItemVM>>(value, "ControlModes");
				}
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001AA94 File Offset: 0x00018C94
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001AA9C File Offset: 0x00018C9C
		[DataSourceProperty]
		public WalkModeItemVM LastUsedItem
		{
			get
			{
				return this._lastUsedItem;
			}
			set
			{
				if (value != this._lastUsedItem)
				{
					this._lastUsedItem = value;
					base.OnPropertyChangedWithValue<WalkModeItemVM>(value, "LastUsedItem");
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001AABA File Offset: 0x00018CBA
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001AAC2 File Offset: 0x00018CC2
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x04000344 RID: 836
		private MBBindingList<WalkModeItemVM> _controlModes;

		// Token: 0x04000345 RID: 837
		private WalkModeItemVM _lastUsedItem;

		// Token: 0x04000346 RID: 838
		private bool _isEnabled;

		// Token: 0x020000F2 RID: 242
		// (Invoke) Token: 0x06000D13 RID: 3347
		public delegate bool GetIsWalkModeActivatedDelegate();

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x06000D17 RID: 3351
		public delegate void SetIsWalkModeActivatedDelegate(bool value);

		// Token: 0x020000F4 RID: 244
		// (Invoke) Token: 0x06000D1B RID: 3355
		public delegate bool GetCanChangeWalkModeActivatedDelegate();
	}
}
