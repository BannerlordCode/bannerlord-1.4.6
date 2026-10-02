using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core.ImageIdentifiers;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.BarterSystem.Barterables
{
	// Token: 0x02000488 RID: 1160
	public class NoAttackBarterable : Barterable
	{
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x0017491F File Offset: 0x00172B1F
		public override string StringID
		{
			get
			{
				return "no_attack_barterable";
			}
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00174926 File Offset: 0x00172B26
		public NoAttackBarterable(Hero originalOwner, Hero otherHero, PartyBase ownerParty, PartyBase otherParty, CampaignTime duration)
			: base(originalOwner, ownerParty)
		{
			this._otherFaction = otherParty.MapFaction;
			this._duration = duration;
			this._otherHero = otherHero;
			this._otherParty = otherParty;
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x00174954 File Offset: 0x00172B54
		public override TextObject Name
		{
			get
			{
				TextObject textObject = new TextObject("{=Y3lGJT8H}{PARTY} won't attack {FACTION} for {DURATION} {?DURATION>1}days{?}day{\\?}.", null);
				textObject.SetTextVariable("PARTY", base.OriginalParty.Name);
				textObject.SetTextVariable("FACTION", this._otherFaction.Name);
				textObject.SetTextVariable("DURATION", this._duration.ToDays.ToString());
				return textObject;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x001749BC File Offset: 0x00172BBC
		public override void Apply()
		{
			if (base.OriginalParty == MobileParty.MainParty.Party)
			{
				if (this._otherFaction.NotAttackableByPlayerUntilTime.IsPast)
				{
					this._otherFaction.NotAttackableByPlayerUntilTime = CampaignTime.Now;
				}
				this._otherFaction.NotAttackableByPlayerUntilTime += this._duration;
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x00174A1C File Offset: 0x00172C1C
		public override int GetUnitValueForFaction(IFaction faction)
		{
			int num = 0;
			float militaryValueOfParty = Campaign.Current.Models.ValuationModel.GetMilitaryValueOfParty(base.OriginalParty.MobileParty);
			if (faction.MapFaction == this._otherFaction.MapFaction && faction.MapFaction.IsAtWarWith(base.OriginalParty.MapFaction))
			{
				num = (int)(militaryValueOfParty * 0.1f);
			}
			else if (faction.MapFaction == base.OriginalParty.MapFaction)
			{
				num = -(int)(militaryValueOfParty * 0.1f);
			}
			return num;
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00174A9F File Offset: 0x00172C9F
		public override ImageIdentifier GetVisualIdentifier()
		{
			return null;
		}

		// Token: 0x04001449 RID: 5193
		private readonly IFaction _otherFaction;

		// Token: 0x0400144A RID: 5194
		private readonly CampaignTime _duration;

		// Token: 0x0400144B RID: 5195
		private readonly Hero _otherHero;

		// Token: 0x0400144C RID: 5196
		private readonly PartyBase _otherParty;
	}
}
