using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x02000148 RID: 328
	public class PerkSelectionVM : ViewModel
	{
		// Token: 0x06001F7B RID: 8059 RVA: 0x00073A11 File Offset: 0x00071C11
		public PerkSelectionVM(HeroDeveloper developer, Action<SkillObject> refreshPerksOf, Action onPerkSelection)
		{
			this._developer = developer;
			this._refreshPerksOf = refreshPerksOf;
			this._onPerkSelection = onPerkSelection;
			this._selectedPerks = new List<PerkObject>();
			this.AvailablePerks = new MBBindingList<PerkSelectionItemVM>();
			this.IsActive = false;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00073A4C File Offset: 0x00071C4C
		public void SetCurrentSelectionPerk(PerkVM perk)
		{
			if (this.AvailablePerks.Count > 0 || this.IsActive)
			{
				this.ExecuteDeactivate();
			}
			this.AvailablePerks.Clear();
			this._currentInitialPerk = perk;
			this.AvailablePerks.Add(new PerkSelectionItemVM(perk.Perk, new Action<PerkSelectionItemVM>(this.OnSelectPerk)));
			if (perk.AlternativeType == 2)
			{
				this.AvailablePerks.Insert(0, new PerkSelectionItemVM(perk.Perk.AlternativePerk, new Action<PerkSelectionItemVM>(this.OnSelectPerk)));
			}
			else if (perk.AlternativeType == 1)
			{
				this.AvailablePerks.Add(new PerkSelectionItemVM(perk.Perk.AlternativePerk, new Action<PerkSelectionItemVM>(this.OnSelectPerk)));
			}
			this.IsActive = true;
			this.OnSelectPerk(perk);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00073B1C File Offset: 0x00071D1C
		private void OnSelectPerk(PerkVM selectedPerk)
		{
			this._selectedPerks.Add(selectedPerk.Perk);
			this._refreshPerksOf(selectedPerk.Perk.Skill);
			this.IsActive = false;
			Game.Current.EventManager.TriggerEvent<PerkSelectedByPlayerEvent>(new PerkSelectedByPlayerEvent(selectedPerk.Perk));
			Action onPerkSelection = this._onPerkSelection;
			if (onPerkSelection == null)
			{
				return;
			}
			onPerkSelection();
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00073B84 File Offset: 0x00071D84
		private void OnSelectPerk(PerkSelectionItemVM selectedPerk)
		{
			this._selectedPerks.Add(selectedPerk.Perk);
			this._refreshPerksOf(selectedPerk.Perk.Skill);
			this.IsActive = false;
			Game.Current.EventManager.TriggerEvent<PerkSelectedByPlayerEvent>(new PerkSelectedByPlayerEvent(selectedPerk.Perk));
			Action onPerkSelection = this._onPerkSelection;
			if (onPerkSelection == null)
			{
				return;
			}
			onPerkSelection();
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00073BEC File Offset: 0x00071DEC
		public void ResetSelectedPerks()
		{
			foreach (PerkObject perkObject in this._selectedPerks)
			{
				this._refreshPerksOf(perkObject.Skill);
			}
			this._selectedPerks.Clear();
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00073C54 File Offset: 0x00071E54
		public void ApplySelectedPerks()
		{
			foreach (PerkObject perkObject in this._selectedPerks.ToList<PerkObject>())
			{
				this._developer.AddPerk(perkObject);
				this._selectedPerks.Remove(perkObject);
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00073CC0 File Offset: 0x00071EC0
		public bool IsPerkSelected(PerkObject perk)
		{
			return this._selectedPerks.Contains(perk);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00073CCE File Offset: 0x00071ECE
		public bool IsAnyPerkSelected()
		{
			return this._selectedPerks.Count > 0;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00073CDE File Offset: 0x00071EDE
		public void ExecuteDeactivate()
		{
			this.IsActive = false;
			this._refreshPerksOf(this._currentInitialPerk.Perk.Skill);
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x00073D02 File Offset: 0x00071F02
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x00073D0A File Offset: 0x00071F0A
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
					Game.Current.EventManager.TriggerEvent<PerkSelectionToggleEvent>(new PerkSelectionToggleEvent(this.IsActive));
				}
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x00073D42 File Offset: 0x00071F42
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x00073D4A File Offset: 0x00071F4A
		[DataSourceProperty]
		public MBBindingList<PerkSelectionItemVM> AvailablePerks
		{
			get
			{
				return this._availablePerks;
			}
			set
			{
				if (value != this._availablePerks)
				{
					this._availablePerks = value;
					base.OnPropertyChangedWithValue<MBBindingList<PerkSelectionItemVM>>(value, "AvailablePerks");
				}
			}
		}

		// Token: 0x04000EB3 RID: 3763
		private readonly HeroDeveloper _developer;

		// Token: 0x04000EB4 RID: 3764
		private readonly List<PerkObject> _selectedPerks;

		// Token: 0x04000EB5 RID: 3765
		private readonly Action<SkillObject> _refreshPerksOf;

		// Token: 0x04000EB6 RID: 3766
		private readonly Action _onPerkSelection;

		// Token: 0x04000EB7 RID: 3767
		private PerkVM _currentInitialPerk;

		// Token: 0x04000EB8 RID: 3768
		private bool _isActive;

		// Token: 0x04000EB9 RID: 3769
		private MBBindingList<PerkSelectionItemVM> _availablePerks;
	}
}
