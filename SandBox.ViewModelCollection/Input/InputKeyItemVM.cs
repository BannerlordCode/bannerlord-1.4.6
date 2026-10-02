using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Input
{
	// Token: 0x02000057 RID: 87
	public class InputKeyItemVM : ViewModel
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0001476F File Offset: 0x0001296F
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00014777 File Offset: 0x00012977
		public GameKey GameKey { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014780 File Offset: 0x00012980
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014788 File Offset: 0x00012988
		public HotKey HotKey { get; private set; }

		// Token: 0x0600056E RID: 1390 RVA: 0x00014791 File Offset: 0x00012991
		private InputKeyItemVM()
		{
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			HotKeyManager.OnKeybindsChanged += this.OnKeybindsChanged;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000147CA File Offset: 0x000129CA
		public override void OnFinalize()
		{
			base.OnFinalize();
			Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveStateChanged));
			HotKeyManager.OnKeybindsChanged -= this.OnKeybindsChanged;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00014803 File Offset: 0x00012A03
		private void OnGamepadActiveStateChanged()
		{
			this.ForceRefresh();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001480B File Offset: 0x00012A0B
		private void OnKeybindsChanged()
		{
			this.ForceRefresh();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00014813 File Offset: 0x00012A13
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ForceRefresh();
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014821 File Offset: 0x00012A21
		public void SetForcedVisibility(bool? isVisible)
		{
			this._forcedVisibility = isVisible;
			this.UpdateVisibility();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014830 File Offset: 0x00012A30
		private void ForceRefresh()
		{
			this.UpdateVisibility();
			this.KeyID = string.Empty;
			this.KeyName = string.Empty;
			if (this._forcedID != null)
			{
				this.KeyID = this._forcedID;
				TextObject forcedName = this._forcedName;
				this.KeyName = ((forcedName != null) ? forcedName.ToString() : null) ?? string.Empty;
				return;
			}
			this.KeyID = this.GetKeyId();
			this.KeyName = this.GetKeyName().ToString();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000148AC File Offset: 0x00012AAC
		private string GetKeyId()
		{
			if (Input.IsGamepadActive)
			{
				if (this.GameKey != null)
				{
					Key controllerKey = this.GameKey.ControllerKey;
					if (controllerKey == null)
					{
						return null;
					}
					return controllerKey.InputKey.ToString();
				}
				else if (this.HotKey != null)
				{
					Key key = this.HotKey.Keys.Find((Key k) => k.IsControllerInput);
					if (key == null)
					{
						return null;
					}
					return key.InputKey.ToString();
				}
			}
			if (this.GameKey != null)
			{
				Key keyboardKey = this.GameKey.KeyboardKey;
				if (keyboardKey == null)
				{
					return null;
				}
				return keyboardKey.InputKey.ToString();
			}
			else
			{
				if (this.HotKey == null)
				{
					return string.Empty;
				}
				Key key2 = this.HotKey.Keys.Find((Key k) => !k.IsControllerInput);
				if (key2 == null)
				{
					return null;
				}
				return key2.InputKey.ToString();
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000149C4 File Offset: 0x00012BC4
		private TextObject GetKeyName()
		{
			if (this._forcedName != null)
			{
				return this._forcedName;
			}
			if (Game.Current != null)
			{
				if (this.HotKey != null)
				{
					return Game.Current.GameTextManager.FindText("str_key_name", this.HotKey.GroupId + "_" + this.HotKey.Id);
				}
				if (this.GameKey != null)
				{
					return Game.Current.GameTextManager.FindText("str_key_name", this.GameKey.GroupId + "_" + this.GameKey.StringId);
				}
			}
			return TextObject.GetEmpty();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00014A6C File Offset: 0x00012C6C
		private void UpdateVisibility()
		{
			this.IsVisible = this._forcedVisibility ?? (!this._isVisibleToConsoleOnly || Input.IsGamepadActive);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public static InputKeyItemVM CreateFromGameKey(GameKey gameKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014AC3 File Offset: 0x00012CC3
		public static InputKeyItemVM CreateFromHotKey(HotKey hotKey, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00014ADE File Offset: 0x00012CDE
		public static InputKeyItemVM CreateFromHotKeyWithForcedName(HotKey hotKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.HotKey = hotKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00014B00 File Offset: 0x00012D00
		public static InputKeyItemVM CreateFromGameKeyWithForcedName(GameKey gameKey, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM.GameKey = gameKey;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014B22 File Offset: 0x00012D22
		public static InputKeyItemVM CreateFromForcedID(string forcedID, TextObject forcedName, bool isConsoleOnly)
		{
			InputKeyItemVM inputKeyItemVM = new InputKeyItemVM();
			inputKeyItemVM._forcedID = forcedID;
			inputKeyItemVM._forcedName = forcedName;
			inputKeyItemVM._isVisibleToConsoleOnly = isConsoleOnly;
			inputKeyItemVM.ForceRefresh();
			return inputKeyItemVM;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00014B44 File Offset: 0x00012D44
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00014B4C File Offset: 0x00012D4C
		[DataSourceProperty]
		public string KeyID
		{
			get
			{
				return this._keyID;
			}
			set
			{
				if (value != this._keyID)
				{
					this._keyID = value;
					base.OnPropertyChangedWithValue<string>(value, "KeyID");
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00014B6F File Offset: 0x00012D6F
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x00014B77 File Offset: 0x00012D77
		[DataSourceProperty]
		public string KeyName
		{
			get
			{
				return this._keyName;
			}
			set
			{
				if (value != this._keyName)
				{
					this._keyName = value;
					base.OnPropertyChangedWithValue<string>(value, "KeyName");
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00014B9A File Offset: 0x00012D9A
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00014BA2 File Offset: 0x00012DA2
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
				}
			}
		}

		// Token: 0x040002AF RID: 687
		private bool _isVisibleToConsoleOnly;

		// Token: 0x040002B0 RID: 688
		private TextObject _forcedName;

		// Token: 0x040002B1 RID: 689
		private string _forcedID;

		// Token: 0x040002B2 RID: 690
		private bool? _forcedVisibility;

		// Token: 0x040002B3 RID: 691
		private string _keyID;

		// Token: 0x040002B4 RID: 692
		private string _keyName;

		// Token: 0x040002B5 RID: 693
		private bool _isVisible;
	}
}
