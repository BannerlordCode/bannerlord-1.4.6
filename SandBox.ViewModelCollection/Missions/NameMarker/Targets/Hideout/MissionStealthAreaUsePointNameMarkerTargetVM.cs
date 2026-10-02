using System;
using SandBox.Objects.Usables;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Missions.NameMarker.Targets.Hideout
{
	// Token: 0x0200003E RID: 62
	public class MissionStealthAreaUsePointNameMarkerTargetVM : MissionNameMarkerTargetBaseVM
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00011188 File Offset: 0x0000F388
		public MissionStealthAreaUsePointNameMarkerTargetVM(StealthAreaUsePoint usePoint)
		{
			this._usePoint = usePoint;
			base.IconType = "call_troops";
			base.NameType = "Normal";
			this.RefreshValues();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000111B3 File Offset: 0x0000F3B3
		public override bool Equals(MissionNameMarkerTargetBaseVM other)
		{
			return false;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public override void UpdatePosition(Camera missionCamera)
		{
			MatrixFrame globalFrame = this._usePoint.GameEntity.GetGlobalFrame();
			base.UpdatePositionWith(missionCamera, globalFrame.origin + Vec3.Up * 0.5f);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000111FA File Offset: 0x0000F3FA
		protected override TextObject GetName()
		{
			return new TextObject("{=GmjiZk9P}Call Troops", null);
		}

		// Token: 0x0400021E RID: 542
		private StealthAreaUsePoint _usePoint;
	}
}
