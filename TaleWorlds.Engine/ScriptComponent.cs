using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000086 RID: 134
	[EngineClass("rglScript_component")]
	public abstract class ScriptComponent : NativeObject
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x0000D4EA File Offset: 0x0000B6EA
		protected ScriptComponent()
		{
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0000D4F2 File Offset: 0x0000B6F2
		internal ScriptComponent(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0000D501 File Offset: 0x0000B701
		public string GetName()
		{
			return EngineApplicationInterface.IScriptComponent.GetName(base.Pointer);
		}
	}
}
