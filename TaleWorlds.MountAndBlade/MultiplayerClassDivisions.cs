using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000307 RID: 775
	public class MultiplayerClassDivisions
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000A99F7 File Offset: 0x000A7BF7
		// (set) Token: 0x06002C3F RID: 11327 RVA: 0x000A99FE File Offset: 0x000A7BFE
		public static List<MultiplayerClassDivisions.MPHeroClassGroup> MultiplayerHeroClassGroups { get; private set; }

		// Token: 0x06002C40 RID: 11328 RVA: 0x000A9A08 File Offset: 0x000A7C08
		public static IEnumerable<MultiplayerClassDivisions.MPHeroClass> GetMPHeroClasses(BasicCultureObject culture)
		{
			return from x in MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>()
				where x.Culture == culture
				select x;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000A9A3D File Offset: 0x000A7C3D
		public static MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> GetMPHeroClasses()
		{
			return MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>();
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000A9A4C File Offset: 0x000A7C4C
		public static MultiplayerClassDivisions.MPHeroClass GetMPHeroClassForCharacter(BasicCharacterObject character)
		{
			return MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>().FirstOrDefault<MultiplayerClassDivisions.MPHeroClass>((MultiplayerClassDivisions.MPHeroClass x) => x.HeroCharacter == character || x.TroopCharacter == character);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000A9A84 File Offset: 0x000A7C84
		public static List<List<IReadOnlyPerkObject>> GetAllPerksForHeroClass(MultiplayerClassDivisions.MPHeroClass heroClass, string forcedForGameMode = null)
		{
			List<List<IReadOnlyPerkObject>> list = new List<List<IReadOnlyPerkObject>>();
			for (int i = 0; i < 3; i++)
			{
				list.Add(heroClass.GetAllAvailablePerksForListIndex(i, forcedForGameMode).ToList<IReadOnlyPerkObject>());
			}
			return list;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000A9AB8 File Offset: 0x000A7CB8
		public static MultiplayerClassDivisions.MPHeroClass GetMPHeroClassForPeer(MissionPeer peer, bool skipTeamCheck = false)
		{
			Team team = peer.Team;
			if ((!skipTeamCheck && (team == null || team.Side == BattleSideEnum.None)) || (peer.SelectedTroopIndex < 0 && peer.ControlledAgent == null))
			{
				return null;
			}
			if (peer.ControlledAgent != null)
			{
				return MultiplayerClassDivisions.GetMPHeroClassForCharacter(peer.ControlledAgent.Character);
			}
			if (peer.SelectedTroopIndex >= 0)
			{
				return MultiplayerClassDivisions.GetMPHeroClasses(peer.Culture).ToList<MultiplayerClassDivisions.MPHeroClass>()[peer.SelectedTroopIndex];
			}
			Debug.FailedAssert("This should not be seen.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\MultiplayerClassDivisions.cs", "GetMPHeroClassForPeer", 255);
			return null;
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000A9B48 File Offset: 0x000A7D48
		public static TargetIconType GetMPHeroClassForFormation(Formation formation)
		{
			switch (formation.PhysicalClass)
			{
			case FormationClass.Infantry:
				return TargetIconType.Infantry_Light;
			case FormationClass.Ranged:
				return TargetIconType.Archer_Light;
			case FormationClass.Cavalry:
				return TargetIconType.Cavalry_Light;
			default:
				return TargetIconType.HorseArcher_Light;
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000A9B78 File Offset: 0x000A7D78
		public static List<List<IReadOnlyPerkObject>> GetAvailablePerksForPeer(MissionPeer missionPeer)
		{
			if (((missionPeer != null) ? missionPeer.Team : null) != null)
			{
				return MultiplayerClassDivisions.GetAllPerksForHeroClass(MultiplayerClassDivisions.GetMPHeroClassForPeer(missionPeer, false), null);
			}
			return new List<List<IReadOnlyPerkObject>>();
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000A9B9C File Offset: 0x000A7D9C
		public static void Initialize()
		{
			MultiplayerClassDivisions.MultiplayerHeroClassGroups = new List<MultiplayerClassDivisions.MPHeroClassGroup>
			{
				new MultiplayerClassDivisions.MPHeroClassGroup("Infantry"),
				new MultiplayerClassDivisions.MPHeroClassGroup("Ranged"),
				new MultiplayerClassDivisions.MPHeroClassGroup("Cavalry"),
				new MultiplayerClassDivisions.MPHeroClassGroup("HorseArcher")
			};
			MultiplayerClassDivisions.AvailableCultures = from x in MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>().ToArray()
				where x.IsMainCulture
				select x;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000A9C2B File Offset: 0x000A7E2B
		public static void Release()
		{
			MultiplayerClassDivisions.MultiplayerHeroClassGroups.Clear();
			MultiplayerClassDivisions.AvailableCultures = null;
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000A9C3D File Offset: 0x000A7E3D
		private static BasicCharacterObject GetMPCharacter(string stringId)
		{
			return MBObjectManager.Instance.GetObject<BasicCharacterObject>(stringId);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000A9C4C File Offset: 0x000A7E4C
		public static int GetMinimumTroopCost(BasicCultureObject culture = null)
		{
			MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> mpheroClasses = MultiplayerClassDivisions.GetMPHeroClasses();
			if (culture != null)
			{
				return mpheroClasses.Where<MultiplayerClassDivisions.MPHeroClass>((MultiplayerClassDivisions.MPHeroClass c) => c.Culture == culture).Min<MultiplayerClassDivisions.MPHeroClass>((MultiplayerClassDivisions.MPHeroClass troop) => troop.TroopCost);
			}
			return mpheroClasses.Min<MultiplayerClassDivisions.MPHeroClass>((MultiplayerClassDivisions.MPHeroClass troop) => troop.TroopCost);
		}

		// Token: 0x04001180 RID: 4480
		public static IEnumerable<BasicCultureObject> AvailableCultures;

		// Token: 0x020005E3 RID: 1507
		public class MPHeroClass : MBObjectBase
		{
			// Token: 0x17000A87 RID: 2695
			// (get) Token: 0x06003EBF RID: 16063 RVA: 0x000F5B0B File Offset: 0x000F3D0B
			// (set) Token: 0x06003EC0 RID: 16064 RVA: 0x000F5B13 File Offset: 0x000F3D13
			public BasicCharacterObject HeroCharacter { get; private set; }

			// Token: 0x17000A88 RID: 2696
			// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x000F5B1C File Offset: 0x000F3D1C
			// (set) Token: 0x06003EC2 RID: 16066 RVA: 0x000F5B24 File Offset: 0x000F3D24
			public BasicCharacterObject TroopCharacter { get; private set; }

			// Token: 0x17000A89 RID: 2697
			// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x000F5B2D File Offset: 0x000F3D2D
			// (set) Token: 0x06003EC4 RID: 16068 RVA: 0x000F5B35 File Offset: 0x000F3D35
			public BasicCharacterObject BannerBearerCharacter { get; private set; }

			// Token: 0x17000A8A RID: 2698
			// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000F5B3E File Offset: 0x000F3D3E
			// (set) Token: 0x06003EC6 RID: 16070 RVA: 0x000F5B46 File Offset: 0x000F3D46
			public BasicCultureObject Culture { get; private set; }

			// Token: 0x17000A8B RID: 2699
			// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x000F5B4F File Offset: 0x000F3D4F
			// (set) Token: 0x06003EC8 RID: 16072 RVA: 0x000F5B57 File Offset: 0x000F3D57
			public MultiplayerClassDivisions.MPHeroClassGroup ClassGroup { get; private set; }

			// Token: 0x17000A8C RID: 2700
			// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000F5B60 File Offset: 0x000F3D60
			// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000F5B68 File Offset: 0x000F3D68
			public string HeroIdleAnim { get; private set; }

			// Token: 0x17000A8D RID: 2701
			// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000F5B71 File Offset: 0x000F3D71
			// (set) Token: 0x06003ECC RID: 16076 RVA: 0x000F5B79 File Offset: 0x000F3D79
			public string HeroMountIdleAnim { get; private set; }

			// Token: 0x17000A8E RID: 2702
			// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000F5B82 File Offset: 0x000F3D82
			// (set) Token: 0x06003ECE RID: 16078 RVA: 0x000F5B8A File Offset: 0x000F3D8A
			public string TroopIdleAnim { get; private set; }

			// Token: 0x17000A8F RID: 2703
			// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000F5B93 File Offset: 0x000F3D93
			// (set) Token: 0x06003ED0 RID: 16080 RVA: 0x000F5B9B File Offset: 0x000F3D9B
			public string TroopMountIdleAnim { get; private set; }

			// Token: 0x17000A90 RID: 2704
			// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x000F5BA4 File Offset: 0x000F3DA4
			// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x000F5BAC File Offset: 0x000F3DAC
			public int ArmorValue { get; private set; }

			// Token: 0x17000A91 RID: 2705
			// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x000F5BB5 File Offset: 0x000F3DB5
			// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x000F5BBD File Offset: 0x000F3DBD
			public int Health { get; private set; }

			// Token: 0x17000A92 RID: 2706
			// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x000F5BC6 File Offset: 0x000F3DC6
			// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x000F5BCE File Offset: 0x000F3DCE
			public float HeroMovementSpeedMultiplier { get; private set; }

			// Token: 0x17000A93 RID: 2707
			// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000F5BD7 File Offset: 0x000F3DD7
			// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000F5BDF File Offset: 0x000F3DDF
			public float HeroCombatMovementSpeedMultiplier { get; private set; }

			// Token: 0x17000A94 RID: 2708
			// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000F5BE8 File Offset: 0x000F3DE8
			// (set) Token: 0x06003EDA RID: 16090 RVA: 0x000F5BF0 File Offset: 0x000F3DF0
			public float HeroTopSpeedReachDuration { get; private set; }

			// Token: 0x17000A95 RID: 2709
			// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000F5BF9 File Offset: 0x000F3DF9
			// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000F5C01 File Offset: 0x000F3E01
			public float TroopMovementSpeedMultiplier { get; private set; }

			// Token: 0x17000A96 RID: 2710
			// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000F5C0A File Offset: 0x000F3E0A
			// (set) Token: 0x06003EDE RID: 16094 RVA: 0x000F5C12 File Offset: 0x000F3E12
			public float TroopCombatMovementSpeedMultiplier { get; private set; }

			// Token: 0x17000A97 RID: 2711
			// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000F5C1B File Offset: 0x000F3E1B
			// (set) Token: 0x06003EE0 RID: 16096 RVA: 0x000F5C23 File Offset: 0x000F3E23
			public float TroopTopSpeedReachDuration { get; private set; }

			// Token: 0x17000A98 RID: 2712
			// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000F5C2C File Offset: 0x000F3E2C
			// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x000F5C34 File Offset: 0x000F3E34
			public float TroopMultiplier { get; private set; }

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000F5C3D File Offset: 0x000F3E3D
			// (set) Token: 0x06003EE4 RID: 16100 RVA: 0x000F5C45 File Offset: 0x000F3E45
			public int TroopCost { get; private set; }

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000F5C4E File Offset: 0x000F3E4E
			// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x000F5C56 File Offset: 0x000F3E56
			public int TroopCasualCost { get; private set; }

			// Token: 0x17000A9B RID: 2715
			// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000F5C5F File Offset: 0x000F3E5F
			// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x000F5C67 File Offset: 0x000F3E67
			public int TroopBattleCost { get; private set; }

			// Token: 0x17000A9C RID: 2716
			// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000F5C70 File Offset: 0x000F3E70
			// (set) Token: 0x06003EEA RID: 16106 RVA: 0x000F5C78 File Offset: 0x000F3E78
			public int MeleeAI { get; private set; }

			// Token: 0x17000A9D RID: 2717
			// (get) Token: 0x06003EEB RID: 16107 RVA: 0x000F5C81 File Offset: 0x000F3E81
			// (set) Token: 0x06003EEC RID: 16108 RVA: 0x000F5C89 File Offset: 0x000F3E89
			public int RangedAI { get; private set; }

			// Token: 0x17000A9E RID: 2718
			// (get) Token: 0x06003EED RID: 16109 RVA: 0x000F5C92 File Offset: 0x000F3E92
			// (set) Token: 0x06003EEE RID: 16110 RVA: 0x000F5C9A File Offset: 0x000F3E9A
			public TextObject HeroInformation { get; private set; }

			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000F5CA3 File Offset: 0x000F3EA3
			// (set) Token: 0x06003EF0 RID: 16112 RVA: 0x000F5CAB File Offset: 0x000F3EAB
			public TextObject TroopInformation { get; private set; }

			// Token: 0x17000AA0 RID: 2720
			// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000F5CB4 File Offset: 0x000F3EB4
			// (set) Token: 0x06003EF2 RID: 16114 RVA: 0x000F5CBC File Offset: 0x000F3EBC
			public TargetIconType IconType { get; private set; }

			// Token: 0x17000AA1 RID: 2721
			// (get) Token: 0x06003EF3 RID: 16115 RVA: 0x000F5CC5 File Offset: 0x000F3EC5
			public TextObject HeroName
			{
				get
				{
					return this.HeroCharacter.Name;
				}
			}

			// Token: 0x17000AA2 RID: 2722
			// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x000F5CD2 File Offset: 0x000F3ED2
			public TextObject TroopName
			{
				get
				{
					return this.TroopCharacter.Name;
				}
			}

			// Token: 0x06003EF5 RID: 16117 RVA: 0x000F5CDF File Offset: 0x000F3EDF
			public override bool Equals(object obj)
			{
				return obj is MultiplayerClassDivisions.MPHeroClass && ((MultiplayerClassDivisions.MPHeroClass)obj).StringId.Equals(base.StringId);
			}

			// Token: 0x06003EF6 RID: 16118 RVA: 0x000F5D01 File Offset: 0x000F3F01
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06003EF7 RID: 16119 RVA: 0x000F5D0C File Offset: 0x000F3F0C
			public List<IReadOnlyPerkObject> GetAllAvailablePerksForListIndex(int index, string forcedForGameMode = null)
			{
				string text = forcedForGameMode ?? MultiplayerOptions.OptionType.GameType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				List<IReadOnlyPerkObject> list = new List<IReadOnlyPerkObject>();
				foreach (IReadOnlyPerkObject readOnlyPerkObject in this._perks)
				{
					foreach (string text2 in readOnlyPerkObject.GameModes)
					{
						if ((text2.Equals(text, StringComparison.InvariantCultureIgnoreCase) || text2.Equals("all", StringComparison.InvariantCultureIgnoreCase)) && readOnlyPerkObject.PerkListIndex == index)
						{
							list.Add(readOnlyPerkObject);
							break;
						}
					}
				}
				return list;
			}

			// Token: 0x06003EF8 RID: 16120 RVA: 0x000F5DD8 File Offset: 0x000F3FD8
			public override void Deserialize(MBObjectManager objectManager, XmlNode node)
			{
				base.Deserialize(objectManager, node);
				this.HeroCharacter = MultiplayerClassDivisions.GetMPCharacter(node.Attributes["hero"].Value);
				this.TroopCharacter = MultiplayerClassDivisions.GetMPCharacter(node.Attributes["troop"].Value);
				XmlAttribute xmlAttribute = node.Attributes["banner_bearer"];
				string text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
				if (text != null)
				{
					this.BannerBearerCharacter = MultiplayerClassDivisions.GetMPCharacter(text);
				}
				XmlAttribute xmlAttribute2 = node.Attributes["hero_idle_anim"];
				this.HeroIdleAnim = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
				XmlAttribute xmlAttribute3 = node.Attributes["hero_mount_idle_anim"];
				this.HeroMountIdleAnim = ((xmlAttribute3 != null) ? xmlAttribute3.Value : null);
				XmlAttribute xmlAttribute4 = node.Attributes["troop_idle_anim"];
				this.TroopIdleAnim = ((xmlAttribute4 != null) ? xmlAttribute4.Value : null);
				XmlAttribute xmlAttribute5 = node.Attributes["troop_mount_idle_anim"];
				this.TroopMountIdleAnim = ((xmlAttribute5 != null) ? xmlAttribute5.Value : null);
				this.Culture = this.HeroCharacter.Culture;
				this.ClassGroup = new MultiplayerClassDivisions.MPHeroClassGroup(this.HeroCharacter.DefaultFormationClass.GetName());
				this.TroopMultiplier = (float)Convert.ToDouble(node.Attributes["multiplier"].Value);
				this.TroopCost = Convert.ToInt32(node.Attributes["cost"].Value);
				this.ArmorValue = Convert.ToInt32(node.Attributes["armor"].Value);
				XmlAttribute xmlAttribute6 = node.Attributes["casual_cost"];
				XmlAttribute xmlAttribute7 = node.Attributes["battle_cost"];
				this.TroopCasualCost = ((xmlAttribute6 != null) ? Convert.ToInt32(node.Attributes["casual_cost"].Value) : this.TroopCost);
				this.TroopBattleCost = ((xmlAttribute7 != null) ? Convert.ToInt32(node.Attributes["battle_cost"].Value) : this.TroopCost);
				this.Health = 100;
				this.MeleeAI = 50;
				this.RangedAI = 50;
				XmlNode xmlNode = node.Attributes["hitpoints"];
				if (xmlNode != null)
				{
					this.Health = Convert.ToInt32(xmlNode.Value);
				}
				this.HeroMovementSpeedMultiplier = (float)Convert.ToDouble(node.Attributes["movement_speed"].Value);
				this.HeroCombatMovementSpeedMultiplier = (float)Convert.ToDouble(node.Attributes["combat_movement_speed"].Value);
				this.HeroTopSpeedReachDuration = (float)Convert.ToDouble(node.Attributes["acceleration"].Value);
				XmlAttribute xmlAttribute8 = node.Attributes["troop_movement_speed"];
				XmlAttribute xmlAttribute9 = node.Attributes["troop_combat_movement_speed"];
				XmlAttribute xmlAttribute10 = node.Attributes["troop_acceleration"];
				this.TroopMovementSpeedMultiplier = ((xmlAttribute8 != null) ? ((float)Convert.ToDouble(xmlAttribute8.Value)) : this.HeroMovementSpeedMultiplier);
				this.TroopCombatMovementSpeedMultiplier = ((xmlAttribute9 != null) ? ((float)Convert.ToDouble(xmlAttribute9.Value)) : this.HeroCombatMovementSpeedMultiplier);
				this.TroopTopSpeedReachDuration = ((xmlAttribute10 != null) ? ((float)Convert.ToDouble(xmlAttribute10.Value)) : this.HeroTopSpeedReachDuration);
				this.MeleeAI = Convert.ToInt32(node.Attributes["melee_ai"].Value);
				this.RangedAI = Convert.ToInt32(node.Attributes["ranged_ai"].Value);
				TargetIconType targetIconType;
				if (Enum.TryParse<TargetIconType>(node.Attributes["icon"].Value, true, out targetIconType))
				{
					this.IconType = targetIconType;
				}
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.NodeType != XmlNodeType.Comment && xmlNode2.Name == "Perks")
					{
						this._perks = new List<IReadOnlyPerkObject>();
						foreach (object obj2 in xmlNode2.ChildNodes)
						{
							XmlNode xmlNode3 = (XmlNode)obj2;
							if (xmlNode3.NodeType != XmlNodeType.Comment)
							{
								this._perks.Add(MPPerkObject.Deserialize(xmlNode3));
							}
						}
					}
				}
			}

			// Token: 0x06003EF9 RID: 16121 RVA: 0x000F6264 File Offset: 0x000F4464
			public bool IsTroopCharacter(BasicCharacterObject character)
			{
				return this.TroopCharacter == character;
			}

			// Token: 0x04001FB5 RID: 8117
			private List<IReadOnlyPerkObject> _perks = new List<IReadOnlyPerkObject>();
		}

		// Token: 0x020005E4 RID: 1508
		public class MPHeroClassGroup
		{
			// Token: 0x06003EFB RID: 16123 RVA: 0x000F6282 File Offset: 0x000F4482
			public MPHeroClassGroup(string stringId)
			{
				this.StringId = stringId;
				this.Name = GameTexts.FindText("str_troop_type_name", this.StringId);
			}

			// Token: 0x06003EFC RID: 16124 RVA: 0x000F62A7 File Offset: 0x000F44A7
			public override bool Equals(object obj)
			{
				return obj is MultiplayerClassDivisions.MPHeroClassGroup && ((MultiplayerClassDivisions.MPHeroClassGroup)obj).StringId.Equals(this.StringId);
			}

			// Token: 0x06003EFD RID: 16125 RVA: 0x000F62C9 File Offset: 0x000F44C9
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x04001FB6 RID: 8118
			public readonly string StringId;

			// Token: 0x04001FB7 RID: 8119
			public readonly TextObject Name;
		}
	}
}
