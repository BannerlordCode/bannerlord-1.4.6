using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.MountAndBlade.View.Tableaus.Thumbnails;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders.ImageIdentifiers
{
	// Token: 0x02000027 RID: 39
	public abstract class ImageIdentifierTextureProvider : TextureProvider, IDisposable
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000092BC File Offset: 0x000074BC
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000092C4 File Offset: 0x000074C4
		protected ThumbnailCreationData ThumbnailCreationData { get; set; }

		// Token: 0x06000181 RID: 385 RVA: 0x000092CD File Offset: 0x000074CD
		public ImageIdentifierTextureProvider()
		{
			this._textureRequiresRefreshing = true;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000092DC File Offset: 0x000074DC
		~ImageIdentifierTextureProvider()
		{
			this.OnDisposed();
		}

		// Token: 0x06000183 RID: 387
		protected abstract void OnCreateImageWithId(string id, string additionalArgs);

		// Token: 0x06000184 RID: 388 RVA: 0x00009308 File Offset: 0x00007508
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00009318 File Offset: 0x00007518
		private void ReleaseCache()
		{
			if (this.ThumbnailCreationData == null)
			{
				return;
			}
			if (!this.ThumbnailCreationData.IsProcessed)
			{
				Debug.FailedAssert("Created thumbnail data but trying to release it before its processed", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\TextureProviders\\ImageIdentifiers\\ImageIdentifierTextureProvider.cs", "ReleaseCache", 50);
				return;
			}
			ThumbnailCacheManager.Current.DestroyTexture(this.ThumbnailCreationData);
			this.ThumbnailCreationData = null;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000936A File Offset: 0x0000756A
		public override void Clear(bool clearNextFrame)
		{
			base.Clear(clearNextFrame);
			this._providedTexture = null;
			this._textureRequiresRefreshing = true;
			this.ReleaseCache();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009387 File Offset: 0x00007587
		protected virtual bool GetCanForceCheckTexture()
		{
			return false;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000938A File Offset: 0x0000758A
		protected virtual void OnCheckTexture()
		{
			this.CreateImageWithId(this.ImageId, this.AdditionalArgs);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000939E File Offset: 0x0000759E
		protected override TaleWorlds.TwoDimension.Texture OnGetTextureForRender(TwoDimensionContext twoDimensionContext, string name)
		{
			return this._providedTexture;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000093A6 File Offset: 0x000075A6
		protected void ForceRefreshTextures()
		{
			this._textureRequiresRefreshing = true;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000093B0 File Offset: 0x000075B0
		private void CheckTexture()
		{
			if (this._textureRequiresRefreshing || this.GetCanForceCheckTexture())
			{
				this._texture = null;
				this.ReleaseCache();
				this.OnCheckTexture();
				this._textureRequiresRefreshing = false;
			}
			if (this._handleNewlyCreatedTexture)
			{
				TaleWorlds.Engine.Texture texture = null;
				TaleWorlds.TwoDimension.Texture providedTexture = this._providedTexture;
				EngineTexture engineTexture;
				if ((engineTexture = ((providedTexture != null) ? providedTexture.PlatformTexture : null) as EngineTexture) != null)
				{
					texture = engineTexture.Texture;
				}
				if (this._texture != texture)
				{
					if (this._texture != null)
					{
						EngineTexture engineTexture2 = new EngineTexture(this._texture);
						this._providedTexture = new TaleWorlds.TwoDimension.Texture(engineTexture2);
					}
					else
					{
						this._providedTexture = null;
					}
				}
				this._handleNewlyCreatedTexture = false;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009457 File Offset: 0x00007657
		public void CreateImageWithId(string id, string additionalArgs)
		{
			this.OnCreateImageWithId(id, additionalArgs ?? string.Empty);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000946A File Offset: 0x0000766A
		protected void OnTextureCreated(TaleWorlds.Engine.Texture texture)
		{
			this._texture = texture;
			this._textureRequiresRefreshing = false;
			this._handleNewlyCreatedTexture = true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009481 File Offset: 0x00007681
		protected void OnTextureCreationCancelled()
		{
			this._texture = null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000948A File Offset: 0x0000768A
		private void OnDisposed()
		{
			this.ReleaseCache();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00009492 File Offset: 0x00007692
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000949A File Offset: 0x0000769A
		public bool IsReleased
		{
			get
			{
				return this._isReleased;
			}
			set
			{
				if (this._isReleased != value)
				{
					this._isReleased = value;
					if (this._isReleased)
					{
						this.ReleaseCache();
						this._isReleased = false;
					}
				}
				this._textureRequiresRefreshing = true;
				this._handleNewlyCreatedTexture = true;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000094CF File Offset: 0x000076CF
		void IDisposable.Dispose()
		{
			this.OnDisposed();
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000094D7 File Offset: 0x000076D7
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000094DF File Offset: 0x000076DF
		public bool IsBig
		{
			get
			{
				return this._isBig;
			}
			set
			{
				if (this._isBig != value)
				{
					this._isBig = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000094F8 File Offset: 0x000076F8
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00009500 File Offset: 0x00007700
		public string ImageId
		{
			get
			{
				return this._imageId;
			}
			set
			{
				if (this._imageId != value)
				{
					this._imageId = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000951E File Offset: 0x0000771E
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009526 File Offset: 0x00007726
		public string AdditionalArgs
		{
			get
			{
				return this._additionalArgs;
			}
			set
			{
				if (this._additionalArgs != value)
				{
					this._additionalArgs = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x040000CF RID: 207
		private bool _textureRequiresRefreshing;

		// Token: 0x040000D0 RID: 208
		private bool _handleNewlyCreatedTexture;

		// Token: 0x040000D1 RID: 209
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000D2 RID: 210
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x040000D3 RID: 211
		private string _imageId;

		// Token: 0x040000D4 RID: 212
		private string _additionalArgs;

		// Token: 0x040000D5 RID: 213
		private bool _isBig;

		// Token: 0x040000D6 RID: 214
		private bool _isReleased;
	}
}
