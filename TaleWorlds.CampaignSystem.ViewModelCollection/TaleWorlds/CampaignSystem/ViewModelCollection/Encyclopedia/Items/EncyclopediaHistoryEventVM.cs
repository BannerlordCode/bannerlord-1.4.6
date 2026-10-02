using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000E7 RID: 231
	public class EncyclopediaHistoryEventVM : EncyclopediaLinkVM
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x0005544F File Offset: 0x0005364F
		public EncyclopediaHistoryEventVM(IEncyclopediaLog log)
		{
			this._log = log;
			this.RefreshValues();
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00055464 File Offset: 0x00053664
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.HistoryEventTimeText = this._log.GameTime.ToString();
			this.HistoryEventText = this._log.GetEncyclopediaText().ToString();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000554AC File Offset: 0x000536AC
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000554BE File Offset: 0x000536BE
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x000554C6 File Offset: 0x000536C6
		[DataSourceProperty]
		public string HistoryEventTimeText
		{
			get
			{
				return this._historyEventTimeText;
			}
			set
			{
				if (value != this._historyEventTimeText)
				{
					this._historyEventTimeText = value;
					base.OnPropertyChangedWithValue<string>(value, "HistoryEventTimeText");
				}
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000554E9 File Offset: 0x000536E9
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x000554F1 File Offset: 0x000536F1
		[DataSourceProperty]
		public string HistoryEventText
		{
			get
			{
				return this._historyEventText;
			}
			set
			{
				if (value != this._historyEventText)
				{
					this._historyEventText = value;
					base.OnPropertyChangedWithValue<string>(value, "HistoryEventText");
				}
			}
		}

		// Token: 0x040009D6 RID: 2518
		private readonly IEncyclopediaLog _log;

		// Token: 0x040009D7 RID: 2519
		private string _historyEventText;

		// Token: 0x040009D8 RID: 2520
		private string _historyEventTimeText;
	}
}
