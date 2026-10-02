using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x0200001D RID: 29
	public class BannerTableauTextureProvider : TextureProvider
	{
		// Token: 0x17000021 RID: 33
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00008622 File Offset: 0x00006822
		public string BannerCodeText
		{
			set
			{
				this._bannerTableau.SetBannerCode(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00008630 File Offset: 0x00006830
		public bool IsNineGrid
		{
			set
			{
				this._bannerTableau.SetIsNineGrid(value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000863E File Offset: 0x0000683E
		public float CustomRenderScale
		{
			set
			{
				this._bannerTableau.SetCustomRenderScale(value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000864C File Offset: 0x0000684C
		public Vec2 UpdatePositionValueManual
		{
			set
			{
				this._bannerTableau.SetUpdatePositionValueManual(value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000865A File Offset: 0x0000685A
		public Vec2 UpdateSizeValueManual
		{
			set
			{
				this._bannerTableau.SetUpdateSizeValueManual(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00008668 File Offset: 0x00006868
		public ValueTuple<float, bool> UpdateRotationValueManualWithMirror
		{
			set
			{
				this._bannerTableau.SetUpdateRotationValueManual(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00008676 File Offset: 0x00006876
		public int MeshIndexToUpdate
		{
			set
			{
				this._bannerTableau.SetMeshIndexToUpdate(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00008696 File Offset: 0x00006896
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00008684 File Offset: 0x00006884
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
			set
			{
				if (this._isHidden != value)
				{
					this._isHidden = value;
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000869E File Offset: 0x0000689E
		public BannerTableauTextureProvider()
		{
			this._bannerTableau = new BannerTableau();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000086B1 File Offset: 0x000068B1
		public override void Clear(bool clearNextFrame)
		{
			this._bannerTableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000086C8 File Offset: 0x000068C8
		private void CheckTexture()
		{
			if (this._texture != this._bannerTableau.Texture)
			{
				this._texture = this._bannerTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture engineTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(engineTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000872C File Offset: 0x0000692C
		protected override TaleWorlds.TwoDimension.Texture OnGetTextureForRender(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000873A File Offset: 0x0000693A
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._bannerTableau.SetTargetSize(width, height);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008751 File Offset: 0x00006951
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._bannerTableau.OnTick(dt);
		}

		// Token: 0x040000AE RID: 174
		private BannerTableau _bannerTableau;

		// Token: 0x040000AF RID: 175
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000B0 RID: 176
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x040000B1 RID: 177
		private bool _isHidden;
	}
}
