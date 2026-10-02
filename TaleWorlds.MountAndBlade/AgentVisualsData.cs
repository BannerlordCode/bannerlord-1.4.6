using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C6 RID: 710
	public class AgentVisualsData
	{
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x0009A790 File Offset: 0x00098990
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x0009A798 File Offset: 0x00098998
		public MBActionSet ActionSetData { get; private set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x0009A7A1 File Offset: 0x000989A1
		// (set) Token: 0x060028CA RID: 10442 RVA: 0x0009A7A9 File Offset: 0x000989A9
		public MatrixFrame FrameData { get; private set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x0009A7B2 File Offset: 0x000989B2
		// (set) Token: 0x060028CC RID: 10444 RVA: 0x0009A7BA File Offset: 0x000989BA
		public BodyProperties BodyPropertiesData { get; private set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0009A7C3 File Offset: 0x000989C3
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x0009A7CB File Offset: 0x000989CB
		public Equipment EquipmentData { get; private set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0009A7D4 File Offset: 0x000989D4
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0009A7DC File Offset: 0x000989DC
		public int RightWieldedItemIndexData { get; private set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x0009A7E5 File Offset: 0x000989E5
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x0009A7ED File Offset: 0x000989ED
		public int LeftWieldedItemIndexData { get; private set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x0009A7F6 File Offset: 0x000989F6
		// (set) Token: 0x060028D4 RID: 10452 RVA: 0x0009A7FE File Offset: 0x000989FE
		public SkeletonType SkeletonTypeData { get; private set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x0009A807 File Offset: 0x00098A07
		// (set) Token: 0x060028D6 RID: 10454 RVA: 0x0009A80F File Offset: 0x00098A0F
		public Banner BannerData { get; private set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x0009A818 File Offset: 0x00098A18
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x0009A820 File Offset: 0x00098A20
		public GameEntity CachedWeaponSlot0Entity { get; private set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x0009A829 File Offset: 0x00098A29
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x0009A831 File Offset: 0x00098A31
		public GameEntity CachedWeaponSlot1Entity { get; private set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x0009A83A File Offset: 0x00098A3A
		// (set) Token: 0x060028DC RID: 10460 RVA: 0x0009A842 File Offset: 0x00098A42
		public GameEntity CachedWeaponSlot2Entity { get; private set; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x0009A84B File Offset: 0x00098A4B
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x0009A853 File Offset: 0x00098A53
		public GameEntity CachedWeaponSlot3Entity { get; private set; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x0009A85C File Offset: 0x00098A5C
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x0009A864 File Offset: 0x00098A64
		public GameEntity CachedWeaponSlot4Entity { get; private set; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x0009A86D File Offset: 0x00098A6D
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x0009A875 File Offset: 0x00098A75
		public Scene SceneData { get; private set; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x0009A87E File Offset: 0x00098A7E
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x0009A886 File Offset: 0x00098A86
		public Monster MonsterData { get; private set; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0009A88F File Offset: 0x00098A8F
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x0009A897 File Offset: 0x00098A97
		public bool PrepareImmediatelyData { get; private set; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x0009A8A0 File Offset: 0x00098AA0
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x0009A8A8 File Offset: 0x00098AA8
		public bool UseScaledWeaponsData { get; private set; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x0009A8B1 File Offset: 0x00098AB1
		// (set) Token: 0x060028EA RID: 10474 RVA: 0x0009A8B9 File Offset: 0x00098AB9
		public bool UseTranslucencyData { get; private set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0009A8C2 File Offset: 0x00098AC2
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x0009A8CA File Offset: 0x00098ACA
		public bool UseTesselationData { get; private set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x0009A8D3 File Offset: 0x00098AD3
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x0009A8DB File Offset: 0x00098ADB
		public bool UseMorphAnimsData { get; private set; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x0009A8E4 File Offset: 0x00098AE4
		// (set) Token: 0x060028F0 RID: 10480 RVA: 0x0009A8EC File Offset: 0x00098AEC
		public uint ClothColor1Data { get; private set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x0009A8F5 File Offset: 0x00098AF5
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x0009A8FD File Offset: 0x00098AFD
		public uint ClothColor2Data { get; private set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0009A906 File Offset: 0x00098B06
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x0009A90E File Offset: 0x00098B0E
		public float ScaleData { get; private set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x0009A917 File Offset: 0x00098B17
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x0009A91F File Offset: 0x00098B1F
		public string CharacterObjectStringIdData { get; private set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x0009A928 File Offset: 0x00098B28
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x0009A930 File Offset: 0x00098B30
		public ActionIndexCache ActionCodeData { get; private set; } = ActionIndexCache.act_none;

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x0009A939 File Offset: 0x00098B39
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x0009A941 File Offset: 0x00098B41
		public GameEntity EntityData { get; private set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x0009A94A File Offset: 0x00098B4A
		// (set) Token: 0x060028FC RID: 10492 RVA: 0x0009A952 File Offset: 0x00098B52
		public bool HasClippingPlaneData { get; private set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x0009A95B File Offset: 0x00098B5B
		// (set) Token: 0x060028FE RID: 10494 RVA: 0x0009A963 File Offset: 0x00098B63
		public string MountCreationKeyData { get; private set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x0009A96C File Offset: 0x00098B6C
		// (set) Token: 0x06002900 RID: 10496 RVA: 0x0009A974 File Offset: 0x00098B74
		public bool AddColorRandomnessData { get; private set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x0009A97D File Offset: 0x00098B7D
		// (set) Token: 0x06002902 RID: 10498 RVA: 0x0009A985 File Offset: 0x00098B85
		public int RaceData { get; private set; }

		// Token: 0x06002903 RID: 10499 RVA: 0x0009A990 File Offset: 0x00098B90
		public AgentVisualsData(AgentVisualsData agentVisualsData)
		{
			this.AgentVisuals = agentVisualsData.AgentVisuals;
			this.ActionSetData = agentVisualsData.ActionSetData;
			this.FrameData = agentVisualsData.FrameData;
			this.BodyPropertiesData = agentVisualsData.BodyPropertiesData;
			this.EquipmentData = agentVisualsData.EquipmentData;
			this.RightWieldedItemIndexData = agentVisualsData.RightWieldedItemIndexData;
			this.LeftWieldedItemIndexData = agentVisualsData.LeftWieldedItemIndexData;
			this.SkeletonTypeData = agentVisualsData.SkeletonTypeData;
			this.BannerData = agentVisualsData.BannerData;
			this.CachedWeaponSlot0Entity = agentVisualsData.CachedWeaponSlot0Entity;
			this.CachedWeaponSlot1Entity = agentVisualsData.CachedWeaponSlot1Entity;
			this.CachedWeaponSlot2Entity = agentVisualsData.CachedWeaponSlot2Entity;
			this.CachedWeaponSlot3Entity = agentVisualsData.CachedWeaponSlot3Entity;
			this.CachedWeaponSlot4Entity = agentVisualsData.CachedWeaponSlot4Entity;
			this.SceneData = agentVisualsData.SceneData;
			this.MonsterData = agentVisualsData.MonsterData;
			this.PrepareImmediatelyData = agentVisualsData.PrepareImmediatelyData;
			this.UseScaledWeaponsData = agentVisualsData.UseScaledWeaponsData;
			this.UseTranslucencyData = agentVisualsData.UseTranslucencyData;
			this.UseTesselationData = agentVisualsData.UseTesselationData;
			this.UseMorphAnimsData = agentVisualsData.UseMorphAnimsData;
			this.ClothColor1Data = agentVisualsData.ClothColor1Data;
			this.ClothColor2Data = agentVisualsData.ClothColor2Data;
			this.ScaleData = agentVisualsData.ScaleData;
			this.ActionCodeData = agentVisualsData.ActionCodeData;
			this.EntityData = agentVisualsData.EntityData;
			this.CharacterObjectStringIdData = agentVisualsData.CharacterObjectStringIdData;
			this.HasClippingPlaneData = agentVisualsData.HasClippingPlaneData;
			this.MountCreationKeyData = agentVisualsData.MountCreationKeyData;
			this.AddColorRandomnessData = agentVisualsData.AddColorRandomnessData;
			this.RaceData = agentVisualsData.RaceData;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0009AB22 File Offset: 0x00098D22
		public AgentVisualsData()
		{
			this.ClothColor1Data = uint.MaxValue;
			this.ClothColor2Data = uint.MaxValue;
			this.RightWieldedItemIndexData = -1;
			this.LeftWieldedItemIndexData = -1;
			this.ScaleData = 0f;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0009AB5C File Offset: 0x00098D5C
		public AgentVisualsData Equipment(Equipment equipment)
		{
			this.EquipmentData = equipment;
			return this;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0009AB66 File Offset: 0x00098D66
		public AgentVisualsData BodyProperties(BodyProperties bodyProperties)
		{
			this.BodyPropertiesData = bodyProperties;
			return this;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0009AB70 File Offset: 0x00098D70
		public AgentVisualsData Frame(MatrixFrame frame)
		{
			this.FrameData = frame;
			return this;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0009AB7A File Offset: 0x00098D7A
		public AgentVisualsData ActionSet(MBActionSet actionSet)
		{
			this.ActionSetData = actionSet;
			return this;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0009AB84 File Offset: 0x00098D84
		public AgentVisualsData Scene(Scene scene)
		{
			this.SceneData = scene;
			return this;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0009AB8E File Offset: 0x00098D8E
		public AgentVisualsData Monster(Monster monster)
		{
			this.MonsterData = monster;
			return this;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0009AB98 File Offset: 0x00098D98
		public AgentVisualsData PrepareImmediately(bool prepareImmediately)
		{
			this.PrepareImmediatelyData = prepareImmediately;
			return this;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0009ABA2 File Offset: 0x00098DA2
		public AgentVisualsData UseScaledWeapons(bool useScaledWeapons)
		{
			this.UseScaledWeaponsData = useScaledWeapons;
			return this;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x0009ABAC File Offset: 0x00098DAC
		public AgentVisualsData SkeletonType(SkeletonType skeletonType)
		{
			this.SkeletonTypeData = skeletonType;
			return this;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x0009ABB6 File Offset: 0x00098DB6
		public AgentVisualsData UseMorphAnims(bool useMorphAnims)
		{
			this.UseMorphAnimsData = useMorphAnims;
			return this;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0009ABC0 File Offset: 0x00098DC0
		public AgentVisualsData ClothColor1(uint clothColor1)
		{
			this.ClothColor1Data = clothColor1;
			return this;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0009ABCA File Offset: 0x00098DCA
		public AgentVisualsData ClothColor2(uint clothColor2)
		{
			this.ClothColor2Data = clothColor2;
			return this;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0009ABD4 File Offset: 0x00098DD4
		public AgentVisualsData Banner(Banner banner)
		{
			this.BannerData = banner;
			return this;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0009ABDE File Offset: 0x00098DDE
		public AgentVisualsData Race(int race)
		{
			this.RaceData = race;
			return this;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0009ABE8 File Offset: 0x00098DE8
		public GameEntity GetCachedWeaponEntity(EquipmentIndex slotIndex)
		{
			switch (slotIndex)
			{
			case EquipmentIndex.WeaponItemBeginSlot:
				return this.CachedWeaponSlot0Entity;
			case EquipmentIndex.Weapon1:
				return this.CachedWeaponSlot1Entity;
			case EquipmentIndex.Weapon2:
				return this.CachedWeaponSlot2Entity;
			case EquipmentIndex.Weapon3:
				return this.CachedWeaponSlot3Entity;
			case EquipmentIndex.ExtraWeaponSlot:
				return this.CachedWeaponSlot4Entity;
			default:
				return null;
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0009AC38 File Offset: 0x00098E38
		public AgentVisualsData CachedWeaponEntity(EquipmentIndex slotIndex, GameEntity cachedWeaponEntity)
		{
			switch (slotIndex)
			{
			case EquipmentIndex.WeaponItemBeginSlot:
				this.CachedWeaponSlot0Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.Weapon1:
				this.CachedWeaponSlot1Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.Weapon2:
				this.CachedWeaponSlot2Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.Weapon3:
				this.CachedWeaponSlot3Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.ExtraWeaponSlot:
				this.CachedWeaponSlot4Entity = cachedWeaponEntity;
				break;
			}
			return this;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x0009AC8D File Offset: 0x00098E8D
		public AgentVisualsData Entity(GameEntity entity)
		{
			this.EntityData = entity;
			return this;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0009AC97 File Offset: 0x00098E97
		public AgentVisualsData UseTranslucency(bool useTranslucency)
		{
			this.UseTranslucencyData = useTranslucency;
			return this;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0009ACA1 File Offset: 0x00098EA1
		public AgentVisualsData UseTesselation(bool useTesselation)
		{
			this.UseTesselationData = useTesselation;
			return this;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0009ACAB File Offset: 0x00098EAB
		public AgentVisualsData ActionCode(in ActionIndexCache actionCode)
		{
			this.ActionCodeData = actionCode;
			return this;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0009ACBA File Offset: 0x00098EBA
		public AgentVisualsData RightWieldedItemIndex(int rightWieldedItemIndex)
		{
			this.RightWieldedItemIndexData = rightWieldedItemIndex;
			return this;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x0009ACC4 File Offset: 0x00098EC4
		public AgentVisualsData LeftWieldedItemIndex(int leftWieldedItemIndex)
		{
			this.LeftWieldedItemIndexData = leftWieldedItemIndex;
			return this;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0009ACCE File Offset: 0x00098ECE
		public AgentVisualsData Scale(float scale)
		{
			this.ScaleData = scale;
			return this;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0009ACD8 File Offset: 0x00098ED8
		public AgentVisualsData CharacterObjectStringId(string characterObjectStringId)
		{
			this.CharacterObjectStringIdData = characterObjectStringId;
			return this;
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0009ACE2 File Offset: 0x00098EE2
		public AgentVisualsData HasClippingPlane(bool hasClippingPlane)
		{
			this.HasClippingPlaneData = hasClippingPlane;
			return this;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0009ACEC File Offset: 0x00098EEC
		public AgentVisualsData MountCreationKey(string mountCreationKey)
		{
			this.MountCreationKeyData = mountCreationKey;
			return this;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0009ACF6 File Offset: 0x00098EF6
		public AgentVisualsData AddColorRandomness(bool addColorRandomness)
		{
			this.AddColorRandomnessData = addColorRandomness;
			return this;
		}

		// Token: 0x04000FA9 RID: 4009
		public MBAgentVisuals AgentVisuals;
	}
}
