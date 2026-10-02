using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002EA RID: 746
	public class CustomGameUsableMap
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000A4F26 File Offset: 0x000A3126
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000A4F2E File Offset: 0x000A312E
		public string Map { get; private set; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000A4F37 File Offset: 0x000A3137
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x000A4F3F File Offset: 0x000A313F
		public bool IsCompatibleWithAllGameTypes { get; private set; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x000A4F48 File Offset: 0x000A3148
		// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x000A4F50 File Offset: 0x000A3150
		public List<string> CompatibleGameTypes { get; private set; }

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000A4F59 File Offset: 0x000A3159
		public CustomGameUsableMap(string map, bool isCompatibleWithAllGameTypes, List<string> compatibleGameTypes)
		{
			this.Map = map;
			this.IsCompatibleWithAllGameTypes = isCompatibleWithAllGameTypes;
			this.CompatibleGameTypes = compatibleGameTypes;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000A4F78 File Offset: 0x000A3178
		public override bool Equals(object obj)
		{
			CustomGameUsableMap customGameUsableMap;
			if ((customGameUsableMap = obj as CustomGameUsableMap) != null)
			{
				return !(customGameUsableMap.Map != this.Map) && customGameUsableMap.IsCompatibleWithAllGameTypes == this.IsCompatibleWithAllGameTypes && (((this.CompatibleGameTypes == null || this.CompatibleGameTypes.Count == 0) && (customGameUsableMap.CompatibleGameTypes == null || customGameUsableMap.CompatibleGameTypes.Count == 0)) || (this.CompatibleGameTypes != null && this.CompatibleGameTypes.Count != 0 && customGameUsableMap.CompatibleGameTypes != null && customGameUsableMap.CompatibleGameTypes.Count != 0 && this.CompatibleGameTypes.SequenceEqual<string>(customGameUsableMap.CompatibleGameTypes)));
			}
			return base.Equals(obj);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000A5028 File Offset: 0x000A3228
		public override int GetHashCode()
		{
			return (((((this.Map != null) ? this.Map.GetHashCode() : 0) * 397) ^ this.IsCompatibleWithAllGameTypes.GetHashCode()) * 397) ^ ((this.CompatibleGameTypes != null) ? this.CompatibleGameTypes.GetHashCode() : 0);
		}
	}
}
