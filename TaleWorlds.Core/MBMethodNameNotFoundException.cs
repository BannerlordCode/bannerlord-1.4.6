using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A3 RID: 163
	public class MBMethodNameNotFoundException : MBException
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0001DDA1 File Offset: 0x0001BFA1
		public MBMethodNameNotFoundException(string methodName)
			: base("Unable to find method " + methodName)
		{
		}
	}
}
