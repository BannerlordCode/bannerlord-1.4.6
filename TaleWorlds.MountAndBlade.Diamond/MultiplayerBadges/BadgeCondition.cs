using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000164 RID: 356
	public class BadgeCondition
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0000F2BE File Offset: 0x0000D4BE
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0000F2C6 File Offset: 0x0000D4C6
		public ConditionType Type { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0000F2CF File Offset: 0x0000D4CF
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x0000F2D7 File Offset: 0x0000D4D7
		public ConditionGroupType GroupType { get; private set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public TextObject Description { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0000F2F1 File Offset: 0x0000D4F1
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
		public string StringId { get; private set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0000F302 File Offset: 0x0000D502
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0000F30A File Offset: 0x0000D50A
		public IReadOnlyDictionary<string, string> Parameters { get; private set; }

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000F314 File Offset: 0x0000D514
		public BadgeCondition(int index, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			ConditionType conditionType;
			if (!Enum.TryParse<ConditionType>((attributes != null) ? attributes["type"].Value : null, true, out conditionType))
			{
				conditionType = ConditionType.Custom;
				Debug.FailedAssert("No 'type' was provided for a condition", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", ".ctor", 47);
			}
			this.Type = conditionType;
			ConditionGroupType conditionGroupType = ConditionGroupType.Any;
			XmlAttributeCollection attributes2 = node.Attributes;
			bool flag;
			if (attributes2 == null)
			{
				flag = null != null;
			}
			else
			{
				XmlAttribute xmlAttribute = attributes2["group_type"];
				flag = ((xmlAttribute != null) ? xmlAttribute.Value : null) != null;
			}
			if (flag && !Enum.TryParse<ConditionGroupType>(node.Attributes["group_type"].Value, true, out conditionGroupType))
			{
				Debug.FailedAssert("Provided 'group_type' was wrong for a condition", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", ".ctor", 54);
			}
			this.GroupType = conditionGroupType;
			XmlAttributeCollection attributes3 = node.Attributes;
			this.Description = new TextObject((attributes3 != null) ? attributes3["description"].Value : null, null);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Parameter")
				{
					string text = xmlNode.Attributes["name"].Value.Trim();
					string text2 = xmlNode.Attributes["value"].Value.Trim();
					dictionary[text] = text2;
				}
			}
			this.Parameters = dictionary;
			XmlAttributeCollection attributes4 = node.Attributes;
			string text3;
			if (attributes4 == null)
			{
				text3 = null;
			}
			else
			{
				XmlAttribute xmlAttribute2 = attributes4["id"];
				text3 = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
			}
			this.StringId = text3;
			string text4;
			if (this.StringId == null && this.Parameters.TryGetValue("property", out text4))
			{
				this.StringId = text4 + ((this.GroupType == ConditionGroupType.Party) ? ".Party" : ((this.GroupType == ConditionGroupType.Solo) ? ".Solo" : ""));
			}
			if (this.StringId == null)
			{
				this.StringId = "condition." + index;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000F53C File Offset: 0x0000D73C
		public bool Check(string value)
		{
			ConditionType type = this.Type;
			if (type != ConditionType.PlayerData)
			{
				return false;
			}
			string text;
			if (!this.Parameters.TryGetValue("value", out text))
			{
				Debug.FailedAssert("Given condition doesn't have a value parameter", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 94);
				return false;
			}
			return value == text;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000F58C File Offset: 0x0000D78C
		public bool Check(int value)
		{
			ConditionType type = this.Type;
			if (type - ConditionType.PlayerDataNumeric > 2)
			{
				return false;
			}
			string text;
			if (this.Parameters.TryGetValue("value", out text))
			{
				int num;
				if (!int.TryParse(text, out num))
				{
					Debug.FailedAssert("Given condition value parameter is not valid number", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 115);
					return false;
				}
				return value == num;
			}
			else
			{
				string text2;
				bool flag = this.Parameters.TryGetValue("min_value", out text2);
				string text3;
				bool flag2 = this.Parameters.TryGetValue("max_value", out text3);
				int minValue = int.MinValue;
				int maxValue = int.MaxValue;
				if (flag && !int.TryParse(text2, out minValue))
				{
					Debug.FailedAssert("Given condition min_value parameter is not valid number", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 129);
					return false;
				}
				if (flag2 && !int.TryParse(text3, out maxValue))
				{
					Debug.FailedAssert("Given condition max_value parameter is not valid number", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeCondition.cs", "Check", 134);
					return false;
				}
				return (flag || flag2) && value >= minValue && value <= maxValue;
			}
		}
	}
}
