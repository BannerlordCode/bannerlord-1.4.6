using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Core
{
	// Token: 0x0200009F RID: 159
	public class MBException : ApplicationException
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x0001DD36 File Offset: 0x0001BF36
		public MBException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public MBException(string message)
			: base(message)
		{
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001DD49 File Offset: 0x0001BF49
		public MBException()
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001DD51 File Offset: 0x0001BF51
		public MBException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
