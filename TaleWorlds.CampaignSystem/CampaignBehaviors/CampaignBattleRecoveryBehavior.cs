using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D6 RID: 982
	public class CampaignBattleRecoveryBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003AF2 RID: 15090 RVA: 0x000F4E90 File Offset: 0x000F3090
		public override void RegisterEvents()
		{
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.DailyTickParty));
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000F4EC0 File Offset: 0x000F30C0
		private void DailyTickParty(MobileParty party)
		{
			if (!party.IsCurrentlyAtSea && MBRandom.RandomFloat < DefaultPerks.Medicine.Veterinarian.PrimaryBonus && party.HasPerk(DefaultPerks.Medicine.Veterinarian, false))
			{
				ItemModifier @object = MBObjectManager.Instance.GetObject<ItemModifier>("lame_horse");
				int num = MBRandom.RandomInt(party.ItemRoster.Count);
				for (int i = num; i < party.ItemRoster.Count + num; i++)
				{
					int num2 = i % party.ItemRoster.Count;
					ItemObject itemAtIndex = party.ItemRoster.GetItemAtIndex(num2);
					ItemRosterElement elementCopyAtIndex = party.ItemRoster.GetElementCopyAtIndex(num2);
					if (elementCopyAtIndex.EquipmentElement.ItemModifier == @object)
					{
						party.ItemRoster.AddToCounts(elementCopyAtIndex.EquipmentElement, -1);
						party.ItemRoster.Add(new ItemRosterElement(itemAtIndex, 1, null));
						return;
					}
				}
			}
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000F4F9C File Offset: 0x000F319C
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F4F9E File Offset: 0x000F319E
		private void OnMapEventEnded(MapEvent mapEvent)
		{
			this.CheckRecoveryForMapEventSide(mapEvent.AttackerSide);
			this.CheckRecoveryForMapEventSide(mapEvent.DefenderSide);
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F4FB8 File Offset: 0x000F31B8
		private void CheckRecoveryForMapEventSide(MapEventSide mapEventSide)
		{
			if (mapEventSide.MapEvent.EventType == MapEvent.BattleTypes.FieldBattle || mapEventSide.MapEvent.EventType == MapEvent.BattleTypes.Siege || mapEventSide.MapEvent.EventType == MapEvent.BattleTypes.SiegeOutside)
			{
				foreach (MapEventParty mapEventParty in mapEventSide.Parties)
				{
					PartyBase party = mapEventParty.Party;
					if (party.IsMobile)
					{
						MobileParty mobileParty = party.MobileParty;
						foreach (TroopRosterElement troopRosterElement in mapEventParty.WoundedInBattle.GetTroopRoster())
						{
							int num = mapEventParty.WoundedInBattle.FindIndexOfTroop(troopRosterElement.Character);
							int elementNumber = mapEventParty.WoundedInBattle.GetElementNumber(num);
							if (mobileParty.HasPerk(DefaultPerks.Medicine.BattleHardened, false))
							{
								float num2 = DefaultPerks.Medicine.BattleHardened.PrimaryBonus;
								if (mobileParty.IsCurrentlyAtSea)
								{
									num2 *= 0.5f;
								}
								this.GiveTroopXp(troopRosterElement, elementNumber, party, MathF.Round(num2));
							}
						}
						foreach (TroopRosterElement troopRosterElement2 in mapEventParty.DiedInBattle.GetTroopRoster())
						{
							int num3 = mapEventParty.DiedInBattle.FindIndexOfTroop(troopRosterElement2.Character);
							int elementNumber2 = mapEventParty.DiedInBattle.GetElementNumber(num3);
							if (!mobileParty.IsCurrentlyAtSea && mobileParty.HasPerk(DefaultPerks.Medicine.Veterinarian, false) && troopRosterElement2.Character.IsMounted)
							{
								this.RecoverMountWithChance(troopRosterElement2, elementNumber2, party);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000F51B0 File Offset: 0x000F33B0
		private void RecoverMountWithChance(TroopRosterElement troopRosterElement, int count, PartyBase party)
		{
			EquipmentElement equipmentElement = troopRosterElement.Character.Equipment[10];
			if (equipmentElement.Item != null)
			{
				for (int i = 0; i < count; i++)
				{
					if (MBRandom.RandomFloat < DefaultPerks.Medicine.Veterinarian.SecondaryBonus)
					{
						party.ItemRoster.AddToCounts(equipmentElement.Item, 1);
					}
				}
			}
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x000F520A File Offset: 0x000F340A
		private void GiveTroopXp(TroopRosterElement troopRosterElement, int count, PartyBase partyBase, int xp)
		{
			partyBase.MemberRoster.AddXpToTroop(troopRosterElement.Character, xp * count);
		}
	}
}
