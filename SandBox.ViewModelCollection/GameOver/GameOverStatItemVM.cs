using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x02000059 RID: 89
	public class GameOverStatItemVM : ViewModel
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x00014D43 File Offset: 0x00012F43
		public GameOverStatItemVM(StatItem item)
		{
			this._item = item;
			this.RefreshValues();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00014D58 File Offset: 0x00012F58
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DefinitionText = GameTexts.FindText("str_game_over_stat_item", this._item.ID).ToString();
			this.ValueText = this._item.Value;
			this.StatTypeAsString = Enum.GetName(typeof(StatItem.StatType), this._item.Type);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00014DC1 File Offset: 0x00012FC1
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00014DC9 File Offset: 0x00012FC9
		[DataSourceProperty]
		public string DefinitionText
		{
			get
			{
				return this._definitionText;
			}
			set
			{
				if (value != this._definitionText)
				{
					this._definitionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DefinitionText");
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00014DEC File Offset: 0x00012FEC
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00014DF4 File Offset: 0x00012FF4
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

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00014E17 File Offset: 0x00013017
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00014E1F File Offset: 0x0001301F
		[DataSourceProperty]
		public string StatTypeAsString
		{
			get
			{
				return this._statTypeAsString;
			}
			set
			{
				if (value != this._statTypeAsString)
				{
					this._statTypeAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "StatTypeAsString");
				}
			}
		}

		// Token: 0x040002BC RID: 700
		private readonly StatItem _item;

		// Token: 0x040002BD RID: 701
		private string _definitionText;

		// Token: 0x040002BE RID: 702
		private string _valueText;

		// Token: 0x040002BF RID: 703
		private string _statTypeAsString;
	}
}
