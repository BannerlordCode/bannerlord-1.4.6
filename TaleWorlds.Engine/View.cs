using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200009B RID: 155
	[EngineClass("rglView")]
	public abstract class View : NativeObject
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x0000F76C File Offset: 0x0000D96C
		internal View(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0000F77B File Offset: 0x0000D97B
		public void SetScale(Vec2 scale)
		{
			EngineApplicationInterface.IView.SetScale(base.Pointer, scale.x, scale.y);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0000F799 File Offset: 0x0000D999
		public void SetOffset(Vec2 offset)
		{
			EngineApplicationInterface.IView.SetOffset(base.Pointer, offset.x, offset.y);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0000F7B7 File Offset: 0x0000D9B7
		public void SetRenderOrder(int value)
		{
			EngineApplicationInterface.IView.SetRenderOrder(base.Pointer, value);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0000F7CA File Offset: 0x0000D9CA
		public void SetRenderOption(View.ViewRenderOptions optionEnum, bool value)
		{
			EngineApplicationInterface.IView.SetRenderOption(base.Pointer, (int)optionEnum, value);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0000F7DE File Offset: 0x0000D9DE
		public void SetRenderTarget(Texture texture)
		{
			EngineApplicationInterface.IView.SetRenderTarget(base.Pointer, texture.Pointer);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0000F7F6 File Offset: 0x0000D9F6
		public void SetDepthTarget(Texture texture)
		{
			EngineApplicationInterface.IView.SetDepthTarget(base.Pointer, texture.Pointer);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0000F80E File Offset: 0x0000DA0E
		public void DontClearBackground()
		{
			this.SetRenderOption(View.ViewRenderOptions.ClearColor, false);
			this.SetRenderOption(View.ViewRenderOptions.ClearDepth, false);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0000F820 File Offset: 0x0000DA20
		public void SetClearColor(uint rgba)
		{
			EngineApplicationInterface.IView.SetClearColor(base.Pointer, rgba);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0000F833 File Offset: 0x0000DA33
		public void SetEnable(bool value)
		{
			EngineApplicationInterface.IView.SetEnable(base.Pointer, value);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0000F846 File Offset: 0x0000DA46
		public void SetRenderOnDemand(bool value)
		{
			EngineApplicationInterface.IView.SetRenderOnDemand(base.Pointer, value);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0000F859 File Offset: 0x0000DA59
		public void SetAutoDepthTargetCreation(bool value)
		{
			EngineApplicationInterface.IView.SetAutoDepthTargetCreation(base.Pointer, value);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0000F86C File Offset: 0x0000DA6C
		public void SetSaveFinalResultToDisk(bool value)
		{
			EngineApplicationInterface.IView.SetSaveFinalResultToDisk(base.Pointer, value);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0000F87F File Offset: 0x0000DA7F
		public void SetFileNameToSaveResult(string name)
		{
			EngineApplicationInterface.IView.SetFileNameToSaveResult(base.Pointer, name);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0000F892 File Offset: 0x0000DA92
		public void SetFileTypeToSave(View.TextureSaveFormat format)
		{
			EngineApplicationInterface.IView.SetFileTypeToSave(base.Pointer, (int)format);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0000F8A5 File Offset: 0x0000DAA5
		public void SetFilePathToSaveResult(string name)
		{
			EngineApplicationInterface.IView.SetFilePathToSaveResult(base.Pointer, name);
		}

		// Token: 0x020000D6 RID: 214
		public enum TextureSaveFormat
		{
			// Token: 0x04000470 RID: 1136
			TextureTypeUnknown,
			// Token: 0x04000471 RID: 1137
			TextureTypeBmp,
			// Token: 0x04000472 RID: 1138
			TextureTypeJpg,
			// Token: 0x04000473 RID: 1139
			TextureTypePng,
			// Token: 0x04000474 RID: 1140
			TextureTypeDds,
			// Token: 0x04000475 RID: 1141
			TextureTypeTif,
			// Token: 0x04000476 RID: 1142
			TextureTypePsd,
			// Token: 0x04000477 RID: 1143
			TextureTypeRaw
		}

		// Token: 0x020000D7 RID: 215
		public enum PostfxConfig : uint
		{
			// Token: 0x04000479 RID: 1145
			pfx_config_bloom = 1U,
			// Token: 0x0400047A RID: 1146
			pfx_config_sunshafts,
			// Token: 0x0400047B RID: 1147
			pfx_config_motionblur = 4U,
			// Token: 0x0400047C RID: 1148
			pfx_config_dof = 8U,
			// Token: 0x0400047D RID: 1149
			pfx_config_tsao = 16U,
			// Token: 0x0400047E RID: 1150
			pfx_config_fxaa = 64U,
			// Token: 0x0400047F RID: 1151
			pfx_config_smaa = 128U,
			// Token: 0x04000480 RID: 1152
			pfx_config_temporal_smaa = 256U,
			// Token: 0x04000481 RID: 1153
			pfx_config_temporal_resolve = 512U,
			// Token: 0x04000482 RID: 1154
			pfx_config_temporal_filter = 1024U,
			// Token: 0x04000483 RID: 1155
			pfx_config_contour = 2048U,
			// Token: 0x04000484 RID: 1156
			pfx_config_ssr = 4096U,
			// Token: 0x04000485 RID: 1157
			pfx_config_sssss = 8192U,
			// Token: 0x04000486 RID: 1158
			pfx_config_streaks = 16384U,
			// Token: 0x04000487 RID: 1159
			pfx_config_lens_flares = 32768U,
			// Token: 0x04000488 RID: 1160
			pfx_config_chromatic_aberration = 65536U,
			// Token: 0x04000489 RID: 1161
			pfx_config_vignette = 131072U,
			// Token: 0x0400048A RID: 1162
			pfx_config_sharpen = 262144U,
			// Token: 0x0400048B RID: 1163
			pfx_config_grain = 524288U,
			// Token: 0x0400048C RID: 1164
			pfx_config_temporal_shadow = 1048576U,
			// Token: 0x0400048D RID: 1165
			pfx_config_editor_scene = 2097152U,
			// Token: 0x0400048E RID: 1166
			pfx_config_custom1 = 16777216U,
			// Token: 0x0400048F RID: 1167
			pfx_config_custom2 = 33554432U,
			// Token: 0x04000490 RID: 1168
			pfx_config_custom3 = 67108864U,
			// Token: 0x04000491 RID: 1169
			pfx_config_custom4 = 134217728U,
			// Token: 0x04000492 RID: 1170
			pfx_config_hexagon_vignette = 268435456U,
			// Token: 0x04000493 RID: 1171
			pfx_config_screen_rt_injection = 536870912U,
			// Token: 0x04000494 RID: 1172
			pfx_config_high_dof = 1073741824U,
			// Token: 0x04000495 RID: 1173
			pfx_lower_bound = 1U,
			// Token: 0x04000496 RID: 1174
			pfx_upper_bound = 536870912U
		}

		// Token: 0x020000D8 RID: 216
		public enum ViewRenderOptions
		{
			// Token: 0x04000498 RID: 1176
			ClearColor,
			// Token: 0x04000499 RID: 1177
			ClearDepth
		}
	}
}
