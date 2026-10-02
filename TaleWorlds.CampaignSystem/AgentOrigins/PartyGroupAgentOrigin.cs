using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.TroopSuppliers;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.AgentOrigins
{
	// Token: 0x0200048E RID: 1166
	public class PartyGroupAgentOrigin : IAgentOriginBase
	{
		// Token: 0x060049D3 RID: 18899 RVA: 0x00175D4C File Offset: 0x00173F4C
		internal PartyGroupAgentOrigin(PartyGroupTroopSupplier supplier, UniqueTroopDescriptor descriptor, int rank)
		{
			this._supplier = supplier;
			this._descriptor = descriptor;
			this._rank = rank;
			AgentOriginUtilities.GetDefaultTroopTraits(this.Troop, out this._hasThrownWeapon, out this._hasSpear, out this._hasShield, out this._hasHeavyArmor);
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00175D8C File Offset: 0x00173F8C
		public PartyBase Party
		{
			get
			{
				return this._supplier.GetParty(this._descriptor);
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x00175D9F File Offset: 0x00173F9F
		public IBattleCombatant BattleCombatant
		{
			get
			{
				return this.Party;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x00175DA7 File Offset: 0x00173FA7
		public Banner Banner
		{
			get
			{
				if (this.Party.LeaderHero == null)
				{
					return this.Party.MapFaction.Banner;
				}
				return this.Party.LeaderHero.ClanBanner;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x00175DD8 File Offset: 0x00173FD8
		public int UniqueSeed
		{
			get
			{
				return this._descriptor.UniqueSeed;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x00175DF3 File Offset: 0x00173FF3
		public CharacterObject Troop
		{
			get
			{
				return this._supplier.GetTroop(this._descriptor);
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x00175E06 File Offset: 0x00174006
		bool IAgentOriginBase.HasThrownWeapon
		{
			get
			{
				return this._hasThrownWeapon;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x00175E0E File Offset: 0x0017400E
		bool IAgentOriginBase.HasHeavyArmor
		{
			get
			{
				return this._hasHeavyArmor;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x00175E16 File Offset: 0x00174016
		bool IAgentOriginBase.HasShield
		{
			get
			{
				return this._hasShield;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x00175E1E File Offset: 0x0017401E
		bool IAgentOriginBase.HasSpear
		{
			get
			{
				return this._hasSpear;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x00175E26 File Offset: 0x00174026
		BasicCharacterObject IAgentOriginBase.Troop
		{
			get
			{
				return this.Troop;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x00175E2E File Offset: 0x0017402E
		public UniqueTroopDescriptor TroopDesc
		{
			get
			{
				return this._descriptor;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x00175E36 File Offset: 0x00174036
		public int Rank
		{
			get
			{
				return this._rank;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x00175E3E File Offset: 0x0017403E
		public bool IsUnderPlayersCommand
		{
			get
			{
				return this.Troop == Hero.MainHero.CharacterObject || PartyBase.IsPartyUnderPlayerCommand(this.Party);
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060049E1 RID: 18913 RVA: 0x00175E60 File Offset: 0x00174060
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

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00175ED2 File Offset: 0x001740D2
		public uint FactionColor
		{
			get
			{
				return this.Party.MapFaction.Color;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x00175EE4 File Offset: 0x001740E4
		public uint FactionColor2
		{
			get
			{
				return this.Party.MapFaction.Color2;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x00175EF6 File Offset: 0x001740F6
		public int Seed
		{
			get
			{
				return CharacterHelper.GetPartyMemberFaceSeed(this.Party, this.Troop, this.Rank);
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x00175F0F File Offset: 0x0017410F
		public void SetWounded()
		{
			if (!this._isRemoved)
			{
				this._supplier.OnTroopWounded(this._descriptor);
				this._isRemoved = true;
			}
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x00175F34 File Offset: 0x00174134
		public void SetKilled()
		{
			if (!this._isRemoved)
			{
				this._supplier.OnTroopKilled(this._descriptor);
				if (this.Troop.IsHero)
				{
					KillCharacterAction.ApplyByBattle(this.Troop.HeroObject, null, true);
				}
				this._isRemoved = true;
			}
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x00175F80 File Offset: 0x00174180
		public void SetRouted(bool isOrderRetreat)
		{
			if (!this._isRemoved)
			{
				this._supplier.OnTroopRouted(this._descriptor, isOrderRetreat);
				this._isRemoved = true;
			}
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x00175FA3 File Offset: 0x001741A3
		public void OnAgentRemoved(float agentHealth)
		{
			if (this.Troop.IsHero)
			{
				this.Troop.HeroObject.HitPoints = MathF.Max(1, MathF.Round(agentHealth));
			}
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x00175FCE File Offset: 0x001741CE
		void IAgentOriginBase.OnScoreHit(BasicCharacterObject victim, BasicCharacterObject captain, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon)
		{
			this._supplier.OnTroopScoreHit(this._descriptor, victim, damage, isFatal, isTeamKill, attackerWeapon);
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x00175FE9 File Offset: 0x001741E9
		public void SetBanner(Banner banner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x00175FF0 File Offset: 0x001741F0
		TroopTraitsMask IAgentOriginBase.GetTraitsMask()
		{
			return AgentOriginUtilities.GetDefaultTraitsMask(this);
		}

		// Token: 0x04001462 RID: 5218
		private readonly PartyGroupTroopSupplier _supplier;

		// Token: 0x04001463 RID: 5219
		private readonly UniqueTroopDescriptor _descriptor;

		// Token: 0x04001464 RID: 5220
		private readonly int _rank;

		// Token: 0x04001465 RID: 5221
		private bool _isRemoved;

		// Token: 0x04001466 RID: 5222
		private bool _hasThrownWeapon;

		// Token: 0x04001467 RID: 5223
		private bool _hasHeavyArmor;

		// Token: 0x04001468 RID: 5224
		private bool _hasShield;

		// Token: 0x04001469 RID: 5225
		private bool _hasSpear;
	}
}
