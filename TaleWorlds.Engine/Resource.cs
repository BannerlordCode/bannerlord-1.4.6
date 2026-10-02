using System;
using System.Diagnostics;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200007E RID: 126
	[EngineClass("rglResource")]
	public abstract class Resource : NativeObject
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0000B116 File Offset: 0x00009316
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000B128 File Offset: 0x00009328
		protected Resource()
		{
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000B130 File Offset: 0x00009330
		internal Resource(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000B13F File Offset: 0x0000933F
		[Conditional("_RGL_KEEP_ASSERTS")]
		protected void CheckResourceParameter(Resource param, string paramName = "")
		{
			if (param == null)
			{
				throw new NullReferenceException(paramName);
			}
			if (!param.IsValid)
			{
				throw new ArgumentException(paramName);
			}
		}
	}
}
