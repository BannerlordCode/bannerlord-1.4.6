using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000150 RID: 336
	public class CharacterCreationGainGroupItemVM : ViewModel
	{
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x00075366 File Offset: 0x00073566
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x0007536E File Offset: 0x0007356E
		public CharacterAttribute AttributeObj { get; private set; }

		// Token: 0x06001FEA RID: 8170 RVA: 0x00075378 File Offset: 0x00073578
		public CharacterCreationGainGroupItemVM(CharacterAttribute attributeObj)
		{
			this.AttributeObj = attributeObj;
			this.Skills = new MBBindingList<CharacterCreationGainedSkillItemVM>();
			this.Attribute = new CharacterCreationGainedAttributeItemVM(this.AttributeObj);
			List<SkillObject> list = TaleWorlds.CampaignSystem.Extensions.Skills.All.ToList<SkillObject>();
			list.Sort(CampaignUIHelper.SkillObjectComparerInstance);
			using (List<SkillObject>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SkillObject skill = enumerator.Current;
					if (!CampaignUIHelper.GetIsNavalSkill(skill) && skill.Attributes.FirstOrDefault<CharacterAttribute>() == attributeObj && !this.Skills.Any<CharacterCreationGainedSkillItemVM>((CharacterCreationGainedSkillItemVM s) => s.SkillObj == skill))
					{
						this.Skills.Add(new CharacterCreationGainedSkillItemVM(skill));
					}
				}
			}
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00075458 File Offset: 0x00073658
		public void ResetValues()
		{
			this.Attribute.ResetValues();
			this.Skills.ApplyActionOnAllItems(delegate(CharacterCreationGainedSkillItemVM s)
			{
				s.ResetValues();
			});
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x0007548F File Offset: 0x0007368F
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x00075497 File Offset: 0x00073697
		[DataSourceProperty]
		public MBBindingList<CharacterCreationGainedSkillItemVM> Skills
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
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationGainedSkillItemVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x000754B5 File Offset: 0x000736B5
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x000754BD File Offset: 0x000736BD
		[DataSourceProperty]
		public CharacterCreationGainedAttributeItemVM Attribute
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
					base.OnPropertyChangedWithValue<CharacterCreationGainedAttributeItemVM>(value, "Attribute");
				}
			}
		}

		// Token: 0x04000EDF RID: 3807
		private MBBindingList<CharacterCreationGainedSkillItemVM> _skills;

		// Token: 0x04000EE0 RID: 3808
		private CharacterCreationGainedAttributeItemVM _attribute;
	}
}
