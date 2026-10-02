using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x0200001D RID: 29
	public class SettlementNameplateEventItemVM : ViewModel
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000C329 File Offset: 0x0000A529
		public SettlementNameplateEventItemVM(SettlementNameplateEventItemVM.SettlementEventType eventType)
		{
			this.EventType = eventType;
			this.Type = (int)eventType;
			this.AdditionalParameters = "";
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C34A File Offset: 0x0000A54A
		public SettlementNameplateEventItemVM(string productionIconId = "")
		{
			this.EventType = SettlementNameplateEventItemVM.SettlementEventType.Production;
			this.Type = (int)this.EventType;
			this.AdditionalParameters = productionIconId;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000C36C File Offset: 0x0000A56C
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000C374 File Offset: 0x0000A574
		[DataSourceProperty]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000C392 File Offset: 0x0000A592
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000C39A File Offset: 0x0000A59A
		[DataSourceProperty]
		public string AdditionalParameters
		{
			get
			{
				return this._additionalParameters;
			}
			set
			{
				if (value != this._additionalParameters)
				{
					this._additionalParameters = value;
					base.OnPropertyChangedWithValue<string>(value, "AdditionalParameters");
				}
			}
		}

		// Token: 0x0400015C RID: 348
		public readonly SettlementNameplateEventItemVM.SettlementEventType EventType;

		// Token: 0x0400015D RID: 349
		private int _type;

		// Token: 0x0400015E RID: 350
		private string _additionalParameters;

		// Token: 0x02000086 RID: 134
		public enum SettlementEventType
		{
			// Token: 0x04000379 RID: 889
			Tournament,
			// Token: 0x0400037A RID: 890
			AvailableIssue,
			// Token: 0x0400037B RID: 891
			ActiveQuest,
			// Token: 0x0400037C RID: 892
			ActiveStoryQuest,
			// Token: 0x0400037D RID: 893
			TrackedIssue,
			// Token: 0x0400037E RID: 894
			TrackedStoryQuest,
			// Token: 0x0400037F RID: 895
			Production
		}
	}
}
