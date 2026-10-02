using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x0200005E RID: 94
	public static class FilePaths
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00018E5B File Offset: 0x0001705B
		public static PlatformDirectoryPath SavePath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Game Saves");
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00018E68 File Offset: 0x00017068
		public static PlatformDirectoryPath RecordingsPath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Recordings");
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00018E75 File Offset: 0x00017075
		public static PlatformDirectoryPath StatisticsPath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Statistics");
			}
		}

		// Token: 0x0400039F RID: 927
		public const string SaveDirectoryName = "Game Saves";

		// Token: 0x040003A0 RID: 928
		public const string RecordingsDirectoryName = "Recordings";

		// Token: 0x040003A1 RID: 929
		public const string StatisticsDirectoryName = "Statistics";
	}
}
