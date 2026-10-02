using System;
using System.Collections.Generic;
using System.Xml;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000167 RID: 359
	public class ConditionalBadge : Badge
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0000FEA2 File Offset: 0x0000E0A2
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0000FEAA File Offset: 0x0000E0AA
		public IReadOnlyList<BadgeCondition> BadgeConditions { get; private set; }

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000FEB3 File Offset: 0x0000E0B3
		public ConditionalBadge(int index, BadgeType badgeType)
			: base(index, badgeType)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			List<BadgeCondition> list = new List<BadgeCondition>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Condition")
				{
					BadgeCondition badgeCondition = new BadgeCondition(list.Count, xmlNode);
					list.Add(badgeCondition);
				}
			}
			this.BadgeConditions = list;
		}
	}
}
