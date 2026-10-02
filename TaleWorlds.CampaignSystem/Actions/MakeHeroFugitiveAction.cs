using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004BC RID: 1212
	public static class MakeHeroFugitiveAction
	{
		// Token: 0x06004AC5 RID: 19141 RVA: 0x0017A69C File Offset: 0x0017889C
		private static void ApplyInternal(Hero fugitive, bool showNotification)
		{
			if (fugitive.IsAlive)
			{
				if (fugitive.PartyBelongedTo != null)
				{
					if (fugitive.PartyBelongedTo.LeaderHero == fugitive)
					{
						DestroyPartyAction.Apply(null, fugitive.PartyBelongedTo);
					}
					else
					{
						fugitive.PartyBelongedTo.MemberRoster.RemoveTroop(fugitive.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
					}
				}
				if (fugitive.CurrentSettlement != null)
				{
					LeaveSettlementAction.ApplyForCharacterOnly(fugitive);
				}
				fugitive.ChangeState(Hero.CharacterStates.Fugitive);
				CampaignEventDispatcher.Instance.OnCharacterBecameFugitive(fugitive, showNotification);
			}
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0017A717 File Offset: 0x00178917
		public static void Apply(Hero fugitive, bool showNotification = false)
		{
			MakeHeroFugitiveAction.ApplyInternal(fugitive, showNotification);
		}
	}
}
