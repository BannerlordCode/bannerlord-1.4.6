using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000165 RID: 357
	public static class BadgeManager
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0000F683 File Offset: 0x0000D883
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0000F68A File Offset: 0x0000D88A
		public static List<Badge> Badges { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0000F692 File Offset: 0x0000D892
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0000F699 File Offset: 0x0000D899
		public static bool IsInitialized { get; private set; }

		// Token: 0x060009EA RID: 2538 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public static void InitializeWithXML(string xmlPath)
		{
			Debug.Print("BadgeManager::InitializeWithXML", 0, Debug.DebugColor.White, 17592186044416UL);
			if (BadgeManager.IsInitialized)
			{
				return;
			}
			BadgeManager.LoadFromXml(xmlPath);
			BadgeManager.IsInitialized = true;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public static void OnFinalize()
		{
			Debug.Print("BadgeManager::OnFinalize", 0, Debug.DebugColor.White, 17592186044416UL);
			if (!BadgeManager.IsInitialized)
			{
				return;
			}
			BadgeManager._badgesById.Clear();
			BadgeManager._badgesByType.Clear();
			BadgeManager.Badges.Clear();
			BadgeManager._badgesById = null;
			BadgeManager._badgesByType = null;
			BadgeManager.Badges = null;
			BadgeManager.IsInitialized = false;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000F734 File Offset: 0x0000D934
		private static void LoadFromXml(string path)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (StreamReader streamReader = new StreamReader(path))
			{
				string text = streamReader.ReadToEnd();
				xmlDocument.LoadXml(text);
				streamReader.Close();
			}
			BadgeManager._badgesById = new Dictionary<string, Badge>();
			BadgeManager._badgesByType = new Dictionary<BadgeType, List<Badge>>();
			BadgeManager.Badges = new List<Badge>();
			foreach (object obj in xmlDocument.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Badges")
				{
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.Name == "Badge")
						{
							BadgeType badgeType = BadgeType.Custom;
							if (!Enum.TryParse<BadgeType>(xmlNode2.Attributes["type"].Value, true, out badgeType))
							{
								Debug.FailedAssert("No 'type' was provided for a badge", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "LoadFromXml", 82);
							}
							Badge badge = null;
							if (badgeType > BadgeType.OnLogin)
							{
								if (badgeType == BadgeType.Conditional)
								{
									badge = new ConditionalBadge(BadgeManager.Badges.Count, badgeType);
								}
							}
							else
							{
								badge = new Badge(BadgeManager.Badges.Count, badgeType);
							}
							badge.Deserialize(xmlNode2);
							BadgeManager._badgesById[badge.StringId] = badge;
							BadgeManager.Badges.Add(badge);
							List<Badge> list;
							if (!BadgeManager._badgesByType.TryGetValue(badgeType, out list))
							{
								list = new List<Badge>();
								BadgeManager._badgesByType.Add(badgeType, list);
							}
							list.Add(badge);
						}
					}
				}
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000F950 File Offset: 0x0000DB50
		public static Badge GetByIndex(int index)
		{
			if (index == -1 || BadgeManager.Badges == null || BadgeManager.Badges.Count <= index || index < 0)
			{
				return null;
			}
			return BadgeManager.Badges[index];
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000F97C File Offset: 0x0000DB7C
		public static Badge GetById(string id)
		{
			Badge badge;
			if (id == null || !BadgeManager._badgesById.TryGetValue(id, out badge))
			{
				return null;
			}
			return badge;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		public static List<Badge> GetByType(BadgeType type)
		{
			List<Badge> list;
			if (!BadgeManager._badgesByType.TryGetValue(type, out list))
			{
				list = new List<Badge>();
				BadgeManager._badgesByType.Add(type, list);
			}
			return list;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		public static string GetBadgeConditionValue(this PlayerData playerData, BadgeCondition condition)
		{
			if (playerData == null)
			{
				Debug.FailedAssert("PlayerData is null on get value", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionValue", 143);
				return "";
			}
			string text;
			if (!condition.Parameters.TryGetValue("property", out text))
			{
				Debug.FailedAssert("Condition with type PlayerData does not have Property parameter", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionValue", 150);
				return "";
			}
			if (text == "ShownBadgeId")
			{
				return playerData.ShownBadgeId;
			}
			return "";
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		public static int GetBadgeConditionNumericValue(this PlayerData playerData, BadgeCondition condition)
		{
			if (playerData == null)
			{
				Debug.FailedAssert("PlayerData is null on get value", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionNumericValue", 167);
				return 0;
			}
			string text;
			if (!condition.Parameters.TryGetValue("property", out text))
			{
				Debug.FailedAssert("Condition with type PlayerDataNumeric does not have Property parameter", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\MultiplayerBadges\\BadgeManager.cs", "GetBadgeConditionNumericValue", 174);
				return 0;
			}
			int num = 0;
			string[] array = text.Split(new char[] { '.' });
			string text2 = array[0];
			uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text2);
			if (num2 <= 1096112509U)
			{
				if (num2 <= 267161228U)
				{
					if (num2 != 192547213U)
					{
						if (num2 == 267161228U)
						{
							if (text2 == "Stats")
							{
								if (array.Length == 3 && playerData.Stats != null)
								{
									string text3 = array[1].Trim().ToLower();
									PlayerStatsBase[] stats = playerData.Stats;
									int i = 0;
									while (i < stats.Length)
									{
										PlayerStatsBase playerStatsBase = stats[i];
										if (playerStatsBase.GameType.Trim().ToLower() == text3)
										{
											text2 = array[2];
											if (text2 == "KillCount")
											{
												num = playerStatsBase.KillCount;
												break;
											}
											if (text2 == "DeathCount")
											{
												num = playerStatsBase.DeathCount;
												break;
											}
											if (text2 == "AssistCount")
											{
												num = playerStatsBase.AssistCount;
												break;
											}
											if (text2 == "WinCount")
											{
												num = playerStatsBase.WinCount;
												break;
											}
											if (!(text2 == "LoseCount"))
											{
												break;
											}
											num = playerStatsBase.LoseCount;
											break;
										}
										else
										{
											i++;
										}
									}
								}
							}
						}
					}
					else if (text2 == "AssistCount")
					{
						num = playerData.AssistCount;
					}
				}
				else if (num2 != 1093842208U)
				{
					if (num2 == 1096112509U)
					{
						if (text2 == "Level")
						{
							num = playerData.Level;
						}
					}
				}
				else if (text2 == "WinCount")
				{
					num = playerData.WinCount;
				}
			}
			else if (num2 <= 2667250970U)
			{
				if (num2 != 1128891543U)
				{
					if (num2 == 2667250970U)
					{
						if (text2 == "Playtime")
						{
							num = playerData.Playtime;
						}
					}
				}
				else if (text2 == "LoseCount")
				{
					num = playerData.LoseCount;
				}
			}
			else if (num2 != 3945868512U)
			{
				if (num2 == 4058818476U)
				{
					if (text2 == "DeathCount")
					{
						num = playerData.DeathCount;
					}
				}
			}
			else if (text2 == "KillCount")
			{
				num = playerData.KillCount;
			}
			return num;
		}

		// Token: 0x040004D4 RID: 1236
		public const string PropertyParameterName = "property";

		// Token: 0x040004D5 RID: 1237
		public const string ValueParameterName = "value";

		// Token: 0x040004D6 RID: 1238
		public const string MinValueParameterName = "min_value";

		// Token: 0x040004D7 RID: 1239
		public const string MaxValueParameterName = "max_value";

		// Token: 0x040004D8 RID: 1240
		public const string IsBestParameterName = "is_best";

		// Token: 0x040004DB RID: 1243
		private static Dictionary<string, Badge> _badgesById;

		// Token: 0x040004DC RID: 1244
		private static Dictionary<BadgeType, List<Badge>> _badgesByType;
	}
}
