using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000F3 RID: 243
	public class EducationGainedAttributeItemVM : ViewModel
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x00056BD8 File Offset: 0x00054DD8
		public EducationGainedAttributeItemVM(CharacterAttribute attributeObj)
		{
			this._attributeObj = attributeObj;
			TextObject nameExtended = this._attributeObj.Name;
			TextObject desc = this._attributeObj.Description;
			this.Hint = new BasicTooltipViewModel(delegate
			{
				GameTexts.SetVariable("STR1", nameExtended);
				GameTexts.SetVariable("STR2", desc);
				return GameTexts.FindText("str_string_newline_string", null).ToString();
			});
			this.SetValue(0, 0);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00056C39 File Offset: 0x00054E39
		internal void ResetValues()
		{
			this.SetValue(0, 0);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00056C44 File Offset: 0x00054E44
		public void SetValue(int gainedFromOtherStages, int gainedFromCurrentStage)
		{
			this.HasIncreasedInCurrentStage = gainedFromCurrentStage > 0;
			GameTexts.SetVariable("LEFT", this._attributeObj.Name);
			GameTexts.SetVariable("RIGHT", gainedFromOtherStages + gainedFromCurrentStage);
			this.NameText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00056C93 File Offset: 0x00054E93
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x00056C9B File Offset: 0x00054E9B
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x00056CB9 File Offset: 0x00054EB9
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x00056CC1 File Offset: 0x00054EC1
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00056CE4 File Offset: 0x00054EE4
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x00056CEC File Offset: 0x00054EEC
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

		// Token: 0x04000A0C RID: 2572
		private readonly CharacterAttribute _attributeObj;

		// Token: 0x04000A0D RID: 2573
		private string _nameText;

		// Token: 0x04000A0E RID: 2574
		private bool _hasIncreasedInCurrentStage;

		// Token: 0x04000A0F RID: 2575
		private BasicTooltipViewModel _hint;
	}
}
