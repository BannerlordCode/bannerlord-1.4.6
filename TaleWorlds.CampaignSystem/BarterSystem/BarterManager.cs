using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x0200047D RID: 1149
	public class BarterManager
	{
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x00172A8A File Offset: 0x00170C8A
		public static BarterManager Instance
		{
			get
			{
				return Campaign.Current.BarterManager;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x00172A96 File Offset: 0x00170C96
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x00172A9E File Offset: 0x00170C9E
		[SaveableProperty(1)]
		public bool LastBarterIsAccepted { get; internal set; }

		// Token: 0x060048E7 RID: 18663 RVA: 0x00172AA7 File Offset: 0x00170CA7
		public BarterManager()
		{
			this._barteredHeroes = new Dictionary<Hero, CampaignTime>();
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x00172ABA File Offset: 0x00170CBA
		public void BeginPlayerBarter(BarterData args)
		{
			if (this.BarterBegin != null)
			{
				this.BarterBegin(args);
			}
			ICampaignMission campaignMission = CampaignMission.Current;
			if (campaignMission == null)
			{
				return;
			}
			campaignMission.SetMissionMode(MissionMode.Barter, false);
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x00172AE4 File Offset: 0x00170CE4
		private void AddBaseBarterables(BarterData args, IEnumerable<Barterable> defaultBarterables)
		{
			if (defaultBarterables != null)
			{
				bool flag = false;
				foreach (Barterable barterable in defaultBarterables)
				{
					if (!flag)
					{
						args.AddBarterGroup(new DefaultsBarterGroup());
						flag = true;
					}
					barterable.SetIsOffered(true);
					args.AddBarterable<OtherBarterGroup>(barterable, true);
					barterable.SetIsOffered(true);
				}
			}
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x00172B50 File Offset: 0x00170D50
		public void StartBarterOffer(Hero offerer, Hero other, PartyBase offererParty, PartyBase otherParty, Hero beneficiaryOfOtherHero = null, BarterManager.BarterContextInitializer InitContext = null, int persuasionCostReduction = 0, bool isAIBarter = false, IEnumerable<Barterable> defaultBarterables = null)
		{
			this.LastBarterIsAccepted = false;
			if (offerer == Hero.MainHero && other != null && InitContext == null)
			{
				if (!this.CanPlayerBarterWithHero(other))
				{
					Debug.FailedAssert("Barter with the hero is on cooldown.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\BarterSystem\\BarterManager.cs", "StartBarterOffer", 83);
					return;
				}
				this.ClearHeroCooldowns();
			}
			BarterData barterData = new BarterData(offerer, beneficiaryOfOtherHero ?? other, offererParty, otherParty, InitContext, persuasionCostReduction, false);
			this.AddBaseBarterables(barterData, defaultBarterables);
			CampaignEventDispatcher.Instance.OnBarterablesRequested(barterData);
			Campaign.Current.ConversationManager.CurrentConversationIsFirst = false;
			if (!isAIBarter)
			{
				Campaign.Current.BarterManager.BeginPlayerBarter(barterData);
			}
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x00172BE8 File Offset: 0x00170DE8
		public void ExecuteAiBarter(IFaction faction1, IFaction faction2, Hero faction1Hero, Hero faction2Hero, Barterable barterable)
		{
			this.ExecuteAiBarter(faction1, faction2, faction1Hero, faction2Hero, new Barterable[] { barterable });
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x00172C0C File Offset: 0x00170E0C
		public void ExecuteAiBarter(IFaction faction1, IFaction faction2, Hero faction1Hero, Hero faction2Hero, IEnumerable<Barterable> baseBarterables)
		{
			BarterData barterData = new BarterData(faction1.Leader, faction2.Leader, null, null, null, 0, true);
			barterData.AddBarterGroup(new DefaultsBarterGroup());
			foreach (Barterable barterable in baseBarterables)
			{
				barterable.SetIsOffered(true);
				barterData.AddBarterable<DefaultsBarterGroup>(barterable, true);
			}
			CampaignEventDispatcher.Instance.OnBarterablesRequested(barterData);
			Campaign.Current.BarterManager.ExecuteAIBarter(barterData, faction1, faction2, faction1Hero, faction2Hero);
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x00172CA0 File Offset: 0x00170EA0
		public void ExecuteAIBarter(BarterData barterData, IFaction faction1, IFaction faction2, Hero faction1Hero, Hero faction2Hero)
		{
			this.MakeBalanced(barterData, faction1, faction2, faction2Hero, 1f);
			this.MakeBalanced(barterData, faction2, faction1, faction1Hero, 1f);
			float offerValueForFaction = this.GetOfferValueForFaction(barterData, faction1);
			float offerValueForFaction2 = this.GetOfferValueForFaction(barterData, faction2);
			if (offerValueForFaction >= 0f && offerValueForFaction2 >= 0f)
			{
				this.ApplyBarterOffer(barterData.OffererHero, barterData.OtherHero, barterData.GetOfferedBarterables());
			}
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x00172D08 File Offset: 0x00170F08
		private void MakeBalanced(BarterData args, IFaction faction1, IFaction faction2, Hero faction2Hero, float fulfillRatio)
		{
			foreach (ValueTuple<Barterable, int> valueTuple in BarterHelper.GetAutoBalanceBarterablesAdd(args, faction1, faction2, faction2Hero, fulfillRatio))
			{
				Barterable item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				if (!item.IsOffered)
				{
					item.SetIsOffered(true);
					item.CurrentAmount = 0;
				}
				item.CurrentAmount += item2;
			}
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x00172D84 File Offset: 0x00170F84
		public void Close()
		{
			if (CampaignMission.Current != null)
			{
				CampaignMission.Current.SetMissionMode(MissionMode.Conversation, false);
			}
			if (this.Closed != null)
			{
				this.Closed();
			}
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00172DAC File Offset: 0x00170FAC
		public bool IsOfferAcceptable(BarterData args, Hero hero, PartyBase party)
		{
			return this.GetOfferValue(hero, party, args.OffererParty, args.GetOfferedBarterables()) > -0.01f;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x00172DCC File Offset: 0x00170FCC
		public float GetOfferValueForFaction(BarterData barterData, IFaction faction)
		{
			int num = 0;
			foreach (Barterable barterable in barterData.GetOfferedBarterables())
			{
				num += barterable.GetValueForFaction(faction);
			}
			return (float)num;
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x00172E28 File Offset: 0x00171028
		public float GetOfferValue(Hero selfHero, PartyBase selfParty, PartyBase offererParty, IEnumerable<Barterable> offeredBarters)
		{
			float num = 0f;
			IFaction faction;
			if (((selfHero != null) ? selfHero.Clan : null) != null)
			{
				IFaction clan = selfHero.Clan;
				faction = clan;
			}
			else
			{
				faction = selfParty.MapFaction;
			}
			IFaction faction2 = faction;
			foreach (Barterable barterable in offeredBarters)
			{
				num += (float)barterable.GetValueForFaction(faction2);
			}
			this._overpayAmount = ((num > 0f) ? num : 0f);
			return num;
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x00172EB4 File Offset: 0x001710B4
		public void ApplyAndFinalizePlayerBarter(Hero offererHero, Hero otherHero, BarterData barterData)
		{
			this.LastBarterIsAccepted = true;
			this.ApplyBarterOffer(offererHero, otherHero, barterData.GetOfferedBarterables());
			if (otherHero != null)
			{
				this.HandleHeroCooldown(otherHero);
			}
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x00172ED5 File Offset: 0x001710D5
		public void CancelAndFinalizePlayerBarter(Hero offererHero, Hero otherHero, BarterData barterData)
		{
			this.CancelBarter(offererHero, otherHero, barterData.GetOfferedBarterables());
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x00172EE8 File Offset: 0x001710E8
		private void ApplyBarterOffer(Hero offererHero, Hero otherHero, List<Barterable> barters)
		{
			foreach (Barterable barterable in barters)
			{
				barterable.Apply();
			}
			CampaignEventDispatcher.Instance.OnBarterAccepted(offererHero, otherHero, barters);
			if (offererHero == Hero.MainHero)
			{
				if (this._overpayAmount > 0f && otherHero != null)
				{
					this.ApplyOverpayBonus(otherHero);
				}
				this.Close();
				if (Campaign.Current.ConversationManager.IsConversationInProgress)
				{
					Campaign.Current.ConversationManager.ContinueConversation();
				}
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_offer_accepted", null), 0, null, null, "");
			}
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00172FA0 File Offset: 0x001711A0
		private void CancelBarter(Hero offererHero, Hero otherHero, List<Barterable> offeredBarters)
		{
			this.Close();
			MBInformationManager.AddQuickInformation(GameTexts.FindText("str_offer_rejected", null), 0, null, null, "");
			CampaignEventDispatcher.Instance.OnBarterCanceled(offererHero, otherHero, offeredBarters);
			Campaign.Current.ConversationManager.ContinueConversation();
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x00172FDC File Offset: 0x001711DC
		private void ApplyOverpayBonus(Hero otherHero)
		{
			if (otherHero.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
			{
				return;
			}
			int num = Campaign.Current.Models.BarterModel.CalculateOverpayRelationIncreaseCosts(otherHero, this._overpayAmount);
			if (num > 0)
			{
				ChangeRelationAction.ApplyRelationChangeBetweenHeroes(Hero.MainHero, otherHero, num, true);
			}
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x00173030 File Offset: 0x00171230
		public bool CanPlayerBarterWithHero(Hero hero)
		{
			CampaignTime campaignTime;
			return !this._barteredHeroes.TryGetValue(hero, out campaignTime) || campaignTime.IsPast;
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x00173058 File Offset: 0x00171258
		private void HandleHeroCooldown(Hero hero)
		{
			CampaignTime campaignTime = CampaignTime.Now + CampaignTime.Days((float)Campaign.Current.Models.BarterModel.BarterCooldownWithHeroInDays);
			if (!this._barteredHeroes.ContainsKey(hero))
			{
				this._barteredHeroes.Add(hero, campaignTime);
				return;
			}
			this._barteredHeroes[hero] = campaignTime;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x001730B4 File Offset: 0x001712B4
		private void ClearHeroCooldowns()
		{
			foreach (KeyValuePair<Hero, CampaignTime> keyValuePair in new Dictionary<Hero, CampaignTime>(this._barteredHeroes))
			{
				if (keyValuePair.Value.IsPast)
				{
					this._barteredHeroes.Remove(keyValuePair.Key);
				}
			}
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x0017312C File Offset: 0x0017132C
		public bool InitializeMarriageBarterContext(Barterable barterable, BarterData args, object obj)
		{
			Hero hero = null;
			Hero hero2 = null;
			if (obj != null)
			{
				Tuple<Hero, Hero> tuple = obj as Tuple<Hero, Hero>;
				if (tuple != null)
				{
					hero = tuple.Item1;
					hero2 = tuple.Item2;
				}
			}
			MarriageBarterable marriageBarterable = barterable as MarriageBarterable;
			return marriageBarterable != null && hero != null && hero2 != null && marriageBarterable.ProposingHero == hero2 && marriageBarterable.HeroBeingProposedTo == hero;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0017317C File Offset: 0x0017137C
		public bool InitializeJoinFactionBarterContext(Barterable barterable, BarterData args, object obj)
		{
			return barterable.GetType() == typeof(JoinKingdomAsClanBarterable) && barterable.OriginalOwner == Hero.OneToOneConversationHero;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x001731A4 File Offset: 0x001713A4
		public bool InitializeMakePeaceBarterContext(Barterable barterable, BarterData args, object obj)
		{
			return barterable.GetType() == typeof(PeaceBarterable) && barterable.OriginalOwner == args.OtherHero;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x001731CD File Offset: 0x001713CD
		public bool InitializeSafePassageBarterContext(Barterable barterable, BarterData args, object obj)
		{
			if (barterable.GetType() == typeof(SafePassageBarterable))
			{
				PartyBase originalParty = barterable.OriginalParty;
				MobileParty conversationParty = MobileParty.ConversationParty;
				return originalParty == ((conversationParty != null) ? conversationParty.Party : null);
			}
			return false;
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x00173201 File Offset: 0x00171401
		internal static void AutoGeneratedStaticCollectObjectsBarterManager(object o, List<object> collectedObjects)
		{
			((BarterManager)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0017320F File Offset: 0x0017140F
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this._barteredHeroes);
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x0017321D File Offset: 0x0017141D
		internal static object AutoGeneratedGetMemberValueLastBarterIsAccepted(object o)
		{
			return ((BarterManager)o).LastBarterIsAccepted;
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0017322F File Offset: 0x0017142F
		internal static object AutoGeneratedGetMemberValue_barteredHeroes(object o)
		{
			return ((BarterManager)o)._barteredHeroes;
		}

		// Token: 0x04001428 RID: 5160
		public BarterManager.BarterCloseEventDelegate Closed;

		// Token: 0x04001429 RID: 5161
		public BarterManager.BarterBeginEventDelegate BarterBegin;

		// Token: 0x0400142A RID: 5162
		[SaveableField(2)]
		private readonly Dictionary<Hero, CampaignTime> _barteredHeroes;

		// Token: 0x0400142B RID: 5163
		private float _overpayAmount;

		// Token: 0x02000883 RID: 2179
		// (Invoke) Token: 0x060068B4 RID: 26804
		public delegate bool BarterContextInitializer(Barterable barterable, BarterData args, object obj = null);

		// Token: 0x02000884 RID: 2180
		// (Invoke) Token: 0x060068B8 RID: 26808
		public delegate void BarterCloseEventDelegate();

		// Token: 0x02000885 RID: 2181
		// (Invoke) Token: 0x060068BC RID: 26812
		public delegate void BarterBeginEventDelegate(BarterData args);
	}
}
