using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200042C RID: 1068
	public class PeaceOfferCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x0014BD04 File Offset: 0x00149F04
		private static TextObject PeacePanelTitleText
		{
			get
			{
				return new TextObject("{=ho5EndaV}Decision", null);
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x0014BD11 File Offset: 0x00149F11
		private static TextObject PeacePanelOkText
		{
			get
			{
				return new TextObject("{=oHaWR73d}Ok", null);
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x0014BD1E File Offset: 0x00149F1E
		private static TextObject PeacePanelAffirmativeText
		{
			get
			{
				return new TextObject("{=Y94H6XnK}Accept", null);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x0014BD2B File Offset: 0x00149F2B
		private static TextObject PeacePanelNegativeText
		{
			get
			{
				return new TextObject("{=cOgmdp9e}Decline", null);
			}
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x0014BD38 File Offset: 0x00149F38
		public override void RegisterEvents()
		{
			CampaignEvents.OnPeaceOfferedToPlayerEvent.AddNonSerializedListener(this, new Action<IFaction, int, int>(this.OnPeaceOffered));
			CampaignEvents.OnPeaceOfferResolvedEvent.AddNonSerializedListener(this, new Action<IFaction>(this.OnPeaceOfferResolved));
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x0014BD68 File Offset: 0x00149F68
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<int>("_currentPeaceOfferTributeAmount", ref this._currentPeaceOfferTributeAmount);
			dataStore.SyncData<IFaction>("_opponentFaction", ref this._opponentFaction);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0014BD90 File Offset: 0x00149F90
		private void OnPeaceOffered(IFaction opponentFaction, int tributeAmount, int tributeDuration)
		{
			if (this._opponentFaction == null)
			{
				this._opponentFaction = opponentFaction;
				this._currentPeaceOfferTributeAmount = tributeAmount;
				this._currentPeaceOfferTributeDuration = tributeDuration;
				TextObject textObject = ((tributeAmount > 0) ? ((Hero.MainHero.MapFaction.Leader == Hero.MainHero) ? PeaceOfferCampaignBehavior.PeaceOfferTributePaidPanelDescriptionText : PeaceOfferCampaignBehavior.PeaceOfferTributePaidPanelPlayerIsVassalDescriptionText) : ((tributeAmount < 0) ? ((Hero.MainHero.MapFaction.Leader == Hero.MainHero) ? PeaceOfferCampaignBehavior.PeaceOfferTributeWantedPanelDescriptionText : PeaceOfferCampaignBehavior.PeaceOfferTributeWantedPanelPlayerIsVassalDescriptionText) : ((Hero.MainHero.MapFaction.Leader == Hero.MainHero) ? PeaceOfferCampaignBehavior.PeaceOfferDefaultPanelDescriptionText : PeaceOfferCampaignBehavior.PeaceOfferDefaultPanelPlayerIsVassalDescriptionText)));
				textObject.SetTextVariable("MAP_FACTION_NAME", opponentFaction.InformalName);
				textObject.SetTextVariable("GOLD_AMOUNT", MathF.Abs(this._currentPeaceOfferTributeAmount));
				textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
				TextObject peacePanelNegativeText = PeaceOfferCampaignBehavior.PeacePanelNegativeText;
				this._influenceCostOfDecline = 0;
				Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
				if (Hero.MainHero.MapFaction.Leader == Hero.MainHero)
				{
					InformationManager.ShowInquiry(new InquiryData(PeaceOfferCampaignBehavior.PeacePanelTitleText.ToString(), textObject.ToString(), true, (float)this._influenceCostOfDecline <= 0.1f || Hero.MainHero.Clan.Influence >= (float)this._influenceCostOfDecline, PeaceOfferCampaignBehavior.PeacePanelAffirmativeText.ToString(), peacePanelNegativeText.ToString(), new Action(this.AcceptPeaceOffer), new Action(this.DeclinePeaceOffer), "", 0f, null, null, null), true, false);
					return;
				}
				InformationManager.ShowInquiry(new InquiryData(PeaceOfferCampaignBehavior.PeacePanelTitleText.ToString(), textObject.ToString(), false, true, PeaceOfferCampaignBehavior.PeacePanelOkText.ToString(), PeaceOfferCampaignBehavior.PeacePanelOkText.ToString(), new Action(this.OkPeaceOffer), new Action(this.OkPeaceOffer), "", 0f, null, null, null), true, false);
			}
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x0014BF6E File Offset: 0x0014A16E
		private void OnPeaceOfferResolved(IFaction opponentFaction)
		{
			if (Hero.MainHero.MapFaction.Leader != Hero.MainHero && opponentFaction != null)
			{
				this._opponentFaction = opponentFaction;
				this.OkPeaceOffer();
			}
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x0014BF98 File Offset: 0x0014A198
		private void OkPeaceOffer()
		{
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				this.AcceptPeaceOffer();
				return;
			}
			Kingdom kingdom = Clan.PlayerClan.Kingdom;
			KingdomDecision kingdomDecision = kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
			{
				MakePeaceKingdomDecision makePeaceKingdomDecision2;
				return (makePeaceKingdomDecision2 = s as MakePeaceKingdomDecision) != null && makePeaceKingdomDecision2.ProposerClan.MapFaction == Hero.MainHero.MapFaction && makePeaceKingdomDecision2.FactionToMakePeaceWith == this._opponentFaction;
			});
			if (kingdomDecision != null)
			{
				kingdom.RemoveDecision(kingdomDecision);
			}
			MakePeaceKingdomDecision makePeaceKingdomDecision = new MakePeaceKingdomDecision(Hero.MainHero.MapFaction.Leader.Clan, this._opponentFaction, -this._currentPeaceOfferTributeAmount, this._currentPeaceOfferTributeDuration, true, true);
			((Kingdom)Hero.MainHero.MapFaction).AddDecision(makePeaceKingdomDecision, true);
			this._opponentFaction = null;
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x0014C031 File Offset: 0x0014A231
		private void AcceptPeaceOffer()
		{
			MakePeaceAction.ApplyByKingdomDecision(this._opponentFaction, Hero.MainHero.MapFaction, this._currentPeaceOfferTributeAmount, this._currentPeaceOfferTributeDuration);
			this._opponentFaction = null;
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x0014C05B File Offset: 0x0014A25B
		private void DeclinePeaceOffer()
		{
			this._opponentFaction = null;
			ChangeClanInfluenceAction.Apply(Clan.PlayerClan, (float)(-(float)this._influenceCostOfDecline));
		}

		// Token: 0x0400135D RID: 4957
		private static TextObject PeaceOfferDefaultPanelDescriptionText = new TextObject("{=IB1xsVEr}A courier has arrived from the {MAP_FACTION_NAME}. They offer you a white peace. Your vassals have left the decision with you.", null);

		// Token: 0x0400135E RID: 4958
		private static TextObject PeaceOfferTributePaidPanelDescriptionText = new TextObject("{=JJQ0Hp4m}A courier has arrived from the {MAP_FACTION_NAME}. The {MAP_FACTION_NAME} will pay {GOLD_AMOUNT} {GOLD_ICON} in tribute each day to end the war between your realms. Your vassals have left the decision with you.", null);

		// Token: 0x0400135F RID: 4959
		private static TextObject PeaceOfferTributeWantedPanelDescriptionText = new TextObject("{=Nd0Vhkxn}A courier has arrived from the {MAP_FACTION_NAME}. They offer you peace if you agree to pay a {GOLD_AMOUNT} {GOLD_ICON} daily tribute. Your vassals have left the decision with you.", null);

		// Token: 0x04001360 RID: 4960
		private static TextObject PeaceOfferDefaultPanelPlayerIsVassalDescriptionText = new TextObject("{=gNf0ALKw}A courier has arrived from the {MAP_FACTION_NAME}. They offer you a white peace. Your kingdom will vote whether to accept the offer.", null);

		// Token: 0x04001361 RID: 4961
		private static TextObject PeaceOfferTributePaidPanelPlayerIsVassalDescriptionText = new TextObject("{=SR9FC5jH}A courier has arrived from the {MAP_FACTION_NAME} bearing a peace offer. The {MAP_FACTION_NAME} will pay {GOLD_AMOUNT} {GOLD_ICON} in tribute each day to end the war between your realms. Your kingdom will vote whether to accept the offer.", null);

		// Token: 0x04001362 RID: 4962
		private static TextObject PeaceOfferTributeWantedPanelPlayerIsVassalDescriptionText = new TextObject("{=sbFboHmV}A courier has arrived from the {MAP_FACTION_NAME}. They offer you peace if you agree to pay a {GOLD_AMOUNT} {GOLD_ICON} daily tribute. Your kingdom will vote whether to accept the offer.", null);

		// Token: 0x04001363 RID: 4963
		private IFaction _opponentFaction;

		// Token: 0x04001364 RID: 4964
		private int _currentPeaceOfferTributeAmount;

		// Token: 0x04001365 RID: 4965
		private int _currentPeaceOfferTributeDuration;

		// Token: 0x04001366 RID: 4966
		private int _influenceCostOfDecline;
	}
}
