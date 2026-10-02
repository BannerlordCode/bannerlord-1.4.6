using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A0 RID: 160
	public class MBTypeMismatchException : MBException
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x0001DD5B File Offset: 0x0001BF5B
		public MBTypeMismatchException(string exceptionString)
			: base("Type Does not match with the expected one. " + exceptionString)
		{
		}
	}
}
