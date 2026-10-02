using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000272 RID: 626
	public class AssignPlayerRoleInTeamMissionController : MissionLogic
	{
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002300 RID: 8960 RVA: 0x0007BF10 File Offset: 0x0007A110
		// (remove) Token: 0x06002301 RID: 8961 RVA: 0x0007BF48 File Offset: 0x0007A148
		public event PlayerTurnToChooseFormationToLeadEvent OnPlayerTurnToChooseFormationToLead;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002302 RID: 8962 RVA: 0x0007BF80 File Offset: 0x0007A180
		// (remove) Token: 0x06002303 RID: 8963 RVA: 0x0007BFB8 File Offset: 0x0007A1B8
		public event AllFormationsAssignedSergeantsEvent OnAllFormationsAssignedSergeants;

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x0007BFED File Offset: 0x0007A1ED
		public bool IsPlayerInArmy { get; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0007BFF5 File Offset: 0x0007A1F5
		public bool IsPlayerGeneral { get; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0007BFFD File Offset: 0x0007A1FD
		public bool IsPlayerSergeant { get; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0007C005 File Offset: 0x0007A205
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x0007C00D File Offset: 0x0007A20D
		public int PlayerChosenIndex { get; protected set; }

		// Token: 0x06002309 RID: 8969 RVA: 0x0007C016 File Offset: 0x0007A216
		public AssignPlayerRoleInTeamMissionController(bool isPlayerGeneral, bool isPlayerSergeant, bool isPlayerInArmy, List<string> charactersInPlayerSideByPriority = null)
		{
			this.IsPlayerGeneral = isPlayerGeneral;
			this.IsPlayerSergeant = isPlayerSergeant;
			this.IsPlayerInArmy = isPlayerInArmy;
			this.PlayerChosenIndex = -1;
			this.CharactersInPlayerSideByPriority = charactersInPlayerSideByPriority;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0007C042 File Offset: 0x0007A242
		public override void AfterStart()
		{
			Mission.Current.PlayerTeam.SetPlayerRole(this.IsPlayerGeneral, this.IsPlayerSergeant);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0007C060 File Offset: 0x0007A260
		public override void OnTeamDeployed(Team team)
		{
			base.OnTeamDeployed(team);
			if (team == base.Mission.PlayerTeam)
			{
				team.PlayerOrderController.Owner = Agent.Main;
				if (team.IsPlayerGeneral)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						formation.PlayerOwner = Agent.Main;
					}
				}
				team.PlayerOrderController.SelectAllFormations(false);
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0007C0F0 File Offset: 0x0007A2F0
		public virtual void OnPlayerTeamDeployed()
		{
			if (MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
			{
				Team playerTeam = Mission.Current.PlayerTeam;
				this.FormationsLockedWithSergeants = new Dictionary<int, Agent>();
				this.FormationsWithLooselyChosenSergeants = new Dictionary<int, Agent>();
				if (playerTeam.IsPlayerGeneral)
				{
					this.CharacterNamesInPlayerSideByPriorityQueue = new Queue<string>();
					this.RemainingFormationsToAssignSergeantsTo = new List<Formation>();
				}
				else
				{
					this.CharacterNamesInPlayerSideByPriorityQueue = ((this.CharactersInPlayerSideByPriority != null) ? new Queue<string>(this.CharactersInPlayerSideByPriority) : new Queue<string>());
					this.RemainingFormationsToAssignSergeantsTo = playerTeam.FormationsIncludingSpecialAndEmpty.WhereQ<Formation>((Formation f) => f.CountOfUnits > 0).ToList<Formation>();
					while (this.RemainingFormationsToAssignSergeantsTo.Count > 0 && this.CharacterNamesInPlayerSideByPriorityQueue.Count > 0)
					{
						string nextAgentNameToProcess = this.CharacterNamesInPlayerSideByPriorityQueue.Dequeue();
						Agent agent = playerTeam.ActiveAgents.FirstOrDefault<Agent>((Agent aa) => aa.Character.StringId.Equals(nextAgentNameToProcess));
						if (agent != null)
						{
							if (agent == Agent.Main)
							{
								break;
							}
							Formation formation = this.ChooseFormationToLead(this.RemainingFormationsToAssignSergeantsTo, agent);
							if (formation != null)
							{
								this.FormationsLockedWithSergeants.Add(formation.Index, agent);
								this.RemainingFormationsToAssignSergeantsTo.Remove(formation);
							}
						}
					}
				}
				PlayerTurnToChooseFormationToLeadEvent onPlayerTurnToChooseFormationToLead = this.OnPlayerTurnToChooseFormationToLead;
				if (onPlayerTurnToChooseFormationToLead == null)
				{
					return;
				}
				onPlayerTurnToChooseFormationToLead(this.FormationsLockedWithSergeants, this.RemainingFormationsToAssignSergeantsTo.Select<Formation, int>((Formation ftcsf) => ftcsf.Index).ToList<int>());
			}
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0007C27C File Offset: 0x0007A47C
		public virtual void OnPlayerChoiceMade(int chosenIndex)
		{
			if (this.PlayerChosenIndex != chosenIndex)
			{
				this.PlayerChosenIndex = chosenIndex;
				this.FormationsWithLooselyChosenSergeants.Clear();
				List<Formation> list = base.Mission.PlayerTeam.FormationsIncludingEmpty.WhereQ<Formation>((Formation f) => f.CountOfUnits > 0 && !this.FormationsLockedWithSergeants.ContainsKey(f.Index)).ToList<Formation>();
				if (chosenIndex != -1)
				{
					Formation formation = list.FirstOrDefault<Formation>((Formation fr) => fr.Index == chosenIndex);
					this.FormationsWithLooselyChosenSergeants.Add(chosenIndex, base.Mission.PlayerTeam.PlayerOrderController.Owner);
					list.Remove(formation);
				}
				Queue<string> queue = new Queue<string>(this.CharacterNamesInPlayerSideByPriorityQueue);
				while (list.Count > 0 && queue.Count > 0)
				{
					string nextAgentNameToProcess = queue.Dequeue();
					Agent agent = base.Mission.PlayerTeam.ActiveAgents.FirstOrDefault<Agent>((Agent aa) => aa.Character.StringId.Equals(nextAgentNameToProcess));
					if (agent != null)
					{
						Formation formation2 = this.ChooseFormationToLead(list, agent);
						if (formation2 != null)
						{
							this.FormationsWithLooselyChosenSergeants.Add(formation2.Index, agent);
							list.Remove(formation2);
						}
					}
				}
				if (this.OnAllFormationsAssignedSergeants != null)
				{
					this.OnAllFormationsAssignedSergeants(this.FormationsWithLooselyChosenSergeants);
				}
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0007C3D8 File Offset: 0x0007A5D8
		public void OnPlayerChoiceFinalized()
		{
			foreach (KeyValuePair<int, Agent> keyValuePair in this.FormationsLockedWithSergeants)
			{
				this.AssignSergeant(keyValuePair.Value.Team.GetFormation((FormationClass)keyValuePair.Key), keyValuePair.Value);
			}
			foreach (KeyValuePair<int, Agent> keyValuePair2 in this.FormationsWithLooselyChosenSergeants)
			{
				this.AssignSergeant(keyValuePair2.Value.Team.GetFormation((FormationClass)keyValuePair2.Key), keyValuePair2.Value);
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0007C4AC File Offset: 0x0007A6AC
		protected virtual void AssignSergeant(Formation formationToLead, Agent sergeant)
		{
			sergeant.Formation = formationToLead;
			if (!sergeant.IsAIControlled || sergeant == Agent.Main)
			{
				formationToLead.PlayerOwner = sergeant;
			}
			formationToLead.Captain = sergeant;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0007C4D4 File Offset: 0x0007A6D4
		private Formation ChooseFormationToLead(IEnumerable<Formation> formationsToChooseFrom, Agent agent)
		{
			bool hasMount = agent.HasMount;
			bool flag = agent.HasRangedWeapon(false);
			List<Formation> list = formationsToChooseFrom.ToList<Formation>();
			while (list.Count > 0)
			{
				Formation formation = list.MaxBy<Formation, float>((Formation ftcf) => ftcf.QuerySystem.FormationPower);
				list.Remove(formation);
				if ((flag || (!formation.QuerySystem.IsRangedFormation && !formation.QuerySystem.IsRangedCavalryFormation)) && (hasMount || (!formation.QuerySystem.IsCavalryFormation && !formation.QuerySystem.IsRangedCavalryFormation)))
				{
					return formation;
				}
			}
			return null;
		}

		// Token: 0x04000D6E RID: 3438
		protected readonly List<string> CharactersInPlayerSideByPriority;

		// Token: 0x04000D6F RID: 3439
		protected Queue<string> CharacterNamesInPlayerSideByPriorityQueue;

		// Token: 0x04000D70 RID: 3440
		protected List<Formation> RemainingFormationsToAssignSergeantsTo;

		// Token: 0x04000D71 RID: 3441
		protected Dictionary<int, Agent> FormationsLockedWithSergeants;

		// Token: 0x04000D72 RID: 3442
		protected Dictionary<int, Agent> FormationsWithLooselyChosenSergeants;
	}
}
