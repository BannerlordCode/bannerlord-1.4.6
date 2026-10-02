using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x0200009E RID: 158
	public class MBEquipmentRoster : MBObjectBase
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001D872 File Offset: 0x0001BA72
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001D87A File Offset: 0x0001BA7A
		public BasicCultureObject EquipmentCulture { get; private set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001D883 File Offset: 0x0001BA83
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0001D88B File Offset: 0x0001BA8B
		public EquipmentCategories EquipmentCategories { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001D894 File Offset: 0x0001BA94
		public MBReadOnlyList<Equipment> AllEquipments
		{
			get
			{
				if (this._equipments.IsEmpty<Equipment>())
				{
					return new MBList<Equipment>(1) { MBEquipmentRoster.EmptyEquipment };
				}
				return this._equipments;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001D8BB File Offset: 0x0001BABB
		public Equipment DefaultEquipment
		{
			get
			{
				if (!this._equipments.IsEmpty<Equipment>())
				{
					return this._equipments.FirstOrDefault<Equipment>();
				}
				return MBEquipmentRoster.EmptyEquipment;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001D8DB File Offset: 0x0001BADB
		public void Init(MBObjectManager objectManager, XmlNode node)
		{
			if (node.Name == "EquipmentRoster")
			{
				this.InitEquipment(objectManager, node);
				return;
			}
			Debug.FailedAssert("false", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\MBEquipmentRoster.cs", "Init", 78);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001D910 File Offset: 0x0001BB10
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			if (node.Attributes["culture"] != null)
			{
				this.EquipmentCulture = MBObjectManager.Instance.ReadObjectReferenceFromXml<BasicCultureObject>("culture", node);
			}
			if (this.EquipmentCulture == null)
			{
				Debug.Print("EquipmentRoster with id: " + base.StringId + " don't have culture definition, make sure this is intended", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "EquipmentSet")
				{
					this.InitEquipment(objectManager, xmlNode);
				}
				if (xmlNode.Name == "Flags")
				{
					foreach (object obj2 in xmlNode.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj2;
						EquipmentCategories equipmentCategories = (EquipmentCategories)Enum.Parse(typeof(EquipmentCategories), xmlAttribute.Name);
						if (bool.Parse(xmlAttribute.InnerText))
						{
							this.EquipmentCategories |= equipmentCategories;
						}
					}
				}
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001DA74 File Offset: 0x0001BC74
		private void InitEquipment(MBObjectManager objectManager, XmlNode node)
		{
			base.Initialize();
			Equipment.EquipmentType equipmentType = Equipment.EquipmentType.Battle;
			if (node.Attributes["equipmentType"] != null)
			{
				if (!Enum.TryParse<Equipment.EquipmentType>(node.Attributes["equipmentType"].Value, out equipmentType))
				{
					Debug.FailedAssert("This equipment definition is wrong", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\MBEquipmentRoster.cs", "InitEquipment", 128);
				}
			}
			else if (node.Attributes["civilian"] != null && bool.Parse(node.Attributes["civilian"].Value))
			{
				equipmentType = Equipment.EquipmentType.Civilian;
				node.Name == "EquipmentSet";
			}
			Equipment equipment = new Equipment(equipmentType);
			equipment.Deserialize(objectManager, node);
			this._equipments.Add(equipment);
			base.AfterInitialized();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001DB38 File Offset: 0x0001BD38
		public void AddEquipmentRoster(MBEquipmentRoster equipmentRoster, Equipment.EquipmentType equipmentType)
		{
			foreach (Equipment equipment in equipmentRoster._equipments.ToList<Equipment>())
			{
				if ((equipmentType == Equipment.EquipmentType.Stealth && equipment.IsStealth) || (equipmentType == Equipment.EquipmentType.Civilian && equipment.IsCivilian) || (equipmentType == Equipment.EquipmentType.Battle && equipment.IsBattle))
				{
					this._equipments.Add(equipment);
				}
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		public void AddOverriddenEquipments(MBObjectManager objectManager, List<XmlNode> overridenEquipmentSlots)
		{
			List<Equipment> list = this._equipments.ToList<Equipment>();
			this._equipments.Clear();
			foreach (Equipment equipment in list)
			{
				this._equipments.Add(equipment.Clone(false));
			}
			foreach (XmlNode xmlNode in overridenEquipmentSlots)
			{
				foreach (Equipment equipment2 in this._equipments)
				{
					equipment2.DeserializeNode(objectManager, xmlNode);
				}
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		public void OrderEquipments()
		{
			this._equipments = new MBList<Equipment>(this._equipments.OrderByDescending<Equipment, bool>((Equipment eq) => !eq.IsCivilian && !eq.IsStealth));
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001DCD7 File Offset: 0x0001BED7
		public void InitializeDefaultEquipment(string equipmentName)
		{
			if (this._equipments[0] == null)
			{
				this._equipments[0] = new Equipment(Equipment.EquipmentType.Battle);
			}
			this._equipments[0].FillFrom(Game.Current.GetDefaultEquipmentWithName(equipmentName), true);
		}

		// Token: 0x0400050B RID: 1291
		public static readonly Equipment EmptyEquipment = new Equipment(Equipment.EquipmentType.Civilian);

		// Token: 0x0400050C RID: 1292
		private MBList<Equipment> _equipments = new MBList<Equipment>();
	}
}
