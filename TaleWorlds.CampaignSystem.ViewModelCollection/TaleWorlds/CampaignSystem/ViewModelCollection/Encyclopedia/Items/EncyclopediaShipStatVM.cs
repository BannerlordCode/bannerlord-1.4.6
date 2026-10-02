using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000EA RID: 234
	public class EncyclopediaShipStatVM : ViewModel
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x000556F0 File Offset: 0x000538F0
		public EncyclopediaShipStatVM(string statId, TextObject name, string value, Func<List<TooltipProperty>> getTooltipProperties = null)
		{
			this._nameTextObj = name;
			this.ValueText = value;
			this.StatId = statId;
			if (getTooltipProperties != null)
			{
				this.Tooltip = new BasicTooltipViewModel(getTooltipProperties);
			}
			else
			{
				this.Tooltip = new BasicTooltipViewModel(() => GameTexts.FindText("str_ship_stat_explanation", this.StatId).ToString());
			}
			this.RefreshValues();
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00055748 File Offset: 0x00053948
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject textObject = GameTexts.FindText("str_LEFT_colon", null);
			textObject.SetTextVariable("LEFT", this._nameTextObj.ToString());
			this.Name = textObject.ToString();
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0005578A File Offset: 0x0005398A
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00055792 File Offset: 0x00053992
		[DataSourceProperty]
		public string StatId
		{
			get
			{
				return this._statId;
			}
			set
			{
				if (value != this._statId)
				{
					this._statId = value;
					base.OnPropertyChangedWithValue<string>(value, "StatId");
				}
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000557B5 File Offset: 0x000539B5
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x000557BD File Offset: 0x000539BD
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

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x000557E0 File Offset: 0x000539E0
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x000557E8 File Offset: 0x000539E8
		[DataSourceProperty]
		public string ValueText
		{
			get
			{
				return this._valueText;
			}
			set
			{
				if (value != this._valueText)
				{
					this._valueText = value;
					base.OnPropertyChangedWithValue<string>(value, "ValueText");
				}
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0005580B File Offset: 0x00053A0B
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x00055813 File Offset: 0x00053A13
		[DataSourceProperty]
		public BasicTooltipViewModel Tooltip
		{
			get
			{
				return this._tooltip;
			}
			set
			{
				if (value != this._tooltip)
				{
					this._tooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Tooltip");
				}
			}
		}

		// Token: 0x040009DF RID: 2527
		private readonly TextObject _nameTextObj;

		// Token: 0x040009E0 RID: 2528
		private string _statId;

		// Token: 0x040009E1 RID: 2529
		private string _name;

		// Token: 0x040009E2 RID: 2530
		private string _valueText;

		// Token: 0x040009E3 RID: 2531
		private BasicTooltipViewModel _tooltip;
	}
}
