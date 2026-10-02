using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000323 RID: 803
	[AttributeUsage(AttributeTargets.Method)]
	public class ConsoleCommandMethod : Attribute
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000B0068 File Offset: 0x000AE268
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x000B0070 File Offset: 0x000AE270
		public string CommandName { get; private set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000B0079 File Offset: 0x000AE279
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x000B0081 File Offset: 0x000AE281
		public string Description { get; private set; }

		// Token: 0x06002D9C RID: 11676 RVA: 0x000B008A File Offset: 0x000AE28A
		public ConsoleCommandMethod(string commandName, string description)
		{
			this.CommandName = commandName;
			this.Description = description;
		}
	}
}
