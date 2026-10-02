using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;

namespace SandBox.View.Map
{
	// Token: 0x0200005F RID: 95
	public class SnowAndRainTextureDefiner : ScriptComponentBehavior
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0001DE98 File Offset: 0x0001C098
		protected override void OnInit()
		{
			this.SetDataToScene();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		protected override void OnTerrainReload(int step)
		{
			if (step == 1)
			{
				this.SetDataToScene();
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		protected override void OnEditorInit()
		{
			if (base.GameEntity.Scene.ContainsTerrain)
			{
				base.GameEntity.Scene.SetDynamicSnowTexture(this.SnowAndRainTexture);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		protected override void OnEditorVariableChanged(string variableName)
		{
			if (variableName == "SnowAndRainTexture" && base.GameEntity.Scene.ContainsTerrain)
			{
				base.GameEntity.Scene.SetDynamicSnowTexture(this.SnowAndRainTexture);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001DF30 File Offset: 0x0001C130
		private void SetDataToScene()
		{
			if (this.SnowAndRainTexture != null)
			{
				((MapScene)Campaign.Current.MapSceneWrapper).SetSnowAndRainDataWithDimension(this.SnowAndRainTexture, this.WeatherNodeGridWidthAndHeight);
			}
		}

		// Token: 0x040001E8 RID: 488
		[EditorVisibleScriptComponentVariable(true)]
		public Texture SnowAndRainTexture;

		// Token: 0x040001E9 RID: 489
		[EditorVisibleScriptComponentVariable(true)]
		public int WeatherNodeGridWidthAndHeight;
	}
}
