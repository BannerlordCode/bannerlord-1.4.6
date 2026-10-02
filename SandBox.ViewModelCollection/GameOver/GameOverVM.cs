using System;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x0200005D RID: 93
	public class GameOverVM : ViewModel
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x00014EF0 File Offset: 0x000130F0
		public GameOverVM(GameOverState.GameOverReason reason, Action onClose)
		{
			this._onClose = onClose;
			this._reason = reason;
			this._statsProvider = new GameOverStatsProvider();
			this.Categories = new MBBindingList<GameOverStatCategoryVM>();
			this.IsPositiveGameOver = this._reason == GameOverState.GameOverReason.Victory;
			this.ClanBanner = new BannerImageIdentifierVM(Hero.MainHero.ClanBanner, true);
			this.ReasonAsString = Enum.GetName(typeof(GameOverState.GameOverReason), this._reason);
			this.RefreshValues();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00014F74 File Offset: 0x00013174
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.CloseText = (this.IsPositiveGameOver ? new TextObject("{=AdgAJbAP}Return To The Map", null).ToString() : GameTexts.FindText("str_main_menu", null).ToString());
			this.TitleText = GameTexts.FindText("str_game_over_title", this.ReasonAsString).ToString();
			this.StatisticsTitle = GameTexts.FindText("str_statistics", null).ToString();
			this.Categories.Clear();
			foreach (StatCategory statCategory in this._statsProvider.GetGameOverStats())
			{
				this.Categories.Add(new GameOverStatCategoryVM(statCategory, new Action<GameOverStatCategoryVM>(this.OnCategorySelection)));
			}
			this.OnCategorySelection(this.Categories[0]);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00015060 File Offset: 0x00013260
		private void OnCategorySelection(GameOverStatCategoryVM newCategory)
		{
			if (this._currentCategory != null)
			{
				this._currentCategory.IsSelected = false;
			}
			this._currentCategory = newCategory;
			if (this._currentCategory != null)
			{
				this._currentCategory.IsSelected = true;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00015091 File Offset: 0x00013291
		public void ExecuteClose()
		{
			Action onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000150A9 File Offset: 0x000132A9
		public void SetCloseInputKey(HotKey hotKey)
		{
			this.CloseInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000150B8 File Offset: 0x000132B8
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM closeInputKey = this.CloseInputKey;
			if (closeInputKey == null)
			{
				return;
			}
			closeInputKey.OnFinalize();
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000150D0 File Offset: 0x000132D0
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x000150D8 File Offset: 0x000132D8
		[DataSourceProperty]
		public string CloseText
		{
			get
			{
				return this._closeText;
			}
			set
			{
				if (value != this._closeText)
				{
					this._closeText = value;
					base.OnPropertyChangedWithValue<string>(value, "CloseText");
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000150FB File Offset: 0x000132FB
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00015103 File Offset: 0x00013303
		[DataSourceProperty]
		public string StatisticsTitle
		{
			get
			{
				return this._statisticsTitle;
			}
			set
			{
				if (value != this._statisticsTitle)
				{
					this._statisticsTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "StatisticsTitle");
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00015126 File Offset: 0x00013326
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0001512E File Offset: 0x0001332E
		[DataSourceProperty]
		public string ReasonAsString
		{
			get
			{
				return this._reasonAsString;
			}
			set
			{
				if (value != this._reasonAsString)
				{
					this._reasonAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "ReasonAsString");
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00015151 File Offset: 0x00013351
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00015159 File Offset: 0x00013359
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001517C File Offset: 0x0001337C
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00015184 File Offset: 0x00013384
		[DataSourceProperty]
		public BannerImageIdentifierVM ClanBanner
		{
			get
			{
				return this._clanBanner;
			}
			set
			{
				if (value != this._clanBanner)
				{
					this._clanBanner = value;
					base.OnPropertyChangedWithValue<BannerImageIdentifierVM>(value, "ClanBanner");
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000151A2 File Offset: 0x000133A2
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x000151AA File Offset: 0x000133AA
		[DataSourceProperty]
		public bool IsPositiveGameOver
		{
			get
			{
				return this._isPositiveGameOver;
			}
			set
			{
				if (value != this._isPositiveGameOver)
				{
					this._isPositiveGameOver = value;
					base.OnPropertyChangedWithValue(value, "IsPositiveGameOver");
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000151C8 File Offset: 0x000133C8
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000151D0 File Offset: 0x000133D0
		[DataSourceProperty]
		public InputKeyItemVM CloseInputKey
		{
			get
			{
				return this._closeInputKey;
			}
			set
			{
				if (value != this._closeInputKey)
				{
					this._closeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CloseInputKey");
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000151EE File Offset: 0x000133EE
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000151F6 File Offset: 0x000133F6
		[DataSourceProperty]
		public MBBindingList<GameOverStatCategoryVM> Categories
		{
			get
			{
				return this._categories;
			}
			set
			{
				if (value != this._categories)
				{
					this._categories = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameOverStatCategoryVM>>(value, "Categories");
				}
			}
		}

		// Token: 0x040002C6 RID: 710
		private readonly Action _onClose;

		// Token: 0x040002C7 RID: 711
		private readonly GameOverStatsProvider _statsProvider;

		// Token: 0x040002C8 RID: 712
		private readonly GameOverState.GameOverReason _reason;

		// Token: 0x040002C9 RID: 713
		private GameOverStatCategoryVM _currentCategory;

		// Token: 0x040002CA RID: 714
		private string _closeText;

		// Token: 0x040002CB RID: 715
		private string _titleText;

		// Token: 0x040002CC RID: 716
		private string _reasonAsString;

		// Token: 0x040002CD RID: 717
		private string _statisticsTitle;

		// Token: 0x040002CE RID: 718
		private bool _isPositiveGameOver;

		// Token: 0x040002CF RID: 719
		private InputKeyItemVM _closeInputKey;

		// Token: 0x040002D0 RID: 720
		private BannerImageIdentifierVM _clanBanner;

		// Token: 0x040002D1 RID: 721
		private MBBindingList<GameOverStatCategoryVM> _categories;
	}
}
