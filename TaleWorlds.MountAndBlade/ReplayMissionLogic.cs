using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000295 RID: 661
	public class ReplayMissionLogic : MissionLogic
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x00084C6B File Offset: 0x00082E6B
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x00084C73 File Offset: 0x00082E73
		public string FileName { get; private set; }

		// Token: 0x060024A5 RID: 9381 RVA: 0x00084C7C File Offset: 0x00082E7C
		public ReplayMissionLogic(bool isMultiplayer, string fileName = "")
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				this.FileName = fileName;
			}
			this._isMultiplayer = isMultiplayer;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00084C9A File Offset: 0x00082E9A
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (this._isMultiplayer)
			{
				GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			}
			MBCommon.CurrentGameType = MBCommon.GameType.SingleReplay;
			GameNetwork.InitializeClientSide(null, 0, -1, -1);
			base.Mission.Recorder.RestoreRecordFromFile(this.FileName);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00084CD5 File Offset: 0x00082ED5
		public override void OnRemoveBehavior()
		{
			if (this._isMultiplayer)
			{
				GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
				GameNetwork.EndReplay();
			}
			GameNetwork.TerminateClientSide();
			base.Mission.Recorder.ClearRecordBuffers();
			base.OnRemoveBehavior();
		}

		// Token: 0x04000E23 RID: 3619
		private bool _isMultiplayer;
	}
}
