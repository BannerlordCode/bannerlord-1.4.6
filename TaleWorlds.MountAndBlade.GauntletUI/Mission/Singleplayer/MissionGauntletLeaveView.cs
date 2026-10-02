using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200003A RID: 58
	[OverrideView(typeof(MissionLeaveView))]
	public class MissionGauntletLeaveView : MissionView
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000F638 File Offset: 0x0000D838
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._dataSource = new MissionLeaveVM(new Func<float>(base.Mission.GetMissionEndTimerValue), new Func<float>(base.Mission.GetMissionEndTimeInSeconds));
			this._gauntletLayer = new GauntletLayer("MissionLeave", 47, false);
			this._gauntletLayer.LoadMovie("LeaveUI", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this._dataSource.Tick(dt);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
		private void OnEscapeMenuToggled(bool isOpened)
		{
			ScreenManager.SetSuspendLayer(this._gauntletLayer, !isOpened);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000F6FA File Offset: 0x0000D8FA
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (this._gauntletLayer != null)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F71F File Offset: 0x0000D91F
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (this._gauntletLayer != null)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x04000153 RID: 339
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000154 RID: 340
		private MissionLeaveVM _dataSource;
	}
}
