using System;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200032D RID: 813
	[ScriptComponentParams("ship_visual_only", "ShipColorAssigner")]
	public class ColorAssigner : ScriptComponentBehavior
	{
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000B1281 File Offset: 0x000AF481
		public Color ShipColor
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000B1289 File Offset: 0x000AF489
		public Color RamDebrisColor
		{
			get
			{
				return this._ramDebrisColor;
			}
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000B12BA File Offset: 0x000AF4BA
		protected internal override void OnInit()
		{
			base.OnInit();
			this.SetColor(base.GameEntity);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000B12CE File Offset: 0x000AF4CE
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.SetColor(base.GameEntity);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000B12E2 File Offset: 0x000AF4E2
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			if (variableName == "Set Colors" || variableName == "Factor Color")
			{
				this.SetColor(base.GameEntity);
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B130A File Offset: 0x000AF50A
		public void SetColor(WeakGameEntity entity)
		{
			entity.SetColorToAllMeshesWithTagRecursive(this._color.ToUnsignedInteger(), "auto_factor_color");
		}

		// Token: 0x04001229 RID: 4649
		[EditableScriptComponentVariable(true, "Factor Color")]
		private Color _color = Color.White;

		// Token: 0x0400122A RID: 4650
		[EditableScriptComponentVariable(true, "Ram Debris Color")]
		private Color _ramDebrisColor = Color.White;

		// Token: 0x0400122B RID: 4651
		[EditableScriptComponentVariable(true, "Set Colors")]
		private SimpleButton _refreshButton = new SimpleButton();
	}
}
