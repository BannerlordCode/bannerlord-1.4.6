using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000045 RID: 69
	public class MbEvent<T> : IMbEvent<T>, IMbEventBase
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0002658C File Offset: 0x0002478C
		public void AddNonSerializedListener(object owner, Action<T> action)
		{
			MbEvent<T>.EventHandlerRec<T> eventHandlerRec = new MbEvent<T>.EventHandlerRec<T>(owner, action);
			MbEvent<T>.EventHandlerRec<T> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000265B6 File Offset: 0x000247B6
		public void Invoke(T t)
		{
			this.InvokeList(this._nonSerializedListenerList, t);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000265C5 File Offset: 0x000247C5
		private void InvokeList(MbEvent<T>.EventHandlerRec<T> list, T t)
		{
			while (list != null)
			{
				list.Action(t);
				list = list.Next;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000265E0 File Offset: 0x000247E0
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000265F0 File Offset: 0x000247F0
		private void ClearListenerOfList(ref MbEvent<T>.EventHandlerRec<T> list, object o)
		{
			MbEvent<T>.EventHandlerRec<T> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			MbEvent<T>.EventHandlerRec<T> eventHandlerRec2 = list;
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

		// Token: 0x040002B8 RID: 696
		private MbEvent<T>.EventHandlerRec<T> _nonSerializedListenerList;

		// Token: 0x0200050A RID: 1290
		internal class EventHandlerRec<TS>
		{
			// Token: 0x17000ED2 RID: 3794
			// (get) Token: 0x06004C01 RID: 19457 RVA: 0x0017DAC2 File Offset: 0x0017BCC2
			// (set) Token: 0x06004C02 RID: 19458 RVA: 0x0017DACA File Offset: 0x0017BCCA
			internal Action<TS> Action { get; private set; }

			// Token: 0x17000ED3 RID: 3795
			// (get) Token: 0x06004C03 RID: 19459 RVA: 0x0017DAD3 File Offset: 0x0017BCD3
			// (set) Token: 0x06004C04 RID: 19460 RVA: 0x0017DADB File Offset: 0x0017BCDB
			internal object Owner { get; private set; }

			// Token: 0x06004C05 RID: 19461 RVA: 0x0017DAE4 File Offset: 0x0017BCE4
			public EventHandlerRec(object owner, Action<TS> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015BD RID: 5565
			public MbEvent<T>.EventHandlerRec<TS> Next;
		}
	}
}
