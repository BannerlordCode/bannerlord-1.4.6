using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Missions.NameMarker.Targets
{
	// Token: 0x0200003A RID: 58
	public class MissionGenericMarkerTargetVM : MissionNameMarkerTargetBaseVM
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x00010F89 File Offset: 0x0000F189
		public MissionGenericMarkerTargetVM(string identifier, string nameType, string iconType, Vec3 position, TextObject name)
		{
			this.Identifier = identifier;
			base.NameType = nameType;
			base.IconType = iconType;
			this._position = position;
			this._name = name;
			this.RefreshValues();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010FBC File Offset: 0x0000F1BC
		public override bool Equals(MissionNameMarkerTargetBaseVM other)
		{
			MissionGenericMarkerTargetVM missionGenericMarkerTargetVM;
			return (missionGenericMarkerTargetVM = other as MissionGenericMarkerTargetVM) != null && missionGenericMarkerTargetVM.Identifier == this.Identifier;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00010FE6 File Offset: 0x0000F1E6
		public override void UpdatePosition(Camera missionCamera)
		{
			base.UpdatePositionWith(missionCamera, this._position + MissionNameMarkerHelper.DefaultHeightOffset);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00010FFF File Offset: 0x0000F1FF
		protected override TextObject GetName()
		{
			return this._name;
		}

		// Token: 0x04000219 RID: 537
		public readonly string Identifier;

		// Token: 0x0400021A RID: 538
		private readonly Vec3 _position;

		// Token: 0x0400021B RID: 539
		private readonly TextObject _name;
	}
}
