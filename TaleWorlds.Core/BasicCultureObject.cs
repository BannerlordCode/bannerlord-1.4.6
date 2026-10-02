using System;
using System.Xml;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x0200001B RID: 27
	public class BasicCultureObject : MBObjectBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006593 File Offset: 0x00004793
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000659B File Offset: 0x0000479B
		public TextObject Name { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000065A4 File Offset: 0x000047A4
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000065AC File Offset: 0x000047AC
		public bool IsMainCulture { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000065B5 File Offset: 0x000047B5
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000065BD File Offset: 0x000047BD
		public bool IsBandit { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000065C6 File Offset: 0x000047C6
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000065CE File Offset: 0x000047CE
		public bool CanHaveSettlement { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000065D7 File Offset: 0x000047D7
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000065DF File Offset: 0x000047DF
		public uint Color { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000065E8 File Offset: 0x000047E8
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000065F0 File Offset: 0x000047F0
		public uint Color2 { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000065F9 File Offset: 0x000047F9
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00006601 File Offset: 0x00004801
		public uint ClothAlternativeColor { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000660A File Offset: 0x0000480A
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00006612 File Offset: 0x00004812
		public uint ClothAlternativeColor2 { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000661B File Offset: 0x0000481B
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00006623 File Offset: 0x00004823
		public uint BackgroundColor1 { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000662C File Offset: 0x0000482C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00006634 File Offset: 0x00004834
		public uint ForegroundColor1 { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000663D File Offset: 0x0000483D
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006645 File Offset: 0x00004845
		public uint BackgroundColor2 { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000664E File Offset: 0x0000484E
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006656 File Offset: 0x00004856
		public uint ForegroundColor2 { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000665F File Offset: 0x0000485F
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00006667 File Offset: 0x00004867
		public string EncounterBackgroundMesh { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006670 File Offset: 0x00004870
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00006678 File Offset: 0x00004878
		public Banner Banner { get; private set; }

		// Token: 0x06000183 RID: 387 RVA: 0x00006681 File Offset: 0x00004881
		public override string ToString()
		{
			return this.Name.ToString();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006690 File Offset: 0x00004890
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.Name = new TextObject(node.Attributes["name"].Value, null);
			this.Color = ((node.Attributes["color"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["color"].Value, 16));
			this.Color2 = ((node.Attributes["color2"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["color2"].Value, 16));
			this.ClothAlternativeColor = ((node.Attributes["cloth_alternative_color1"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["cloth_alternative_color1"].Value, 16));
			this.ClothAlternativeColor2 = ((node.Attributes["cloth_alternative_color2"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["cloth_alternative_color2"].Value, 16));
			this.BackgroundColor1 = ((node.Attributes["banner_background_color1"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["banner_background_color1"].Value, 16));
			this.ForegroundColor1 = ((node.Attributes["banner_foreground_color1"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["banner_foreground_color1"].Value, 16));
			this.BackgroundColor2 = ((node.Attributes["banner_background_color2"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["banner_background_color2"].Value, 16));
			this.ForegroundColor2 = ((node.Attributes["banner_foreground_color2"] == null) ? uint.MaxValue : Convert.ToUInt32(node.Attributes["banner_foreground_color2"].Value, 16));
			this.IsMainCulture = node.Attributes["is_main_culture"] != null && Convert.ToBoolean(node.Attributes["is_main_culture"].Value);
			this.EncounterBackgroundMesh = ((node.Attributes["encounter_background_mesh"] == null) ? null : node.Attributes["encounter_background_mesh"].Value);
			this.Banner = ((node.Attributes["faction_banner_key"] == null) ? new Banner() : new Banner(node.Attributes["faction_banner_key"].Value));
			this.IsBandit = false;
			this.IsBandit = node.Attributes["is_bandit"] != null && Convert.ToBoolean(node.Attributes["is_bandit"].Value);
			this.CanHaveSettlement = false;
			this.CanHaveSettlement = node.Attributes["can_have_settlement"] != null && Convert.ToBoolean(node.Attributes["can_have_settlement"].Value);
		}
	}
}
