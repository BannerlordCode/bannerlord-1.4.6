using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000120 RID: 288
	internal class ItemList
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x000083D0 File Offset: 0x000065D0
		static ItemList()
		{
			XmlDocument xmlDocument = new XmlDocument();
			string text = ModuleHelper.GetModuleFullPath("Native") + "ModuleData/mpitems.xml";
			if (ConfigurationManager.GetAppSettings("MultiplayerItemsFileName") != null)
			{
				text = ConfigurationManager.GetAppSettings("MultiplayerItemsFileName");
			}
			xmlDocument.Load(text);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Items");
			if (xmlNode == null)
			{
				throw new Exception("'Items' node is not defined in mpitems.xml");
			}
			Debug.Print("---" + xmlNode.Name, 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.NodeType == XmlNodeType.Element)
				{
					ItemInnerData itemInnerData = new ItemInnerData();
					itemInnerData.Deserialize(xmlNode2);
					Debug.Print(itemInnerData.TypeId, 0, Debug.DebugColor.White, 17592186044416UL);
					if (!ItemList._items.ContainsKey(itemInnerData.TypeId))
					{
						ItemList._items.Add(itemInnerData.TypeId, itemInnerData);
					}
					else
					{
						Debug.Print("--- Item type id already exists, check mpitems.xml for item type Id:" + itemInnerData.TypeId, 0, Debug.DebugColor.White, 17592186044416UL);
					}
				}
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00008524 File Offset: 0x00006724
		internal static ItemType GetItemTypeOf(string typeId)
		{
			return ItemList._items[typeId].Type;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00008536 File Offset: 0x00006736
		internal static bool IsItemValid(string itemId, string modifierId)
		{
			return ItemList._items.ContainsKey(itemId);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00008543 File Offset: 0x00006743
		internal static int GetPriceOf(string itemId, string modifierId)
		{
			return ItemList._items[itemId].Price;
		}

		// Token: 0x040002C8 RID: 712
		private static Dictionary<string, ItemInnerData> _items = new Dictionary<string, ItemInnerData>();
	}
}
