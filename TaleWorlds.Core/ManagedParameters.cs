using System;
using System.IO;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x0200009A RID: 154
	public sealed class ManagedParameters : IManagedParametersInitializer
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0001D281 File Offset: 0x0001B481
		public static ManagedParameters Instance { get; } = new ManagedParameters();

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D288 File Offset: 0x0001B488
		public static float GetParameter(ManagedParametersEnum managedParameterType)
		{
			return ManagedParameters.Instance._managedParametersArray[(int)managedParameterType];
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001D296 File Offset: 0x0001B496
		public static void SetParameter(ManagedParametersEnum managedParameterType, float newValue)
		{
			ManagedParameters.Instance._managedParametersArray[(int)managedParameterType] = newValue;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		public void Initialize(string relativeXmlPath)
		{
			XmlDocument mergedXmlForManaged = MBObjectManager.GetMergedXmlForManaged("CoreParameters", true, true, "");
			this.LoadFromXml(mergedXmlForManaged);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001D2CE File Offset: 0x0001B4CE
		private ManagedParameters()
		{
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		private static XmlDocument LoadXmlFile(string path)
		{
			Debug.Print("opening " + path, 0, Debug.DebugColor.White, 17592186044416UL);
			XmlDocument xmlDocument = new XmlDocument();
			StreamReader streamReader = new StreamReader(path);
			string text = streamReader.ReadToEnd();
			xmlDocument.LoadXml(text);
			streamReader.Close();
			return xmlDocument;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001D330 File Offset: 0x0001B530
		private void LoadFromXml(XmlNode doc)
		{
			Debug.Print("loading managed_core_parameters.xml", 0, Debug.DebugColor.White, 17592186044416UL);
			if (doc.ChildNodes.Count < 1)
			{
				throw new TWXmlLoadException("Incorrect XML document format.");
			}
			XmlNode xmlNode = doc.SelectSingleNode(".//managed_core_parameters");
			if (xmlNode == null)
			{
				throw new TWXmlLoadException("Incorrect XML document format.");
			}
			for (XmlNode xmlNode2 = xmlNode.ChildNodes[0]; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
			{
				ManagedParametersEnum managedParametersEnum;
				if (xmlNode2.Name == "managed_core_parameter" && xmlNode2.NodeType != XmlNodeType.Comment && Enum.TryParse<ManagedParametersEnum>(xmlNode2.Attributes["id"].Value, true, out managedParametersEnum))
				{
					this._managedParametersArray[(int)managedParametersEnum] = float.Parse(xmlNode2.Attributes["value"].Value);
				}
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001D3F9 File Offset: 0x0001B5F9
		public float GetManagedParameter(ManagedParametersEnum managedParameterEnum)
		{
			return this._managedParametersArray[(int)managedParameterEnum];
		}

		// Token: 0x040004FC RID: 1276
		private readonly float[] _managedParametersArray = new float[72];
	}
}
