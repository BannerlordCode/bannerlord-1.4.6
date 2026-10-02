using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x020003A3 RID: 931
	public class ShipVisual : ScriptComponentBehavior
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000D9C36 File Offset: 0x000D7E36
		// (set) Token: 0x060034F2 RID: 13554 RVA: 0x000D9C3E File Offset: 0x000D7E3E
		public int Seed { get; private set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000D9C47 File Offset: 0x000D7E47
		// (set) Token: 0x060034F4 RID: 13556 RVA: 0x000D9C4F File Offset: 0x000D7E4F
		public string CustomSailPatternId { get; private set; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000D9C58 File Offset: 0x000D7E58
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x000D9C60 File Offset: 0x000D7E60
		public List<ScriptComponentBehavior> SailVisuals { get; private set; } = new List<ScriptComponentBehavior>();

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000D9C69 File Offset: 0x000D7E69
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000D9C71 File Offset: 0x000D7E71
		public float Health
		{
			get
			{
				return this._health;
			}
			set
			{
				this._health = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000D9C89 File Offset: 0x000D7E89
		public void Initialize(int seed, string customSailPatternId = "")
		{
			this.Seed = seed;
			this.CustomSailPatternId = customSailPatternId;
		}

		// Token: 0x0400167E RID: 5758
		private float _health = 1f;

		// Token: 0x0400167F RID: 5759
		[TupleElementNames(new string[] { "sailColor1", "sailColor2" })]
		public ValueTuple<uint, uint> SailColors = new ValueTuple<uint, uint>(Colors.White.ToUnsignedInteger(), Colors.White.ToUnsignedInteger());
	}
}
