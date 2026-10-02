using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000047 RID: 71
	public class ReferenceMBEvent<T1> : ReferenceIMBEvent<T1>, IMbEventBase
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x00026654 File Offset: 0x00024854
		public void AddNonSerializedListener(object owner, ReferenceAction<T1> action)
		{
			ReferenceMBEvent<T1>.EventHandlerRec<T1> eventHandlerRec = new ReferenceMBEvent<T1>.EventHandlerRec<T1>(owner, action);
			ReferenceMBEvent<T1>.EventHandlerRec<T1> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002667E File Offset: 0x0002487E
		public void Invoke(ref T1 t1)
		{
			this.InvokeList(this._nonSerializedListenerList, ref t1);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002668D File Offset: 0x0002488D
		private void InvokeList(ReferenceMBEvent<T1>.EventHandlerRec<T1> list, ref T1 t1)
		{
			while (list != null)
			{
				list.Action(ref t1);
				list = list.Next;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000266A8 File Offset: 0x000248A8
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000266B8 File Offset: 0x000248B8
		private void ClearListenerOfList(ref ReferenceMBEvent<T1>.EventHandlerRec<T1> list, object o)
		{
			ReferenceMBEvent<T1>.EventHandlerRec<T1> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			ReferenceMBEvent<T1>.EventHandlerRec<T1> eventHandlerRec2 = list;
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

		// Token: 0x040002B9 RID: 697
		private ReferenceMBEvent<T1>.EventHandlerRec<T1> _nonSerializedListenerList;

		// Token: 0x0200050B RID: 1291
		internal class EventHandlerRec<TS>
		{
			// Token: 0x17000ED4 RID: 3796
			// (get) Token: 0x06004C06 RID: 19462 RVA: 0x0017DAFA File Offset: 0x0017BCFA
			// (set) Token: 0x06004C07 RID: 19463 RVA: 0x0017DB02 File Offset: 0x0017BD02
			internal ReferenceAction<TS> Action { get; private set; }

			// Token: 0x17000ED5 RID: 3797
			// (get) Token: 0x06004C08 RID: 19464 RVA: 0x0017DB0B File Offset: 0x0017BD0B
			// (set) Token: 0x06004C09 RID: 19465 RVA: 0x0017DB13 File Offset: 0x0017BD13
			internal object Owner { get; private set; }

			// Token: 0x06004C0A RID: 19466 RVA: 0x0017DB1C File Offset: 0x0017BD1C
			public EventHandlerRec(object owner, ReferenceAction<TS> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015C0 RID: 5568
			public ReferenceMBEvent<T1>.EventHandlerRec<TS> Next;
		}
	}
}
