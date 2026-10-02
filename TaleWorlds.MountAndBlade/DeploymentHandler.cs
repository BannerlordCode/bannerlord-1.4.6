using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000281 RID: 641
	public abstract class DeploymentHandler : MissionLogic
	{
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060023B8 RID: 9144 RVA: 0x000800F8 File Offset: 0x0007E2F8
		// (remove) Token: 0x060023B9 RID: 9145 RVA: 0x00080130 File Offset: 0x0007E330
		public event Action OnPlayerSideDeploymentReady;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060023BA RID: 9146 RVA: 0x00080168 File Offset: 0x0007E368
		// (remove) Token: 0x060023BB RID: 9147 RVA: 0x000801A0 File Offset: 0x0007E3A0
		public event Action OnEnemySideDeploymentReady;

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000801D5 File Offset: 0x0007E3D5
		public Team PlayerTeam
		{
			get
			{
				return base.Mission.PlayerTeam;
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000801E2 File Offset: 0x0007E3E2
		public DeploymentHandler(bool isPlayerAttacker)
		{
			this.IsPlayerAttacker = isPlayerAttacker;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000801F1 File Offset: 0x0007E3F1
		public override void OnBehaviorInitialize()
		{
			this._deploymentMissionController = base.Mission.GetMissionBehavior<DeploymentMissionController>();
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x00080204 File Offset: 0x0007E404
		public override void EarlyStart()
		{
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00080206 File Offset: 0x0007E406
		public override void AfterStart()
		{
			this.PreviousMissionMode = base.Mission.Mode;
			base.Mission.SetMissionMode(MissionMode.Deployment, true);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x00080226 File Offset: 0x0007E426
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			base.Mission.SetMissionMode(this.PreviousMissionMode, false);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00080240 File Offset: 0x0007E440
		public override void OnBattleSideDeployed(BattleSideEnum side)
		{
			if (side == base.Mission.PlayerTeam.Side)
			{
				Action onPlayerSideDeploymentReady = this.OnPlayerSideDeploymentReady;
				if (onPlayerSideDeploymentReady == null)
				{
					return;
				}
				onPlayerSideDeploymentReady();
				return;
			}
			else
			{
				Action onEnemySideDeploymentReady = this.OnEnemySideDeploymentReady;
				if (onEnemySideDeploymentReady == null)
				{
					return;
				}
				onEnemySideDeploymentReady();
				return;
			}
		}

		// Token: 0x060023C3 RID: 9155
		public abstract void AutoDeployTeamUsingDeploymentPlan(Team playerTeam);

		// Token: 0x060023C4 RID: 9156
		public abstract void ForceUpdateAllUnits();

		// Token: 0x060023C5 RID: 9157 RVA: 0x00080276 File Offset: 0x0007E476
		public virtual void FinishDeployment()
		{
			Mission mission = base.Mission ?? Mission.Current;
			this._deploymentMissionController.FinishDeployment();
			mission.IsTeleportingAgents = false;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00080298 File Offset: 0x0007E498
		public void InitializeDeploymentPoints()
		{
			if (!this._areDeploymentPointsInitialized)
			{
				foreach (DeploymentPoint deploymentPoint in base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>())
				{
					deploymentPoint.Hide();
				}
				this._areDeploymentPointsInitialized = true;
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000802FC File Offset: 0x0007E4FC
		public static void OrderController_OnOrderIssued_Aux(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController = null, params object[] delegateParams)
		{
			DeploymentHandler.<>c__DisplayClass22_0 CS$<>8__locals1;
			CS$<>8__locals1.appliedFormations = appliedFormations;
			CS$<>8__locals1.orderController = orderController;
			bool flag = false;
			using (List<Formation>.Enumerator enumerator = CS$<>8__locals1.appliedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CountOfUnits > 0)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			switch (orderType)
			{
			case OrderType.None:
				Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\DeploymentHandler.cs", "OrderController_OnOrderIssued_Aux", 158);
				return;
			case OrderType.Move:
			case OrderType.MoveToLineSegment:
			case OrderType.MoveToLineSegmentWithHorizontalLayout:
			case OrderType.FollowMe:
			case OrderType.FollowEntity:
			case OrderType.Advance:
			case OrderType.FallBack:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.Charge:
			case OrderType.ChargeWithTarget:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.StandYourGround:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.Retreat:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.LookAtEnemy:
			case OrderType.LookAtDirection:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.ArrangementLine:
			case OrderType.ArrangementCloseOrder:
			case OrderType.ArrangementLoose:
			case OrderType.ArrangementCircular:
			case OrderType.ArrangementSchiltron:
			case OrderType.ArrangementVee:
			case OrderType.ArrangementColumn:
			case OrderType.ArrangementScatter:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.FormCustom:
			case OrderType.FormDeep:
			case OrderType.FormWide:
			case OrderType.FormWider:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.CohesionHigh:
			case OrderType.CohesionMedium:
			case OrderType.CohesionLow:
			case OrderType.HoldFire:
			case OrderType.FireAtWill:
				return;
			case OrderType.Mount:
			case OrderType.Dismount:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.AIControlOn:
			case OrderType.AIControlOff:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.Transfer:
			case OrderType.Use:
			case OrderType.AttackEntity:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref CS$<>8__locals1);
				return;
			case OrderType.PointDefence:
				Debug.FailedAssert("will be removed", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\DeploymentHandler.cs", "OrderController_OnOrderIssued_Aux", 230);
				return;
			}
			Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\DeploymentHandler.cs", "OrderController_OnOrderIssued_Aux", 233);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000804E4 File Offset: 0x0007E6E4
		[CompilerGenerated]
		internal unsafe static void <OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|22_0(ref DeploymentHandler.<>c__DisplayClass22_0 A_0)
		{
			foreach (Formation formation in A_0.appliedFormations)
			{
				if (formation.CountOfUnits > 0 && (A_0.orderController == null || A_0.orderController.FormationUpdateEnabledAfterSetOrder))
				{
					MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
					bool flag = false;
					if (formation.IsPlayerTroopInFormation)
					{
						flag = movementOrder.OrderEnum == MovementOrder.MovementOrderEnum.Follow;
					}
					bool flag2 = movementOrder.OrderEnum == MovementOrder.MovementOrderEnum.Stop;
					OrderController.TryCancelStopOrder(formation);
					formation.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.ForceUpdateCachedAndFormationValues(true, false);
					}, flag ? Mission.Current.MainAgent : null);
					formation.SetHasPendingUnitPositions(false);
					if (flag2)
					{
						formation.SetMovementOrder(MovementOrder.MovementOrderStop);
					}
				}
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000805D0 File Offset: 0x0007E7D0
		[CompilerGenerated]
		internal unsafe static void <OrderController_OnOrderIssued_Aux>g__ForcePositioning|22_1(ref DeploymentHandler.<>c__DisplayClass22_0 A_0)
		{
			foreach (Formation formation in A_0.appliedFormations)
			{
				if (formation.CountOfUnits > 0)
				{
					Vec2 direction = formation.FacingOrder.GetDirection(formation, null);
					Formation formation2 = formation;
					MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
					formation2.SetPositioning(new WorldPosition?(movementOrder.CreateNewOrderWorldPositionMT(formation, WorldPosition.WorldPositionEnforcedCache.None)), new Vec2?(direction), null);
				}
			}
		}

		// Token: 0x04000DB8 RID: 3512
		protected MissionMode PreviousMissionMode;

		// Token: 0x04000DB9 RID: 3513
		protected readonly bool IsPlayerAttacker;

		// Token: 0x04000DBA RID: 3514
		protected DeploymentMissionController _deploymentMissionController;

		// Token: 0x04000DBB RID: 3515
		private bool _areDeploymentPointsInitialized;
	}
}
