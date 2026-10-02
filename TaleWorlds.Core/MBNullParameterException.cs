using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A5 RID: 165
	public class MBNullParameterException : MBException
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x0001DDC7 File Offset: 0x0001BFC7
		public MBNullParameterException(string parameterName)
			: base("The parameter cannot be null : " + parameterName)
		{
		}
	}
}
