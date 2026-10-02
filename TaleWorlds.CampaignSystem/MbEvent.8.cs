using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000057 RID: 87
	public class MbEvent<T1, T2, T3, T4, T5, T6, T7> : IMbEvent<T1, T2, T3, T4, T5, T6, T7>, IMbEventBase
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x00026CC8 File Offset: 0x00024EC8
		public void AddNonSerializedListener(object owner, Action<T1, T2, T3, T4, T5, T6, T7> action)
		{
			MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> eventHandlerRec = new MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7>(owner, action);
			MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00026CF4 File Offset: 0x00024EF4
		public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
		{
			this.InvokeList(this._nonSerializedListenerList, t1, t2, t3, t4, t5, t6, t7);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00026D18 File Offset: 0x00024F18
		private void InvokeList(MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> list, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
		{
			while (list != null)
			{
				list.Action(t1, t2, t3, t4, t5, t6, t7);
				list = list.Next;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00026D3E File Offset: 0x00024F3E
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00026D50 File Offset: 0x00024F50
		private void ClearListenerOfList(ref MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> list, object o)
		{
			MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> eventHandlerRec2 = list;
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

		// Token: 0x040002C1 RID: 705
		private MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<T1, T2, T3, T4, T5, T6, T7> _nonSerializedListenerList;

		// Token: 0x02000513 RID: 1299
		internal class EventHandlerRec<TA, TB, TC, TD, TE, TF, TG>
		{
			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06004C2E RID: 19502 RVA: 0x0017DCBA File Offset: 0x0017BEBA
			// (set) Token: 0x06004C2F RID: 19503 RVA: 0x0017DCC2 File Offset: 0x0017BEC2
			internal Action<TA, TB, TC, TD, TE, TF, TG> Action { get; private set; }

			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06004C30 RID: 19504 RVA: 0x0017DCCB File Offset: 0x0017BECB
			// (set) Token: 0x06004C31 RID: 19505 RVA: 0x0017DCD3 File Offset: 0x0017BED3
			internal object Owner { get; private set; }

			// Token: 0x06004C32 RID: 19506 RVA: 0x0017DCDC File Offset: 0x0017BEDC
			public EventHandlerRec(object owner, Action<TA, TB, TC, TD, TE, TF, TG> action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x040015D8 RID: 5592
			public MbEvent<T1, T2, T3, T4, T5, T6, T7>.EventHandlerRec<TA, TB, TC, TD, TE, TF, TG> Next;
		}
	}
}
