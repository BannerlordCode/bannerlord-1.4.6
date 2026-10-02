using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200005B RID: 91
	public static class FaceGen
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x00018C44 File Offset: 0x00016E44
		public static void SetInstance(IFaceGen faceGen)
		{
			FaceGen._instance = faceGen;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00018C4C File Offset: 0x00016E4C
		public static BodyProperties GetRandomBodyProperties(int race, bool isFemale, BodyProperties bodyPropertiesMin, BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tatooTags, float variationAmount)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetRandomBodyProperties(race, isFemale, bodyPropertiesMin, bodyPropertiesMax, hairCoverType, seed, hairTags, beardTags, tatooTags, variationAmount);
			}
			return bodyPropertiesMin;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00018C7C File Offset: 0x00016E7C
		public static int GetRaceCount()
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return 0;
			}
			return instance.GetRaceCount();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00018C8E File Offset: 0x00016E8E
		public static int GetRaceOrDefault(string raceId)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return 0;
			}
			return instance.GetRaceOrDefault(raceId);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00018CA1 File Offset: 0x00016EA1
		public static string GetBaseMonsterNameFromRace(int race)
		{
			IFaceGen instance = FaceGen._instance;
			return ((instance != null) ? instance.GetBaseMonsterNameFromRace(race) : null) ?? null;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00018CBA File Offset: 0x00016EBA
		public static string[] GetRaceNames()
		{
			IFaceGen instance = FaceGen._instance;
			return ((instance != null) ? instance.GetRaceNames() : null) ?? null;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00018CD2 File Offset: 0x00016ED2
		public static Monster GetMonster(string monsterID)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return null;
			}
			return instance.GetMonster(monsterID);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00018CE5 File Offset: 0x00016EE5
		public static Monster GetMonsterWithSuffix(int race, string suffix)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return null;
			}
			return instance.GetMonsterWithSuffix(race, suffix);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00018CF9 File Offset: 0x00016EF9
		public static Monster GetBaseMonsterFromRace(int race)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return null;
			}
			return instance.GetBaseMonsterFromRace(race);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00018D0C File Offset: 0x00016F0C
		public static void GenerateParentKey(BodyProperties childBodyProperties, int race, ref BodyProperties motherBodyProperties, ref BodyProperties fatherBodyProperties)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return;
			}
			instance.GenerateParentBody(childBodyProperties, race, ref motherBodyProperties, ref fatherBodyProperties);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00018D21 File Offset: 0x00016F21
		public static void SetHair(ref BodyProperties bodyProperties, int hair, int beard, int tattoo)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return;
			}
			instance.SetHair(ref bodyProperties, hair, beard, tattoo);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00018D36 File Offset: 0x00016F36
		public static void SetBody(ref BodyProperties bodyProperties, int build, int weight)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return;
			}
			instance.SetBody(ref bodyProperties, build, weight);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00018D4A File Offset: 0x00016F4A
		public static void SetPigmentation(ref BodyProperties bodyProperties, int skinColor, int hairColor, int eyeColor)
		{
			IFaceGen instance = FaceGen._instance;
			if (instance == null)
			{
				return;
			}
			instance.SetPigmentation(ref bodyProperties, skinColor, hairColor, eyeColor);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00018D5F File Offset: 0x00016F5F
		public static BodyProperties GetBodyPropertiesWithAge(ref BodyProperties originalBodyProperties, float age)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetBodyPropertiesWithAge(ref originalBodyProperties, age);
			}
			return originalBodyProperties;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00018D7B File Offset: 0x00016F7B
		public static BodyMeshMaturityType GetMaturityTypeWithAge(float age)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetMaturityTypeWithAge(age);
			}
			return BodyMeshMaturityType.Child;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00018D91 File Offset: 0x00016F91
		public static int[] GetHairIndicesByTag(int race, int curGender, float age, string tag)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetHairIndicesByTag(race, curGender, age, tag);
			}
			return Array.Empty<int>();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00018DAE File Offset: 0x00016FAE
		public static int[] GetFacialIndicesByTag(int race, int curGender, float age, string tag)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetFacialIndicesByTag(race, curGender, age, tag);
			}
			return Array.Empty<int>();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00018DCB File Offset: 0x00016FCB
		public static int[] GetTattooIndicesByTag(int race, int curGender, float age, string tag)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetTattooIndicesByTag(race, curGender, age, tag);
			}
			return Array.Empty<int>();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00018DE8 File Offset: 0x00016FE8
		public static float GetTattooZeroProbability(int race, int curGender, float age)
		{
			if (FaceGen._instance != null)
			{
				return FaceGen._instance.GetTattooZeroProbability(race, curGender, age);
			}
			return 0f;
		}

		// Token: 0x04000392 RID: 914
		public const string MonsterSuffixSettlement = "_settlement";

		// Token: 0x04000393 RID: 915
		public const string MonsterSuffixSettlementSlow = "_settlement_slow";

		// Token: 0x04000394 RID: 916
		public const string MonsterSuffixSettlementFast = "_settlement_fast";

		// Token: 0x04000395 RID: 917
		public const string MonsterSuffixChild = "_child";

		// Token: 0x04000396 RID: 918
		public static bool ShowDebugValues;

		// Token: 0x04000397 RID: 919
		public static bool UpdateDeformKeys;

		// Token: 0x04000398 RID: 920
		private static IFaceGen _instance;
	}
}
