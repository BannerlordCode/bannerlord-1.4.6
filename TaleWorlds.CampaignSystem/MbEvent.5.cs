using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000051 RID: 81
	public class MbEvent<T1, T2, T3, T4> : IMbEvent<T1, T2, T3, T4>, IMbEventBase
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00026A4C File Offset: 0x00024C4C
		public void AddNonSerializedListener(object owner, Action<T1, T2, T3, T4> action)
		{
			MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> eventHandlerRec = new MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4>(owner, action);
			MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00026A76 File Offset: 0x00024C76
		public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4)
		{
			this.InvokeList(this._nonSerializedListenerList, t1, t2, t3, t4);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00026A89 File Offset: 0x00024C89
		private void InvokeList(MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> list, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			while (list != null)
			{
				list.Action(t1, t2, t3, t4);
				list = list.Next;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00026AA9 File Offset: 0x00024CA9
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00026AB8 File Offset: 0x00024CB8
		private void ClearListenerOfList(ref MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> list, object o)
		{
			MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> eventHandlerRec2 = list;
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

		// Token: 0x040002BE RID: 702
		private MbEvent<T1, T2, T3, T4>.EventHandlerRec<T1, T2, T3, T4> _nonSerializedListenerList;

		// Token: 0x02000510 RID: 1296
		internal class EventHandlerRec<TA, TB, TC, TD>
		{
			// Token: 0x17000EDE RID: 3806
			// (get) Token: 0x06004C1F RID: 19487 RVA: 0x0017DC12 File Offset: 0x0017BE12
			// (set) Token: 0x06004C20 RID: 19488 RVA: 0x0017DC1A File Offset: 0x0017BE1A
			internal Action<TA, TB, TC, TD> Action { get; private set; }

			// Token: 0x17000EDF RID: 3807
			// (get) Token: 0x06004C21 RID: 19489 RVA: 0x0017DC23 File Offset: 0x0017BE23
			// (set) Token: 0x06004C22 RID: 19490 RVA: 0x0017DC2B File Offset: 0x0017BE2B
			internal object Owner { get; private set; }

			// Token: 0x06004C23 RID: 19491 RVA: 0x0017DC34 File Offset: 0x0017BE34
			public EventHandlerRec(object owner, Action<TA, TB, TC, TD> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015CF RID: 5583
			public MbEvent<T1, T2, T3, T4>.EventHandlerRec<TA, TB, TC, TD> Next;
		}
	}
}
