using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000ED RID: 237
	public class EncyclopediaTroopTreeNodeVM : ViewModel
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x00055A24 File Offset: 0x00053C24
		public EncyclopediaTroopTreeNodeVM(CharacterObject rootCharacter, CharacterObject activeCharacter, bool isAlternativeUpgrade, PerkObject alternativeUpgradePerk = null)
		{
			this.Branch = new MBBindingList<EncyclopediaTroopTreeNodeVM>();
			this.IsActiveUnit = rootCharacter == activeCharacter;
			this.IsAlternativeUpgrade = isAlternativeUpgrade;
			if (alternativeUpgradePerk != null && this.IsAlternativeUpgrade)
			{
				this.AlternativeUpgradeTooltip = new BasicTooltipViewModel(delegate
				{
					TextObject textObject = new TextObject("{=LVJKy6a8}This troop requires {PERK_NAME} ({PERK_SKILL}) perk to upgrade.", null);
					textObject.SetTextVariable("PERK_NAME", alternativeUpgradePerk.Name);
					textObject.SetTextVariable("PERK_SKILL", alternativeUpgradePerk.Skill.Name);
					return textObject.ToString();
				});
			}
			this.Unit = new EncyclopediaUnitVM(rootCharacter, this.IsActiveUnit);
			foreach (CharacterObject characterObject in rootCharacter.UpgradeTargets)
			{
				if (characterObject == rootCharacter)
				{
					Debug.FailedAssert("A character cannot be it's own upgrade target!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Encyclopedia\\Items\\EncyclopediaTroopTreeNodeVM.cs", ".ctor", 36);
				}
				else if (Campaign.Current.EncyclopediaManager.GetPageOf(typeof(CharacterObject)).IsValidEncyclopediaItem(characterObject))
				{
					bool flag = rootCharacter.Culture.IsBandit && !characterObject.Culture.IsBandit;
					PerkObject perkObject;
					Campaign.Current.Models.PartyTroopUpgradeModel.DoesPartyHaveRequiredPerksForUpgrade(PartyBase.MainParty, rootCharacter, characterObject, out perkObject);
					this.Branch.Add(new EncyclopediaTroopTreeNodeVM(characterObject, activeCharacter, flag, perkObject));
				}
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00055B4A File Offset: 0x00053D4A
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Branch.ApplyActionOnAllItems(delegate(EncyclopediaTroopTreeNodeVM x)
			{
				x.RefreshValues();
			});
			this.Unit.RefreshValues();
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00055B87 File Offset: 0x00053D87
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x00055B8F File Offset: 0x00053D8F
		[DataSourceProperty]
		public bool IsActiveUnit
		{
			get
			{
				return this._isActiveUnit;
			}
			set
			{
				if (value != this._isActiveUnit)
				{
					this._isActiveUnit = value;
					base.OnPropertyChangedWithValue(value, "IsActiveUnit");
				}
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00055BAD File Offset: 0x00053DAD
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00055BB5 File Offset: 0x00053DB5
		[DataSourceProperty]
		public bool IsAlternativeUpgrade
		{
			get
			{
				return this._isAlternativeUpgrade;
			}
			set
			{
				if (value != this._isAlternativeUpgrade)
				{
					this._isAlternativeUpgrade = value;
					base.OnPropertyChangedWithValue(value, "IsAlternativeUpgrade");
				}
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00055BD3 File Offset: 0x00053DD3
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x00055BDB File Offset: 0x00053DDB
		[DataSourceProperty]
		public MBBindingList<EncyclopediaTroopTreeNodeVM> Branch
		{
			get
			{
				return this._branch;
			}
			set
			{
				if (value != this._branch)
				{
					this._branch = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaTroopTreeNodeVM>>(value, "Branch");
				}
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00055BF9 File Offset: 0x00053DF9
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00055C01 File Offset: 0x00053E01
		[DataSourceProperty]
		public EncyclopediaUnitVM Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if (value != this._unit)
				{
					this._unit = value;
					base.OnPropertyChangedWithValue<EncyclopediaUnitVM>(value, "Unit");
				}
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00055C1F File Offset: 0x00053E1F
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x00055C27 File Offset: 0x00053E27
		[DataSourceProperty]
		public BasicTooltipViewModel AlternativeUpgradeTooltip
		{
			get
			{
				return this._alternativeUpgradeTooltip;
			}
			set
			{
				if (value != this._alternativeUpgradeTooltip)
				{
					this._alternativeUpgradeTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "AlternativeUpgradeTooltip");
				}
			}
		}

		// Token: 0x040009EC RID: 2540
		private MBBindingList<EncyclopediaTroopTreeNodeVM> _branch;

		// Token: 0x040009ED RID: 2541
		private EncyclopediaUnitVM _unit;

		// Token: 0x040009EE RID: 2542
		private bool _isActiveUnit;

		// Token: 0x040009EF RID: 2543
		private bool _isAlternativeUpgrade;

		// Token: 0x040009F0 RID: 2544
		private BasicTooltipViewModel _alternativeUpgradeTooltip;
	}
}
