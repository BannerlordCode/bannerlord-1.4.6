using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000F1 RID: 241
	public class EducationGainGroupItemVM : ViewModel
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x000568A4 File Offset: 0x00054AA4
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x000568AC File Offset: 0x00054AAC
		public CharacterAttribute AttributeObj { get; private set; }

		// Token: 0x060015FF RID: 5631 RVA: 0x000568B8 File Offset: 0x00054AB8
		public EducationGainGroupItemVM(CharacterAttribute attributeObj)
		{
			this.AttributeObj = attributeObj;
			this.Skills = new MBBindingList<EducationGainedSkillItemVM>();
			this.Attribute = new EducationGainedAttributeItemVM(this.AttributeObj);
			List<SkillObject> list = TaleWorlds.CampaignSystem.Extensions.Skills.All.ToList<SkillObject>();
			list.Sort(CampaignUIHelper.SkillObjectComparerInstance);
			using (List<SkillObject>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SkillObject skill = enumerator.Current;
					if (!CampaignUIHelper.GetIsNavalSkill(skill) && skill.Attributes.FirstOrDefault<CharacterAttribute>() == this.AttributeObj && !this.Skills.Any<EducationGainedSkillItemVM>((EducationGainedSkillItemVM s) => s.SkillObj == skill))
					{
						this.Skills.Add(new EducationGainedSkillItemVM(skill));
					}
				}
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0005699C File Offset: 0x00054B9C
		public void ResetValues()
		{
			this.Attribute.ResetValues();
			this.Skills.ApplyActionOnAllItems(delegate(EducationGainedSkillItemVM s)
			{
				s.ResetValues();
			});
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x000569D3 File Offset: 0x00054BD3
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x000569DB File Offset: 0x00054BDB
		[DataSourceProperty]
		public MBBindingList<EducationGainedSkillItemVM> Skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				if (value != this._skills)
				{
					this._skills = value;
					base.OnPropertyChangedWithValue<MBBindingList<EducationGainedSkillItemVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x000569F9 File Offset: 0x00054BF9
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x00056A01 File Offset: 0x00054C01
		[DataSourceProperty]
		public EducationGainedAttributeItemVM Attribute
		{
			get
			{
				return this._attribute;
			}
			set
			{
				if (value != this._attribute)
				{
					this._attribute = value;
					base.OnPropertyChangedWithValue<EducationGainedAttributeItemVM>(value, "Attribute");
				}
			}
		}

		// Token: 0x04000A03 RID: 2563
		private MBBindingList<EducationGainedSkillItemVM> _skills;

		// Token: 0x04000A04 RID: 2564
		private EducationGainedAttributeItemVM _attribute;
	}
}
