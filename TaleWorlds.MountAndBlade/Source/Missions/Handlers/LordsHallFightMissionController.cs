using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003DA RID: 986
	public class LordsHallFightMissionController : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x000E1875 File Offset: 0x000DFA75
		// (set) Token: 0x06003685 RID: 13957 RVA: 0x000E187D File Offset: 0x000DFA7D
		public BattleSideEnum PlayerSide { get; private set; }

		// Token: 0x06003686 RID: 13958 RVA: 0x000E1888 File Offset: 0x000DFA88
		public LordsHallFightMissionController(IMissionTroopSupplier[] suppliers, float areaLostRatio, float attackerDefenderTroopCountRatio, int attackerSideTroopCountMax, int defenderSideTroopCountMax, BattleSideEnum playerSide)
		{
			this.PlayerSide = playerSide;
			this._areaLostRatio = areaLostRatio;
			this._attackerDefenderTroopCountRatio = attackerDefenderTroopCountRatio;
			this._attackerSideTroopCountMax = attackerSideTroopCountMax;
			this._defenderSideTroopCountMax = defenderSideTroopCountMax;
			this._missionSides = new LordsHallFightMissionController.MissionSide[2];
			this._playerSide = playerSide;
			for (int i = 0; i < 2; i++)
			{
				IMissionTroopSupplier missionTroopSupplier = suppliers[i];
				bool flag = i == (int)playerSide;
				this._missionSides[i] = new LordsHallFightMissionController.MissionSide((BattleSideEnum)i, missionTroopSupplier, flag);
			}
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000E18FB File Offset: 0x000DFAFB
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.Mission.GetAgentTroopClass_Override += this.GetLordsHallFightTroopClass;
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000E191A File Offset: 0x000DFB1A
		public override void OnMissionStateFinalized()
		{
			base.OnMissionStateFinalized();
			base.Mission.GetAgentTroopClass_Override -= this.GetLordsHallFightTroopClass;
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000E1939 File Offset: 0x000DFB39
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = false;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000E1950 File Offset: 0x000DFB50
		public override void OnMissionTick(float dt)
		{
			if (!this._isMissionInitialized)
			{
				this.InitializeMission();
				this._isMissionInitialized = true;
				return;
			}
			if (!this._troopsInitialized)
			{
				this._troopsInitialized = true;
			}
			if (this._setChargeOrderNextFrame)
			{
				if (base.Mission.PlayerTeam.ActiveAgents.Count > 0)
				{
					base.Mission.PlayerTeam.PlayerOrderController.SelectAllFormations(false);
					base.Mission.PlayerTeam.PlayerOrderController.SetOrder(OrderType.Charge);
				}
				this._setChargeOrderNextFrame = false;
			}
			this.CheckForReinforcement();
			this.CheckIfAnyAreaIsLostByDefender();
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000E19E4 File Offset: 0x000DFBE4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (!affectedAgent.Team.IsDefender)
			{
				this._setChargeOrderNextFrame = affectedAgent.IsMainAgent;
				this._removedAllyCounter++;
				if (this._removedAllyCounter == 5)
				{
					this._spawnReinforcements = true;
					this._removedAllyCounter = 0;
				}
				return;
			}
			Tuple<int, LordsHallFightMissionController.AreaEntityData> tuple = this.FindAgentMachine(affectedAgent);
			if (tuple == null)
			{
				return;
			}
			tuple.Item2.StopUse();
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000E1A48 File Offset: 0x000DFC48
		private Tuple<int, LordsHallFightMissionController.AreaEntityData> FindAgentMachine(Agent agent)
		{
			Tuple<int, LordsHallFightMissionController.AreaEntityData> tuple = null;
			foreach (KeyValuePair<int, Dictionary<int, LordsHallFightMissionController.AreaData>> keyValuePair in this._dividedAreaDictionary)
			{
				if (tuple != null)
				{
					break;
				}
				foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair2 in keyValuePair.Value)
				{
					LordsHallFightMissionController.AreaEntityData areaEntityData = keyValuePair2.Value.FindAgentMachine(agent);
					if (areaEntityData != null)
					{
						tuple = new Tuple<int, LordsHallFightMissionController.AreaEntityData>(keyValuePair.Key, areaEntityData);
						break;
					}
				}
			}
			return tuple;
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000E1B00 File Offset: 0x000DFD00
		private void InitializeMission()
		{
			this._areaIndexList = new List<int>();
			this._dividedAreaDictionary = new Dictionary<int, Dictionary<int, LordsHallFightMissionController.AreaData>>();
			IEnumerable<FightAreaMarker> enumerable = from area in base.Mission.ActiveMissionObjects.FindAllWithType<FightAreaMarker>()
				orderby area.AreaIndex
				select area;
			base.Mission.DeploymentPlan.MakeDefaultDeploymentPlans();
			foreach (FightAreaMarker fightAreaMarker in enumerable)
			{
				if (!this._dividedAreaDictionary.ContainsKey(fightAreaMarker.AreaIndex))
				{
					this._dividedAreaDictionary.Add(fightAreaMarker.AreaIndex, new Dictionary<int, LordsHallFightMissionController.AreaData>());
				}
				if (!this._dividedAreaDictionary[fightAreaMarker.AreaIndex].ContainsKey(fightAreaMarker.SubAreaIndex))
				{
					this._dividedAreaDictionary[fightAreaMarker.AreaIndex].Add(fightAreaMarker.SubAreaIndex, new LordsHallFightMissionController.AreaData(new List<FightAreaMarker> { fightAreaMarker }));
				}
				else
				{
					this._dividedAreaDictionary[fightAreaMarker.AreaIndex][fightAreaMarker.SubAreaIndex].AddAreaMarker(fightAreaMarker);
				}
			}
			this._areaIndexList = this._dividedAreaDictionary.Keys.ToList<int>();
			this._missionSides[0].SpawnTroops(this._dividedAreaDictionary, this._defenderSideTroopCountMax);
			int numberOfActiveTroops = this._missionSides[0].NumberOfActiveTroops;
			this._defenderTeams = new Team[2];
			this._defenderTeams[0] = Mission.Current.DefenderTeam;
			this._defenderTeams[1] = Mission.Current.DefenderAllyTeam;
			int num = MathF.Max(1, MathF.Min(this._attackerSideTroopCountMax, MathF.Round((float)numberOfActiveTroops * this._attackerDefenderTroopCountRatio)));
			this._missionSides[1].SpawnTroops(num, false);
			bool flag = Mission.Current.AttackerTeam == Mission.Current.PlayerTeam || (Mission.Current.AttackerAllyTeam != null && Mission.Current.AttackerAllyTeam == Mission.Current.PlayerTeam);
			this._attackerTeams = new Team[2];
			this._attackerTeams[0] = Mission.Current.AttackerTeam;
			this._attackerTeams[1] = Mission.Current.AttackerAllyTeam;
			foreach (Team team in this._attackerTeams)
			{
				if (team != null)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						if (formation.CountOfUnits > 0)
						{
							formation.SetArrangementOrder(ArrangementOrder.ArrangementOrderSquare);
							formation.SetFormOrder(FormOrder.FormOrderDeep, true);
						}
						formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
						formation.SetFiringOrder(FiringOrder.FiringOrderHoldYourFire);
						if (flag)
						{
							formation.PlayerOwner = Mission.Current.MainAgent;
						}
					}
				}
			}
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000E1E0C File Offset: 0x000E000C
		private void CheckForReinforcement()
		{
			if (this._spawnReinforcements)
			{
				this._missionSides[1].SpawnTroops(5, true);
				this._spawnReinforcements = false;
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000E1E2C File Offset: 0x000E002C
		public void StartSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(true);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000E1E3C File Offset: 0x000E003C
		public void StopSpawner(BattleSideEnum side)
		{
			this._missionSides[(int)side].SetSpawnTroops(false);
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000E1E4C File Offset: 0x000E004C
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return this._missionSides[(int)side].TroopSpawningActive;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000E1E5B File Offset: 0x000E005B
		public float GetReinforcementInterval(BattleSideEnum side = BattleSideEnum.None)
		{
			return 0f;
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000E1E62 File Offset: 0x000E0062
		public bool IsSideDepleted(BattleSideEnum side)
		{
			return this._missionSides[(int)side].NumberOfActiveTroops == 0;
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000E1E74 File Offset: 0x000E0074
		public int GetNumberOfPlayerControllableTroops()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000E1E7B File Offset: 0x000E007B
		public IEnumerable<IAgentOriginBase> GetAllTroopsForSide(BattleSideEnum side)
		{
			return this._missionSides[(int)side].GetAllTroops();
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000E1E8A File Offset: 0x000E008A
		public bool GetSpawnHorses(BattleSideEnum side)
		{
			return false;
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000E1E90 File Offset: 0x000E0090
		private void CheckIfAnyAreaIsLostByDefender()
		{
			int num = -1;
			for (int i = 0; i < this._areaIndexList.Count; i++)
			{
				int num2 = this._areaIndexList[i];
				if (num2 > this._lastAreaLostByDefender && num < 0)
				{
					foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in this._dividedAreaDictionary[num2])
					{
						if (this.IsAreaLostByDefender(keyValuePair.Value))
						{
							num = num2;
							break;
						}
					}
				}
			}
			if (num > 0)
			{
				this.OnAreaLost(num);
			}
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000E1F34 File Offset: 0x000E0134
		private void OnAreaLost(int areaIndex)
		{
			int num = MathF.Min(this._areaIndexList.IndexOf(areaIndex) + 1, this._areaIndexList.Count - 1);
			for (int i = MathF.Max(0, this._areaIndexList.IndexOf(this._lastAreaLostByDefender)); i < num; i++)
			{
				int num2 = this._areaIndexList[i];
				foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in this._dividedAreaDictionary[num2])
				{
					this.StartAreaPullBack(keyValuePair.Value, this._areaIndexList[num]);
				}
			}
			this._lastAreaLostByDefender = areaIndex;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000E1FF8 File Offset: 0x000E01F8
		private void StartAreaPullBack(LordsHallFightMissionController.AreaData areaData, int nextAreaIndex)
		{
			foreach (LordsHallFightMissionController.AreaEntityData areaEntityData in areaData.ArcherUsablePoints)
			{
				if (areaEntityData.InUse)
				{
					Agent userAgent = areaEntityData.UserAgent;
					areaEntityData.StopUse();
					LordsHallFightMissionController.AreaEntityData areaEntityData2 = this.FindPosition(nextAreaIndex, true);
					if (areaEntityData2 != null)
					{
						areaEntityData2.AssignAgent(userAgent);
					}
				}
			}
			foreach (LordsHallFightMissionController.AreaEntityData areaEntityData3 in areaData.InfantryUsablePoints)
			{
				if (areaEntityData3.InUse)
				{
					Agent userAgent2 = areaEntityData3.UserAgent;
					areaEntityData3.StopUse();
					LordsHallFightMissionController.AreaEntityData areaEntityData4 = this.FindPosition(nextAreaIndex, false);
					if (areaEntityData4 != null)
					{
						areaEntityData4.AssignAgent(userAgent2);
					}
				}
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000E20C8 File Offset: 0x000E02C8
		private LordsHallFightMissionController.AreaEntityData FindPosition(int nextAreaIndex, bool isArcher)
		{
			int num = this.SelectBestSubArea(nextAreaIndex, isArcher);
			if (num < 0)
			{
				isArcher = !isArcher;
				num = this.SelectBestSubArea(nextAreaIndex, isArcher);
			}
			return this._dividedAreaDictionary[nextAreaIndex][num].GetAvailableMachines(isArcher).GetRandomElementInefficiently<LordsHallFightMissionController.AreaEntityData>();
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000E2110 File Offset: 0x000E0310
		private int SelectBestSubArea(int areaIndex, bool isArcher)
		{
			int num = -1;
			float num2 = 0f;
			foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in this._dividedAreaDictionary[areaIndex])
			{
				float areaAvailabilityRatio = this.GetAreaAvailabilityRatio(keyValuePair.Value, isArcher);
				if (areaAvailabilityRatio > num2)
				{
					num2 = areaAvailabilityRatio;
					num = keyValuePair.Key;
				}
			}
			return num;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000E218C File Offset: 0x000E038C
		private float GetAreaAvailabilityRatio(LordsHallFightMissionController.AreaData areaData, bool isArcher)
		{
			int num = (isArcher ? areaData.ArcherUsablePoints.Count<LordsHallFightMissionController.AreaEntityData>() : areaData.InfantryUsablePoints.Count<LordsHallFightMissionController.AreaEntityData>());
			int num2;
			if (!isArcher)
			{
				num2 = areaData.InfantryUsablePoints.Count<LordsHallFightMissionController.AreaEntityData>((LordsHallFightMissionController.AreaEntityData x) => !x.InUse);
			}
			else
			{
				num2 = areaData.ArcherUsablePoints.Count<LordsHallFightMissionController.AreaEntityData>((LordsHallFightMissionController.AreaEntityData x) => !x.InUse);
			}
			int num3 = num2;
			if (num != 0)
			{
				return (float)num3 / (float)num;
			}
			return 0f;
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000E2220 File Offset: 0x000E0420
		private bool IsAreaLostByDefender(LordsHallFightMissionController.AreaData areaData)
		{
			int num = 0;
			foreach (Team team in this._defenderTeams)
			{
				if (team != null)
				{
					foreach (Agent agent in team.ActiveAgents)
					{
						if (this.IsAgentInArea(agent, areaData))
						{
							num++;
						}
					}
				}
			}
			int num2 = MathF.Round((float)num * this._areaLostRatio);
			bool flag = num2 == 0;
			if (!flag)
			{
				foreach (Team team2 in this._attackerTeams)
				{
					if (team2 != null)
					{
						foreach (Agent agent2 in team2.ActiveAgents)
						{
							if (this.IsAgentInArea(agent2, areaData))
							{
								num2--;
								if (num2 == 0)
								{
									flag = true;
									break;
								}
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000E2338 File Offset: 0x000E0538
		private bool IsAgentInArea(Agent agent, LordsHallFightMissionController.AreaData areaData)
		{
			bool flag = false;
			Vec3 position = agent.Position;
			using (IEnumerator<FightAreaMarker> enumerator = areaData.AreaList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsPositionInRange(position))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000E2394 File Offset: 0x000E0594
		private FormationClass GetLordsHallFightTroopClass(BattleSideEnum side, BasicCharacterObject agentCharacter)
		{
			return agentCharacter.GetFormationClass().DismountedClass();
		}

		// Token: 0x0400176F RID: 5999
		private const int ReinforcementWaveAgentCount = 5;

		// Token: 0x04001771 RID: 6001
		private readonly float _areaLostRatio;

		// Token: 0x04001772 RID: 6002
		private readonly float _attackerDefenderTroopCountRatio;

		// Token: 0x04001773 RID: 6003
		private readonly int _attackerSideTroopCountMax;

		// Token: 0x04001774 RID: 6004
		private readonly int _defenderSideTroopCountMax;

		// Token: 0x04001775 RID: 6005
		private readonly LordsHallFightMissionController.MissionSide[] _missionSides;

		// Token: 0x04001776 RID: 6006
		private Team[] _attackerTeams;

		// Token: 0x04001777 RID: 6007
		private Team[] _defenderTeams;

		// Token: 0x04001778 RID: 6008
		private Dictionary<int, Dictionary<int, LordsHallFightMissionController.AreaData>> _dividedAreaDictionary;

		// Token: 0x04001779 RID: 6009
		private List<int> _areaIndexList;

		// Token: 0x0400177A RID: 6010
		private int _lastAreaLostByDefender;

		// Token: 0x0400177B RID: 6011
		private bool _troopsInitialized;

		// Token: 0x0400177C RID: 6012
		private bool _isMissionInitialized;

		// Token: 0x0400177D RID: 6013
		private bool _spawnReinforcements;

		// Token: 0x0400177E RID: 6014
		private bool _setChargeOrderNextFrame;

		// Token: 0x0400177F RID: 6015
		private BattleSideEnum _playerSide;

		// Token: 0x04001780 RID: 6016
		private int _removedAllyCounter;

		// Token: 0x0200068E RID: 1678
		private class MissionSide
		{
			// Token: 0x17000AF5 RID: 2805
			// (get) Token: 0x06004172 RID: 16754 RVA: 0x000FB8A4 File Offset: 0x000F9AA4
			public bool TroopSpawningActive
			{
				get
				{
					return this._troopSpawningActive;
				}
			}

			// Token: 0x17000AF6 RID: 2806
			// (get) Token: 0x06004173 RID: 16755 RVA: 0x000FB8AC File Offset: 0x000F9AAC
			public int NumberOfActiveTroops
			{
				get
				{
					return this._numberOfSpawnedTroops - this._troopSupplier.NumRemovedTroops;
				}
			}

			// Token: 0x06004174 RID: 16756 RVA: 0x000FB8C0 File Offset: 0x000F9AC0
			public MissionSide(BattleSideEnum side, IMissionTroopSupplier troopSupplier, bool isPlayerSide)
			{
				this._side = side;
				this._isPlayerSide = isPlayerSide;
				this._troopSupplier = troopSupplier;
			}

			// Token: 0x06004175 RID: 16757 RVA: 0x000FB8E4 File Offset: 0x000F9AE4
			public void SpawnTroops(Dictionary<int, Dictionary<int, LordsHallFightMissionController.AreaData>> areaMarkerDictionary, int spawnCount)
			{
				List<IAgentOriginBase> list = this._troopSupplier.SupplyTroops(spawnCount).OrderByDescending<IAgentOriginBase, int>(delegate(IAgentOriginBase x)
				{
					FormationClass agentTroopClass = Mission.Current.GetAgentTroopClass(this._side, x.Troop);
					if (agentTroopClass != FormationClass.Ranged && agentTroopClass != FormationClass.HorseArcher)
					{
						return 0;
					}
					return 1;
				}).ToList<IAgentOriginBase>();
				for (int i = 0; i < list.Count; i++)
				{
					IAgentOriginBase agentOriginBase = list[i];
					bool flag = Mission.Current.GetAgentTroopClass(this._side, agentOriginBase.Troop).IsRanged();
					List<KeyValuePair<int, LordsHallFightMissionController.AreaData>> list2 = areaMarkerDictionary.ElementAt<KeyValuePair<int, Dictionary<int, LordsHallFightMissionController.AreaData>>>(i % areaMarkerDictionary.Count).Value.ToList<KeyValuePair<int, LordsHallFightMissionController.AreaData>>();
					List<ValueTuple<KeyValuePair<int, LordsHallFightMissionController.AreaData>, float>> list3 = new List<ValueTuple<KeyValuePair<int, LordsHallFightMissionController.AreaData>, float>>();
					foreach (KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair in list2)
					{
						int num = 1000 * keyValuePair.Value.GetAvailableMachines(flag).Count<LordsHallFightMissionController.AreaEntityData>() + keyValuePair.Value.GetAvailableMachines(!flag).Count<LordsHallFightMissionController.AreaEntityData>();
						list3.Add(new ValueTuple<KeyValuePair<int, LordsHallFightMissionController.AreaData>, float>(new KeyValuePair<int, LordsHallFightMissionController.AreaData>(keyValuePair.Key, keyValuePair.Value), (float)num));
					}
					KeyValuePair<int, LordsHallFightMissionController.AreaData> keyValuePair2 = MBRandom.ChooseWeighted<KeyValuePair<int, LordsHallFightMissionController.AreaData>>(list3);
					LordsHallFightMissionController.AreaEntityData areaEntityData = keyValuePair2.Value.GetAvailableMachines(flag).GetRandomElementInefficiently<LordsHallFightMissionController.AreaEntityData>() ?? keyValuePair2.Value.GetAvailableMachines(!flag).GetRandomElementInefficiently<LordsHallFightMissionController.AreaEntityData>();
					MatrixFrame globalFrame = areaEntityData.Entity.GetGlobalFrame();
					Agent agent = Mission.Current.SpawnTroop(agentOriginBase, false, false, false, false, 0, 0, false, false, new Vec3?(globalFrame.origin), new Vec2?(globalFrame.rotation.f.AsVec2.Normalized()), null, null, FormationClass.NumberOfAllFormations, false);
					this._numberOfSpawnedTroops++;
					AgentFlag agentFlags = agent.GetAgentFlags();
					agent.SetAgentFlags(agentFlags & ~AgentFlag.CanRetreat);
					agent.WieldInitialWeapons(Agent.WeaponWieldActionType.Instant, Equipment.InitialWeaponEquipPreference.Any);
					agent.SetWatchState(Agent.WatchState.Alarmed);
					agent.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefensiveArrangementMove);
					areaEntityData.AssignAgent(agent);
				}
			}

			// Token: 0x06004176 RID: 16758 RVA: 0x000FBACC File Offset: 0x000F9CCC
			public void SpawnTroops(int spawnCount, bool isReinforcement)
			{
				if (this._troopSpawningActive)
				{
					List<IAgentOriginBase> list = this._troopSupplier.SupplyTroops(spawnCount).ToList<IAgentOriginBase>();
					for (int i = 0; i < list.Count; i++)
					{
						if (BattleSideEnum.Attacker == this._side)
						{
							Mission.Current.SpawnTroop(list[i], this._isPlayerSide, true, false, isReinforcement, spawnCount, i, true, true, null, null, null, null, FormationClass.NumberOfAllFormations, false);
							this._numberOfSpawnedTroops++;
						}
					}
				}
			}

			// Token: 0x06004177 RID: 16759 RVA: 0x000FBB51 File Offset: 0x000F9D51
			public void SetSpawnTroops(bool spawnTroops)
			{
				this._troopSpawningActive = spawnTroops;
			}

			// Token: 0x06004178 RID: 16760 RVA: 0x000FBB5A File Offset: 0x000F9D5A
			public IEnumerable<IAgentOriginBase> GetAllTroops()
			{
				return this._troopSupplier.GetAllTroops();
			}

			// Token: 0x040022AC RID: 8876
			private readonly BattleSideEnum _side;

			// Token: 0x040022AD RID: 8877
			private readonly IMissionTroopSupplier _troopSupplier;

			// Token: 0x040022AE RID: 8878
			private readonly bool _isPlayerSide;

			// Token: 0x040022AF RID: 8879
			private bool _troopSpawningActive = true;

			// Token: 0x040022B0 RID: 8880
			private int _numberOfSpawnedTroops;
		}

		// Token: 0x0200068F RID: 1679
		private class AreaData
		{
			// Token: 0x17000AF7 RID: 2807
			// (get) Token: 0x0600417A RID: 16762 RVA: 0x000FBB97 File Offset: 0x000F9D97
			public IEnumerable<FightAreaMarker> AreaList
			{
				get
				{
					return this._areaList;
				}
			}

			// Token: 0x17000AF8 RID: 2808
			// (get) Token: 0x0600417B RID: 16763 RVA: 0x000FBB9F File Offset: 0x000F9D9F
			public IEnumerable<LordsHallFightMissionController.AreaEntityData> ArcherUsablePoints
			{
				get
				{
					return this._archerUsablePoints;
				}
			}

			// Token: 0x17000AF9 RID: 2809
			// (get) Token: 0x0600417C RID: 16764 RVA: 0x000FBBA7 File Offset: 0x000F9DA7
			public IEnumerable<LordsHallFightMissionController.AreaEntityData> InfantryUsablePoints
			{
				get
				{
					return this._infantryUsablePoints;
				}
			}

			// Token: 0x0600417D RID: 16765 RVA: 0x000FBBB0 File Offset: 0x000F9DB0
			public AreaData(List<FightAreaMarker> areaList)
			{
				this._areaList = new List<FightAreaMarker>();
				this._archerUsablePoints = new List<LordsHallFightMissionController.AreaEntityData>();
				this._infantryUsablePoints = new List<LordsHallFightMissionController.AreaEntityData>();
				foreach (FightAreaMarker fightAreaMarker in areaList)
				{
					this.AddAreaMarker(fightAreaMarker);
				}
			}

			// Token: 0x0600417E RID: 16766 RVA: 0x000FBC28 File Offset: 0x000F9E28
			public IEnumerable<LordsHallFightMissionController.AreaEntityData> GetAvailableMachines(bool isArcher)
			{
				List<LordsHallFightMissionController.AreaEntityData> list = (isArcher ? this._archerUsablePoints : this._infantryUsablePoints);
				foreach (LordsHallFightMissionController.AreaEntityData areaEntityData in list)
				{
					if (!areaEntityData.InUse)
					{
						yield return areaEntityData;
					}
				}
				List<LordsHallFightMissionController.AreaEntityData>.Enumerator enumerator = default(List<LordsHallFightMissionController.AreaEntityData>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x0600417F RID: 16767 RVA: 0x000FBC40 File Offset: 0x000F9E40
			public void AddAreaMarker(FightAreaMarker marker)
			{
				this._areaList.Add(marker);
				using (List<GameEntity>.Enumerator enumerator = marker.GetGameEntitiesWithTagInRange("defender_archer").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameEntity entity2 = enumerator.Current;
						PathFaceRecord nullFaceRecord = PathFaceRecord.NullFaceRecord;
						Mission.Current.Scene.GetNavMeshFaceIndex(ref nullFaceRecord, entity2.GetGlobalFrame().origin, true);
						if (nullFaceRecord.FaceIndex != -1 && this._archerUsablePoints.All<LordsHallFightMissionController.AreaEntityData>((LordsHallFightMissionController.AreaEntityData x) => x.Entity != entity2))
						{
							this._archerUsablePoints.Add(new LordsHallFightMissionController.AreaEntityData(entity2));
						}
					}
				}
				using (List<GameEntity>.Enumerator enumerator = marker.GetGameEntitiesWithTagInRange("defender_infantry").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameEntity entity = enumerator.Current;
						if (this._infantryUsablePoints.All<LordsHallFightMissionController.AreaEntityData>((LordsHallFightMissionController.AreaEntityData x) => x.Entity != entity))
						{
							this._infantryUsablePoints.Add(new LordsHallFightMissionController.AreaEntityData(entity));
						}
					}
				}
			}

			// Token: 0x06004180 RID: 16768 RVA: 0x000FBD8C File Offset: 0x000F9F8C
			public LordsHallFightMissionController.AreaEntityData FindAgentMachine(Agent agent)
			{
				return this._infantryUsablePoints.FirstOrDefault<LordsHallFightMissionController.AreaEntityData>((LordsHallFightMissionController.AreaEntityData x) => x.UserAgent == agent) ?? this._archerUsablePoints.FirstOrDefault<LordsHallFightMissionController.AreaEntityData>((LordsHallFightMissionController.AreaEntityData x) => x.UserAgent == agent);
			}

			// Token: 0x040022B1 RID: 8881
			private const string ArcherSpawnPointTag = "defender_archer";

			// Token: 0x040022B2 RID: 8882
			private const string InfantrySpawnPointTag = "defender_infantry";

			// Token: 0x040022B3 RID: 8883
			private readonly List<FightAreaMarker> _areaList;

			// Token: 0x040022B4 RID: 8884
			private readonly List<LordsHallFightMissionController.AreaEntityData> _archerUsablePoints;

			// Token: 0x040022B5 RID: 8885
			private readonly List<LordsHallFightMissionController.AreaEntityData> _infantryUsablePoints;
		}

		// Token: 0x02000690 RID: 1680
		private class AreaEntityData
		{
			// Token: 0x17000AFA RID: 2810
			// (get) Token: 0x06004181 RID: 16769 RVA: 0x000FBDD8 File Offset: 0x000F9FD8
			// (set) Token: 0x06004182 RID: 16770 RVA: 0x000FBDE0 File Offset: 0x000F9FE0
			public Agent UserAgent { get; private set; }

			// Token: 0x17000AFB RID: 2811
			// (get) Token: 0x06004183 RID: 16771 RVA: 0x000FBDE9 File Offset: 0x000F9FE9
			public bool InUse
			{
				get
				{
					return this.UserAgent != null;
				}
			}

			// Token: 0x06004184 RID: 16772 RVA: 0x000FBDF4 File Offset: 0x000F9FF4
			public AreaEntityData(GameEntity entity)
			{
				this.Entity = entity;
			}

			// Token: 0x06004185 RID: 16773 RVA: 0x000FBE04 File Offset: 0x000FA004
			public void AssignAgent(Agent agent)
			{
				this.UserAgent = agent;
				MatrixFrame globalFrame = this.Entity.GetGlobalFrame();
				agent.SetBehaviorValueSet(HumanAIComponent.BehaviorValueSet.DefaultMove);
				this.UserAgent.SetFormationFrameEnabled(new WorldPosition(agent.Mission.Scene, globalFrame.origin), globalFrame.rotation.f.AsVec2.Normalized(), Vec2.Zero, 0f);
			}

			// Token: 0x06004186 RID: 16774 RVA: 0x000FBE6F File Offset: 0x000FA06F
			public void StopUse()
			{
				if (this.UserAgent.IsActive())
				{
					this.UserAgent.SetFormationFrameDisabled();
				}
				this.UserAgent = null;
			}

			// Token: 0x040022B6 RID: 8886
			public readonly GameEntity Entity;
		}
	}
}
