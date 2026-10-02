using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000275 RID: 629
	public class BattleDeploymentMissionController : DeploymentMissionController
	{
		// Token: 0x06002337 RID: 9015 RVA: 0x0007CE9B File Offset: 0x0007B09B
		public BattleDeploymentMissionController(bool isPlayerAttacker)
			: base(isPlayerAttacker)
		{
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0007CEA4 File Offset: 0x0007B0A4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._battleDeploymentHandler = base.Mission.GetMissionBehavior<BattleDeploymentHandler>();
			this.MissionAgentSpawnLogic = base.Mission.GetMissionBehavior<DefaultBattleMissionAgentSpawnLogic>();
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0007CECE File Offset: 0x0007B0CE
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0007CED8 File Offset: 0x0007B0D8
		protected override void OnAfterStart()
		{
			for (int i = 0; i < 2; i++)
			{
				this.MissionAgentSpawnLogic.SetSpawnTroops((BattleSideEnum)i, false, false);
			}
			this.MissionAgentSpawnLogic.SetReinforcementsSpawnEnabled(false, true);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0007CF0C File Offset: 0x0007B10C
		protected override void OnSetupTeamsOfSide(BattleSideEnum battleSide)
		{
			this.MissionAgentSpawnLogic.SetSpawnTroops(battleSide, true, true);
			base.SetupAgentAIStatesForSide(battleSide);
			this.MissionAgentSpawnLogic.OnSideDeploymentOver(battleSide);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0007CF30 File Offset: 0x0007B130
		protected override void OnSetupTeamsFinished()
		{
			base.Mission.IsTeleportingAgents = true;
			foreach (Team team in base.Mission.Teams)
			{
				if (team.GeneralAgent != null)
				{
					WorldPosition worldPosition;
					Vec2 vec;
					base.Mission.GetFormationSpawnFrame(team, FormationClass.NumberOfRegularFormations, false, out worldPosition, out vec, true);
					if (worldPosition.GetNavMesh() != UIntPtr.Zero && worldPosition.IsValid)
					{
						team.GeneralAgent.TrySetFormationFrame(in worldPosition, in vec);
					}
				}
			}
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
		protected override void BeforeDeploymentFinished()
		{
			base.Mission.IsTeleportingAgents = false;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0007CFE2 File Offset: 0x0007B1E2
		protected override void AfterDeploymentFinished()
		{
			this.MissionAgentSpawnLogic.SetReinforcementsSpawnEnabled(true, true);
			base.Mission.RemoveMissionBehavior(this._battleDeploymentHandler);
		}

		// Token: 0x04000D80 RID: 3456
		protected DefaultBattleMissionAgentSpawnLogic MissionAgentSpawnLogic;

		// Token: 0x04000D81 RID: 3457
		private BattleDeploymentHandler _battleDeploymentHandler;
	}
}
