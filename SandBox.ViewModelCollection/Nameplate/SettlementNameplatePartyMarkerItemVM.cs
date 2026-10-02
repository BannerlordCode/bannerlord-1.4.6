using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x0200001E RID: 30
	public class SettlementNameplatePartyMarkerItemVM : ViewModel
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000C3BD File Offset: 0x0000A5BD
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		public MobileParty Party { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public int SortIndex { get; private set; }

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public SettlementNameplatePartyMarkerItemVM(MobileParty mobileParty)
		{
			this.Party = mobileParty;
			this.IsBandit = mobileParty.IsBandit;
			if (mobileParty.IsCaravan)
			{
				this.IsCaravan = true;
				this.SortIndex = 1;
				return;
			}
			if (mobileParty.IsLordParty && mobileParty.LeaderHero != null)
			{
				this.IsLord = true;
				Clan actualClan = mobileParty.ActualClan;
				this.Visual = new BannerImageIdentifierVM((actualClan != null) ? actualClan.Banner : null, true);
				this.SortIndex = 0;
				return;
			}
			this.IsDefault = true;
			this.SortIndex = 2;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000C468 File Offset: 0x0000A668
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000C470 File Offset: 0x0000A670
		public BannerImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<BannerImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000C48E File Offset: 0x0000A68E
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000C496 File Offset: 0x0000A696
		public bool IsCaravan
		{
			get
			{
				return this._isCaravan;
			}
			set
			{
				if (value != this._isCaravan)
				{
					this._isCaravan = value;
					base.OnPropertyChangedWithValue(value, "IsCaravan");
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public bool IsLord
		{
			get
			{
				return this._isLord;
			}
			set
			{
				if (value != this._isLord)
				{
					this._isLord = value;
					base.OnPropertyChangedWithValue(value, "IsLord");
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000C4E2 File Offset: 0x0000A6E2
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
			set
			{
				if (value != this._isDefault)
				{
					this._isDefault = value;
					base.OnPropertyChangedWithValue(value, "IsDefault");
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000C500 File Offset: 0x0000A700
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000C508 File Offset: 0x0000A708
		public bool IsBandit
		{
			get
			{
				return this._isBandit;
			}
			set
			{
				if (value != this._isBandit)
				{
					this._isBandit = value;
					base.OnPropertyChangedWithValue(value, "IsBandit");
				}
			}
		}

		// Token: 0x04000161 RID: 353
		private BannerImageIdentifierVM _visual;

		// Token: 0x04000162 RID: 354
		private bool _isCaravan;

		// Token: 0x04000163 RID: 355
		private bool _isLord;

		// Token: 0x04000164 RID: 356
		private bool _isDefault;

		// Token: 0x04000165 RID: 357
		private bool _isBandit;
	}
}
