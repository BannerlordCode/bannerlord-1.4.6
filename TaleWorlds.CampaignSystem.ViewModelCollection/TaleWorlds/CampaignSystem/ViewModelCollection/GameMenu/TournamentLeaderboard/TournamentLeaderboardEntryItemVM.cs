using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TournamentLeaderboard
{
	// Token: 0x020000AD RID: 173
	public class TournamentLeaderboardEntryItemVM : ViewModel
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00044036 File Offset: 0x00042236
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x0004403E File Offset: 0x0004223E
		public int Rank { get; private set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00044047 File Offset: 0x00042247
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x0004404F File Offset: 0x0004224F
		public float PrizeValue { get; private set; }

		// Token: 0x060010C4 RID: 4292 RVA: 0x00044058 File Offset: 0x00042258
		public TournamentLeaderboardEntryItemVM(Hero hero, int victories, int placement)
		{
			this._heroObj = hero;
			this.PrizeStr = "-";
			this.Rank = placement;
			this.PlacementOnLeaderboard = placement;
			this.IsChampion = placement == 1;
			this.Victories = victories;
			float num;
			if (float.TryParse(this.PrizeStr, out num))
			{
				this.PrizeValue = num;
			}
			this.IsMainHero = hero == TaleWorlds.CampaignSystem.Hero.MainHero;
			this.Hero = new HeroVM(hero, false);
			this.ChampionRewardsHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetTournamentChampionRewardsTooltip(hero, null));
			this.RefreshValues();
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00044108 File Offset: 0x00042308
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._heroObj.Name.ToString();
			GameTexts.SetVariable("RANK", this.Rank);
			this.RankText = GameTexts.FindText("str_leaderboard_rank", null).ToString();
			HeroVM hero = this.Hero;
			if (hero == null)
			{
				return;
			}
			hero.RefreshValues();
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00044167 File Offset: 0x00042367
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x0004416F File Offset: 0x0004236F
		[DataSourceProperty]
		public BasicTooltipViewModel ChampionRewardsHint
		{
			get
			{
				return this._championRewardsHint;
			}
			set
			{
				if (value != this._championRewardsHint)
				{
					this._championRewardsHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ChampionRewardsHint");
				}
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0004418D File Offset: 0x0004238D
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x00044195 File Offset: 0x00042395
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x000441B8 File Offset: 0x000423B8
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x000441C0 File Offset: 0x000423C0
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000441E3 File Offset: 0x000423E3
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x000441EB File Offset: 0x000423EB
		[DataSourceProperty]
		public int Victories
		{
			get
			{
				return this._victories;
			}
			set
			{
				if (value != this._victories)
				{
					this._victories = value;
					base.OnPropertyChangedWithValue(value, "Victories");
				}
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00044209 File Offset: 0x00042409
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00044211 File Offset: 0x00042411
		[DataSourceProperty]
		public bool IsChampion
		{
			get
			{
				return this._isChampion;
			}
			set
			{
				if (value != this._isChampion)
				{
					this._isChampion = value;
					base.OnPropertyChangedWithValue(value, "IsChampion");
				}
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0004422F File Offset: 0x0004242F
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x00044237 File Offset: 0x00042437
		[DataSourceProperty]
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChangedWithValue(value, "IsMainHero");
				}
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00044255 File Offset: 0x00042455
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x0004425D File Offset: 0x0004245D
		[DataSourceProperty]
		public HeroVM Hero
		{
			get
			{
				return this._hero;
			}
			set
			{
				if (value != this._hero)
				{
					this._hero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "Hero");
				}
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0004427B File Offset: 0x0004247B
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00044283 File Offset: 0x00042483
		[DataSourceProperty]
		public string PrizeStr
		{
			get
			{
				return this._prizeStr;
			}
			set
			{
				if (value != this._prizeStr)
				{
					this._prizeStr = value;
					base.OnPropertyChangedWithValue<string>(value, "PrizeStr");
				}
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000442A6 File Offset: 0x000424A6
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x000442AE File Offset: 0x000424AE
		[DataSourceProperty]
		public int PlacementOnLeaderboard
		{
			get
			{
				return this._placementOnLeaderboard;
			}
			set
			{
				if (value != this._placementOnLeaderboard)
				{
					this._placementOnLeaderboard = value;
					base.OnPropertyChangedWithValue(value, "PlacementOnLeaderboard");
				}
			}
		}

		// Token: 0x040007A7 RID: 1959
		private readonly Hero _heroObj;

		// Token: 0x040007A8 RID: 1960
		private int _placementOnLeaderboard;

		// Token: 0x040007A9 RID: 1961
		private int _victories;

		// Token: 0x040007AA RID: 1962
		private bool _isMainHero;

		// Token: 0x040007AB RID: 1963
		private bool _isChampion;

		// Token: 0x040007AC RID: 1964
		private string _name;

		// Token: 0x040007AD RID: 1965
		private string _rankText;

		// Token: 0x040007AE RID: 1966
		private string _prizeStr;

		// Token: 0x040007AF RID: 1967
		private HeroVM _hero;

		// Token: 0x040007B0 RID: 1968
		private BasicTooltipViewModel _championRewardsHint;
	}
}
