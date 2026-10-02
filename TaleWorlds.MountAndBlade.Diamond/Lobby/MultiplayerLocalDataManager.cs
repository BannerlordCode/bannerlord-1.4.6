using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby
{
	// Token: 0x0200016C RID: 364
	public class MultiplayerLocalDataManager
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00010151 File Offset: 0x0000E351
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00010158 File Offset: 0x0000E358
		public static MultiplayerLocalDataManager Instance { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00010160 File Offset: 0x0000E360
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00010168 File Offset: 0x0000E368
		public TauntSlotDataContainer TauntSlotData { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00010171 File Offset: 0x0000E371
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00010179 File Offset: 0x0000E379
		public MatchHistoryDataContainer MatchHistory { get; private set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00010182 File Offset: 0x0000E382
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0001018A File Offset: 0x0000E38A
		public FavoriteServerDataContainer FavoriteServers { get; private set; }

		// Token: 0x06000A19 RID: 2585 RVA: 0x00010193 File Offset: 0x0000E393
		private MultiplayerLocalDataManager()
		{
			this.TauntSlotData = new TauntSlotDataContainer();
			this.MatchHistory = new MatchHistoryDataContainer();
			this.FavoriteServers = new FavoriteServerDataContainer();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000101BC File Offset: 0x0000E3BC
		public static void InitializeManager()
		{
			if (MultiplayerLocalDataManager.Instance != null)
			{
				Debug.FailedAssert("Multiplayer local data manager is already initialized", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "InitializeManager", 34);
				return;
			}
			MultiplayerLocalDataManager.Instance = new MultiplayerLocalDataManager();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000101E6 File Offset: 0x0000E3E6
		public static void FinalizeManager()
		{
			if (MultiplayerLocalDataManager.Instance == null)
			{
				Debug.FailedAssert("Multiplayer local data manager is not initialized", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "FinalizeManager", 45);
				return;
			}
			MultiplayerLocalDataManager.Instance.WaitForAsyncOperations();
			MultiplayerLocalDataManager.Instance = null;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00010218 File Offset: 0x0000E418
		public async void Tick(float dt)
		{
			if (!this._isBusy)
			{
				this._isBusy = true;
				await this.TauntSlotData.Tick(dt);
				await this.MatchHistory.Tick(dt);
				await this.FavoriteServers.Tick(dt);
				this._isBusy = false;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00010259 File Offset: 0x0000E459
		private void WaitForAsyncOperations()
		{
			while (this._isBusy)
			{
			}
		}

		// Token: 0x040004F1 RID: 1265
		internal const float FileUpdateIntervalInSeconds = 2f;

		// Token: 0x040004F5 RID: 1269
		private bool _isBusy;
	}
}
