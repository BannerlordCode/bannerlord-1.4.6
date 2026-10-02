using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A4 RID: 164
	public class MBInvalidParameterException : MBException
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x0001DDB4 File Offset: 0x0001BFB4
		public MBInvalidParameterException(string parameterName)
			: base("The parameter must be valid : " + parameterName)
		{
		}
	}
}
