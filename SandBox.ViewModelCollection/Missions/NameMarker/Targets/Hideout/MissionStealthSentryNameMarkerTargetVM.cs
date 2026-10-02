using System;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Missions.NameMarker.Targets.Hideout
{
	// Token: 0x02000040 RID: 64
	public class MissionStealthSentryNameMarkerTargetVM : MissionNameMarkerTargetVM<Agent>
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0001133B File Offset: 0x0000F53B
		public MissionStealthSentryNameMarkerTargetVM(Agent target)
			: base(target)
		{
			base.IconType = "sentry";
			base.NameType = "Enemy";
			base.IsEnemy = true;
			this.RefreshValues();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011367 File Offset: 0x0000F567
		public override void UpdatePosition(Camera missionCamera)
		{
			base.UpdatePositionWith(missionCamera, base.Target.GetEyeGlobalPosition() + MissionNameMarkerHelper.AgentHeightOffset);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011385 File Offset: 0x0000F585
		protected override TextObject GetName()
		{
			return new TextObject("{=KdT0PM8Y}Sentry", null);
		}
	}
}
