using System;
using System.Globalization;
using System.Xml;

namespace TaleWorlds.Core
{
	// Token: 0x020000E1 RID: 225
	public static class XmlHelper
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x000253F0 File Offset: 0x000235F0
		public static int ReadInt(XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute == null)
			{
				return 0;
			}
			return int.Parse(xmlAttribute.Value);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002541C File Offset: 0x0002361C
		public static void ReadInt(ref int val, XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute != null)
			{
				val = int.Parse(xmlAttribute.Value);
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00025448 File Offset: 0x00023648
		public static float ReadFloat(XmlNode node, string str, float defaultValue = 0f)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute == null)
			{
				return defaultValue;
			}
			return float.Parse(xmlAttribute.Value);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00025474 File Offset: 0x00023674
		public static string ReadString(XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute == null)
			{
				return "";
			}
			return xmlAttribute.Value;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000254A0 File Offset: 0x000236A0
		public static void ReadHexCode(ref uint val, XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			if (xmlAttribute != null)
			{
				string text = xmlAttribute.Value;
				text = text.Substring(2);
				val = uint.Parse(text, NumberStyles.HexNumber);
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000254DC File Offset: 0x000236DC
		public static bool ReadBool(XmlNode node, string str)
		{
			XmlAttribute xmlAttribute = node.Attributes[str];
			return xmlAttribute != null && Convert.ToBoolean(xmlAttribute.InnerText);
		}
	}
}
