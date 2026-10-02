using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics
{
	// Token: 0x02000178 RID: 376
	public static class CosmeticsManager
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x000114C9 File Offset: 0x0000F6C9
		static CosmeticsManager()
		{
			CosmeticsManager.LoadFromXml(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/mpcosmetics.xml");
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x000114F8 File Offset: 0x0000F6F8
		public static MBReadOnlyList<CosmeticElement> CosmeticElementsList
		{
			get
			{
				return CosmeticsManager._cosmeticElementList;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00011500 File Offset: 0x0000F700
		public static CosmeticElement GetCosmeticElement(string cosmeticId)
		{
			CosmeticElement cosmeticElement;
			if (CosmeticsManager._cosmeticElementsLookup.TryGetValue(cosmeticId, out cosmeticElement))
			{
				return cosmeticElement;
			}
			return null;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00011520 File Offset: 0x0000F720
		public static void LoadFromXml(string path)
		{
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(path);
			streamReader.ReadToEnd();
			xmlDocument.Load(path);
			streamReader.Close();
			CosmeticsManager._cosmeticElementsLookup.Clear();
			MBList<CosmeticElement> mblist = new MBList<CosmeticElement>();
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Cosmetics")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "Cosmetic")
						{
							string value = xmlNode2.Attributes["id"].Value;
							CosmeticsManager.CosmeticType cosmeticType = CosmeticsManager.CosmeticType.Clothing;
							string value2 = xmlNode2.Attributes["type"].Value;
							if (value2 == "Clothing")
							{
								cosmeticType = CosmeticsManager.CosmeticType.Clothing;
							}
							else if (value2 == "Frame")
							{
								cosmeticType = CosmeticsManager.CosmeticType.Frame;
							}
							else if (value2 == "Sigil")
							{
								cosmeticType = CosmeticsManager.CosmeticType.Sigil;
							}
							else if (value2 == "Taunt")
							{
								cosmeticType = CosmeticsManager.CosmeticType.Taunt;
							}
							else
							{
								Debug.FailedAssert("Invalid cosmetic type: " + value2, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Cosmetics\\CosmeticsManager.cs", "LoadFromXml", 103);
							}
							CosmeticsManager.CosmeticRarity cosmeticRarity = CosmeticsManager.CosmeticRarity.Common;
							string value3 = xmlNode2.Attributes["rarity"].Value;
							if (value3 == "Common")
							{
								cosmeticRarity = CosmeticsManager.CosmeticRarity.Common;
							}
							else if (value3 == "Rare")
							{
								cosmeticRarity = CosmeticsManager.CosmeticRarity.Rare;
							}
							else if (value3 == "Unique")
							{
								cosmeticRarity = CosmeticsManager.CosmeticRarity.Unique;
							}
							else
							{
								Debug.FailedAssert("Invalid cosmetic rarity: " + value3, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Cosmetics\\CosmeticsManager.cs", "LoadFromXml", 123);
							}
							int num = int.Parse(xmlNode2.Attributes["cost"].Value);
							switch (cosmeticType)
							{
							case CosmeticsManager.CosmeticType.Clothing:
							{
								List<string> list = new List<string>();
								List<Tuple<string, string>> list2 = new List<Tuple<string, string>>();
								foreach (object obj3 in xmlNode2.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj3;
									if (xmlNode3.Name == "Replace")
									{
										foreach (object obj4 in xmlNode3.ChildNodes)
										{
											XmlNode xmlNode4 = (XmlNode)obj4;
											if (xmlNode4.Name == "Item")
											{
												list.Add(xmlNode4.Attributes.Item(0).Value);
											}
											else if (xmlNode4.Name == "Itemless")
											{
												list2.Add(Tuple.Create<string, string>(xmlNode4.Attributes.Item(0).Value, xmlNode4.Attributes.Item(1).Value));
											}
										}
									}
								}
								mblist.Add(new ClothingCosmeticElement(value, cosmeticRarity, num, list, list2));
								break;
							}
							case CosmeticsManager.CosmeticType.Frame:
								mblist.Add(new CosmeticElement(value, cosmeticRarity, num, cosmeticType));
								break;
							case CosmeticsManager.CosmeticType.Sigil:
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								string text;
								if (attributes == null)
								{
									text = null;
								}
								else
								{
									XmlAttribute xmlAttribute = attributes["banner_code"];
									text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
								}
								string text2 = text;
								mblist.Add(new SigilCosmeticElement(value, cosmeticRarity, num, text2));
								break;
							}
							case CosmeticsManager.CosmeticType.Taunt:
							{
								XmlAttributeCollection attributes2 = xmlNode2.Attributes;
								string text3;
								if (attributes2 == null)
								{
									text3 = null;
								}
								else
								{
									XmlAttribute xmlAttribute2 = attributes2["name"];
									text3 = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
								}
								string text4 = text3;
								TauntCosmeticElement tauntCosmeticElement = new TauntCosmeticElement(-1, value, cosmeticRarity, num, text4);
								mblist.Add(tauntCosmeticElement);
								break;
							}
							}
						}
					}
				}
			}
			CosmeticsManager._cosmeticElementsLookup = new Dictionary<string, CosmeticElement>();
			foreach (CosmeticElement cosmeticElement in mblist)
			{
				CosmeticsManager._cosmeticElementsLookup[cosmeticElement.Id] = cosmeticElement;
			}
			CosmeticsManager._cosmeticElementList = mblist;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000119DC File Offset: 0x0000FBDC
		private static bool CheckForCosmeticsListDuplicatesDebug()
		{
			for (int i = 0; i < CosmeticsManager._cosmeticElementList.Count; i++)
			{
				for (int j = i + 1; j < CosmeticsManager._cosmeticElementList.Count; j++)
				{
					if (CosmeticsManager._cosmeticElementList[i].Id == CosmeticsManager._cosmeticElementList[j].Id)
					{
						Debug.FailedAssert(CosmeticsManager._cosmeticElementList[i].Id + " has more than one entry.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Cosmetics\\CosmeticsManager.cs", "CheckForCosmeticsListDuplicatesDebug", 200);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400051D RID: 1309
		private static MBReadOnlyList<CosmeticElement> _cosmeticElementList = new MBReadOnlyList<CosmeticElement>();

		// Token: 0x0400051E RID: 1310
		private static Dictionary<string, CosmeticElement> _cosmeticElementsLookup = new Dictionary<string, CosmeticElement>();

		// Token: 0x020001D8 RID: 472
		public enum CosmeticRarity
		{
			// Token: 0x040006D6 RID: 1750
			Default,
			// Token: 0x040006D7 RID: 1751
			Common,
			// Token: 0x040006D8 RID: 1752
			Rare,
			// Token: 0x040006D9 RID: 1753
			Unique
		}

		// Token: 0x020001D9 RID: 473
		public enum CosmeticType
		{
			// Token: 0x040006DB RID: 1755
			Clothing,
			// Token: 0x040006DC RID: 1756
			Frame,
			// Token: 0x040006DD RID: 1757
			Sigil,
			// Token: 0x040006DE RID: 1758
			Taunt
		}
	}
}
