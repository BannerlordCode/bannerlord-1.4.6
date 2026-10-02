using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Conversation
{
	// Token: 0x02000119 RID: 281
	public class PersuasionOptionVM : ViewModel
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000624E8 File Offset: 0x000606E8
		private ConversationSentenceOption _option
		{
			get
			{
				return this._manager.CurOptions[this._index];
			}
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00062500 File Offset: 0x00060700
		public PersuasionOptionVM(ConversationManager manager, int index, Action onReadyToContinue)
		{
			this._index = index;
			this._manager = manager;
			this._onReadyToContinue = onReadyToContinue;
			if (ConversationManager.GetPersuasionIsActive() && this._option.HasPersuasion)
			{
				float num;
				float num2;
				float num3;
				float num4;
				this._manager.GetPersuasionChances(this._option, out num, out num2, out num3, out num4);
				this.CritFailChance = (int)(num3 * 100f);
				this.FailChance = (int)(num4 * 100f);
				this.SuccessChance = (int)(num * 100f);
				this.CritSuccessChance = (int)(num2 * 100f);
				this._args = this._option.PersuationOptionArgs;
			}
			this.RefreshValues();
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000625AC File Offset: 0x000607AC
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (ConversationManager.GetPersuasionIsActive() && this._option.HasPersuasion)
			{
				GameTexts.SetVariable("NUMBER", this.CritFailChance);
				this.CritFailChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				GameTexts.SetVariable("NUMBER", this.FailChance);
				this.FailChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				GameTexts.SetVariable("NUMBER", this.SuccessChance);
				this.SuccessChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				GameTexts.SetVariable("NUMBER", this.CritSuccessChance);
				this.CritSuccessChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				this.CritFailHint = new BasicTooltipViewModel(delegate
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_critical_fail", null));
					GameTexts.SetVariable("NUMBER", this.CritFailChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.FailHint = new BasicTooltipViewModel(delegate
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_fail", null));
					GameTexts.SetVariable("NUMBER", this.FailChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.SuccessHint = new BasicTooltipViewModel(delegate
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_success", null));
					GameTexts.SetVariable("NUMBER", this.SuccessChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.CritSuccessHint = new BasicTooltipViewModel(delegate
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_critical_success", null));
					GameTexts.SetVariable("NUMBER", this.CritSuccessChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.ProgressingOptionHint = new HintViewModel(GameTexts.FindText("str_persuasion_progressing_hint", null), null);
				this.BlockingOptionHint = new HintViewModel(GameTexts.FindText("str_persuasion_blocking_hint", null), null);
				this.IsABlockingOption = this._args.CanBlockOtherOption;
				this.IsAProgressingOption = this._args.CanMoveToTheNextReservation;
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0006271D File Offset: 0x0006091D
		internal void OnPersuasionProgress(Tuple<PersuasionOptionArgs, PersuasionOptionResult> result)
		{
			this.IsPersuasionResultReady = true;
			if (result.Item1 == this._args)
			{
				this.PersuasionResultIndex = (int)result.Item2;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00062740 File Offset: 0x00060940
		public string GetPersuasionAdditionalText()
		{
			string text = null;
			if (this._args != null)
			{
				if (this._args.SkillUsed != null)
				{
					text = ((Hero.MainHero.GetSkillValue(this._args.SkillUsed) <= 50) ? "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>" : "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>").Replace("{TEXT}", this._args.SkillUsed.Name.ToString());
				}
				if (this._args.TraitUsed != null && !this._args.TraitUsed.IsHidden)
				{
					int traitLevel = Hero.MainHero.GetTraitLevel(this._args.TraitUsed);
					string text2;
					if (traitLevel == 0)
					{
						text2 = "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>";
					}
					else
					{
						text2 = (((traitLevel > 0 && this._args.TraitEffect == TraitEffect.Positive) || (traitLevel < 0 && this._args.TraitEffect == TraitEffect.Negative)) ? "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>" : "<a style=\"Conversation.Persuasion.Negative\"><b>{TEXT}</b></a>");
					}
					text2 = text2.Replace("{TEXT}", this._args.TraitUsed.Name.ToString());
					if (text != null)
					{
						GameTexts.SetVariable("LEFT", text);
						GameTexts.SetVariable("RIGHT", text2);
						text = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
					}
					else
					{
						text = text2;
					}
				}
				if (this._args.TraitCorrelation != null)
				{
					foreach (Tuple<TraitObject, int> tuple in this._args.TraitCorrelation)
					{
						if (tuple.Item2 != 0 && this._args.TraitUsed != tuple.Item1 && !tuple.Item1.IsHidden)
						{
							int traitLevel2 = Hero.MainHero.GetTraitLevel(tuple.Item1);
							string text3;
							if (traitLevel2 == 0)
							{
								text3 = "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>";
							}
							else
							{
								text3 = ((traitLevel2 * tuple.Item2 > 0) ? "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>" : "<a style=\"Conversation.Persuasion.Negative\"><b>{TEXT}</b></a>");
							}
							text3 = text3.Replace("{TEXT}", tuple.Item1.Name.ToString());
							if (text != null)
							{
								GameTexts.SetVariable("LEFT", text);
								GameTexts.SetVariable("RIGHT", text3);
								text = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
							}
							else
							{
								text = text3;
							}
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				GameTexts.SetVariable("STR", text);
				return GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			}
			return string.Empty;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000629A1 File Offset: 0x00060BA1
		public void ExecuteReadyToContinue()
		{
			Action onReadyToContinue = this._onReadyToContinue;
			if (onReadyToContinue == null)
			{
				return;
			}
			onReadyToContinue.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x000629B9 File Offset: 0x00060BB9
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x000629C1 File Offset: 0x00060BC1
		[DataSourceProperty]
		public bool IsPersuasionResultReady
		{
			get
			{
				return this._isPersuasionResultReady;
			}
			set
			{
				if (this._isPersuasionResultReady != value)
				{
					this._isPersuasionResultReady = value;
					base.OnPropertyChangedWithValue(value, "IsPersuasionResultReady");
				}
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x000629DF File Offset: 0x00060BDF
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x000629E7 File Offset: 0x00060BE7
		[DataSourceProperty]
		public bool IsABlockingOption
		{
			get
			{
				return this._isABlockingOption;
			}
			set
			{
				if (this._isABlockingOption != value)
				{
					this._isABlockingOption = value;
					base.OnPropertyChangedWithValue(value, "IsABlockingOption");
				}
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00062A05 File Offset: 0x00060C05
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00062A0D File Offset: 0x00060C0D
		[DataSourceProperty]
		public bool IsAProgressingOption
		{
			get
			{
				return this._isAProgressingOption;
			}
			set
			{
				if (this._isAProgressingOption != value)
				{
					this._isAProgressingOption = value;
					base.OnPropertyChangedWithValue(value, "IsAProgressingOption");
				}
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00062A2B File Offset: 0x00060C2B
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00062A33 File Offset: 0x00060C33
		[DataSourceProperty]
		public int SuccessChance
		{
			get
			{
				return this._successChance;
			}
			set
			{
				if (this._successChance != value)
				{
					this._successChance = value;
					base.OnPropertyChangedWithValue(value, "SuccessChance");
				}
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00062A51 File Offset: 0x00060C51
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x00062A59 File Offset: 0x00060C59
		[DataSourceProperty]
		public int PersuasionResultIndex
		{
			get
			{
				return this._persuasionResultIndex;
			}
			set
			{
				if (this._persuasionResultIndex != value)
				{
					this._persuasionResultIndex = value;
					base.OnPropertyChangedWithValue(value, "PersuasionResultIndex");
				}
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00062A77 File Offset: 0x00060C77
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00062A7F File Offset: 0x00060C7F
		[DataSourceProperty]
		public int FailChance
		{
			get
			{
				return this._failChance;
			}
			set
			{
				if (this._failChance != value)
				{
					this._failChance = value;
					base.OnPropertyChangedWithValue(value, "FailChance");
				}
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00062A9D File Offset: 0x00060C9D
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x00062AA5 File Offset: 0x00060CA5
		[DataSourceProperty]
		public int CritSuccessChance
		{
			get
			{
				return this._critSuccessChance;
			}
			set
			{
				if (this._critSuccessChance != value)
				{
					this._critSuccessChance = value;
					base.OnPropertyChangedWithValue(value, "CritSuccessChance");
				}
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00062AC3 File Offset: 0x00060CC3
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x00062ACB File Offset: 0x00060CCB
		[DataSourceProperty]
		public int CritFailChance
		{
			get
			{
				return this._critFailChance;
			}
			set
			{
				if (this._critFailChance != value)
				{
					this._critFailChance = value;
					base.OnPropertyChangedWithValue(value, "CritFailChance");
				}
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x00062AE9 File Offset: 0x00060CE9
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x00062AF1 File Offset: 0x00060CF1
		[DataSourceProperty]
		public string FailChanceText
		{
			get
			{
				return this._failChanceText;
			}
			set
			{
				if (this._failChanceText != value)
				{
					this._failChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "FailChanceText");
				}
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00062B14 File Offset: 0x00060D14
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x00062B1C File Offset: 0x00060D1C
		[DataSourceProperty]
		public string CritFailChanceText
		{
			get
			{
				return this._critFailChanceText;
			}
			set
			{
				if (this._critFailChanceText != value)
				{
					this._critFailChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "CritFailChanceText");
				}
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x00062B3F File Offset: 0x00060D3F
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x00062B47 File Offset: 0x00060D47
		[DataSourceProperty]
		public string SuccessChanceText
		{
			get
			{
				return this._successChanceText;
			}
			set
			{
				if (this._successChanceText != value)
				{
					this._successChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "SuccessChanceText");
				}
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00062B6A File Offset: 0x00060D6A
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x00062B72 File Offset: 0x00060D72
		[DataSourceProperty]
		public string CritSuccessChanceText
		{
			get
			{
				return this._critSuccessChanceText;
			}
			set
			{
				if (this._critSuccessChanceText != value)
				{
					this._critSuccessChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "CritSuccessChanceText");
				}
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00062B95 File Offset: 0x00060D95
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x00062B9D File Offset: 0x00060D9D
		[DataSourceProperty]
		public BasicTooltipViewModel CritFailHint
		{
			get
			{
				return this._critFailHint;
			}
			set
			{
				if (this._critFailHint != value)
				{
					this._critFailHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CritFailHint");
				}
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00062BBB File Offset: 0x00060DBB
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x00062BC3 File Offset: 0x00060DC3
		[DataSourceProperty]
		public BasicTooltipViewModel FailHint
		{
			get
			{
				return this._failHint;
			}
			set
			{
				if (this._failHint != value)
				{
					this._failHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "FailHint");
				}
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00062BE1 File Offset: 0x00060DE1
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x00062BE9 File Offset: 0x00060DE9
		[DataSourceProperty]
		public BasicTooltipViewModel SuccessHint
		{
			get
			{
				return this._successHint;
			}
			set
			{
				if (this._successHint != value)
				{
					this._successHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SuccessHint");
				}
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00062C07 File Offset: 0x00060E07
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x00062C0F File Offset: 0x00060E0F
		[DataSourceProperty]
		public BasicTooltipViewModel CritSuccessHint
		{
			get
			{
				return this._critSuccessHint;
			}
			set
			{
				if (this._critSuccessHint != value)
				{
					this._critSuccessHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CritSuccessHint");
				}
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00062C2D File Offset: 0x00060E2D
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x00062C35 File Offset: 0x00060E35
		[DataSourceProperty]
		public HintViewModel BlockingOptionHint
		{
			get
			{
				return this._blockingOptionHint;
			}
			set
			{
				if (this._blockingOptionHint != value)
				{
					this._blockingOptionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BlockingOptionHint");
				}
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00062C53 File Offset: 0x00060E53
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x00062C5B File Offset: 0x00060E5B
		[DataSourceProperty]
		public HintViewModel ProgressingOptionHint
		{
			get
			{
				return this._progressingOptionHint;
			}
			set
			{
				if (this._progressingOptionHint != value)
				{
					this._progressingOptionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ProgressingOptionHint");
				}
			}
		}

		// Token: 0x04000BE6 RID: 3046
		private const int _minSkillValueForPositive = 50;

		// Token: 0x04000BE7 RID: 3047
		private readonly ConversationManager _manager;

		// Token: 0x04000BE8 RID: 3048
		private readonly PersuasionOptionArgs _args;

		// Token: 0x04000BE9 RID: 3049
		private readonly Action _onReadyToContinue;

		// Token: 0x04000BEA RID: 3050
		private readonly int _index;

		// Token: 0x04000BEB RID: 3051
		private int _critFailChance;

		// Token: 0x04000BEC RID: 3052
		private int _failChance;

		// Token: 0x04000BED RID: 3053
		private int _successChance;

		// Token: 0x04000BEE RID: 3054
		private int _critSuccessChance;

		// Token: 0x04000BEF RID: 3055
		private bool _isPersuasionResultReady;

		// Token: 0x04000BF0 RID: 3056
		private int _persuasionResultIndex = -1;

		// Token: 0x04000BF1 RID: 3057
		private bool _isABlockingOption;

		// Token: 0x04000BF2 RID: 3058
		private bool _isAProgressingOption;

		// Token: 0x04000BF3 RID: 3059
		private string _critFailChanceText;

		// Token: 0x04000BF4 RID: 3060
		private string _failChanceText;

		// Token: 0x04000BF5 RID: 3061
		private string _successChanceText;

		// Token: 0x04000BF6 RID: 3062
		private string _critSuccessChanceText;

		// Token: 0x04000BF7 RID: 3063
		private BasicTooltipViewModel _critFailHint;

		// Token: 0x04000BF8 RID: 3064
		private BasicTooltipViewModel _failHint;

		// Token: 0x04000BF9 RID: 3065
		private BasicTooltipViewModel _successHint;

		// Token: 0x04000BFA RID: 3066
		private BasicTooltipViewModel _critSuccessHint;

		// Token: 0x04000BFB RID: 3067
		private HintViewModel _progressingOptionHint;

		// Token: 0x04000BFC RID: 3068
		private HintViewModel _blockingOptionHint;
	}
}
