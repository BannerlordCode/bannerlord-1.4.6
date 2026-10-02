using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000F7 RID: 247
	public class EducationOptionVM : StringItemWithActionVM
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00057977 File Offset: 0x00055B77
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0005797F File Offset: 0x00055B7F
		public string OptionEffect { get; private set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00057988 File Offset: 0x00055B88
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x00057990 File Offset: 0x00055B90
		public string OptionDescription { get; private set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00057999 File Offset: 0x00055B99
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x000579A1 File Offset: 0x00055BA1
		public EducationCampaignBehavior.EducationCharacterProperties[] CharacterProperties { get; private set; }

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x000579AA File Offset: 0x00055BAA
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x000579B2 File Offset: 0x00055BB2
		public string ActionID { get; private set; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x000579BB File Offset: 0x00055BBB
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x000579C3 File Offset: 0x00055BC3
		public ValueTuple<CharacterAttribute, int>[] OptionAttributes { get; private set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x000579CC File Offset: 0x00055BCC
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x000579D4 File Offset: 0x00055BD4
		public ValueTuple<SkillObject, int>[] OptionSkills { get; private set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x000579DD File Offset: 0x00055BDD
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x000579E5 File Offset: 0x00055BE5
		public ValueTuple<SkillObject, int>[] OptionFocusPoints { get; private set; }

		// Token: 0x06001666 RID: 5734 RVA: 0x000579F0 File Offset: 0x00055BF0
		public EducationOptionVM(Action<object> onExecute, string optionId, TextObject optionText, TextObject optionDescription, TextObject optionEffect, bool isSelected, ValueTuple<CharacterAttribute, int>[] optionAttributes, ValueTuple<SkillObject, int>[] optionSkills, ValueTuple<SkillObject, int>[] optionFocusPoints, EducationCampaignBehavior.EducationCharacterProperties[] characterProperties)
			: base(onExecute, optionText.ToString(), optionId)
		{
			this.IsSelected = isSelected;
			this.CharacterProperties = characterProperties;
			this._optionTextObject = optionText;
			this._optionDescriptionObject = optionDescription;
			this._optionEffectObject = optionEffect;
			this.OptionAttributes = optionAttributes;
			this.OptionSkills = optionSkills;
			this.OptionFocusPoints = optionFocusPoints;
			this.RefreshValues();
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00057A50 File Offset: 0x00055C50
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.OptionEffect = this._optionEffectObject.ToString();
			this.OptionDescription = this._optionDescriptionObject.ToString();
			base.ActionText = this._optionTextObject.ToString();
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x00057A8B File Offset: 0x00055C8B
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x00057A93 File Offset: 0x00055C93
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x04000A3C RID: 2620
		private readonly TextObject _optionTextObject;

		// Token: 0x04000A3D RID: 2621
		private readonly TextObject _optionDescriptionObject;

		// Token: 0x04000A3E RID: 2622
		private readonly TextObject _optionEffectObject;

		// Token: 0x04000A3F RID: 2623
		private bool _isSelected;
	}
}
