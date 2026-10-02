using System;
using System.Diagnostics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000282 RID: 642
	public abstract class DeploymentMissionController : MissionLogic
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x00080668 File Offset: 0x0007E868
		// (set) Token: 0x060023CB RID: 9163 RVA: 0x00080670 File Offset: 0x0007E870
		public bool TeamSetupOver { get; private set; }

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060023CC RID: 9164 RVA: 0x0008067C File Offset: 0x0007E87C
		// (remove) Token: 0x060023CD RID: 9165 RVA: 0x000806B4 File Offset: 0x0007E8B4
		public event Action OnAfterSetupTeams;

		// Token: 0x060023CE RID: 9166 RVA: 0x000806E9 File Offset: 0x0007E8E9
		public DeploymentMissionController(bool isPlayerAttacker)
		{
			this.IsPlayerAttacker = isPlayerAttacker;
			this.PlayerSide = (this.IsPlayerAttacker ? BattleSideEnum.Attacker : BattleSideEnum.Defender);
			this.EnemySide = (this.IsPlayerAttacker ? BattleSideEnum.Defender : BattleSideEnum.Attacker);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0008071C File Offset: 0x0007E91C
		public override void AfterStart()
		{
			base.Mission.AllowAiTicking = false;
			this.OnAfterStart();
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00080730 File Offset: 0x0007E930
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.Mission.AreOrderGesturesEnabled_AdditionalCondition += this.AreOrderGesturesEnabled_AdditionalCondition;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00080750 File Offset: 0x0007E950
		public void FinishDeployment()
		{
			this.BeforeDeploymentFinished();
			if (this.IsPlayerAttacker)
			{
				this.UnhideAgentsOfSide(BattleSideEnum.Defender);
			}
			Mission.Current.OnDeploymentFinished();
			foreach (Team team in base.Mission.Teams)
			{
				foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						formation.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							if (agent.IsAIControlled)
							{
								agent.SetAlarmState(Agent.AIStateFlag.Alarmed);
								agent.SetIsAIPaused(false);
								if (agent.GetAgentFlags().HasAnyFlag(AgentFlag.CanWieldWeapon))
								{
									agent.ResetEnemyCaches();
								}
								HumanAIComponent humanAIComponent = agent.HumanAIComponent;
								if (humanAIComponent == null)
								{
									return;
								}
								humanAIComponent.SyncBehaviorParamsIfNecessary();
							}
						}, null);
					}
				}
			}
			Agent mainAgent = base.Mission.MainAgent;
			if (mainAgent != null)
			{
				mainAgent.SetDetachableFromFormation(true);
				mainAgent.Controller = AgentControllerType.Player;
			}
			base.Mission.AllowAiTicking = true;
			base.Mission.DisableDying = false;
			base.Mission.SetFallAvoidSystemActive(false);
			Mission.Current.OnAfterDeploymentFinished();
			this.AfterDeploymentFinished();
			base.Mission.RemoveMissionBehavior(this);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00080888 File Offset: 0x0007EA88
		public override void OnAgentControllerSetToPlayer(Agent agent)
		{
			if (!this.IsPlayerControllerSetToNone)
			{
				agent.Controller = AgentControllerType.None;
				agent.SetIsAIPaused(true);
				agent.SetDetachableFromFormation(false);
				this.IsPlayerControllerSetToNone = true;
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000808B0 File Offset: 0x0007EAB0
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (!this.TeamSetupOver && base.Mission.Scene != null)
			{
				this.SetupTeams();
				this.TeamSetupOver = true;
			}
			if (this.TeamSetupOver && !this.AfterSetupTeamsCalled)
			{
				Action onAfterSetupTeams = this.OnAfterSetupTeams;
				if (onAfterSetupTeams != null)
				{
					onAfterSetupTeams();
				}
				this.AfterSetupTeamsCalled = true;
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00080914 File Offset: 0x0007EB14
		protected void SetupAgentAIStatesForSide(BattleSideEnum battleSide)
		{
			foreach (Team team in Mission.GetTeamsOfSide(battleSide))
			{
				foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
				{
					if (formation.CountOfUnits > 0)
					{
						formation.ApplyActionOnEachUnit(delegate(Agent agent)
						{
							if (agent.IsAIControlled)
							{
								agent.SetAlarmState(Agent.AIStateFlag.None);
								agent.SetIsAIPaused(true);
							}
						}, null);
					}
				}
			}
		}

		// Token: 0x060023D5 RID: 9173
		protected abstract void OnAfterStart();

		// Token: 0x060023D6 RID: 9174
		protected abstract void OnSetupTeamsOfSide(BattleSideEnum side);

		// Token: 0x060023D7 RID: 9175
		protected abstract void OnSetupTeamsFinished();

		// Token: 0x060023D8 RID: 9176
		protected abstract void BeforeDeploymentFinished();

		// Token: 0x060023D9 RID: 9177
		protected abstract void AfterDeploymentFinished();

		// Token: 0x060023DA RID: 9178 RVA: 0x000809C4 File Offset: 0x0007EBC4
		protected virtual void SetupAIOfEnemySide(BattleSideEnum enemySide)
		{
			Team team = ((enemySide == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam : base.Mission.DefenderTeam);
			this.SetupAIOfEnemyTeam(team);
			Team team2 = ((enemySide == BattleSideEnum.Attacker) ? base.Mission.AttackerAllyTeam : base.Mission.DefenderAllyTeam);
			if (team2 != null)
			{
				this.SetupAIOfEnemyTeam(team2);
			}
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x00080A1C File Offset: 0x0007EC1C
		protected virtual void SetupAIOfEnemyTeam(Team team)
		{
			foreach (Formation formation in team.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.SetControlledByAI(true, false);
				}
			}
			team.QuerySystem.Expire();
			base.Mission.AllowAiTicking = true;
			base.Mission.ForceTickOccasionally = true;
			team.ResetTactic();
			bool isTeleportingAgents = Mission.Current.IsTeleportingAgents;
			base.Mission.IsTeleportingAgents = true;
			team.Tick(0f);
			base.Mission.IsTeleportingAgents = isTeleportingAgents;
			base.Mission.AllowAiTicking = false;
			base.Mission.ForceTickOccasionally = false;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00080AE8 File Offset: 0x0007ECE8
		private void SetupTeams()
		{
			Utilities.SetLoadingScreenPercentage(0.92f);
			base.Mission.DisableDying = true;
			base.Mission.SetFallAvoidSystemActive(true);
			this.OnSetupTeamsOfSide(this.EnemySide);
			this.SetupAIOfEnemySide(this.EnemySide);
			if (this.IsPlayerAttacker)
			{
				this.HideAgentsOfSide(BattleSideEnum.Defender);
			}
			this.OnSetupTeamsOfSide(this.PlayerSide);
			this.OnSetupTeamsFinished();
			base.Mission.AreOrderGesturesEnabled_AdditionalCondition -= this.AreOrderGesturesEnabled_AdditionalCondition;
			Utilities.SetLoadingScreenPercentage(0.96f);
			if (!MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
			{
				this.FinishDeployment();
			}
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00080B88 File Offset: 0x0007ED88
		private void HideAgentsOfSide(BattleSideEnum side)
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman && agent.Team != null && agent.Team.Side == side)
				{
					agent.SetRenderCheckEnabled(false);
					agent.AgentVisuals.SetVisible(false);
					Agent mountAgent = agent.MountAgent;
					if (mountAgent != null)
					{
						mountAgent.SetRenderCheckEnabled(false);
					}
					Agent mountAgent2 = agent.MountAgent;
					if (mountAgent2 != null)
					{
						mountAgent2.AgentVisuals.SetVisible(false);
					}
				}
			}
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00080C34 File Offset: 0x0007EE34
		private void UnhideAgentsOfSide(BattleSideEnum side)
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman && agent.Team != null && agent.Team.Side == side)
				{
					agent.SetRenderCheckEnabled(true);
					agent.AgentVisuals.SetVisible(true);
					Agent mountAgent = agent.MountAgent;
					if (mountAgent != null)
					{
						mountAgent.SetRenderCheckEnabled(true);
					}
					Agent mountAgent2 = agent.MountAgent;
					if (mountAgent2 != null)
					{
						mountAgent2.AgentVisuals.SetVisible(true);
					}
				}
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00080CE0 File Offset: 0x0007EEE0
		private bool AreOrderGesturesEnabled_AdditionalCondition()
		{
			return false;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00080CE3 File Offset: 0x0007EEE3
		[Conditional("DEBUG")]
		private void DebugTick()
		{
			if (Input.DebugInput.IsHotKeyPressed("SwapToEnemy"))
			{
				base.Mission.MainAgent.Controller = AgentControllerType.AI;
				base.Mission.PlayerEnemyTeam.Leader.Controller = AgentControllerType.Player;
				this.SwapTeams();
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00080D23 File Offset: 0x0007EF23
		private void SwapTeams()
		{
			base.Mission.PlayerTeam = base.Mission.PlayerEnemyTeam;
		}

		// Token: 0x04000DBD RID: 3517
		protected readonly bool IsPlayerAttacker;

		// Token: 0x04000DBE RID: 3518
		protected bool IsPlayerControllerSetToNone;

		// Token: 0x04000DBF RID: 3519
		protected BattleSideEnum PlayerSide;

		// Token: 0x04000DC0 RID: 3520
		protected BattleSideEnum EnemySide;

		// Token: 0x04000DC1 RID: 3521
		protected bool AfterSetupTeamsCalled;
	}
}
