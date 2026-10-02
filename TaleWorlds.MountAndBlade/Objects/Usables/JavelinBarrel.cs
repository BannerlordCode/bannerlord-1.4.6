using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Objects.Usables
{
	// Token: 0x020003A9 RID: 937
	public class JavelinBarrel : AmmoBarrelBase
	{
		// Token: 0x06003527 RID: 13607 RVA: 0x000DA9DB File Offset: 0x000D8BDB
		protected override int GetSoundEvent()
		{
			return SoundEvent.GetEventIdFromString(this._pickupSoundEventString);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000DA9E8 File Offset: 0x000D8BE8
		protected override WeaponClass[] GetRequiredWeaponClasses()
		{
			return new WeaponClass[]
			{
				WeaponClass.Arrow,
				WeaponClass.Bolt,
				WeaponClass.SlingStone,
				WeaponClass.Cartridge,
				WeaponClass.ThrowingAxe,
				WeaponClass.ThrowingKnife,
				WeaponClass.Javelin
			};
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000DA9FB File Offset: 0x000D8BFB
		public override TextObject GetDescriptionText(WeakGameEntity gameEntity)
		{
			return new TextObject("{=ybGIoUvT}Ammunition Barrels", null);
		}

		// Token: 0x04001697 RID: 5783
		private readonly string _pickupSoundEventString = "event:/mission/combat/pickup_arrows";
	}
}
