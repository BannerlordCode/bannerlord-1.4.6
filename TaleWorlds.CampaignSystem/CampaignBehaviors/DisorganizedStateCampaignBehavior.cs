using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003EA RID: 1002
	internal class DisorganizedStateCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E25 RID: 15909 RVA: 0x00110354 File Offset: 0x0010E554
		public override void RegisterEvents()
		{
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnd));
			CampaignEvents.PartyRemovedFromArmyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyRemovedFromArmy));
			CampaignEvents.GameMenuOptionSelectedEvent.AddNonSerializedListener(this, new Action<GameMenu, GameMenuOption>(this.OnGameMenuOptionSelected));
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x001103C0 File Offset: 0x0010E5C0
		private void OnGameMenuOptionSelected(GameMenu gameMenu, GameMenuOption gameMenuOption)
		{
			if (this._checkForEvent && (gameMenuOption.IdString == "str_order_attack" || gameMenuOption.IdString == "attack"))
			{
				foreach (MapEventParty mapEventParty in MobileParty.MainParty.MapEvent.DefenderSide.Parties)
				{
					if (Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(mapEventParty.Party))
					{
						mapEventParty.Party.MobileParty.SetDisorganized(true);
					}
				}
			}
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x00110478 File Offset: 0x0010E678
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (mapEvent.IsSallyOut)
			{
				if (!mapEvent.AttackerSide.IsMainPartyAmongParties())
				{
					using (List<MapEventParty>.Enumerator enumerator = mapEvent.DefenderSide.Parties.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MapEventParty mapEventParty = enumerator.Current;
							if (Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(mapEventParty.Party))
							{
								mapEventParty.Party.MobileParty.SetDisorganized(true);
							}
						}
						return;
					}
				}
				this._checkForEvent = true;
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x00110514 File Offset: 0x0010E714
		private void OnPartyRemovedFromArmy(MobileParty mobileParty)
		{
			if (Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(mobileParty.Party))
			{
				mobileParty.SetDisorganized(true);
			}
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x0011053C File Offset: 0x0010E73C
		private void OnMapEventEnd(MapEvent mapEvent)
		{
			bool flag;
			if (mapEvent.AttackerSide.Parties.Sum<MapEventParty>((MapEventParty x) => x.HealthyManCountAtStart) == mapEvent.AttackerSide.Parties.Sum<MapEventParty>((MapEventParty x) => x.Party.NumberOfHealthyMembers))
			{
				flag = mapEvent.DefenderSide.Parties.Sum<MapEventParty>((MapEventParty x) => x.HealthyManCountAtStart) != mapEvent.DefenderSide.Parties.Sum<MapEventParty>((MapEventParty x) => x.Party.NumberOfHealthyMembers);
			}
			else
			{
				flag = true;
			}
			if (flag && !mapEvent.IsHideoutBattle)
			{
				foreach (PartyBase partyBase in mapEvent.InvolvedParties)
				{
					if (partyBase.IsActive)
					{
						MobileParty mobileParty = partyBase.MobileParty;
						if ((mobileParty == null || !mobileParty.IsMainParty || !mapEvent.DiplomaticallyFinished || !mapEvent.AttackerSide.MapFaction.IsAtWarWith(mapEvent.DefenderSide.MapFaction)) && (!mapEvent.IsSallyOut || partyBase.MapEventSide.MissionSide == BattleSideEnum.Defender) && Campaign.Current.Models.PartyImpairmentModel.CanGetDisorganized(partyBase) && (mapEvent.RetreatingSide == BattleSideEnum.None || mapEvent.RetreatingSide != partyBase.Side))
						{
							partyBase.MobileParty.SetDisorganized(true);
						}
					}
				}
			}
			this._checkForEvent = false;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001106F8 File Offset: 0x0010E8F8
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<bool>("_checkForEvent", ref this._checkForEvent);
		}

		// Token: 0x040012C8 RID: 4808
		private bool _checkForEvent;
	}
}
