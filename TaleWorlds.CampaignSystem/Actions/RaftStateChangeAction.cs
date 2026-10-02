using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004C1 RID: 1217
	public class RaftStateChangeAction
	{
		// Token: 0x06004AD2 RID: 19154 RVA: 0x0017AC2C File Offset: 0x00178E2C
		private static void ApplyInternal(MobileParty mobileParty, bool isRaftState)
		{
			mobileParty.IsInRaftState = isRaftState;
			if (mobileParty.Army != null)
			{
				mobileParty.Army = null;
			}
			if (isRaftState)
			{
				mobileParty.MovePartyToTheClosestLand();
				mobileParty.Ai.DisableAi();
				if (mobileParty.Party.PrisonRoster.TotalManCount > 0)
				{
					if (mobileParty.Party.PrisonRoster.TotalHeroes > 0)
					{
						foreach (TroopRosterElement troopRosterElement in mobileParty.PrisonRoster.GetTroopRoster())
						{
							if (troopRosterElement.Character.IsHero)
							{
								EndCaptivityAction.ApplyByEscape(troopRosterElement.Character.HeroObject, null, true);
							}
						}
					}
					mobileParty.PrisonRoster.Clear();
				}
			}
			else
			{
				mobileParty.Ai.EnableAi();
				mobileParty.RecalculateShortTermBehavior();
				mobileParty.Ai.DefaultBehaviorNeedsUpdate = true;
				mobileParty.Ai.RethinkAtNextHourlyTick = true;
			}
			CampaignEventDispatcher.Instance.OnMobilePartyRaftStateChanged(mobileParty);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x0017AD34 File Offset: 0x00178F34
		public static void ActivateRaftStateForParty(MobileParty mobileParty)
		{
			RaftStateChangeAction.ApplyInternal(mobileParty, true);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x0017AD3D File Offset: 0x00178F3D
		public static void DeactivateRaftStateForParty(MobileParty mobileParty)
		{
			RaftStateChangeAction.ApplyInternal(mobileParty, false);
		}
	}
}
