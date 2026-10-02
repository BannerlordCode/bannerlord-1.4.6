using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x0200001F RID: 31
	public class CharacterTableauTextureProvider : TextureProvider
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000886D File Offset: 0x00006A6D
		public float CustomAnimationProgressRatio
		{
			get
			{
				return this._characterTableau.GetCustomAnimationProgressRatio();
			}
		}

		// Token: 0x1700002B RID: 43
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000887A File Offset: 0x00006A7A
		public string BannerCodeText
		{
			set
			{
				this._characterTableau.SetBannerCode(value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00008888 File Offset: 0x00006A88
		public string BodyProperties
		{
			set
			{
				this._characterTableau.SetBodyProperties(value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00008896 File Offset: 0x00006A96
		public int StanceIndex
		{
			set
			{
				this._characterTableau.SetStanceIndex(value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000088A4 File Offset: 0x00006AA4
		public bool IsFemale
		{
			set
			{
				this._characterTableau.SetIsFemale(value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000088B2 File Offset: 0x00006AB2
		public int Race
		{
			set
			{
				this._characterTableau.SetRace(value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000088C0 File Offset: 0x00006AC0
		public bool IsBannerShownInBackground
		{
			set
			{
				this._characterTableau.SetIsBannerShownInBackground(value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000088CE File Offset: 0x00006ACE
		public bool IsEquipmentAnimActive
		{
			set
			{
				this._characterTableau.SetIsEquipmentAnimActive(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000088DC File Offset: 0x00006ADC
		public string EquipmentCode
		{
			set
			{
				this._characterTableau.SetEquipmentCode(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000088EA File Offset: 0x00006AEA
		public string IdleAction
		{
			set
			{
				this._characterTableau.SetIdleAction(value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000088F8 File Offset: 0x00006AF8
		public string IdleFaceAnim
		{
			set
			{
				this._characterTableau.SetIdleFaceAnim(value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00008906 File Offset: 0x00006B06
		public bool CurrentlyRotating
		{
			set
			{
				this._characterTableau.RotateCharacter(value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00008914 File Offset: 0x00006B14
		public string MountCreationKey
		{
			set
			{
				this._characterTableau.SetMountCreationKey(value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00008922 File Offset: 0x00006B22
		public uint ArmorColor1
		{
			set
			{
				this._characterTableau.SetArmorColor1(value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00008930 File Offset: 0x00006B30
		public uint ArmorColor2
		{
			set
			{
				this._characterTableau.SetArmorColor2(value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000893E File Offset: 0x00006B3E
		public string CharStringId
		{
			set
			{
				this._characterTableau.SetCharStringID(value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000894C File Offset: 0x00006B4C
		public bool TriggerCharacterMountPlacesSwap
		{
			set
			{
				this._characterTableau.TriggerCharacterMountPlacesSwap();
			}
		}

		// Token: 0x1700003B RID: 59
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00008959 File Offset: 0x00006B59
		public float CustomRenderScale
		{
			set
			{
				this._characterTableau.SetCustomRenderScale(value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008967 File Offset: 0x00006B67
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000897A File Offset: 0x00006B7A
		public bool IsPlayingCustomAnimations
		{
			get
			{
				CharacterTableau characterTableau = this._characterTableau;
				return characterTableau != null && characterTableau.IsRunningCustomAnimation;
			}
			set
			{
				if (value)
				{
					this._characterTableau.StartCustomAnimation();
					return;
				}
				this._characterTableau.StopCustomAnimation();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00008996 File Offset: 0x00006B96
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000089A3 File Offset: 0x00006BA3
		public bool ShouldLoopCustomAnimation
		{
			get
			{
				return this._characterTableau.ShouldLoopCustomAnimation;
			}
			set
			{
				this._characterTableau.ShouldLoopCustomAnimation = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000089B1 File Offset: 0x00006BB1
		public int LeftHandWieldedEquipmentIndex
		{
			set
			{
				this._characterTableau.SetLeftHandWieldedEquipmentIndex(value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000089BF File Offset: 0x00006BBF
		public int RightHandWieldedEquipmentIndex
		{
			set
			{
				this._characterTableau.SetRightHandWieldedEquipmentIndex(value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000089CD File Offset: 0x00006BCD
		public float CustomAnimationWaitDuration
		{
			set
			{
				this._characterTableau.CustomAnimationWaitDuration = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000089DB File Offset: 0x00006BDB
		public string CustomAnimation
		{
			set
			{
				this._characterTableau.SetCustomAnimation(value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000089E9 File Offset: 0x00006BE9
		public bool IsTableauEnabled
		{
			set
			{
				this._characterTableau.SetEnabled(value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00008A09 File Offset: 0x00006C09
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000089F7 File Offset: 0x00006BF7
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

		// Token: 0x06000145 RID: 325 RVA: 0x00008A11 File Offset: 0x00006C11
		public CharacterTableauTextureProvider()
		{
			this._characterTableau = new CharacterTableau();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008A24 File Offset: 0x00006C24
		public override void Clear(bool clearNextFrame)
		{
			this._characterTableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008A38 File Offset: 0x00006C38
		private void CheckTexture()
		{
			if (this._texture != this._characterTableau.Texture)
			{
				this._texture = this._characterTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture engineTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(engineTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008A9C File Offset: 0x00006C9C
		protected override TaleWorlds.TwoDimension.Texture OnGetTextureForRender(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008AAA File Offset: 0x00006CAA
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._characterTableau.SetTargetSize(width, height);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008AC1 File Offset: 0x00006CC1
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._characterTableau.OnTick(dt);
		}

		// Token: 0x040000B6 RID: 182
		private CharacterTableau _characterTableau;

		// Token: 0x040000B7 RID: 183
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000B8 RID: 184
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x040000B9 RID: 185
		private bool _isHidden;
	}
}
