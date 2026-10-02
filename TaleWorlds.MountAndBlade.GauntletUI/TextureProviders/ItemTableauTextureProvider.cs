using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x02000020 RID: 32
	public class ItemTableauTextureProvider : TextureProvider
	{
		// Token: 0x17000044 RID: 68
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00008ADC File Offset: 0x00006CDC
		public string ItemModifierId
		{
			set
			{
				this._itemTableau.SetItemModifierId(value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00008AEA File Offset: 0x00006CEA
		public string StringId
		{
			set
			{
				this._itemTableau.SetStringId(value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public ItemRosterElement Item
		{
			set
			{
				this._itemTableau.SetItem(value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00008B06 File Offset: 0x00006D06
		public int Ammo
		{
			set
			{
				this._itemTableau.SetAmmo(value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00008B14 File Offset: 0x00006D14
		public int AverageUnitCost
		{
			set
			{
				this._itemTableau.SetAverageUnitCost(value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00008B22 File Offset: 0x00006D22
		public string BannerCode
		{
			set
			{
				this._itemTableau.SetBannerCode(value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00008B30 File Offset: 0x00006D30
		public bool CurrentlyRotating
		{
			set
			{
				this._itemTableau.RotateItem(value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00008B3E File Offset: 0x00006D3E
		public float RotateItemVertical
		{
			set
			{
				this._itemTableau.RotateItemVerticalWithAmount(value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00008B4C File Offset: 0x00006D4C
		public float RotateItemHorizontal
		{
			set
			{
				this._itemTableau.RotateItemHorizontalWithAmount(value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00008B5A File Offset: 0x00006D5A
		public float InitialTiltRotation
		{
			set
			{
				this._itemTableau.SetInitialTiltRotation(value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00008B68 File Offset: 0x00006D68
		public float InitialPanRotation
		{
			set
			{
				this._itemTableau.SetInitialPanRotation(value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00008B76 File Offset: 0x00006D76
		public float CurrentZoom
		{
			set
			{
				this._itemTableau.Zoom((double)value);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008B85 File Offset: 0x00006D85
		public ItemTableauTextureProvider()
		{
			this._itemTableau = new ItemTableau();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008B98 File Offset: 0x00006D98
		public override void Clear(bool clearNextFrame)
		{
			this._itemTableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008BAC File Offset: 0x00006DAC
		private void CheckTexture()
		{
			if (this._texture != this._itemTableau.Texture)
			{
				this._texture = this._itemTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture engineTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(engineTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008C10 File Offset: 0x00006E10
		protected override TaleWorlds.TwoDimension.Texture OnGetTextureForRender(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008C1E File Offset: 0x00006E1E
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._itemTableau.SetTargetSize(width, height);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008C35 File Offset: 0x00006E35
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._itemTableau.OnTick(dt);
		}

		// Token: 0x040000BA RID: 186
		private readonly ItemTableau _itemTableau;

		// Token: 0x040000BB RID: 187
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000BC RID: 188
		private TaleWorlds.TwoDimension.Texture _providedTexture;
	}
}
