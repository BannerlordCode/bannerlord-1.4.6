using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200012C RID: 300
	public class ClanRoleItemVM : ViewModel
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00067F45 File Offset: 0x00066145
		// (set) Token: 0x06001C1F RID: 7199 RVA: 0x00067F4D File Offset: 0x0006614D
		public PartyRole Role { get; private set; }

		// Token: 0x06001C20 RID: 7200 RVA: 0x00067F58 File Offset: 0x00066158
		public ClanRoleItemVM(MobileParty party, PartyRole role, MBBindingList<ClanPartyMemberItemVM> heroMembers, Action<ClanRoleItemVM> onRoleSelectionToggled, Action onRoleAssigned)
		{
			this.Role = role;
			this._comparer = new ClanRoleItemVM.ClanRoleMemberComparer();
			this._party = party;
			this._onRoleSelectionToggled = onRoleSelectionToggled;
			this._onRoleAssigned = onRoleAssigned;
			this._heroMembers = heroMembers;
			this.Members = new MBBindingList<ClanRoleMemberItemVM>();
			this.NotAssignedHint = new HintViewModel(new TextObject("{=S1iS3OYj}Party leader is default for unassigned roles", null), null);
			this.DisabledHint = new HintViewModel();
			this.IsEnabled = true;
			this.RoleId = ClanRoleItemVM.GetRoleIdentifier(role);
			this.Refresh();
			this.RefreshValues();
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00067FE8 File Offset: 0x000661E8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = GameTexts.FindText("role", this.Role.ToString()).ToString();
			this.NoEffectText = GameTexts.FindText("str_clan_role_no_effect", null).ToString();
			ClanRoleMemberItemVM effectiveOwner = this.EffectiveOwner;
			this.AssignedMemberEffects = ((effectiveOwner != null) ? effectiveOwner.GetEffectsList(this.Role) : null) ?? "";
			this.HasEffects = !string.IsNullOrEmpty(this.AssignedMemberEffects);
			this.Members.ApplyActionOnAllItems(delegate(ClanRoleMemberItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0006809F File Offset: 0x0006629F
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Members.ApplyActionOnAllItems(delegate(ClanRoleMemberItemVM x)
			{
				x.OnFinalize();
			});
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000680D4 File Offset: 0x000662D4
		private static string GetRoleIdentifier(PartyRole role)
		{
			switch (role)
			{
			case PartyRole.Ruler:
				return "rule";
			case PartyRole.ClanLeader:
				return "clan_leader";
			case PartyRole.Governor:
				return "governor";
			case PartyRole.ArmyCommander:
				return "commander";
			case PartyRole.PartyLeader:
				return "party_leader";
			case PartyRole.PartyOwner:
				return "party_owner";
			case PartyRole.Surgeon:
				return "surgeon";
			case PartyRole.Engineer:
				return "engineer";
			case PartyRole.Scout:
				return "scout";
			case PartyRole.Quartermaster:
				return "quartermaser";
			case PartyRole.PartyMember:
				return "member";
			case PartyRole.Personal:
				return "personal";
			case PartyRole.Captain:
				return "captain";
			case PartyRole.FirstMate:
				return "first_mate";
			case PartyRole.Navigator:
				return "navigator";
			}
			return string.Empty;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00068188 File Offset: 0x00066388
		public void Refresh()
		{
			this.Members.ApplyActionOnAllItems(delegate(ClanRoleMemberItemVM x)
			{
				x.OnFinalize();
			});
			this.Members.Clear();
			foreach (ClanPartyMemberItemVM clanPartyMemberItemVM in this._heroMembers)
			{
				if (clanPartyMemberItemVM.IsLeader)
				{
					this.ClanLeader = new ClanRoleMemberItemVM(this._party, this.Role, clanPartyMemberItemVM, null);
				}
				else if (Campaign.Current.Models.ClanMemberPartyRoleModel.IsHeroAssignableForPartyRole(clanPartyMemberItemVM.HeroObject, this.Role, this._party))
				{
					this.Members.Add(new ClanRoleMemberItemVM(this._party, this.Role, clanPartyMemberItemVM, new Action(this.OnRoleAssigned)));
				}
			}
			this.Members.Add(new ClanRoleMemberItemVM(this._party, this.Role, null, new Action(this.OnRoleAssigned)));
			this.Members.Sort(this._comparer);
			Hero hero;
			Hero hero2;
			this.GetMemberAssignedToRole(this._party, this.Role, out hero, out hero2);
			Hero hero3 = hero2;
			ClanRoleMemberItemVM clanLeader = this.ClanLeader;
			if (hero3 == ((clanLeader != null) ? clanLeader.Member.HeroObject : null))
			{
				this.EffectiveOwner = this.ClanLeader;
			}
			else
			{
				for (int i = 0; i < this.Members.Count; i++)
				{
					ClanRoleMemberItemVM clanRoleMemberItemVM = this.Members[i];
					if (clanRoleMemberItemVM.Member.HeroObject == hero2)
					{
						this.EffectiveOwner = clanRoleMemberItemVM;
						break;
					}
				}
			}
			this.IsNotAssigned = hero == null;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00068340 File Offset: 0x00066540
		public void ExecuteToggleRoleSelection()
		{
			Action<ClanRoleItemVM> onRoleSelectionToggled = this._onRoleSelectionToggled;
			if (onRoleSelectionToggled == null)
			{
				return;
			}
			onRoleSelectionToggled(this);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00068354 File Offset: 0x00066554
		private void GetMemberAssignedToRole(MobileParty party, PartyRole role, out Hero roleOwner, out Hero effectiveRoleOwner)
		{
			roleOwner = party.GetRoleHolder(role);
			switch (role)
			{
			case PartyRole.Surgeon:
				effectiveRoleOwner = party.EffectiveSurgeon;
				return;
			case PartyRole.Engineer:
				effectiveRoleOwner = party.EffectiveEngineer;
				return;
			case PartyRole.Scout:
				effectiveRoleOwner = party.EffectiveScout;
				return;
			case PartyRole.Quartermaster:
				effectiveRoleOwner = party.EffectiveQuartermaster;
				return;
			case PartyRole.FirstMate:
				effectiveRoleOwner = party.EffectiveFirstMate;
				return;
			case PartyRole.Navigator:
				effectiveRoleOwner = party.EffectiveNavigator;
				return;
			}
			effectiveRoleOwner = party.LeaderHero;
			roleOwner = party.LeaderHero;
			Debug.FailedAssert("Given party role is not valid.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanRoleItemVM.cs", "GetMemberAssignedToRole", 175);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000683FE File Offset: 0x000665FE
		private void OnRoleAssigned()
		{
			MBInformationManager.HideInformations();
			Action onRoleAssigned = this._onRoleAssigned;
			if (onRoleAssigned == null)
			{
				return;
			}
			onRoleAssigned();
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00068415 File Offset: 0x00066615
		public void SetEnabled(bool enabled, TextObject disabledHint)
		{
			this.IsEnabled = enabled;
			this.DisabledHint.HintText = disabledHint;
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0006842A File Offset: 0x0006662A
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x00068432 File Offset: 0x00066632
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00068450 File Offset: 0x00066650
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00068458 File Offset: 0x00066658
		[DataSourceProperty]
		public ClanRoleMemberItemVM ClanLeader
		{
			get
			{
				return this._clanLeader;
			}
			set
			{
				if (value != this._clanLeader)
				{
					this._clanLeader = value;
					base.OnPropertyChangedWithValue<ClanRoleMemberItemVM>(value, "ClanLeader");
				}
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00068476 File Offset: 0x00066676
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0006847E File Offset: 0x0006667E
		[DataSourceProperty]
		public MBBindingList<ClanRoleMemberItemVM> Members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != this._members)
				{
					this._members = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanRoleMemberItemVM>>(value, "Members");
				}
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0006849C File Offset: 0x0006669C
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x000684A4 File Offset: 0x000666A4
		[DataSourceProperty]
		public ClanRoleMemberItemVM EffectiveOwner
		{
			get
			{
				return this._effectiveOwner;
			}
			set
			{
				if (value != this._effectiveOwner)
				{
					this._effectiveOwner = value;
					base.OnPropertyChangedWithValue<ClanRoleMemberItemVM>(value, "EffectiveOwner");
				}
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x000684C2 File Offset: 0x000666C2
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x000684CA File Offset: 0x000666CA
		[DataSourceProperty]
		public HintViewModel NotAssignedHint
		{
			get
			{
				return this._notAssignedHint;
			}
			set
			{
				if (value != this._notAssignedHint)
				{
					this._notAssignedHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "NotAssignedHint");
				}
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x000684E8 File Offset: 0x000666E8
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x000684F0 File Offset: 0x000666F0
		[DataSourceProperty]
		public HintViewModel DisabledHint
		{
			get
			{
				return this._disabledHint;
			}
			set
			{
				if (value != this._disabledHint)
				{
					this._disabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledHint");
				}
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0006850E File Offset: 0x0006670E
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x00068516 File Offset: 0x00066716
		[DataSourceProperty]
		public bool IsNotAssigned
		{
			get
			{
				return this._isNotAssigned;
			}
			set
			{
				if (value != this._isNotAssigned)
				{
					this._isNotAssigned = value;
					base.OnPropertyChangedWithValue(value, "IsNotAssigned");
				}
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00068534 File Offset: 0x00066734
		// (set) Token: 0x06001C38 RID: 7224 RVA: 0x0006853C File Offset: 0x0006673C
		[DataSourceProperty]
		public bool HasEffects
		{
			get
			{
				return this._hasEffects;
			}
			set
			{
				if (value != this._hasEffects)
				{
					this._hasEffects = value;
					base.OnPropertyChangedWithValue(value, "HasEffects");
				}
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0006855A File Offset: 0x0006675A
		// (set) Token: 0x06001C3A RID: 7226 RVA: 0x00068562 File Offset: 0x00066762
		[DataSourceProperty]
		public string RoleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				if (value != this._roleId)
				{
					this._roleId = value;
					base.OnPropertyChangedWithValue<string>(value, "RoleId");
				}
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00068585 File Offset: 0x00066785
		// (set) Token: 0x06001C3C RID: 7228 RVA: 0x0006858D File Offset: 0x0006678D
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x000685B0 File Offset: 0x000667B0
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x000685B8 File Offset: 0x000667B8
		[DataSourceProperty]
		public string AssignedMemberEffects
		{
			get
			{
				return this._assignedMemberEffects;
			}
			set
			{
				if (value != this._assignedMemberEffects)
				{
					this._assignedMemberEffects = value;
					base.OnPropertyChangedWithValue<string>(value, "AssignedMemberEffects");
				}
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x000685DB File Offset: 0x000667DB
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x000685E3 File Offset: 0x000667E3
		[DataSourceProperty]
		public string NoEffectText
		{
			get
			{
				return this._noEffectText;
			}
			set
			{
				if (value != this._noEffectText)
				{
					this._noEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoEffectText");
				}
			}
		}

		// Token: 0x04000D1D RID: 3357
		private Action<ClanRoleItemVM> _onRoleSelectionToggled;

		// Token: 0x04000D1E RID: 3358
		private Action _onRoleAssigned;

		// Token: 0x04000D1F RID: 3359
		private MBBindingList<ClanPartyMemberItemVM> _heroMembers;

		// Token: 0x04000D20 RID: 3360
		private MobileParty _party;

		// Token: 0x04000D21 RID: 3361
		private ClanRoleItemVM.ClanRoleMemberComparer _comparer;

		// Token: 0x04000D22 RID: 3362
		private bool _isEnabled;

		// Token: 0x04000D23 RID: 3363
		private ClanRoleMemberItemVM _clanLeader;

		// Token: 0x04000D24 RID: 3364
		private MBBindingList<ClanRoleMemberItemVM> _members;

		// Token: 0x04000D25 RID: 3365
		private ClanRoleMemberItemVM _effectiveOwner;

		// Token: 0x04000D26 RID: 3366
		private HintViewModel _notAssignedHint;

		// Token: 0x04000D27 RID: 3367
		private HintViewModel _disabledHint;

		// Token: 0x04000D28 RID: 3368
		private bool _isNotAssigned;

		// Token: 0x04000D29 RID: 3369
		private bool _hasEffects;

		// Token: 0x04000D2A RID: 3370
		private string _roleId;

		// Token: 0x04000D2B RID: 3371
		private string _name;

		// Token: 0x04000D2C RID: 3372
		private string _assignedMemberEffects;

		// Token: 0x04000D2D RID: 3373
		private string _noEffectText;

		// Token: 0x0200028C RID: 652
		private class ClanRoleMemberComparer : IComparer<ClanRoleMemberItemVM>
		{
			// Token: 0x060025E2 RID: 9698 RVA: 0x00082200 File Offset: 0x00080400
			public int Compare(ClanRoleMemberItemVM x, ClanRoleMemberItemVM y)
			{
				int num = y.RelevantSkillValue.CompareTo(x.RelevantSkillValue);
				if (num == 0)
				{
					return x.Member.HeroObject.Name.ToString().CompareTo(y.Member.HeroObject.Name.ToString());
				}
				return num;
			}
		}
	}
}
