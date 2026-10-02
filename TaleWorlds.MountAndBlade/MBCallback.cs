using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D9 RID: 729
	public class MBCallback : ManagedFromNativeCallback
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x000A36BD File Offset: 0x000A18BD
		public MBCallback(string[] conditionals = null, bool isMultiThreadCallable = false)
			: base(conditionals, isMultiThreadCallable)
		{
		}
	}
}
