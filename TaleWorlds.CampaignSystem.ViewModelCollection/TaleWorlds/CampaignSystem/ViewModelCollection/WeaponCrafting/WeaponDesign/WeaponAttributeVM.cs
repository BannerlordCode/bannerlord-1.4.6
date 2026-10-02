using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000104 RID: 260
	public class WeaponAttributeVM : ViewModel
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0005A91C File Offset: 0x00058B1C
		public DamageTypes DamageType { get; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0005A924 File Offset: 0x00058B24
		public CraftingTemplate.CraftingStatTypes AttributeType { get; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0005A92C File Offset: 0x00058B2C
		public float AttributeValue { get; }

		// Token: 0x06001790 RID: 6032 RVA: 0x0005A934 File Offset: 0x00058B34
		public WeaponAttributeVM(CraftingTemplate.CraftingStatTypes type, DamageTypes damageType, string attributeName, float attributeValue)
		{
			this.AttributeType = type;
			this.DamageType = damageType;
			this.AttributeValue = attributeValue;
			string text = ((this.AttributeValue > 100f) ? attributeValue.ToString("F0") : attributeValue.ToString("F1"));
			string text2 = "<span style=\"Value\">" + text + "</span>";
			TextObject textObject = new TextObject("{=!}{ATTR_NAME}{ATTR_VALUE_RTT}", null);
			textObject.SetTextVariable("ATTR_NAME", attributeName);
			textObject.SetTextVariable("ATTR_VALUE_RTT", text2);
			this.AttributeFieldText = textObject.ToString();
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0005A9C8 File Offset: 0x00058BC8
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x0005A9D0 File Offset: 0x00058BD0
		[DataSourceProperty]
		public string AttributeFieldText
		{
			get
			{
				return this._attributeFieldText;
			}
			set
			{
				if (value != this._attributeFieldText)
				{
					this._attributeFieldText = value;
					base.OnPropertyChangedWithValue<string>(value, "AttributeFieldText");
				}
			}
		}

		// Token: 0x04000ACA RID: 2762
		private string _attributeFieldText;
	}
}
