using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.Multiplayer.View.MissionViews;
using TaleWorlds.MountAndBlade.Multiplayer.ViewModelCollection;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace TaleWorlds.MountAndBlade.Multiplayer.GauntletUI.Mission
{
	// Token: 0x0200001C RID: 28
	[OverrideView(typeof(MissionMultiplayerVoiceChatUIHandler))]
	public class MissionGauntletVoiceChat : MissionView
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00007CFA File Offset: 0x00005EFA
		public MissionGauntletVoiceChat()
		{
			this.ViewOrderPriority = 60;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007D0C File Offset: 0x00005F0C
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._dataSource = new MultiplayerVoiceChatVM(base.Mission);
			this._gauntletLayer = new GauntletLayer("MultiplayerVoiceChat", this.ViewOrderPriority, false);
			this._gauntletLayer.LoadMovie("MultiplayerVoiceChat", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007D6F File Offset: 0x00005F6F
		public override void OnMissionScreenFinalize()
		{
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._gauntletLayer = null;
			base.OnMissionScreenFinalize();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007DA1 File Offset: 0x00005FA1
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			this._dataSource.OnTick(dt);
		}

		// Token: 0x0400008C RID: 140
		private MultiplayerVoiceChatVM _dataSource;

		// Token: 0x0400008D RID: 141
		private GauntletLayer _gauntletLayer;
	}
}
