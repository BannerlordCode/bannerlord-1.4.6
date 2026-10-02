using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Objects.Usables
{
	// Token: 0x020003A6 RID: 934
	public class ArrowBarrel : AmmoBarrelBase
	{
		// Token: 0x06003512 RID: 13586 RVA: 0x000DA2E7 File Offset: 0x000D84E7
		protected override int GetSoundEvent()
		{
			return SoundEvent.GetEventIdFromString(this._pickupSoundEventString);
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000DA2F4 File Offset: 0x000D84F4
		protected override WeaponClass[] GetRequiredWeaponClasses()
		{
			return new WeaponClass[]
			{
				WeaponClass.Arrow,
				WeaponClass.Bolt
			};
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000DA306 File Offset: 0x000D8506
		public override TextObject GetDescriptionText(WeakGameEntity gameEntity)
		{
			return new TextObject("{=bWi4aMO9}Arrow Barrel", null);
		}

		// Token: 0x04001687 RID: 5767
		private readonly string _pickupSoundEventString = "event:/mission/combat/pickup_arrows";
	}
}
