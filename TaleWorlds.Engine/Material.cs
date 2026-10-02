using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005E RID: 94
	public sealed class Material : Resource
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x00008701 File Offset: 0x00006901
		public static Material GetDefaultMaterial()
		{
			return EngineApplicationInterface.IMaterial.GetDefaultMaterial();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000870D File Offset: 0x0000690D
		public static Material GetOutlineMaterial(Mesh mesh)
		{
			return EngineApplicationInterface.IMaterial.GetOutlineMaterial(mesh.GetMaterial().Pointer);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00008724 File Offset: 0x00006924
		public static Material GetDefaultTableauSampleMaterial(bool transparency)
		{
			if (!transparency)
			{
				return Material.GetFromResource("sample_shield_matte");
			}
			return Material.GetFromResource("tableau_with_transparency");
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00008740 File Offset: 0x00006940
		public static Material CreateTableauMaterial(RenderTargetComponent.TextureUpdateEventHandler eventHandler, object objectRef, Material sampleMaterial, int tableauSizeX, int tableauSizeY, bool continuousTableau = false)
		{
			if (sampleMaterial == null)
			{
				sampleMaterial = Material.GetDefaultTableauSampleMaterial(true);
			}
			Material material = sampleMaterial.CreateCopy();
			uint num = (uint)material.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
			ulong shaderFlags = material.GetShaderFlags();
			material.SetShaderFlags(shaderFlags | (ulong)num);
			string text = "";
			Type type = objectRef.GetType();
			MaterialCacheIDGetMethodDelegate materialCacheIDGetMethodDelegate;
			if (!continuousTableau && HasTableauCache.TableauCacheTypes.TryGetValue(type, out materialCacheIDGetMethodDelegate))
			{
				text = materialCacheIDGetMethodDelegate(objectRef);
				text = text.ToLower();
				Texture texture = Texture.CheckAndGetFromResource(text);
				if (texture != null)
				{
					material.SetTexture(Material.MBTextureType.DiffuseMap2, texture);
					return material;
				}
			}
			if (text != "")
			{
				Texture.ScaleTextureWithRatio(ref tableauSizeX, ref tableauSizeY);
			}
			Texture texture2 = Texture.CreateTableauTexture(text, eventHandler, objectRef, tableauSizeX, tableauSizeY);
			if (text != "")
			{
				TableauView tableauView = texture2.TableauView;
				tableauView.SetSaveFinalResultToDisk(true);
				tableauView.SetFileNameToSaveResult(text);
				tableauView.SetFileTypeToSave(View.TextureSaveFormat.TextureTypeDds);
			}
			if (text != "")
			{
				texture2.TransformRenderTargetToResource(text);
			}
			material.SetTexture(Material.MBTextureType.DiffuseMap2, texture2);
			return material;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00008842 File Offset: 0x00006A42
		internal Material(UIntPtr sourceMaterialPointer)
			: base(sourceMaterialPointer)
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000884B File Offset: 0x00006A4B
		public Material CreateCopy()
		{
			return EngineApplicationInterface.IMaterial.CreateCopy(base.Pointer);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000885D File Offset: 0x00006A5D
		public static Material GetFromResource(string materialName)
		{
			return EngineApplicationInterface.IMaterial.GetFromResource(materialName);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000886A File Offset: 0x00006A6A
		public void SetShader(Shader shader)
		{
			EngineApplicationInterface.IMaterial.SetShader(base.Pointer, shader.Pointer);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00008882 File Offset: 0x00006A82
		public Shader GetShader()
		{
			return EngineApplicationInterface.IMaterial.GetShader(base.Pointer);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00008894 File Offset: 0x00006A94
		public ulong GetShaderFlags()
		{
			return EngineApplicationInterface.IMaterial.GetShaderFlags(base.Pointer);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000088A6 File Offset: 0x00006AA6
		public void SetShaderFlags(ulong flagEntry)
		{
			EngineApplicationInterface.IMaterial.SetShaderFlags(base.Pointer, flagEntry);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000088B9 File Offset: 0x00006AB9
		public void SetMeshVectorArgument(float x, float y, float z, float w)
		{
			EngineApplicationInterface.IMaterial.SetMeshVectorArgument(base.Pointer, x, y, z, w);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000088D0 File Offset: 0x00006AD0
		public void SetTexture(Material.MBTextureType textureType, Texture texture)
		{
			EngineApplicationInterface.IMaterial.SetTexture(base.Pointer, (int)textureType, texture.Pointer);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000088E9 File Offset: 0x00006AE9
		public void SetTextureAtSlot(int textureSlot, Texture texture)
		{
			EngineApplicationInterface.IMaterial.SetTextureAtSlot(base.Pointer, textureSlot, texture.Pointer);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00008902 File Offset: 0x00006B02
		public void SetAreaMapScale(float scale)
		{
			EngineApplicationInterface.IMaterial.SetAreaMapScale(base.Pointer, scale);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00008915 File Offset: 0x00006B15
		public void SetEnableSkinning(bool enable)
		{
			EngineApplicationInterface.IMaterial.SetEnableSkinning(base.Pointer, enable);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00008928 File Offset: 0x00006B28
		public bool UsingSkinning()
		{
			return EngineApplicationInterface.IMaterial.UsingSkinning(base.Pointer);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000893A File Offset: 0x00006B3A
		public Texture GetTexture(Material.MBTextureType textureType)
		{
			return EngineApplicationInterface.IMaterial.GetTexture(base.Pointer, (int)textureType);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000894D File Offset: 0x00006B4D
		public Texture GetTextureWithSlot(int textureSlot)
		{
			return EngineApplicationInterface.IMaterial.GetTexture(base.Pointer, textureSlot);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00008960 File Offset: 0x00006B60
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00008972 File Offset: 0x00006B72
		public string Name
		{
			get
			{
				return EngineApplicationInterface.IMaterial.GetName(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMaterial.SetName(base.Pointer, value);
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00008985 File Offset: 0x00006B85
		public static Material GetAlphaMaskTableauMaterial()
		{
			return EngineApplicationInterface.IMaterial.GetFromResource("tableau_with_alpha_mask");
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00008996 File Offset: 0x00006B96
		public Material.MBAlphaBlendMode GetAlphaBlendMode()
		{
			return (Material.MBAlphaBlendMode)EngineApplicationInterface.IMaterial.GetAlphaBlendMode(base.Pointer);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000089A9 File Offset: 0x00006BA9
		public void SetAlphaBlendMode(Material.MBAlphaBlendMode alphaBlendMode)
		{
			EngineApplicationInterface.IMaterial.SetAlphaBlendMode(base.Pointer, (int)alphaBlendMode);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000089BC File Offset: 0x00006BBC
		public void SetAlphaTestValue(float alphaTestValue)
		{
			EngineApplicationInterface.IMaterial.SetAlphaTestValue(base.Pointer, alphaTestValue);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000089CF File Offset: 0x00006BCF
		public float GetAlphaTestValue()
		{
			return EngineApplicationInterface.IMaterial.GetAlphaTestValue(base.Pointer);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000089E1 File Offset: 0x00006BE1
		private bool CheckMaterialShaderFlag(Material.MBMaterialShaderFlags flagEntry)
		{
			return (EngineApplicationInterface.IMaterial.GetShaderFlags(base.Pointer) & (ulong)((long)flagEntry)) > 0UL;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000089FC File Offset: 0x00006BFC
		private void SetMaterialShaderFlag(Material.MBMaterialShaderFlags flagEntry, bool value)
		{
			ulong num = (EngineApplicationInterface.IMaterial.GetShaderFlags(base.Pointer) & (ulong)(~(ulong)((long)flagEntry))) | (ulong)((long)flagEntry & (value ? 255L : 0L));
			EngineApplicationInterface.IMaterial.SetShaderFlags(base.Pointer, num);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00008A40 File Offset: 0x00006C40
		public void AddMaterialShaderFlag(string flagName, bool showErrors)
		{
			EngineApplicationInterface.IMaterial.AddMaterialShaderFlag(base.Pointer, flagName, showErrors);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00008A54 File Offset: 0x00006C54
		public void RemoveMaterialShaderFlag(string flagName)
		{
			EngineApplicationInterface.IMaterial.RemoveMaterialShaderFlag(base.Pointer, flagName);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00008A67 File Offset: 0x00006C67
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x00008A70 File Offset: 0x00006C70
		public bool UsingSpecular
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecular);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecular, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00008A7A File Offset: 0x00006C7A
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00008A83 File Offset: 0x00006C83
		public bool UsingSpecularMap
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularMap);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularMap, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00008A8D File Offset: 0x00006C8D
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00008A96 File Offset: 0x00006C96
		public bool UsingEnvironmentMap
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseEnvironmentMap);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseEnvironmentMap, value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00008AA0 File Offset: 0x00006CA0
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00008AAD File Offset: 0x00006CAD
		public bool UsingSpecularAlpha
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularAlpha);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularAlpha, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00008ABB File Offset: 0x00006CBB
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00008AC5 File Offset: 0x00006CC5
		public bool UsingDynamicLight
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDynamicLight);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDynamicLight, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00008AD0 File Offset: 0x00006CD0
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00008ADA File Offset: 0x00006CDA
		public bool UsingSunLight
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSunLight);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSunLight, value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00008AE5 File Offset: 0x00006CE5
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00008AF2 File Offset: 0x00006CF2
		public bool UsingFresnel
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseFresnel);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseFresnel, value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00008B00 File Offset: 0x00006D00
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00008B0D File Offset: 0x00006D0D
		public bool IsSunShadowReceiver
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.SunShadowReceiver);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.SunShadowReceiver, value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00008B1B File Offset: 0x00006D1B
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00008B28 File Offset: 0x00006D28
		public bool IsDynamicShadowReceiver
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.DynamicShadowReceiver);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.DynamicShadowReceiver, value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00008B36 File Offset: 0x00006D36
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00008B43 File Offset: 0x00006D43
		public bool UsingDiffuseAlphaMap
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDiffuseAlphaMap);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDiffuseAlphaMap, value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00008B51 File Offset: 0x00006D51
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00008B5E File Offset: 0x00006D5E
		public bool UsingParallaxMapping
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxMapping);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxMapping, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00008B6C File Offset: 0x00006D6C
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00008B79 File Offset: 0x00006D79
		public bool UsingParallaxOcclusion
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxOcclusion);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxOcclusion, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00008B87 File Offset: 0x00006D87
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x00008B99 File Offset: 0x00006D99
		public MaterialFlags Flags
		{
			get
			{
				return EngineApplicationInterface.IMaterial.GetFlags(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMaterial.SetFlags(base.Pointer, value);
			}
		}

		// Token: 0x020000C4 RID: 196
		public enum MBTextureType
		{
			// Token: 0x040003EF RID: 1007
			DiffuseMap,
			// Token: 0x040003F0 RID: 1008
			DiffuseMap2,
			// Token: 0x040003F1 RID: 1009
			BumpMap,
			// Token: 0x040003F2 RID: 1010
			EnvironmentMap,
			// Token: 0x040003F3 RID: 1011
			SpecularMap
		}

		// Token: 0x020000C5 RID: 197
		[EngineStruct("rglAlpha_blend_mode", true, "rgl_abm", false)]
		public enum MBAlphaBlendMode : byte
		{
			// Token: 0x040003F5 RID: 1013
			NoAlphaBlend,
			// Token: 0x040003F6 RID: 1014
			Modulate,
			// Token: 0x040003F7 RID: 1015
			AddAlpha,
			// Token: 0x040003F8 RID: 1016
			Multiply,
			// Token: 0x040003F9 RID: 1017
			Add,
			// Token: 0x040003FA RID: 1018
			Max,
			// Token: 0x040003FB RID: 1019
			Factor,
			// Token: 0x040003FC RID: 1020
			AddModulateCombined,
			// Token: 0x040003FD RID: 1021
			NoAlphaBlendNoWrite,
			// Token: 0x040003FE RID: 1022
			ModulateNoWrite,
			// Token: 0x040003FF RID: 1023
			GbufferAlphaBlend,
			// Token: 0x04000400 RID: 1024
			GbufferAlphaBlendWithVtResolve,
			// Token: 0x04000401 RID: 1025
			NoAlphaBlendNoAlphaWrite,
			// Token: 0x04000402 RID: 1026
			Total
		}

		// Token: 0x020000C6 RID: 198
		[Flags]
		private enum MBMaterialShaderFlags
		{
			// Token: 0x04000404 RID: 1028
			UseSpecular = 1,
			// Token: 0x04000405 RID: 1029
			UseSpecularMap = 2,
			// Token: 0x04000406 RID: 1030
			UseHemisphericalAmbient = 4,
			// Token: 0x04000407 RID: 1031
			UseEnvironmentMap = 8,
			// Token: 0x04000408 RID: 1032
			UseDXT5Normal = 16,
			// Token: 0x04000409 RID: 1033
			UseDynamicLight = 32,
			// Token: 0x0400040A RID: 1034
			UseSunLight = 64,
			// Token: 0x0400040B RID: 1035
			UseSpecularAlpha = 128,
			// Token: 0x0400040C RID: 1036
			UseFresnel = 256,
			// Token: 0x0400040D RID: 1037
			SunShadowReceiver = 512,
			// Token: 0x0400040E RID: 1038
			DynamicShadowReceiver = 1024,
			// Token: 0x0400040F RID: 1039
			UseDiffuseAlphaMap = 2048,
			// Token: 0x04000410 RID: 1040
			UseParallaxMapping = 4096,
			// Token: 0x04000411 RID: 1041
			UseParallaxOcclusion = 8192,
			// Token: 0x04000412 RID: 1042
			UseAlphaTestingBit0 = 16384,
			// Token: 0x04000413 RID: 1043
			UseAlphaTestingBit1 = 32768,
			// Token: 0x04000414 RID: 1044
			UseAreaMap = 65536,
			// Token: 0x04000415 RID: 1045
			UseDetailNormalMap = 131072,
			// Token: 0x04000416 RID: 1046
			UseGroundSlopeAlpha = 262144,
			// Token: 0x04000417 RID: 1047
			UseSelfIllumination = 524288,
			// Token: 0x04000418 RID: 1048
			UseColorMapping = 1048576,
			// Token: 0x04000419 RID: 1049
			UseCubicAmbient = 2097152
		}
	}
}
