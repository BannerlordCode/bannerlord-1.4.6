using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A5 RID: 677
	public class MissionBattleSchedulerClientComponent : MissionLobbyComponent
	{
		// Token: 0x06002563 RID: 9571 RVA: 0x00087387 File Offset: 0x00085587
		public override void QuitMission()
		{
			base.QuitMission();
			if (base.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending && NetworkMain.GameClient.LoggedIn && NetworkMain.GameClient.CurrentState == LobbyClient.State.AtBattle)
			{
				NetworkMain.GameClient.QuitFromMatchmakerGame();
			}
		}
	}
}
