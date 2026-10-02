using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000E5 RID: 229
	public class EncyclopediaFactionVM : ViewModel
	{
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0005523A File Offset: 0x0005343A
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x00055242 File Offset: 0x00053442
		public IFaction Faction { get; private set; }

		// Token: 0x0600158E RID: 5518 RVA: 0x0005524C File Offset: 0x0005344C
		public EncyclopediaFactionVM(IFaction faction)
		{
			this.Faction = faction;
			if (faction != null)
			{
				this.ImageIdentifier = new BannerImageIdentifierVM(faction.Banner, true);
				this.IsDestroyed = faction.IsEliminated;
			}
			else
			{
				this.ImageIdentifier = new BannerImageIdentifierVM(null, false);
				this.IsDestroyed = false;
			}
			this.RefreshValues();
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000552A3 File Offset: 0x000534A3
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Faction != null)
			{
				this.NameText = this.Faction.Name.ToString();
				return;
			}
			this.NameText = new TextObject("{=2abtb4xu}Independent", null).ToString();
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000552E0 File Offset: 0x000534E0
		public void ExecuteLink()
		{
			if (this.Faction != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Faction.EncyclopediaLink);
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00055304 File Offset: 0x00053504
		public void ExecuteBeginHint()
		{
			if (this.Faction is Clan)
			{
				InformationManager.ShowTooltip(typeof(Clan), new object[] { this.Faction });
				return;
			}
			if (this.Faction is Kingdom)
			{
				InformationManager.ShowTooltip(typeof(Kingdom), new object[] { this.Faction });
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00055368 File Offset: 0x00053568
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x0005536F File Offset: 0x0005356F
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x00055377 File Offset: 0x00053577
		[DataSourceProperty]
		public BannerImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChanged("Banner");
				}
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00055394 File Offset: 0x00053594
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x0005539C File Offset: 0x0005359C
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000553BF File Offset: 0x000535BF
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x000553C7 File Offset: 0x000535C7
		[DataSourceProperty]
		public bool IsDestroyed
		{
			get
			{
				return this._isDestroyed;
			}
			set
			{
				if (value != this._isDestroyed)
				{
					this._isDestroyed = value;
					base.OnPropertyChangedWithValue(value, "IsDestroyed");
				}
			}
		}

		// Token: 0x040009D1 RID: 2513
		private BannerImageIdentifierVM _imageIdentifier;

		// Token: 0x040009D2 RID: 2514
		private string _nameText;

		// Token: 0x040009D3 RID: 2515
		private bool _isDestroyed;
	}
}
