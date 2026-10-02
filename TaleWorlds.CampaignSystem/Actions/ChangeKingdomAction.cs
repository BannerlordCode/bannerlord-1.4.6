using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200049B RID: 1179
	public static class ChangeKingdomAction
	{
		// Token: 0x06004A25 RID: 18981 RVA: 0x001773A8 File Offset: 0x001755A8
		private static void ApplyInternal(Clan clan, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, CampaignTime shouldStayInKingdomUntil, int awardMultiplier = 0, bool byRebellion = false, bool showNotification = true)
		{
			Kingdom kingdom = clan.Kingdom;
			clan.DebtToKingdom = 0;
			if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdom || detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinAsMercenary || detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdomByDefection)
			{
				clan.ShouldStayInKingdomUntil = shouldStayInKingdomUntil;
				FactionHelper.AdjustFactionStancesForClanJoiningKingdom(clan, newKingdom);
			}
			else
			{
				clan.ShouldStayInKingdomUntil = CampaignTime.Zero;
			}
			if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdom || detail == ChangeKingdomAction.ChangeKingdomActionDetail.CreateKingdom || detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdomByDefection)
			{
				if (clan.IsUnderMercenaryService)
				{
					EndMercenaryServiceAction.EndByDefault(clan);
				}
				if (kingdom != null)
				{
					clan.ClanLeaveKingdom(!byRebellion);
				}
				if (newKingdom != null && detail == ChangeKingdomAction.ChangeKingdomActionDetail.CreateKingdom)
				{
					ChangeRulingClanAction.Apply(newKingdom, clan);
				}
				clan.Kingdom = newKingdom;
			}
			else if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.JoinAsMercenary)
			{
				StartMercenaryServiceAction.ApplyByDefault(clan, newKingdom, awardMultiplier);
			}
			else if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveWithRebellion || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveKingdom || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveAsMercenary || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveByClanDestruction || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveByKingdomDestruction)
			{
				clan.Kingdom = null;
				bool flag = false;
				if (clan.IsUnderMercenaryService)
				{
					flag = true;
					EndMercenaryServiceAction.EndByLeavingKingdom(clan);
				}
				if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveWithRebellion)
				{
					DeclareWarAction.ApplyByRebellion(kingdom, clan);
					using (List<IFaction>.Enumerator enumerator = kingdom.FactionsAtWarWith.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IFaction faction = enumerator.Current;
							if (faction != clan && !clan.IsAtWarWith(faction))
							{
								DeclareWarAction.ApplyByDefault(clan, faction);
							}
						}
						goto IL_0298;
					}
				}
				if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveKingdom)
				{
					using (List<Settlement>.Enumerator enumerator2 = new List<Settlement>(clan.Settlements).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Settlement settlement = enumerator2.Current;
							ChangeOwnerOfSettlementAction.ApplyByLeaveFaction(kingdom.Leader, settlement);
							foreach (Hero hero in new List<Hero>(settlement.HeroesWithoutParty))
							{
								if (hero.CurrentSettlement != null && hero.Clan == clan)
								{
									if (hero.PartyBelongedTo != null)
									{
										LeaveSettlementAction.ApplyForParty(hero.PartyBelongedTo);
										EnterSettlementAction.ApplyForParty(hero.PartyBelongedTo, clan.Leader.HomeSettlement);
									}
									else
									{
										LeaveSettlementAction.ApplyForCharacterOnly(hero);
										EnterSettlementAction.ApplyForCharacterOnly(hero, clan.Leader.HomeSettlement);
									}
								}
							}
						}
						goto IL_0298;
					}
				}
				if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveByKingdomDestruction)
				{
					if (flag)
					{
						using (List<IFaction>.Enumerator enumerator = kingdom.FactionsAtWarWith.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IFaction faction2 = enumerator.Current;
								if (clan != faction2 && !Campaign.Current.Models.DiplomacyModel.IsAtConstantWar(clan, faction2))
								{
									MakePeaceAction.Apply(clan, faction2);
								}
							}
							goto IL_0298;
						}
					}
					foreach (IFaction faction3 in kingdom.FactionsAtWarWith)
					{
						if (clan != faction3 && !clan.GetStanceWith(faction3).IsAtWar)
						{
							DeclareWarAction.ApplyByDefault(clan, faction3);
						}
					}
				}
			}
			IL_0298:
			if (detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveAsMercenary || detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveKingdom)
			{
				foreach (IFaction faction4 in clan.FactionsAtWarWith.ToList<IFaction>())
				{
					if (clan != faction4 && !Campaign.Current.Models.DiplomacyModel.IsAtConstantWar(clan, faction4))
					{
						MakePeaceAction.Apply(clan, faction4);
						FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(clan, faction4);
						FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(faction4, clan);
					}
				}
				ChangeKingdomAction.CheckIfPartyIconIsDirty(clan, kingdom);
			}
			foreach (WarPartyComponent warPartyComponent in clan.WarPartyComponents)
			{
				if (warPartyComponent.MobileParty.MapEvent == null)
				{
					warPartyComponent.MobileParty.SetMoveModeHold();
				}
			}
			CampaignEventDispatcher.Instance.OnClanChangedKingdom(clan, kingdom, newKingdom, detail, showNotification);
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x0017777C File Offset: 0x0017597C
		public static void ApplyByJoinToKingdom(Clan clan, Kingdom newKingdom, CampaignTime shouldStayInKingdomUntil = default(CampaignTime), bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdom, shouldStayInKingdomUntil, 0, false, showNotification);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x0017778A File Offset: 0x0017598A
		public static void ApplyByJoinToKingdomByDefection(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, CampaignTime shouldStayInKingdomUntil = default(CampaignTime), bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail.JoinKingdomByDefection, shouldStayInKingdomUntil, 0, false, showNotification);
			CampaignEventDispatcher.Instance.OnClanDefected(clan, oldKingdom, newKingdom);
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x001777A6 File Offset: 0x001759A6
		public static void ApplyByCreateKingdom(Clan clan, Kingdom newKingdom, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail.CreateKingdom, CampaignTime.Zero, 0, false, showNotification);
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x001777B8 File Offset: 0x001759B8
		public static void ApplyByLeaveByKingdomDestruction(Clan clan, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, null, ChangeKingdomAction.ChangeKingdomActionDetail.LeaveByKingdomDestruction, CampaignTime.Zero, 0, false, showNotification);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x001777CA File Offset: 0x001759CA
		public static void ApplyByLeaveKingdom(Clan clan, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, null, ChangeKingdomAction.ChangeKingdomActionDetail.LeaveKingdom, CampaignTime.Zero, 0, false, showNotification);
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x001777DC File Offset: 0x001759DC
		public static void ApplyByLeaveWithRebellionAgainstKingdom(Clan clan, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, null, ChangeKingdomAction.ChangeKingdomActionDetail.LeaveWithRebellion, CampaignTime.Zero, 0, false, showNotification);
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x001777EE File Offset: 0x001759EE
		public static void ApplyByJoinFactionAsMercenary(Clan clan, Kingdom newKingdom, CampaignTime shouldStayInKingdomUntil = default(CampaignTime), int awardMultiplier = 50, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail.JoinAsMercenary, shouldStayInKingdomUntil, awardMultiplier, false, showNotification);
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x001777FD File Offset: 0x001759FD
		public static void ApplyByLeaveKingdomAsMercenary(Clan mercenaryClan, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(mercenaryClan, null, ChangeKingdomAction.ChangeKingdomActionDetail.LeaveAsMercenary, CampaignTime.Zero, 0, false, showNotification);
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0017780F File Offset: 0x00175A0F
		public static void ApplyByLeaveKingdomByClanDestruction(Clan clan, bool showNotification = true)
		{
			ChangeKingdomAction.ApplyInternal(clan, null, ChangeKingdomAction.ChangeKingdomActionDetail.LeaveByClanDestruction, CampaignTime.Zero, 0, false, showNotification);
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x00177824 File Offset: 0x00175A24
		private static void CheckIfPartyIconIsDirty(Clan clan, Kingdom oldKingdom)
		{
			IFaction faction;
			if (clan.Kingdom == null)
			{
				faction = clan;
			}
			else
			{
				IFaction kingdom = clan.Kingdom;
				faction = kingdom;
			}
			IFaction faction2 = faction;
			IFaction faction3 = oldKingdom ?? clan;
			foreach (MobileParty mobileParty in MobileParty.All)
			{
				if (mobileParty.IsVisible && ((mobileParty.Party.Owner != null && mobileParty.Party.Owner.Clan == clan) || (clan == Clan.PlayerClan && ((!FactionManager.IsAtWarAgainstFaction(mobileParty.MapFaction, faction2) && FactionManager.IsAtWarAgainstFaction(mobileParty.MapFaction, faction3)) || (FactionManager.IsAtWarAgainstFaction(mobileParty.MapFaction, faction2) && !FactionManager.IsAtWarAgainstFaction(mobileParty.MapFaction, faction3))))))
				{
					mobileParty.Party.SetVisualAsDirty();
				}
			}
			foreach (Settlement settlement in clan.Settlements)
			{
				settlement.Party.SetVisualAsDirty();
			}
		}

		// Token: 0x04001475 RID: 5237
		public const float PotentialSettlementsPerNobleEffect = 0.2f;

		// Token: 0x04001476 RID: 5238
		public const float NewGainedFiefsValueForKingdomConstant = 0.1f;

		// Token: 0x04001477 RID: 5239
		public const float LordsUnitStrengthValue = 20f;

		// Token: 0x04001478 RID: 5240
		public const float MercenaryUnitStrengthValue = 5f;

		// Token: 0x04001479 RID: 5241
		public const float MinimumNeededGoldForRecruitingMercenaries = 20000f;

		// Token: 0x0200088C RID: 2188
		public enum ChangeKingdomActionDetail
		{
			// Token: 0x0400247C RID: 9340
			JoinAsMercenary,
			// Token: 0x0400247D RID: 9341
			JoinKingdom,
			// Token: 0x0400247E RID: 9342
			JoinKingdomByDefection,
			// Token: 0x0400247F RID: 9343
			LeaveKingdom,
			// Token: 0x04002480 RID: 9344
			LeaveWithRebellion,
			// Token: 0x04002481 RID: 9345
			LeaveAsMercenary,
			// Token: 0x04002482 RID: 9346
			LeaveByClanDestruction,
			// Token: 0x04002483 RID: 9347
			CreateKingdom,
			// Token: 0x04002484 RID: 9348
			LeaveByKingdomDestruction
		}
	}
}
