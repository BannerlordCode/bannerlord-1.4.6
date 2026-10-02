using System;
using SandBox.Missions.AgentBehaviors;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Missions.MainAgentDetection
{
	// Token: 0x02000042 RID: 66
	public class MissionDisguiseMarkerItemVM : ViewModel
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00011530 File Offset: 0x0000F730
		public DisguiseMissionLogic.ShadowingAgentOffenseInfo OffenseInfo { get; }

		// Token: 0x06000447 RID: 1095 RVA: 0x00011538 File Offset: 0x0000F738
		public MissionDisguiseMarkerItemVM(Camera missionCamera, DisguiseMissionLogic.ShadowingAgentOffenseInfo offenseInfo)
		{
			this._missionCamera = missionCamera;
			this.OffenseInfo = offenseInfo;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001154E File Offset: 0x0000F74E
		public void RefreshVisuals()
		{
			DisguiseMissionLogic.ShadowingAgentOffenseInfo offenseInfo = this.OffenseInfo;
			this.OffenseTypeIdentifier = this.GetOffenseTypeIdentifier((offenseInfo != null) ? offenseInfo.OffenseType : StealthOffenseTypes.None);
			this.UpdateAlarmState();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00011574 File Offset: 0x0000F774
		public void UpdatePosition()
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			Vec3 position = this.OffenseInfo.Agent.Position;
			position.z += this.OffenseInfo.Agent.GetEyeGlobalHeight() + 0.35f;
			if (position.IsValid)
			{
				MBWindowManager.WorldToScreenInsideUsableArea(this._missionCamera, position, ref num, ref num2, ref num3);
			}
			if (!position.IsValid || num3 < 0f || !MathF.IsValidValue(num) || !MathF.IsValidValue(num2))
			{
				num = -10000f;
				num2 = -10000f;
				num3 = 0f;
			}
			this.ScreenPosition = new Vec2(num, num2);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00011624 File Offset: 0x0000F824
		private void UpdateAlarmState()
		{
			Agent agent = this.OffenseInfo.Agent;
			AgentNavigator agentNavigator = agent.GetComponent<CampaignAgentComponent>().AgentNavigator;
			AlarmedBehaviorGroup alarmedBehaviorGroup = ((agentNavigator != null) ? agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>() : null);
			Agent.AIStateFlag aistateFlags = agent.AIStateFlags;
			if (aistateFlags.HasAnyFlag(Agent.AIStateFlag.Alarmed))
			{
				this._activeAlarmState = MissionDisguiseMarkerItemVM.AgentAlarmStateEnum.Alarmed;
			}
			else if (aistateFlags.HasAnyFlag(Agent.AIStateFlag.Cautious))
			{
				this._activeAlarmState = MissionDisguiseMarkerItemVM.AgentAlarmStateEnum.Cautious;
			}
			else if (aistateFlags.HasAnyFlag(Agent.AIStateFlag.PatrollingCautious))
			{
				this._activeAlarmState = MissionDisguiseMarkerItemVM.AgentAlarmStateEnum.PatrollingCautious;
			}
			else
			{
				this._activeAlarmState = MissionDisguiseMarkerItemVM.AgentAlarmStateEnum.None;
			}
			float num;
			if (aistateFlags.HasAnyFlag(Agent.AIStateFlag.Alarmed))
			{
				num = 1f;
			}
			else
			{
				num = MathF.Clamp(alarmedBehaviorGroup.AlarmFactor / 2f, 0f, 1f);
			}
			this.AlarmState = this._activeAlarmState.ToString();
			this.AlarmProgress = (int)(num * 100f);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000116EC File Offset: 0x0000F8EC
		private string GetOffenseTypeIdentifier(StealthOffenseTypes offenseType)
		{
			if (this.IsStealthModeEnabled || !this.IsInVision || !this.IsInVisibilityRange)
			{
				this._offenseType = MissionDisguiseMarkerItemVM.AgentStealthOffenseType.None;
				return this._offenseType.ToString();
			}
			switch (offenseType)
			{
			case StealthOffenseTypes.None:
				this._offenseType = MissionDisguiseMarkerItemVM.AgentStealthOffenseType.Default;
				break;
			case StealthOffenseTypes.IsVisible:
				this._offenseType = (this.IsSuspicious ? MissionDisguiseMarkerItemVM.AgentStealthOffenseType.Suspicious : MissionDisguiseMarkerItemVM.AgentStealthOffenseType.Visible);
				break;
			case StealthOffenseTypes.IsInPersonalZone:
				this._offenseType = MissionDisguiseMarkerItemVM.AgentStealthOffenseType.Suspicious;
				break;
			}
			return this._offenseType.ToString();
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00011773 File Offset: 0x0000F973
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001177B File Offset: 0x0000F97B
		[DataSourceProperty]
		public Vec2 ScreenPosition
		{
			get
			{
				return this._screenPosition;
			}
			set
			{
				if (value != this._screenPosition)
				{
					this._screenPosition = value;
					base.OnPropertyChangedWithValue(value, "ScreenPosition");
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001179E File Offset: 0x0000F99E
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000117A6 File Offset: 0x0000F9A6
		[DataSourceProperty]
		public int AlarmProgress
		{
			get
			{
				return this._alarmProgress;
			}
			set
			{
				if (value != this._alarmProgress)
				{
					this._alarmProgress = value;
					base.OnPropertyChangedWithValue(value, "AlarmProgress");
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000117C4 File Offset: 0x0000F9C4
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000117CC File Offset: 0x0000F9CC
		[DataSourceProperty]
		public string AlarmState
		{
			get
			{
				return this._alarmState;
			}
			set
			{
				if (value != this._alarmState)
				{
					this._alarmState = value;
					base.OnPropertyChangedWithValue<string>(value, "AlarmState");
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000117EF File Offset: 0x0000F9EF
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000117F7 File Offset: 0x0000F9F7
		[DataSourceProperty]
		public string OffenseTypeIdentifier
		{
			get
			{
				return this._offenseTypeIdentifier;
			}
			set
			{
				if (value != this._offenseTypeIdentifier)
				{
					this._offenseTypeIdentifier = value;
					base.OnPropertyChangedWithValue<string>(value, "OffenseTypeIdentifier");
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001181A File Offset: 0x0000FA1A
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00011822 File Offset: 0x0000FA22
		[DataSourceProperty]
		public bool IsStealthModeEnabled
		{
			get
			{
				return this._isStealthModeEnabled;
			}
			set
			{
				if (value != this._isStealthModeEnabled)
				{
					this._isStealthModeEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsStealthModeEnabled");
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00011840 File Offset: 0x0000FA40
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00011848 File Offset: 0x0000FA48
		[DataSourceProperty]
		public bool IsSuspicious
		{
			get
			{
				return this._isSuspicious;
			}
			set
			{
				if (value != this._isSuspicious)
				{
					this._isSuspicious = value;
					base.OnPropertyChangedWithValue(value, "IsSuspicious");
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00011866 File Offset: 0x0000FA66
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0001186E File Offset: 0x0000FA6E
		[DataSourceProperty]
		public bool IsTarget
		{
			get
			{
				return this._isTarget;
			}
			set
			{
				if (value != this._isTarget)
				{
					this._isTarget = value;
					base.OnPropertyChangedWithValue(value, "IsTarget");
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001188C File Offset: 0x0000FA8C
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00011894 File Offset: 0x0000FA94
		[DataSourceProperty]
		public bool IsInVision
		{
			get
			{
				return this._isInVision;
			}
			set
			{
				if (value != this._isInVision)
				{
					this._isInVision = value;
					base.OnPropertyChangedWithValue(value, "IsInVision");
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x000118B2 File Offset: 0x0000FAB2
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x000118BA File Offset: 0x0000FABA
		[DataSourceProperty]
		public bool IsInVisibilityRange
		{
			get
			{
				return this._isInVisibilityRange;
			}
			set
			{
				if (value != this._isInVisibilityRange)
				{
					this._isInVisibilityRange = value;
					base.OnPropertyChangedWithValue(value, "IsInVisibilityRange");
				}
			}
		}

		// Token: 0x0400022B RID: 555
		private Camera _missionCamera;

		// Token: 0x0400022D RID: 557
		private MissionDisguiseMarkerItemVM.AgentAlarmStateEnum _activeAlarmState;

		// Token: 0x0400022E RID: 558
		private MissionDisguiseMarkerItemVM.AgentStealthOffenseType _offenseType;

		// Token: 0x0400022F RID: 559
		private Vec2 _screenPosition;

		// Token: 0x04000230 RID: 560
		private int _alarmProgress;

		// Token: 0x04000231 RID: 561
		private string _alarmState;

		// Token: 0x04000232 RID: 562
		private string _offenseTypeIdentifier;

		// Token: 0x04000233 RID: 563
		private bool _isStealthModeEnabled;

		// Token: 0x04000234 RID: 564
		private bool _isSuspicious;

		// Token: 0x04000235 RID: 565
		private bool _isTarget;

		// Token: 0x04000236 RID: 566
		private bool _isInVision;

		// Token: 0x04000237 RID: 567
		private bool _isInVisibilityRange;

		// Token: 0x020000A1 RID: 161
		public enum AgentAlarmStateEnum
		{
			// Token: 0x040003B8 RID: 952
			None = -1,
			// Token: 0x040003B9 RID: 953
			Alarmed,
			// Token: 0x040003BA RID: 954
			Cautious,
			// Token: 0x040003BB RID: 955
			PatrollingCautious,
			// Token: 0x040003BC RID: 956
			Suspicious,
			// Token: 0x040003BD RID: 957
			Visible
		}

		// Token: 0x020000A2 RID: 162
		public enum AgentStealthOffenseType
		{
			// Token: 0x040003BF RID: 959
			None = -1,
			// Token: 0x040003C0 RID: 960
			Default,
			// Token: 0x040003C1 RID: 961
			Visible,
			// Token: 0x040003C2 RID: 962
			Suspicious
		}
	}
}
