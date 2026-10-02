using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D4 RID: 980
	public class BattleCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003AE1 RID: 15073 RVA: 0x000F462B File Offset: 0x000F282B
		public override void RegisterEvents()
		{
			CampaignEvents.OnHeroCombatHitEvent.AddNonSerializedListener(this, new Action<CharacterObject, CharacterObject, PartyBase, WeaponComponentData, bool, int>(BattleCampaignBehavior.OnHeroCombatHit));
			CampaignEvents.OnCollectLootsItemsEvent.AddNonSerializedListener(this, new Action<PartyBase, ItemRoster>(BattleCampaignBehavior.OnCollectLootItems));
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000F465C File Offset: 0x000F285C
		private static void OnCollectLootItems(PartyBase winnerParty, ItemRoster gainedLoots)
		{
			if (winnerParty.IsMobile && winnerParty.MobileParty.HasPerk(DefaultPerks.Engineering.Metallurgy, false))
			{
				foreach (ItemRosterElement itemRosterElement in gainedLoots.ToMBList<ItemRosterElement>())
				{
					ItemModifier itemModifier = itemRosterElement.EquipmentElement.ItemModifier;
					if (itemModifier != null && itemModifier.PriceMultiplier < 1f)
					{
						for (int i = 0; i < itemRosterElement.Amount; i++)
						{
							if (MBRandom.RandomFloat < DefaultPerks.Engineering.Metallurgy.PrimaryBonus)
							{
								gainedLoots.AddToCounts(itemRosterElement.EquipmentElement, -1);
								ItemRosterElement itemRosterElement2 = new ItemRosterElement(itemRosterElement.EquipmentElement.Item, 1, null);
								gainedLoots.Add(itemRosterElement2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000F474C File Offset: 0x000F294C
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000F4750 File Offset: 0x000F2950
		private static void OnHeroCombatHit(CharacterObject attacker, CharacterObject attacked, PartyBase party, WeaponComponentData attackerWeapon, bool isFatal, int xpGained)
		{
			if (isFatal && attackerWeapon != null && party.MemberRoster.TotalRegulars > 0 && BattleCampaignBehavior.IsWeaponSuitableToGetBaptisedInBloodPerkBonus(attackerWeapon) && attacker.HeroObject.GetPerkValue(DefaultPerks.TwoHanded.BaptisedInBlood))
			{
				for (int i = 0; i < party.MemberRoster.Count; i++)
				{
					TroopRosterElement elementCopyAtIndex = party.MemberRoster.GetElementCopyAtIndex(i);
					if (!elementCopyAtIndex.Character.IsHero && elementCopyAtIndex.Character.IsInfantry)
					{
						party.MemberRoster.AddXpToTroopAtIndex(i, (int)DefaultPerks.TwoHanded.BaptisedInBlood.PrimaryBonus * elementCopyAtIndex.Number);
					}
				}
			}
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000F47EE File Offset: 0x000F29EE
		private static bool IsWeaponSuitableToGetBaptisedInBloodPerkBonus(WeaponComponentData attackerWeapon)
		{
			return attackerWeapon.WeaponClass == WeaponClass.TwoHandedSword || attackerWeapon.WeaponClass == WeaponClass.TwoHandedPolearm || attackerWeapon.WeaponClass == WeaponClass.TwoHandedAxe || attackerWeapon.WeaponClass == WeaponClass.TwoHandedMace;
		}
	}
}
