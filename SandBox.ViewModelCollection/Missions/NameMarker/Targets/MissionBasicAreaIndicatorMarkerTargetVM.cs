using System;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Missions.NameMarker.Targets
{
	// Token: 0x02000038 RID: 56
	public class MissionBasicAreaIndicatorMarkerTargetVM : MissionNameMarkerTargetVM<BasicAreaIndicator>
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		public MissionBasicAreaIndicatorMarkerTargetVM(BasicAreaIndicator target, Vec3 position)
			: base(target)
		{
			base.NameType = "Passage";
			base.IconType = (string.IsNullOrEmpty(base.Target.Type) ? "common_area" : base.Target.Type);
			this._position = position;
			this.RefreshValues();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010E1A File Offset: 0x0000F01A
		public override void UpdatePosition(Camera missionCamera)
		{
			base.UpdatePositionWith(missionCamera, this._position + MissionNameMarkerHelper.DefaultHeightOffset);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010E33 File Offset: 0x0000F033
		protected override TextObject GetName()
		{
			return base.Target.GetName();
		}

		// Token: 0x04000217 RID: 535
		private readonly Vec3 _position;
	}
}
