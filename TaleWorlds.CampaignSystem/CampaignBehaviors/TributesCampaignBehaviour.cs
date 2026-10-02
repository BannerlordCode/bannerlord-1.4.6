using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200044B RID: 1099
	public class TributesCampaignBehaviour : CampaignBehaviorBase
	{
		// Token: 0x060046A3 RID: 18083 RVA: 0x0016162A File Offset: 0x0015F82A
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanEarnedGoldFromTributeEvent.AddNonSerializedListener(this, new Action<Clan, IFaction>(TributesCampaignBehaviour.OnClanEarnedGoldFromTribute));
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00161644 File Offset: 0x0015F844
		private static void OnClanEarnedGoldFromTribute(Clan clan, IFaction payerFaction)
		{
			StanceLink stanceWith = clan.MapFaction.GetStanceWith(payerFaction);
			if ((clan == Clan.PlayerClan || payerFaction == Clan.PlayerClan.MapFaction) && stanceWith.GetRemainingTributePaymentCount() == 0)
			{
				bool flag = payerFaction == Clan.PlayerClan.MapFaction;
				TextObject textObject = (flag ? new TextObject("{=LJFXfmpn}The tribute your kingdom owed to {ENEMY_FACTION} is now complete.", null) : new TextObject("{=aod7KVc8}The tribute {ENEMY_FACTION} owed to your kingdom is now complete.", null));
				IFaction faction = (flag ? clan.MapFaction : payerFaction);
				textObject.SetTextVariable("ENEMY_FACTION", faction.Name);
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new TributeFinishedMapNotification(textObject, faction));
			}
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x001616DD File Offset: 0x0015F8DD
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
