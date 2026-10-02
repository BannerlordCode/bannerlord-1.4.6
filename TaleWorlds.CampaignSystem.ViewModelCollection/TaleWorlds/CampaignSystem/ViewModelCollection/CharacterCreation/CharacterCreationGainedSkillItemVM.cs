using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000151 RID: 337
	public class CharacterCreationGainedSkillItemVM : ViewModel
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000754DB File Offset: 0x000736DB
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x000754E3 File Offset: 0x000736E3
		public SkillObject SkillObj { get; private set; }

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000754EC File Offset: 0x000736EC
		public CharacterCreationGainedSkillItemVM(SkillObject skill)
		{
			this.FocusPointGainList = new MBBindingList<BoolItemWithActionVM>();
			this.SkillObj = skill;
			this.SkillId = this.SkillObj.StringId;
			this.Skill = new EncyclopediaSkillVM(skill, 0);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00075524 File Offset: 0x00073724
		public void SetValue(int gainedFromOtherStages, int gainedFromCurrentStage)
		{
			this.FocusPointGainList.Clear();
			for (int i = 0; i < gainedFromOtherStages; i++)
			{
				this.FocusPointGainList.Add(new BoolItemWithActionVM(null, false, null));
			}
			for (int j = 0; j < gainedFromCurrentStage; j++)
			{
				this.FocusPointGainList.Add(new BoolItemWithActionVM(null, true, null));
			}
			this.HasIncreasedInCurrentStage = gainedFromCurrentStage > 0;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00075584 File Offset: 0x00073784
		internal void ResetValues()
		{
			this.SetValue(0, 0);
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0007558E File Offset: 0x0007378E
		// (set) Token: 0x06001FF6 RID: 8182 RVA: 0x00075596 File Offset: 0x00073796
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

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x000755B9 File Offset: 0x000737B9
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x000755C1 File Offset: 0x000737C1
		[DataSourceProperty]
		public EncyclopediaSkillVM Skill
		{
			get
			{
				return this._skill;
			}
			set
			{
				if (value != this._skill)
				{
					this._skill = value;
					base.OnPropertyChangedWithValue<EncyclopediaSkillVM>(value, "Skill");
				}
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x000755DF File Offset: 0x000737DF
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x000755E7 File Offset: 0x000737E7
		[DataSourceProperty]
		public bool HasIncreasedInCurrentStage
		{
			get
			{
				return this._hasIncreasedInCurrentStage;
			}
			set
			{
				if (value != this._hasIncreasedInCurrentStage)
				{
					this._hasIncreasedInCurrentStage = value;
					base.OnPropertyChangedWithValue(value, "HasIncreasedInCurrentStage");
				}
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00075605 File Offset: 0x00073805
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x0007560D File Offset: 0x0007380D
		[DataSourceProperty]
		public MBBindingList<BoolItemWithActionVM> FocusPointGainList
		{
			get
			{
				return this._focusPointGainList;
			}
			set
			{
				if (value != this._focusPointGainList)
				{
					this._focusPointGainList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BoolItemWithActionVM>>(value, "FocusPointGainList");
				}
			}
		}

		// Token: 0x04000EE2 RID: 3810
		private string _skillId;

		// Token: 0x04000EE3 RID: 3811
		private EncyclopediaSkillVM _skill;

		// Token: 0x04000EE4 RID: 3812
		private bool _hasIncreasedInCurrentStage;

		// Token: 0x04000EE5 RID: 3813
		private MBBindingList<BoolItemWithActionVM> _focusPointGainList;
	}
}
