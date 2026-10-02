using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Roster;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000394 RID: 916
	public interface IMenuContextHandler
	{
		// Token: 0x06003506 RID: 13574
		void OnBackgroundMeshNameSet(string name);

		// Token: 0x06003507 RID: 13575
		void OnOpenTownManagement();

		// Token: 0x06003508 RID: 13576
		void OnOpenRecruitVolunteers();

		// Token: 0x06003509 RID: 13577
		void OnOpenTournamentLeaderboard();

		// Token: 0x0600350A RID: 13578
		void OnOpenTroopSelection(TroopRoster fullRoster, TroopRoster initialSelections, List<Ship> eligibleShips, Func<CharacterObject, bool> canChangeStatusOfTroop, Action<TroopRoster> onDone, int maxSelectableTroopCount, int minSelectableTroopCount, bool isNavalRaid);

		// Token: 0x0600350B RID: 13579
		void OnMenuCreate();

		// Token: 0x0600350C RID: 13580
		void OnMenuActivate();

		// Token: 0x0600350D RID: 13581
		void OnMenuRefresh();

		// Token: 0x0600350E RID: 13582
		void OnHourlyTick();

		// Token: 0x0600350F RID: 13583
		void OnPanelSoundIDSet(string panelSoundID);

		// Token: 0x06003510 RID: 13584
		void OnAmbientSoundIDSet(string ambientSoundID);
	}
}
