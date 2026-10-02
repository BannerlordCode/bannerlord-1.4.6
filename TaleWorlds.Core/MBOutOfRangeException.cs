using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A2 RID: 162
	public class MBOutOfRangeException : MBException
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x0001DD8E File Offset: 0x0001BF8E
		public MBOutOfRangeException(string parameterName)
			: base("The given value is out of range : " + parameterName)
		{
		}
	}
}
