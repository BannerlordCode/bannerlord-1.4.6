using System;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Missions.NameMarker.Targets
{
	// Token: 0x02000037 RID: 55
	public class MissionAnimatedBasicAreaIndicatorMarkerTargetVM : MissionNameMarkerTargetVM<AnimatedBasicAreaIndicator>
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x00010D48 File Offset: 0x0000EF48
		public MissionAnimatedBasicAreaIndicatorMarkerTargetVM(AnimatedBasicAreaIndicator target)
			: base(target)
		{
			base.NameType = "Passage";
			base.IconType = (string.IsNullOrEmpty(base.Target.Type) ? "common_area" : base.Target.Type);
			this.RefreshValues();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010D97 File Offset: 0x0000EF97
		public override void UpdatePosition(Camera missionCamera)
		{
			base.UpdatePositionWith(missionCamera, base.Target.GetPosition() + MissionNameMarkerHelper.DefaultHeightOffset);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010DB5 File Offset: 0x0000EFB5
		protected override TextObject GetName()
		{
			return base.Target.GetName();
		}
	}
}
