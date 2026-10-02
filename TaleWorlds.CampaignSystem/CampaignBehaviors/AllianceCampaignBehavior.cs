using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003CF RID: 975
	public class AllianceCampaignBehavior : CampaignBehaviorBase, IAllianceCampaignBehavior
	{
		// Token: 0x06003A4E RID: 14926 RVA: 0x000EF580 File Offset: 0x000ED780
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickClanEvent.AddNonSerializedListener(this, new Action<Clan>(this.DailyTickClan));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnMakePeace));
			CampaignEvents.KingdomDestroyedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomDestroyed));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000EF62E File Offset: 0x000ED82E
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<AllianceCampaignBehavior.Alliance>>("_alliances", ref this._alliances);
			dataStore.SyncData<List<AllianceCampaignBehavior.CallToWarAgreement>>("_callToWarAgreements", ref this._callToWarAgreements);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000EF654 File Offset: 0x000ED854
		public void OnAllianceOfferedToPlayer(Kingdom offeringKingdom)
		{
			if (Clan.PlayerClan.Kingdom.Clans.Count == 1)
			{
				object obj = new TextObject("{=ho5EndaV}Decision", null);
				TextObject textObject = new TextObject("{=eAhgrwkZ}As {RULER_NAME_AND_TITLE}, you must decide if an alliance will be formed with the {KINGDOM_NAME}.", null);
				TextObject textObject2 = GameTexts.FindText("str_faction_ruler_name_with_title", Hero.MainHero.MapFaction.Culture.StringId);
				textObject2.SetCharacterProperties("RULER", Hero.MainHero.CharacterObject, false);
				textObject.SetTextVariable("RULER_NAME_AND_TITLE", textObject2);
				textObject.SetTextVariable("KINGDOM_NAME", offeringKingdom.Name);
				TextObject textObject3 = new TextObject("{=Y94H6XnK}Accept", null);
				TextObject textObject4 = new TextObject("{=cOgmdp9e}Decline", null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, textObject3.ToString(), textObject4.ToString(), delegate
				{
					this.AcceptStartingAlliance(offeringKingdom);
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			object obj2 = new TextObject("{=ho5EndaV}Decision", null);
			TextObject textObject5 = new TextObject("{=eTylgLCc}A courier has arrived from the {KINGDOM_NAME}. They offer you an alliance. Your kingdom will vote whether to accept the offer.", null);
			textObject5.SetTextVariable("KINGDOM_NAME", offeringKingdom.Name);
			TextObject textObject6 = new TextObject("{=oHaWR73d}Ok", null);
			InformationManager.ShowInquiry(new InquiryData(obj2.ToString(), textObject5.ToString(), true, false, textObject6.ToString(), textObject6.ToString(), delegate
			{
				this.ConfirmAllianceOffer(offeringKingdom);
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000EF7D8 File Offset: 0x000ED9D8
		public void OnAllianceOfferedToPlayerKingdom(Kingdom offeringKingdom)
		{
			if (Clan.PlayerClan.Kingdom.Clans.Count == 1)
			{
				TextObject textObject = new TextObject("{=1V8f9vRM}A courier bearing an alliance offer from the {PROPOSER_KINGDOM} has arrived at the court of your realm.", null);
				textObject.SetTextVariable("PROPOSER_KINGDOM", offeringKingdom.InformalName);
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new AllianceOfferMapNotification(offeringKingdom, textObject));
				return;
			}
			this.AddAllianceDecision(Clan.PlayerClan.Kingdom, offeringKingdom);
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000EF844 File Offset: 0x000EDA44
		public void OnCallToWarAgreementProposedToPlayer(Kingdom proposerKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			if (Clan.PlayerClan.Kingdom.Clans.Count == 1)
			{
				int callToWarCost = Campaign.Current.Models.AllianceModel.GetCallToWarCost(proposerKingdom, Clan.PlayerClan.Kingdom, kingdomToCallToWarAgainst);
				object obj = new TextObject("{=ho5EndaV}Decision", null);
				TextObject textObject = new TextObject("{=L81DPSom}As {RULER_NAME_AND_TITLE}, you must decide if your realm will answer the call of the {CALLING_KINGDOM} and declare war on the {KINGDOM_TO_CALL_TO_WAR_AGAINST} for {CALL_TO_WAR_COST}{GOLD_ICON}.", null);
				TextObject textObject2 = GameTexts.FindText("str_faction_ruler_name_with_title", Hero.MainHero.MapFaction.Culture.StringId);
				textObject2.SetCharacterProperties("RULER", Hero.MainHero.CharacterObject, false);
				textObject.SetTextVariable("RULER_NAME_AND_TITLE", textObject2);
				textObject.SetTextVariable("CALLING_KINGDOM", proposerKingdom.Name);
				textObject.SetTextVariable("KINGDOM_TO_CALL_TO_WAR_AGAINST", kingdomToCallToWarAgainst.Name);
				textObject.SetTextVariable("CALL_TO_WAR_COST", callToWarCost);
				textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
				TextObject textObject3 = new TextObject("{=Y94H6XnK}Accept", null);
				TextObject textObject4 = new TextObject("{=cOgmdp9e}Decline", null);
				InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, textObject3.ToString(), textObject4.ToString(), delegate
				{
					this.StartCallToWarAgreement(proposerKingdom, Clan.PlayerClan.Kingdom, kingdomToCallToWarAgainst, callToWarCost, false);
				}, delegate
				{
					this.DenyCallToWarAgreement(proposerKingdom, Clan.PlayerClan.Kingdom);
				}, "", 0f, null, null, null), false, false);
				return;
			}
			object obj2 = new TextObject("{=ho5EndaV}Decision", null);
			TextObject textObject5 = new TextObject("{=FuNbouTu}A courier has arrived from the {KINGDOM_NAME}. They call your kingdom to war against {KINGDOM_TO_CALL_TO_WAR_AGAINST}. Your kingdom will vote whether to accept the offer.", null);
			textObject5.SetTextVariable("KINGDOM_NAME", proposerKingdom.Name);
			textObject5.SetTextVariable("KINGDOM_TO_CALL_TO_WAR_AGAINST", kingdomToCallToWarAgainst.Name);
			TextObject textObject6 = new TextObject("{=oHaWR73d}Ok", null);
			InformationManager.ShowInquiry(new InquiryData(obj2.ToString(), textObject5.ToString(), true, false, textObject6.ToString(), textObject6.ToString(), delegate
			{
				this.ConfirmCallToWarAgreementOffer(proposerKingdom, kingdomToCallToWarAgainst);
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000EFA84 File Offset: 0x000EDC84
		public void OnCallToWarAgreementProposedToPlayerKingdom(Kingdom proposerKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			if (Clan.PlayerClan.Kingdom.Clans.Count == 1)
			{
				TextObject textObject = new TextObject("{=PneX4Ayw}A courier bearing a call to war offer from the {KINGDOM_NAME} against {KINGDOM_TO_CALL_TO_WAR_AGAINST} has arrived at the court of your realm.", null);
				textObject.SetTextVariable("KINGDOM_NAME", proposerKingdom.Name);
				textObject.SetTextVariable("KINGDOM_TO_CALL_TO_WAR_AGAINST", kingdomToCallToWarAgainst.Name);
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new AcceptCallToWarOfferMapNotification(proposerKingdom, kingdomToCallToWarAgainst, textObject));
				return;
			}
			KingdomDecision kingdomDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
			{
				AcceptCallToWarAgreementDecision acceptCallToWarAgreementDecision2;
				return (acceptCallToWarAgreementDecision2 = s as AcceptCallToWarAgreementDecision) != null && acceptCallToWarAgreementDecision2.CallingKingdom == proposerKingdom && acceptCallToWarAgreementDecision2.KingdomToCallToWarAgainst == kingdomToCallToWarAgainst;
			});
			if (kingdomDecision != null)
			{
				Clan.PlayerClan.Kingdom.RemoveDecision(kingdomDecision);
			}
			AcceptCallToWarAgreementDecision acceptCallToWarAgreementDecision = new AcceptCallToWarAgreementDecision(Clan.PlayerClan, proposerKingdom, kingdomToCallToWarAgainst);
			Clan.PlayerClan.Kingdom.AddDecision(acceptCallToWarAgreementDecision, true);
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000EFB74 File Offset: 0x000EDD74
		public void OnCallToWarAgreementProposedByPlayer(Kingdom proposedKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			if (Clan.PlayerClan.Kingdom.Clans.Count == 1)
			{
				int callToWarCost = Campaign.Current.Models.AllianceModel.GetCallToWarCost(Clan.PlayerClan.Kingdom, proposedKingdom, kingdomToCallToWarAgainst);
				if (callToWarCost <= Clan.PlayerClan.Gold)
				{
					object obj = new TextObject("{=ho5EndaV}Decision", null);
					TextObject textObject = new TextObject("{=AwCnrOan}As {RULER_NAME_AND_TITLE}, you must decide if the {CALLED_KINGDOM} will be called to war against the {KINGDOM_TO_CALL_TO_WAR_AGAINST} for {CALL_TO_WAR_COST}{GOLD_ICON}.", null);
					TextObject textObject2 = GameTexts.FindText("str_faction_ruler_name_with_title", Hero.MainHero.MapFaction.Culture.StringId);
					textObject2.SetCharacterProperties("RULER", Hero.MainHero.CharacterObject, false);
					textObject.SetTextVariable("RULER_NAME_AND_TITLE", textObject2);
					textObject.SetTextVariable("CALLED_KINGDOM", proposedKingdom.Name);
					textObject.SetTextVariable("KINGDOM_TO_CALL_TO_WAR_AGAINST", kingdomToCallToWarAgainst.Name);
					textObject.SetTextVariable("CALL_TO_WAR_COST", callToWarCost);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					TextObject textObject3 = new TextObject("{=Y94H6XnK}Accept", null);
					TextObject textObject4 = new TextObject("{=cOgmdp9e}Decline", null);
					InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, true, textObject3.ToString(), textObject4.ToString(), delegate
					{
						this.StartCallToWarAgreement(Clan.PlayerClan.Kingdom, proposedKingdom, kingdomToCallToWarAgainst, callToWarCost, false);
					}, null, "", 0f, null, null, null), false, false);
					return;
				}
			}
			else
			{
				object obj2 = new TextObject("{=ho5EndaV}Decision", null);
				TextObject textObject5 = new TextObject("{=qgN9o2ip}It is time to call our ally the {KINGDOM_NAME} to war againts {KINGDOM_TO_CALL_TO_WAR_AGAINST}!. Your kingdom will vote whether to propose a call to war agreement to them.", null);
				textObject5.SetTextVariable("KINGDOM_NAME", proposedKingdom.Name);
				textObject5.SetTextVariable("KINGDOM_TO_CALL_TO_WAR_AGAINST", kingdomToCallToWarAgainst.Name);
				TextObject textObject6 = new TextObject("{=oHaWR73d}Ok", null);
				InformationManager.ShowInquiry(new InquiryData(obj2.ToString(), textObject5.ToString(), true, false, textObject6.ToString(), textObject6.ToString(), delegate
				{
					this.ConfirmCallToWarAgreementProposalOffer(proposedKingdom, kingdomToCallToWarAgainst);
				}, null, "", 0f, null, null, null), false, false);
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000EFDBC File Offset: 0x000EDFBC
		public CampaignTime GetAllianceEndDate(Kingdom kingdom1, Kingdom kingdom2)
		{
			AllianceCampaignBehavior.Alliance alliance;
			if (!this.TryGetAlliance(kingdom1, kingdom2, out alliance))
			{
				Debug.FailedAssert("Cant find alliance", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\AllianceCampaignBehavior.cs", "GetAllianceEndDate", 277);
				return CampaignTime.Zero;
			}
			return alliance.EndTime;
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000EFDFC File Offset: 0x000EDFFC
		public void OnCallToWarAgreementProposedByPlayerKingdom(Kingdom proposedKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			if (Clan.PlayerClan.Kingdom.Clans.Count == 1)
			{
				TextObject textObject = new TextObject("{=dDsJyerw}Call the {CALLED_KINGDOM} to War Against the {KINGDOM_TO_CALL_TO_WAR_AGAINST}.", null);
				textObject.SetTextVariable("CALLED_KINGDOM", proposedKingdom.Name);
				textObject.SetTextVariable("KINGDOM_TO_CALL_TO_WAR_AGAINST", kingdomToCallToWarAgainst.Name);
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new ProposeCallToWarOfferMapNotification(proposedKingdom, kingdomToCallToWarAgainst, textObject));
				return;
			}
			KingdomDecision kingdomDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
			{
				ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision2;
				return (proposeCallToWarAgreementDecision2 = s as ProposeCallToWarAgreementDecision) != null && proposeCallToWarAgreementDecision2.CalledKingdom == proposedKingdom && proposeCallToWarAgreementDecision2.KingdomToCallToWarAgainst == kingdomToCallToWarAgainst;
			});
			if (kingdomDecision != null)
			{
				Clan.PlayerClan.Kingdom.RemoveDecision(kingdomDecision);
			}
			ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision = new ProposeCallToWarAgreementDecision(Clan.PlayerClan, proposedKingdom, kingdomToCallToWarAgainst);
			Clan.PlayerClan.Kingdom.AddDecision(proposeCallToWarAgreementDecision, true);
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000EFEEC File Offset: 0x000EE0EC
		public bool IsAllyWithKingdom(Kingdom kingdom1, Kingdom kingdom2)
		{
			AllianceCampaignBehavior.Alliance alliance;
			return kingdom1 != null && kingdom2 != null && kingdom1 != kingdom2 && !kingdom1.IsEliminated && !kingdom2.IsEliminated && this.TryGetAlliance(kingdom1, kingdom2, out alliance);
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000EFF20 File Offset: 0x000EE120
		public void StartAlliance(Kingdom proposerKingdom, Kingdom receiverKingdom)
		{
			if (!this.IsAllyWithKingdom(proposerKingdom, receiverKingdom))
			{
				StanceLink stanceWith = proposerKingdom.GetStanceWith(receiverKingdom);
				if (stanceWith.GetDailyTributeToPay(proposerKingdom) != 0)
				{
					stanceWith.SetDailyTributePaid(proposerKingdom, 0, 0);
				}
				if (stanceWith.GetDailyTributeToPay(proposerKingdom) != 0)
				{
					stanceWith.SetDailyTributePaid(proposerKingdom, 0, 0);
				}
				this.AddAlliance(proposerKingdom, receiverKingdom);
				CampaignEventDispatcher.Instance.OnAllianceStarted(proposerKingdom, receiverKingdom);
				foreach (IFaction faction in proposerKingdom.FactionsAtWarWith.WhereQ<IFaction>((IFaction f) => f.IsKingdomFaction && !f.IsAtWarWith(receiverKingdom)).ToList<IFaction>())
				{
					if (proposerKingdom == Clan.PlayerClan.Kingdom && !Hero.MainHero.Clan.IsUnderMercenaryService)
					{
						this.OnCallToWarAgreementProposedByPlayerKingdom(receiverKingdom, (Kingdom)faction);
					}
					else
					{
						ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision = new ProposeCallToWarAgreementDecision(proposerKingdom.RulingClan, receiverKingdom, (Kingdom)faction);
						proposerKingdom.AddDecision(proposeCallToWarAgreementDecision, true);
					}
				}
				foreach (IFaction faction2 in receiverKingdom.FactionsAtWarWith.WhereQ<IFaction>((IFaction f) => f.IsKingdomFaction && !f.IsAtWarWith(proposerKingdom)).ToList<IFaction>())
				{
					if (receiverKingdom == Clan.PlayerClan.Kingdom && !Hero.MainHero.Clan.IsUnderMercenaryService)
					{
						this.OnCallToWarAgreementProposedByPlayerKingdom(proposerKingdom, (Kingdom)faction2);
					}
					else
					{
						ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision2 = new ProposeCallToWarAgreementDecision(receiverKingdom.RulingClan, proposerKingdom, (Kingdom)faction2);
						receiverKingdom.AddDecision(proposeCallToWarAgreementDecision2, true);
					}
				}
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000F0144 File Offset: 0x000EE344
		public void EndAlliance(Kingdom kingdom1, Kingdom kingdom2)
		{
			foreach (AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement in this.GetCallToWarAgreements(kingdom1, kingdom2))
			{
				this.EndCallToWarAgreement(callToWarAgreement.CallingKingdom, callToWarAgreement.CalledKingdom, callToWarAgreement.KingdomToCallToWarAgainst);
			}
			this.RemoveAlliance(kingdom1, kingdom2);
			CampaignEventDispatcher.Instance.OnAllianceEnded(kingdom1, kingdom2);
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000F01C0 File Offset: 0x000EE3C0
		public bool HasCalledToWar(Kingdom callingKingdom, Kingdom calledKingdom)
		{
			return callingKingdom != null && calledKingdom != null && callingKingdom != calledKingdom && !callingKingdom.IsEliminated && !calledKingdom.IsEliminated && calledKingdom.IsAllyWith(callingKingdom) && this._callToWarAgreements.AnyQ<AllianceCampaignBehavior.CallToWarAgreement>((AllianceCampaignBehavior.CallToWarAgreement c) => c.CallingKingdom == callingKingdom && c.CalledKingdom == calledKingdom);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F0248 File Offset: 0x000EE448
		public bool IsAtWarByCallToWarAgreement(Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst, out Kingdom callingKingdom)
		{
			callingKingdom = null;
			if (kingdomToCallToWarAgainst == null || calledKingdom == null || kingdomToCallToWarAgainst == calledKingdom || kingdomToCallToWarAgainst.IsEliminated || calledKingdom.IsEliminated)
			{
				return false;
			}
			for (int i = 0; i < this._callToWarAgreements.Count; i++)
			{
				AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement = this._callToWarAgreements[i];
				if (callToWarAgreement.CalledKingdom == calledKingdom && callToWarAgreement.KingdomToCallToWarAgainst == kingdomToCallToWarAgainst)
				{
					callingKingdom = callToWarAgreement.CallingKingdom;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000F02B4 File Offset: 0x000EE4B4
		public void StartCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst, int callToWarCost, bool isPlayerPaying = false)
		{
			if (this.IsAllyWithKingdom(callingKingdom, calledKingdom) && !calledKingdom.IsAtWarWith(kingdomToCallToWarAgainst))
			{
				AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement = this.AddCallToWarAgreement(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
				this.UpdateAllianceEndTime(callingKingdom, calledKingdom, callToWarAgreement.EndTime);
				if (isPlayerPaying)
				{
					Hero.MainHero.ChangeHeroGold(-callToWarCost);
					calledKingdom.CallToWarWallet += callToWarCost;
				}
				else
				{
					callingKingdom.CallToWarWallet -= callToWarCost;
					calledKingdom.CallToWarWallet += callToWarCost;
				}
				CampaignEventDispatcher.Instance.OnCallToWarAgreementStarted(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
				this.ApplyAcceptingCallToWarOfferBonus(callingKingdom, calledKingdom);
				DeclareWarAction.ApplyByCallToWarAgreement(calledKingdom, kingdomToCallToWarAgainst);
			}
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000F0348 File Offset: 0x000EE548
		public void EndCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			this.RemoveCallToWarAgreement(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
			CampaignEventDispatcher.Instance.OnCallToWarAgreementEnded(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000F0360 File Offset: 0x000EE560
		public void DenyCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom)
		{
			this.ApplyDenyingCallToWarOfferPenalty(callingKingdom, calledKingdom);
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000F036C File Offset: 0x000EE56C
		public List<Kingdom> GetKingdomsToCallToWarAgainst(Kingdom callingKingdom, Kingdom calledKingdom)
		{
			if (callingKingdom != calledKingdom)
			{
				return this._callToWarAgreements.WhereQ<AllianceCampaignBehavior.CallToWarAgreement>((AllianceCampaignBehavior.CallToWarAgreement c) => c.CallingKingdom == callingKingdom && c.CalledKingdom == calledKingdom).SelectQ<AllianceCampaignBehavior.CallToWarAgreement, Kingdom>((AllianceCampaignBehavior.CallToWarAgreement x) => x.KingdomToCallToWarAgainst).ToListQ<Kingdom>();
			}
			return new List<Kingdom>();
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000F03E4 File Offset: 0x000EE5E4
		private AllianceCampaignBehavior.Alliance AddAlliance(Kingdom kingdom1, Kingdom kingdom2)
		{
			AllianceCampaignBehavior.Alliance alliance = new AllianceCampaignBehavior.Alliance(kingdom1, kingdom2, CampaignTime.Now + Campaign.Current.Models.AllianceModel.MaxDurationOfAlliance);
			this._alliances.Add(alliance);
			kingdom1.UpdateAlliedKingdoms();
			kingdom2.UpdateAlliedKingdoms();
			return alliance;
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000F0434 File Offset: 0x000EE634
		private void RemoveAlliance(Kingdom kingdom1, Kingdom kingdom2)
		{
			int num = this._alliances.Count - 1;
			while (-1 < num)
			{
				AllianceCampaignBehavior.Alliance alliance = this._alliances[num];
				if ((alliance.Kingdom1 == kingdom1 && alliance.Kingdom2 == kingdom2) || (alliance.Kingdom2 == kingdom1 && alliance.Kingdom1 == kingdom2))
				{
					this._alliances.RemoveAt(num);
					break;
				}
				num--;
			}
			kingdom1.UpdateAlliedKingdoms();
			kingdom2.UpdateAlliedKingdoms();
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000F04A4 File Offset: 0x000EE6A4
		private AllianceCampaignBehavior.CallToWarAgreement AddCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement = new AllianceCampaignBehavior.CallToWarAgreement(callingKingdom, calledKingdom, kingdomToCallToWarAgainst, CampaignTime.Now + Campaign.Current.Models.AllianceModel.MaxDurationOfWarParticipation);
			this._callToWarAgreements.Add(callToWarAgreement);
			return callToWarAgreement;
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		private void RemoveCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			int num = this._callToWarAgreements.Count - 1;
			while (-1 < num)
			{
				AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement = this._callToWarAgreements[num];
				if (callToWarAgreement.CallingKingdom == callingKingdom && callToWarAgreement.CalledKingdom == calledKingdom && callToWarAgreement.KingdomToCallToWarAgainst == kingdomToCallToWarAgainst)
				{
					this._callToWarAgreements.RemoveAt(num);
					return;
				}
				num--;
			}
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000F0544 File Offset: 0x000EE744
		private List<AllianceCampaignBehavior.CallToWarAgreement> GetCallToWarAgreements(Kingdom kingdom1, Kingdom kingdom2)
		{
			if (kingdom1 != kingdom2)
			{
				return this._callToWarAgreements.Where<AllianceCampaignBehavior.CallToWarAgreement>((AllianceCampaignBehavior.CallToWarAgreement c) => (c.CallingKingdom == kingdom1 && c.CalledKingdom == kingdom2) || (c.CallingKingdom == kingdom2 && c.CalledKingdom == kingdom1)).ToListQ<AllianceCampaignBehavior.CallToWarAgreement>();
			}
			return new List<AllianceCampaignBehavior.CallToWarAgreement>();
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000F0598 File Offset: 0x000EE798
		private bool TryGetAlliance(Kingdom kingdom1, Kingdom kingdom2, out AllianceCampaignBehavior.Alliance foundAlliance)
		{
			bool flag = false;
			foundAlliance = default(AllianceCampaignBehavior.Alliance);
			foreach (AllianceCampaignBehavior.Alliance alliance in this._alliances)
			{
				if ((alliance.Kingdom1 == kingdom1 && alliance.Kingdom2 == kingdom2) || (alliance.Kingdom2 == kingdom1 && alliance.Kingdom1 == kingdom2))
				{
					foundAlliance = alliance;
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000F0620 File Offset: 0x000EE820
		private void UpdateAllianceEndTime(Kingdom kingdom1, Kingdom kingdom2, CampaignTime newEndTime)
		{
			for (int i = 0; i < this._alliances.Count; i++)
			{
				AllianceCampaignBehavior.Alliance alliance = this._alliances[i];
				if (((alliance.Kingdom1 == kingdom1 && alliance.Kingdom2 == kingdom2) || (alliance.Kingdom2 == kingdom1 && alliance.Kingdom1 == kingdom2)) && alliance.EndTime < newEndTime)
				{
					this._alliances[i] = new AllianceCampaignBehavior.Alliance(this._alliances[i].Kingdom1, this._alliances[i].Kingdom2, newEndTime);
					return;
				}
			}
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000F06BA File Offset: 0x000EE8BA
		private void AcceptStartingAlliance(Kingdom proposerKingdom)
		{
			this.StartAlliance(proposerKingdom, Clan.PlayerClan.Kingdom);
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000F06CD File Offset: 0x000EE8CD
		private void ConfirmAllianceOffer(Kingdom proposerKingdom)
		{
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				this.AcceptStartingAlliance(proposerKingdom);
				return;
			}
			this.AddAllianceDecision(Clan.PlayerClan.Kingdom, proposerKingdom);
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000F06F4 File Offset: 0x000EE8F4
		private void ConfirmCallToWarAgreementProposalOffer(Kingdom proposedKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision = new ProposeCallToWarAgreementDecision(Clan.PlayerClan, proposedKingdom, kingdomToCallToWarAgainst);
			int callToWarCost = proposeCallToWarAgreementDecision.CallToWarCost;
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				this.StartCallToWarAgreement(Clan.PlayerClan.Kingdom, proposedKingdom, kingdomToCallToWarAgainst, callToWarCost, false);
				return;
			}
			KingdomDecision kingdomDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
			{
				ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision2;
				return (proposeCallToWarAgreementDecision2 = s as ProposeCallToWarAgreementDecision) != null && proposeCallToWarAgreementDecision2.CalledKingdom == proposedKingdom && proposeCallToWarAgreementDecision2.KingdomToCallToWarAgainst == kingdomToCallToWarAgainst;
			});
			if (kingdomDecision != null)
			{
				Clan.PlayerClan.Kingdom.RemoveDecision(kingdomDecision);
			}
			Clan.PlayerClan.Kingdom.AddDecision(proposeCallToWarAgreementDecision, true);
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000F07A4 File Offset: 0x000EE9A4
		private void ConfirmCallToWarAgreementOffer(Kingdom proposerKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			AcceptCallToWarAgreementDecision acceptCallToWarAgreementDecision = new AcceptCallToWarAgreementDecision(Clan.PlayerClan, proposerKingdom, kingdomToCallToWarAgainst);
			int callToWarCost = acceptCallToWarAgreementDecision.CallToWarCost;
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				this.StartCallToWarAgreement(proposerKingdom, Clan.PlayerClan.Kingdom, kingdomToCallToWarAgainst, callToWarCost, false);
				return;
			}
			KingdomDecision kingdomDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
			{
				AcceptCallToWarAgreementDecision acceptCallToWarAgreementDecision2;
				return (acceptCallToWarAgreementDecision2 = s as AcceptCallToWarAgreementDecision) != null && acceptCallToWarAgreementDecision2.CallingKingdom == proposerKingdom && acceptCallToWarAgreementDecision2.KingdomToCallToWarAgainst == kingdomToCallToWarAgainst;
			});
			if (kingdomDecision != null)
			{
				Clan.PlayerClan.Kingdom.RemoveDecision(kingdomDecision);
			}
			Clan.PlayerClan.Kingdom.AddDecision(acceptCallToWarAgreementDecision, true);
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000F0853 File Offset: 0x000EEA53
		private void ApplyBrokenAlliancePenalty(Kingdom kingdom, Kingdom otherKingdom, DeclareWarAction.DeclareWarDetail detail)
		{
			Hero hero = ((detail == DeclareWarAction.DeclareWarDetail.CausedByPlayerHostility) ? Hero.MainHero : kingdom.Leader);
			ChangeRelationAction.ApplyRelationChangeBetweenHeroes(hero, otherKingdom.Leader, -100, true);
			if (hero == Hero.MainHero)
			{
				TraitLevelingHelper.OnAllianceBrokenThroughHostility();
			}
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000F0881 File Offset: 0x000EEA81
		private void ApplyDenyingCallToWarOfferPenalty(Kingdom callingKingdom, Kingdom calledKingdom)
		{
			ChangeRelationAction.ApplyRelationChangeBetweenHeroes(calledKingdom.Leader, callingKingdom.Leader, -50, true);
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000F0897 File Offset: 0x000EEA97
		private void ApplyAcceptingCallToWarOfferBonus(Kingdom callingKingdom, Kingdom calledKingdom)
		{
			ChangeRelationAction.ApplyRelationChangeBetweenHeroes(calledKingdom.Leader, callingKingdom.Leader, 10, true);
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000F08B0 File Offset: 0x000EEAB0
		private void AddAllianceDecision(Kingdom kingdomToAddDecision, Kingdom kingdomToOffer)
		{
			KingdomDecision kingdomDecision = kingdomToAddDecision.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
			{
				StartAllianceDecision startAllianceDecision2;
				return (startAllianceDecision2 = s as StartAllianceDecision) != null && startAllianceDecision2.KingdomToStartAllianceWith == kingdomToOffer;
			});
			if (kingdomDecision != null)
			{
				kingdomToAddDecision.RemoveDecision(kingdomDecision);
			}
			StartAllianceDecision startAllianceDecision = new StartAllianceDecision(Campaign.Current.Models.AllianceModel.GetProposerClanForAllianceDecision(kingdomToAddDecision, kingdomToOffer), kingdomToOffer);
			TextObject textObject;
			if (startAllianceDecision.CanMakeDecision(out textObject, false))
			{
				kingdomToAddDecision.AddDecision(startAllianceDecision, true);
			}
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000F0928 File Offset: 0x000EEB28
		private static void RefreshAlliedKingdoms()
		{
			foreach (Kingdom kingdom in Kingdom.All)
			{
				kingdom.UpdateAlliedKingdoms();
			}
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x000F0978 File Offset: 0x000EEB78
		private void DailyTickClan(Clan clan)
		{
			if (!clan.IsEliminated)
			{
				clan.Aggressiveness -= 1f;
				if (clan.Kingdom != null && clan.Kingdom.RulingClan == clan)
				{
					Kingdom kingdom = clan.Kingdom;
					if (!kingdom.AlliedKingdoms.IsEmpty<Kingdom>())
					{
						for (int i = kingdom.AlliedKingdoms.Count - 1; i > -1; i--)
						{
							Kingdom kingdom2 = kingdom.AlliedKingdoms[i];
							AllianceCampaignBehavior.Alliance alliance;
							if (this.TryGetAlliance(kingdom2, kingdom, out alliance))
							{
								List<AllianceCampaignBehavior.CallToWarAgreement> callToWarAgreements = this.GetCallToWarAgreements(kingdom, kingdom2);
								for (int j = callToWarAgreements.Count - 1; j > -1; j--)
								{
									AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement = callToWarAgreements[j];
									if (callToWarAgreement.EndTime.IsPast)
									{
										this.EndCallToWarAgreement(callToWarAgreement.CallingKingdom, callToWarAgreement.CalledKingdom, callToWarAgreement.KingdomToCallToWarAgainst);
									}
								}
								if (alliance.EndTime.IsPast)
								{
									this.EndAlliance(kingdom, kingdom2);
									if (kingdom == Clan.PlayerClan.Kingdom)
									{
										this.AddAllianceDecision(kingdom, kingdom2);
									}
									else
									{
										this.AddAllianceDecision(kingdom2, kingdom);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000F0A9C File Offset: 0x000EEC9C
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
		{
			if (faction1.IsKingdomFaction && faction2.IsKingdomFaction)
			{
				Kingdom kingdom = (Kingdom)faction1;
				Kingdom kingdom2 = (Kingdom)faction2;
				if (kingdom.IsAllyWith(kingdom2))
				{
					this.ApplyBrokenAlliancePenalty(kingdom, kingdom2, detail);
					this.EndAlliance(kingdom, kingdom2);
				}
				foreach (Kingdom kingdom3 in kingdom.AlliedKingdoms.ToList<Kingdom>())
				{
					if (!kingdom3.IsAtWarWith(kingdom2))
					{
						if (kingdom == Clan.PlayerClan.Kingdom && !Hero.MainHero.Clan.IsUnderMercenaryService)
						{
							this.OnCallToWarAgreementProposedByPlayerKingdom(kingdom3, kingdom2);
						}
						else
						{
							ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision = new ProposeCallToWarAgreementDecision(kingdom.RulingClan, kingdom3, kingdom2);
							kingdom.AddDecision(proposeCallToWarAgreementDecision, true);
						}
					}
				}
				foreach (Kingdom kingdom4 in kingdom2.AlliedKingdoms.ToList<Kingdom>())
				{
					if (!kingdom4.IsAtWarWith(kingdom))
					{
						if (kingdom2 == Clan.PlayerClan.Kingdom && !Hero.MainHero.Clan.IsUnderMercenaryService)
						{
							this.OnCallToWarAgreementProposedByPlayerKingdom(kingdom4, kingdom);
						}
						else
						{
							ProposeCallToWarAgreementDecision proposeCallToWarAgreementDecision2 = new ProposeCallToWarAgreementDecision(kingdom2.RulingClan, kingdom4, kingdom);
							kingdom2.AddDecision(proposeCallToWarAgreementDecision2, true);
						}
					}
				}
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000F0C04 File Offset: 0x000EEE04
		private void OnMakePeace(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			if (faction1.IsKingdomFaction && faction2.IsKingdomFaction)
			{
				Kingdom kingdom1 = (Kingdom)faction1;
				Kingdom kingdom2 = (Kingdom)faction2;
				using (List<Kingdom>.Enumerator enumerator = kingdom1.AlliedKingdoms.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Kingdom ally2 = enumerator.Current;
						if (this._callToWarAgreements.AnyQ<AllianceCampaignBehavior.CallToWarAgreement>((AllianceCampaignBehavior.CallToWarAgreement c) => c.CallingKingdom == kingdom1 && c.CalledKingdom == ally2 && c.KingdomToCallToWarAgainst == kingdom2))
						{
							this.EndCallToWarAgreement(kingdom1, ally2, kingdom2);
						}
					}
				}
				using (List<Kingdom>.Enumerator enumerator = kingdom2.AlliedKingdoms.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Kingdom ally = enumerator.Current;
						if (this._callToWarAgreements.AnyQ<AllianceCampaignBehavior.CallToWarAgreement>((AllianceCampaignBehavior.CallToWarAgreement c) => c.CallingKingdom == kingdom2 && c.CalledKingdom == ally && c.KingdomToCallToWarAgainst == kingdom1))
						{
							this.EndCallToWarAgreement(kingdom2, ally, kingdom1);
						}
					}
				}
			}
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x000F0D64 File Offset: 0x000EEF64
		private void OnKingdomDestroyed(Kingdom kingdom)
		{
			IEnumerable<AllianceCampaignBehavior.Alliance> alliances = this._alliances;
			Func<AllianceCampaignBehavior.Alliance, bool> <>9__0;
			Func<AllianceCampaignBehavior.Alliance, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (AllianceCampaignBehavior.Alliance a) => a.Kingdom1 == kingdom || a.Kingdom2 == kingdom);
			}
			foreach (AllianceCampaignBehavior.Alliance alliance in alliances.Where<AllianceCampaignBehavior.Alliance>(func).ToList<AllianceCampaignBehavior.Alliance>())
			{
				this.EndAlliance(alliance.Kingdom1, alliance.Kingdom2);
			}
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000F0DF8 File Offset: 0x000EEFF8
		private void OnGameLoadFinished()
		{
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.4.0.110693", 0)))
			{
				for (int i = 0; i < this._callToWarAgreements.Count; i++)
				{
					AllianceCampaignBehavior.CallToWarAgreement callToWarAgreement = this._callToWarAgreements[i];
					this.UpdateAllianceEndTime(callToWarAgreement.CallingKingdom, callToWarAgreement.CalledKingdom, callToWarAgreement.EndTime);
				}
			}
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000F0E61 File Offset: 0x000EF061
		private void OnNewGameCreated(CampaignGameStarter starter)
		{
			AllianceCampaignBehavior.RefreshAlliedKingdoms();
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000F0E68 File Offset: 0x000EF068
		private void OnGameLoaded(CampaignGameStarter starter)
		{
			AllianceCampaignBehavior.RefreshAlliedKingdoms();
		}

		// Token: 0x04001239 RID: 4665
		private const int BreakingAllianceRelationPenalty = -100;

		// Token: 0x0400123A RID: 4666
		private const int DenyingCallToWarRelationPenalty = -50;

		// Token: 0x0400123B RID: 4667
		private const int AcceptingCallToWarRelationBonus = 10;

		// Token: 0x0400123C RID: 4668
		private List<AllianceCampaignBehavior.Alliance> _alliances = new List<AllianceCampaignBehavior.Alliance>();

		// Token: 0x0400123D RID: 4669
		private List<AllianceCampaignBehavior.CallToWarAgreement> _callToWarAgreements = new List<AllianceCampaignBehavior.CallToWarAgreement>();

		// Token: 0x020007A2 RID: 1954
		public class AllianceCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06006304 RID: 25348 RVA: 0x001C116B File Offset: 0x001BF36B
			public AllianceCampaignBehaviorTypeDefiner()
				: base(312270)
			{
			}

			// Token: 0x06006305 RID: 25349 RVA: 0x001C1178 File Offset: 0x001BF378
			protected override void DefineStructTypes()
			{
				base.AddStructDefinition(typeof(AllianceCampaignBehavior.Alliance), 1, null);
				base.AddStructDefinition(typeof(AllianceCampaignBehavior.CallToWarAgreement), 2, null);
			}

			// Token: 0x06006306 RID: 25350 RVA: 0x001C119E File Offset: 0x001BF39E
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(List<AllianceCampaignBehavior.Alliance>));
				base.ConstructContainerDefinition(typeof(List<AllianceCampaignBehavior.CallToWarAgreement>));
			}
		}

		// Token: 0x020007A3 RID: 1955
		internal struct Alliance
		{
			// Token: 0x06006307 RID: 25351 RVA: 0x001C11C0 File Offset: 0x001BF3C0
			public Alliance(Kingdom kingdom1, Kingdom kingdom2, CampaignTime endTime)
			{
				this.Kingdom1 = kingdom1;
				this.Kingdom2 = kingdom2;
				this.EndTime = endTime;
			}

			// Token: 0x06006308 RID: 25352 RVA: 0x001C11D8 File Offset: 0x001BF3D8
			public static void AutoGeneratedStaticCollectObjectsAlliance(object o, List<object> collectedObjects)
			{
				((AllianceCampaignBehavior.Alliance)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06006309 RID: 25353 RVA: 0x001C11F4 File Offset: 0x001BF3F4
			private void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.Kingdom1);
				collectedObjects.Add(this.Kingdom2);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.EndTime, collectedObjects);
			}

			// Token: 0x0600630A RID: 25354 RVA: 0x001C121F File Offset: 0x001BF41F
			internal static object AutoGeneratedGetMemberValueKingdom1(object o)
			{
				return ((AllianceCampaignBehavior.Alliance)o).Kingdom1;
			}

			// Token: 0x0600630B RID: 25355 RVA: 0x001C122C File Offset: 0x001BF42C
			internal static object AutoGeneratedGetMemberValueKingdom2(object o)
			{
				return ((AllianceCampaignBehavior.Alliance)o).Kingdom2;
			}

			// Token: 0x0600630C RID: 25356 RVA: 0x001C1239 File Offset: 0x001BF439
			internal static object AutoGeneratedGetMemberValueEndTime(object o)
			{
				return ((AllianceCampaignBehavior.Alliance)o).EndTime;
			}

			// Token: 0x04001F08 RID: 7944
			[SaveableField(0)]
			public readonly Kingdom Kingdom1;

			// Token: 0x04001F09 RID: 7945
			[SaveableField(1)]
			public readonly Kingdom Kingdom2;

			// Token: 0x04001F0A RID: 7946
			[SaveableField(2)]
			public CampaignTime EndTime;
		}

		// Token: 0x020007A4 RID: 1956
		internal struct CallToWarAgreement
		{
			// Token: 0x0600630D RID: 25357 RVA: 0x001C124B File Offset: 0x001BF44B
			public CallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst, CampaignTime endTime)
			{
				this.CallingKingdom = callingKingdom;
				this.CalledKingdom = calledKingdom;
				this.KingdomToCallToWarAgainst = kingdomToCallToWarAgainst;
				this.EndTime = endTime;
			}

			// Token: 0x0600630E RID: 25358 RVA: 0x001C126C File Offset: 0x001BF46C
			public static void AutoGeneratedStaticCollectObjectsCallToWarAgreement(object o, List<object> collectedObjects)
			{
				((AllianceCampaignBehavior.CallToWarAgreement)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600630F RID: 25359 RVA: 0x001C1288 File Offset: 0x001BF488
			private void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.CallingKingdom);
				collectedObjects.Add(this.CalledKingdom);
				collectedObjects.Add(this.KingdomToCallToWarAgainst);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.EndTime, collectedObjects);
			}

			// Token: 0x06006310 RID: 25360 RVA: 0x001C12BF File Offset: 0x001BF4BF
			internal static object AutoGeneratedGetMemberValueCallingKingdom(object o)
			{
				return ((AllianceCampaignBehavior.CallToWarAgreement)o).CallingKingdom;
			}

			// Token: 0x06006311 RID: 25361 RVA: 0x001C12CC File Offset: 0x001BF4CC
			internal static object AutoGeneratedGetMemberValueCalledKingdom(object o)
			{
				return ((AllianceCampaignBehavior.CallToWarAgreement)o).CalledKingdom;
			}

			// Token: 0x06006312 RID: 25362 RVA: 0x001C12D9 File Offset: 0x001BF4D9
			internal static object AutoGeneratedGetMemberValueKingdomToCallToWarAgainst(object o)
			{
				return ((AllianceCampaignBehavior.CallToWarAgreement)o).KingdomToCallToWarAgainst;
			}

			// Token: 0x06006313 RID: 25363 RVA: 0x001C12E6 File Offset: 0x001BF4E6
			internal static object AutoGeneratedGetMemberValueEndTime(object o)
			{
				return ((AllianceCampaignBehavior.CallToWarAgreement)o).EndTime;
			}

			// Token: 0x04001F0B RID: 7947
			[SaveableField(0)]
			public readonly Kingdom CallingKingdom;

			// Token: 0x04001F0C RID: 7948
			[SaveableField(1)]
			public readonly Kingdom CalledKingdom;

			// Token: 0x04001F0D RID: 7949
			[SaveableField(2)]
			public readonly Kingdom KingdomToCallToWarAgainst;

			// Token: 0x04001F0E RID: 7950
			[SaveableField(3)]
			public CampaignTime EndTime;
		}
	}
}
