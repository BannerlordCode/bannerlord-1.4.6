using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000055 RID: 85
	public class MbEvent<T1, T2, T3, T4, T5, T6> : IMbEvent<T1, T2, T3, T4, T5, T6>, IMbEventBase
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x00026BF0 File Offset: 0x00024DF0
		public void AddNonSerializedListener(object owner, Action<T1, T2, T3, T4, T5, T6> action)
		{
			MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> eventHandlerRec = new MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6>(owner, action);
			MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00026C1A File Offset: 0x00024E1A
		public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			this.InvokeList(this._nonSerializedListenerList, t1, t2, t3, t4, t5, t6);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00026C31 File Offset: 0x00024E31
		private void InvokeList(MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> list, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			while (list != null)
			{
				list.Action(t1, t2, t3, t4, t5, t6);
				list = list.Next;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00026C55 File Offset: 0x00024E55
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00026C64 File Offset: 0x00024E64
		private void ClearListenerOfList(ref MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> list, object o)
		{
			MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> eventHandlerRec2 = list;
			if (eventHandlerRec2 == eventHandlerRec)
			{
				list = eventHandlerRec2.Next;
				return;
			}
			while (eventHandlerRec2 != null)
			{
				if (eventHandlerRec2.Next == eventHandlerRec)
				{
					eventHandlerRec2.Next = eventHandlerRec.Next;
				}
				else
				{
					eventHandlerRec2 = eventHandlerRec2.Next;
				}
			}
		}

		// Token: 0x040002C0 RID: 704
		private MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<T1, T2, T3, T4, T5, T6> _nonSerializedListenerList;

		// Token: 0x02000512 RID: 1298
		internal class EventHandlerRec<TA, TB, TC, TD, TE, TF>
		{
			// Token: 0x17000EE2 RID: 3810
			// (get) Token: 0x06004C29 RID: 19497 RVA: 0x0017DC82 File Offset: 0x0017BE82
			// (set) Token: 0x06004C2A RID: 19498 RVA: 0x0017DC8A File Offset: 0x0017BE8A
			internal Action<TA, TB, TC, TD, TE, TF> Action { get; private set; }

			// Token: 0x17000EE3 RID: 3811
			// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0017DC93 File Offset: 0x0017BE93
			// (set) Token: 0x06004C2C RID: 19500 RVA: 0x0017DC9B File Offset: 0x0017BE9B
			internal object Owner { get; private set; }

			// Token: 0x06004C2D RID: 19501 RVA: 0x0017DCA4 File Offset: 0x0017BEA4
			public EventHandlerRec(object owner, Action<TA, TB, TC, TD, TE, TF> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015D5 RID: 5589
			public MbEvent<T1, T2, T3, T4, T5, T6>.EventHandlerRec<TA, TB, TC, TD, TE, TF> Next;
		}
	}
}
