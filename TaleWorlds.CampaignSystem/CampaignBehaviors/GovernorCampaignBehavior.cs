using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003F4 RID: 1012
	public class GovernorCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003F95 RID: 16277 RVA: 0x0012055C File Offset: 0x0011E75C
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.DailyTickSettlement));
			CampaignEvents.OnHeroChangedClanEvent.AddNonSerializedListener(this, new Action<Hero, Clan>(this.OnHeroChangedClan));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x001205DC File Offset: 0x0011E7DC
		private void OnGameLoadFinished()
		{
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.0", 0))
			{
				foreach (Town town in Town.AllFiefs)
				{
					if (town.Governor != null && town != town.Governor.GovernorOf)
					{
						town.Governor = null;
					}
				}
			}
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x0012065C File Offset: 0x0011E85C
		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x00120668 File Offset: 0x0011E868
		private void DailyTickSettlement(Settlement settlement)
		{
			if ((settlement.IsTown || settlement.IsCastle) && settlement.Town.Governor != null)
			{
				Hero governor = settlement.Town.Governor;
				if (MBRandom.RandomFloat <= DefaultPerks.Charm.InBloom.SecondaryBonus && governor.GetPerkValue(DefaultPerks.Charm.InBloom))
				{
					Hero randomElementWithPredicate = settlement.Notables.GetRandomElementWithPredicate<Hero>((Hero x) => x.IsFemale != governor.IsFemale);
					if (randomElementWithPredicate != null)
					{
						ChangeRelationAction.ApplyRelationChangeBetweenHeroes(governor.Clan.Leader, randomElementWithPredicate, 1, true);
					}
				}
				if (MBRandom.RandomFloat <= DefaultPerks.Charm.YoungAndRespectful.SecondaryBonus && governor.GetPerkValue(DefaultPerks.Charm.YoungAndRespectful))
				{
					Hero randomElementWithPredicate2 = settlement.Notables.GetRandomElementWithPredicate<Hero>((Hero x) => x.IsFemale == governor.IsFemale);
					if (randomElementWithPredicate2 != null)
					{
						ChangeRelationAction.ApplyRelationChangeBetweenHeroes(governor.Clan.Leader, randomElementWithPredicate2, 1, true);
					}
				}
				if (MBRandom.RandomFloat <= DefaultPerks.Charm.MeaningfulFavors.SecondaryBonus && governor.GetPerkValue(DefaultPerks.Charm.MeaningfulFavors))
				{
					foreach (Hero hero in settlement.Notables)
					{
						if (hero.Power >= 200f)
						{
							ChangeRelationAction.ApplyRelationChangeBetweenHeroes(settlement.OwnerClan.Leader, hero, 1, true);
						}
					}
				}
				SkillLevelingManager.OnSettlementGoverned(governor, settlement);
			}
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x001207EC File Offset: 0x0011E9EC
		private void OnHeroChangedClan(Hero hero, Clan oldClan)
		{
			if (hero.GovernorOf != null && hero.GovernorOf.OwnerClan != hero.Clan)
			{
				ChangeGovernorAction.RemoveGovernorOf(hero);
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0012080F File Offset: 0x0011EA0F
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x00120814 File Offset: 0x0011EA14
		private void AddDialogs(CampaignGameStarter starter)
		{
			starter.AddPlayerLine("governor_talk_start", "hero_main_options", "governor_talk_start_reply", "{=zBo78JQb}How are things doing here in {GOVERNOR_SETTLEMENT}?", new ConversationSentence.OnConditionDelegate(this.governor_talk_start_on_condition), null, 100, null, null);
			starter.AddDialogLine("governor_talk_start_reply", "governor_talk_start_reply", "lord_pretalk", "{=!}{SETTLEMENT_DESCRIPTION}", new ConversationSentence.OnConditionDelegate(this.governor_talk_start_reply_on_condition), null, 200, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_start", "hero_main_options", "governor_kingdom_creation_reply", "{=EKuB6Ohf}It is time to take a momentous step... It is time to proclaim a new kingdom.", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_start_on_condition), new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_start_on_consequence), 200, new ConversationSentence.OnClickableConditionDelegate(this.governor_talk_kingdom_creation_start_clickable_condition), null);
			starter.AddDialogLine("governor_talk_kingdom_creation_reply", "governor_kingdom_creation_reply", "governor_kingdom_creation_culture_selection", "{=ZyNjXUHc}I am at your command.", null, null, 100, null);
			starter.AddDialogLine("governor_talk_kingdom_creation_culture_selection", "governor_kingdom_creation_culture_selection", "governor_kingdom_creation_culture_selection_options", "{=jxEVSu98}The language of our documents, and our customary laws... Whose should we use?", null, null, 100, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selection_option", "governor_kingdom_creation_culture_selection_options", "governor_kingdom_creation_culture_selected", "{CULTURE_OPTION_0}", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_culture_option_0_on_condition), new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_culture_option_0_on_consequence), 100, null, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selection_option_2", "governor_kingdom_creation_culture_selection_options", "governor_kingdom_creation_culture_selected", "{CULTURE_OPTION_1}", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_culture_option_1_on_condition), new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_culture_option_1_on_consequence), 100, null, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selection_option_3", "governor_kingdom_creation_culture_selection_options", "governor_kingdom_creation_culture_selected", "{CULTURE_OPTION_2}", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_culture_option_2_on_condition), new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_culture_option_2_on_consequence), 100, null, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selection_other", "governor_kingdom_creation_culture_selection_options", "governor_kingdom_creation_culture_selection", "{=kcuNzSvf}I have another people in mind.", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_culture_other_on_condition), new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_culture_other_on_consequence), 100, null, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selection_cancel", "governor_kingdom_creation_culture_selection_options", "governor_kingdom_creation_exit", "{=hbzs5tLd}On second thought, perhaps now is not the right time.", null, null, 100, null, null);
			starter.AddDialogLine("governor_talk_kingdom_creation_exit_reply", "governor_kingdom_creation_exit", "close_window", "{=ppi6eVos}As you wish.", null, null, 100, null);
			starter.AddDialogLine("governor_talk_kingdom_creation_culture_selected", "governor_kingdom_creation_culture_selected", "governor_kingdom_creation_culture_selected_confirmation", "{=VOtKthQU}Yes. A kingdom using {CULTURE_ADJECTIVE} law would institute the following: {INITIAL_POLICY_NAMES}.", new ConversationSentence.OnConditionDelegate(this.governor_kingdom_creation_culture_selected_on_condition), null, 100, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selected_player_reply", "governor_kingdom_creation_culture_selected_confirmation", "governor_kingdom_creation_name_selection", "{=dzXaXKaC}Very well.", null, null, 100, null, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_culture_selected_player_reply_2", "governor_kingdom_creation_culture_selected_confirmation", "governor_kingdom_creation_culture_selection", "{=kTjsx8gN}Perhaps we should choose another set of laws and customs.", null, null, 100, null, null);
			starter.AddDialogLine("governor_talk_kingdom_creation_name_selection", "governor_kingdom_creation_name_selection", "governor_kingdom_creation_name_selection_response", "{=wT1ducZX}Now. What will the kingdom be called?", null, null, 100, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_name_selection_player", "governor_kingdom_creation_name_selection_response", "governor_kingdom_creation_name_selection_prompted", "{=XRoG766S}I'll name it...", null, new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_name_selection_on_consequence), 100, null, null);
			starter.AddDialogLine("governor_talk_kingdom_creation_name_selection_response", "governor_kingdom_creation_name_selection_prompted", "governor_kingdom_creation_name_selected", "{=shf5aY3l}I'm listening...", null, null, 100, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_name_selection_cancel", "governor_kingdom_creation_name_selection_response", "governor_kingdom_creation_exit", "{=7HpfrmIU}On a second thought... Now is not the right time to do this.", null, null, 100, null, null);
			starter.AddDialogLine("governor_talk_kingdom_creation_name_selection_final_response", "governor_kingdom_creation_name_selected", "governor_kingdom_creation_finalization", "{=CzJZ5zhT}So it shall be proclaimed throughout your domain. May {KINGDOM_NAME} forever be victorious!", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_finalization_on_condition), null, 100, null);
			starter.AddPlayerLine("governor_talk_kingdom_creation_finalization", "governor_kingdom_creation_finalization", "close_window", "{=VRbbIWNf}So it shall be.", new ConversationSentence.OnConditionDelegate(this.governor_talk_kingdom_creation_finalization_on_condition), new ConversationSentence.OnConsequenceDelegate(this.governor_talk_kingdom_creation_finalization_on_consequence), 100, null, null);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x00120B79 File Offset: 0x0011ED79
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (victim.GovernorOf != null)
			{
				ChangeGovernorAction.RemoveGovernorOf(victim);
			}
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x00120B8C File Offset: 0x0011ED8C
		private bool governor_talk_start_on_condition()
		{
			if (Hero.OneToOneConversationHero != null && Hero.OneToOneConversationHero.GovernorOf != null && Hero.OneToOneConversationHero.CurrentSettlement != null && Hero.OneToOneConversationHero.CurrentSettlement.IsTown && Hero.OneToOneConversationHero.CurrentSettlement.Town == Hero.OneToOneConversationHero.GovernorOf && Hero.OneToOneConversationHero.GovernorOf.Owner.Owner == Hero.MainHero)
			{
				MBTextManager.SetTextVariable("GOVERNOR_SETTLEMENT", Hero.OneToOneConversationHero.CurrentSettlement.Name, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x00120C1C File Offset: 0x0011EE1C
		private bool governor_talk_start_reply_on_condition()
		{
			Settlement currentSettlement = Hero.OneToOneConversationHero.CurrentSettlement;
			TextObject textObject = TextObject.GetEmpty();
			switch (currentSettlement.Town.GetProsperityLevel())
			{
			case SettlementComponent.ProsperityLevel.Low:
				textObject = new TextObject("{=rbJEuVKg}Things could certainly be better, my {?HERO.GENDER}lady{?}lord{\\?}. The merchants say business is slow, and the people complain that goods are expensive and in short supply.", null);
				break;
			case SettlementComponent.ProsperityLevel.Mid:
				textObject = new TextObject("{=HgdbSrq9}Things are all right, my {?HERO.GENDER}lady{?}lord{\\?}. The merchants say that they are breaking even, for the most part. Some prices are high, but most of what the people need is available.", null);
				break;
			case SettlementComponent.ProsperityLevel.High:
				textObject = new TextObject("{=8G94SlPD}We are doing well, my {?HERO.GENDER}lady{?}lord{\\?}. The merchants say business is brisk, and everything the people need appears to be in good supply.", null);
				break;
			}
			StringHelpers.SetCharacterProperties("HERO", CharacterObject.PlayerCharacter, textObject, false);
			MBTextManager.SetTextVariable("SETTLEMENT_DESCRIPTION", textObject.ToString(), false);
			return true;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x00120CA4 File Offset: 0x0011EEA4
		private bool governor_talk_kingdom_creation_start_on_condition()
		{
			return Clan.PlayerClan.Kingdom == null && Hero.OneToOneConversationHero != null && Hero.OneToOneConversationHero.GovernorOf != null && Hero.OneToOneConversationHero.GovernorOf.Settlement.MapFaction == Hero.MainHero.MapFaction;
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x00120CF2 File Offset: 0x0011EEF2
		private void governor_talk_kingdom_creation_start_on_consequence()
		{
			this._availablePlayerKingdomCultures.Clear();
			this._availablePlayerKingdomCultures = Campaign.Current.Models.KingdomCreationModel.GetAvailablePlayerKingdomCultures().ToList<CultureObject>();
			this._kingdomCreationCurrentCulturePageIndex = 0;
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x00120D28 File Offset: 0x0011EF28
		private bool governor_talk_kingdom_creation_start_clickable_condition(out TextObject explanation)
		{
			List<TextObject> list;
			bool flag = Campaign.Current.Models.KingdomCreationModel.IsPlayerKingdomCreationPossible(out list);
			string text = "";
			foreach (TextObject textObject in list)
			{
				text += textObject;
				if (textObject != list[list.Count - 1])
				{
					text += "\n";
				}
			}
			explanation = new TextObject(text, null);
			return flag;
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00120DC4 File Offset: 0x0011EFC4
		private bool governor_talk_kingdom_creation_culture_option_0_on_condition()
		{
			return this.HandleAvailableCultureConditionAndText(0);
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00120DCD File Offset: 0x0011EFCD
		private bool governor_talk_kingdom_creation_culture_option_1_on_condition()
		{
			return this.HandleAvailableCultureConditionAndText(1);
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x00120DD6 File Offset: 0x0011EFD6
		private bool governor_talk_kingdom_creation_culture_option_2_on_condition()
		{
			return this.HandleAvailableCultureConditionAndText(2);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x00120DE0 File Offset: 0x0011EFE0
		private bool HandleAvailableCultureConditionAndText(int index)
		{
			int cultureIndex = this.GetCultureIndex(index);
			if (this._availablePlayerKingdomCultures.Count > cultureIndex)
			{
				TextObject textObject = new TextObject("{=mY6DbVfc}The language and laws of {CULTURE_NAME}.", null);
				textObject.SetTextVariable("CULTURE_NAME", FactionHelper.GetInformalNameForFactionCulture(this._availablePlayerKingdomCultures[cultureIndex]));
				MBTextManager.SetTextVariable("CULTURE_OPTION_" + index.ToString(), textObject, false);
				return true;
			}
			return false;
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x00120E47 File Offset: 0x0011F047
		private int GetCultureIndex(int optionIndex)
		{
			return this._kingdomCreationCurrentCulturePageIndex * 3 + optionIndex;
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x00120E53 File Offset: 0x0011F053
		private void governor_talk_kingdom_creation_culture_option_0_on_consequence()
		{
			this._kingdomCreationChosenCulture = this._availablePlayerKingdomCultures[this.GetCultureIndex(0)];
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x00120E6D File Offset: 0x0011F06D
		private void governor_talk_kingdom_creation_culture_option_1_on_consequence()
		{
			this._kingdomCreationChosenCulture = this._availablePlayerKingdomCultures[this.GetCultureIndex(1)];
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x00120E87 File Offset: 0x0011F087
		private void governor_talk_kingdom_creation_culture_option_2_on_consequence()
		{
			this._kingdomCreationChosenCulture = this._availablePlayerKingdomCultures[this.GetCultureIndex(2)];
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00120EA1 File Offset: 0x0011F0A1
		private bool governor_talk_kingdom_creation_culture_other_on_condition()
		{
			return this._availablePlayerKingdomCultures.Count > 3;
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00120EB1 File Offset: 0x0011F0B1
		private void governor_talk_kingdom_creation_culture_other_on_consequence()
		{
			this._kingdomCreationCurrentCulturePageIndex++;
			if (this._kingdomCreationCurrentCulturePageIndex > MathF.Ceiling((float)this._availablePlayerKingdomCultures.Count / 3f) - 1)
			{
				this._kingdomCreationCurrentCulturePageIndex = 0;
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00120EEC File Offset: 0x0011F0EC
		private bool governor_kingdom_creation_culture_selected_on_condition()
		{
			TextObject textObject = GameTexts.GameTextHelper.MergeTextObjectsWithComma(this._kingdomCreationChosenCulture.DefaultPolicyList.Select<PolicyObject, TextObject>((PolicyObject t) => t.Name).ToList<TextObject>(), true);
			MBTextManager.SetTextVariable("INITIAL_POLICY_NAMES", textObject, false);
			MBTextManager.SetTextVariable("CULTURE_ADJECTIVE", FactionHelper.GetAdjectiveForFactionCulture(this._kingdomCreationChosenCulture), false);
			return true;
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00120F58 File Offset: 0x0011F158
		private void governor_talk_kingdom_creation_name_selection_on_consequence()
		{
			this._kingdomCreationChosenName = TextObject.GetEmpty();
			InformationManager.ShowTextInquiry(new TextInquiryData(new TextObject("{=RuaA8t97}Kingdom Name", null).ToString(), string.Empty, true, true, GameTexts.FindText("str_done", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action<string>(this.OnKingdomNameSelectionDone), new Action(this.OnKingdomNameSelectionCancel), false, new Func<string, Tuple<bool, string>>(FactionHelper.IsKingdomNameApplicable), "", ""), false, false);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00120FE2 File Offset: 0x0011F1E2
		private void OnKingdomNameSelectionDone(string chosenName)
		{
			this._kingdomCreationChosenName = new TextObject(chosenName, null);
			Campaign.Current.ConversationManager.ContinueConversation();
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00121000 File Offset: 0x0011F200
		private void OnKingdomNameSelectionCancel()
		{
			Campaign.Current.ConversationManager.EndConversation();
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x00121011 File Offset: 0x0011F211
		private bool governor_talk_kingdom_creation_finalization_on_condition()
		{
			MBTextManager.SetTextVariable("KINGDOM_NAME", this._kingdomCreationChosenName, false);
			return true;
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x00121028 File Offset: 0x0011F228
		private void governor_talk_kingdom_creation_finalization_on_consequence()
		{
			Campaign.Current.KingdomManager.CreateKingdom(this._kingdomCreationChosenName, this._kingdomCreationChosenName, this._kingdomCreationChosenCulture, Clan.PlayerClan, this._kingdomCreationChosenCulture.DefaultPolicyList, null, null, null);
		}

		// Token: 0x040012F7 RID: 4855
		private const int CultureDialogueOptionCount = 3;

		// Token: 0x040012F8 RID: 4856
		private List<CultureObject> _availablePlayerKingdomCultures = new List<CultureObject>();

		// Token: 0x040012F9 RID: 4857
		private int _kingdomCreationCurrentCulturePageIndex;

		// Token: 0x040012FA RID: 4858
		private CultureObject _kingdomCreationChosenCulture;

		// Token: 0x040012FB RID: 4859
		private TextObject _kingdomCreationChosenName;
	}
}
