using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x02000074 RID: 116
	public abstract class GameState : MBObjectBase
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001A407 File Offset: 0x00018607
		public GameState Predecessor
		{
			get
			{
				return this.GameStateManager.FindPredecessor(this);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0001A415 File Offset: 0x00018615
		public bool IsActive
		{
			get
			{
				return this.GameStateManager != null && this.GameStateManager.ActiveState == this;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001A42F File Offset: 0x0001862F
		public IReadOnlyCollection<IGameStateListener> Listeners
		{
			get
			{
				return this._listeners.AsReadOnly();
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001A43C File Offset: 0x0001863C
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001A444 File Offset: 0x00018644
		public GameStateManager GameStateManager { get; internal set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0001A44D File Offset: 0x0001864D
		public virtual bool IsMusicMenuState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001A450 File Offset: 0x00018650
		public virtual bool IsMenuState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001A453 File Offset: 0x00018653
		protected GameState()
		{
			this._listeners = new List<IGameStateListener>();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001A466 File Offset: 0x00018666
		public bool RegisterListener(IGameStateListener listener)
		{
			if (listener == null)
			{
				Debug.FailedAssert("Can not register null listener to game state.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.Core\\GameState.cs", "RegisterListener", 47);
			}
			if (this._listeners.Contains(listener))
			{
				return false;
			}
			this._listeners.Add(listener);
			return true;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001A49E File Offset: 0x0001869E
		public bool UnregisterListener(IGameStateListener listener)
		{
			return this._listeners.Remove(listener);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001A4AC File Offset: 0x000186AC
		public T GetListenerOfType<T>()
		{
			using (List<IGameStateListener>.Enumerator enumerator = this._listeners.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGameStateListener gameStateListener;
					if ((gameStateListener = enumerator.Current) is T)
					{
						return (T)((object)gameStateListener);
					}
				}
			}
			return default(T);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001A518 File Offset: 0x00018718
		internal void HandleInitialize()
		{
			this.OnInitialize();
			foreach (IGameStateListener gameStateListener in this._listeners)
			{
				gameStateListener.OnInitialize();
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001A570 File Offset: 0x00018770
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001A574 File Offset: 0x00018774
		internal void HandleFinalize()
		{
			this.OnFinalize();
			foreach (IGameStateListener gameStateListener in this._listeners)
			{
				gameStateListener.OnFinalize();
			}
			this._listeners = null;
			this.GameStateManager = null;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001A5D8 File Offset: 0x000187D8
		protected virtual void OnFinalize()
		{
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001A5DC File Offset: 0x000187DC
		internal void HandleActivate()
		{
			GameState.NumberOfListenerActivations = 0;
			if (this.IsActive)
			{
				this.OnActivate();
				if (this.IsActive && this._listeners.Count != 0 && GameState.NumberOfListenerActivations == 0)
				{
					foreach (IGameStateListener gameStateListener in this._listeners)
					{
						gameStateListener.OnActivate();
					}
					GameState.NumberOfListenerActivations++;
				}
				if (!string.IsNullOrEmpty(GameStateManager.StateActivateCommand))
				{
					bool flag;
					CommandLineFunctionality.CallFunction(GameStateManager.StateActivateCommand, "", out flag);
				}
				Debug.ReportMemoryBookmark("GameState Activated: " + base.GetType().Name);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0001A6A4 File Offset: 0x000188A4
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0001A6AC File Offset: 0x000188AC
		public bool Activated { get; private set; }

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A6B5 File Offset: 0x000188B5
		protected virtual void OnActivate()
		{
			this.Activated = true;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A6C0 File Offset: 0x000188C0
		internal void HandleDeactivate()
		{
			this.OnDeactivate();
			foreach (IGameStateListener gameStateListener in this._listeners)
			{
				gameStateListener.OnDeactivate();
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A718 File Offset: 0x00018918
		protected virtual void OnDeactivate()
		{
			this.Activated = false;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A721 File Offset: 0x00018921
		protected internal virtual void OnTick(float dt)
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001A723 File Offset: 0x00018923
		protected internal virtual void OnIdleTick(float dt)
		{
		}

		// Token: 0x04000410 RID: 1040
		public int Level;

		// Token: 0x04000411 RID: 1041
		private List<IGameStateListener> _listeners;

		// Token: 0x04000412 RID: 1042
		public static int NumberOfListenerActivations;
	}
}
