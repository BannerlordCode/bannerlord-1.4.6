using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200013F RID: 319
	public class ClanPartiesVM : ViewModel
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0006ECAB File Offset: 0x0006CEAB
		// (set) Token: 0x06001DFF RID: 7679 RVA: 0x0006ECB3 File Offset: 0x0006CEB3
		public int TotalExpense { get; private set; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0006ECBC File Offset: 0x0006CEBC
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x0006ECC4 File Offset: 0x0006CEC4
		public int TotalIncome { get; private set; }

		// Token: 0x06001E02 RID: 7682 RVA: 0x0006ECD0 File Offset: 0x0006CED0
		public ClanPartiesVM(Action onExpenseChange, Action<Hero> openPartyAsManage, Action onRefresh, Action<ClanCardSelectionInfo> openCardSelectionPopup)
		{
			this._onExpenseChange = onExpenseChange;
			this._onRefresh = onRefresh;
			this._disbandBehavior = Campaign.Current.GetCampaignBehavior<IDisbandPartyCampaignBehavior>();
			this._teleportationBehavior = Campaign.Current.GetCampaignBehavior<ITeleportationCampaignBehavior>();
			this._openPartyAsManage = openPartyAsManage;
			this._openCardSelectionPopup = openCardSelectionPopup;
			this._faction = Hero.MainHero.Clan;
			this.Parties = new MBBindingList<ClanPartyItemVM>();
			this.Garrisons = new MBBindingList<ClanPartyItemVM>();
			this.Caravans = new MBBindingList<ClanPartyItemVM>();
			MBBindingList<MBBindingList<ClanPartyItemVM>> mbbindingList = new MBBindingList<MBBindingList<ClanPartyItemVM>> { this.Parties, this.Garrisons, this.Caravans };
			this.SortController = new ClanPartiesSortControllerVM(mbbindingList);
			this.CreateNewPartyActionHint = new HintViewModel();
			this.RefreshPartiesList();
			this.RefreshValues();
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0006EDD8 File Offset: 0x0006CFD8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SizeText = GameTexts.FindText("str_clan_party_size", null).ToString();
			this.MoraleText = GameTexts.FindText("str_morale", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.CreateNewPartyText = GameTexts.FindText("str_clan_create_new_party", null).ToString();
			this.GarrisonsText = GameTexts.FindText("str_clan_garrisons", null).ToString();
			this.CaravansText = GameTexts.FindText("str_clan_caravans", null).ToString();
			this.RefreshPartiesList();
			this.Parties.ApplyActionOnAllItems(delegate(ClanPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.Garrisons.ApplyActionOnAllItems(delegate(ClanPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.Caravans.ApplyActionOnAllItems(delegate(ClanPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.SortController.RefreshValues();
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0006EF14 File Offset: 0x0006D114
		public void RefreshTotalExpense()
		{
			IEnumerable<ClanPartyItemVM> enumerable = from p in this.Parties.Union<ClanPartyItemVM>(this.Garrisons).Union<ClanPartyItemVM>(this.Caravans)
				where p.ShouldPartyHaveExpense
				select p;
			int num;
			if (enumerable == null)
			{
				num = 0;
			}
			else
			{
				num = enumerable.Sum<ClanPartyItemVM>((ClanPartyItemVM p) => p.Expense);
			}
			this.TotalExpense = num;
			this.TotalIncome = this.Caravans.Sum<ClanPartyItemVM>((ClanPartyItemVM p) => p.Income);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0006EFC4 File Offset: 0x0006D1C4
		public void RefreshPartiesList()
		{
			this.Parties.Clear();
			this.Garrisons.Clear();
			this.Caravans.Clear();
			this.SortController.ResetAllStates();
			foreach (WarPartyComponent warPartyComponent in this._faction.WarPartyComponents)
			{
				if (warPartyComponent.MobileParty == MobileParty.MainParty)
				{
					this.Parties.Insert(0, new ClanPartyItemVM(warPartyComponent.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Main, this._disbandBehavior, this._teleportationBehavior));
				}
				else
				{
					this.Parties.Add(new ClanPartyItemVM(warPartyComponent.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Member, this._disbandBehavior, this._teleportationBehavior));
				}
			}
			using (IEnumerator<CaravanPartyComponent> enumerator2 = this._faction.Heroes.SelectMany<Hero, CaravanPartyComponent>((Hero h) => h.OwnedCaravans).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					CaravanPartyComponent party = enumerator2.Current;
					if (!this.Caravans.Any<ClanPartyItemVM>((ClanPartyItemVM c) => c.Party.MobileParty == party.MobileParty))
					{
						this.Caravans.Add(new ClanPartyItemVM(party.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Caravan, this._disbandBehavior, this._teleportationBehavior));
					}
				}
			}
			using (IEnumerator<MobileParty> enumerator3 = (from a in this._faction.Settlements
				where a.Town != null
				select a into s
				select s.Town.GarrisonParty).GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					MobileParty garrison = enumerator3.Current;
					if (garrison != null && !this.Garrisons.Any<ClanPartyItemVM>((ClanPartyItemVM c) => c.Party == garrison.Party))
					{
						this.Garrisons.Add(new ClanPartyItemVM(garrison.Party, new Action<ClanPartyItemVM>(this.OnPartySelection), new Action(this.OnAnyExpenseChange), new Action(this.OnShowChangeLeaderPopup), ClanPartyItemVM.ClanPartyType.Garrison, this._disbandBehavior, this._teleportationBehavior));
					}
				}
			}
			int count = this._faction.WarPartyComponents.Count;
			this._faction.Heroes.Where<Hero>((Hero h) => !h.IsDisabled).Union<Hero>(this._faction.Companions).Any<Hero>((Hero h) => h.IsActive && h.PartyBelongedToAsPrisoner == null && !h.IsChild && h.CanLeadParty() && (h.PartyBelongedTo == null || h.PartyBelongedTo.LeaderHero != h));
			TextObject textObject;
			this.CanCreateNewParty = this.GetCanCreateNewParty(out textObject);
			this.CreateNewPartyActionHint.HintText = textObject;
			GameTexts.SetVariable("CURRENT", count);
			GameTexts.SetVariable("LIMIT", this._faction.WarPartyLimit);
			this.PartiesText = GameTexts.FindText("str_clan_parties", null).ToString();
			GameTexts.SetVariable("CURRENT", this.Caravans.Count);
			this.CaravansText = GameTexts.FindText("str_clan_caravans", null).ToString();
			GameTexts.SetVariable("CURRENT", this.Garrisons.Count);
			this.GarrisonsText = GameTexts.FindText("str_clan_garrisons", null).ToString();
			this.OnPartySelection(this.GetDefaultMember());
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0006F3F0 File Offset: 0x0006D5F0
		private bool GetCanCreateNewParty(out TextObject disabledReason)
		{
			IEnumerable<Hero> enumerable = from h in this._faction.Heroes.Where<Hero>((Hero h) => !h.IsDisabled).Union<Hero>(this._faction.Companions)
				where h.IsActive && h.PartyBelongedToAsPrisoner == null && !h.IsChild && h.CanLeadParty() && (h.PartyBelongedTo == null || h.PartyBelongedTo.LeaderHero != h)
				select h;
			bool flag = !enumerable.IsEmpty<Hero>();
			int partyGoldLowerThreshold = Campaign.Current.Models.ClanFinanceModel.PartyGoldLowerThreshold;
			bool flag2 = enumerable.Any<Hero>((Hero h) => Hero.MainHero.Gold > partyGoldLowerThreshold - h.Gold);
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (MobileParty.MainParty.IsCurrentlyAtSea || MobileParty.MainParty.IsInRaftState)
			{
				disabledReason = GameTexts.FindText("str_cannot_perform_action_while_sailing", null);
				return false;
			}
			if (this._faction.WarPartyLimit - this._faction.WarPartyComponents.Count <= 0)
			{
				disabledReason = GameTexts.FindText("str_clan_doesnt_have_empty_party_slots", null);
				return false;
			}
			if (!flag)
			{
				disabledReason = GameTexts.FindText("str_clan_doesnt_have_available_heroes", null);
				return false;
			}
			if (!flag2)
			{
				disabledReason = new TextObject("{=VSUqbvbE}You don't have enough gold to create a new party.", null);
				return false;
			}
			disabledReason = TextObject.GetEmpty();
			return true;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0006F52B File Offset: 0x0006D72B
		private void OnAnyExpenseChange()
		{
			this.RefreshTotalExpense();
			this._onExpenseChange();
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0006F53E File Offset: 0x0006D73E
		private ClanPartyItemVM GetDefaultMember()
		{
			return this.Parties.FirstOrDefault<ClanPartyItemVM>();
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0006F54B File Offset: 0x0006D74B
		public void ExecuteCreateNewParty()
		{
			if (this.CanCreateNewParty)
			{
				if (this.GetNewPartyLeaderCandidates().Any<ClanCardSelectionItemInfo>())
				{
					this.OnShowNewPartyPopup();
					return;
				}
				MBInformationManager.AddQuickInformation(new TextObject("{=qZvNIVGV}There is no one available in your clan who can lead a party right now.", null), 0, null, null, "");
			}
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0006F584 File Offset: 0x0006D784
		public void SelectParty(PartyBase party)
		{
			foreach (ClanPartyItemVM clanPartyItemVM in this.Parties)
			{
				if (clanPartyItemVM.Party == party)
				{
					this.OnPartySelection(clanPartyItemVM);
					break;
				}
			}
			foreach (ClanPartyItemVM clanPartyItemVM2 in this.Caravans)
			{
				if (clanPartyItemVM2.Party == party)
				{
					this.OnPartySelection(clanPartyItemVM2);
					break;
				}
			}
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0006F624 File Offset: 0x0006D824
		private void OnPartySelection(ClanPartyItemVM party)
		{
			if (this.CurrentSelectedParty != null)
			{
				this.CurrentSelectedParty.IsSelected = false;
			}
			this.CurrentSelectedParty = party;
			if (party != null)
			{
				party.IsSelected = true;
			}
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0006F64C File Offset: 0x0006D84C
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Parties.ApplyActionOnAllItems(delegate(ClanPartyItemVM p)
			{
				p.OnFinalize();
			});
			this.Garrisons.ApplyActionOnAllItems(delegate(ClanPartyItemVM p)
			{
				p.OnFinalize();
			});
			this.Caravans.ApplyActionOnAllItems(delegate(ClanPartyItemVM p)
			{
				p.OnFinalize();
			});
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0006F6E0 File Offset: 0x0006D8E0
		public void OnShowNewPartyPopup()
		{
			ClanCardSelectionInfo clanCardSelectionInfo = new ClanCardSelectionInfo(new TextObject("{=0Q4Xo2BQ}Select the Leader of the New Party", null), this.GetNewPartyLeaderCandidates(), new Action<List<object>, Action>(this.OnNewPartyCreationOver), false, 1, 0);
			Action<ClanCardSelectionInfo> openCardSelectionPopup = this._openCardSelectionPopup;
			if (openCardSelectionPopup == null)
			{
				return;
			}
			openCardSelectionPopup(clanCardSelectionInfo);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0006F725 File Offset: 0x0006D925
		private IEnumerable<ClanCardSelectionItemInfo> GetNewPartyLeaderCandidates()
		{
			int partyGoldLowerThreshold = Campaign.Current.Models.ClanFinanceModel.PartyGoldLowerThreshold;
			foreach (Hero hero in this._faction.Heroes.Where<Hero>((Hero h) => !h.IsDisabled).Union<Hero>(this._faction.Companions))
			{
				if ((hero.IsActive || hero.IsReleased || hero.IsFugitive) && !hero.IsChild && hero != Hero.MainHero && hero.CanBeGovernorOrHavePartyRole())
				{
					bool flag = false;
					TextObject textObject = TextObject.GetEmpty();
					if (hero.PartyBelongedToAsPrisoner != null)
					{
						textObject = new TextObject("{=vOojEcIf}You cannot assign a prisoner member as a new party leader", null);
					}
					else if (hero.IsReleased)
					{
						textObject = new TextObject("{=OhNYkblK}This hero has just escaped from captors and will be available after some time.", null);
					}
					else if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero == hero)
					{
						textObject = new TextObject("{=aFYwbosi}This hero is already leading a party.", null);
					}
					else if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.LeaderHero != Hero.MainHero)
					{
						textObject = new TextObject("{=FjJi1DJb}This hero is already a part of an another party.", null);
					}
					else if (hero.GovernorOf != null)
					{
						textObject = new TextObject("{=Hz8XO8wk}Governors cannot lead a mobile party and be a governor at the same time.", null);
					}
					else if (hero.HeroState == Hero.CharacterStates.Disabled)
					{
						textObject = new TextObject("{=slzfQzl3}This hero is lost", null);
					}
					else if (hero.HeroState == Hero.CharacterStates.Fugitive)
					{
						textObject = new TextObject("{=dD3kRDHi}This hero is a fugitive and running from their captors. They will be available after some time.", null);
					}
					else if (partyGoldLowerThreshold - hero.Gold > Hero.MainHero.Gold)
					{
						textObject = new TextObject("{=xpCdwmlX}You don't have enough gold to make {HERO.NAME} a party leader.", null);
						textObject.SetCharacterProperties("HERO", hero.CharacterObject, false);
					}
					else if (hero.PartyBelongedTo != null && hero.PartyBelongedTo.IsCurrentlyAtSea)
					{
						textObject = new TextObject("{=1ELK1UbN}{HERO.NAME} is currently sailing.", null);
						textObject.SetCharacterProperties("HERO", hero.CharacterObject, false);
					}
					else
					{
						flag = true;
					}
					yield return new ClanCardSelectionItemInfo(hero, hero.Name, new CharacterImageIdentifier(CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false)), CardSelectionItemSpriteType.None, null, null, this.GetNewPartyLeaderCandidateProperties(hero), !flag, textObject, null);
				}
			}
			IEnumerator<Hero> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x0006F735 File Offset: 0x0006D935
		private IEnumerable<ClanCardSelectionItemPropertyInfo> GetNewPartyLeaderCandidateProperties(Hero hero)
		{
			yield return new ClanCardSelectionItemPropertyInfo(TextObject.GetEmpty());
			TextObject textObject = new TextObject("{=hwrQqWir}No Skills", null);
			int num = 0;
			foreach (SkillObject skillObject in this._leaderAssignmentRelevantSkills)
			{
				TextObject textObject2 = new TextObject("{=!}{SKILL_VALUE}", null);
				textObject2.SetTextVariable("SKILL_VALUE", hero.GetSkillValue(skillObject));
				TextObject textObject3 = ClanCardSelectionItemPropertyInfo.CreateLabeledValueText(skillObject.Name, textObject2);
				if (num == 0)
				{
					textObject = textObject3;
				}
				else
				{
					TextObject textObject4 = GameTexts.FindText("str_string_newline_newline_string", null);
					textObject4.SetTextVariable("STR1", textObject);
					textObject4.SetTextVariable("STR2", textObject3);
					textObject = textObject4;
				}
				num++;
			}
			yield return new ClanCardSelectionItemPropertyInfo(GameTexts.FindText("str_skills", null), textObject);
			yield break;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0006F74C File Offset: 0x0006D94C
		private void OnNewPartyCreationOver(List<object> selectedItems, Action closePopup)
		{
			if (selectedItems.Count == 1)
			{
				Hero newLeader = selectedItems.FirstOrDefault<object>() as Hero;
				int partyGoldLowerThreshold = Campaign.Current.Models.ClanFinanceModel.PartyGoldLowerThreshold;
				if (newLeader.Gold < partyGoldLowerThreshold)
				{
					string text = new TextObject("{=DAYoD0aW}Create Party", null).ToString();
					string text2 = new TextObject("{=fRz2DJf4}Creating the party will cost you {PARTY_COST}{GOLD_ICON}. Are you sure?", null).SetTextVariable("PARTY_COST", partyGoldLowerThreshold - newLeader.Gold).SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">").ToString();
					InformationManager.ShowInquiry(new InquiryData(text, text2, true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=3CpNUnVl}Cancel", null).ToString(), delegate
					{
						Action closePopup4 = closePopup;
						if (closePopup4 != null)
						{
							closePopup4();
						}
						this.CreateNewClanParty(newLeader, partyGoldLowerThreshold);
					}, null, "", 0f, null, null, null), false, false);
					return;
				}
				Action closePopup2 = closePopup;
				if (closePopup2 != null)
				{
					closePopup2();
				}
				this.CreateNewClanParty(newLeader, partyGoldLowerThreshold);
				return;
			}
			else
			{
				Action closePopup3 = closePopup;
				if (closePopup3 == null)
				{
					return;
				}
				closePopup3();
				return;
			}
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0006F898 File Offset: 0x0006DA98
		private void CreateNewClanParty(Hero newLeader, int partyGoldLowerThreshold)
		{
			if (newLeader.PartyBelongedTo == MobileParty.MainParty)
			{
				this._openPartyAsManage(newLeader);
				return;
			}
			MobileParty mobileParty = MobilePartyHelper.CreateNewClanMobileParty(newLeader, this._faction);
			if (newLeader.Gold < partyGoldLowerThreshold)
			{
				GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, newLeader, partyGoldLowerThreshold - newLeader.Gold, false);
			}
			mobileParty.SetMoveModeHold();
			this._onRefresh();
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0006F8FC File Offset: 0x0006DAFC
		public void OnShowChangeLeaderPopup()
		{
			ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
			bool flag;
			if (currentSelectedParty == null)
			{
				flag = null != null;
			}
			else
			{
				PartyBase party = currentSelectedParty.Party;
				flag = ((party != null) ? party.MobileParty : null) != null;
			}
			if (flag)
			{
				ClanCardSelectionInfo clanCardSelectionInfo = new ClanCardSelectionInfo(GameTexts.FindText("str_change_party_leader", null), this.GetChangeLeaderCandidates(), new Action<List<object>, Action>(this.OnChangeLeaderOver), false, 1, 0);
				Action<ClanCardSelectionInfo> openCardSelectionPopup = this._openCardSelectionPopup;
				if (openCardSelectionPopup == null)
				{
					return;
				}
				openCardSelectionPopup(clanCardSelectionInfo);
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0006F961 File Offset: 0x0006DB61
		private IEnumerable<ClanCardSelectionItemInfo> GetChangeLeaderCandidates()
		{
			TextObject textObject;
			bool canDisbandParty = this.GetCanDisbandParty(out textObject);
			yield return new ClanCardSelectionItemInfo(GameTexts.FindText("str_disband_party", null), !canDisbandParty, textObject, null);
			foreach (Hero hero in this._faction.Heroes.Where<Hero>((Hero h) => !h.IsDisabled).Union<Hero>(this._faction.Companions))
			{
				if ((hero.IsActive || hero.IsReleased || hero.IsFugitive || hero.IsTraveling) && !hero.IsChild && hero != Hero.MainHero && hero.CanLeadParty())
				{
					Hero hero2 = hero;
					ClanPartyMemberItemVM leaderMember = this.CurrentSelectedParty.LeaderMember;
					if (hero2 != ((leaderMember != null) ? leaderMember.HeroObject : null))
					{
						TextObject textObject2;
						bool flag = FactionHelper.IsMainClanMemberAvailableForPartyLeaderChange(hero, true, this.CurrentSelectedParty.Party.MobileParty, out textObject2);
						CharacterImageIdentifier characterImageIdentifier = new CharacterImageIdentifier(CampaignUIHelper.GetCharacterCode(hero.CharacterObject, false));
						yield return new ClanCardSelectionItemInfo(hero, hero.Name, characterImageIdentifier, CardSelectionItemSpriteType.None, null, null, this.GetChangeLeaderCandidateProperties(hero), !flag, textObject2, null);
					}
				}
			}
			IEnumerator<Hero> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x0006F971 File Offset: 0x0006DB71
		private IEnumerable<ClanCardSelectionItemPropertyInfo> GetChangeLeaderCandidateProperties(Hero hero)
		{
			TextObject teleportationDelayText = CampaignUIHelper.GetTeleportationDelayText(hero, this.CurrentSelectedParty.Party);
			yield return new ClanCardSelectionItemPropertyInfo(teleportationDelayText);
			TextObject textObject = new TextObject("{=hwrQqWir}No Skills", null);
			int num = 0;
			foreach (SkillObject skillObject in this._leaderAssignmentRelevantSkills)
			{
				TextObject textObject2 = new TextObject("{=!}{SKILL_VALUE}", null);
				textObject2.SetTextVariable("SKILL_VALUE", hero.GetSkillValue(skillObject));
				TextObject textObject3 = ClanCardSelectionItemPropertyInfo.CreateLabeledValueText(skillObject.Name, textObject2);
				if (num == 0)
				{
					textObject = textObject3;
				}
				else
				{
					TextObject textObject4 = GameTexts.FindText("str_string_newline_newline_string", null);
					textObject4.SetTextVariable("STR1", textObject);
					textObject4.SetTextVariable("STR2", textObject3);
					textObject = textObject4;
				}
				num++;
			}
			yield return new ClanCardSelectionItemPropertyInfo(GameTexts.FindText("str_skills", null), textObject);
			yield break;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0006F988 File Offset: 0x0006DB88
		private void OnChangeLeaderOver(List<object> selectedItems, Action closePopup)
		{
			if (selectedItems.Count == 1)
			{
				Hero newLeader = selectedItems.FirstOrDefault<object>() as Hero;
				bool isDisband = newLeader == null;
				int partyGoldLowerThreshold = Campaign.Current.Models.ClanFinanceModel.PartyGoldLowerThreshold;
				ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
				PartyBase partyBase = ((currentSelectedParty != null) ? currentSelectedParty.Party : null);
				MobileParty mobileParty = ((partyBase != null) ? partyBase.MobileParty : null);
				DelayedTeleportationModel delayedTeleportationModel = Campaign.Current.Models.DelayedTeleportationModel;
				int num = ((!isDisband && mobileParty != null) ? ((int)Math.Ceiling((double)delayedTeleportationModel.GetTeleportationDelayAsHours(newLeader, mobileParty.Party).ResultNumber)) : 0);
				MBTextManager.SetTextVariable("TRAVEL_DURATION", CampaignUIHelper.GetHoursAndDaysTextFromHourValue(num).ToString(), false);
				Hero newLeader2 = newLeader;
				if (((newLeader2 != null) ? newLeader2.CharacterObject : null) != null)
				{
					StringHelpers.SetCharacterProperties("LEADER", newLeader.CharacterObject, null, false);
					MBTextManager.SetTextVariable("PARTY_COST", partyGoldLowerThreshold - newLeader.Gold);
					MBTextManager.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">", false);
					MBTextManager.SetTextVariable("DOES_LEADER_NEED_GOLD", (partyGoldLowerThreshold > newLeader.Gold) ? 1 : 0);
				}
				if (isDisband && partyBase != null && partyBase.Ships.Count > 0)
				{
					MBTextManager.SetTextVariable("DOES_DISBANDING_PARTY_HAVE_SHIP", true);
				}
				object obj = GameTexts.FindText(isDisband ? "str_disband_party" : "str_change_clan_party_leader", null);
				TextObject textObject = GameTexts.FindText(isDisband ? "str_disband_party_inquiry" : ((num == 0) ? "str_change_clan_party_leader_instantly_inquiry" : "str_change_clan_party_leader_inquiry"), null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate
				{
					Action closePopup3 = closePopup;
					if (closePopup3 != null)
					{
						closePopup3();
					}
					this.OnPartyLeaderChanged(newLeader);
					if (isDisband)
					{
						this.OnDisbandCurrentParty();
					}
					else if (newLeader.Gold < partyGoldLowerThreshold)
					{
						GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, newLeader, partyGoldLowerThreshold - newLeader.Gold, false);
					}
					Action onRefresh = this._onRefresh;
					if (onRefresh == null)
					{
						return;
					}
					onRefresh();
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			Action closePopup2 = closePopup;
			if (closePopup2 == null)
			{
				return;
			}
			closePopup2();
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x0006FBC0 File Offset: 0x0006DDC0
		private void OnPartyLeaderChanged(Hero newLeader)
		{
			ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
			bool flag;
			if (currentSelectedParty == null)
			{
				flag = null != null;
			}
			else
			{
				PartyBase party = currentSelectedParty.Party;
				flag = ((party != null) ? party.LeaderHero : null) != null;
			}
			if (flag)
			{
				if (newLeader == null)
				{
					Hero leaderHero = this.CurrentSelectedParty.Party.LeaderHero;
					this.CurrentSelectedParty.Party.MobileParty.RemovePartyLeader();
					MakeHeroFugitiveAction.Apply(leaderHero, false);
				}
				else
				{
					TeleportHeroAction.ApplyDelayedTeleportToParty(this.CurrentSelectedParty.Party.LeaderHero, MobileParty.MainParty);
				}
			}
			if (newLeader != null)
			{
				TeleportHeroAction.ApplyDelayedTeleportToPartyAsPartyLeader(newLeader, this.CurrentSelectedParty.Party.MobileParty);
			}
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0006FC50 File Offset: 0x0006DE50
		private void OnDisbandCurrentParty()
		{
			DisbandPartyAction.StartDisband(this.CurrentSelectedParty.Party.MobileParty);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0006FC68 File Offset: 0x0006DE68
		private bool GetCanDisbandParty(out TextObject cannotDisbandReason)
		{
			bool flag = false;
			cannotDisbandReason = TextObject.GetEmpty();
			ClanPartyItemVM currentSelectedParty = this.CurrentSelectedParty;
			MobileParty mobileParty;
			if (currentSelectedParty == null)
			{
				mobileParty = null;
			}
			else
			{
				PartyBase party = currentSelectedParty.Party;
				mobileParty = ((party != null) ? party.MobileParty : null);
			}
			MobileParty mobileParty2 = mobileParty;
			if (mobileParty2 != null)
			{
				TextObject textObject;
				if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
				{
					cannotDisbandReason = textObject;
				}
				else if (mobileParty2.IsMilitia)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_milita_party", null);
				}
				else if (mobileParty2.IsGarrison)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_garrison_party", null);
				}
				else if (mobileParty2.IsMainParty)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_main_party", null);
				}
				else if (this.CurrentSelectedParty.IsDisbanding)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_already_disbanding_party", null);
				}
				else if (mobileParty2.MapEvent != null || mobileParty2.SiegeEvent != null)
				{
					cannotDisbandReason = GameTexts.FindText("str_cannot_disband_during_battle", null);
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0006FD37 File Offset: 0x0006DF37
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x0006FD3F File Offset: 0x0006DF3F
		[DataSourceProperty]
		public HintViewModel CreateNewPartyActionHint
		{
			get
			{
				return this._createNewPartyActionHint;
			}
			set
			{
				if (value != this._createNewPartyActionHint)
				{
					this._createNewPartyActionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CreateNewPartyActionHint");
				}
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0006FD5D File Offset: 0x0006DF5D
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0006FD65 File Offset: 0x0006DF65
		[DataSourceProperty]
		public bool IsAnyValidPartySelected
		{
			get
			{
				return this._isAnyValidPartySelected;
			}
			set
			{
				if (value != this._isAnyValidPartySelected)
				{
					this._isAnyValidPartySelected = value;
					base.OnPropertyChangedWithValue(value, "IsAnyValidPartySelected");
				}
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x0006FD83 File Offset: 0x0006DF83
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0006FD8B File Offset: 0x0006DF8B
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0006FDAE File Offset: 0x0006DFAE
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x0006FDB6 File Offset: 0x0006DFB6
		[DataSourceProperty]
		public string CaravansText
		{
			get
			{
				return this._caravansText;
			}
			set
			{
				if (value != this._caravansText)
				{
					this._caravansText = value;
					base.OnPropertyChangedWithValue<string>(value, "CaravansText");
				}
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0006FDD9 File Offset: 0x0006DFD9
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0006FDE1 File Offset: 0x0006DFE1
		[DataSourceProperty]
		public string GarrisonsText
		{
			get
			{
				return this._garrisonsText;
			}
			set
			{
				if (value != this._garrisonsText)
				{
					this._garrisonsText = value;
					base.OnPropertyChangedWithValue<string>(value, "GarrisonsText");
				}
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0006FE04 File Offset: 0x0006E004
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0006FE0C File Offset: 0x0006E00C
		[DataSourceProperty]
		public string PartiesText
		{
			get
			{
				return this._partiesText;
			}
			set
			{
				if (value != this._partiesText)
				{
					this._partiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "PartiesText");
				}
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0006FE2F File Offset: 0x0006E02F
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x0006FE37 File Offset: 0x0006E037
		[DataSourceProperty]
		public string MoraleText
		{
			get
			{
				return this._moraleText;
			}
			set
			{
				if (value != this._moraleText)
				{
					this._moraleText = value;
					base.OnPropertyChangedWithValue<string>(value, "MoraleText");
				}
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x0006FE5A File Offset: 0x0006E05A
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x0006FE62 File Offset: 0x0006E062
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0006FE85 File Offset: 0x0006E085
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0006FE8D File Offset: 0x0006E08D
		[DataSourceProperty]
		public string CreateNewPartyText
		{
			get
			{
				return this._createNewPartyText;
			}
			set
			{
				if (value != this._createNewPartyText)
				{
					this._createNewPartyText = value;
					base.OnPropertyChangedWithValue<string>(value, "CreateNewPartyText");
				}
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x0006FEB0 File Offset: 0x0006E0B0
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x0006FEB8 File Offset: 0x0006E0B8
		[DataSourceProperty]
		public string SizeText
		{
			get
			{
				return this._sizeText;
			}
			set
			{
				if (value != this._sizeText)
				{
					this._sizeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SizeText");
				}
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0006FEDB File Offset: 0x0006E0DB
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x0006FEE3 File Offset: 0x0006E0E3
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0006FF01 File Offset: 0x0006E101
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x0006FF09 File Offset: 0x0006E109
		[DataSourceProperty]
		public bool CanCreateNewParty
		{
			get
			{
				return this._canCreateNewParty;
			}
			set
			{
				if (value != this._canCreateNewParty)
				{
					this._canCreateNewParty = value;
					base.OnPropertyChangedWithValue(value, "CanCreateNewParty");
				}
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x0006FF27 File Offset: 0x0006E127
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x0006FF2F File Offset: 0x0006E12F
		[DataSourceProperty]
		public MBBindingList<ClanPartyItemVM> Parties
		{
			get
			{
				return this._parties;
			}
			set
			{
				if (value != this._parties)
				{
					this._parties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyItemVM>>(value, "Parties");
				}
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x0006FF4D File Offset: 0x0006E14D
		// (set) Token: 0x06001E34 RID: 7732 RVA: 0x0006FF55 File Offset: 0x0006E155
		[DataSourceProperty]
		public MBBindingList<ClanPartyItemVM> Caravans
		{
			get
			{
				return this._caravans;
			}
			set
			{
				if (value != this._caravans)
				{
					this._caravans = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyItemVM>>(value, "Caravans");
				}
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0006FF73 File Offset: 0x0006E173
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x0006FF7B File Offset: 0x0006E17B
		[DataSourceProperty]
		public MBBindingList<ClanPartyItemVM> Garrisons
		{
			get
			{
				return this._garrisons;
			}
			set
			{
				if (value != this._garrisons)
				{
					this._garrisons = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanPartyItemVM>>(value, "Garrisons");
				}
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0006FF99 File Offset: 0x0006E199
		// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0006FFA1 File Offset: 0x0006E1A1
		[DataSourceProperty]
		public ClanPartyItemVM CurrentSelectedParty
		{
			get
			{
				return this._currentSelectedParty;
			}
			set
			{
				if (value != this._currentSelectedParty)
				{
					this._currentSelectedParty = value;
					base.OnPropertyChangedWithValue<ClanPartyItemVM>(value, "CurrentSelectedParty");
					this.IsAnyValidPartySelected = value != null;
				}
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0006FFC9 File Offset: 0x0006E1C9
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x0006FFD1 File Offset: 0x0006E1D1
		[DataSourceProperty]
		public ClanPartiesSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChangedWithValue<ClanPartiesSortControllerVM>(value, "SortController");
				}
			}
		}

		// Token: 0x04000E07 RID: 3591
		private Action _onExpenseChange;

		// Token: 0x04000E08 RID: 3592
		private Action<Hero> _openPartyAsManage;

		// Token: 0x04000E09 RID: 3593
		private Action<ClanCardSelectionInfo> _openCardSelectionPopup;

		// Token: 0x04000E0A RID: 3594
		private readonly IDisbandPartyCampaignBehavior _disbandBehavior;

		// Token: 0x04000E0B RID: 3595
		private readonly ITeleportationCampaignBehavior _teleportationBehavior;

		// Token: 0x04000E0C RID: 3596
		private readonly Action _onRefresh;

		// Token: 0x04000E0D RID: 3597
		private readonly Clan _faction;

		// Token: 0x04000E0E RID: 3598
		private readonly IEnumerable<SkillObject> _leaderAssignmentRelevantSkills = new List<SkillObject>
		{
			DefaultSkills.Engineering,
			DefaultSkills.Steward,
			DefaultSkills.Scouting,
			DefaultSkills.Medicine
		};

		// Token: 0x04000E0F RID: 3599
		private MBBindingList<ClanPartyItemVM> _parties;

		// Token: 0x04000E10 RID: 3600
		private MBBindingList<ClanPartyItemVM> _garrisons;

		// Token: 0x04000E11 RID: 3601
		private MBBindingList<ClanPartyItemVM> _caravans;

		// Token: 0x04000E12 RID: 3602
		private ClanPartyItemVM _currentSelectedParty;

		// Token: 0x04000E13 RID: 3603
		private HintViewModel _createNewPartyActionHint;

		// Token: 0x04000E14 RID: 3604
		private bool _canCreateNewParty;

		// Token: 0x04000E15 RID: 3605
		private bool _isSelected;

		// Token: 0x04000E16 RID: 3606
		private string _nameText;

		// Token: 0x04000E17 RID: 3607
		private string _moraleText;

		// Token: 0x04000E18 RID: 3608
		private string _locationText;

		// Token: 0x04000E19 RID: 3609
		private string _sizeText;

		// Token: 0x04000E1A RID: 3610
		private string _createNewPartyText;

		// Token: 0x04000E1B RID: 3611
		private string _partiesText;

		// Token: 0x04000E1C RID: 3612
		private string _caravansText;

		// Token: 0x04000E1D RID: 3613
		private string _garrisonsText;

		// Token: 0x04000E1E RID: 3614
		private bool _isAnyValidPartySelected;

		// Token: 0x04000E1F RID: 3615
		private ClanPartiesSortControllerVM _sortController;
	}
}
