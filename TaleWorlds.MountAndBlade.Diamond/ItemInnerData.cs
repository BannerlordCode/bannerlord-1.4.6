using System;
using System.Xml;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000121 RID: 289
	internal class ItemInnerData
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000855D File Offset: 0x0000675D
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00008565 File Offset: 0x00006765
		internal string TypeId { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000856E File Offset: 0x0000676E
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00008576 File Offset: 0x00006776
		internal ItemType Type { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0000857F File Offset: 0x0000677F
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00008587 File Offset: 0x00006787
		internal int Price { get; private set; }

		// Token: 0x06000682 RID: 1666 RVA: 0x00008590 File Offset: 0x00006790
		internal void Deserialize(XmlNode node)
		{
			this.TypeId = node.Attributes["id"].Value;
			this.Price = ((node.Attributes["value"] != null) ? int.Parse(node.Attributes["value"].Value) : 0);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "flags")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "flag" && xmlNode2.Attributes["name"].Value == "type")
						{
							string value = xmlNode2.Attributes["value"].Value;
							this.Type = (ItemType)Enum.Parse(typeof(ItemType), value, true);
						}
					}
				}
			}
		}
	}
}
