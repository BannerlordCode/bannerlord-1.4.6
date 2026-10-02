using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D6 RID: 726
	public struct MissionWeapon
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060029CC RID: 10700 RVA: 0x0009F12B File Offset: 0x0009D32B
		// (set) Token: 0x060029CD RID: 10701 RVA: 0x0009F133 File Offset: 0x0009D333
		public ItemObject Item { get; private set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x0009F13C File Offset: 0x0009D33C
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x0009F144 File Offset: 0x0009D344
		public ItemModifier ItemModifier { get; private set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060029D0 RID: 10704 RVA: 0x0009F14D File Offset: 0x0009D34D
		public int WeaponsCount
		{
			get
			{
				return this._weapons.Count;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x0009F15A File Offset: 0x0009D35A
		public WeaponComponentData CurrentUsageItem
		{
			get
			{
				if (this._weapons == null || this._weapons.Count == 0)
				{
					return null;
				}
				return this._weapons[this.CurrentUsageIndex];
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x0009F184 File Offset: 0x0009D384
		// (set) Token: 0x060029D3 RID: 10707 RVA: 0x0009F18C File Offset: 0x0009D38C
		public short ReloadPhase { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x0009F198 File Offset: 0x0009D398
		public short ReloadPhaseCount
		{
			get
			{
				short num = 1;
				if (this.CurrentUsageItem != null)
				{
					num = this.CurrentUsageItem.ReloadPhaseCount;
				}
				return num;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x0009F1BC File Offset: 0x0009D3BC
		public bool IsReloading
		{
			get
			{
				return this.ReloadPhase < this.ReloadPhaseCount;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x0009F1CC File Offset: 0x0009D3CC
		// (set) Token: 0x060029D7 RID: 10711 RVA: 0x0009F1D4 File Offset: 0x0009D3D4
		public Banner Banner { get; private set; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x0009F1DD File Offset: 0x0009D3DD
		// (set) Token: 0x060029D9 RID: 10713 RVA: 0x0009F1E5 File Offset: 0x0009D3E5
		public float GlossMultiplier { get; private set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x0009F1EE File Offset: 0x0009D3EE
		public short RawDataForNetwork
		{
			get
			{
				return this._dataValue;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x0009F1F6 File Offset: 0x0009D3F6
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x0009F1FE File Offset: 0x0009D3FE
		public short HitPoints
		{
			get
			{
				return this._dataValue;
			}
			set
			{
				this._dataValue = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x0009F207 File Offset: 0x0009D407
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x0009F20F File Offset: 0x0009D40F
		public short Amount
		{
			get
			{
				return this._dataValue;
			}
			set
			{
				this._dataValue = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x0009F218 File Offset: 0x0009D418
		public short Ammo
		{
			get
			{
				MissionWeapon.MissionSubWeapon ammoWeapon = this._ammoWeapon;
				if (ammoWeapon == null)
				{
					return 0;
				}
				return ammoWeapon.Value._dataValue;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x0009F230 File Offset: 0x0009D430
		public MissionWeapon AmmoWeapon
		{
			get
			{
				MissionWeapon.MissionSubWeapon ammoWeapon = this._ammoWeapon;
				if (ammoWeapon == null)
				{
					return MissionWeapon.Invalid;
				}
				return ammoWeapon.Value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x0009F247 File Offset: 0x0009D447
		public short MaxAmmo
		{
			get
			{
				return this._modifiedMaxDataValue;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x0009F24F File Offset: 0x0009D44F
		public short ModifiedMaxAmount
		{
			get
			{
				return this._modifiedMaxDataValue;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x0009F257 File Offset: 0x0009D457
		public short ModifiedMaxHitPoints
		{
			get
			{
				return this._modifiedMaxDataValue;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x0009F25F File Offset: 0x0009D45F
		public bool IsEmpty
		{
			get
			{
				return this.CurrentUsageItem == null;
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x0009F26C File Offset: 0x0009D46C
		public MissionWeapon(ItemObject item, ItemModifier itemModifier, Banner banner)
		{
			this.Item = item;
			this.ItemModifier = itemModifier;
			this.Banner = banner;
			this.CurrentUsageIndex = 0;
			this._weapons = new List<WeaponComponentData>(1);
			this._modifiedMaxDataValue = 0;
			this._hasAnyConsumableUsage = false;
			if (item != null && item.Weapons != null)
			{
				foreach (WeaponComponentData weaponComponentData in item.Weapons)
				{
					this._weapons.Add(weaponComponentData);
					bool isConsumable = weaponComponentData.IsConsumable;
					if (isConsumable || weaponComponentData.IsRangedWeapon || weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.HasHitPoints))
					{
						this._modifiedMaxDataValue = weaponComponentData.MaxDataValue;
						if (itemModifier != null)
						{
							if (weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.HasHitPoints))
							{
								this._modifiedMaxDataValue = weaponComponentData.GetModifiedMaximumHitPoints(itemModifier);
							}
							else if (isConsumable)
							{
								this._modifiedMaxDataValue = weaponComponentData.GetModifiedStackCount(itemModifier);
							}
						}
					}
					if (isConsumable)
					{
						this._hasAnyConsumableUsage = true;
					}
				}
			}
			this._dataValue = this._modifiedMaxDataValue;
			this.ReloadPhase = 0;
			this._ammoWeapon = null;
			this._attachedWeapons = null;
			this._attachedWeaponFrames = null;
			this.GlossMultiplier = 1f;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0009F3B4 File Offset: 0x0009D5B4
		public MissionWeapon(ItemObject primaryItem, ItemModifier itemModifier, Banner banner, short dataValue)
		{
			this = new MissionWeapon(primaryItem, itemModifier, banner);
			this._dataValue = dataValue;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0009F3C7 File Offset: 0x0009D5C7
		public MissionWeapon(ItemObject primaryItem, ItemModifier itemModifier, Banner banner, short dataValue, short reloadPhase, MissionWeapon? ammoWeapon)
		{
			this = new MissionWeapon(primaryItem, itemModifier, banner, dataValue);
			this.ReloadPhase = reloadPhase;
			this._ammoWeapon = ((ammoWeapon != null) ? new MissionWeapon.MissionSubWeapon(ammoWeapon.Value) : null);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x0009F3FA File Offset: 0x0009D5FA
		public TextObject GetModifiedItemName()
		{
			if (this.ItemModifier == null)
			{
				return this.Item.Name;
			}
			TextObject name = this.ItemModifier.Name;
			name.SetTextVariable("ITEMNAME", this.Item.Name);
			return name;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x0009F432 File Offset: 0x0009D632
		public bool IsEqualTo(MissionWeapon other)
		{
			return this.Item == other.Item;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0009F443 File Offset: 0x0009D643
		public bool IsSameType(MissionWeapon other)
		{
			return this.Item.PrimaryWeapon.WeaponClass == other.Item.PrimaryWeapon.WeaponClass;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0009F468 File Offset: 0x0009D668
		public float GetWeight()
		{
			float num = (this.Item.PrimaryWeapon.IsConsumable ? (this.GetBaseWeight() * (float)this._dataValue) : this.GetBaseWeight());
			MissionWeapon.MissionSubWeapon ammoWeapon = this._ammoWeapon;
			return num + ((ammoWeapon != null) ? ammoWeapon.Value.GetWeight() : 0f);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0009F4BC File Offset: 0x0009D6BC
		private float GetBaseWeight()
		{
			return this.Item.Weight;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0009F4C9 File Offset: 0x0009D6C9
		public WeaponComponentData GetWeaponComponentDataForUsage(int usageIndex)
		{
			return this._weapons[usageIndex];
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0009F4D7 File Offset: 0x0009D6D7
		public int GetGetModifiedArmorForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedArmor(this.ItemModifier);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x0009F4F5 File Offset: 0x0009D6F5
		public int GetModifiedThrustDamageForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedThrustDamage(this.ItemModifier);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0009F513 File Offset: 0x0009D713
		public int GetModifiedSwingDamageForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedSwingDamage(this.ItemModifier);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0009F531 File Offset: 0x0009D731
		public int GetModifiedMissileDamageForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedMissileDamage(this.ItemModifier);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0009F54F File Offset: 0x0009D74F
		public int GetModifiedThrustSpeedForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedThrustSpeed(this.ItemModifier);
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0009F56D File Offset: 0x0009D76D
		public int GetModifiedSwingSpeedForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedSwingSpeed(this.ItemModifier);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0009F58B File Offset: 0x0009D78B
		public int GetModifiedMissileSpeedForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedMissileSpeed(this.ItemModifier);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0009F5A9 File Offset: 0x0009D7A9
		public int GetModifiedMissileSpeedForUsage(int usageIndex)
		{
			return this._weapons[usageIndex].GetModifiedMissileSpeed(this.ItemModifier);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0009F5C2 File Offset: 0x0009D7C2
		public int GetModifiedHandlingForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedHandling(this.ItemModifier);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0009F5E0 File Offset: 0x0009D7E0
		public WeaponData GetWeaponData(bool needBatchedVersionForMeshes)
		{
			if (!this.IsEmpty && this.Item.WeaponComponent != null)
			{
				WeaponComponent weaponComponent = this.Item.WeaponComponent;
				WeaponData weaponData = new WeaponData
				{
					WeaponKind = (int)this.Item.Id.InternalValue,
					ItemHolsterIndices = this.Item.GetItemHolsterIndices(),
					ReloadPhase = this.ReloadPhase,
					Difficulty = this.Item.Difficulty,
					BaseWeight = this.GetBaseWeight(),
					HasFlagAnimation = false,
					WeaponFrame = weaponComponent.PrimaryWeapon.Frame,
					ScaleFactor = this.Item.ScaleFactor,
					TotalInertia = weaponComponent.PrimaryWeapon.TotalInertia,
					CenterOfMass = weaponComponent.PrimaryWeapon.CenterOfMass,
					CenterOfMass3D = weaponComponent.PrimaryWeapon.CenterOfMass3D,
					HolsterPositionShift = this.Item.HolsterPositionShift,
					TrailParticleName = weaponComponent.PrimaryWeapon.TrailParticleName,
					SkeletonName = this.Item.SkeletonName,
					StaticAnimationName = this.Item.StaticAnimationName,
					AmmoOffset = weaponComponent.PrimaryWeapon.AmmoOffset
				};
				string physicsMaterial = weaponComponent.PrimaryWeapon.PhysicsMaterial;
				weaponData.PhysicsMaterialIndex = (string.IsNullOrEmpty(physicsMaterial) ? PhysicsMaterial.InvalidPhysicsMaterial.Index : PhysicsMaterial.GetFromName(physicsMaterial).Index);
				weaponData.FlyingSoundCode = SoundManager.GetEventGlobalIndex(weaponComponent.PrimaryWeapon.FlyingSoundCode);
				weaponData.PassbySoundCode = SoundManager.GetEventGlobalIndex(weaponComponent.PrimaryWeapon.PassbySoundCode);
				weaponData.StickingFrame = weaponComponent.PrimaryWeapon.StickingFrame;
				weaponData.CollisionShape = ((!needBatchedVersionForMeshes || string.IsNullOrEmpty(this.Item.CollisionBodyName)) ? null : PhysicsShape.GetFromResource(this.Item.CollisionBodyName, false));
				weaponData.Shape = ((!needBatchedVersionForMeshes || string.IsNullOrEmpty(this.Item.BodyName)) ? null : PhysicsShape.GetFromResource(this.Item.BodyName, false));
				weaponData.DataValue = this._dataValue;
				weaponData.CurrentUsageIndex = this.CurrentUsageIndex;
				int rangedUsageIndex = this.GetRangedUsageIndex();
				WeaponComponentData weaponComponentData;
				if (this.GetConsumableIfAny(out weaponComponentData))
				{
					weaponData.AirFrictionConstant = ItemObject.GetAirFrictionConstant(weaponComponentData.WeaponClass, weaponComponentData.WeaponFlags);
				}
				else if (rangedUsageIndex >= 0)
				{
					weaponData.AirFrictionConstant = ItemObject.GetAirFrictionConstant(this.GetWeaponComponentDataForUsage(rangedUsageIndex).WeaponClass, this.GetWeaponComponentDataForUsage(rangedUsageIndex).WeaponFlags);
				}
				weaponData.GlossMultiplier = this.GlossMultiplier;
				weaponData.HasLowerHolsterPriority = this.Item.HasLowerHolsterPriority;
				MissionWeapon.OnGetWeaponDataDelegate onGetWeaponDataHandler = MissionWeapon.OnGetWeaponDataHandler;
				if (onGetWeaponDataHandler != null)
				{
					onGetWeaponDataHandler(ref weaponData, this, false, this.Banner, needBatchedVersionForMeshes);
				}
				return weaponData;
			}
			return WeaponData.InvalidWeaponData;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0009F8BC File Offset: 0x0009DABC
		public WeaponStatsData[] GetWeaponStatsData()
		{
			WeaponStatsData[] array = new WeaponStatsData[this._weapons.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetWeaponStatsDataForUsage(i);
			}
			return array;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0009F8F8 File Offset: 0x0009DAF8
		public WeaponStatsData GetWeaponStatsDataForUsage(int usageIndex)
		{
			WeaponStatsData weaponStatsData = default(WeaponStatsData);
			WeaponComponentData weaponComponentData = this._weapons[usageIndex];
			weaponStatsData.WeaponClass = (int)weaponComponentData.WeaponClass;
			weaponStatsData.AmmoClass = (int)weaponComponentData.AmmoClass;
			weaponStatsData.Properties = (uint)this.Item.ItemFlags;
			weaponStatsData.WeaponFlags = (ulong)weaponComponentData.WeaponFlags;
			weaponStatsData.ItemUsageIndex = (string.IsNullOrEmpty(weaponComponentData.ItemUsage) ? (-1) : weaponComponentData.GetItemUsageIndex());
			weaponStatsData.ThrustSpeed = weaponComponentData.GetModifiedThrustSpeed(this.ItemModifier);
			weaponStatsData.SwingSpeed = weaponComponentData.GetModifiedSwingSpeed(this.ItemModifier);
			weaponStatsData.MissileSpeed = weaponComponentData.GetModifiedMissileSpeed(this.ItemModifier);
			weaponStatsData.ShieldArmor = weaponComponentData.GetModifiedArmor(this.ItemModifier);
			weaponStatsData.Accuracy = weaponComponentData.Accuracy;
			weaponStatsData.WeaponLength = weaponComponentData.WeaponLength;
			weaponStatsData.WeaponBalance = weaponComponentData.WeaponBalance;
			weaponStatsData.ThrustDamage = weaponComponentData.GetModifiedThrustDamage(this.ItemModifier);
			weaponStatsData.ThrustDamageType = (int)weaponComponentData.ThrustDamageType;
			weaponStatsData.SwingDamage = weaponComponentData.GetModifiedSwingDamage(this.ItemModifier);
			weaponStatsData.SwingDamageType = (int)weaponComponentData.SwingDamageType;
			weaponStatsData.DefendSpeed = weaponComponentData.GetModifiedHandling(this.ItemModifier);
			weaponStatsData.SweetSpot = weaponComponentData.SweetSpotReach;
			weaponStatsData.MaxDataValue = this._modifiedMaxDataValue;
			weaponStatsData.WeaponFrame = weaponComponentData.Frame;
			weaponStatsData.RotationSpeed = weaponComponentData.RotationSpeed;
			weaponStatsData.ReloadPhaseCount = weaponComponentData.ReloadPhaseCount;
			return weaponStatsData;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0009FA78 File Offset: 0x0009DC78
		public WeaponData GetAmmoWeaponData(bool needBatchedVersion)
		{
			return this.AmmoWeapon.GetWeaponData(needBatchedVersion);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x0009FA94 File Offset: 0x0009DC94
		public WeaponStatsData[] GetAmmoWeaponStatsData()
		{
			return this.AmmoWeapon.GetWeaponStatsData();
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0009FAAF File Offset: 0x0009DCAF
		public int GetAttachedWeaponsCount()
		{
			List<MissionWeapon.MissionSubWeapon> attachedWeapons = this._attachedWeapons;
			if (attachedWeapons == null)
			{
				return 0;
			}
			return attachedWeapons.Count;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0009FAC2 File Offset: 0x0009DCC2
		public MissionWeapon GetAttachedWeapon(int attachmentIndex)
		{
			return this._attachedWeapons[attachmentIndex].Value;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x0009FAD5 File Offset: 0x0009DCD5
		public MatrixFrame GetAttachedWeaponFrame(int attachmentIndex)
		{
			return this._attachedWeaponFrames[attachmentIndex];
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x0009FAE3 File Offset: 0x0009DCE3
		public bool IsShield()
		{
			return this._weapons.Count == 1 && this._weapons[0].IsShield;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0009FB06 File Offset: 0x0009DD06
		public bool IsBanner()
		{
			return this._weapons.Count == 1 && this._weapons[0].WeaponClass == WeaponClass.Banner;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0009FB30 File Offset: 0x0009DD30
		public bool IsAnyAmmo()
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsAmmo)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0009FB8C File Offset: 0x0009DD8C
		public bool HasAnyUsageWithWeaponClass(WeaponClass weaponClass)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.WeaponClass == weaponClass)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x0009FBE8 File Offset: 0x0009DDE8
		public bool HasAnyUsageWithAmmoClass(WeaponClass ammoClass)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.AmmoClass == ammoClass)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x0009FC44 File Offset: 0x0009DE44
		public bool HasAllUsagesWithAnyWeaponFlag(WeaponFlags flags)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.WeaponFlags.HasAnyFlag(flags))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x0009FCA4 File Offset: 0x0009DEA4
		public bool HasAnyUsageWithoutWeaponFlag(WeaponFlags flags)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.WeaponFlags.HasAnyFlag(flags))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0009FD04 File Offset: 0x0009DF04
		public bool HasAnyUsageWithItemUsageSetFlags(ItemObject.ItemUsageSetFlags flags)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (MBItem.GetItemUsageSetFlags(enumerator.Current.ItemUsage).HasAllFlags(flags))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0009FD68 File Offset: 0x0009DF68
		public void GatherInformationFromWeapon(out bool weaponHasMelee, out bool weaponHasShield, out bool weaponHasPolearm, out bool weaponHasNonConsumableRanged, out bool weaponHasThrown, out WeaponClass rangedAmmoClass)
		{
			weaponHasMelee = false;
			weaponHasShield = false;
			weaponHasPolearm = false;
			weaponHasNonConsumableRanged = false;
			weaponHasThrown = false;
			rangedAmmoClass = WeaponClass.Undefined;
			foreach (WeaponComponentData weaponComponentData in this._weapons)
			{
				weaponHasMelee = weaponHasMelee || weaponComponentData.IsMeleeWeapon;
				weaponHasShield = weaponHasShield || weaponComponentData.IsShield;
				weaponHasPolearm = weaponComponentData.IsPolearm;
				if (weaponComponentData.IsRangedWeapon)
				{
					weaponHasThrown = weaponComponentData.IsConsumable;
					weaponHasNonConsumableRanged = !weaponHasThrown;
					rangedAmmoClass = weaponComponentData.AmmoClass;
				}
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0009FE14 File Offset: 0x0009E014
		public bool GetConsumableIfAny(out WeaponComponentData consumableWeapon)
		{
			consumableWeapon = null;
			if (this._hasAnyConsumableUsage)
			{
				foreach (WeaponComponentData weaponComponentData in this._weapons)
				{
					if (weaponComponentData.IsConsumable)
					{
						consumableWeapon = weaponComponentData;
						break;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0009FE7C File Offset: 0x0009E07C
		public bool IsAnyConsumable()
		{
			return this._hasAnyConsumableUsage;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x0009FE84 File Offset: 0x0009E084
		public int GetRangedUsageIndex()
		{
			for (int i = 0; i < this._weapons.Count; i++)
			{
				if (this._weapons[i].IsRangedWeapon)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0009FEC0 File Offset: 0x0009E0C0
		public MissionWeapon Consume(short count)
		{
			this.Amount -= count;
			return new MissionWeapon(this.Item, this.ItemModifier, this.Banner, count, 0, null);
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0009FF00 File Offset: 0x0009E100
		public void ConsumeAmmo(short count)
		{
			if (count > 0)
			{
				MissionWeapon value = this._ammoWeapon.Value;
				value.Amount = count;
				this._ammoWeapon = new MissionWeapon.MissionSubWeapon(value);
				return;
			}
			this._ammoWeapon = null;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0009FF39 File Offset: 0x0009E139
		public void SetAmmo(MissionWeapon ammoWeapon)
		{
			this._ammoWeapon = new MissionWeapon.MissionSubWeapon(ammoWeapon);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0009FF48 File Offset: 0x0009E148
		public void ReloadAmmo(MissionWeapon ammoWeapon, short reloadPhase)
		{
			if (this._ammoWeapon != null && this._ammoWeapon.Value.Amount >= 0)
			{
				ammoWeapon.Amount += this._ammoWeapon.Value.Amount;
			}
			this._ammoWeapon = new MissionWeapon.MissionSubWeapon(ammoWeapon);
			this.ReloadPhase = reloadPhase;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0009FFA8 File Offset: 0x0009E1A8
		public void AttachWeapon(MissionWeapon attachedWeapon, ref MatrixFrame attachFrame)
		{
			if (this._attachedWeapons == null)
			{
				this._attachedWeapons = new List<MissionWeapon.MissionSubWeapon>();
				this._attachedWeaponFrames = new List<MatrixFrame>();
			}
			this._attachedWeapons.Add(new MissionWeapon.MissionSubWeapon(attachedWeapon));
			this._attachedWeaponFrames.Add(attachFrame);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x0009FFF5 File Offset: 0x0009E1F5
		public void RemoveAttachedWeapon(int attachmentIndex)
		{
			this._attachedWeapons.RemoveAt(attachmentIndex);
			this._attachedWeaponFrames.RemoveAt(attachmentIndex);
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000A000F File Offset: 0x0009E20F
		public bool HasEnoughSpaceForAmount(int amount)
		{
			return (int)(this.ModifiedMaxAmount - this.Amount) >= amount;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000A0024 File Offset: 0x0009E224
		public void SetRandomGlossMultiplier(int seed)
		{
			Random random = new Random(seed);
			float num = 1f + (random.NextFloat() * 2f - 1f) * 0.3f;
			this.GlossMultiplier = num;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000A005E File Offset: 0x0009E25E
		public void AddExtraModifiedMaxValue(short extraValue)
		{
			this._modifiedMaxDataValue += extraValue;
		}

		// Token: 0x04001010 RID: 4112
		public const short ReloadPhaseCountMax = 10;

		// Token: 0x04001011 RID: 4113
		public static MissionWeapon.OnGetWeaponDataDelegate OnGetWeaponDataHandler;

		// Token: 0x04001012 RID: 4114
		public static readonly MissionWeapon Invalid = new MissionWeapon(null, null, null);

		// Token: 0x04001015 RID: 4117
		private readonly List<WeaponComponentData> _weapons;

		// Token: 0x04001016 RID: 4118
		public int CurrentUsageIndex;

		// Token: 0x04001017 RID: 4119
		private bool _hasAnyConsumableUsage;

		// Token: 0x04001018 RID: 4120
		private short _dataValue;

		// Token: 0x04001019 RID: 4121
		private short _modifiedMaxDataValue;

		// Token: 0x0400101D RID: 4125
		private MissionWeapon.MissionSubWeapon _ammoWeapon;

		// Token: 0x0400101E RID: 4126
		private List<MissionWeapon.MissionSubWeapon> _attachedWeapons;

		// Token: 0x0400101F RID: 4127
		private List<MatrixFrame> _attachedWeaponFrames;

		// Token: 0x020005B9 RID: 1465
		public struct ImpactSoundModifier
		{
			// Token: 0x04001F08 RID: 7944
			public const string ModifierName = "impactModifier";

			// Token: 0x04001F09 RID: 7945
			public const float None = 0f;

			// Token: 0x04001F0A RID: 7946
			public const float ActiveBlock = 0.1f;

			// Token: 0x04001F0B RID: 7947
			public const float ChamberBlocked = 0.2f;

			// Token: 0x04001F0C RID: 7948
			public const float CrushThrough = 0.3f;
		}

		// Token: 0x020005BA RID: 1466
		private class MissionSubWeapon
		{
			// Token: 0x17000A7E RID: 2686
			// (get) Token: 0x06003E34 RID: 15924 RVA: 0x000F4BB5 File Offset: 0x000F2DB5
			// (set) Token: 0x06003E35 RID: 15925 RVA: 0x000F4BBD File Offset: 0x000F2DBD
			public MissionWeapon Value { get; private set; }

			// Token: 0x06003E36 RID: 15926 RVA: 0x000F4BC6 File Offset: 0x000F2DC6
			public MissionSubWeapon(MissionWeapon subWeapon)
			{
				this.Value = subWeapon;
			}
		}

		// Token: 0x020005BB RID: 1467
		// (Invoke) Token: 0x06003E38 RID: 15928
		public delegate void OnGetWeaponDataDelegate(ref WeaponData weaponData, MissionWeapon weapon, bool isFemale, Banner banner, bool needBatchedVersion);
	}
}
