using System;

namespace TaleWorlds.CampaignSystem.Issues
{
	// Token: 0x0200037E RID: 894
	public struct PotentialIssueData
	{
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000D8944 File Offset: 0x000D6B44
		public PotentialIssueData.StartIssueDelegate OnStartIssue { get; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000D894C File Offset: 0x000D6B4C
		public string IssueId { get; }

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000D8954 File Offset: 0x000D6B54
		public Type IssueType { get; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000D895C File Offset: 0x000D6B5C
		public IssueBase.IssueFrequency Frequency { get; }

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000D8964 File Offset: 0x000D6B64
		public object RelatedObject { get; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000D896C File Offset: 0x000D6B6C
		public bool IsValid
		{
			get
			{
				return this.OnStartIssue != null;
			}
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000D8977 File Offset: 0x000D6B77
		public PotentialIssueData(PotentialIssueData.StartIssueDelegate onStartIssue, Type issueType, IssueBase.IssueFrequency frequency, object relatedObject = null)
		{
			this.OnStartIssue = onStartIssue;
			this.IssueId = issueType.Name;
			this.IssueType = issueType;
			this.Frequency = frequency;
			this.RelatedObject = relatedObject;
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000D89A2 File Offset: 0x000D6BA2
		public PotentialIssueData(Type issueType, IssueBase.IssueFrequency frequency)
		{
			this.OnStartIssue = null;
			this.IssueId = issueType.Name;
			this.IssueType = issueType;
			this.Frequency = frequency;
			this.RelatedObject = null;
		}

		// Token: 0x0200076B RID: 1899
		// (Invoke) Token: 0x060060FC RID: 24828
		public delegate IssueBase StartIssueDelegate(in PotentialIssueData pid, Hero issueOwner);
	}
}
