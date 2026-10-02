using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Helpers
{
	// Token: 0x02000026 RID: 38
	public static class PortStateHelper
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00010F08 File Offset: 0x0000F108
		public static void OpenAsTrade(Town town)
		{
			PortState portState = GameStateManager.Current.CreateState<PortState>(new object[]
			{
				town.Settlement.Party,
				PartyBase.MainParty,
				PortScreenModes.TradeMode
			});
			GameStateManager.Current.PushState(portState, 0);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00010F54 File Offset: 0x0000F154
		public static void OpenAsLoot(MBReadOnlyList<Ship> lootShips, Action onEndAction = null)
		{
			PortState portState = GameStateManager.Current.CreateState<PortState>(new object[]
			{
				null,
				PartyBase.MainParty,
				lootShips,
				PartyBase.MainParty.Ships,
				onEndAction,
				PortScreenModes.LootMode
			});
			GameStateManager.Current.PushState(portState, 0);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public static void OpenAsRestricted(Town town, TextObject restrictedReason)
		{
			PortState portState = GameStateManager.Current.CreateState<PortState>(new object[]
			{
				town.Settlement.Party,
				PartyBase.MainParty,
				PortScreenModes.Restricted
			});
			GameStateManager.Current.PushState(portState, 0);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public static void OpenAsStoryMode(Settlement settlement)
		{
			PortState portState = GameStateManager.Current.CreateState<PortState>(new object[]
			{
				settlement,
				PartyBase.MainParty,
				PortScreenModes.Story
			});
			GameStateManager.Current.PushState(portState, 0);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00011030 File Offset: 0x0000F230
		public static void OpenAsManageFleet(MBReadOnlyList<Ship> leftShips)
		{
			PortState portState = GameStateManager.Current.CreateState<PortState>(new object[]
			{
				null,
				PartyBase.MainParty,
				leftShips,
				PartyBase.MainParty.Ships,
				PortScreenModes.Manage
			});
			GameStateManager.Current.PushState(portState, 0);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0001107C File Offset: 0x0000F27C
		public static void OpenAsManageOtherFleet(PartyBase other, Action onEndAction)
		{
			PortState portState = GameStateManager.Current.CreateState<PortState>(new object[]
			{
				other,
				PartyBase.MainParty,
				onEndAction,
				PortScreenModes.ManageOther
			});
			GameStateManager.Current.PushState(portState, 0);
		}
	}
}
