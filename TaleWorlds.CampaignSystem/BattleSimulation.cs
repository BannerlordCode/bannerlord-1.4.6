using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200002C RID: 44
	public class BattleSimulation : IBattleObserver
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00013B23 File Offset: 0x00011D23
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00013B2B File Offset: 0x00011D2B
		public bool IsSimulationFinished { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00013B34 File Offset: 0x00011D34
		private bool IsPlayerJoinedBattle
		{
			get
			{
				return PlayerEncounter.Current.IsJoinedBattle;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00013B40 File Offset: 0x00011D40
		public MapEvent MapEvent
		{
			get
			{
				return this._mapEvent;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00013B48 File Offset: 0x00011D48
		public bool IsPlayerRetreated
		{
			get
			{
				return this._isPlayerRetreated;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00013B50 File Offset: 0x00011D50
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00013B58 File Offset: 0x00011D58
		public IBattleObserver BattleObserver
		{
			get
			{
				return this._battleObserver;
			}
			set
			{
				this._battleObserver = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00013B61 File Offset: 0x00011D61
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00013B69 File Offset: 0x00011D69
		public List<List<BattleResultPartyData>> Teams { get; private set; }

		// Token: 0x060001E5 RID: 485 RVA: 0x00013B74 File Offset: 0x00011D74
		public BattleSimulation(FlattenedTroopRoster selectedTroopsForPlayerSide, FlattenedTroopRoster selectedTroopsForOtherSide)
		{
			this._mapEvent = PlayerEncounter.Battle ?? PlayerEncounter.StartBattle();
			this._mapEvent.IsPlayerSimulation = true;
			this._mapEvent.BattleObserver = this;
			this.SelectedTroops[(int)this._mapEvent.PlayerSide] = selectedTroopsForPlayerSide;
			this.SelectedTroops[(int)this._mapEvent.GetOtherSide(this._mapEvent.PlayerSide)] = selectedTroopsForOtherSide;
			this._mapEvent.GetNumberOfInvolvedMen();
			if (this._mapEvent.IsSiegeAssault)
			{
				PlayerSiege.StartPlayerSiege(MobileParty.MainParty.Party.Side, true, this._mapEvent.MapEventSettlement);
			}
			List<List<BattleResultPartyData>> list = new List<List<BattleResultPartyData>>
			{
				new List<BattleResultPartyData>(),
				new List<BattleResultPartyData>()
			};
			foreach (PartyBase partyBase in this._mapEvent.InvolvedParties)
			{
				BattleResultPartyData battleResultPartyData = default(BattleResultPartyData);
				bool flag = false;
				foreach (BattleResultPartyData battleResultPartyData2 in list[(int)partyBase.Side])
				{
					if (battleResultPartyData2.Party == partyBase)
					{
						flag = true;
						battleResultPartyData = battleResultPartyData2;
						break;
					}
				}
				if (!flag)
				{
					battleResultPartyData = new BattleResultPartyData(partyBase);
					list[(int)partyBase.Side].Add(battleResultPartyData);
				}
				for (int i = 0; i < partyBase.MemberRoster.Count; i++)
				{
					TroopRosterElement elementCopyAtIndex = partyBase.MemberRoster.GetElementCopyAtIndex(i);
					if (!battleResultPartyData.Characters.Contains(elementCopyAtIndex.Character))
					{
						battleResultPartyData.Characters.Add(elementCopyAtIndex.Character);
					}
				}
			}
			this.Teams = list;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00013D60 File Offset: 0x00011F60
		public void Play()
		{
			this._simulationState = BattleSimulation.SimulationState.Play;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00013D69 File Offset: 0x00011F69
		public void FastForward()
		{
			this._simulationState = BattleSimulation.SimulationState.FastForward;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00013D72 File Offset: 0x00011F72
		public void Skip()
		{
			this._simulationState = BattleSimulation.SimulationState.Skip;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00013D7B File Offset: 0x00011F7B
		public void Pause()
		{
			this._simulationState = BattleSimulation.SimulationState.Pause;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00013D84 File Offset: 0x00011F84
		public void OnFinished()
		{
			foreach (PartyBase partyBase in this._mapEvent.InvolvedParties)
			{
				partyBase.MemberRoster.RemoveZeroCounts();
			}
			GameMenu.ActivateGameMenu("encounter");
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00013DE4 File Offset: 0x00011FE4
		public void OnPlayerRetreat()
		{
			this._isPlayerRetreated = true;
			this._mapEvent.CommitXpGains();
			this.OnFinished();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00013E00 File Offset: 0x00012000
		public void Tick(float dt)
		{
			if (this.IsSimulationFinished)
			{
				return;
			}
			if (PlayerEncounter.Current == null)
			{
				Debug.FailedAssert("PlayerEncounter.Current == null", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\BattleSimulation.cs", "Tick", 158);
				this.IsSimulationFinished = true;
				return;
			}
			if (BattleSimulation.ShouldFinishSimulation())
			{
				this.IsSimulationFinished = true;
				return;
			}
			if (this._simulationState == BattleSimulation.SimulationState.Skip)
			{
				while (!BattleSimulation.ShouldFinishSimulation())
				{
					this.SimulateBattle();
				}
				return;
			}
			if (this._simulationState == BattleSimulation.SimulationState.FastForward)
			{
				dt *= 6f;
			}
			else if (this._simulationState == BattleSimulation.SimulationState.Pause)
			{
				dt = 0f;
			}
			this._numTicks += dt;
			while (this._numTicks >= 1f && !BattleSimulation.ShouldFinishSimulation())
			{
				this.SimulateBattle();
				this._numTicks -= 1f;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00013EC6 File Offset: 0x000120C6
		public void ResetSimulation()
		{
			this.MapEvent.SimulateBattleSetup(PlayerEncounter.CurrentBattleSimulation.SelectedTroops);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00013EE0 File Offset: 0x000120E0
		public void TroopNumberChanged(BattleSideEnum side, IBattleCombatant battleCombatant, BasicCharacterObject character, int number = 0, int numberKilled = 0, int numberWounded = 0, int numberRouted = 0, int killCount = 0, int numberReadyToUpgrade = 0)
		{
			IBattleObserver battleObserver = this.BattleObserver;
			if (battleObserver == null)
			{
				return;
			}
			battleObserver.TroopNumberChanged(side, battleCombatant, character, number, numberKilled, numberWounded, numberRouted, killCount, numberReadyToUpgrade);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00013F0C File Offset: 0x0001210C
		public void HeroSkillIncreased(BattleSideEnum side, IBattleCombatant battleCombatant, BasicCharacterObject heroCharacter, SkillObject skill)
		{
			IBattleObserver battleObserver = this.BattleObserver;
			if (battleObserver == null)
			{
				return;
			}
			battleObserver.HeroSkillIncreased(side, battleCombatant, heroCharacter, skill);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00013F23 File Offset: 0x00012123
		public void BattleResultsReady()
		{
			IBattleObserver battleObserver = this.BattleObserver;
			if (battleObserver == null)
			{
				return;
			}
			battleObserver.BattleResultsReady();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00013F35 File Offset: 0x00012135
		public void TroopSideChanged(BattleSideEnum prevSide, BattleSideEnum newSide, IBattleCombatant battleCombatant, BasicCharacterObject character)
		{
			IBattleObserver battleObserver = this.BattleObserver;
			if (battleObserver == null)
			{
				return;
			}
			battleObserver.TroopSideChanged(prevSide, newSide, battleCombatant, character);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00013F4C File Offset: 0x0001214C
		private void SimulateBattle()
		{
			this._mapEvent.SimulatePlayerEncounterBattle();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00013F59 File Offset: 0x00012159
		private static bool ShouldFinishSimulation()
		{
			return PlayerEncounter.Battle.HasWinner;
		}

		// Token: 0x04000022 RID: 34
		private readonly MapEvent _mapEvent;

		// Token: 0x04000023 RID: 35
		private bool _isPlayerRetreated;

		// Token: 0x04000024 RID: 36
		private float _numTicks;

		// Token: 0x04000025 RID: 37
		private IBattleObserver _battleObserver;

		// Token: 0x04000027 RID: 39
		public readonly FlattenedTroopRoster[] SelectedTroops = new FlattenedTroopRoster[2];

		// Token: 0x04000028 RID: 40
		private BattleSimulation.SimulationState _simulationState;

		// Token: 0x020004F8 RID: 1272
		private enum SimulationState
		{
			// Token: 0x0400156B RID: 5483
			Play,
			// Token: 0x0400156C RID: 5484
			FastForward,
			// Token: 0x0400156D RID: 5485
			Skip,
			// Token: 0x0400156E RID: 5486
			Pause
		}
	}
}
