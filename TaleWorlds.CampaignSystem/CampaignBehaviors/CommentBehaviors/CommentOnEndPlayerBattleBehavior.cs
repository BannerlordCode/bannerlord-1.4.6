using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000460 RID: 1120
	public class CommentOnEndPlayerBattleBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004836 RID: 18486 RVA: 0x0016BC0E File Offset: 0x00169E0E
		public override void RegisterEvents()
		{
			CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(this, new Action<MapEvent>(this.OnPlayerBattleEnded));
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0016BC27 File Offset: 0x00169E27
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0016BC29 File Offset: 0x00169E29
		private void OnPlayerBattleEnded(MapEvent mapEvent)
		{
			if (!mapEvent.IsHideoutBattle || mapEvent.BattleState != BattleState.None)
			{
				LogEntry.AddLogEntry(new PlayerBattleEndedLogEntry(mapEvent));
			}
		}
	}
}
