using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000099 RID: 153
	public static class RandomOwnerExtensions
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x00054C8E File Offset: 0x00052E8E
		public static int RandomIntWithSeed(this IRandomOwner obj, uint seed)
		{
			return MBRandom.RandomIntWithSeed((uint)obj.RandomValue, seed);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00054C9C File Offset: 0x00052E9C
		public static int RandomIntWithSeed(this IRandomOwner obj, uint seed, int max)
		{
			return obj.RandomIntWithSeed(seed, 0, max);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00054CA7 File Offset: 0x00052EA7
		public static int RandomIntWithSeed(this IRandomOwner obj, uint seed, int min, int max)
		{
			return RandomOwnerExtensions.Random(obj.RandomIntWithSeed(seed), min, max);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00054CB7 File Offset: 0x00052EB7
		public static float RandomFloatWithSeed(this IRandomOwner obj, uint seed)
		{
			return MBRandom.RandomFloatWithSeed((uint)obj.RandomValue, seed);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00054CC5 File Offset: 0x00052EC5
		public static float RandomFloatWithSeed(this IRandomOwner obj, uint seed, float max)
		{
			return obj.RandomFloatWithSeed(seed, 0f, max);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00054CD4 File Offset: 0x00052ED4
		public static float RandomFloatWithSeed(this IRandomOwner obj, uint seed, float min, float max)
		{
			return RandomOwnerExtensions.Random(obj.RandomFloatWithSeed(seed), min, max);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00054CE4 File Offset: 0x00052EE4
		public static int RandomInt(this IRandomOwner obj)
		{
			return obj.RandomValue;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00054CEC File Offset: 0x00052EEC
		public static int RandomInt(this IRandomOwner obj, int max)
		{
			return obj.RandomInt(0, max);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00054CF6 File Offset: 0x00052EF6
		public static int RandomInt(this IRandomOwner obj, int min, int max)
		{
			return RandomOwnerExtensions.Random(obj.RandomInt(), min, max);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00054D05 File Offset: 0x00052F05
		public static float RandomFloat(this IRandomOwner obj)
		{
			return (float)obj.RandomValue / 2.1474836E+09f;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00054D14 File Offset: 0x00052F14
		public static float RandomFloat(this IRandomOwner obj, float max)
		{
			return obj.RandomFloat(0f, max);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00054D22 File Offset: 0x00052F22
		public static float RandomFloat(this IRandomOwner obj, float min, float max)
		{
			return RandomOwnerExtensions.Random(obj.RandomFloat(), min, max);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00054D34 File Offset: 0x00052F34
		private static int Random(int randomValue, int min, int max)
		{
			int num = max - min;
			if (num == 0)
			{
				Debug.FailedAssert("invalid Random parameters", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\IRandomOwner.cs", "Random", 79);
				return 0;
			}
			return min + randomValue % num;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00054D68 File Offset: 0x00052F68
		private static float Random(float randomValue, float min, float max)
		{
			float num = max - min;
			if (num <= 1E-45f)
			{
				Debug.FailedAssert("invalid Random parameters", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\IRandomOwner.cs", "Random", 91);
				return min;
			}
			return min + randomValue * num;
		}
	}
}
