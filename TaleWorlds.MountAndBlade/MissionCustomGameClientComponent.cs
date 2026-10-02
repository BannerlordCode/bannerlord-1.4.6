using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A7 RID: 679
	public class MissionCustomGameClientComponent : MissionLobbyComponent
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x00087419 File Offset: 0x00085619
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._lobbyClient = NetworkMain.GameClient;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0008742C File Offset: 0x0008562C
		public void SetServerEndingBeforeClientLoaded(bool isServerEndingBeforeClientLoaded)
		{
			this._isServerEndedBeforeClientLoaded = isServerEndingBeforeClientLoaded;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00087438 File Offset: 0x00085638
		public override void QuitMission()
		{
			base.QuitMission();
			if (GameNetwork.IsServer)
			{
				if (base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && this._lobbyClient.LoggedIn && this._lobbyClient.CurrentState == LobbyClient.State.HostingCustomGame)
				{
					this._lobbyClient.EndCustomGame();
					return;
				}
			}
			else if (!this._isServerEndedBeforeClientLoaded && base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && this._lobbyClient.LoggedIn && this._lobbyClient.CurrentState == LobbyClient.State.InCustomGame)
			{
				this._lobbyClient.QuitFromCustomGame();
			}
		}

		// Token: 0x04000E66 RID: 3686
		private LobbyClient _lobbyClient;

		// Token: 0x04000E67 RID: 3687
		private bool _isServerEndedBeforeClientLoaded;
	}
}
