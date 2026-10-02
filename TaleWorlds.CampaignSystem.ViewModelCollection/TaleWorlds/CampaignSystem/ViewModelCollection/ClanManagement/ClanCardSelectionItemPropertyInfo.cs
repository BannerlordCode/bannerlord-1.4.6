using System;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200011B RID: 283
	public readonly struct ClanCardSelectionItemPropertyInfo
	{
		// Token: 0x06001A2F RID: 6703 RVA: 0x00063190 File Offset: 0x00061390
		public ClanCardSelectionItemPropertyInfo(TextObject title, TextObject value)
		{
			this.Title = title;
			this.Value = value;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000631B0 File Offset: 0x000613B0
		public ClanCardSelectionItemPropertyInfo(TextObject value)
		{
			this.Title = null;
			this.Value = value;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000631CD File Offset: 0x000613CD
		public static TextObject CreateLabeledValueText(TextObject label, TextObject value)
		{
			TextObject textObject = new TextObject("{=!}<span style=\"Label\">{LABEL}</span>: {VALUE}", null);
			textObject.SetTextVariable("LABEL", label);
			textObject.SetTextVariable("VALUE", value);
			return textObject;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000631F4 File Offset: 0x000613F4
		public static TextObject CreateActionGoldChangeText(int goldChange)
		{
			if (goldChange != 0)
			{
				bool flag = goldChange > 0;
				string text = (flag ? "PositiveChange" : "NegativeChange");
				TextObject textObject = (flag ? new TextObject("{=8N1EdPB3}You will earn {GOLD}{GOLD_ICON}", null) : new TextObject("{=kjaACKUq}This action will cost {GOLD}{GOLD_ICON}", null));
				textObject.SetTextVariable("GOLD", string.Format("<span style=\"{0}\">{1}</span>", text, Math.Abs(goldChange)));
				textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
				return textObject;
			}
			return TextObject.GetEmpty();
		}

		// Token: 0x04000C0A RID: 3082
		public readonly TextObject Title;

		// Token: 0x04000C0B RID: 3083
		public readonly TextObject Value;
	}
}
