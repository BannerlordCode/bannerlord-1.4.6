using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000446 RID: 1094
	public class TownSecurityCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004669 RID: 18025 RVA: 0x0015FE8C File Offset: 0x0015E08C
		public override void RegisterEvents()
		{
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.MapEventEnded));
			CampaignEvents.OnSiegeEventEndedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.SiegeEventEnded));
			CampaignEvents.OnHideoutDeactivatedEvent.AddNonSerializedListener(this, new Action<Settlement>(this.OnHideoutDeactivated));
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x0015FEE0 File Offset: 0x0015E0E0
		private void OnHideoutDeactivated(Settlement hideout)
		{
			SettlementSecurityModel model = Campaign.Current.Models.SettlementSecurityModel;
			foreach (Settlement settlement in Settlement.All.Where<Settlement>((Settlement t) => t.IsTown && t.Position.DistanceSquared(hideout.Position) < model.HideoutClearedSecurityEffectRadius * model.HideoutClearedSecurityEffectRadius).ToList<Settlement>())
			{
				settlement.Town.Security += (float)model.HideoutClearedSecurityGain;
			}
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x0015FF80 File Offset: 0x0015E180
		private void MapEventEnded(MapEvent mapEvent)
		{
			if (mapEvent.IsFieldBattle && mapEvent.HasWinner)
			{
				SettlementSecurityModel model = Campaign.Current.Models.SettlementSecurityModel;
				using (List<Settlement>.Enumerator enumerator = Settlement.All.Where<Settlement>((Settlement t) => t.IsTown && t.Position.DistanceSquared(mapEvent.Position) < model.MapEventSecurityEffectRadius * model.MapEventSecurityEffectRadius).ToList<Settlement>().GetEnumerator())
				{
					Func<PartyBase, bool> <>9__3;
					while (enumerator.MoveNext())
					{
						Settlement town = enumerator.Current;
						if (mapEvent.Winner.Parties.Any<MapEventParty>((MapEventParty party) => party.Party.IsMobile && party.Party.MobileParty.IsBandit) && mapEvent.InvolvedParties.Any<PartyBase>((PartyBase party) => this.ValidCivilianPartyCondition(party, mapEvent, town.MapFaction)))
						{
							float num = mapEvent.StrengthOfSide[(int)mapEvent.DefeatedSide];
							town.Town.Security += model.GetLootedNearbyPartySecurityEffect(town.Town, num);
						}
						else
						{
							IEnumerable<PartyBase> involvedParties = mapEvent.InvolvedParties;
							Func<PartyBase, bool> func;
							if ((func = <>9__3) == null)
							{
								func = (<>9__3 = (PartyBase party) => this.ValidBanditPartyCondition(party, mapEvent));
							}
							if (involvedParties.Any<PartyBase>(func))
							{
								float num2 = mapEvent.StrengthOfSide[(int)mapEvent.DefeatedSide];
								town.Town.Security += model.GetNearbyBanditPartyDefeatedSecurityEffect(town.Town, num2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x001601E8 File Offset: 0x0015E3E8
		private bool ValidCivilianPartyCondition(PartyBase party, MapEvent mapEvent, IFaction mapFaction)
		{
			return party.IsMobile && ((party.Side != mapEvent.WinningSide && party.MobileParty.IsVillager && DiplomacyHelper.IsSameFactionAndNotEliminated(party.MapFaction, mapFaction)) || (party.MobileParty.IsCaravan && !party.MapFaction.IsAtWarWith(mapFaction)));
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x00160248 File Offset: 0x0015E448
		private bool ValidBanditPartyCondition(PartyBase party, MapEvent mapEvent)
		{
			if (party.Side != mapEvent.WinningSide)
			{
				MobileParty mobileParty = party.MobileParty;
				return mobileParty != null && mobileParty.IsBandit;
			}
			return false;
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x0016026B File Offset: 0x0015E46B
		private void SiegeEventEnded(SiegeEvent siegeEvent)
		{
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x0016026D File Offset: 0x0015E46D
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
