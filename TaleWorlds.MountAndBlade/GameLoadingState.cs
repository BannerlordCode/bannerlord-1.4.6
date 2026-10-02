using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023C RID: 572
	public class GameLoadingState : GameState
	{
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000749A7 File Offset: 0x00072BA7
		public override bool IsMusicMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000749B2 File Offset: 0x00072BB2
		public void SetLoadingParameters(MBGameManager gameLoader)
		{
			Game.OnGameCreated += this.OnGameCreated;
			this._gameLoader = gameLoader;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000749CC File Offset: 0x00072BCC
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!this._loadingFinished)
			{
				this._loadingFinished = this._gameLoader.DoLoadingForGameManager();
				return;
			}
			GameStateManager.Current = Game.Current.GameStateManager;
			this._gameLoader.OnLoadFinished();
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00074A09 File Offset: 0x00072C09
		private void OnGameCreated()
		{
			Game.OnGameCreated -= this.OnGameCreated;
			Game.Current.OnItemDeserializedEvent += delegate(ItemObject itemObject)
			{
				if (itemObject.Type == ItemObject.ItemTypeEnum.HandArmor)
				{
					Utilities.RegisterMeshForGPUMorph(itemObject.MultiMeshName);
				}
			};
		}

		// Token: 0x04000CB6 RID: 3254
		private bool _loadingFinished;

		// Token: 0x04000CB7 RID: 3255
		private MBGameManager _gameLoader;
	}
}
