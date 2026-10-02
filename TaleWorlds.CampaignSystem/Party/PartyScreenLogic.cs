using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Party
{
	// Token: 0x020002FF RID: 767
	public class PartyScreenLogic
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06002C8F RID: 11407 RVA: 0x000BAB20 File Offset: 0x000B8D20
		// (remove) Token: 0x06002C90 RID: 11408 RVA: 0x000BAB58 File Offset: 0x000B8D58
		public event PartyScreenLogic.PartyGoldDelegate PartyGoldChange;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06002C91 RID: 11409 RVA: 0x000BAB90 File Offset: 0x000B8D90
		// (remove) Token: 0x06002C92 RID: 11410 RVA: 0x000BABC8 File Offset: 0x000B8DC8
		public event PartyScreenLogic.PartyMoraleDelegate PartyMoraleChange;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06002C93 RID: 11411 RVA: 0x000BAC00 File Offset: 0x000B8E00
		// (remove) Token: 0x06002C94 RID: 11412 RVA: 0x000BAC38 File Offset: 0x000B8E38
		public event PartyScreenLogic.PartyInfluenceDelegate PartyInfluenceChange;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06002C95 RID: 11413 RVA: 0x000BAC70 File Offset: 0x000B8E70
		// (remove) Token: 0x06002C96 RID: 11414 RVA: 0x000BACA8 File Offset: 0x000B8EA8
		public event PartyScreenLogic.PartyHorseDelegate PartyHorseChange;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06002C97 RID: 11415 RVA: 0x000BACE0 File Offset: 0x000B8EE0
		// (remove) Token: 0x06002C98 RID: 11416 RVA: 0x000BAD18 File Offset: 0x000B8F18
		public event PartyScreenLogic.PresentationUpdate Update;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06002C99 RID: 11417 RVA: 0x000BAD50 File Offset: 0x000B8F50
		// (remove) Token: 0x06002C9A RID: 11418 RVA: 0x000BAD88 File Offset: 0x000B8F88
		public event PartyScreenClosedDelegate PartyScreenClosedEvent;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06002C9B RID: 11419 RVA: 0x000BADC0 File Offset: 0x000B8FC0
		// (remove) Token: 0x06002C9C RID: 11420 RVA: 0x000BADF8 File Offset: 0x000B8FF8
		public event PartyScreenLogic.AfterResetDelegate AfterReset;

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000BAE2D File Offset: 0x000B902D
		// (set) Token: 0x06002C9E RID: 11422 RVA: 0x000BAE35 File Offset: 0x000B9035
		public PartyScreenLogic.TroopSortType ActiveOtherPartySortType
		{
			get
			{
				return this._activeOtherPartySortType;
			}
			set
			{
				this._activeOtherPartySortType = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000BAE3E File Offset: 0x000B903E
		// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x000BAE46 File Offset: 0x000B9046
		public PartyScreenLogic.TroopSortType ActiveMainPartySortType
		{
			get
			{
				return this._activeMainPartySortType;
			}
			set
			{
				this._activeMainPartySortType = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000BAE4F File Offset: 0x000B904F
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x000BAE57 File Offset: 0x000B9057
		public bool IsOtherPartySortAscending
		{
			get
			{
				return this._isOtherPartySortAscending;
			}
			set
			{
				this._isOtherPartySortAscending = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000BAE60 File Offset: 0x000B9060
		// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x000BAE68 File Offset: 0x000B9068
		public bool IsMainPartySortAscending
		{
			get
			{
				return this._isMainPartySortAscending;
			}
			set
			{
				this._isMainPartySortAscending = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000BAE71 File Offset: 0x000B9071
		// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x000BAE79 File Offset: 0x000B9079
		public PartyScreenLogic.TransferState MemberTransferState { get; private set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000BAE82 File Offset: 0x000B9082
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x000BAE8A File Offset: 0x000B908A
		public PartyScreenLogic.TransferState PrisonerTransferState { get; private set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000BAE93 File Offset: 0x000B9093
		// (set) Token: 0x06002CAA RID: 11434 RVA: 0x000BAE9B File Offset: 0x000B909B
		public PartyScreenLogic.TransferState AccompanyingTransferState { get; private set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000BAEA4 File Offset: 0x000B90A4
		// (set) Token: 0x06002CAC RID: 11436 RVA: 0x000BAEAC File Offset: 0x000B90AC
		public TextObject LeftPartyName { get; private set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000BAEB5 File Offset: 0x000B90B5
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x000BAEBD File Offset: 0x000B90BD
		public TextObject RightPartyName { get; private set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000BAEC6 File Offset: 0x000B90C6
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x000BAECE File Offset: 0x000B90CE
		public TextObject Header { get; private set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000BAED7 File Offset: 0x000B90D7
		// (set) Token: 0x06002CB2 RID: 11442 RVA: 0x000BAEDF File Offset: 0x000B90DF
		public int LeftPartyMembersSizeLimit { get; private set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000BAEE8 File Offset: 0x000B90E8
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x000BAEF0 File Offset: 0x000B90F0
		public int LeftPartyPrisonersSizeLimit { get; private set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000BAEF9 File Offset: 0x000B90F9
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000BAF01 File Offset: 0x000B9101
		public int RightPartyMembersSizeLimit { get; private set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000BAF0A File Offset: 0x000B910A
		// (set) Token: 0x06002CB8 RID: 11448 RVA: 0x000BAF12 File Offset: 0x000B9112
		public int RightPartyPrisonersSizeLimit { get; private set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000BAF1B File Offset: 0x000B911B
		// (set) Token: 0x06002CBA RID: 11450 RVA: 0x000BAF23 File Offset: 0x000B9123
		public bool DoNotApplyGoldTransactions { get; private set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x000BAF2C File Offset: 0x000B912C
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x000BAF34 File Offset: 0x000B9134
		public bool ShowProgressBar { get; private set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000BAF3D File Offset: 0x000B913D
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x000BAF45 File Offset: 0x000B9145
		public string DoneReasonString { get; private set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000BAF4E File Offset: 0x000B914E
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x000BAF56 File Offset: 0x000B9156
		public bool IsTroopUpgradesDisabled { get; private set; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000BAF5F File Offset: 0x000B915F
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000BAF67 File Offset: 0x000B9167
		public CharacterObject RightPartyLeader { get; private set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000BAF70 File Offset: 0x000B9170
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x000BAF78 File Offset: 0x000B9178
		public CharacterObject LeftPartyLeader { get; private set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000BAF81 File Offset: 0x000B9181
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x000BAF89 File Offset: 0x000B9189
		public PartyBase LeftOwnerParty { get; private set; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000BAF92 File Offset: 0x000B9192
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000BAF9A File Offset: 0x000B919A
		public PartyBase RightOwnerParty { get; private set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000BAFA3 File Offset: 0x000B91A3
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000BAFAB File Offset: 0x000B91AB
		public PartyScreenData CurrentData { get; private set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000BAFB4 File Offset: 0x000B91B4
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x000BAFBC File Offset: 0x000B91BC
		public bool TransferHealthiesGetWoundedsFirst { get; private set; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x000BAFC5 File Offset: 0x000B91C5
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x000BAFCD File Offset: 0x000B91CD
		public int QuestModeWageDaysMultiplier { get; private set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000BAFD6 File Offset: 0x000B91D6
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x000BAFDE File Offset: 0x000B91DE
		public Game Game
		{
			get
			{
				return this._game;
			}
			set
			{
				this._game = value;
			}
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000BAFE8 File Offset: 0x000B91E8
		public PartyScreenLogic()
		{
			this._game = Game.Current;
			this.MemberRosters = new TroopRoster[2];
			this.PrisonerRosters = new TroopRoster[2];
			this.CurrentData = new PartyScreenData();
			this._initialData = new PartyScreenData();
			this._defaultComparers = new Dictionary<PartyScreenLogic.TroopSortType, PartyScreenLogic.TroopComparer>
			{
				{
					PartyScreenLogic.TroopSortType.Custom,
					new PartyScreenLogic.TroopDefaultComparer()
				},
				{
					PartyScreenLogic.TroopSortType.Type,
					new PartyScreenLogic.TroopTypeComparer()
				},
				{
					PartyScreenLogic.TroopSortType.Name,
					new PartyScreenLogic.TroopNameComparer()
				},
				{
					PartyScreenLogic.TroopSortType.Count,
					new PartyScreenLogic.TroopCountComparer()
				},
				{
					PartyScreenLogic.TroopSortType.Tier,
					new PartyScreenLogic.TroopTierComparer()
				}
			};
			this.IsTroopUpgradesDisabled = false;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000BB084 File Offset: 0x000B9284
		public void Initialize(PartyScreenLogicInitializationData initializationData)
		{
			this.MemberRosters[1] = initializationData.RightMemberRoster;
			this.PrisonerRosters[1] = initializationData.RightPrisonerRoster;
			this.MemberRosters[0] = initializationData.LeftMemberRoster;
			this.PrisonerRosters[0] = initializationData.LeftPrisonerRoster;
			Hero rightLeaderHero = initializationData.RightLeaderHero;
			this.RightPartyLeader = ((rightLeaderHero != null) ? rightLeaderHero.CharacterObject : null);
			Hero leftLeaderHero = initializationData.LeftLeaderHero;
			this.LeftPartyLeader = ((leftLeaderHero != null) ? leftLeaderHero.CharacterObject : null);
			this.RightOwnerParty = initializationData.RightOwnerParty;
			this.LeftOwnerParty = initializationData.LeftOwnerParty;
			this.RightPartyName = initializationData.RightPartyName;
			this.RightPartyMembersSizeLimit = initializationData.RightPartyMembersSizeLimit;
			this.RightPartyPrisonersSizeLimit = initializationData.RightPartyPrisonersSizeLimit;
			this.LeftPartyName = initializationData.LeftPartyName;
			this.LeftPartyMembersSizeLimit = initializationData.LeftPartyMembersSizeLimit;
			this.LeftPartyPrisonersSizeLimit = initializationData.LeftPartyPrisonersSizeLimit;
			this.Header = initializationData.Header;
			this.QuestModeWageDaysMultiplier = initializationData.QuestModeWageDaysMultiplier;
			this.TransferHealthiesGetWoundedsFirst = initializationData.TransferHealthiesGetWoundedsFirst;
			this.SetPartyGoldChangeAmount(0);
			this.SetHorseChangeAmount(0);
			this.SetInfluenceChangeAmount(0, 0, 0);
			this.SetMoraleChangeAmount(0);
			this.CurrentData.BindRostersFrom(this.MemberRosters[1], this.PrisonerRosters[1], this.MemberRosters[0], this.PrisonerRosters[0], this.RightOwnerParty, this.LeftOwnerParty);
			this._initialData.InitializeCopyFrom(initializationData.RightOwnerParty, initializationData.LeftOwnerParty);
			this._initialData.CopyFromPartyAndRoster(this.MemberRosters[1], this.PrisonerRosters[1], this.MemberRosters[0], this.PrisonerRosters[0], this.RightOwnerParty);
			if (initializationData.PartyPresentationDoneButtonDelegate == null)
			{
				Debug.FailedAssert("Done handler is given null for party screen!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\PartyScreenLogic.cs", "Initialize", 242);
				initializationData.PartyPresentationDoneButtonDelegate = new PartyPresentationDoneButtonDelegate(PartyScreenLogic.DefaultDoneHandler);
			}
			this.PartyPresentationDoneButtonDelegate = initializationData.PartyPresentationDoneButtonDelegate;
			this.PartyPresentationDoneButtonConditionDelegate = initializationData.PartyPresentationDoneButtonConditionDelegate;
			this.PartyPresentationCancelButtonActivateDelegate = initializationData.PartyPresentationCancelButtonActivateDelegate;
			this.PartyPresentationCancelButtonDelegate = initializationData.PartyPresentationCancelButtonDelegate;
			this.IsTroopUpgradesDisabled = initializationData.IsTroopUpgradesDisabled || initializationData.RightOwnerParty == null;
			this.MemberTransferState = initializationData.MemberTransferState;
			this.PrisonerTransferState = initializationData.PrisonerTransferState;
			this.AccompanyingTransferState = initializationData.AccompanyingTransferState;
			this.IsTroopTransferableDelegate = initializationData.TroopTransferableDelegate;
			this.CanTalkToHeroDelegate = initializationData.CanTalkToTroopDelegate;
			this.PartyPresentationCancelButtonActivateDelegate = initializationData.PartyPresentationCancelButtonActivateDelegate;
			this.PartyPresentationCancelButtonDelegate = initializationData.PartyPresentationCancelButtonDelegate;
			this.PartyScreenClosedEvent = initializationData.PartyScreenClosedDelegate;
			this.DoNotApplyGoldTransactions = initializationData.DoNotApplyGoldTransactions;
			this.ShowProgressBar = initializationData.ShowProgressBar;
			if (this._partyScreenMode == PartyScreenHelper.PartyScreenMode.QuestTroopManage)
			{
				int num = -this.MemberRosters[0].Sum((TroopRosterElement t) => t.Character.TroopWage * t.Number * this.QuestModeWageDaysMultiplier);
				this._initialData.PartyGoldChangeAmount = num;
				this.SetPartyGoldChangeAmount(num);
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000BB347 File Offset: 0x000B9547
		private void SetPartyGoldChangeAmount(int newTotalAmount)
		{
			this.CurrentData.PartyGoldChangeAmount = newTotalAmount;
			PartyScreenLogic.PartyGoldDelegate partyGoldChange = this.PartyGoldChange;
			if (partyGoldChange == null)
			{
				return;
			}
			partyGoldChange();
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000BB365 File Offset: 0x000B9565
		private void SetMoraleChangeAmount(int newAmount)
		{
			this.CurrentData.PartyMoraleChangeAmount = newAmount;
			PartyScreenLogic.PartyMoraleDelegate partyMoraleChange = this.PartyMoraleChange;
			if (partyMoraleChange == null)
			{
				return;
			}
			partyMoraleChange();
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000BB383 File Offset: 0x000B9583
		private void SetHorseChangeAmount(int newAmount)
		{
			this.CurrentData.PartyHorseChangeAmount = newAmount;
			PartyScreenLogic.PartyHorseDelegate partyHorseChange = this.PartyHorseChange;
			if (partyHorseChange == null)
			{
				return;
			}
			partyHorseChange();
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000BB3A1 File Offset: 0x000B95A1
		private void SetInfluenceChangeAmount(int heroInfluence, int troopInfluence, int prisonerInfluence)
		{
			this.CurrentData.PartyInfluenceChangeAmount = new ValueTuple<int, int, int>(heroInfluence, troopInfluence, prisonerInfluence);
			PartyScreenLogic.PartyInfluenceDelegate partyInfluenceChange = this.PartyInfluenceChange;
			if (partyInfluenceChange == null)
			{
				return;
			}
			partyInfluenceChange();
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000BB3C8 File Offset: 0x000B95C8
		private void ProcessCommand(PartyScreenLogic.PartyCommand command)
		{
			switch (command.Code)
			{
			case PartyScreenLogic.PartyCommandCode.TransferTroop:
				this.TransferTroop(command, true);
				return;
			case PartyScreenLogic.PartyCommandCode.UpgradeTroop:
				this.UpgradeTroop(command);
				return;
			case PartyScreenLogic.PartyCommandCode.TransferPartyLeaderTroop:
				this.TransferPartyLeaderTroop(command);
				return;
			case PartyScreenLogic.PartyCommandCode.TransferTroopToLeaderSlot:
				this.TransferTroopToLeaderSlot(command);
				return;
			case PartyScreenLogic.PartyCommandCode.ShiftTroop:
				this.ShiftTroop(command);
				return;
			case PartyScreenLogic.PartyCommandCode.RecruitTroop:
				this.RecruitPrisoner(command);
				return;
			case PartyScreenLogic.PartyCommandCode.ExecuteTroop:
				this.ExecuteTroop(command);
				return;
			case PartyScreenLogic.PartyCommandCode.TransferAllTroops:
				this.TransferAllTroops(command);
				return;
			case PartyScreenLogic.PartyCommandCode.SortTroops:
				this.SortTroops(command);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000BB44F File Offset: 0x000B964F
		public void AddCommand(PartyScreenLogic.PartyCommand command)
		{
			this.ProcessCommand(command);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000BB458 File Offset: 0x000B9658
		public bool ValidateCommand(PartyScreenLogic.PartyCommand command)
		{
			if (command.Code == PartyScreenLogic.PartyCommandCode.TransferTroop || command.Code == PartyScreenLogic.PartyCommandCode.TransferTroopToLeaderSlot)
			{
				CharacterObject character = command.Character;
				if (character == CharacterObject.PlayerCharacter)
				{
					return false;
				}
				int num;
				if (command.Type == PartyScreenLogic.TroopType.Member)
				{
					num = this.MemberRosters[(int)command.RosterSide].FindIndexOfTroop(character);
					bool flag = num != -1 && this.MemberRosters[(int)command.RosterSide].GetElementNumber(num) >= command.TotalNumber;
					bool flag2 = command.RosterSide != PartyScreenLogic.PartyRosterSide.Left || command.Index != 0;
					return flag && flag2;
				}
				num = this.PrisonerRosters[(int)command.RosterSide].FindIndexOfTroop(character);
				return num != -1 && this.PrisonerRosters[(int)command.RosterSide].GetElementNumber(num) >= command.TotalNumber;
			}
			else if (command.Code == PartyScreenLogic.PartyCommandCode.ShiftTroop)
			{
				CharacterObject character2 = command.Character;
				if (character2 == this.LeftPartyLeader || character2 == this.RightPartyLeader || ((command.RosterSide != PartyScreenLogic.PartyRosterSide.Left || (this.LeftPartyLeader != null && command.Index == 0)) && (command.RosterSide != PartyScreenLogic.PartyRosterSide.Right || (this.RightPartyLeader != null && command.Index == 0))))
				{
					return false;
				}
				int num2;
				if (command.Type == PartyScreenLogic.TroopType.Member)
				{
					num2 = this.MemberRosters[(int)command.RosterSide].FindIndexOfTroop(character2);
					return num2 != -1 && num2 != command.Index;
				}
				num2 = this.PrisonerRosters[(int)command.RosterSide].FindIndexOfTroop(character2);
				return num2 != -1 && num2 != command.Index;
			}
			else
			{
				if (command.Code == PartyScreenLogic.PartyCommandCode.TransferPartyLeaderTroop)
				{
					CharacterObject character3 = command.Character;
					BasicCharacterObject playerTroop = this._game.PlayerTroop;
					return false;
				}
				if (command.Code == PartyScreenLogic.PartyCommandCode.UpgradeTroop)
				{
					CharacterObject character4 = command.Character;
					int num3 = this.MemberRosters[(int)command.RosterSide].FindIndexOfTroop(character4);
					if (num3 == -1 || this.MemberRosters[(int)command.RosterSide].GetElementNumber(num3) < command.TotalNumber || character4.UpgradeTargets.Length == 0)
					{
						return false;
					}
					if (command.UpgradeTarget >= character4.UpgradeTargets.Length)
					{
						MBInformationManager.AddQuickInformation(new TextObject("{=kaQ7DsW3}Character does not have upgrade target.", null), 0, null, null, "");
						return false;
					}
					CharacterObject characterObject = character4.UpgradeTargets[command.UpgradeTarget];
					int upgradeXpCost = character4.GetUpgradeXpCost(PartyBase.MainParty, command.UpgradeTarget);
					int upgradeGoldCost = character4.GetUpgradeGoldCost(PartyBase.MainParty, command.UpgradeTarget);
					if (this.MemberRosters[(int)command.RosterSide].GetElementXp(num3) < upgradeXpCost * command.TotalNumber)
					{
						MBInformationManager.AddQuickInformation(new TextObject("{=m1bIfPf1}Character does not have enough experience for upgrade.", null), 0, null, null, "");
						return false;
					}
					CharacterObject characterObject2 = ((command.RosterSide == PartyScreenLogic.PartyRosterSide.Left) ? this.LeftPartyLeader : this.RightPartyLeader);
					int? num4 = ((characterObject2 != null) ? new int?(characterObject2.HeroObject.Gold) : null) + this.CurrentData.PartyGoldChangeAmount;
					int num5 = upgradeGoldCost * command.TotalNumber;
					if (!((num4.GetValueOrDefault() >= num5) & (num4 != null)))
					{
						MBTextManager.SetTextVariable("VALUE", upgradeGoldCost);
						MBInformationManager.AddQuickInformation(GameTexts.FindText("str_gold_needed_for_upgrade", null), 0, null, null, "");
						return false;
					}
					if (characterObject.UpgradeRequiresItemFromCategory == null)
					{
						return true;
					}
					foreach (ItemRosterElement itemRosterElement in this.RightOwnerParty.ItemRoster)
					{
						if (itemRosterElement.EquipmentElement.Item.ItemCategory == characterObject.UpgradeRequiresItemFromCategory)
						{
							return true;
						}
					}
					MBTextManager.SetTextVariable("REQUIRED_ITEM", characterObject.UpgradeRequiresItemFromCategory.GetName(), false);
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_item_needed_for_upgrade", null), 0, null, null, "");
					return false;
				}
				else
				{
					if (command.Code == PartyScreenLogic.PartyCommandCode.RecruitTroop)
					{
						return this.IsPrisonerRecruitable(command.Type, command.Character, command.RosterSide);
					}
					if (command.Code == PartyScreenLogic.PartyCommandCode.ExecuteTroop)
					{
						return this.IsExecutable(command.Type, command.Character, command.RosterSide);
					}
					if (command.Code == PartyScreenLogic.PartyCommandCode.TransferAllTroops)
					{
						return this.GetRoster(command.RosterSide, command.Type).Count != 0;
					}
					if (command.Code == PartyScreenLogic.PartyCommandCode.SortTroops)
					{
						return this.GetActiveSortTypeForSide(command.RosterSide) != command.SortType || this.GetIsAscendingSortForSide(command.RosterSide) != command.IsSortAscending;
					}
					throw new MBUnknownTypeException("Unknown command type in ValidateCommand.");
				}
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000BB90C File Offset: 0x000B9B0C
		private void OnReset(bool fromCancel)
		{
			PartyScreenLogic.AfterResetDelegate afterReset = this.AfterReset;
			if (afterReset == null)
			{
				return;
			}
			afterReset(this, fromCancel);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000BB920 File Offset: 0x000B9B20
		protected void TransferTroopToLeaderSlot(PartyScreenLogic.PartyCommand command)
		{
			bool flag = false;
			if (this.ValidateCommand(command))
			{
				CharacterObject character = command.Character;
				if (command.Type == PartyScreenLogic.TroopType.Member)
				{
					int num = this.MemberRosters[(int)command.RosterSide].FindIndexOfTroop(character);
					TroopRosterElement elementCopyAtIndex = this.MemberRosters[(int)command.RosterSide].GetElementCopyAtIndex(num);
					int num2 = command.TotalNumber * (elementCopyAtIndex.Xp / elementCopyAtIndex.Number);
					this.MemberRosters[(int)command.RosterSide].AddToCounts(character, -command.TotalNumber, false, -command.WoundedNumber, 0, true, num);
					this.MemberRosters[(int)(PartyScreenLogic.PartyRosterSide.Right - command.RosterSide)].AddToCounts(character, command.TotalNumber, false, command.WoundedNumber, 0, true, 0);
					if (elementCopyAtIndex.Number != command.TotalNumber)
					{
						this.MemberRosters[(int)command.RosterSide].AddXpToTroop(character, -num2);
					}
					this.MemberRosters[(int)(PartyScreenLogic.PartyRosterSide.Right - command.RosterSide)].AddXpToTroop(character, num2);
				}
				flag = true;
			}
			if (flag)
			{
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate != null)
				{
					updateDelegate(command);
				}
				PartyScreenLogic.PresentationUpdate update = this.Update;
				if (update == null)
				{
					return;
				}
				update(command);
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000BBA40 File Offset: 0x000B9C40
		protected void TransferTroop(PartyScreenLogic.PartyCommand command, bool invokeUpdate)
		{
			bool flag = false;
			if (this.ValidateCommand(command))
			{
				CharacterObject troop = command.Character;
				if (command.Type == PartyScreenLogic.TroopType.Member)
				{
					TroopRoster troopRoster = this.MemberRosters[(int)command.RosterSide];
					TroopRoster troopRoster2 = this.MemberRosters[(int)(PartyScreenLogic.PartyRosterSide.Right - command.RosterSide)];
					int num = troopRoster.FindIndexOfTroop(troop);
					TroopRosterElement elementCopyAtIndex = troopRoster.GetElementCopyAtIndex(num);
					int num2 = ((troop.UpgradeTargets.Length != 0) ? troop.UpgradeTargets.Max<CharacterObject>((CharacterObject x) => Campaign.Current.Models.PartyTroopUpgradeModel.GetXpCostForUpgrade(PartyBase.MainParty, troop, x)) : 0);
					int num4;
					if (command.RosterSide == PartyScreenLogic.PartyRosterSide.Right)
					{
						int num3 = (elementCopyAtIndex.Number - command.TotalNumber) * num2;
						num4 = ((elementCopyAtIndex.Xp >= num3 && num3 >= 0) ? (elementCopyAtIndex.Xp - num3) : 0);
					}
					else
					{
						int num5 = command.TotalNumber * num2;
						num4 = ((elementCopyAtIndex.Xp > num5 && num5 >= 0) ? num5 : elementCopyAtIndex.Xp);
						troopRoster.AddXpToTroop(troop, -num4);
					}
					troopRoster.AddToCounts(troop, -command.TotalNumber, false, -command.WoundedNumber, 0, false, -1);
					int num6 = command.Index;
					if (num6 == troopRoster2.Count && troopRoster2.Contains(troop))
					{
						num6 = troopRoster2.Count - 1;
					}
					troopRoster2.AddToCounts(troop, command.TotalNumber, false, command.WoundedNumber, 0, false, num6);
					troopRoster2.AddXpToTroop(troop, num4);
				}
				else
				{
					TroopRoster troopRoster3 = this.PrisonerRosters[(int)command.RosterSide];
					TroopRoster troopRoster4 = this.PrisonerRosters[(int)(PartyScreenLogic.PartyRosterSide.Right - command.RosterSide)];
					int num7 = troopRoster3.FindIndexOfTroop(troop);
					TroopRosterElement elementCopyAtIndex2 = troopRoster3.GetElementCopyAtIndex(num7);
					int conformityNeededToRecruitPrisoner = Campaign.Current.Models.PrisonerRecruitmentCalculationModel.GetConformityNeededToRecruitPrisoner(elementCopyAtIndex2.Character);
					int num9;
					if (command.RosterSide == PartyScreenLogic.PartyRosterSide.Right)
					{
						this.UpdatePrisonerTransferHistory(troop, -command.TotalNumber);
						int num8 = (elementCopyAtIndex2.Number - command.TotalNumber) * conformityNeededToRecruitPrisoner;
						num9 = ((elementCopyAtIndex2.Xp >= num8 && num8 >= 0) ? (elementCopyAtIndex2.Xp - num8) : 0);
					}
					else
					{
						this.UpdatePrisonerTransferHistory(troop, command.TotalNumber);
						int num10 = command.TotalNumber * conformityNeededToRecruitPrisoner;
						num9 = ((elementCopyAtIndex2.Xp > num10 && num10 >= 0) ? num10 : elementCopyAtIndex2.Xp);
						troopRoster3.AddXpToTroop(troop, -num9);
					}
					troopRoster3.AddToCounts(troop, -command.TotalNumber, false, -command.WoundedNumber, 0, false, -1);
					int num11 = command.Index;
					if (num11 == troopRoster4.Count && troopRoster4.Contains(troop))
					{
						num11 = troopRoster4.Count - 1;
					}
					troopRoster4.AddToCounts(troop, command.TotalNumber, false, command.WoundedNumber, 0, false, num11);
					troopRoster4.AddXpToTroop(troop, num9);
					if (this.CurrentData.RightRecruitableData.ContainsKey(troop))
					{
						this.CurrentData.RightRecruitableData[troop] = MathF.Max(MathF.Min(this.CurrentData.RightRecruitableData[troop], this.PrisonerRosters[1].GetElementNumber(troop)), Campaign.Current.Models.PrisonerRecruitmentCalculationModel.CalculateRecruitableNumber(PartyBase.MainParty, troop));
					}
				}
				flag = true;
			}
			if (flag)
			{
				if (this.PrisonerTransferState == PartyScreenLogic.TransferState.TransferableWithTrade && command.Type == PartyScreenLogic.TroopType.Prisoner)
				{
					int num12 = ((command.RosterSide == PartyScreenLogic.PartyRosterSide.Right) ? 1 : (-1));
					this.SetPartyGoldChangeAmount(this.CurrentData.PartyGoldChangeAmount + Campaign.Current.Models.RansomValueCalculationModel.PrisonerRansomValue(command.Character, Hero.MainHero) * command.TotalNumber * num12);
				}
				if (this._partyScreenMode == PartyScreenHelper.PartyScreenMode.QuestTroopManage)
				{
					int num13 = ((command.RosterSide == PartyScreenLogic.PartyRosterSide.Right) ? (-1) : 1);
					this.SetPartyGoldChangeAmount(this.CurrentData.PartyGoldChangeAmount + command.Character.TroopWage * command.TotalNumber * this.QuestModeWageDaysMultiplier * num13);
				}
				PartyState activePartyState = PartyScreenHelper.GetActivePartyState();
				if (activePartyState != null && activePartyState.IsDonating)
				{
					Settlement currentSettlement = Hero.MainHero.CurrentSettlement;
					float num14 = 0f;
					float num15 = 0f;
					float num16 = 0f;
					foreach (TroopTradeDifference troopTradeDifference in this.CurrentData.GetTroopTradeDifferencesFromTo(this._initialData, PartyScreenLogic.PartyRosterSide.Left))
					{
						int differenceCount = troopTradeDifference.DifferenceCount;
						if (differenceCount > 0)
						{
							if (!troopTradeDifference.IsPrisoner)
							{
								num15 += (float)differenceCount * Campaign.Current.Models.PrisonerDonationModel.CalculateInfluenceGainAfterTroopDonation(PartyBase.MainParty, troopTradeDifference.Troop, currentSettlement);
							}
							else if (troopTradeDifference.Troop.IsHero)
							{
								num14 += Campaign.Current.Models.PrisonerDonationModel.CalculateInfluenceGainAfterPrisonerDonation(PartyBase.MainParty, troopTradeDifference.Troop, currentSettlement);
							}
							else
							{
								num16 += (float)differenceCount * Campaign.Current.Models.PrisonerDonationModel.CalculateInfluenceGainAfterPrisonerDonation(PartyBase.MainParty, troopTradeDifference.Troop, currentSettlement);
							}
						}
					}
					this.SetInfluenceChangeAmount((int)num14, (int)num15, (int)num16);
				}
				if (invokeUpdate)
				{
					PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
					if (updateDelegate != null)
					{
						updateDelegate(command);
					}
					PartyScreenLogic.PresentationUpdate update = this.Update;
					if (update == null)
					{
						return;
					}
					update(command);
				}
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000BBFD0 File Offset: 0x000BA1D0
		protected void ShiftTroop(PartyScreenLogic.PartyCommand command)
		{
			bool flag = false;
			if (this.ValidateCommand(command))
			{
				CharacterObject character = command.Character;
				if (command.Type == PartyScreenLogic.TroopType.Member)
				{
					int num = this.MemberRosters[(int)command.RosterSide].FindIndexOfTroop(character);
					int num2 = ((num < command.Index) ? (command.Index - 1) : command.Index);
					this.MemberRosters[(int)command.RosterSide].ShiftTroopToIndex(num, num2);
				}
				else
				{
					int num3 = this.PrisonerRosters[(int)command.RosterSide].FindIndexOfTroop(character);
					this.PrisonerRosters[(int)command.RosterSide].GetElementCopyAtIndex(num3);
					int num4 = ((num3 < command.Index) ? (command.Index - 1) : command.Index);
					this.PrisonerRosters[(int)command.RosterSide].ShiftTroopToIndex(num3, num4);
				}
				flag = true;
			}
			if (flag)
			{
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate != null)
				{
					updateDelegate(command);
				}
				PartyScreenLogic.PresentationUpdate update = this.Update;
				if (update == null)
				{
					return;
				}
				update(command);
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000BC0C3 File Offset: 0x000BA2C3
		protected void TransferPartyLeaderTroop(PartyScreenLogic.PartyCommand command)
		{
			if (this.ValidateCommand(command))
			{
				PartyBase partyBase = ((command.RosterSide == PartyScreenLogic.PartyRosterSide.Left) ? this.LeftOwnerParty : this.RightOwnerParty);
			}
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000BC0E8 File Offset: 0x000BA2E8
		protected void UpgradeTroop(PartyScreenLogic.PartyCommand command)
		{
			if (this.ValidateCommand(command))
			{
				CharacterObject character = command.Character;
				CharacterObject characterObject = character.UpgradeTargets[command.UpgradeTarget];
				TroopRoster roster = this.GetRoster(command.RosterSide, command.Type);
				int num = roster.FindIndexOfTroop(character);
				int num2 = character.GetUpgradeXpCost(PartyBase.MainParty, command.UpgradeTarget) * command.TotalNumber;
				roster.SetElementXp(num, roster.GetElementXp(num) - num2);
				List<ValueTuple<EquipmentElement, int>> list = null;
				this.SetPartyGoldChangeAmount(this.CurrentData.PartyGoldChangeAmount - character.GetUpgradeGoldCost(PartyBase.MainParty, command.UpgradeTarget) * command.TotalNumber);
				if (characterObject.UpgradeRequiresItemFromCategory != null)
				{
					list = this.RemoveItemFromItemRoster(characterObject.UpgradeRequiresItemFromCategory, command.TotalNumber);
				}
				int num3 = 0;
				foreach (TroopRosterElement troopRosterElement in roster.GetTroopRoster())
				{
					if (troopRosterElement.Character == character && command.TotalNumber > troopRosterElement.Number - troopRosterElement.WoundedNumber)
					{
						num3 = command.TotalNumber - (troopRosterElement.Number - troopRosterElement.WoundedNumber);
					}
				}
				roster.AddToCounts(character, -command.TotalNumber, false, -num3, 0, true, -1);
				roster.AddToCounts(characterObject, command.TotalNumber, false, num3, 0, true, command.Index);
				this.AddUpgradeToHistory(character, characterObject, command.TotalNumber);
				this.AddUsedHorsesToHistory(list);
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate == null)
				{
					return;
				}
				updateDelegate(command);
			}
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000BC278 File Offset: 0x000BA478
		protected void RecruitPrisoner(PartyScreenLogic.PartyCommand command)
		{
			bool flag = false;
			if (this.ValidateCommand(command))
			{
				CharacterObject character = command.Character;
				TroopRoster troopRoster = this.PrisonerRosters[(int)command.RosterSide];
				int num = MathF.Min(this.CurrentData.RightRecruitableData[character], command.TotalNumber);
				if (num > 0)
				{
					Dictionary<CharacterObject, int> rightRecruitableData = this.CurrentData.RightRecruitableData;
					CharacterObject characterObject = character;
					rightRecruitableData[characterObject] -= num;
					int conformityNeededToRecruitPrisoner = Campaign.Current.Models.PrisonerRecruitmentCalculationModel.GetConformityNeededToRecruitPrisoner(character);
					troopRoster.AddXpToTroop(character, -conformityNeededToRecruitPrisoner * num);
					troopRoster.AddToCounts(character, -num, false, 0, 0, true, -1);
					this.MemberRosters[(int)command.RosterSide].AddToCounts(command.Character, num, false, 0, 0, true, command.Index);
					this.AddRecruitToHistory(character, num);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate != null)
				{
					updateDelegate(command);
				}
				PartyScreenLogic.PresentationUpdate update = this.Update;
				if (update == null)
				{
					return;
				}
				update(command);
			}
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000BC37C File Offset: 0x000BA57C
		protected void ExecuteTroop(PartyScreenLogic.PartyCommand command)
		{
			bool flag = false;
			if (this.ValidateCommand(command))
			{
				CharacterObject character = command.Character;
				this.PrisonerRosters[(int)command.RosterSide].AddToCounts(character, -1, false, 0, 0, true, -1);
				KillCharacterAction.ApplyByExecution(character.HeroObject, Hero.MainHero, true, false);
				flag = true;
			}
			if (flag)
			{
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate != null)
				{
					updateDelegate(command);
				}
				PartyScreenLogic.PresentationUpdate update = this.Update;
				if (update != null)
				{
					update(command);
				}
				if (command.RosterSide == PartyScreenLogic.PartyRosterSide.Left)
				{
					this._initialData.LeftPrisonerRoster.AddToCounts(command.Character, -1, false, 0, 0, true, -1);
					return;
				}
				if (PartyScreenLogic.PartyRosterSide.Right == command.RosterSide)
				{
					this._initialData.RightPrisonerRoster.AddToCounts(command.Character, -1, false, 0, 0, true, -1);
				}
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000BC43C File Offset: 0x000BA63C
		protected void TransferAllTroops(PartyScreenLogic.PartyCommand command)
		{
			if (this.ValidateCommand(command))
			{
				PartyScreenLogic.PartyRosterSide partyRosterSide = PartyScreenLogic.PartyRosterSide.Right - command.RosterSide;
				TroopRoster roster = this.GetRoster(command.RosterSide, command.Type);
				List<TroopRosterElement> listFromRoster = this.GetListFromRoster(roster);
				int num = -1;
				if (command.RosterSide == PartyScreenLogic.PartyRosterSide.Right)
				{
					if (command.Type == PartyScreenLogic.TroopType.Prisoner)
					{
						num = this.LeftPartyPrisonersSizeLimit - this.PrisonerRosters[0].TotalManCount;
					}
					else
					{
						num = this.LeftPartyMembersSizeLimit - this.MemberRosters[0].TotalManCount;
					}
				}
				else if (command.RosterSide == PartyScreenLogic.PartyRosterSide.Left)
				{
					if (command.Type == PartyScreenLogic.TroopType.Prisoner)
					{
						num = this.RightPartyPrisonersSizeLimit - this.PrisonerRosters[1].TotalManCount;
					}
					else
					{
						num = this.RightPartyMembersSizeLimit - this.MemberRosters[1].TotalManCount;
					}
				}
				if (num <= 0)
				{
					num = listFromRoster.Sum<TroopRosterElement>((TroopRosterElement x) => x.Number);
				}
				IEnumerable<string> enumerable = ((command.Type == PartyScreenLogic.TroopType.Member) ? Campaign.Current.GetCampaignBehavior<IViewDataTracker>().GetPartyTroopLocks() : Campaign.Current.GetCampaignBehavior<IViewDataTracker>().GetPartyPrisonerLocks());
				int num2 = 0;
				while (num2 < listFromRoster.Count && num > 0)
				{
					TroopRosterElement troopRosterElement = listFromRoster[num2];
					if ((command.RosterSide != PartyScreenLogic.PartyRosterSide.Right || !enumerable.Contains(troopRosterElement.Character.StringId)) && this.IsTroopTransferable(command.Type, troopRosterElement.Character, (int)command.RosterSide))
					{
						PartyScreenLogic.PartyCommand partyCommand = new PartyScreenLogic.PartyCommand();
						int num3 = MBMath.ClampInt(troopRosterElement.Number, 0, num);
						partyCommand.FillForTransferTroop(command.RosterSide, command.Type, troopRosterElement.Character, num3, troopRosterElement.WoundedNumber, -1);
						this.TransferTroop(partyCommand, false);
						num -= num3;
					}
					num2++;
				}
				PartyScreenLogic.TroopSortType activeSortTypeForSide = this.GetActiveSortTypeForSide(partyRosterSide);
				if (activeSortTypeForSide != PartyScreenLogic.TroopSortType.Custom)
				{
					TroopRoster roster2 = this.GetRoster(partyRosterSide, PartyScreenLogic.TroopType.Member);
					TroopRoster roster3 = this.GetRoster(partyRosterSide, PartyScreenLogic.TroopType.Prisoner);
					this.SortRoster(roster2, activeSortTypeForSide);
					this.SortRoster(roster3, activeSortTypeForSide);
				}
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate != null)
				{
					updateDelegate(command);
				}
				PartyScreenLogic.PresentationUpdate update = this.Update;
				if (update == null)
				{
					return;
				}
				update(command);
			}
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000BC654 File Offset: 0x000BA854
		protected void SortTroops(PartyScreenLogic.PartyCommand command)
		{
			if (this.ValidateCommand(command))
			{
				this.SetActiveSortTypeForSide(command.RosterSide, command.SortType);
				this.SetIsAscendingForSide(command.RosterSide, command.IsSortAscending);
				this.UpdateComparersAscendingOrder(command.IsSortAscending);
				if (command.SortType != PartyScreenLogic.TroopSortType.Custom)
				{
					TroopRoster roster = this.GetRoster(command.RosterSide, PartyScreenLogic.TroopType.Member);
					TroopRoster roster2 = this.GetRoster(command.RosterSide, PartyScreenLogic.TroopType.Prisoner);
					this.SortRoster(roster, command.SortType);
					this.SortRoster(roster2, command.SortType);
				}
				PartyScreenLogic.PresentationUpdate updateDelegate = this.UpdateDelegate;
				if (updateDelegate != null)
				{
					updateDelegate(command);
				}
				PartyScreenLogic.PresentationUpdate update = this.Update;
				if (update == null)
				{
					return;
				}
				update(command);
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000BC700 File Offset: 0x000BA900
		public int GetIndexToInsertTroop(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, TroopRosterElement troop)
		{
			PartyScreenLogic.TroopSortType activeSortTypeForSide = this.GetActiveSortTypeForSide(side);
			if (activeSortTypeForSide != PartyScreenLogic.TroopSortType.Custom)
			{
				return -1;
			}
			PartyScreenLogic.TroopComparer comparer = this.GetComparer(activeSortTypeForSide);
			TroopRoster roster = this.GetRoster(side, type);
			for (int i = 0; i < roster.Count; i++)
			{
				TroopRosterElement elementCopyAtIndex = roster.GetElementCopyAtIndex(i);
				if (!elementCopyAtIndex.Character.IsHero)
				{
					if (elementCopyAtIndex.Character.StringId == troop.Character.StringId)
					{
						return -1;
					}
					if (comparer.Compare(elementCopyAtIndex, troop) < 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000BC782 File Offset: 0x000BA982
		public PartyScreenLogic.TroopSortType GetActiveSortTypeForSide(PartyScreenLogic.PartyRosterSide side)
		{
			if (side == PartyScreenLogic.PartyRosterSide.Left)
			{
				return this.ActiveOtherPartySortType;
			}
			if (side == PartyScreenLogic.PartyRosterSide.Right)
			{
				return this.ActiveMainPartySortType;
			}
			return PartyScreenLogic.TroopSortType.Invalid;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000BC79A File Offset: 0x000BA99A
		private void SetActiveSortTypeForSide(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopSortType sortType)
		{
			if (side == PartyScreenLogic.PartyRosterSide.Left)
			{
				this.ActiveOtherPartySortType = sortType;
				return;
			}
			if (side == PartyScreenLogic.PartyRosterSide.Right)
			{
				this.ActiveMainPartySortType = sortType;
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000BC7B2 File Offset: 0x000BA9B2
		public bool GetIsAscendingSortForSide(PartyScreenLogic.PartyRosterSide side)
		{
			if (side == PartyScreenLogic.PartyRosterSide.Left)
			{
				return this.IsOtherPartySortAscending;
			}
			return side == PartyScreenLogic.PartyRosterSide.Right && this.IsMainPartySortAscending;
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000BC7CA File Offset: 0x000BA9CA
		private void SetIsAscendingForSide(PartyScreenLogic.PartyRosterSide side, bool isAscending)
		{
			if (side == PartyScreenLogic.PartyRosterSide.Left)
			{
				this.IsOtherPartySortAscending = isAscending;
				return;
			}
			if (side == PartyScreenLogic.PartyRosterSide.Right)
			{
				this.IsMainPartySortAscending = isAscending;
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000BC7E4 File Offset: 0x000BA9E4
		private List<TroopRosterElement> GetListFromRoster(TroopRoster roster)
		{
			List<TroopRosterElement> list = new List<TroopRosterElement>();
			for (int i = 0; i < roster.Count; i++)
			{
				list.Add(roster.GetElementCopyAtIndex(i));
			}
			return list;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000BC818 File Offset: 0x000BAA18
		private void SyncRosterWithList(TroopRoster roster, List<TroopRosterElement> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				TroopRosterElement troopRosterElement = list[i];
				int num = roster.FindIndexOfTroop(troopRosterElement.Character);
				roster.SwapTroopsAtIndices(num, i);
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000BC854 File Offset: 0x000BAA54
		[Conditional("DEBUG")]
		private void EnsureRosterIsSyncedWithList(TroopRoster roster, List<TroopRosterElement> list)
		{
			if (roster.Count != list.Count)
			{
				Debug.FailedAssert("Roster count is not synced with the list count", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\PartyScreenLogic.cs", "EnsureRosterIsSyncedWithList", 1081);
				return;
			}
			for (int i = 0; i < roster.Count; i++)
			{
				if (roster.GetCharacterAtIndex(i).StringId != list[i].Character.StringId)
				{
					Debug.FailedAssert("Roster is not synced with the list at index: " + i, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\PartyScreenLogic.cs", "EnsureRosterIsSyncedWithList", 1091);
					return;
				}
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000BC8E4 File Offset: 0x000BAAE4
		private void SortRoster(TroopRoster originalRoster, PartyScreenLogic.TroopSortType sortType)
		{
			PartyScreenLogic.TroopComparer troopComparer = this._defaultComparers[sortType];
			if (!this.IsRosterOrdered(originalRoster, troopComparer))
			{
				List<TroopRosterElement> listFromRoster = this.GetListFromRoster(originalRoster);
				listFromRoster.Sort(this._defaultComparers[sortType]);
				this.SyncRosterWithList(originalRoster, listFromRoster);
			}
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000BC92C File Offset: 0x000BAB2C
		private bool IsRosterOrdered(TroopRoster roster, PartyScreenLogic.TroopComparer comparer)
		{
			for (int i = 1; i < roster.Count; i++)
			{
				TroopRosterElement elementCopyAtIndex = roster.GetElementCopyAtIndex(i - 1);
				TroopRosterElement elementCopyAtIndex2 = roster.GetElementCopyAtIndex(i);
				if (comparer.Compare(elementCopyAtIndex, elementCopyAtIndex2) >= 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000BC96C File Offset: 0x000BAB6C
		public bool IsDoneActive()
		{
			object obj = Hero.MainHero.Gold < -this.CurrentData.PartyGoldChangeAmount && this.CurrentData.PartyGoldChangeAmount < 0;
			PartyPresentationDoneButtonConditionDelegate partyPresentationDoneButtonConditionDelegate = this.PartyPresentationDoneButtonConditionDelegate;
			Tuple<bool, TextObject> tuple = ((partyPresentationDoneButtonConditionDelegate != null) ? partyPresentationDoneButtonConditionDelegate(this.MemberRosters[0], this.PrisonerRosters[0], this.MemberRosters[1], this.PrisonerRosters[1], this.LeftPartyMembersSizeLimit, 0) : null);
			bool flag = this.PartyPresentationDoneButtonConditionDelegate == null || (tuple != null && tuple.Item1);
			this.DoneReasonString = null;
			object obj2 = obj;
			if (obj2 != null)
			{
				this.DoneReasonString = GameTexts.FindText("str_inventory_popup_player_not_enough_gold", null).ToString();
			}
			else
			{
				string text;
				if (tuple == null)
				{
					text = null;
				}
				else
				{
					TextObject item = tuple.Item2;
					text = ((item != null) ? item.ToString() : null);
				}
				this.DoneReasonString = text ?? string.Empty;
			}
			return obj2 == 0 && flag;
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000BCA42 File Offset: 0x000BAC42
		public bool IsCancelActive()
		{
			return this.PartyPresentationCancelButtonActivateDelegate == null || this.PartyPresentationCancelButtonActivateDelegate();
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000BCA5C File Offset: 0x000BAC5C
		public bool DoneLogic(bool isForced)
		{
			if (Hero.MainHero.Gold < -this.CurrentData.PartyGoldChangeAmount && this.CurrentData.PartyGoldChangeAmount < 0)
			{
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_inventory_popup_player_not_enough_gold", null), 0, null, null, "");
				return false;
			}
			FlattenedTroopRoster flattenedTroopRoster = new FlattenedTroopRoster(4);
			FlattenedTroopRoster flattenedTroopRoster2 = new FlattenedTroopRoster(4);
			foreach (Tuple<CharacterObject, int> tuple in this.CurrentData.TransferredPrisonersHistory)
			{
				int num = MathF.Abs(tuple.Item2);
				if (tuple.Item2 < 0)
				{
					flattenedTroopRoster.Add(tuple.Item1, num, 0);
				}
				else if (tuple.Item2 > 0)
				{
					flattenedTroopRoster2.Add(tuple.Item1, num, 0);
				}
			}
			if (Settlement.CurrentSettlement != null && !flattenedTroopRoster2.IsEmpty<FlattenedTroopRosterElement>())
			{
				CampaignEventDispatcher.Instance.OnPrisonersChangeInSettlement(Settlement.CurrentSettlement, flattenedTroopRoster2, null, true);
			}
			bool flag = this.PartyPresentationDoneButtonDelegate(this.MemberRosters[0], this.PrisonerRosters[0], this.MemberRosters[1], this.PrisonerRosters[1], flattenedTroopRoster2, flattenedTroopRoster, isForced, this.LeftOwnerParty, this.RightOwnerParty);
			if (flag)
			{
				if (!this.DoNotApplyGoldTransactions)
				{
					GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.CurrentData.PartyGoldChangeAmount, false);
				}
				if (this.CurrentData.PartyInfluenceChangeAmount.Item2 != 0)
				{
					GainKingdomInfluenceAction.ApplyForLeavingTroopToGarrison(Hero.MainHero, (float)this.CurrentData.PartyInfluenceChangeAmount.Item2);
				}
				this.FireCampaignRelatedEvents();
				this.SetPartyGoldChangeAmount(0);
				this.SetHorseChangeAmount(0);
				this.SetInfluenceChangeAmount(0, 0, 0);
				this.SetMoraleChangeAmount(0);
				this.CurrentData.UpgradedTroopsHistory = new List<Tuple<CharacterObject, CharacterObject, int>>();
				this.CurrentData.TransferredPrisonersHistory = new List<Tuple<CharacterObject, int>>();
				this.CurrentData.RecruitedPrisonersHistory = new List<Tuple<CharacterObject, int>>();
				this.CurrentData.UsedUpgradeHorsesHistory = new List<Tuple<EquipmentElement, int>>();
				this._initialData.CopyFromScreenData(this.CurrentData);
			}
			return flag;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000BCC5C File Offset: 0x000BAE5C
		public void OnPartyScreenClosed(bool fromCancel)
		{
			if (fromCancel)
			{
				PartyPresentationCancelButtonDelegate partyPresentationCancelButtonDelegate = this.PartyPresentationCancelButtonDelegate;
				if (partyPresentationCancelButtonDelegate != null)
				{
					partyPresentationCancelButtonDelegate();
				}
			}
			PartyScreenClosedDelegate partyScreenClosedEvent = this.PartyScreenClosedEvent;
			if (partyScreenClosedEvent == null)
			{
				return;
			}
			partyScreenClosedEvent(this.LeftOwnerParty, this.MemberRosters[0], this.PrisonerRosters[0], this.RightOwnerParty, this.MemberRosters[1], this.PrisonerRosters[1], fromCancel);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000BCCBC File Offset: 0x000BAEBC
		private void UpdateComparersAscendingOrder(bool isAscending)
		{
			foreach (KeyValuePair<PartyScreenLogic.TroopSortType, PartyScreenLogic.TroopComparer> keyValuePair in this._defaultComparers)
			{
				keyValuePair.Value.SetIsAscending(isAscending);
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000BCD18 File Offset: 0x000BAF18
		private void FireCampaignRelatedEvents()
		{
			foreach (Tuple<CharacterObject, CharacterObject, int> tuple in this.CurrentData.UpgradedTroopsHistory)
			{
				CampaignEventDispatcher.Instance.OnPlayerUpgradedTroops(tuple.Item1, tuple.Item2, tuple.Item3);
			}
			FlattenedTroopRoster flattenedTroopRoster = new FlattenedTroopRoster(4);
			foreach (Tuple<CharacterObject, int> tuple2 in this.CurrentData.RecruitedPrisonersHistory)
			{
				flattenedTroopRoster.Add(tuple2.Item1, tuple2.Item2, 0);
			}
			if (!flattenedTroopRoster.IsEmpty<FlattenedTroopRosterElement>())
			{
				CampaignEventDispatcher.Instance.OnMainPartyPrisonerRecruited(flattenedTroopRoster);
			}
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000BCDF8 File Offset: 0x000BAFF8
		public bool IsTroopTransferable(PartyScreenLogic.TroopType troopType, CharacterObject character, int side)
		{
			return this.IsTroopRosterTransferable(troopType) && !character.IsNotTransferableInPartyScreen && character != CharacterObject.PlayerCharacter && (this.IsTroopTransferableDelegate == null || this.IsTroopTransferableDelegate(character, troopType, (PartyScreenLogic.PartyRosterSide)side, this.LeftOwnerParty));
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000BCE34 File Offset: 0x000BB034
		public bool IsTroopRosterTransferable(PartyScreenLogic.TroopType troopType)
		{
			if (troopType == PartyScreenLogic.TroopType.Prisoner)
			{
				return this.PrisonerTransferState == PartyScreenLogic.TransferState.Transferable || this.PrisonerTransferState == PartyScreenLogic.TransferState.TransferableWithTrade;
			}
			return troopType == PartyScreenLogic.TroopType.Member && (this.MemberTransferState == PartyScreenLogic.TransferState.Transferable || this.MemberTransferState == PartyScreenLogic.TransferState.TransferableWithTrade);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000BCE69 File Offset: 0x000BB069
		public bool IsPrisonerRecruitable(PartyScreenLogic.TroopType troopType, CharacterObject character, PartyScreenLogic.PartyRosterSide side)
		{
			return side == PartyScreenLogic.PartyRosterSide.Right && troopType == PartyScreenLogic.TroopType.Prisoner && !character.IsHero && this.CurrentData.RightRecruitableData.ContainsKey(character) && this.CurrentData.RightRecruitableData[character] > 0;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000BCEA8 File Offset: 0x000BB0A8
		public string GetRecruitableReasonString(CharacterObject character, bool isRecruitable, int troopCount, out bool showStackModifierText)
		{
			showStackModifierText = false;
			if (isRecruitable)
			{
				showStackModifierText = true;
				if (this.RightOwnerParty.PartySizeLimit <= this.MemberRosters[1].TotalManCount)
				{
					return GameTexts.FindText("str_recruit_party_size_limit", null).ToString();
				}
				return GameTexts.FindText("str_recruit_prisoner", null).ToString();
			}
			else
			{
				if (character.IsHero)
				{
					return GameTexts.FindText("str_cannot_recruit_hero", null).ToString();
				}
				return GameTexts.FindText("str_cannot_recruit_prisoner", null).ToString();
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000BCF28 File Offset: 0x000BB128
		public bool IsExecutable(PartyScreenLogic.TroopType troopType, CharacterObject character, PartyScreenLogic.PartyRosterSide side)
		{
			return troopType == PartyScreenLogic.TroopType.Prisoner && side == PartyScreenLogic.PartyRosterSide.Right && character.IsHero && character.HeroObject.Age >= (float)Campaign.Current.Models.AgeModel.HeroComesOfAge && PlayerEncounter.Current == null && FaceGen.GetMaturityTypeWithAge(character.Age) > BodyMeshMaturityType.Tween;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000BCF7E File Offset: 0x000BB17E
		public string GetExecutableReasonString(CharacterObject character, bool isExecutable)
		{
			if (isExecutable)
			{
				return GameTexts.FindText("str_execute_prisoner", null).ToString();
			}
			if (!character.IsHero)
			{
				return GameTexts.FindText("str_cannot_execute_nonhero", null).ToString();
			}
			return GameTexts.FindText("str_cannot_execute_hero", null).ToString();
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000BCFC0 File Offset: 0x000BB1C0
		public int GetCurrentQuestCurrentCount(bool includePrisoners, bool includeMembers)
		{
			int num = 0;
			if (includeMembers)
			{
				num += this.MemberRosters[0].Sum((TroopRosterElement item) => item.Number - item.WoundedNumber);
			}
			if (includePrisoners)
			{
				num += this.PrisonerRosters[0].Sum((TroopRosterElement item) => item.Number - item.WoundedNumber);
			}
			return num;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000BD034 File Offset: 0x000BB234
		public int GetCurrentQuestRequiredCount()
		{
			return this.LeftPartyMembersSizeLimit;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000BD03C File Offset: 0x000BB23C
		private static bool DefaultDoneHandler(TroopRoster leftMemberRoster, TroopRoster leftPrisonRoster, TroopRoster rightMemberRoster, TroopRoster rightPrisonRoster, FlattenedTroopRoster takenPrisonerRoster, FlattenedTroopRoster releasedPrisonerRoster, bool isForced, PartyBase leftParty = null, PartyBase rightParty = null)
		{
			return true;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000BD040 File Offset: 0x000BB240
		private void AddUpgradeToHistory(CharacterObject fromTroop, CharacterObject toTroop, int num)
		{
			Tuple<CharacterObject, CharacterObject, int> tuple = this.CurrentData.UpgradedTroopsHistory.Find((Tuple<CharacterObject, CharacterObject, int> t) => t.Item1 == fromTroop && t.Item2 == toTroop);
			if (tuple != null)
			{
				int item = tuple.Item3;
				this.CurrentData.UpgradedTroopsHistory.Remove(tuple);
				this.CurrentData.UpgradedTroopsHistory.Add(new Tuple<CharacterObject, CharacterObject, int>(fromTroop, toTroop, num + item));
				return;
			}
			this.CurrentData.UpgradedTroopsHistory.Add(new Tuple<CharacterObject, CharacterObject, int>(fromTroop, toTroop, num));
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000BD0E4 File Offset: 0x000BB2E4
		private void AddUsedHorsesToHistory(List<ValueTuple<EquipmentElement, int>> usedHorses)
		{
			if (usedHorses != null)
			{
				using (List<ValueTuple<EquipmentElement, int>>.Enumerator enumerator = usedHorses.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ValueTuple<EquipmentElement, int> usedHorse = enumerator.Current;
						Tuple<EquipmentElement, int> tuple = this.CurrentData.UsedUpgradeHorsesHistory.Find((Tuple<EquipmentElement, int> t) => t.Equals(usedHorse.Item1));
						if (tuple != null)
						{
							int item = tuple.Item2;
							this.CurrentData.UsedUpgradeHorsesHistory.Remove(tuple);
							this.CurrentData.UsedUpgradeHorsesHistory.Add(new Tuple<EquipmentElement, int>(usedHorse.Item1, item + usedHorse.Item2));
						}
						else
						{
							this.CurrentData.UsedUpgradeHorsesHistory.Add(new Tuple<EquipmentElement, int>(usedHorse.Item1, usedHorse.Item2));
						}
					}
				}
				PartyScreenData currentData = this.CurrentData;
				this.SetHorseChangeAmount(currentData.PartyHorseChangeAmount += usedHorses.Sum<ValueTuple<EquipmentElement, int>>((ValueTuple<EquipmentElement, int> t) => t.Item2));
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000BD218 File Offset: 0x000BB418
		private void UpdatePrisonerTransferHistory(CharacterObject troop, int amount)
		{
			Tuple<CharacterObject, int> tuple = this.CurrentData.TransferredPrisonersHistory.Find((Tuple<CharacterObject, int> t) => t.Item1 == troop);
			if (tuple != null)
			{
				int item = tuple.Item2;
				this.CurrentData.TransferredPrisonersHistory.Remove(tuple);
				this.CurrentData.TransferredPrisonersHistory.Add(new Tuple<CharacterObject, int>(troop, amount + item));
				return;
			}
			this.CurrentData.TransferredPrisonersHistory.Add(new Tuple<CharacterObject, int>(troop, amount));
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000BD2A8 File Offset: 0x000BB4A8
		private void AddRecruitToHistory(CharacterObject troop, int amount)
		{
			Tuple<CharacterObject, int> tuple = this.CurrentData.RecruitedPrisonersHistory.Find((Tuple<CharacterObject, int> t) => t.Item1 == troop);
			if (tuple != null)
			{
				int item = tuple.Item2;
				this.CurrentData.RecruitedPrisonersHistory.Remove(tuple);
				this.CurrentData.RecruitedPrisonersHistory.Add(new Tuple<CharacterObject, int>(troop, amount + item));
			}
			else
			{
				this.CurrentData.RecruitedPrisonersHistory.Add(new Tuple<CharacterObject, int>(troop, amount));
			}
			int prisonerRecruitmentMoraleEffect = Campaign.Current.Models.PrisonerRecruitmentCalculationModel.GetPrisonerRecruitmentMoraleEffect(this.RightOwnerParty, troop, amount);
			this.SetMoraleChangeAmount(this.CurrentData.PartyMoraleChangeAmount + prisonerRecruitmentMoraleEffect);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000BD36C File Offset: 0x000BB56C
		private string GetItemLockStringID(EquipmentElement equipmentElement)
		{
			return equipmentElement.Item.StringId + ((equipmentElement.ItemModifier != null) ? equipmentElement.ItemModifier.StringId : "");
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000BD39C File Offset: 0x000BB59C
		private List<ValueTuple<EquipmentElement, int>> RemoveItemFromItemRoster(ItemCategory itemCategory, int numOfItemsLeftToRemove = 1)
		{
			List<ValueTuple<EquipmentElement, int>> list = new List<ValueTuple<EquipmentElement, int>>();
			IEnumerable<string> lockedItems = Campaign.Current.GetCampaignBehavior<IViewDataTracker>().GetInventoryLocks();
			foreach (ItemRosterElement itemRosterElement in from x in this.RightOwnerParty.ItemRoster.Where<ItemRosterElement>(delegate(ItemRosterElement x)
				{
					ItemObject item = x.EquipmentElement.Item;
					return ((item != null) ? item.ItemCategory : null) == itemCategory;
				})
				orderby x.EquipmentElement.Item.Value
				orderby lockedItems.Contains(this.GetItemLockStringID(x.EquipmentElement))
				select x)
			{
				int num = MathF.Min(numOfItemsLeftToRemove, itemRosterElement.Amount);
				this.RightOwnerParty.ItemRoster.AddToCounts(itemRosterElement.EquipmentElement, -num);
				numOfItemsLeftToRemove -= num;
				list.Add(new ValueTuple<EquipmentElement, int>(itemRosterElement.EquipmentElement, num));
				if (numOfItemsLeftToRemove <= 0)
				{
					break;
				}
			}
			if (numOfItemsLeftToRemove > 0)
			{
				Debug.FailedAssert("Couldn't find enough upgrade req items in the inventory.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Party\\PartyScreenLogic.cs", "RemoveItemFromItemRoster", 1509);
			}
			return list;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000BD4C4 File Offset: 0x000BB6C4
		public void Reset(bool fromCancel)
		{
			this.ResetLogic(fromCancel);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000BD4CD File Offset: 0x000BB6CD
		private void ResetLogic(bool fromCancel)
		{
			if (this.CurrentData != this._initialData)
			{
				this.CurrentData.ResetUsing(this._initialData);
				PartyScreenLogic.AfterResetDelegate afterReset = this.AfterReset;
				if (afterReset == null)
				{
					return;
				}
				afterReset(this, fromCancel);
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000BD505 File Offset: 0x000BB705
		public void SavePartyScreenData()
		{
			this._savedData = new PartyScreenData();
			this._savedData.InitializeCopyFrom(this.CurrentData.RightParty, this.CurrentData.LeftParty);
			this._savedData.CopyFromScreenData(this.CurrentData);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000BD544 File Offset: 0x000BB744
		public void ResetToLastSavedPartyScreenData(bool fromCancel)
		{
			if (this.CurrentData != this._savedData)
			{
				this.CurrentData.ResetUsing(this._savedData);
				PartyScreenLogic.AfterResetDelegate afterReset = this.AfterReset;
				if (afterReset == null)
				{
					return;
				}
				afterReset(this, fromCancel);
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000BD57C File Offset: 0x000BB77C
		public void RemoveZeroCounts()
		{
			for (int i = 0; i < this.MemberRosters.Length; i++)
			{
				this.MemberRosters[i].RemoveZeroCounts();
			}
			for (int j = 0; j < this.PrisonerRosters.Length; j++)
			{
				this.PrisonerRosters[j].RemoveZeroCounts();
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000BD5C9 File Offset: 0x000BB7C9
		public int GetTroopRecruitableAmount(CharacterObject troop)
		{
			if (!this.CurrentData.RightRecruitableData.ContainsKey(troop))
			{
				return 0;
			}
			return this.CurrentData.RightRecruitableData[troop];
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000BD5F1 File Offset: 0x000BB7F1
		public TroopRoster GetRoster(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType troopType)
		{
			if (troopType == PartyScreenLogic.TroopType.Member)
			{
				return this.MemberRosters[(int)side];
			}
			if (troopType == PartyScreenLogic.TroopType.Prisoner)
			{
				return this.PrisonerRosters[(int)side];
			}
			return null;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000BD60E File Offset: 0x000BB80E
		internal void OnDoneEvent(List<TroopTradeDifference> freshlySellList)
		{
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000BD610 File Offset: 0x000BB810
		public bool IsThereAnyChanges()
		{
			return this._initialData.IsThereAnyTroopTradeDifferenceBetween(this.CurrentData);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000BD624 File Offset: 0x000BB824
		public bool HaveRightSideGainedTroops()
		{
			foreach (TroopTradeDifference troopTradeDifference in this._initialData.GetTroopTradeDifferencesFromTo(this.CurrentData, PartyScreenLogic.PartyRosterSide.None))
			{
				if (!troopTradeDifference.IsPrisoner && troopTradeDifference.FromCount < troopTradeDifference.ToCount)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000BD6A0 File Offset: 0x000BB8A0
		public PartyScreenLogic.TroopComparer GetComparer(PartyScreenLogic.TroopSortType sortType)
		{
			return this._defaultComparers[sortType];
		}

		// Token: 0x04000CDD RID: 3293
		public PartyPresentationDoneButtonDelegate PartyPresentationDoneButtonDelegate;

		// Token: 0x04000CDE RID: 3294
		public PartyPresentationDoneButtonConditionDelegate PartyPresentationDoneButtonConditionDelegate;

		// Token: 0x04000CDF RID: 3295
		public PartyPresentationCancelButtonActivateDelegate PartyPresentationCancelButtonActivateDelegate;

		// Token: 0x04000CE0 RID: 3296
		public PartyPresentationCancelButtonDelegate PartyPresentationCancelButtonDelegate;

		// Token: 0x04000CE1 RID: 3297
		public PartyScreenLogic.PresentationUpdate UpdateDelegate;

		// Token: 0x04000CE2 RID: 3298
		public IsTroopTransferableDelegate IsTroopTransferableDelegate;

		// Token: 0x04000CE3 RID: 3299
		public CanTalkToHeroDelegate CanTalkToHeroDelegate;

		// Token: 0x04000CEB RID: 3307
		private PartyScreenLogic.TroopSortType _activeOtherPartySortType;

		// Token: 0x04000CEC RID: 3308
		private PartyScreenLogic.TroopSortType _activeMainPartySortType;

		// Token: 0x04000CED RID: 3309
		private bool _isOtherPartySortAscending;

		// Token: 0x04000CEE RID: 3310
		private bool _isMainPartySortAscending;

		// Token: 0x04000D04 RID: 3332
		public TroopRoster[] MemberRosters;

		// Token: 0x04000D05 RID: 3333
		public TroopRoster[] PrisonerRosters;

		// Token: 0x04000D06 RID: 3334
		public bool IsConsumablesChanges;

		// Token: 0x04000D07 RID: 3335
		private PartyScreenHelper.PartyScreenMode _partyScreenMode;

		// Token: 0x04000D08 RID: 3336
		private readonly Dictionary<PartyScreenLogic.TroopSortType, PartyScreenLogic.TroopComparer> _defaultComparers;

		// Token: 0x04000D09 RID: 3337
		private readonly PartyScreenData _initialData;

		// Token: 0x04000D0A RID: 3338
		private PartyScreenData _savedData;

		// Token: 0x04000D0B RID: 3339
		private Game _game;

		// Token: 0x020006A5 RID: 1701
		public enum TroopSortType
		{
			// Token: 0x04001AED RID: 6893
			Invalid = -1,
			// Token: 0x04001AEE RID: 6894
			Custom,
			// Token: 0x04001AEF RID: 6895
			Type,
			// Token: 0x04001AF0 RID: 6896
			Name,
			// Token: 0x04001AF1 RID: 6897
			Count,
			// Token: 0x04001AF2 RID: 6898
			Tier
		}

		// Token: 0x020006A6 RID: 1702
		public enum PartyRosterSide : byte
		{
			// Token: 0x04001AF4 RID: 6900
			None = 99,
			// Token: 0x04001AF5 RID: 6901
			Right = 1,
			// Token: 0x04001AF6 RID: 6902
			Left = 0
		}

		// Token: 0x020006A7 RID: 1703
		[Flags]
		public enum TroopType
		{
			// Token: 0x04001AF8 RID: 6904
			Member = 1,
			// Token: 0x04001AF9 RID: 6905
			Prisoner = 2,
			// Token: 0x04001AFA RID: 6906
			None = 3
		}

		// Token: 0x020006A8 RID: 1704
		public enum PartyCommandCode
		{
			// Token: 0x04001AFC RID: 6908
			TransferTroop,
			// Token: 0x04001AFD RID: 6909
			UpgradeTroop,
			// Token: 0x04001AFE RID: 6910
			TransferPartyLeaderTroop,
			// Token: 0x04001AFF RID: 6911
			TransferTroopToLeaderSlot,
			// Token: 0x04001B00 RID: 6912
			ShiftTroop,
			// Token: 0x04001B01 RID: 6913
			RecruitTroop,
			// Token: 0x04001B02 RID: 6914
			ExecuteTroop,
			// Token: 0x04001B03 RID: 6915
			TransferAllTroops,
			// Token: 0x04001B04 RID: 6916
			SortTroops
		}

		// Token: 0x020006A9 RID: 1705
		public enum TransferState
		{
			// Token: 0x04001B06 RID: 6918
			NotTransferable,
			// Token: 0x04001B07 RID: 6919
			Transferable,
			// Token: 0x04001B08 RID: 6920
			TransferableWithTrade
		}

		// Token: 0x020006AA RID: 1706
		// (Invoke) Token: 0x0600533D RID: 21309
		public delegate void PresentationUpdate(PartyScreenLogic.PartyCommand command);

		// Token: 0x020006AB RID: 1707
		// (Invoke) Token: 0x06005341 RID: 21313
		public delegate void PartyGoldDelegate();

		// Token: 0x020006AC RID: 1708
		// (Invoke) Token: 0x06005345 RID: 21317
		public delegate void PartyMoraleDelegate();

		// Token: 0x020006AD RID: 1709
		// (Invoke) Token: 0x06005349 RID: 21321
		public delegate void PartyInfluenceDelegate();

		// Token: 0x020006AE RID: 1710
		// (Invoke) Token: 0x0600534D RID: 21325
		public delegate void PartyHorseDelegate();

		// Token: 0x020006AF RID: 1711
		// (Invoke) Token: 0x06005351 RID: 21329
		public delegate void AfterResetDelegate(PartyScreenLogic partyScreenLogic, bool fromCancel);

		// Token: 0x020006B0 RID: 1712
		public class PartyCommand : ISerializableObject
		{
			// Token: 0x17000F7E RID: 3966
			// (get) Token: 0x06005354 RID: 21332 RVA: 0x0018CD9B File Offset: 0x0018AF9B
			// (set) Token: 0x06005355 RID: 21333 RVA: 0x0018CDA3 File Offset: 0x0018AFA3
			public PartyScreenLogic.PartyCommandCode Code { get; private set; }

			// Token: 0x17000F7F RID: 3967
			// (get) Token: 0x06005356 RID: 21334 RVA: 0x0018CDAC File Offset: 0x0018AFAC
			// (set) Token: 0x06005357 RID: 21335 RVA: 0x0018CDB4 File Offset: 0x0018AFB4
			public PartyScreenLogic.PartyRosterSide RosterSide { get; private set; }

			// Token: 0x17000F80 RID: 3968
			// (get) Token: 0x06005358 RID: 21336 RVA: 0x0018CDBD File Offset: 0x0018AFBD
			// (set) Token: 0x06005359 RID: 21337 RVA: 0x0018CDC5 File Offset: 0x0018AFC5
			public CharacterObject Character { get; private set; }

			// Token: 0x17000F81 RID: 3969
			// (get) Token: 0x0600535A RID: 21338 RVA: 0x0018CDCE File Offset: 0x0018AFCE
			// (set) Token: 0x0600535B RID: 21339 RVA: 0x0018CDD6 File Offset: 0x0018AFD6
			public int TotalNumber { get; private set; }

			// Token: 0x17000F82 RID: 3970
			// (get) Token: 0x0600535C RID: 21340 RVA: 0x0018CDDF File Offset: 0x0018AFDF
			// (set) Token: 0x0600535D RID: 21341 RVA: 0x0018CDE7 File Offset: 0x0018AFE7
			public int WoundedNumber { get; private set; }

			// Token: 0x17000F83 RID: 3971
			// (get) Token: 0x0600535E RID: 21342 RVA: 0x0018CDF0 File Offset: 0x0018AFF0
			// (set) Token: 0x0600535F RID: 21343 RVA: 0x0018CDF8 File Offset: 0x0018AFF8
			public int Index { get; private set; }

			// Token: 0x17000F84 RID: 3972
			// (get) Token: 0x06005360 RID: 21344 RVA: 0x0018CE01 File Offset: 0x0018B001
			// (set) Token: 0x06005361 RID: 21345 RVA: 0x0018CE09 File Offset: 0x0018B009
			public int UpgradeTarget { get; private set; }

			// Token: 0x17000F85 RID: 3973
			// (get) Token: 0x06005362 RID: 21346 RVA: 0x0018CE12 File Offset: 0x0018B012
			// (set) Token: 0x06005363 RID: 21347 RVA: 0x0018CE1A File Offset: 0x0018B01A
			public PartyScreenLogic.TroopType Type { get; private set; }

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x06005364 RID: 21348 RVA: 0x0018CE23 File Offset: 0x0018B023
			// (set) Token: 0x06005365 RID: 21349 RVA: 0x0018CE2B File Offset: 0x0018B02B
			public PartyScreenLogic.TroopSortType SortType { get; private set; }

			// Token: 0x17000F87 RID: 3975
			// (get) Token: 0x06005366 RID: 21350 RVA: 0x0018CE34 File Offset: 0x0018B034
			// (set) Token: 0x06005367 RID: 21351 RVA: 0x0018CE3C File Offset: 0x0018B03C
			public bool IsSortAscending { get; private set; }

			// Token: 0x06005369 RID: 21353 RVA: 0x0018CE4D File Offset: 0x0018B04D
			public void FillForTransferTroop(PartyScreenLogic.PartyRosterSide fromSide, PartyScreenLogic.TroopType type, CharacterObject character, int totalNumber, int woundedNumber, int targetIndex)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.TransferTroop;
				this.RosterSide = fromSide;
				this.TotalNumber = totalNumber;
				this.WoundedNumber = woundedNumber;
				this.Character = character;
				this.Type = type;
				this.Index = targetIndex;
			}

			// Token: 0x0600536A RID: 21354 RVA: 0x0018CE83 File Offset: 0x0018B083
			public void FillForShiftTroop(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, CharacterObject character, int targetIndex)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.ShiftTroop;
				this.RosterSide = side;
				this.Character = character;
				this.Type = type;
				this.Index = targetIndex;
			}

			// Token: 0x0600536B RID: 21355 RVA: 0x0018CEA9 File Offset: 0x0018B0A9
			public void FillForTransferTroopToLeaderSlot(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, CharacterObject character, int totalNumber, int woundedNumber, int targetIndex)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.TransferTroopToLeaderSlot;
				this.RosterSide = side;
				this.TotalNumber = totalNumber;
				this.WoundedNumber = woundedNumber;
				this.Character = character;
				this.Type = type;
				this.Index = targetIndex;
			}

			// Token: 0x0600536C RID: 21356 RVA: 0x0018CEDF File Offset: 0x0018B0DF
			public void FillForTransferPartyLeaderTroop(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, CharacterObject character, int totalNumber)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.TransferPartyLeaderTroop;
				this.RosterSide = side;
				this.TotalNumber = totalNumber;
				this.Character = character;
				this.Type = type;
			}

			// Token: 0x0600536D RID: 21357 RVA: 0x0018CF05 File Offset: 0x0018B105
			public void FillForUpgradeTroop(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, CharacterObject character, int number, int upgradeTargetType, int index)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.UpgradeTroop;
				this.RosterSide = side;
				this.TotalNumber = number;
				this.Character = character;
				this.UpgradeTarget = upgradeTargetType;
				this.Type = type;
				this.Index = index;
			}

			// Token: 0x0600536E RID: 21358 RVA: 0x0018CF3B File Offset: 0x0018B13B
			public void FillForRecruitTroop(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, CharacterObject character, int number, int index)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.RecruitTroop;
				this.RosterSide = side;
				this.Character = character;
				this.Type = type;
				this.TotalNumber = number;
				this.Index = index;
			}

			// Token: 0x0600536F RID: 21359 RVA: 0x0018CF69 File Offset: 0x0018B169
			public void FillForExecuteTroop(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type, CharacterObject character)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.ExecuteTroop;
				this.RosterSide = side;
				this.Character = character;
				this.Type = type;
			}

			// Token: 0x06005370 RID: 21360 RVA: 0x0018CF87 File Offset: 0x0018B187
			public void FillForTransferAllTroops(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopType type)
			{
				this.Code = PartyScreenLogic.PartyCommandCode.TransferAllTroops;
				this.RosterSide = side;
				this.Type = type;
			}

			// Token: 0x06005371 RID: 21361 RVA: 0x0018CF9E File Offset: 0x0018B19E
			public void FillForSortTroops(PartyScreenLogic.PartyRosterSide side, PartyScreenLogic.TroopSortType sortType, bool isAscending)
			{
				this.RosterSide = side;
				this.Code = PartyScreenLogic.PartyCommandCode.SortTroops;
				this.SortType = sortType;
				this.IsSortAscending = isAscending;
			}

			// Token: 0x06005372 RID: 21362 RVA: 0x0018CFBC File Offset: 0x0018B1BC
			void ISerializableObject.SerializeTo(IWriter writer)
			{
				writer.WriteByte((byte)this.Code);
				writer.WriteByte((byte)this.RosterSide);
				writer.WriteUInt(this.Character.Id.InternalValue);
				writer.WriteInt(this.TotalNumber);
				writer.WriteInt(this.WoundedNumber);
				writer.WriteInt(this.UpgradeTarget);
				writer.WriteByte((byte)this.Type);
			}

			// Token: 0x06005373 RID: 21363 RVA: 0x0018D02C File Offset: 0x0018B22C
			void ISerializableObject.DeserializeFrom(IReader reader)
			{
				this.Code = (PartyScreenLogic.PartyCommandCode)reader.ReadByte();
				this.RosterSide = (PartyScreenLogic.PartyRosterSide)reader.ReadByte();
				MBGUID mbguid = new MBGUID(reader.ReadUInt());
				this.Character = (CharacterObject)MBObjectManager.Instance.GetObject(mbguid);
				this.TotalNumber = reader.ReadInt();
				this.WoundedNumber = reader.ReadInt();
				this.UpgradeTarget = reader.ReadInt();
				this.Type = (PartyScreenLogic.TroopType)reader.ReadByte();
			}
		}

		// Token: 0x020006B1 RID: 1713
		public abstract class TroopComparer : IComparer<TroopRosterElement>
		{
			// Token: 0x06005374 RID: 21364 RVA: 0x0018D0A4 File Offset: 0x0018B2A4
			public void SetIsAscending(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x06005375 RID: 21365 RVA: 0x0018D0AD File Offset: 0x0018B2AD
			private int GetHeroComparisonResult(TroopRosterElement x, TroopRosterElement y)
			{
				if (x.Character.HeroObject != null)
				{
					if (x.Character.HeroObject == Hero.MainHero)
					{
						return -2;
					}
					if (y.Character.HeroObject == null)
					{
						return -1;
					}
				}
				return 0;
			}

			// Token: 0x06005376 RID: 21366 RVA: 0x0018D0E4 File Offset: 0x0018B2E4
			public int Compare(TroopRosterElement x, TroopRosterElement y)
			{
				int num = (this._isAscending ? 1 : (-1));
				int num2 = this.GetHeroComparisonResult(x, y);
				if (num2 != 0)
				{
					return num2;
				}
				num2 = this.GetHeroComparisonResult(y, x);
				if (num2 != 0)
				{
					return num2 * -1;
				}
				return this.CompareTroops(x, y) * num;
			}

			// Token: 0x06005377 RID: 21367
			protected abstract int CompareTroops(TroopRosterElement x, TroopRosterElement y);

			// Token: 0x04001B13 RID: 6931
			private bool _isAscending;
		}

		// Token: 0x020006B2 RID: 1714
		private class TroopDefaultComparer : PartyScreenLogic.TroopComparer
		{
			// Token: 0x06005379 RID: 21369 RVA: 0x0018D12E File Offset: 0x0018B32E
			protected override int CompareTroops(TroopRosterElement x, TroopRosterElement y)
			{
				return 0;
			}
		}

		// Token: 0x020006B3 RID: 1715
		private class TroopTypeComparer : PartyScreenLogic.TroopComparer
		{
			// Token: 0x0600537B RID: 21371 RVA: 0x0018D13C File Offset: 0x0018B33C
			protected override int CompareTroops(TroopRosterElement x, TroopRosterElement y)
			{
				int defaultFormationClass = (int)x.Character.DefaultFormationClass;
				int defaultFormationClass2 = (int)y.Character.DefaultFormationClass;
				return defaultFormationClass.CompareTo(defaultFormationClass2);
			}
		}

		// Token: 0x020006B4 RID: 1716
		private class TroopNameComparer : PartyScreenLogic.TroopComparer
		{
			// Token: 0x0600537D RID: 21373 RVA: 0x0018D171 File Offset: 0x0018B371
			protected override int CompareTroops(TroopRosterElement x, TroopRosterElement y)
			{
				return x.Character.Name.ToString().CompareTo(y.Character.Name.ToString());
			}
		}

		// Token: 0x020006B5 RID: 1717
		private class TroopCountComparer : PartyScreenLogic.TroopComparer
		{
			// Token: 0x0600537F RID: 21375 RVA: 0x0018D1A0 File Offset: 0x0018B3A0
			protected override int CompareTroops(TroopRosterElement x, TroopRosterElement y)
			{
				return x.Number.CompareTo(y.Number);
			}
		}

		// Token: 0x020006B6 RID: 1718
		private class TroopTierComparer : PartyScreenLogic.TroopComparer
		{
			// Token: 0x06005381 RID: 21377 RVA: 0x0018D1CC File Offset: 0x0018B3CC
			protected override int CompareTroops(TroopRosterElement x, TroopRosterElement y)
			{
				return x.Character.Tier.CompareTo(y.Character.Tier);
			}
		}
	}
}
