using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu
{
	// Token: 0x0200004C RID: 76
	public class InitialMenuVM : ViewModel
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00017ACC File Offset: 0x00015CCC
		public InitialMenuVM(InitialState initialState)
		{
			this.MenuOptions = new MBBindingList<InitialMenuOptionVM>();
			this.Announcement = new InitialMenuAnnouncementVM();
			if (HotKeyManager.ShouldNotifyDocumentVersionDifferent())
			{
				MBInformationManager.AddQuickInformation(new TextObject("{=0Itt3bZM}Current keybind document version is outdated. Keybinds have been reverted to defaults.", null), 0, null, null, "");
			}
			this.RefreshValues();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00017B1C File Offset: 0x00015D1C
		public override void RefreshValues()
		{
			base.RefreshValues();
			MBBindingList<InitialMenuOptionVM> menuOptions = this.MenuOptions;
			if (menuOptions != null)
			{
				menuOptions.ApplyActionOnAllItems(delegate(InitialMenuOptionVM o)
				{
					o.RefreshValues();
				});
			}
			this.Announcement.Refresh();
			this.SelectProfileText = new TextObject("{=wubDWOlh}Select Profile", null).ToString();
			this.DownloadingText = new TextObject("{=i4Oo6aoM}Downloading Content...", null).ToString();
			this.CurrentLanguageString = BannerlordConfig.Language;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00017BA1 File Offset: 0x00015DA1
		public void Tick()
		{
			InitialMenuAnnouncementVM announcement = this.Announcement;
			if (announcement == null)
			{
				return;
			}
			announcement.Tick();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public void RefreshMenuOptions()
		{
			this.MenuOptions.ApplyActionOnAllItems(delegate(InitialMenuOptionVM x)
			{
				x.OnFinalize();
			});
			this.MenuOptions.Clear();
			GameState activeState = GameStateManager.Current.ActiveState;
			foreach (InitialStateOption initialStateOption in Module.CurrentModule.GetInitialStateOptions())
			{
				this.MenuOptions.Add(new InitialMenuOptionVM(initialStateOption));
			}
			this.IsDownloadingContent = Utilities.IsOnlyCoreContentEnabled();
			this.IsNavalDLCEnabled = ModuleHelper.IsModuleActive("NavalDLC");
			this.Announcement.Refresh();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00017C78 File Offset: 0x00015E78
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.MenuOptions.ApplyActionOnAllItems(delegate(InitialMenuOptionVM x)
			{
				x.OnFinalize();
			});
			this.MenuOptions.Clear();
			this.Announcement.OnFinalize();
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00017CCB File Offset: 0x00015ECB
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00017CD3 File Offset: 0x00015ED3
		[DataSourceProperty]
		public MBBindingList<InitialMenuOptionVM> MenuOptions
		{
			get
			{
				return this._menuOptions;
			}
			set
			{
				if (value != this._menuOptions)
				{
					this._menuOptions = value;
					base.OnPropertyChangedWithValue<MBBindingList<InitialMenuOptionVM>>(value, "MenuOptions");
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00017CF1 File Offset: 0x00015EF1
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00017CF9 File Offset: 0x00015EF9
		[DataSourceProperty]
		public InitialMenuAnnouncementVM Announcement
		{
			get
			{
				return this._announcement;
			}
			set
			{
				if (value != this._announcement)
				{
					this._announcement = value;
					base.OnPropertyChangedWithValue<InitialMenuAnnouncementVM>(value, "Announcement");
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00017D17 File Offset: 0x00015F17
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x00017D1F File Offset: 0x00015F1F
		[DataSourceProperty]
		public string DownloadingText
		{
			get
			{
				return this._downloadingText;
			}
			set
			{
				if (value != this._downloadingText)
				{
					this._downloadingText = value;
					base.OnPropertyChangedWithValue<string>(value, "DownloadingText");
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00017D42 File Offset: 0x00015F42
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x00017D4A File Offset: 0x00015F4A
		[DataSourceProperty]
		public string SelectProfileText
		{
			get
			{
				return this._selectProfileText;
			}
			set
			{
				if (value != this._selectProfileText)
				{
					this._selectProfileText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectProfileText");
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00017D6D File Offset: 0x00015F6D
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x00017D75 File Offset: 0x00015F75
		[DataSourceProperty]
		public string ProfileName
		{
			get
			{
				return this._profileName;
			}
			set
			{
				if (value != this._profileName)
				{
					this._profileName = value;
					base.OnPropertyChangedWithValue<string>(value, "ProfileName");
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00017D98 File Offset: 0x00015F98
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00017DA0 File Offset: 0x00015FA0
		[DataSourceProperty]
		public bool IsProfileSelectionEnabled
		{
			get
			{
				return this._isProfileSelectionEnabled;
			}
			set
			{
				if (value != this._isProfileSelectionEnabled)
				{
					this._isProfileSelectionEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsProfileSelectionEnabled");
				}
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00017DBE File Offset: 0x00015FBE
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x00017DC6 File Offset: 0x00015FC6
		[DataSourceProperty]
		public bool IsDownloadingContent
		{
			get
			{
				return this._isDownloadingContent;
			}
			set
			{
				if (value != this._isDownloadingContent)
				{
					this._isDownloadingContent = value;
					base.OnPropertyChangedWithValue(value, "IsDownloadingContent");
				}
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00017DE4 File Offset: 0x00015FE4
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x00017DEC File Offset: 0x00015FEC
		[DataSourceProperty]
		public bool IsNavalDLCEnabled
		{
			get
			{
				return this._isNavalDLCEnabled;
			}
			set
			{
				if (value != this._isNavalDLCEnabled)
				{
					this._isNavalDLCEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsNavalDLCEnabled");
				}
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00017E0A File Offset: 0x0001600A
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00017E12 File Offset: 0x00016012
		[DataSourceProperty]
		public string CurrentLanguageString
		{
			get
			{
				return this._currentLanguageString;
			}
			set
			{
				if (value != this._currentLanguageString)
				{
					this._currentLanguageString = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentLanguageString");
				}
			}
		}

		// Token: 0x040002D5 RID: 725
		private MBBindingList<InitialMenuOptionVM> _menuOptions;

		// Token: 0x040002D6 RID: 726
		private InitialMenuAnnouncementVM _announcement;

		// Token: 0x040002D7 RID: 727
		private bool _isProfileSelectionEnabled;

		// Token: 0x040002D8 RID: 728
		private bool _isDownloadingContent;

		// Token: 0x040002D9 RID: 729
		private bool _isNavalDLCEnabled;

		// Token: 0x040002DA RID: 730
		private string _selectProfileText;

		// Token: 0x040002DB RID: 731
		private string _profileName;

		// Token: 0x040002DC RID: 732
		private string _downloadingText;

		// Token: 0x040002DD RID: 733
		private string _currentLanguageString;
	}
}
