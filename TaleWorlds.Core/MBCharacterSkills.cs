using System;
using System.Xml;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x0200009C RID: 156
	public class MBCharacterSkills : MBObjectBase
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001D81C File Offset: 0x0001BA1C
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0001D824 File Offset: 0x0001BA24
		public PropertyOwner<SkillObject> Skills { get; private set; }

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001D82D File Offset: 0x0001BA2D
		public MBCharacterSkills()
		{
			this.Skills = new PropertyOwner<SkillObject>();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001D840 File Offset: 0x0001BA40
		public void Init(MBObjectManager objectManager, XmlNode node)
		{
			base.Initialize();
			this.Skills.Deserialize(objectManager, node);
			base.AfterInitialized();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001D85B File Offset: 0x0001BA5B
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.Skills.Deserialize(objectManager, node);
		}
	}
}
