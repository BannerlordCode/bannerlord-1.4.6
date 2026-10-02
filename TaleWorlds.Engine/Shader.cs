using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000089 RID: 137
	public sealed class Shader : Resource
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0000DA74 File Offset: 0x0000BC74
		internal Shader(UIntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0000DA7D File Offset: 0x0000BC7D
		public static Shader GetFromResource(string shaderName)
		{
			return EngineApplicationInterface.IShader.GetFromResource(shaderName);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0000DA8A File Offset: 0x0000BC8A
		public string Name
		{
			get
			{
				return EngineApplicationInterface.IShader.GetName(base.Pointer);
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public ulong GetMaterialShaderFlagMask(string flagName, bool showErrors = true)
		{
			return EngineApplicationInterface.IShader.GetMaterialShaderFlagMask(base.Pointer, flagName, showErrors);
		}
	}
}
