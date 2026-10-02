using System;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x02000172 RID: 370
	public static class MetaDataExtensions
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x0008D624 File Offset: 0x0008B824
		public static string GetUniqueGameId(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("UniqueGameId", out text))
			{
				return "";
			}
			return text;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0008D64C File Offset: 0x0008B84C
		public static int GetMainHeroLevel(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainHeroLevel", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0008D674 File Offset: 0x0008B874
		public static float GetMainPartyFood(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainPartyFood", out text))
			{
				return 0f;
			}
			return float.Parse(text);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0008D6A0 File Offset: 0x0008B8A0
		public static int GetMainHeroGold(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainHeroGold", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0008D6C8 File Offset: 0x0008B8C8
		public static float GetClanInfluence(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("ClanInfluence", out text))
			{
				return 0f;
			}
			return float.Parse(text);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0008D6F4 File Offset: 0x0008B8F4
		public static int GetClanFiefs(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("ClanFiefs", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0008D71C File Offset: 0x0008B91C
		public static int GetMainPartyShipCount(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainPartyShipCount", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0008D744 File Offset: 0x0008B944
		public static int GetMainPartyHealthyMemberCount(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainPartyHealthyMemberCount", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0008D76C File Offset: 0x0008B96C
		public static int GetMainPartyPrisonerMemberCount(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainPartyPrisonerMemberCount", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0008D794 File Offset: 0x0008B994
		public static int GetMainPartyWoundedMemberCount(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainPartyWoundedMemberCount", out text))
			{
				return 0;
			}
			return int.Parse(text);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0008D7BC File Offset: 0x0008B9BC
		public static string GetClanBannerCode(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("ClanBannerCode", out text))
			{
				return "";
			}
			return text;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0008D7E4 File Offset: 0x0008B9E4
		public static string GetCharacterName(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("CharacterName", out text))
			{
				return "";
			}
			return text;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0008D80C File Offset: 0x0008BA0C
		public static string GetCharacterVisualCode(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("MainHeroVisual", out text))
			{
				return "";
			}
			return text;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0008D834 File Offset: 0x0008BA34
		public static double GetDayLong(this MetaData metaData)
		{
			string text;
			if (metaData == null || !metaData.TryGetValue("DayLong", out text))
			{
				return 0.0;
			}
			return double.Parse(text);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0008D864 File Offset: 0x0008BA64
		public static bool GetIronmanMode(this MetaData metaData)
		{
			string text;
			int num;
			return metaData != null && metaData.TryGetValue("IronmanMode", out text) && int.TryParse(text, out num) && num == 1;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0008D894 File Offset: 0x0008BA94
		public static int GetPlayerHealthPercentage(this MetaData metaData)
		{
			string text;
			int num;
			if (metaData == null || !metaData.TryGetValue("HealthPercentage", out text) || !int.TryParse(text, out num))
			{
				return 100;
			}
			return num;
		}
	}
}
