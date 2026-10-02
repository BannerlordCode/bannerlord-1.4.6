using System;
using TaleWorlds.CampaignSystem.Actions;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D8 RID: 984
	public class CampaignFactionManagerBehaviour : CampaignBehaviorBase
	{
		// Token: 0x06003B08 RID: 15112 RVA: 0x000F54D0 File Offset: 0x000F36D0
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.KingdomCreatedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomCreated));
			CampaignEvents.OnClanCreatedEvent.AddNonSerializedListener(this, new Action<Clan, bool>(this.OnClanCreated));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdomEvent));
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000F5550 File Offset: 0x000F3750
		private void OnClanChangedKingdomEvent(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool arg5)
		{
			CampaignFactionManagerBehaviour.RefreshFactionsAtWarWith();
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x000F5557 File Offset: 0x000F3757
		private void OnNewGameCreated(CampaignGameStarter obj)
		{
			CampaignFactionManagerBehaviour.RefreshFactionsAtWarWith();
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000F555E File Offset: 0x000F375E
		private void OnGameLoaded(CampaignGameStarter obj)
		{
			CampaignFactionManagerBehaviour.RefreshFactionsAtWarWith();
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000F5565 File Offset: 0x000F3765
		private void OnClanCreated(Clan obj, bool isCompanion)
		{
			CampaignFactionManagerBehaviour.RefreshFactionsAtWarWith();
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x000F556C File Offset: 0x000F376C
		private void OnKingdomCreated(Kingdom obj)
		{
			CampaignFactionManagerBehaviour.RefreshFactionsAtWarWith();
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x000F5574 File Offset: 0x000F3774
		private static void RefreshFactionsAtWarWith()
		{
			foreach (Kingdom kingdom in Kingdom.All)
			{
				kingdom.UpdateFactionsAtWarWith();
			}
			foreach (Clan clan in Clan.All)
			{
				clan.UpdateFactionsAtWarWith();
			}
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000F5604 File Offset: 0x000F3804
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
