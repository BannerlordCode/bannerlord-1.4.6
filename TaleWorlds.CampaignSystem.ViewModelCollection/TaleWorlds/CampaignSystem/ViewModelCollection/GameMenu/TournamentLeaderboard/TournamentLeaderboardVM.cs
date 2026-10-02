using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TournamentLeaderboard
{
	// Token: 0x020000AF RID: 175
	public class TournamentLeaderboardVM : ViewModel
	{
		// Token: 0x060010EE RID: 4334 RVA: 0x00044620 File Offset: 0x00042820
		public TournamentLeaderboardVM()
		{
			this.Entries = new MBBindingList<TournamentLeaderboardEntryItemVM>();
			List<KeyValuePair<Hero, int>> leaderboard = Campaign.Current.TournamentManager.GetLeaderboard();
			for (int i = 0; i < leaderboard.Count; i++)
			{
				this.Entries.Add(new TournamentLeaderboardEntryItemVM(leaderboard[i].Key, leaderboard[i].Value, i + 1));
			}
			this.SortController = new TournamentLeaderboardSortControllerVM(ref this._entries);
			this.RefreshValues();
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000446A8 File Offset: 0x000428A8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DoneText = GameTexts.FindText("str_done", null).ToString();
			this.Entries.ApplyActionOnAllItems(delegate(TournamentLeaderboardEntryItemVM x)
			{
				x.RefreshValues();
			});
			this.HeroText = GameTexts.FindText("str_hero", null).ToString();
			this.VictoriesText = GameTexts.FindText("str_leaderboard_victories", null).ToString();
			this.RankText = GameTexts.FindText("str_rank_sign", null).ToString();
			this.TitleText = GameTexts.FindText("str_leaderboard_title", null).ToString();
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00044753 File Offset: 0x00042953
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey == null)
			{
				return;
			}
			doneInputKey.OnFinalize();
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004476B File Offset: 0x0004296B
		public void ExecuteDone()
		{
			this.IsEnabled = false;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00044774 File Offset: 0x00042974
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00044783 File Offset: 0x00042983
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0004478B File Offset: 0x0004298B
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x000447A9 File Offset: 0x000429A9
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x000447B1 File Offset: 0x000429B1
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

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000447CF File Offset: 0x000429CF
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x000447D7 File Offset: 0x000429D7
		[DataSourceProperty]
		public TournamentLeaderboardSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<TournamentLeaderboardSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000447F5 File Offset: 0x000429F5
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x000447FD File Offset: 0x000429FD
		[DataSourceProperty]
		public MBBindingList<TournamentLeaderboardEntryItemVM> Entries
		{
			get
			{
				return this._entries;
			}
			set
			{
				if (value != this._entries)
				{
					this._entries = value;
					base.OnPropertyChangedWithValue<MBBindingList<TournamentLeaderboardEntryItemVM>>(value, "Entries");
				}
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0004481B File Offset: 0x00042A1B
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00044823 File Offset: 0x00042A23
		[DataSourceProperty]
		public string DoneText
		{
			get
			{
				return this._doneText;
			}
			set
			{
				if (value != this._doneText)
				{
					this._doneText = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneText");
				}
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00044846 File Offset: 0x00042A46
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x0004484E File Offset: 0x00042A4E
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

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00044871 File Offset: 0x00042A71
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x00044879 File Offset: 0x00042A79
		[DataSourceProperty]
		public string HeroText
		{
			get
			{
				return this._heroText;
			}
			set
			{
				if (value != this._heroText)
				{
					this._heroText = value;
					base.OnPropertyChangedWithValue<string>(value, "HeroText");
				}
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0004489C File Offset: 0x00042A9C
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x000448A4 File Offset: 0x00042AA4
		[DataSourceProperty]
		public string VictoriesText
		{
			get
			{
				return this._victoriesText;
			}
			set
			{
				if (value != this._victoriesText)
				{
					this._victoriesText = value;
					base.OnPropertyChangedWithValue<string>(value, "VictoriesText");
				}
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x000448C7 File Offset: 0x00042AC7
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x000448CF File Offset: 0x00042ACF
		[DataSourceProperty]
		public string RankText
		{
			get
			{
				return this._rankText;
			}
			set
			{
				if (value != this._rankText)
				{
					this._rankText = value;
					base.OnPropertyChangedWithValue<string>(value, "RankText");
				}
			}
		}

		// Token: 0x040007BE RID: 1982
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040007BF RID: 1983
		private bool _isEnabled;

		// Token: 0x040007C0 RID: 1984
		private string _doneText;

		// Token: 0x040007C1 RID: 1985
		private string _heroText;

		// Token: 0x040007C2 RID: 1986
		private string _victoriesText;

		// Token: 0x040007C3 RID: 1987
		private string _rankText;

		// Token: 0x040007C4 RID: 1988
		private string _titleText;

		// Token: 0x040007C5 RID: 1989
		private MBBindingList<TournamentLeaderboardEntryItemVM> _entries;

		// Token: 0x040007C6 RID: 1990
		private TournamentLeaderboardSortControllerVM _sortController;
	}
}
