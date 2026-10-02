using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031A RID: 794
	public static class MultiplayerGameTypes
	{
		// Token: 0x06002D33 RID: 11571 RVA: 0x000AF55A File Offset: 0x000AD75A
		public static void Initialize()
		{
			MultiplayerGameTypes.CreateGameTypeInformations();
			MultiplayerGameTypes.LoadMultiplayerSceneInformations();
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000AF566 File Offset: 0x000AD766
		public static bool CheckGameTypeInfoExists(string gameType)
		{
			return MultiplayerGameTypes._multiplayerGameTypeInfos.ContainsKey(gameType);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000AF573 File Offset: 0x000AD773
		public static MultiplayerGameTypeInfo GetGameTypeInfo(string gameType)
		{
			if (MultiplayerGameTypes._multiplayerGameTypeInfos.ContainsKey(gameType))
			{
				return MultiplayerGameTypes._multiplayerGameTypeInfos[gameType];
			}
			Debug.Print("Cannot find game type:" + gameType, 0, Debug.DebugColor.White, 17592186044416UL);
			return null;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000AF5AC File Offset: 0x000AD7AC
		private static void LoadMultiplayerSceneInformations()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(ModuleHelper.GetModuleFullPath("Native") + "ModuleData/Multiplayer/MultiplayerScenes.xml");
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == "MultiplayerScenes")
				{
					using (IEnumerator enumerator2 = xmlNode.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.NodeType != XmlNodeType.Comment)
							{
								string innerText = xmlNode2.Attributes["name"].InnerText;
								foreach (object obj3 in xmlNode2.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj3;
									if (xmlNode3.NodeType != XmlNodeType.Comment)
									{
										string innerText2 = xmlNode3.Attributes["name"].InnerText;
										if (MultiplayerGameTypes._multiplayerGameTypeInfos.ContainsKey(innerText2))
										{
											MultiplayerGameTypes._multiplayerGameTypeInfos[innerText2].Scenes.Add(innerText);
										}
									}
								}
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000AF764 File Offset: 0x000AD964
		private static void CreateGameTypeInformations()
		{
			MultiplayerGameTypes._multiplayerGameTypeInfos = new Dictionary<string, MultiplayerGameTypeInfo>();
			foreach (MultiplayerGameTypeInfo multiplayerGameTypeInfo in Module.CurrentModule.GetMultiplayerGameTypes())
			{
				MultiplayerGameTypes._multiplayerGameTypeInfos.Add(multiplayerGameTypeInfo.GameType, multiplayerGameTypeInfo);
			}
		}

		// Token: 0x040011D2 RID: 4562
		private static Dictionary<string, MultiplayerGameTypeInfo> _multiplayerGameTypeInfos;
	}
}
