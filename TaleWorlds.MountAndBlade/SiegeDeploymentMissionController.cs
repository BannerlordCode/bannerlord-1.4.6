using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029A RID: 666
	public class SiegeDeploymentMissionController : DeploymentMissionController
	{
		// Token: 0x060024D3 RID: 9427 RVA: 0x00085CCF File Offset: 0x00083ECF
		public SiegeDeploymentMissionController(bool isPlayerAttacker)
			: base(isPlayerAttacker)
		{
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00085CD8 File Offset: 0x00083ED8
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._siegeDeploymentHandler = base.Mission.GetMissionBehavior<SiegeDeploymentHandler>();
			this.MissionAgentSpawnLogic = base.Mission.GetMissionBehavior<DefaultBattleMissionAgentSpawnLogic>();
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00085D04 File Offset: 0x00083F04
		public List<ItemObject> GetSiegeMissiles()
		{
			List<ItemObject> list = new List<ItemObject>();
			foreach (WeakGameEntity weakGameEntity in Mission.Current.GetActiveEntitiesWithScriptComponentOfType<RangedSiegeWeapon>())
			{
				RangedSiegeWeapon firstScriptOfType = weakGameEntity.GetFirstScriptOfType<RangedSiegeWeapon>();
				if (!string.IsNullOrEmpty(firstScriptOfType.MissileItemID))
				{
					ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MissileItemID);
					if (!list.Contains(@object))
					{
						list.Add(@object);
					}
				}
				foreach (ItemObject itemObject in new List<ItemObject>
				{
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MultipleProjectileId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MultipleProjectileFlyingId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MultipleFireProjectileId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.MultipleFireProjectileFlyingId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.SingleProjectileId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.SingleProjectileFlyingId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.SingleFireProjectileId),
					MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType.SingleFireProjectileFlyingId)
				})
				{
					if (!list.Contains(itemObject))
					{
						list.Add(itemObject);
					}
				}
			}
			foreach (WeakGameEntity weakGameEntity2 in Mission.Current.GetActiveEntitiesWithScriptComponentOfType<StonePile>())
			{
				StonePile firstScriptOfType2 = weakGameEntity2.GetFirstScriptOfType<StonePile>();
				if (!string.IsNullOrEmpty(firstScriptOfType2.GivenItemID))
				{
					ItemObject object2 = MBObjectManager.Instance.GetObject<ItemObject>(firstScriptOfType2.GivenItemID);
					if (!list.Contains(object2))
					{
						list.Add(object2);
					}
				}
			}
			return list;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00085F2C File Offset: 0x0008412C
		protected override void OnAfterStart()
		{
			this._siegeDeploymentHandler.InitializeDeploymentPoints();
			for (int i = 0; i < 2; i++)
			{
				this.MissionAgentSpawnLogic.SetSpawnTroops((BattleSideEnum)i, false, false);
			}
			this.MissionAgentSpawnLogic.SetReinforcementsSpawnEnabled(false, true);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00085F6C File Offset: 0x0008416C
		protected override void OnSetupTeamsOfSide(BattleSideEnum battleSide)
		{
			foreach (Team team in base.Mission.Teams)
			{
				if (team.Side == battleSide && team.GeneralAgent != null && team.GeneralAgent != Agent.Main)
				{
					team.GeneralAgent.SetDetachableFromFormation(false);
				}
			}
			Team team2 = ((battleSide == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam : base.Mission.DefenderTeam);
			if (team2 == base.Mission.PlayerTeam)
			{
				this._siegeDeploymentHandler.RemoveUnavailableDeploymentPoints(battleSide);
				this._siegeDeploymentHandler.UnHideDeploymentPoints(battleSide);
				this._siegeDeploymentHandler.DeployAllSiegeWeaponsOfPlayer();
			}
			else
			{
				this._siegeDeploymentHandler.DeployAllSiegeWeaponsOfAi();
			}
			this.MissionAgentSpawnLogic.SetSpawnTroops(battleSide, true, true);
			foreach (WeakGameEntity weakGameEntity in base.Mission.GetActiveEntitiesWithScriptComponentOfType<SiegeWeapon>())
			{
				SiegeWeapon firstScriptOfType = weakGameEntity.GetFirstScriptOfType<SiegeWeapon>();
				if (firstScriptOfType != null && firstScriptOfType.GetSide() == battleSide)
				{
					firstScriptOfType.TickAuxForInit();
				}
			}
			base.SetupAgentAIStatesForSide(battleSide);
			if (team2 == base.Mission.PlayerTeam)
			{
				foreach (Formation formation in team2.FormationsIncludingEmpty)
				{
					formation.SetControlledByAI(true, false);
				}
			}
			this.MissionAgentSpawnLogic.OnSideDeploymentOver(team2.Side);
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00086114 File Offset: 0x00084314
		protected override void OnSetupTeamsFinished()
		{
			base.Mission.IsTeleportingAgents = true;
			Agent main = Agent.Main;
			if (main != null)
			{
				Team team = main.Team;
				if (team != null)
				{
					if (base.Mission.DeploymentPlan.HasPlayerSpawnFrame(team.Side))
					{
						WorldPosition worldPosition;
						Vec2 vec;
						base.Mission.DeploymentPlan.GetPlayerSpawnFrame(team.Side, out worldPosition, out vec);
						if (worldPosition.GetNavMesh() != UIntPtr.Zero && worldPosition.IsValid)
						{
							Agent.Main.TrySetFormationFrame(in worldPosition, in vec);
							return;
						}
					}
					else if (team.GeneralAgent == main)
					{
						WorldPosition worldPosition2;
						Vec2 vec2;
						base.Mission.GetFormationSpawnFrame(team, FormationClass.NumberOfRegularFormations, false, out worldPosition2, out vec2, true);
						if (worldPosition2.GetNavMesh() != UIntPtr.Zero && worldPosition2.IsValid)
						{
							main.TrySetFormationFrame(in worldPosition2, in vec2);
						}
					}
				}
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000861E8 File Offset: 0x000843E8
		protected override void BeforeDeploymentFinished()
		{
			BattleSideEnum side = base.Mission.PlayerTeam.Side;
			this._siegeDeploymentHandler.RemoveDeploymentPoints(side);
			foreach (SiegeLadder siegeLadder in (from sl in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeLadder>()
				where !sl.GameEntity.IsVisibleIncludeParents()
				select sl).ToList<SiegeLadder>())
			{
				siegeLadder.SetDisabledSynched();
			}
			foreach (Team team in base.Mission.Teams)
			{
				if (team.GeneralAgent != null && team.GeneralAgent != Agent.Main)
				{
					team.GeneralAgent.SetDetachableFromFormation(true);
				}
			}
			base.Mission.IsTeleportingAgents = false;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000862F4 File Offset: 0x000844F4
		protected override void AfterDeploymentFinished()
		{
			this.MissionAgentSpawnLogic.SetReinforcementsSpawnEnabled(true, true);
			base.Mission.RemoveMissionBehavior(this._siegeDeploymentHandler);
		}

		// Token: 0x04000E4B RID: 3659
		protected DefaultBattleMissionAgentSpawnLogic MissionAgentSpawnLogic;

		// Token: 0x04000E4C RID: 3660
		private SiegeDeploymentHandler _siegeDeploymentHandler;
	}
}
