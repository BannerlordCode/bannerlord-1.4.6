using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.ClanFinance
{
	// Token: 0x02000133 RID: 307
	public class ClanFinanceCommonAreaItemVM : ClanFinanceIncomeItemBaseVM
	{
		// Token: 0x06001CBA RID: 7354 RVA: 0x0006A770 File Offset: 0x00068970
		public ClanFinanceCommonAreaItemVM(Alley alley, Action<ClanFinanceIncomeItemBaseVM> onSelection, Action onRefresh)
			: base(onSelection, onRefresh)
		{
			base.IncomeTypeAsEnum = IncomeTypes.CommonArea;
			this._alley = alley;
			GameTexts.SetVariable("SETTLEMENT_NAME", alley.Settlement.Name);
			GameTexts.SetVariable("COMMON_AREA_NAME", alley.Name);
			base.Name = GameTexts.FindText("str_clan_finance_common_area", null).ToString();
			base.Income = Campaign.Current.Models.AlleyModel.GetDailyIncomeOfAlley(alley);
			base.Visual = ((alley.Owner.CharacterObject != null) ? new CharacterImageIdentifierVM(CharacterCode.CreateFrom(alley.Owner.CharacterObject)) : new CharacterImageIdentifierVM(null));
			base.IncomeValueText = base.DetermineIncomeText(base.Income);
			this.PopulateActionList();
			this.PopulateStatsList();
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0006A837 File Offset: 0x00068A37
		protected override void PopulateActionList()
		{
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0006A839 File Offset: 0x00068A39
		protected override void PopulateStatsList()
		{
			base.ItemProperties.Add(new SelectableItemPropertyVM("TEST", "TEST", false, null));
		}

		// Token: 0x04000D67 RID: 3431
		private Alley _alley;
	}
}
