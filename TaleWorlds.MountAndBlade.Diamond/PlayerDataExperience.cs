using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000143 RID: 323
	public struct PlayerDataExperience
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0000D033 File Offset: 0x0000B233
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0000D03B File Offset: 0x0000B23B
		public int Experience { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0000D044 File Offset: 0x0000B244
		public int Level
		{
			get
			{
				return PlayerDataExperience.CalculateLevelFromExperience(this.Experience);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0000D051 File Offset: 0x0000B251
		public int ExperienceToNextLevel
		{
			get
			{
				return PlayerDataExperience.CalculateExperienceFromLevel(this.Level + 1) - this.Experience;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0000D067 File Offset: 0x0000B267
		public int ExperienceInCurrentLevel
		{
			get
			{
				return this.Experience - PlayerDataExperience.CalculateExperienceFromLevel(this.Level);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000D07B File Offset: 0x0000B27B
		static PlayerDataExperience()
		{
			PlayerDataExperience.InitializeXPRequirements();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000D089 File Offset: 0x0000B289
		public PlayerDataExperience(int experience)
		{
			this.Experience = experience;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000D094 File Offset: 0x0000B294
		public static int CalculateLevelFromExperience(int experience)
		{
			int num = 1;
			int i = 0;
			while (i <= experience)
			{
				i += PlayerDataExperience.ExperienceRequiredForLevel(num + 1);
				if (i <= experience)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000D0BF File Offset: 0x0000B2BF
		public static int CalculateExperienceFromLevel(int level)
		{
			if (level == 1)
			{
				return 0;
			}
			if (level < PlayerDataExperience._maxLevelForXPRequirementCalculation)
			{
				return PlayerDataExperience._levelToXP[level];
			}
			return PlayerDataExperience.ExperienceRequiredForLevel(level) + PlayerDataExperience.CalculateExperienceFromLevel(level - 1);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0000D0E6 File Offset: 0x0000B2E6
		public static int ExperienceRequiredForLevel(int level)
		{
			return Convert.ToInt32(Math.Floor(100.0 * Math.Pow((double)(level - 1), 1.03)));
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0000D110 File Offset: 0x0000B310
		private static void InitializeXPRequirements()
		{
			PlayerDataExperience._levelToXP = new int[PlayerDataExperience._maxLevelForXPRequirementCalculation];
			int num = 0;
			for (int i = 2; i < PlayerDataExperience._maxLevelForXPRequirementCalculation; i++)
			{
				num += PlayerDataExperience.ExperienceRequiredForLevel(i);
				PlayerDataExperience._levelToXP[i] = num;
			}
		}

		// Token: 0x040003BD RID: 957
		private static int[] _levelToXP;

		// Token: 0x040003BE RID: 958
		private static readonly int _maxLevelForXPRequirementCalculation = 30;
	}
}
