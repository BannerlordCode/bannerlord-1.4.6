using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000CD RID: 205
	public class EncyclopediaSettlementPageStatItemVM : ViewModel
	{
		// Token: 0x06001380 RID: 4992 RVA: 0x0004EA6D File Offset: 0x0004CC6D
		public EncyclopediaSettlementPageStatItemVM(BasicTooltipViewModel basicTooltipViewModel, EncyclopediaSettlementPageStatItemVM.DescriptionType type, string statText)
		{
			this._basicTooltipViewModel = basicTooltipViewModel;
			this._typeString = type.ToString();
			this._statText = statText;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0004EA96 File Offset: 0x0004CC96
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x0004EA9E File Offset: 0x0004CC9E
		[DataSourceProperty]
		public BasicTooltipViewModel BasicTooltipViewModel
		{
			get
			{
				return this._basicTooltipViewModel;
			}
			set
			{
				if (value != this._basicTooltipViewModel)
				{
					this._basicTooltipViewModel = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "BasicTooltipViewModel");
				}
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0004EABC File Offset: 0x0004CCBC
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x0004EAC4 File Offset: 0x0004CCC4
		[DataSourceProperty]
		public string TypeString
		{
			get
			{
				return this._typeString;
			}
			set
			{
				if (value != this._typeString)
				{
					this._typeString = value;
					base.OnPropertyChangedWithValue<string>(value, "TypeString");
				}
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0004EAE7 File Offset: 0x0004CCE7
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x0004EAEF File Offset: 0x0004CCEF
		[DataSourceProperty]
		public string StatText
		{
			get
			{
				return this._statText;
			}
			set
			{
				if (value != this._statText)
				{
					this._statText = value;
					base.OnPropertyChangedWithValue<string>(value, "StatText");
				}
			}
		}

		// Token: 0x040008ED RID: 2285
		private BasicTooltipViewModel _basicTooltipViewModel;

		// Token: 0x040008EE RID: 2286
		private string _typeString;

		// Token: 0x040008EF RID: 2287
		private string _statText;

		// Token: 0x02000241 RID: 577
		public enum DescriptionType
		{
			// Token: 0x0400124E RID: 4686
			Wall,
			// Token: 0x0400124F RID: 4687
			Shipyard,
			// Token: 0x04001250 RID: 4688
			Garrison,
			// Token: 0x04001251 RID: 4689
			Militia,
			// Token: 0x04001252 RID: 4690
			Food,
			// Token: 0x04001253 RID: 4691
			Prosperity,
			// Token: 0x04001254 RID: 4692
			Loyalty,
			// Token: 0x04001255 RID: 4693
			Security
		}
	}
}
