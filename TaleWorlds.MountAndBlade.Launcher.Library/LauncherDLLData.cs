using System;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000011 RID: 17
	public class LauncherDLLData
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004562 File Offset: 0x00002762
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000456A File Offset: 0x0000276A
		public SubModuleInfo SubModule { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004573 File Offset: 0x00002773
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000457B File Offset: 0x0000277B
		public bool IsDangerous { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004584 File Offset: 0x00002784
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000458C File Offset: 0x0000278C
		public string VerifyInformation { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004595 File Offset: 0x00002795
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000459D File Offset: 0x0000279D
		public uint Size { get; private set; }

		// Token: 0x06000097 RID: 151 RVA: 0x000045A6 File Offset: 0x000027A6
		public LauncherDLLData(SubModuleInfo subModule, bool isDangerous, string verifyInformation, uint size)
		{
			this.SubModule = subModule;
			this.IsDangerous = isDangerous;
			this.VerifyInformation = verifyInformation;
			this.Size = size;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000045CB File Offset: 0x000027CB
		public void SetIsDLLDangerous(bool isDangerous)
		{
			this.IsDangerous = isDangerous;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000045D4 File Offset: 0x000027D4
		public void SetDLLSize(uint size)
		{
			this.Size = size;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000045DD File Offset: 0x000027DD
		public void SetDLLVerifyInformation(string info)
		{
			this.VerifyInformation = info;
		}
	}
}
