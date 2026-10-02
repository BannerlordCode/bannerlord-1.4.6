using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003F0 RID: 1008
	public class FindingItemOnMapBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003F5F RID: 16223 RVA: 0x0011E267 File Offset: 0x0011C467
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.DailyTickParty));
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0011E280 File Offset: 0x0011C480
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0011E284 File Offset: 0x0011C484
		public void DailyTickParty(MobileParty party)
		{
			if (MBRandom.RandomFloat < DefaultPerks.Scouting.BeastWhisperer.PrimaryBonus && party.HasPerk(DefaultPerks.Scouting.BeastWhisperer, false))
			{
				TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
				if (faceTerrainType == TerrainType.Steppe || faceTerrainType == TerrainType.Plain)
				{
					ItemObject randomElementWithPredicate = Items.All.GetRandomElementWithPredicate<ItemObject>((ItemObject x) => x.IsMountable && !x.NotMerchandise);
					if (randomElementWithPredicate != null)
					{
						party.ItemRoster.AddToCounts(randomElementWithPredicate, 1);
						if (party.IsMainParty)
						{
							TextObject textObject = new TextObject("{=vl9bawa7}{COUNT} {?(COUNT > 1)}{PLURAL(ANIMAL_NAME)} are{?}{ANIMAL_NAME} is{\\?} added to your party.", null);
							textObject.SetTextVariable("COUNT", 1);
							textObject.SetTextVariable("ANIMAL_NAME", randomElementWithPredicate.Name);
							InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
						}
					}
				}
			}
		}
	}
}
