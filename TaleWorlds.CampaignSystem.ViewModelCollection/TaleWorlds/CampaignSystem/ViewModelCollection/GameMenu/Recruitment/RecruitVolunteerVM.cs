using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x020000B3 RID: 179
	public class RecruitVolunteerVM : ViewModel
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004655B File Offset: 0x0004475B
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00046563 File Offset: 0x00044763
		public Hero OwnerHero { get; private set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004656C File Offset: 0x0004476C
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00046574 File Offset: 0x00044774
		public List<CharacterObject> VolunteerTroops { get; private set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004657D File Offset: 0x0004477D
		public int GoldCost { get; }

		// Token: 0x0600119E RID: 4510 RVA: 0x00046588 File Offset: 0x00044788
		public RecruitVolunteerVM(Hero owner, List<CharacterObject> troops, Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> onRecruit, Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> onRemoveFromCart)
		{
			this.OwnerHero = owner;
			this.VolunteerTroops = troops;
			this._onRecruit = onRecruit;
			this._onRemoveFromCart = onRemoveFromCart;
			this.Owner = new RecruitVolunteerOwnerVM(owner, (int)owner.GetRelationWithPlayer());
			this.Troops = new MBBindingList<RecruitVolunteerTroopVM>();
			int num = 0;
			foreach (CharacterObject characterObject in troops)
			{
				RecruitVolunteerTroopVM recruitVolunteerTroopVM = new RecruitVolunteerTroopVM(this, characterObject, num, new Action<RecruitVolunteerTroopVM>(this.ExecuteRecruit), new Action<RecruitVolunteerTroopVM>(this.ExecuteRemoveFromCart));
				recruitVolunteerTroopVM.CanBeRecruited = false;
				recruitVolunteerTroopVM.PlayerHasEnoughRelation = false;
				if (HeroHelper.HeroCanRecruitFromHero(Hero.MainHero, this.OwnerHero, num))
				{
					recruitVolunteerTroopVM.PlayerHasEnoughRelation = true;
					if (characterObject != null)
					{
						recruitVolunteerTroopVM.CanBeRecruited = true;
					}
				}
				num++;
				this.Troops.Add(recruitVolunteerTroopVM);
			}
			this.RecruitHint = new HintViewModel();
			this.RefreshProperties();
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00046688 File Offset: 0x00044888
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.RefreshProperties();
			RecruitVolunteerOwnerVM owner = this.Owner;
			if (owner != null)
			{
				owner.RefreshValues();
			}
			this.Troops.ApplyActionOnAllItems(delegate(RecruitVolunteerTroopVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000466DC File Offset: 0x000448DC
		public void ExecuteRecruit(RecruitVolunteerTroopVM troop)
		{
			this._onRecruit(this, troop);
			this.RefreshProperties();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000466F1 File Offset: 0x000448F1
		public void ExecuteRemoveFromCart(RecruitVolunteerTroopVM troop)
		{
			this._onRemoveFromCart(this, troop);
			this.RefreshProperties();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00046708 File Offset: 0x00044908
		private void RefreshProperties()
		{
			this.RecruitText = this.GoldCost.ToString();
			if (this.RecruitableNumber == 0)
			{
				this.QuantityText = GameTexts.FindText("str_none", null).ToString();
				return;
			}
			GameTexts.SetVariable("QUANTITY", this.RecruitableNumber.ToString());
			this.QuantityText = GameTexts.FindText("str_x_quantity", null).ToString();
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00046774 File Offset: 0x00044974
		public void OnRecruitMoveToCart(RecruitVolunteerTroopVM troop)
		{
			MBInformationManager.HideInformations();
			this.Troops.RemoveAt(troop.Index);
			RecruitVolunteerTroopVM recruitVolunteerTroopVM = new RecruitVolunteerTroopVM(this, null, troop.Index, new Action<RecruitVolunteerTroopVM>(this.ExecuteRecruit), new Action<RecruitVolunteerTroopVM>(this.ExecuteRemoveFromCart));
			recruitVolunteerTroopVM.IsTroopEmpty = true;
			recruitVolunteerTroopVM.PlayerHasEnoughRelation = true;
			this.Troops.Insert(troop.Index, recruitVolunteerTroopVM);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000467DD File Offset: 0x000449DD
		public void OnRecruitRemovedFromCart(RecruitVolunteerTroopVM troop)
		{
			this.Troops.RemoveAt(troop.Index);
			this.Troops.Insert(troop.Index, troop);
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00046802 File Offset: 0x00044A02
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x0004680A File Offset: 0x00044A0A
		[DataSourceProperty]
		public MBBindingList<RecruitVolunteerTroopVM> Troops
		{
			get
			{
				return this._troops;
			}
			set
			{
				if (value != this._troops)
				{
					this._troops = value;
					base.OnPropertyChangedWithValue<MBBindingList<RecruitVolunteerTroopVM>>(value, "Troops");
				}
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00046828 File Offset: 0x00044A28
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00046830 File Offset: 0x00044A30
		[DataSourceProperty]
		public RecruitVolunteerOwnerVM Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				if (value != this._owner)
				{
					this._owner = value;
					base.OnPropertyChangedWithValue<RecruitVolunteerOwnerVM>(value, "Owner");
				}
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0004684E File Offset: 0x00044A4E
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00046856 File Offset: 0x00044A56
		[DataSourceProperty]
		public bool CanRecruit
		{
			get
			{
				return this._canRecruit;
			}
			set
			{
				if (value != this._canRecruit)
				{
					this._canRecruit = value;
					base.OnPropertyChangedWithValue(value, "CanRecruit");
				}
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00046874 File Offset: 0x00044A74
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x0004687C File Offset: 0x00044A7C
		[DataSourceProperty]
		public bool ButtonIsVisible
		{
			get
			{
				return this._buttonIsVisible;
			}
			set
			{
				if (value != this._buttonIsVisible)
				{
					this._buttonIsVisible = value;
					base.OnPropertyChangedWithValue(value, "ButtonIsVisible");
				}
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004689A File Offset: 0x00044A9A
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x000468A2 File Offset: 0x00044AA2
		[DataSourceProperty]
		public string QuantityText
		{
			get
			{
				return this._quantityText;
			}
			set
			{
				if (value != this._quantityText)
				{
					this._quantityText = value;
					base.OnPropertyChangedWithValue<string>(value, "QuantityText");
				}
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x000468C5 File Offset: 0x00044AC5
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x000468CD File Offset: 0x00044ACD
		[DataSourceProperty]
		public string RecruitText
		{
			get
			{
				return this._recruitText;
			}
			set
			{
				if (value != this._recruitText)
				{
					this._recruitText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitText");
				}
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x000468F0 File Offset: 0x00044AF0
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x000468F8 File Offset: 0x00044AF8
		[DataSourceProperty]
		public HintViewModel RecruitHint
		{
			get
			{
				return this._recruitHint;
			}
			set
			{
				if (value != this._recruitHint)
				{
					this._recruitHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RecruitHint");
				}
			}
		}

		// Token: 0x0400080F RID: 2063
		public int RecruitableNumber;

		// Token: 0x04000810 RID: 2064
		private readonly Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> _onRecruit;

		// Token: 0x04000811 RID: 2065
		private readonly Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> _onRemoveFromCart;

		// Token: 0x04000812 RID: 2066
		private string _quantityText;

		// Token: 0x04000813 RID: 2067
		private string _recruitText;

		// Token: 0x04000814 RID: 2068
		private bool _canRecruit;

		// Token: 0x04000815 RID: 2069
		private bool _buttonIsVisible;

		// Token: 0x04000816 RID: 2070
		private HintViewModel _recruitHint;

		// Token: 0x04000817 RID: 2071
		private RecruitVolunteerOwnerVM _owner;

		// Token: 0x04000818 RID: 2072
		private MBBindingList<RecruitVolunteerTroopVM> _troops;
	}
}
