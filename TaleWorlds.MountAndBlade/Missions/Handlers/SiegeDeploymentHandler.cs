using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.AI;

namespace TaleWorlds.MountAndBlade.Missions.Handlers
{
	// Token: 0x020003ED RID: 1005
	public class SiegeDeploymentHandler : BattleDeploymentHandler
	{
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x000E3CC8 File Offset: 0x000E1EC8
		// (set) Token: 0x0600371B RID: 14107 RVA: 0x000E3CD0 File Offset: 0x000E1ED0
		public IEnumerable<DeploymentPoint> PlayerDeploymentPoints { get; private set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000E3CD9 File Offset: 0x000E1ED9
		// (set) Token: 0x0600371D RID: 14109 RVA: 0x000E3CE1 File Offset: 0x000E1EE1
		public IEnumerable<DeploymentPoint> AllDeploymentPoints { get; private set; }

		// Token: 0x0600371E RID: 14110 RVA: 0x000E3CEA File Offset: 0x000E1EEA
		public SiegeDeploymentHandler(bool isPlayerAttacker)
			: base(isPlayerAttacker)
		{
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000E3CF4 File Offset: 0x000E1EF4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MissionSiegeEnginesLogic missionBehavior = base.Mission.GetMissionBehavior<MissionSiegeEnginesLogic>();
			this._defenderSiegeWeaponsController = missionBehavior.GetSiegeWeaponsController(BattleSideEnum.Defender);
			this._attackerSiegeWeaponsController = missionBehavior.GetSiegeWeaponsController(BattleSideEnum.Attacker);
			this._defenderReferencePosition = WorldPosition.Invalid;
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000E3D38 File Offset: 0x000E1F38
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			base.Mission.IsFormationUnitPositionAvailable_AdditionalCondition -= this.Mission_IsFormationUnitPositionAvailable_AdditionalCondition;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000E3D58 File Offset: 0x000E1F58
		public override void AfterStart()
		{
			base.AfterStart();
			this.AllDeploymentPoints = Mission.Current.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			this.PlayerDeploymentPoints = this.AllDeploymentPoints.Where<DeploymentPoint>((DeploymentPoint dp) => dp.Side == base.PlayerTeam.Side);
			foreach (DeploymentPoint deploymentPoint in this.AllDeploymentPoints)
			{
				deploymentPoint.OnDeploymentStateChanged += this.OnDeploymentStateChange;
			}
			base.Mission.IsFormationUnitPositionAvailable_AdditionalCondition += this.Mission_IsFormationUnitPositionAvailable_AdditionalCondition;
			foreach (DeploymentPoint deploymentPoint2 in this.PlayerDeploymentPoints)
			{
				deploymentPoint2.OnDeployOrDisband += this.OnWeaponDeployOrDisband;
			}
			base.Mission.PlayerTeam.OnFormationsChangedInDeployment += this.OnFormationsChanged;
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000E3E60 File Offset: 0x000E2060
		private void OnWeaponDeployOrDisband(DeploymentPoint deploymentPoint)
		{
			if (deploymentPoint.IsDeployed)
			{
				SiegeWeapon siegeWeapon = deploymentPoint.DeployedWeapon as SiegeWeapon;
				if (siegeWeapon != null)
				{
					siegeWeapon.TickAuxForInit();
				}
				this.AutoAssignDetachmentsForDeployment(base.PlayerTeam);
			}
			using (List<Formation>.Enumerator enumerator = base.PlayerTeam.FormationsIncludingEmpty.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.ForceUpdateCachedAndFormationValues(false, false);
					}, null);
				}
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000E3F00 File Offset: 0x000E2100
		public override void FinishDeployment()
		{
			foreach (DeploymentPoint deploymentPoint in this.AllDeploymentPoints)
			{
				deploymentPoint.OnDeploymentStateChanged -= this.OnDeploymentStateChange;
			}
			foreach (DeploymentPoint deploymentPoint2 in this.PlayerDeploymentPoints)
			{
				deploymentPoint2.OnDeployOrDisband -= this.OnWeaponDeployOrDisband;
			}
			base.Mission.PlayerTeam.OnFormationsChangedInDeployment -= this.OnFormationsChanged;
			base.FinishDeployment();
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000E3FC0 File Offset: 0x000E21C0
		public void DeployAllSiegeWeaponsOfPlayer()
		{
			BattleSideEnum side = (this.IsPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
			new SiegeWeaponAutoDeployer((from dp in base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>()
				where dp.Side == side
				select dp).ToList<DeploymentPoint>(), this.GetWeaponsControllerOfSide(side)).DeployAll(side);
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000E4027 File Offset: 0x000E2227
		public int GetMaxDeployableWeaponCountOfPlayer(Type weapon)
		{
			return this.GetWeaponsControllerOfSide(this.IsPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender).GetMaxDeployableWeaponCount(weapon);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000E4044 File Offset: 0x000E2244
		public void DeployAllSiegeWeaponsOfAi()
		{
			BattleSideEnum side = (this.IsPlayerAttacker ? BattleSideEnum.Defender : BattleSideEnum.Attacker);
			new SiegeWeaponAutoDeployer((from dp in base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>()
				where dp.Side == side
				select dp).ToList<DeploymentPoint>(), this.GetWeaponsControllerOfSide(side)).DeployAll(side);
			this.RemoveDeploymentPoints(side);
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000E40B8 File Offset: 0x000E22B8
		public void RemoveDeploymentPoints(BattleSideEnum side)
		{
			IEnumerable<DeploymentPoint> enumerable = base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			Func<DeploymentPoint, bool> <>9__0;
			Func<DeploymentPoint, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (DeploymentPoint dp) => dp.Side == side);
			}
			foreach (DeploymentPoint deploymentPoint in enumerable.Where<DeploymentPoint>(func).ToArray<DeploymentPoint>())
			{
				foreach (SynchedMissionObject synchedMissionObject in deploymentPoint.DeployableWeapons.ToArray<SynchedMissionObject>())
				{
					if (deploymentPoint.DeployedWeapon == null || !synchedMissionObject.GameEntity.IsVisibleIncludeParents())
					{
						SiegeWeapon siegeWeapon = synchedMissionObject as SiegeWeapon;
						if (siegeWeapon != null)
						{
							siegeWeapon.SetDisabledSynched();
						}
					}
				}
				deploymentPoint.SetDisabledSynched();
			}
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000E4180 File Offset: 0x000E2380
		public void RemoveUnavailableDeploymentPoints(BattleSideEnum side)
		{
			IMissionSiegeWeaponsController weapons = ((side == BattleSideEnum.Defender) ? this._defenderSiegeWeaponsController : this._attackerSiegeWeaponsController);
			IEnumerable<DeploymentPoint> enumerable = base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			Func<DeploymentPoint, bool> <>9__0;
			Func<DeploymentPoint, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (DeploymentPoint dp) => dp.Side == side);
			}
			Func<Type, bool> <>9__1;
			foreach (DeploymentPoint deploymentPoint in enumerable.Where<DeploymentPoint>(func).ToArray<DeploymentPoint>())
			{
				IEnumerable<Type> deployableWeaponTypes = deploymentPoint.DeployableWeaponTypes;
				Func<Type, bool> func2;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (Type wt) => weapons.GetMaxDeployableWeaponCount(wt) > 0);
				}
				if (!deployableWeaponTypes.Any<Type>(func2))
				{
					foreach (SiegeWeapon siegeWeapon in deploymentPoint.DeployableWeapons.Select<SynchedMissionObject, SiegeWeapon>((SynchedMissionObject sw) => sw as SiegeWeapon))
					{
						siegeWeapon.SetDisabledSynched();
					}
					deploymentPoint.SetDisabledSynched();
				}
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000E42A8 File Offset: 0x000E24A8
		public void UnHideDeploymentPoints(BattleSideEnum side)
		{
			IEnumerable<DeploymentPoint> enumerable = base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>();
			Func<DeploymentPoint, bool> <>9__0;
			Func<DeploymentPoint, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (DeploymentPoint dp) => !dp.IsDisabled && dp.Side == side);
			}
			foreach (DeploymentPoint deploymentPoint in enumerable.Where<DeploymentPoint>(func))
			{
				deploymentPoint.Show();
			}
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000E4330 File Offset: 0x000E2530
		public int GetDeployableWeaponCountOfPlayer(Type weapon)
		{
			return this.GetWeaponsControllerOfSide(this.IsPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender).GetMaxDeployableWeaponCount(weapon) - this.PlayerDeploymentPoints.Count<DeploymentPoint>((DeploymentPoint dp) => dp.IsDeployed && MissionSiegeWeaponsController.GetWeaponType(dp.DeployedWeapon) == weapon);
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000E4380 File Offset: 0x000E2580
		public void AutoDeployTeamUsingTeamAI(Team team, bool autoAssignDetachments = true)
		{
			List<Formation> list = team.FormationsIncludingEmpty.ToList<Formation>();
			bool allowAiTicking = base.Mission.AllowAiTicking;
			bool forceTickOccasionally = base.Mission.ForceTickOccasionally;
			bool isTeleportingAgents = base.Mission.IsTeleportingAgents;
			base.Mission.AllowAiTicking = true;
			base.Mission.ForceTickOccasionally = true;
			base.Mission.IsTeleportingAgents = true;
			OrderController orderController = (team.IsPlayerTeam ? team.PlayerOrderController : team.MasterOrderController);
			orderController.SelectAllFormations(false);
			base.SetDefaultFormationOrders(orderController);
			team.ResetTactic();
			team.Tick(0f);
			foreach (Formation formation in list)
			{
				formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					agent.ForceUpdateCachedAndFormationValues(false, false);
				}, null);
				formation.SetHasPendingUnitPositions(false);
			}
			orderController.ClearSelectedFormations();
			if (autoAssignDetachments)
			{
				this.AutoAssignDetachmentsForDeployment(team);
			}
			base.Mission.IsTeleportingAgents = isTeleportingAgents;
			base.Mission.ForceTickOccasionally = forceTickOccasionally;
			base.Mission.AllowAiTicking = allowAiTicking;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000E44B0 File Offset: 0x000E26B0
		public void AutoAssignDetachmentsForDeployment(Team team)
		{
			List<Formation> list = team.FormationsIncludingEmpty.ToList<Formation>();
			bool allowAiTicking = base.Mission.AllowAiTicking;
			bool isTeleportingAgents = base.Mission.IsTeleportingAgents;
			base.Mission.AllowAiTicking = true;
			base.Mission.IsTeleportingAgents = true;
			if (!team.DetachmentManager.Detachments.IsEmpty<ValueTuple<IDetachment, DetachmentData>>())
			{
				using (List<Formation>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						enumerator.Current.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							Formation formation2 = agent.Formation;
							if (formation2 == null)
							{
								return;
							}
							formation2.Team.DetachmentManager.TickAgent(agent);
						}, null);
					}
				}
				int num = 0;
				int num2 = 0;
				foreach (ValueTuple<IDetachment, DetachmentData> valueTuple in team.DetachmentManager.Detachments)
				{
					num += valueTuple.Item1.GetNumberOfUsableSlots();
				}
				foreach (Formation formation in team.FormationsIncludingEmpty)
				{
					num2 += formation.CountOfDetachableNonPlayerUnits;
				}
				for (int i = 0; i < MathF.Min(num, num2); i++)
				{
					team.DetachmentManager.TickDetachments();
				}
				using (List<Formation>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						enumerator.Current.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							if (agent.Detachment != null)
							{
								agent.ForceUpdateCachedAndFormationValues(false, false);
							}
						}, null);
					}
				}
			}
			base.Mission.IsTeleportingAgents = isTeleportingAgents;
			base.Mission.AllowAiTicking = allowAiTicking;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000E46A4 File Offset: 0x000E28A4
		private void OnFormationsChanged(Team team)
		{
			this.AutoAssignDetachmentsForDeployment(team);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000E46B0 File Offset: 0x000E28B0
		protected bool Mission_IsFormationUnitPositionAvailable_AdditionalCondition(WorldPosition position, Team team)
		{
			if (team != null && team.Side == BattleSideEnum.Defender)
			{
				Scene scene = base.Mission.Scene;
				if (!this._defenderReferencePosition.IsValid)
				{
					WeakGameEntity weakGameEntity = scene.FindWeakEntityWithTag("defender_infantry");
					this._defenderReferencePosition = new WorldPosition(scene, UIntPtr.Zero, weakGameEntity.GlobalPosition, false);
				}
				return scene.DoesPathExistBetweenPositions(this._defenderReferencePosition, position);
			}
			return true;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000E4718 File Offset: 0x000E2918
		private void OnDeploymentStateChange(DeploymentPoint deploymentPoint, SynchedMissionObject targetObject)
		{
			if (!deploymentPoint.IsDeployed && base.PlayerTeam.DetachmentManager.ContainsDetachment(deploymentPoint.DisbandedWeapon as IDetachment))
			{
				base.PlayerTeam.DetachmentManager.DestroyDetachment(deploymentPoint.DisbandedWeapon as IDetachment);
			}
			SiegeWeapon siegeWeapon;
			if ((siegeWeapon = targetObject as SiegeWeapon) != null)
			{
				IMissionSiegeWeaponsController weaponsControllerOfSide = this.GetWeaponsControllerOfSide(deploymentPoint.Side);
				if (deploymentPoint.IsDeployed)
				{
					weaponsControllerOfSide.OnWeaponDeployed(siegeWeapon);
					return;
				}
				weaponsControllerOfSide.OnWeaponUndeployed(siegeWeapon);
			}
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000E4793 File Offset: 0x000E2993
		private IMissionSiegeWeaponsController GetWeaponsControllerOfSide(BattleSideEnum side)
		{
			if (side != BattleSideEnum.Defender)
			{
				return this._attackerSiegeWeaponsController;
			}
			return this._defenderSiegeWeaponsController;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000E47A8 File Offset: 0x000E29A8
		public Vec2 GetEstimatedAverageDefenderPosition()
		{
			WorldPosition worldPosition;
			Vec2 vec;
			base.Mission.GetFormationSpawnFrame(Mission.Current.DefenderTeam, FormationClass.Infantry, false, out worldPosition, out vec, true);
			return worldPosition.AsVec2;
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000E47D8 File Offset: 0x000E29D8
		[Conditional("DEBUG")]
		private void AssertSiegeWeapons(IEnumerable<DeploymentPoint> allDeploymentPoints)
		{
			HashSet<SynchedMissionObject> hashSet = new HashSet<SynchedMissionObject>();
			foreach (SynchedMissionObject synchedMissionObject in allDeploymentPoints.SelectMany<DeploymentPoint, SynchedMissionObject>((DeploymentPoint amo) => amo.DeployableWeapons))
			{
				if (!hashSet.Add(synchedMissionObject))
				{
					break;
				}
			}
		}

		// Token: 0x040017B4 RID: 6068
		private IMissionSiegeWeaponsController _defenderSiegeWeaponsController;

		// Token: 0x040017B5 RID: 6069
		private IMissionSiegeWeaponsController _attackerSiegeWeaponsController;

		// Token: 0x040017B6 RID: 6070
		private WorldPosition _defenderReferencePosition;
	}
}
