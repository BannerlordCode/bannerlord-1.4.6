using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000070 RID: 112
	[EngineClass("rglNative_script_component")]
	public sealed class NativeScriptComponent : ScriptComponent
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0000A85B File Offset: 0x00008A5B
		internal NativeScriptComponent(UIntPtr pointer)
			: base(pointer)
		{
		}
	}
}
