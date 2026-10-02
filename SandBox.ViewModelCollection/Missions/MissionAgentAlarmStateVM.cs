using System;
using System.Collections.Generic;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Objects;

namespace SandBox.ViewModelCollection.Missions
{
	// Token: 0x0200002B RID: 43
	public class MissionAgentAlarmStateVM : ViewModel
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000F289 File Offset: 0x0000D489
		public MissionAgentAlarmStateVM()
		{
			this.Targets = new MBBindingList<MissionAgentAlarmTargetVM>();
			this._stealthBoxes = new List<StealthBox>();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		public void Initialize(Mission mission, Camera camera)
		{
			this._mission = mission;
			this._camera = camera;
			this._isInitialized = true;
			this._areStealthBoxesDirty = true;
			this.RefreshTargets();
			StealthBox.OnBoxInitialized += this.OnStealthBoxInitialized;
			StealthBox.OnBoxRemoved += this.OnStealthBoxRemoved;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
		public override void OnFinalize()
		{
			base.OnFinalize();
			StealthBox.OnBoxInitialized -= this.OnStealthBoxInitialized;
			StealthBox.OnBoxRemoved -= this.OnStealthBoxRemoved;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000F323 File Offset: 0x0000D523
		private void OnStealthBoxInitialized(StealthBox stealthBox)
		{
			this._areStealthBoxesDirty = true;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000F32C File Offset: 0x0000D52C
		private void OnStealthBoxRemoved(StealthBox stealthBox)
		{
			this._areStealthBoxesDirty = true;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F338 File Offset: 0x0000D538
		private void RefreshStealthBoxEntities()
		{
			this._stealthBoxes.Clear();
			Mission mission = Mission.Current;
			if (((mission != null) ? mission.Scene : null) == null)
			{
				return;
			}
			List<GameEntity> list = new List<GameEntity>();
			Mission.Current.Scene.GetAllEntitiesWithScriptComponent<StealthBox>(ref list);
			for (int i = 0; i < list.Count; i++)
			{
				StealthBox firstScriptOfTypeRecursive = list[i].GetFirstScriptOfTypeRecursive<StealthBox>();
				if (firstScriptOfTypeRecursive != null)
				{
					this._stealthBoxes.Add(firstScriptOfTypeRecursive);
				}
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public void Update()
		{
			if (!this._isInitialized)
			{
				return;
			}
			if (this._disguiseMissionLogic == null)
			{
				Mission mission = this._mission;
				this._disguiseMissionLogic = ((mission != null) ? mission.GetMissionBehavior<DisguiseMissionLogic>() : null);
			}
			DisguiseMissionLogic disguiseMissionLogic = this._disguiseMissionLogic;
			bool flag = disguiseMissionLogic != null && disguiseMissionLogic.IsInStealthMode;
			this.IsMainAgentInSafeArea = this.IsMainAgentInStealthArea();
			for (int i = 0; i < this.Targets.Count; i++)
			{
				MissionAgentAlarmTargetVM missionAgentAlarmTargetVM = this.Targets[i];
				if (this._disguiseMissionLogic == null)
				{
					missionAgentAlarmTargetVM.IsStealthModeEnabled = true;
					missionAgentAlarmTargetVM.IsMainAgentInVisibilityRange = SandBoxUIHelper.IsAgentInVisibilityRangeApproximate(missionAgentAlarmTargetVM.TargetAgent, Agent.Main);
					missionAgentAlarmTargetVM.IsInVision = true;
					missionAgentAlarmTargetVM.IsSuspected = missionAgentAlarmTargetVM.AlarmProgress > 0;
					missionAgentAlarmTargetVM.UpdateScreenPosition(this._camera);
					missionAgentAlarmTargetVM.UpdateValues();
				}
				else
				{
					missionAgentAlarmTargetVM.IsStealthModeEnabled = flag;
					DisguiseMissionLogic.ShadowingAgentOffenseInfo agentOffenseInfo = this._disguiseMissionLogic.GetAgentOffenseInfo(missionAgentAlarmTargetVM.TargetAgent);
					if (agentOffenseInfo != null)
					{
						missionAgentAlarmTargetVM.IsMainAgentInVisibilityRange = SandBoxUIHelper.IsAgentInVisibilityRangeApproximate(missionAgentAlarmTargetVM.TargetAgent, Agent.Main);
						missionAgentAlarmTargetVM.IsInVision = agentOffenseInfo.CanPlayerCameraSeeTheAgent;
						missionAgentAlarmTargetVM.IsSuspected = missionAgentAlarmTargetVM.AlarmProgress > 0;
					}
					missionAgentAlarmTargetVM.UpdateScreenPosition(this._camera);
					missionAgentAlarmTargetVM.UpdateValues();
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		private bool IsMainAgentInStealthArea()
		{
			Agent main = Agent.Main;
			if (main == null)
			{
				return false;
			}
			Mission mission = Mission.Current;
			if (((mission != null) ? mission.Scene : null) == null)
			{
				return false;
			}
			if (this._areStealthBoxesDirty)
			{
				this.RefreshStealthBoxEntities();
				this._areStealthBoxesDirty = false;
			}
			for (int i = 0; i < this._stealthBoxes.Count; i++)
			{
				if (this._stealthBoxes[i].IsAgentInside(main))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000F554 File Offset: 0x0000D754
		public void OnAgentRemoved(Agent agent)
		{
			MissionAgentAlarmTargetVM agentTargetFromAgent = this.GetAgentTargetFromAgent(agent);
			if (agentTargetFromAgent != null)
			{
				this.Targets.Remove(agentTargetFromAgent);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000F57C File Offset: 0x0000D77C
		private void RefreshTargets()
		{
			this.Targets.Clear();
			foreach (Agent agent in Mission.Current.Agents)
			{
				if (agent != null && SandBoxUIHelper.CanAgentBeAlarmed(agent))
				{
					this.Targets.Add(new MissionAgentAlarmTargetVM(agent, new Action<MissionAgentAlarmTargetVM>(this.OnRemoveTarget)));
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000F600 File Offset: 0x0000D800
		public void OnAgentBuild(Agent agent, Banner banner)
		{
			this.RefreshTargets();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000F608 File Offset: 0x0000D808
		public void OnAgentTeamChanged(Team prevTeam, Team newTeam, Agent agent)
		{
			if (agent != null && agent == Agent.Main)
			{
				this.RefreshTargets();
				return;
			}
			MissionAgentAlarmTargetVM agentTargetFromAgent = this.GetAgentTargetFromAgent(agent);
			if (agentTargetFromAgent == null && SandBoxUIHelper.CanAgentBeAlarmed(agent))
			{
				this.Targets.Add(new MissionAgentAlarmTargetVM(agent, new Action<MissionAgentAlarmTargetVM>(this.OnRemoveTarget)));
				return;
			}
			if (agentTargetFromAgent != null && (newTeam == Team.Invalid || (newTeam == null || newTeam.IsPlayerAlly)))
			{
				this.Targets.Remove(agentTargetFromAgent);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000F67E File Offset: 0x0000D87E
		private void OnRemoveTarget(MissionAgentAlarmTargetVM targetToRemove)
		{
			this.Targets.Remove(targetToRemove);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000F690 File Offset: 0x0000D890
		private MissionAgentAlarmTargetVM GetAgentTargetFromAgent(Agent agent)
		{
			for (int i = 0; i < this.Targets.Count; i++)
			{
				MissionAgentAlarmTargetVM missionAgentAlarmTargetVM = this.Targets[i];
				if (missionAgentAlarmTargetVM.TargetAgent == agent)
				{
					return missionAgentAlarmTargetVM;
				}
			}
			return null;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		[DataSourceProperty]
		public MBBindingList<MissionAgentAlarmTargetVM> Targets
		{
			get
			{
				return this._targets;
			}
			set
			{
				if (value != this._targets)
				{
					this._targets = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionAgentAlarmTargetVM>>(value, "Targets");
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000F6FA File Offset: 0x0000D8FA
		[DataSourceProperty]
		public bool IsMainAgentInSafeArea
		{
			get
			{
				return this._isMainAgentInSafeArea;
			}
			set
			{
				if (value != this._isMainAgentInSafeArea)
				{
					this._isMainAgentInSafeArea = value;
					base.OnPropertyChangedWithValue(value, "IsMainAgentInSafeArea");
				}
			}
		}

		// Token: 0x040001C5 RID: 453
		private bool _isInitialized;

		// Token: 0x040001C6 RID: 454
		private Mission _mission;

		// Token: 0x040001C7 RID: 455
		private Camera _camera;

		// Token: 0x040001C8 RID: 456
		private DisguiseMissionLogic _disguiseMissionLogic;

		// Token: 0x040001C9 RID: 457
		private bool _areStealthBoxesDirty;

		// Token: 0x040001CA RID: 458
		private List<StealthBox> _stealthBoxes;

		// Token: 0x040001CB RID: 459
		private bool _isMainAgentInSafeArea;

		// Token: 0x040001CC RID: 460
		private MBBindingList<MissionAgentAlarmTargetVM> _targets;
	}
}
