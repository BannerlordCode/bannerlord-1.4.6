using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000F2 RID: 242
	public class EducationGainedSkillItemVM : ViewModel
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00056A1F File Offset: 0x00054C1F
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x00056A27 File Offset: 0x00054C27
		public SkillObject SkillObj { get; private set; }

		// Token: 0x06001607 RID: 5639 RVA: 0x00056A30 File Offset: 0x00054C30
		public EducationGainedSkillItemVM(SkillObject skill)
		{
			this.FocusPointGainList = new MBBindingList<BoolItemWithActionVM>();
			this.SkillObj = skill;
			this.SkillId = this.SkillObj.StringId;
			this.Skill = new EncyclopediaSkillVM(skill, 0);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00056A68 File Offset: 0x00054C68
		public void SetFocusValue(int gainedFromOtherStages, int gainedFromCurrentStage)
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
			this.HasFocusIncreasedInCurrentStage = gainedFromCurrentStage > 0;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00056AC8 File Offset: 0x00054CC8
		public void SetSkillValue(int gaintedFromOtherStages, int gainedFromCurrentStage)
		{
			this.SkillValueInt = gaintedFromOtherStages + gainedFromCurrentStage;
			this.HasSkillValueIncreasedInCurrentStage = gainedFromCurrentStage > 0;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00056ADD File Offset: 0x00054CDD
		internal void ResetValues()
		{
			this.SetFocusValue(0, 0);
			this.SetSkillValue(0, 0);
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00056AEF File Offset: 0x00054CEF
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00056AF7 File Offset: 0x00054CF7
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00056B1A File Offset: 0x00054D1A
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00056B22 File Offset: 0x00054D22
		[DataSourceProperty]
		public int SkillValueInt
		{
			get
			{
				return this._skillValueInt;
			}
			set
			{
				if (value != this._skillValueInt)
				{
					this._skillValueInt = value;
					base.OnPropertyChangedWithValue(value, "SkillValueInt");
				}
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00056B40 File Offset: 0x00054D40
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00056B48 File Offset: 0x00054D48
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

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00056B66 File Offset: 0x00054D66
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00056B6E File Offset: 0x00054D6E
		[DataSourceProperty]
		public bool HasFocusIncreasedInCurrentStage
		{
			get
			{
				return this._hasFocusIncreasedInCurrentStage;
			}
			set
			{
				if (value != this._hasFocusIncreasedInCurrentStage)
				{
					this._hasFocusIncreasedInCurrentStage = value;
					base.OnPropertyChangedWithValue(value, "HasFocusIncreasedInCurrentStage");
				}
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00056B8C File Offset: 0x00054D8C
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x00056B94 File Offset: 0x00054D94
		[DataSourceProperty]
		public bool HasSkillValueIncreasedInCurrentStage
		{
			get
			{
				return this._hasSkillValueIncreasedInCurrentStage;
			}
			set
			{
				if (value != this._hasSkillValueIncreasedInCurrentStage)
				{
					this._hasSkillValueIncreasedInCurrentStage = value;
					base.OnPropertyChangedWithValue(value, "HasSkillValueIncreasedInCurrentStage");
				}
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00056BB2 File Offset: 0x00054DB2
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x00056BBA File Offset: 0x00054DBA
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

		// Token: 0x04000A06 RID: 2566
		private string _skillId;

		// Token: 0x04000A07 RID: 2567
		private EncyclopediaSkillVM _skill;

		// Token: 0x04000A08 RID: 2568
		private bool _hasFocusIncreasedInCurrentStage;

		// Token: 0x04000A09 RID: 2569
		private bool _hasSkillValueIncreasedInCurrentStage;

		// Token: 0x04000A0A RID: 2570
		private int _skillValueInt;

		// Token: 0x04000A0B RID: 2571
		private MBBindingList<BoolItemWithActionVM> _focusPointGainList;
	}
}
