using System;
using System.IO;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200009F RID: 159
	public sealed class ManagedParameters : IManagedParametersInitializer
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00054D9E File Offset: 0x00052F9E
		public static ManagedParameters Instance { get; } = new ManagedParameters();

		// Token: 0x06001309 RID: 4873 RVA: 0x00054DA8 File Offset: 0x00052FA8
		public void Initialize(string relativeXmlPath)
		{
			XmlDocument xmlDocument = ManagedParameters.LoadXmlFile(relativeXmlPath);
			this.LoadFromXml(xmlDocument);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00054DC4 File Offset: 0x00052FC4
		private void LoadFromXml(XmlNode doc)
		{
			XmlNode xmlNode = null;
			if (doc.ChildNodes[1].ChildNodes[0].Name == "managed_campaign_parameters")
			{
				xmlNode = doc.ChildNodes[1].ChildNodes[0].ChildNodes[0];
			}
			while (xmlNode != null)
			{
				ManagedParametersEnum managedParametersEnum;
				if (xmlNode.Name == "managed_campaign_parameter" && xmlNode.NodeType != XmlNodeType.Comment && Enum.TryParse<ManagedParametersEnum>(xmlNode.Attributes["id"].Value, true, out managedParametersEnum))
				{
					this._managedParametersArray[(int)managedParametersEnum] = bool.Parse(xmlNode.Attributes["value"].Value);
				}
				xmlNode = xmlNode.NextSibling;
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00054E8C File Offset: 0x0005308C
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

		// Token: 0x0600130C RID: 4876 RVA: 0x00054ED5 File Offset: 0x000530D5
		public bool GetManagedParameter(ManagedParametersEnum _managedParametersEnum)
		{
			return this._managedParametersArray[(int)_managedParametersEnum];
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00054EE0 File Offset: 0x000530E0
		public bool SetManagedParameter(ManagedParametersEnum _managedParametersEnum, bool value)
		{
			this._managedParametersArray[(int)_managedParametersEnum] = value;
			return value;
		}

		// Token: 0x0400062D RID: 1581
		private readonly bool[] _managedParametersArray = new bool[2];
	}
}
