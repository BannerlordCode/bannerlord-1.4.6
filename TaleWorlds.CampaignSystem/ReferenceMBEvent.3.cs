using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200004B RID: 75
	public class ReferenceMBEvent<T1, T2, T3> : ReferenceIMBEvent<T1, T2, T3>, IMbEventBase
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x000267E8 File Offset: 0x000249E8
		public void AddNonSerializedListener(object owner, ReferenceAction<T1, T2, T3> action)
		{
			ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> eventHandlerRec = new ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3>(owner, action);
			ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00026812 File Offset: 0x00024A12
		public void Invoke(T1 t1, T2 t2, ref T3 t3)
		{
			this.InvokeList(this._nonSerializedListenerList, t1, t2, ref t3);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00026823 File Offset: 0x00024A23
		private void InvokeList(ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> list, T1 t1, T2 t2, ref T3 t3)
		{
			while (list != null)
			{
				list.Action(t1, t2, ref t3);
				list = list.Next;
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00026841 File Offset: 0x00024A41
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00026850 File Offset: 0x00024A50
		private void ClearListenerOfList(ref ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> list, object o)
		{
			ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> eventHandlerRec2 = list;
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

		// Token: 0x040002BB RID: 699
		private ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<T1, T2, T3> _nonSerializedListenerList;

		// Token: 0x0200050D RID: 1293
		internal class EventHandlerRec<TS, TQ, TR>
		{
			// Token: 0x17000ED8 RID: 3800
			// (get) Token: 0x06004C10 RID: 19472 RVA: 0x0017DB6A File Offset: 0x0017BD6A
			// (set) Token: 0x06004C11 RID: 19473 RVA: 0x0017DB72 File Offset: 0x0017BD72
			internal ReferenceAction<TS, TQ, TR> Action { get; private set; }

			// Token: 0x17000ED9 RID: 3801
			// (get) Token: 0x06004C12 RID: 19474 RVA: 0x0017DB7B File Offset: 0x0017BD7B
			// (set) Token: 0x06004C13 RID: 19475 RVA: 0x0017DB83 File Offset: 0x0017BD83
			internal object Owner { get; private set; }

			// Token: 0x06004C14 RID: 19476 RVA: 0x0017DB8C File Offset: 0x0017BD8C
			public EventHandlerRec(object owner, ReferenceAction<TS, TQ, TR> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015C6 RID: 5574
			public ReferenceMBEvent<T1, T2, T3>.EventHandlerRec<TS, TQ, TR> Next;
		}
	}
}
