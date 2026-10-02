using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E4 RID: 484
	public class BasicGameStarter : IGameStarter
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00061356 File Offset: 0x0005F556
		IEnumerable<GameModel> IGameStarter.Models
		{
			get
			{
				return this._models;
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0006135E File Offset: 0x0005F55E
		public BasicGameStarter()
		{
			this._models = new List<GameModel>();
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00061374 File Offset: 0x0005F574
		public T GetModel<T>() where T : GameModel
		{
			for (int i = this._models.Count - 1; i >= 0; i--)
			{
				T t;
				if ((t = this._models[i] as T) != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000613C3 File Offset: 0x0005F5C3
		public void AddModel(GameModel gameModel)
		{
			this._models.Add(gameModel);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000613D4 File Offset: 0x0005F5D4
		public void AddModel<T>(MBGameModel<T> gameModel) where T : GameModel
		{
			T model = this.GetModel<T>();
			gameModel.Initialize(model);
			this._models.Add(gameModel);
		}

		// Token: 0x04000988 RID: 2440
		private List<GameModel> _models;
	}
}
