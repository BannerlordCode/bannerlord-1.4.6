using System;
using System.Globalization;
using System.Xml;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000160 RID: 352
	public class Badge
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0000F089 File Offset: 0x0000D289
		public int Index { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0000F091 File Offset: 0x0000D291
		public BadgeType Type { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0000F099 File Offset: 0x0000D299
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x0000F0A1 File Offset: 0x0000D2A1
		public string StringId { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0000F0AA File Offset: 0x0000D2AA
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x0000F0B2 File Offset: 0x0000D2B2
		public string GroupId { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0000F0BB File Offset: 0x0000D2BB
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x0000F0C3 File Offset: 0x0000D2C3
		public TextObject Name { get; private set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public TextObject Description { get; private set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0000F0DD File Offset: 0x0000D2DD
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0000F0E5 File Offset: 0x0000D2E5
		public bool IsVisibleOnlyWhenEarned { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0000F0EE File Offset: 0x0000D2EE
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0000F0F6 File Offset: 0x0000D2F6
		public DateTime PeriodStart { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0000F0FF File Offset: 0x0000D2FF
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0000F107 File Offset: 0x0000D307
		public DateTime PeriodEnd { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0000F110 File Offset: 0x0000D310
		public bool IsActive
		{
			get
			{
				return DateTime.UtcNow >= this.PeriodStart && DateTime.UtcNow <= this.PeriodEnd;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0000F136 File Offset: 0x0000D336
		public bool IsTimed
		{
			get
			{
				return this.PeriodStart > DateTime.MinValue || this.PeriodEnd < DateTime.MaxValue;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000F15C File Offset: 0x0000D35C
		public Badge(int index, BadgeType badgeType)
		{
			this.Index = index;
			this.Type = badgeType;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000F174 File Offset: 0x0000D374
		public virtual void Deserialize(XmlNode node)
		{
			this.StringId = node.Attributes["id"].Value;
			XmlAttributeCollection attributes = node.Attributes;
			string text;
			if (attributes == null)
			{
				text = null;
			}
			else
			{
				XmlAttribute xmlAttribute = attributes["group_id"];
				text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
			}
			string text2 = text;
			this.GroupId = (string.IsNullOrWhiteSpace(text2) ? null : text2);
			string value = node.Attributes["name"].Value;
			string value2 = node.Attributes["description"].Value;
			XmlAttribute xmlAttribute2 = node.Attributes["is_visible_only_when_earned"];
			this.IsVisibleOnlyWhenEarned = Convert.ToBoolean((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
			XmlAttribute xmlAttribute3 = node.Attributes["period_start"];
			DateTime dateTime;
			this.PeriodStart = (DateTime.TryParse((xmlAttribute3 != null) ? xmlAttribute3.Value : null, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc) : DateTime.MinValue);
			XmlAttribute xmlAttribute4 = node.Attributes["period_end"];
			DateTime dateTime2;
			this.PeriodEnd = (DateTime.TryParse((xmlAttribute4 != null) ? xmlAttribute4.Value : null, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime2) ? DateTime.SpecifyKind(dateTime2, DateTimeKind.Utc) : DateTime.MaxValue);
			this.Name = new TextObject(value, null);
			this.Description = new TextObject(value2, null);
		}
	}
}
