using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A6 RID: 166
	public class MBNotNullParameterException : MBException
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0001DDDA File Offset: 0x0001BFDA
		public MBNotNullParameterException(string parameterName)
			: base("The parameter must be null : " + parameterName)
		{
		}
	}
}
