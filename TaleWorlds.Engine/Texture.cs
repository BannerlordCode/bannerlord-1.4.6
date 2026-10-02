using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000091 RID: 145
	public sealed class Texture : Resource
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public bool IsReleased { get; private set; }

		// Token: 0x06000CEE RID: 3310 RVA: 0x0000E5D1 File Offset: 0x0000C7D1
		private Texture()
		{
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0000E5D9 File Offset: 0x0000C7D9
		internal Texture(UIntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0000E5E2 File Offset: 0x0000C7E2
		public int Width
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetWidth(base.Pointer);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public int Height
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetHeight(base.Pointer);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0000E606 File Offset: 0x0000C806
		public int MemorySize
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetMemorySize(base.Pointer);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0000E618 File Offset: 0x0000C818
		public bool IsRenderTarget
		{
			get
			{
				return EngineApplicationInterface.ITexture.IsRenderTarget(base.Pointer);
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0000E62A File Offset: 0x0000C82A
		public static Texture CreateTextureFromPath(PlatformFilePath filePath)
		{
			return EngineApplicationInterface.ITexture.CreateTextureFromPath(filePath);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0000E637 File Offset: 0x0000C837
		public void GetPixelData(byte[] bytes)
		{
			EngineApplicationInterface.ITexture.GetPixelData(base.Pointer, bytes);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0000E64A File Offset: 0x0000C84A
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0000E65C File Offset: 0x0000C85C
		public string Name
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetName(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ITexture.SetName(base.Pointer, value);
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0000E66F File Offset: 0x0000C86F
		public void TransformRenderTargetToResource(string name)
		{
			EngineApplicationInterface.ITexture.TransformRenderTargetToResourceTexture(base.Pointer, name);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0000E682 File Offset: 0x0000C882
		public static Texture GetFromResource(string resourceName)
		{
			return EngineApplicationInterface.ITexture.GetFromResource(resourceName);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0000E68F File Offset: 0x0000C88F
		public bool IsLoaded()
		{
			return EngineApplicationInterface.ITexture.IsLoaded(base.Pointer);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0000E6A1 File Offset: 0x0000C8A1
		public void GetSDFBoundingBoxData(ref Vec3 min, ref Vec3 max)
		{
			EngineApplicationInterface.ITexture.GetSDFBoundingBoxData(base.Pointer, ref min, ref max);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0000E6B5 File Offset: 0x0000C8B5
		public static Texture CheckAndGetFromResource(string resourceName)
		{
			return EngineApplicationInterface.ITexture.CheckAndGetFromResource(resourceName);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		public static void ScaleTextureWithRatio(ref int tableauSizeX, ref int tableauSizeY)
		{
			float num = (float)tableauSizeX;
			float num2 = (float)tableauSizeY;
			int num3 = (int)MathF.Log(num, 2f) + 2;
			float num4 = MathF.Pow(2f, (float)num3) / num;
			tableauSizeX = (int)(num * num4);
			tableauSizeY = (int)(num2 * num4);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0000E703 File Offset: 0x0000C903
		public void PreloadTexture(bool blocking)
		{
			EngineApplicationInterface.ITexture.GetCurObject(base.Pointer, blocking);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0000E716 File Offset: 0x0000C916
		public void Release()
		{
			this.IsReleased = true;
			this.RenderTargetComponent.OnTargetReleased();
			base.ManualInvalidate();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0000E730 File Offset: 0x0000C930
		public void ReleaseImmediately()
		{
			this.IsReleased = true;
			this.RenderTargetComponent.OnTargetReleased();
			EngineApplicationInterface.ITexture.Release(base.Pointer);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0000E754 File Offset: 0x0000C954
		public void ReleaseAfterNumberOfFrames(int frameCount)
		{
			this.RenderTargetComponent.OnTargetReleased();
			EngineApplicationInterface.ITexture.ReleaseAfterNumberOfFrames(base.Pointer, frameCount);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0000E772 File Offset: 0x0000C972
		public static Texture LoadTextureFromPath(string fileName, string folder)
		{
			return EngineApplicationInterface.ITexture.LoadTextureFromPath(fileName, folder);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0000E780 File Offset: 0x0000C980
		public static Texture CreateDepthTarget(string name, int width, int height)
		{
			return EngineApplicationInterface.ITexture.CreateDepthTarget(name, width, height);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0000E78F File Offset: 0x0000C98F
		public static Texture CreateFromByteArray(byte[] data, int width, int height)
		{
			return EngineApplicationInterface.ITexture.CreateFromByteArray(data, width, height);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0000E79E File Offset: 0x0000C99E
		public void SaveToFile(string path, bool isRelativePath)
		{
			EngineApplicationInterface.ITexture.SaveToFile(base.Pointer, path, isRelativePath);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0000E7B2 File Offset: 0x0000C9B2
		public void SetTextureAsAlwaysValid()
		{
			EngineApplicationInterface.ITexture.SaveTextureAsAlwaysValid(base.Pointer);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0000E7C4 File Offset: 0x0000C9C4
		public static Texture CreateFromMemory(byte[] data)
		{
			return EngineApplicationInterface.ITexture.CreateFromMemory(data);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0000E7D1 File Offset: 0x0000C9D1
		public static void ReleaseGpuMemories()
		{
			EngineApplicationInterface.ITexture.ReleaseGpuMemories();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0000E7DD File Offset: 0x0000C9DD
		public RenderTargetComponent RenderTargetComponent
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetRenderTargetComponent(base.Pointer);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0000E7EF File Offset: 0x0000C9EF
		public TableauView TableauView
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetTableauView(base.Pointer);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0000E801 File Offset: 0x0000CA01
		public object UserData
		{
			get
			{
				return this.RenderTargetComponent.UserData;
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0000E80E File Offset: 0x0000CA0E
		private void SetTableauView(TableauView tableauView)
		{
			EngineApplicationInterface.ITexture.SetTableauView(base.Pointer, tableauView.Pointer);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0000E828 File Offset: 0x0000CA28
		public static Texture CreateTableauTexture(string name, RenderTargetComponent.TextureUpdateEventHandler eventHandler, object objectRef, int tableauSizeX, int tableauSizeY)
		{
			Texture texture = Texture.CreateRenderTarget(name, tableauSizeX, tableauSizeY, true, true, false, false);
			RenderTargetComponent renderTargetComponent = texture.RenderTargetComponent;
			renderTargetComponent.PaintNeeded += eventHandler;
			renderTargetComponent.UserData = objectRef;
			TableauView tableauView = TableauView.CreateTableauView(name);
			tableauView.SetRenderTarget(texture);
			tableauView.SetAutoDepthTargetCreation(true);
			tableauView.SetSceneUsesSkybox(false);
			tableauView.SetClearColor(4294902015U);
			texture.SetTableauView(tableauView);
			return texture;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0000E885 File Offset: 0x0000CA85
		public static Texture CreateRenderTarget(string name, int width, int height, bool autoMipmaps, bool isTableau, bool createUninitialized = false, bool always_valid = false)
		{
			return EngineApplicationInterface.ITexture.CreateRenderTarget(name, width, height, autoMipmaps, isTableau, createUninitialized, always_valid);
		}
	}
}
