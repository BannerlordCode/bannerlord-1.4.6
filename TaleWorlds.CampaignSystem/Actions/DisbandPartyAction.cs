using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004AD RID: 1197
	public static class DisbandPartyAction
	{
		// Token: 0x06004A7F RID: 19071 RVA: 0x00178D9C File Offset: 0x00176F9C
		public static void StartDisband(MobileParty disbandParty)
		{
			if (disbandParty.IsDisbanding)
			{
				return;
			}
			if (disbandParty.MemberRoster.TotalManCount == 0)
			{
				DestroyPartyAction.Apply(null, disbandParty);
				return;
			}
			IDisbandPartyCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<IDisbandPartyCampaignBehavior>();
			if (campaignBehavior != null && campaignBehavior.IsPartyWaitingForDisband(disbandParty))
			{
				return;
			}
			if (disbandParty.Army != null)
			{
				if (disbandParty == disbandParty.Army.LeaderParty)
				{
					DisbandArmyAction.ApplyByUnknownReason(disbandParty.Army);
				}
				else
				{
					disbandParty.Army = null;
				}
			}
			TextObject textObject = new TextObject("{=ithcVNfA}{CLAN_NAME}{.o} Party", null);
			textObject.SetTextVariable("CLAN_NAME", (disbandParty.ActualClan != null) ? disbandParty.ActualClan.Name : CampaignData.NeutralFactionName);
			disbandParty.Party.SetCustomName(textObject);
			CampaignEventDispatcher.Instance.OnPartyDisbandStarted(disbandParty);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00178E52 File Offset: 0x00177052
		public static void CancelDisband(MobileParty disbandParty)
		{
			CampaignEventDispatcher.Instance.OnPartyDisbandCanceled(disbandParty);
			disbandParty.IsDisbanding = false;
			disbandParty.Party.SetCustomName(TextObject.GetEmpty());
			disbandParty.SetMoveModeHold();
		}
	}
}
