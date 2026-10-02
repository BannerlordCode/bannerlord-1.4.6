using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000392 RID: 914
	public struct WeaponInfo
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000D8CBF File Offset: 0x000D6EBF
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000D8CC7 File Offset: 0x000D6EC7
		public bool IsValid { get; private set; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000D8CD0 File Offset: 0x000D6ED0
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000D8CD8 File Offset: 0x000D6ED8
		public bool IsMeleeWeapon { get; private set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000D8CE1 File Offset: 0x000D6EE1
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000D8CE9 File Offset: 0x000D6EE9
		public bool IsRangedWeapon { get; private set; }

		// Token: 0x06003484 RID: 13444 RVA: 0x000D8CF2 File Offset: 0x000D6EF2
		public WeaponInfo(bool isValid, bool isMeleeWeapon, bool isRangedWeapon)
		{
			this.IsValid = isValid;
			this.IsMeleeWeapon = isMeleeWeapon;
			this.IsRangedWeapon = isRangedWeapon;
		}
	}
}
