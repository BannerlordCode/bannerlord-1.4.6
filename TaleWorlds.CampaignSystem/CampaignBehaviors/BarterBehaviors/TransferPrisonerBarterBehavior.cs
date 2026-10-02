using System;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.BarterBehaviors
{
	// Token: 0x0200046C RID: 1132
	public class TransferPrisonerBarterBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600486D RID: 18541 RVA: 0x0016CE00 File Offset: 0x0016B000
		public override void RegisterEvents()
		{
			CampaignEvents.BarterablesRequested.AddNonSerializedListener(this, new Action<BarterData>(this.CheckForBarters));
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x0016CE19 File Offset: 0x0016B019
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x0016CE1C File Offset: 0x0016B01C
		public void CheckForBarters(BarterData args)
		{
			PartyBase offererParty = args.OffererParty;
			PartyBase otherParty = args.OtherParty;
			if (offererParty != null && otherParty != null)
			{
				foreach (CharacterObject characterObject in offererParty.PrisonerHeroes)
				{
					if (characterObject.IsHero && FactionManager.IsAtWarAgainstFaction(characterObject.HeroObject.MapFaction, otherParty.MapFaction))
					{
						Barterable barterable = new TransferPrisonerBarterable(characterObject.HeroObject, args.OffererHero, args.OffererParty, args.OtherHero, otherParty);
						args.AddBarterable<PrisonerBarterGroup>(barterable, false);
					}
				}
				foreach (CharacterObject characterObject2 in otherParty.PrisonerHeroes)
				{
					if (characterObject2.IsHero && FactionManager.IsAtWarAgainstFaction(characterObject2.HeroObject.MapFaction, offererParty.MapFaction))
					{
						Barterable barterable2 = new TransferPrisonerBarterable(characterObject2.HeroObject, args.OtherHero, args.OtherParty, args.OffererHero, offererParty);
						args.AddBarterable<PrisonerBarterGroup>(barterable2, false);
					}
				}
			}
		}
	}
}
