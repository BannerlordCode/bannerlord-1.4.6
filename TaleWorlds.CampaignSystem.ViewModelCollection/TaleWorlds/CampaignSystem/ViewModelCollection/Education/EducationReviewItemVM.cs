using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000F5 RID: 245
	public class EducationReviewItemVM : ViewModel
	{
		// Token: 0x0600162B RID: 5675 RVA: 0x00056E93 File Offset: 0x00055093
		public void UpdateWith(string gainText)
		{
			this.GainText = gainText;
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x00056E9C File Offset: 0x0005509C
		// (set) Token: 0x0600162D RID: 5677 RVA: 0x00056EA4 File Offset: 0x000550A4
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x00056EC7 File Offset: 0x000550C7
		// (set) Token: 0x0600162F RID: 5679 RVA: 0x00056ECF File Offset: 0x000550CF
		[DataSourceProperty]
		public string GainText
		{
			get
			{
				return this._gainText;
			}
			set
			{
				if (value != this._gainText)
				{
					this._gainText = value;
					base.OnPropertyChangedWithValue<string>(value, "GainText");
				}
			}
		}

		// Token: 0x04000A16 RID: 2582
		private string _title;

		// Token: 0x04000A17 RID: 2583
		private string _gainText;
	}
}
