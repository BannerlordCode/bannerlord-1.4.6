using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Missions.NameMarker.Targets
{
	// Token: 0x0200003C RID: 60
	public class MissionWorkshopNameMarkerTargetVM : MissionNameMarkerTargetVM<Workshop>
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x000110D9 File Offset: 0x0000F2D9
		public MissionWorkshopNameMarkerTargetVM(Workshop target, Vec3 signPosition)
			: base(target)
		{
			base.NameType = "Passage";
			base.IconType = target.WorkshopType.StringId;
			this._signPosition = signPosition;
			this.RefreshValues();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001110B File Offset: 0x0000F30B
		public override void UpdatePosition(Camera missionCamera)
		{
			base.UpdatePositionWith(missionCamera, this._signPosition + MissionNameMarkerHelper.DefaultHeightOffset);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011124 File Offset: 0x0000F324
		protected override TextObject GetName()
		{
			return base.Target.WorkshopType.Name;
		}

		// Token: 0x0400021C RID: 540
		private readonly Vec3 _signPosition;
	}
}
