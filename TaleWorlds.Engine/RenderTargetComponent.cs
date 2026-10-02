using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200007D RID: 125
	public sealed class RenderTargetComponent : DotNetObject
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0000B042 File Offset: 0x00009242
		public Texture RenderTarget
		{
			get
			{
				return (Texture)this._renderTargetWeakReference.GetNativeObject();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0000B054 File Offset: 0x00009254
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0000B05C File Offset: 0x0000925C
		public object UserData { get; internal set; }

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000B065 File Offset: 0x00009265
		internal RenderTargetComponent(Texture renderTarget)
		{
			this._renderTargetWeakReference = new WeakNativeObjectReference(renderTarget);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000B079 File Offset: 0x00009279
		internal void OnTargetReleased()
		{
			this.PaintNeeded = null;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000B082 File Offset: 0x00009282
		[EngineCallback(null, false)]
		internal static RenderTargetComponent CreateRenderTargetComponent(Texture renderTarget)
		{
			return new RenderTargetComponent(renderTarget);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000AB5 RID: 2741 RVA: 0x0000B08C File Offset: 0x0000928C
		// (remove) Token: 0x06000AB6 RID: 2742 RVA: 0x0000B0C4 File Offset: 0x000092C4
		internal event RenderTargetComponent.TextureUpdateEventHandler PaintNeeded;

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000B0F9 File Offset: 0x000092F9
		[EngineCallback(null, false)]
		internal void OnPaintNeeded()
		{
			RenderTargetComponent.TextureUpdateEventHandler paintNeeded = this.PaintNeeded;
			if (paintNeeded == null)
			{
				return;
			}
			paintNeeded(this.RenderTarget, EventArgs.Empty);
		}

		// Token: 0x04000195 RID: 405
		private readonly WeakNativeObjectReference _renderTargetWeakReference;

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x06001003 RID: 4099
		public delegate void TextureUpdateEventHandler(Texture sender, EventArgs e);
	}
}
