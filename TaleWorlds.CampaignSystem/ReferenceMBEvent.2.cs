using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000049 RID: 73
	public class ReferenceMBEvent<T1, T2> : ReferenceIMBEvent<T1, T2>, IMbEventBase
	{
		// Token: 0x06000884 RID: 2180 RVA: 0x0002671C File Offset: 0x0002491C
		public void AddNonSerializedListener(object owner, ReferenceAction<T1, T2> action)
		{
			ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> eventHandlerRec = new ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2>(owner, action);
			ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00026746 File Offset: 0x00024946
		public void Invoke(T1 t1, ref T2 t2)
		{
			this.InvokeList(this._nonSerializedListenerList, t1, ref t2);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00026756 File Offset: 0x00024956
		private void InvokeList(ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> list, T1 t1, ref T2 t2)
		{
			while (list != null)
			{
				list.Action(t1, ref t2);
				list = list.Next;
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00026772 File Offset: 0x00024972
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00026784 File Offset: 0x00024984
		private void ClearListenerOfList(ref ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> list, object o)
		{
			ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> eventHandlerRec2 = list;
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

		// Token: 0x040002BA RID: 698
		private ReferenceMBEvent<T1, T2>.EventHandlerRec<T1, T2> _nonSerializedListenerList;

		// Token: 0x0200050C RID: 1292
		internal class EventHandlerRec<TS, TQ>
		{
			// Token: 0x17000ED6 RID: 3798
			// (get) Token: 0x06004C0B RID: 19467 RVA: 0x0017DB32 File Offset: 0x0017BD32
			// (set) Token: 0x06004C0C RID: 19468 RVA: 0x0017DB3A File Offset: 0x0017BD3A
			internal ReferenceAction<TS, TQ> Action { get; private set; }

			// Token: 0x17000ED7 RID: 3799
			// (get) Token: 0x06004C0D RID: 19469 RVA: 0x0017DB43 File Offset: 0x0017BD43
			// (set) Token: 0x06004C0E RID: 19470 RVA: 0x0017DB4B File Offset: 0x0017BD4B
			internal object Owner { get; private set; }

			// Token: 0x06004C0F RID: 19471 RVA: 0x0017DB54 File Offset: 0x0017BD54
			public EventHandlerRec(object owner, ReferenceAction<TS, TQ> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015C3 RID: 5571
			public ReferenceMBEvent<T1, T2>.EventHandlerRec<TS, TQ> Next;
		}
	}
}
