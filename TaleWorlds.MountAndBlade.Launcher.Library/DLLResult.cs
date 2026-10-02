using System;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000013 RID: 19
	public class DLLResult
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000468E File Offset: 0x0000288E
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004696 File Offset: 0x00002896
		public string DLLName { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000469F File Offset: 0x0000289F
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000046A7 File Offset: 0x000028A7
		public bool IsSafe { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000046B0 File Offset: 0x000028B0
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000046B8 File Offset: 0x000028B8
		public string Information { get; set; }

		// Token: 0x060000A8 RID: 168 RVA: 0x000046C1 File Offset: 0x000028C1
		public DLLResult(string dLLName, bool isSafe, string information)
		{
			this.DLLName = dLLName;
			this.IsSafe = isSafe;
			this.Information = information;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000046DE File Offset: 0x000028DE
		public DLLResult()
		{
			this.DLLName = "";
			this.IsSafe = false;
			this.Information = "";
		}
	}
}
