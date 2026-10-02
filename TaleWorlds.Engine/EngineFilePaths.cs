using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000043 RID: 67
	public static class EngineFilePaths
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00003EB7 File Offset: 0x000020B7
		public static PlatformDirectoryPath ConfigsPath
		{
			get
			{
				return new PlatformDirectoryPath(PlatformFileType.User, "Configs");
			}
		}

		// Token: 0x04000055 RID: 85
		public const string ConfigsDirectoryName = "Configs";
	}
}
