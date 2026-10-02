using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000452 RID: 1106
	public class VillageTradeBoundCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600478F RID: 18319 RVA: 0x001676E4 File Offset: 0x001658E4
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.WarDeclared));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnMakePeace));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.ClanChangedKingdom));
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00167792 File Offset: 0x00165992
		private void OnClanDestroyed(Clan obj)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0016779A File Offset: 0x0016599A
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x0016779C File Offset: 0x0016599C
		private void ClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x001677A4 File Offset: 0x001659A4
		private void OnGameLoaded(CampaignGameStarter obj)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x001677AC File Offset: 0x001659AC
		private void OnMakePeace(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x001677B4 File Offset: 0x001659B4
		private void WarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail declareWarDetail)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x001677BC File Offset: 0x001659BC
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x001677C4 File Offset: 0x001659C4
		public void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.UpdateTradeBounds();
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x001677CC File Offset: 0x001659CC
		private void UpdateTradeBounds()
		{
			foreach (Town town in Campaign.Current.AllCastles)
			{
				foreach (Village village in town.Villages)
				{
					village.TradeBound = Campaign.Current.Models.VillageTradeModel.GetTradeBoundToAssignForVillage(village);
				}
			}
		}
	}
}
