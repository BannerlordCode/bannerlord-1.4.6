using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.AgentOrigins
{
	// Token: 0x0200048F RID: 1167
	public class SimpleAgentOrigin : IAgentOriginBase
	{
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x00175FF8 File Offset: 0x001741F8
		public BasicCharacterObject Troop
		{
			get
			{
				return this._troop;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x00176000 File Offset: 0x00174200
		bool IAgentOriginBase.HasThrownWeapon
		{
			get
			{
				return this._hasThrownWeapon;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060049EE RID: 18926 RVA: 0x00176008 File Offset: 0x00174208
		bool IAgentOriginBase.HasHeavyArmor
		{
			get
			{
				return this._hasHeavyArmor;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x00176010 File Offset: 0x00174210
		bool IAgentOriginBase.HasShield
		{
			get
			{
				return this._hasShield;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x00176018 File Offset: 0x00174218
		bool IAgentOriginBase.HasSpear
		{
			get
			{
				return this._hasSpear;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x00176020 File Offset: 0x00174220
		public bool IsUnderPlayersCommand
		{
			get
			{
				PartyBase party = this.Party;
				return party != null && (party == PartyBase.MainParty || party.Owner == Hero.MainHero || party.MapFaction.Leader == Hero.MainHero);
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x00176064 File Offset: 0x00174264
		public bool IsInSameArmyAsPlayer
		{
			get
			{
				PartyBase party = this.Party;
				MobileParty mobileParty;
				Army army;
				return party != null && (mobileParty = party.MobileParty) != null && (army = mobileParty.Army) != null && army == MobileParty.MainParty.Army && (army.LeaderParty == mobileParty || mobileParty.AttachedTo == army.LeaderParty) && (army.LeaderParty == MobileParty.MainParty || MobileParty.MainParty.AttachedTo == army.LeaderParty);
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x001760D6 File Offset: 0x001742D6
		public uint FactionColor
		{
			get
			{
				if (this.Party != null)
				{
					return this.Party.MapFaction.Color;
				}
				if (this._troop.IsHero)
				{
					return this._troop.HeroObject.MapFaction.Color;
				}
				return 0U;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060049F4 RID: 18932 RVA: 0x00176115 File Offset: 0x00174315
		public uint FactionColor2
		{
			get
			{
				if (this.Party != null)
				{
					return this.Party.MapFaction.Color2;
				}
				if (this._troop.IsHero)
				{
					return this._troop.HeroObject.MapFaction.Color2;
				}
				return 0U;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060049F5 RID: 18933 RVA: 0x00176154 File Offset: 0x00174354
		public int Seed
		{
			get
			{
				if (this.Party != null)
				{
					return CharacterHelper.GetPartyMemberFaceSeed(this.Party, this._troop, this.Rank);
				}
				return CharacterHelper.GetDefaultFaceSeed(this._troop, this.Rank);
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x00176187 File Offset: 0x00174387
		public PartyBase Party
		{
			get
			{
				if (!this._troop.IsHero || this._troop.HeroObject.PartyBelongedTo == null)
				{
					return null;
				}
				return this._troop.HeroObject.PartyBelongedTo.Party;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x001761BF File Offset: 0x001743BF
		public IBattleCombatant BattleCombatant
		{
			get
			{
				return this.Party;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x001761C7 File Offset: 0x001743C7
		public Banner Banner
		{
			get
			{
				return this._banner;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060049F9 RID: 18937 RVA: 0x001761CF File Offset: 0x001743CF
		// (set) Token: 0x060049FA RID: 18938 RVA: 0x001761D7 File Offset: 0x001743D7
		public int Rank { get; private set; }

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x001761E0 File Offset: 0x001743E0
		public int UniqueSeed
		{
			get
			{
				return this._descriptor.UniqueSeed;
			}
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x001761F0 File Offset: 0x001743F0
		public SimpleAgentOrigin(BasicCharacterObject troop, int rank = -1, Banner banner = null, UniqueTroopDescriptor descriptor = default(UniqueTroopDescriptor))
		{
			this._troop = (CharacterObject)troop;
			this._descriptor = descriptor;
			this.Rank = ((rank == -1) ? MBRandom.RandomInt(10000) : rank);
			this._banner = banner;
			AgentOriginUtilities.GetDefaultTroopTraits(this._troop, out this._hasThrownWeapon, out this._hasSpear, out this._hasShield, out this._hasHeavyArmor);
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x00176258 File Offset: 0x00174458
		public void SetWounded()
		{
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0017625A File Offset: 0x0017445A
		public void SetKilled()
		{
			if (this._troop.IsHero)
			{
				KillCharacterAction.ApplyByBattle(this._troop.HeroObject, null, true);
			}
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x0017627B File Offset: 0x0017447B
		public void SetRouted(bool isOrderRetreat)
		{
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x0017627D File Offset: 0x0017447D
		public void OnAgentRemoved(float agentHealth)
		{
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x00176280 File Offset: 0x00174480
		void IAgentOriginBase.OnScoreHit(BasicCharacterObject victim, BasicCharacterObject formationCaptain, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon)
		{
			if (isTeamKill)
			{
				CharacterObject troop = this._troop;
				ExplainedNumber xpFromHit = Campaign.Current.Models.CombatXpModel.GetXpFromHit(troop, (CharacterObject)formationCaptain, (CharacterObject)victim, this.Party, damage, isFatal, CombatXpModel.MissionTypeEnum.Battle);
				if (troop.IsHero && attackerWeapon != null)
				{
					SkillObject skillForWeapon = Campaign.Current.Models.CombatXpModel.GetSkillForWeapon(attackerWeapon, false);
					troop.HeroObject.AddSkillXp(skillForWeapon, (float)xpFromHit.RoundedResultNumber);
				}
			}
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x001762FC File Offset: 0x001744FC
		public void SetBanner(Banner banner)
		{
			this._banner = banner;
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x00176305 File Offset: 0x00174505
		TroopTraitsMask IAgentOriginBase.GetTraitsMask()
		{
			return AgentOriginUtilities.GetDefaultTraitsMask(this);
		}

		// Token: 0x0400146A RID: 5226
		private CharacterObject _troop;

		// Token: 0x0400146B RID: 5227
		private bool _hasThrownWeapon;

		// Token: 0x0400146C RID: 5228
		private bool _hasHeavyArmor;

		// Token: 0x0400146D RID: 5229
		private bool _hasShield;

		// Token: 0x0400146E RID: 5230
		private bool _hasSpear;

		// Token: 0x0400146F RID: 5231
		private Banner _banner;

		// Token: 0x04001471 RID: 5233
		private UniqueTroopDescriptor _descriptor;
	}
}
