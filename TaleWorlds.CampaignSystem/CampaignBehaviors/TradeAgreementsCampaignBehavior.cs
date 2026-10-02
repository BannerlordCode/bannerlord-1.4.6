using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000447 RID: 1095
	public class TradeAgreementsCampaignBehavior : CampaignBehaviorBase, ITradeAgreementsCampaignBehavior
	{
		// Token: 0x06004671 RID: 18033 RVA: 0x00160278 File Offset: 0x0015E478
		public override void RegisterEvents()
		{
			CampaignEvents.KingdomDestroyedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomDestroyed));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.WarDeclared));
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.SettlementEntered));
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x001602CC File Offset: 0x0015E4CC
		private void SettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			int num;
			if (party != null && party.IsActive && party.MapFaction != null && party.IsCaravan && settlement.IsTown && party.MapFaction != settlement.MapFaction && settlement.MapFaction.IsKingdomFaction && party.MapFaction.IsKingdomFaction && party.IsPartyTradeActive && !party.IsCurrentlyUsedByAQuest && this.TryGetTradeAgreement((Kingdom)settlement.MapFaction, (Kingdom)party.MapFaction, out num) && !party.IsFleeing())
			{
				Kingdom kingdom = (Kingdom)settlement.MapFaction;
				this._tradeAgreements[num] = this._tradeAgreements[num].AddGainedGoldToKingdom(kingdom, Campaign.Current.Models.TradeAgreementModel.GetProfitPerCaravanVisit(party));
			}
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x001603B4 File Offset: 0x0015E5B4
		public void OnTradeAgreementOfferedToPlayer(Kingdom fromKingdom)
		{
			if (!Clan.PlayerClan.IsUnderMercenaryService)
			{
				KingdomDecision kingdomDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>(delegate(KingdomDecision s)
				{
					TradeAgreementDecision tradeAgreementDecision;
					return (tradeAgreementDecision = s as TradeAgreementDecision) != null && tradeAgreementDecision.TargetKingdom == fromKingdom;
				});
				if (kingdomDecision != null)
				{
					Clan.PlayerClan.Kingdom.RemoveDecision(kingdomDecision);
				}
				Clan.PlayerClan.Kingdom.AddDecision(new TradeAgreementDecision(TradeAgreementDecision.GetProposerClanForPlayerKingdom(fromKingdom), fromKingdom), true);
				return;
			}
			this.AcceptOffer(fromKingdom);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00160441 File Offset: 0x0015E641
		private void AcceptOffer(Kingdom fromKingdom)
		{
			this.MakeTradeAgreement(fromKingdom, Clan.PlayerClan.Kingdom, Campaign.Current.Models.TradeAgreementModel.GetTradeAgreementDurationInYears(fromKingdom, Clan.PlayerClan.Kingdom));
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00160474 File Offset: 0x0015E674
		private void WarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
		{
			Kingdom kingdom;
			Kingdom kingdom2;
			if ((kingdom = faction1 as Kingdom) != null && (kingdom2 = faction2 as Kingdom) != null && this.HasTradeAgreement(kingdom, kingdom2))
			{
				this.EndTradeAgreement(kingdom, kingdom2);
				this.ApplyBrokenTradeAgreementPenalty(kingdom, kingdom2, detail);
			}
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x001604B0 File Offset: 0x0015E6B0
		private void ApplyBrokenTradeAgreementPenalty(Kingdom kingdom, Kingdom otherKingdom, DeclareWarAction.DeclareWarDetail detail)
		{
			Hero hero = ((detail == DeclareWarAction.DeclareWarDetail.CausedByPlayerHostility) ? Hero.MainHero : kingdom.Leader);
			ChangeRelationAction.ApplyRelationChangeBetweenHeroes(hero, otherKingdom.Leader, -50, true);
			if (hero == Hero.MainHero)
			{
				TraitLevelingHelper.OnTradeAgreementBroken();
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x001604DE File Offset: 0x0015E6DE
		private void OnKingdomDestroyed(Kingdom kingdom)
		{
			this.EndTradeAgreementsOfKingdom(kingdom);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x001604E7 File Offset: 0x0015E6E7
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<TradeAgreementsCampaignBehavior.TradeAgreement>>("_tradeAgreements", ref this._tradeAgreements);
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x001604FC File Offset: 0x0015E6FC
		public void MakeTradeAgreement(Kingdom kingdom1, Kingdom kingdom2, CampaignTime duration)
		{
			Debug.Print(string.Format("Trade agreement signed between {0} and {1}", kingdom1.Name, kingdom2.Name), 0, Debug.DebugColor.White, 17592186044416UL);
			TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement = new TradeAgreementsCampaignBehavior.TradeAgreement(kingdom1, kingdom2, CampaignTime.Now + duration);
			this._tradeAgreements.Add(tradeAgreement);
			CampaignEventDispatcher.Instance.OnTradeAgreementSigned(kingdom1, kingdom2);
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x0016055C File Offset: 0x0015E75C
		public void EndTradeAgreementsOfKingdom(Kingdom kingdom)
		{
			this._tradeAgreements.RemoveAll((TradeAgreementsCampaignBehavior.TradeAgreement t) => t.Kingdom1 == kingdom || t.Kingdom2 == kingdom);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x0016058E File Offset: 0x0015E78E
		public void EndTradeAgreement(Kingdom kingdom1, Kingdom kingdom2)
		{
			this.RemoveTradeAgreement(kingdom1, kingdom2);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x00160598 File Offset: 0x0015E798
		private bool HasTradeAgreement(Kingdom kingdom1, Kingdom kingdom2)
		{
			TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement;
			return this.HasTradeAgreement(kingdom1, kingdom2, out tradeAgreement);
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001605B0 File Offset: 0x0015E7B0
		public bool HasTradeAgreement(Kingdom kingdom1, Kingdom kingdom2, out TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement)
		{
			bool flag = false;
			tradeAgreement = default(TradeAgreementsCampaignBehavior.TradeAgreement);
			int num;
			if (this.TryGetTradeAgreement(kingdom1, kingdom2, out num))
			{
				tradeAgreement = this._tradeAgreements[num];
				if (tradeAgreement.EndTime.IsPast)
				{
					this.EndTradeAgreement(kingdom1, kingdom2);
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x00160604 File Offset: 0x0015E804
		public CampaignTime GetTradeAgreementEndDate(Kingdom kingdom1, Kingdom kingdom2)
		{
			int num;
			if (!this.TryGetTradeAgreement(kingdom1, kingdom2, out num))
			{
				Debug.FailedAssert("Cant find trade agreement", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\TradeAgreementsCampaignBehavior.cs", "GetTradeAgreementEndDate", 236);
				return CampaignTime.Zero;
			}
			return this._tradeAgreements[num].EndTime;
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x00160650 File Offset: 0x0015E850
		private bool TryGetTradeAgreement(Kingdom kingdom1, Kingdom kingdom2, out int index)
		{
			index = -1;
			bool flag = false;
			for (int i = 0; i < this._tradeAgreements.Count; i++)
			{
				TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement = this._tradeAgreements[i];
				if ((tradeAgreement.Kingdom1 == kingdom1 && tradeAgreement.Kingdom2 == kingdom2) || (tradeAgreement.Kingdom2 == kingdom1 && tradeAgreement.Kingdom1 == kingdom2))
				{
					flag = true;
					index = i;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x001606B4 File Offset: 0x0015E8B4
		private void RemoveTradeAgreement(Kingdom kingdom1, Kingdom kingdom2)
		{
			int num = this._tradeAgreements.Count - 1;
			while (-1 < num)
			{
				TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement = this._tradeAgreements[num];
				if ((tradeAgreement.Kingdom1 == kingdom1 && tradeAgreement.Kingdom2 == kingdom2) || (tradeAgreement.Kingdom2 == kingdom1 && tradeAgreement.Kingdom1 == kingdom2))
				{
					this._tradeAgreements.RemoveAt(num);
					return;
				}
				num--;
			}
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00160718 File Offset: 0x0015E918
		public void OnTradeGoldDistributedInKingdom(Kingdom kingdom1, Kingdom kingdom2, Clan clan, int share)
		{
			int num;
			if (this.TryGetTradeAgreement(kingdom1, kingdom2, out num))
			{
				this._tradeAgreements[num] = this._tradeAgreements[num].OnGoldSharedInKingdom(clan.Kingdom, share);
				return;
			}
			Debug.FailedAssert("cant find agreement", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\TradeAgreementsCampaignBehavior.cs", "OnTradeGoldDistributedInKingdom", 284);
		}

		// Token: 0x040013BB RID: 5051
		private const int BreakingAgreementRelationPenalty = -50;

		// Token: 0x040013BC RID: 5052
		private List<TradeAgreementsCampaignBehavior.TradeAgreement> _tradeAgreements = new List<TradeAgreementsCampaignBehavior.TradeAgreement>();

		// Token: 0x0200085B RID: 2139
		public class TradeAgreementsCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06006822 RID: 26658 RVA: 0x001C9EF2 File Offset: 0x001C80F2
			public TradeAgreementsCampaignBehaviorTypeDefiner()
				: base(312260)
			{
			}

			// Token: 0x06006823 RID: 26659 RVA: 0x001C9EFF File Offset: 0x001C80FF
			protected override void DefineStructTypes()
			{
				base.AddStructDefinition(typeof(TradeAgreementsCampaignBehavior.TradeAgreement), 1, null);
			}

			// Token: 0x06006824 RID: 26660 RVA: 0x001C9F13 File Offset: 0x001C8113
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(List<TradeAgreementsCampaignBehavior.TradeAgreement>));
			}
		}

		// Token: 0x0200085C RID: 2140
		public struct TradeAgreement
		{
			// Token: 0x06006825 RID: 26661 RVA: 0x001C9F25 File Offset: 0x001C8125
			public TradeAgreement(Kingdom kingdom1, Kingdom kingdom2, CampaignTime endTime)
			{
				this = default(TradeAgreementsCampaignBehavior.TradeAgreement);
				this.Kingdom1 = kingdom1;
				this.Kingdom2 = kingdom2;
				this.EndTime = endTime;
				this.Kingdom1GoldGained = 0;
				this.Kingdom2GoldGained = 0;
				this.Kingdom1GoldGainedTotal = 0;
				this.Kingdom2GoldGainedTotal = 0;
			}

			// Token: 0x06006826 RID: 26662 RVA: 0x001C9F60 File Offset: 0x001C8160
			public TradeAgreementsCampaignBehavior.TradeAgreement AddGainedGoldToKingdom(Kingdom kingdom, int gold)
			{
				if (kingdom == this.Kingdom1)
				{
					this.Kingdom1GoldGained += gold;
					this.Kingdom1GoldGainedTotal += gold;
				}
				else
				{
					this.Kingdom2GoldGained += gold;
					this.Kingdom2GoldGainedTotal += gold;
				}
				return this;
			}

			// Token: 0x06006827 RID: 26663 RVA: 0x001C9FB6 File Offset: 0x001C81B6
			public TradeAgreementsCampaignBehavior.TradeAgreement OnGoldSharedInKingdom(Kingdom kingdom, int gold)
			{
				if (kingdom == this.Kingdom1)
				{
					this.Kingdom1GoldGained -= gold;
				}
				else
				{
					this.Kingdom2GoldGained -= gold;
				}
				return this;
			}

			// Token: 0x06006828 RID: 26664 RVA: 0x001C9FE8 File Offset: 0x001C81E8
			public static void AutoGeneratedStaticCollectObjectsTradeAgreement(object o, List<object> collectedObjects)
			{
				((TradeAgreementsCampaignBehavior.TradeAgreement)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06006829 RID: 26665 RVA: 0x001CA004 File Offset: 0x001C8204
			private void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.Kingdom1);
				collectedObjects.Add(this.Kingdom2);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.EndTime, collectedObjects);
			}

			// Token: 0x0600682A RID: 26666 RVA: 0x001CA02F File Offset: 0x001C822F
			internal static object AutoGeneratedGetMemberValueKingdom1(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).Kingdom1;
			}

			// Token: 0x0600682B RID: 26667 RVA: 0x001CA03C File Offset: 0x001C823C
			internal static object AutoGeneratedGetMemberValueKingdom2(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).Kingdom2;
			}

			// Token: 0x0600682C RID: 26668 RVA: 0x001CA049 File Offset: 0x001C8249
			internal static object AutoGeneratedGetMemberValueEndTime(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).EndTime;
			}

			// Token: 0x0600682D RID: 26669 RVA: 0x001CA05B File Offset: 0x001C825B
			internal static object AutoGeneratedGetMemberValueKingdom1GoldGained(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).Kingdom1GoldGained;
			}

			// Token: 0x0600682E RID: 26670 RVA: 0x001CA06D File Offset: 0x001C826D
			internal static object AutoGeneratedGetMemberValueKingdom2GoldGained(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).Kingdom2GoldGained;
			}

			// Token: 0x0600682F RID: 26671 RVA: 0x001CA07F File Offset: 0x001C827F
			internal static object AutoGeneratedGetMemberValueKingdom1GoldGainedTotal(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).Kingdom1GoldGainedTotal;
			}

			// Token: 0x06006830 RID: 26672 RVA: 0x001CA091 File Offset: 0x001C8291
			internal static object AutoGeneratedGetMemberValueKingdom2GoldGainedTotal(object o)
			{
				return ((TradeAgreementsCampaignBehavior.TradeAgreement)o).Kingdom2GoldGainedTotal;
			}

			// Token: 0x040023EA RID: 9194
			[SaveableField(1)]
			public readonly Kingdom Kingdom1;

			// Token: 0x040023EB RID: 9195
			[SaveableField(2)]
			public readonly Kingdom Kingdom2;

			// Token: 0x040023EC RID: 9196
			[SaveableField(3)]
			public readonly CampaignTime EndTime;

			// Token: 0x040023ED RID: 9197
			[SaveableField(4)]
			public int Kingdom1GoldGained;

			// Token: 0x040023EE RID: 9198
			[SaveableField(5)]
			public int Kingdom2GoldGained;

			// Token: 0x040023EF RID: 9199
			[SaveableField(6)]
			public int Kingdom1GoldGainedTotal;

			// Token: 0x040023F0 RID: 9200
			[SaveableField(7)]
			public int Kingdom2GoldGainedTotal;
		}
	}
}
