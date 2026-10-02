using System;
using SandBox.View.Missions;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Missions
{
	// Token: 0x0200001F RID: 31
	[OverrideView(typeof(MissionHideoutAmbushCinematicView))]
	public class MissionGauntletHideoutAmbushCinematicView : MissionHideoutAmbushCinematicView
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000B906 File Offset: 0x00009B06
		public MissionGauntletHideoutAmbushCinematicView()
		{
			this._gauntletLayer = new MissionGauntletHideoutAmbushCinematicView.HideoutAmbushCutsceneGauntletLayer(10, false);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B91C File Offset: 0x00009B1C
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B935 File Offset: 0x00009B35
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B950 File Offset: 0x00009B50
		protected override void SetPlayerMovementEnabled(bool isPlayerMovementEnabled)
		{
			base.SetPlayerMovementEnabled(isPlayerMovementEnabled);
			for (int i = 0; i < base.Mission.MissionBehaviors.Count; i++)
			{
				MissionBattleUIBaseView missionBattleUIBaseView;
				if ((missionBattleUIBaseView = base.Mission.MissionBehaviors[i] as MissionBattleUIBaseView) != null)
				{
					if (!isPlayerMovementEnabled)
					{
						missionBattleUIBaseView.SuspendView();
					}
					else
					{
						missionBattleUIBaseView.ResumeView();
					}
				}
			}
			if (isPlayerMovementEnabled)
			{
				this._gauntletLayer.IsFocusLayer = false;
				ScreenManager.TryLoseFocus(this._gauntletLayer);
				this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
				return;
			}
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
		}

		// Token: 0x0400008B RID: 139
		private MissionGauntletHideoutAmbushCinematicView.HideoutAmbushCutsceneGauntletLayer _gauntletLayer;

		// Token: 0x0200007D RID: 125
		private class HideoutAmbushCutsceneGauntletLayer : GauntletLayer
		{
			// Token: 0x0600044C RID: 1100 RVA: 0x00018414 File Offset: 0x00016614
			public HideoutAmbushCutsceneGauntletLayer(int localOrder, bool shouldClear = false)
				: base("MissionHideoutAmbushCutscene", localOrder, shouldClear)
			{
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00018423 File Offset: 0x00016623
			public override bool HitTest()
			{
				return true;
			}
		}
	}
}
