using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000092 RID: 146
	[EngineClass("rglTexture_view")]
	public sealed class TextureView : View
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x0000E89B File Offset: 0x0000CA9B
		internal TextureView(UIntPtr meshPointer)
			: base(meshPointer)
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		public static TextureView CreateTextureView()
		{
			return EngineApplicationInterface.ITextureView.CreateTextureView();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		public void SetTexture(Texture texture)
		{
			EngineApplicationInterface.ITextureView.SetTexture(base.Pointer, texture.Pointer);
		}
	}
}
