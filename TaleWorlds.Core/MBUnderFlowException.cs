using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000A1 RID: 161
	public class MBUnderFlowException : MBException
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x0001DD6E File Offset: 0x0001BF6E
		public MBUnderFlowException()
			: base("The given value is less than the expected value.")
		{
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001DD7B File Offset: 0x0001BF7B
		public MBUnderFlowException(string parameterName)
			: base("The given value is less than the expected value : " + parameterName)
		{
		}
	}
}
