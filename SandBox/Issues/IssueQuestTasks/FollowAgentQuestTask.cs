using System;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace SandBox.Issues.IssueQuestTasks
{
	// Token: 0x020000BE RID: 190
	public class FollowAgentQuestTask : QuestTaskBase
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x00034A4C File Offset: 0x00032C4C
		public FollowAgentQuestTask(Agent followedAgent, GameEntity targetEntity, Action onSucceededAction, Action onCanceledAction, DialogFlow dialogFlow = null)
			: base(dialogFlow, onSucceededAction, null, onCanceledAction)
		{
			this._followedAgent = followedAgent;
			this._followedAgentChar = (CharacterObject)this._followedAgent.Character;
			this._targetEntity = targetEntity;
			this.StartAgentMovement();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00034A84 File Offset: 0x00032C84
		public FollowAgentQuestTask(Agent followedAgent, Agent targetAgent, Action onSucceededAction, Action onCanceledAction, DialogFlow dialogFlow = null)
			: base(dialogFlow, onSucceededAction, null, onCanceledAction)
		{
			this._followedAgent = followedAgent;
			this._targetAgent = targetAgent;
			this.StartAgentMovement();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00034AA8 File Offset: 0x00032CA8
		private void StartAgentMovement()
		{
			if (this._targetEntity != null)
			{
				UsableMachine firstScriptOfType = this._targetEntity.GetFirstScriptOfType<UsableMachine>();
				ScriptBehavior.AddUsableMachineTarget(this._followedAgent, firstScriptOfType);
				return;
			}
			if (this._targetAgent != null)
			{
				ScriptBehavior.AddAgentTarget(this._followedAgent, this._targetAgent);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00034AF8 File Offset: 0x00032CF8
		public void MissionTick(float dt)
		{
			ScriptBehavior scriptBehavior = (ScriptBehavior)this._followedAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehavior<ScriptBehavior>();
			if (scriptBehavior != null && scriptBehavior.IsNearTarget(this._targetAgent) && this._followedAgent.GetCurrentVelocity().LengthSquared < 0.0001f && this._followedAgent.Position.DistanceSquared(Mission.Current.MainAgent.Position) < 16f)
			{
				base.Finish(QuestTaskBase.FinishStates.Success);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00034B80 File Offset: 0x00032D80
		protected override void OnFinished()
		{
			this._followedAgent = null;
			this._followedAgentChar = null;
			this._targetEntity = null;
			this._targetAgent = null;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00034B9E File Offset: 0x00032D9E
		public override void SetReferences()
		{
			CampaignEvents.MissionTickEvent.AddNonSerializedListener(this, new Action<float>(this.MissionTick));
		}

		// Token: 0x04000429 RID: 1065
		private Agent _followedAgent;

		// Token: 0x0400042A RID: 1066
		private CharacterObject _followedAgentChar;

		// Token: 0x0400042B RID: 1067
		private GameEntity _targetEntity;

		// Token: 0x0400042C RID: 1068
		private Agent _targetAgent;
	}
}
