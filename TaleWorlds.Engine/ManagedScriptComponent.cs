using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005B RID: 91
	[EngineClass("rglManaged_script_component")]
	public sealed class ManagedScriptComponent : ScriptComponent
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00008111 File Offset: 0x00006311
		public ScriptComponentBehavior ScriptComponentBehavior
		{
			get
			{
				return EngineApplicationInterface.IScriptComponent.GetScriptComponentBehavior(base.Pointer);
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00008123 File Offset: 0x00006323
		public void SetVariableEditorWidgetStatus(string field, bool enabled)
		{
			EngineApplicationInterface.IScriptComponent.SetVariableEditorWidgetStatus(base.Pointer, field, enabled);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00008137 File Offset: 0x00006337
		public void SetVariableEditorWidgetValue(string field, RglScriptFieldType fieldType, double value)
		{
			EngineApplicationInterface.IScriptComponent.SetVariableEditorWidgetValue(base.Pointer, field, fieldType, value);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000814C File Offset: 0x0000634C
		private ManagedScriptComponent()
		{
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00008154 File Offset: 0x00006354
		internal ManagedScriptComponent(UIntPtr pointer)
			: base(pointer)
		{
		}
	}
}
