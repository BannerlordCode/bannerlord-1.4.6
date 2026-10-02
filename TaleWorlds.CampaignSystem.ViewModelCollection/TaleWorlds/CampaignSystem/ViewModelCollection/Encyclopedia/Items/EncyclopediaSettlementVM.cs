using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000E8 RID: 232
	public class EncyclopediaSettlementVM : ViewModel
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x00055514 File Offset: 0x00053714
		public EncyclopediaSettlementVM(Settlement settlement)
		{
			if (!settlement.IsHideout)
			{
				this._settlement = settlement;
			}
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.FileName = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.RefreshValues();
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00055563 File Offset: 0x00053763
		public override void RefreshValues()
		{
			base.RefreshValues();
			Settlement settlement = this._settlement;
			this.NameText = ((settlement != null) ? settlement.Name.ToString() : null) ?? "";
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00055591 File Offset: 0x00053791
		public void ExecuteLink()
		{
			if (this._settlement != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._settlement.EncyclopediaLink);
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000555B5 File Offset: 0x000537B5
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000555BC File Offset: 0x000537BC
		public void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[] { this._settlement });
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000555DC File Offset: 0x000537DC
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x000555E4 File Offset: 0x000537E4
		[DataSourceProperty]
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				if (value != this._fileName)
				{
					this._fileName = value;
					base.OnPropertyChangedWithValue<string>(value, "FileName");
				}
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x00055607 File Offset: 0x00053807
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0005560F File Offset: 0x0005380F
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

		// Token: 0x040009D9 RID: 2521
		private Settlement _settlement;

		// Token: 0x040009DA RID: 2522
		private string _fileName;

		// Token: 0x040009DB RID: 2523
		private string _nameText;
	}
}
