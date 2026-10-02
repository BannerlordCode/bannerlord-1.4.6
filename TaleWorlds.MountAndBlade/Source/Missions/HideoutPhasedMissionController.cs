using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003D4 RID: 980
	public class HideoutPhasedMissionController : MissionLogic
	{
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000E113F File Offset: 0x000DF33F
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Logic;
			}
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000E1144 File Offset: 0x000DF344
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this._isNewlyPopulatedFormationGivenOrder)
			{
				foreach (Team team in base.Mission.Teams)
				{
					if (team.Side == BattleSideEnum.Defender)
					{
						foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation.CountOfUnits > 0)
							{
								formation.SetMovementOrder(MovementOrder.MovementOrderMove(formation.CachedMedianPosition));
								this._isNewlyPopulatedFormationGivenOrder = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000E120C File Offset: 0x000DF40C
		protected override void OnEndMission()
		{
			base.Mission.AreOrderGesturesEnabled_AdditionalCondition -= this.AreOrderGesturesEnabled_AdditionalCondition;
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000E1225 File Offset: 0x000DF425
		public override void OnBehaviorInitialize()
		{
			this.ReadySpawnPointLogic();
			base.Mission.AreOrderGesturesEnabled_AdditionalCondition += this.AreOrderGesturesEnabled_AdditionalCondition;
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000E1244 File Offset: 0x000DF444
		public override void AfterStart()
		{
			base.AfterStart();
			DefaultBattleMissionAgentSpawnLogic missionBehavior = base.Mission.GetMissionBehavior<DefaultBattleMissionAgentSpawnLogic>();
			if (missionBehavior != null && this.IsPhasingInitialized)
			{
				missionBehavior.AddPhaseChangeAction(BattleSideEnum.Defender, new DefaultBattleMissionAgentSpawnLogic.OnPhaseChangedDelegate(this.OnPhaseChanged));
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000E1281 File Offset: 0x000DF481
		private bool IsPhasingInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000E1284 File Offset: 0x000DF484
		private void ReadySpawnPointLogic()
		{
			List<WeakGameEntity> list = Mission.Current.GetActiveEntitiesWithScriptComponentOfType<HideoutSpawnPointGroup>().ToList<WeakGameEntity>();
			if (list.Count == 0)
			{
				return;
			}
			HideoutSpawnPointGroup[] array = new HideoutSpawnPointGroup[list.Count];
			foreach (WeakGameEntity weakGameEntity in list)
			{
				HideoutSpawnPointGroup firstScriptOfType = weakGameEntity.GetFirstScriptOfType<HideoutSpawnPointGroup>();
				array[firstScriptOfType.PhaseNumber - 1] = firstScriptOfType;
			}
			List<HideoutSpawnPointGroup> list2 = array.ToList<HideoutSpawnPointGroup>();
			list2.RemoveAt(0);
			for (int i = 0; i < 3; i++)
			{
				list2.RemoveAt(MBRandom.RandomInt(list2.Count));
			}
			this._spawnPointFrames = new Stack<MatrixFrame[]>();
			for (int j = 0; j < array.Length; j++)
			{
				if (!list2.Contains(array[j]))
				{
					this._spawnPointFrames.Push(array[j].GetSpawnPointFrames());
					Debug.Print("Spawn " + array[j].PhaseNumber + " is active.", 0, Debug.DebugColor.Green, 64UL);
				}
				array[j].RemoveWithAllChildren();
			}
			this.CreateSpawnPoints();
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000E13AC File Offset: 0x000DF5AC
		private void CreateSpawnPoints()
		{
			MatrixFrame[] array = this._spawnPointFrames.Pop();
			this._spawnPoints = new GameEntity[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsIdentity)
				{
					this._spawnPoints[i] = GameEntity.CreateEmpty(base.Mission.Scene, true, true, true);
					this._spawnPoints[i].SetGlobalFrame(in array[i], true);
					this._spawnPoints[i].AddTag("defender_" + ((FormationClass)i).GetName().ToLower());
				}
			}
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000E1444 File Offset: 0x000DF644
		private void OnPhaseChanged()
		{
			if (this._spawnPointFrames.Count == 0)
			{
				Debug.FailedAssert("No position left.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\HideoutPhasedMissionController.cs", "OnPhaseChanged", 142);
				return;
			}
			for (int i = 0; i < this._spawnPoints.Length; i++)
			{
				if (!(this._spawnPoints[i] == null))
				{
					this._spawnPoints[i].Remove(78);
				}
			}
			this.CreateSpawnPoints();
			this._isNewlyPopulatedFormationGivenOrder = false;
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000E14B7 File Offset: 0x000DF6B7
		private bool AreOrderGesturesEnabled_AdditionalCondition()
		{
			return false;
		}

		// Token: 0x04001765 RID: 5989
		public const int PhaseCount = 4;

		// Token: 0x04001766 RID: 5990
		private GameEntity[] _spawnPoints;

		// Token: 0x04001767 RID: 5991
		private Stack<MatrixFrame[]> _spawnPointFrames;

		// Token: 0x04001768 RID: 5992
		private bool _isNewlyPopulatedFormationGivenOrder = true;
	}
}
