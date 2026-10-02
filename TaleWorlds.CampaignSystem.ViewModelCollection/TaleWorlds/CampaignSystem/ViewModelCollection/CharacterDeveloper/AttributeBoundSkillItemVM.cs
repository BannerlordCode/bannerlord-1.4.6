using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000141 RID: 321
	public class AttributeBoundSkillItemVM : ViewModel
	{
		// Token: 0x06001E5A RID: 7770 RVA: 0x00070534 File Offset: 0x0006E734
		public AttributeBoundSkillItemVM(SkillObject skill)
		{
			this.Name = skill.Name.ToString();
			this.SkillId = skill.StringId;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x00070559 File Offset: 0x0006E759
		// (set) Token: 0x06001E5C RID: 7772 RVA: 0x00070561 File Offset: 0x0006E761
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00070584 File Offset: 0x0006E784
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x0007058C File Offset: 0x0006E78C
		[DataSourceProperty]
		public string SkillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				if (value != this._skillId)
				{
					this._skillId = value;
					base.OnPropertyChangedWithValue<string>(value, "SkillId");
				}
			}
		}

		// Token: 0x04000E32 RID: 3634
		private string _name;

		// Token: 0x04000E33 RID: 3635
		private string _skillId;
	}
}
