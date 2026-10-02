using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003EF RID: 1007
	public class FactionDiscontinuationCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003F52 RID: 16210 RVA: 0x0011DE20 File Offset: 0x0011C020
		public override void RegisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.DailyTickClanEvent.AddNonSerializedListener(this, new Action<Clan>(this.DailyTickClan));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0011DE8C File Offset: 0x0011C08C
		public void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (this._independentClans.ContainsKey(newOwner.Clan))
			{
				this._independentClans.Remove(newOwner.Clan);
			}
			if (this.CanClanBeDiscontinued(oldOwner.Clan))
			{
				this.AddIndependentClan(oldOwner.Clan);
			}
			Kingdom kingdom = oldOwner.Clan.Kingdom;
			if (kingdom != null && this.CanKingdomBeDiscontinued(kingdom))
			{
				this.DiscontinueKingdom(kingdom);
			}
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0011DEFC File Offset: 0x0011C0FC
		public void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			if (newKingdom == null)
			{
				if (this.CanClanBeDiscontinued(clan))
				{
					this.AddIndependentClan(clan);
				}
			}
			else if (this._independentClans.ContainsKey(clan))
			{
				this._independentClans.Remove(clan);
			}
			if (clan == Clan.PlayerClan && oldKingdom != null && this.CanKingdomBeDiscontinued(oldKingdom))
			{
				this.DiscontinueKingdom(oldKingdom);
			}
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0011DF54 File Offset: 0x0011C154
		private void DailyTickClan(Clan clan)
		{
			if (this._independentClans.ContainsKey(clan) && this._independentClans[clan].IsPast)
			{
				this.DiscontinueClan(clan);
			}
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x0011DF8C File Offset: 0x0011C18C
		private bool CanKingdomBeDiscontinued(Kingdom kingdom)
		{
			bool flag = !kingdom.IsEliminated && kingdom != Clan.PlayerClan.Kingdom && kingdom.Settlements.IsEmpty<Settlement>();
			if (flag)
			{
				CampaignEventDispatcher.Instance.CanKingdomBeDiscontinued(kingdom, ref flag);
			}
			return flag;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0011DFD0 File Offset: 0x0011C1D0
		private void DiscontinueKingdom(Kingdom kingdom)
		{
			foreach (Clan clan in new List<Clan>(kingdom.Clans))
			{
				this.FinalizeMapEvents(clan);
				ChangeKingdomAction.ApplyByLeaveByKingdomDestruction(clan, true);
			}
			kingdom.RulingClan = null;
			DestroyKingdomAction.Apply(kingdom);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0011E03C File Offset: 0x0011C23C
		private void FinalizeMapEvents(Clan clan)
		{
			foreach (WarPartyComponent warPartyComponent in clan.WarPartyComponents.ToList<WarPartyComponent>())
			{
				if ((warPartyComponent != null) & warPartyComponent.Party.IsActive)
				{
					if (warPartyComponent.MobileParty.MapEvent != null)
					{
						warPartyComponent.MobileParty.MapEvent.FinalizeEvent();
					}
					if (warPartyComponent.MobileParty.SiegeEvent != null)
					{
						warPartyComponent.MobileParty.SiegeEvent.FinalizeSiegeEvent();
					}
				}
			}
			foreach (Settlement settlement in clan.Settlements)
			{
				if (settlement.Party.MapEvent != null)
				{
					settlement.Party.MapEvent.FinalizeEvent();
				}
				if (settlement.Party.SiegeEvent != null)
				{
					settlement.Party.SiegeEvent.FinalizeSiegeEvent();
				}
			}
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0011E150 File Offset: 0x0011C350
		private bool CanClanBeDiscontinued(Clan clan)
		{
			return clan.Kingdom == null && !clan.IsRebelClan && !clan.IsBanditFaction && !clan.IsMinorFaction && clan != Clan.PlayerClan && clan.Settlements.IsEmpty<Settlement>();
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0011E187 File Offset: 0x0011C387
		private void DiscontinueClan(Clan clan)
		{
			DestroyClanAction.Apply(clan);
			this._independentClans.Remove(clan);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0011E19C File Offset: 0x0011C39C
		private void AddIndependentClan(Clan clan)
		{
			if (!this._independentClans.ContainsKey(clan))
			{
				this._independentClans.Add(clan, CampaignTime.DaysFromNow(28f));
			}
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x0011E1C2 File Offset: 0x0011C3C2
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Clan, CampaignTime>>("_independentClans", ref this._independentClans);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x0011E1D8 File Offset: 0x0011C3D8
		private void OnGameLoadFinished()
		{
			if (MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.2", 0))
			{
				foreach (Kingdom kingdom in Kingdom.All)
				{
					if (!kingdom.IsEliminated && this.CanKingdomBeDiscontinued(kingdom))
					{
						this.DiscontinueKingdom(kingdom);
					}
				}
			}
		}

		// Token: 0x040012ED RID: 4845
		private const float SurvivalDurationForIndependentClanInDays = 28f;

		// Token: 0x040012EE RID: 4846
		private Dictionary<Clan, CampaignTime> _independentClans = new Dictionary<Clan, CampaignTime>();
	}
}
