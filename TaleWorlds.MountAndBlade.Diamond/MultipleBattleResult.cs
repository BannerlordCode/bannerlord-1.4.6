using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class MultipleBattleResult
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00005C49 File Offset: 0x00003E49
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00005C51 File Offset: 0x00003E51
		public List<BattleResult> BattleResults { get; set; }

		// Token: 0x06000501 RID: 1281 RVA: 0x00005C5A File Offset: 0x00003E5A
		public MultipleBattleResult()
		{
			this.BattleResults = new List<BattleResult>();
			this._currentBattleIndex = -1;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00005C74 File Offset: 0x00003E74
		public void CreateNewBattleResult(string gameType)
		{
			BattleResult battleResult = new BattleResult();
			this.BattleResults.Add(battleResult);
			this._currentBattleIndex++;
			if (this._currentBattleIndex > 0)
			{
				foreach (KeyValuePair<string, BattlePlayerEntry> keyValuePair in this.BattleResults[this._currentBattleIndex - 1].PlayerEntries)
				{
					battleResult.AddOrUpdatePlayerEntry(PlayerId.FromString(keyValuePair.Key), keyValuePair.Value.TeamNo, gameType, Guid.Empty, -1);
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00005D20 File Offset: 0x00003F20
		public BattleResult GetCurrentBattleResult()
		{
			return this.BattleResults[this._currentBattleIndex];
		}

		// Token: 0x040001AD RID: 429
		private int _currentBattleIndex;
	}
}
