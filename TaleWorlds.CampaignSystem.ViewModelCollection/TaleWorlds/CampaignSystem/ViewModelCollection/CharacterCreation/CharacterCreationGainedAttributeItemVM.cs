using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000152 RID: 338
	public class CharacterCreationGainedAttributeItemVM : ViewModel
	{
		// Token: 0x06001FFD RID: 8189 RVA: 0x0007562C File Offset: 0x0007382C
		public CharacterCreationGainedAttributeItemVM(CharacterAttribute attributeObj)
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

		// Token: 0x06001FFE RID: 8190 RVA: 0x0007568D File Offset: 0x0007388D
		internal void ResetValues()
		{
			this.SetValue(0, 0);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00075698 File Offset: 0x00073898
		public void SetValue(int gainedFromOtherStages, int gainedFromCurrentStage)
		{
			this.HasIncreasedInCurrentStage = gainedFromCurrentStage > 0;
			GameTexts.SetVariable("LEFT", this._attributeObj.Name);
			GameTexts.SetVariable("RIGHT", gainedFromOtherStages + gainedFromCurrentStage);
			this.NameText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x000756E7 File Offset: 0x000738E7
		// (set) Token: 0x06002001 RID: 8193 RVA: 0x000756EF File Offset: 0x000738EF
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

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0007570D File Offset: 0x0007390D
		// (set) Token: 0x06002003 RID: 8195 RVA: 0x00075715 File Offset: 0x00073915
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

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x00075738 File Offset: 0x00073938
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x00075740 File Offset: 0x00073940
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

		// Token: 0x04000EE6 RID: 3814
		private readonly CharacterAttribute _attributeObj;

		// Token: 0x04000EE7 RID: 3815
		private string _nameText;

		// Token: 0x04000EE8 RID: 3816
		private bool _hasIncreasedInCurrentStage;

		// Token: 0x04000EE9 RID: 3817
		private BasicTooltipViewModel _hint;
	}
}
