using System;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200003A RID: 58
	public class MbEvent : IMbEvent
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0001E7C8 File Offset: 0x0001C9C8
		public void AddNonSerializedListener(object owner, Action action)
		{
			MbEvent.EventHandlerRec eventHandlerRec = new MbEvent.EventHandlerRec(owner, action);
			MbEvent.EventHandlerRec nonSerializedListenerList = this._nonSerializedListenerList;
			this._nonSerializedListenerList = eventHandlerRec;
			eventHandlerRec.Next = nonSerializedListenerList;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001E7F2 File Offset: 0x0001C9F2
		public void Invoke()
		{
			this.InvokeList(this._nonSerializedListenerList);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001E800 File Offset: 0x0001CA00
		private void InvokeList(MbEvent.EventHandlerRec list)
		{
			while (list != null)
			{
				list.Action();
				list = list.Next;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001E81A File Offset: 0x0001CA1A
		public void ClearListeners(object o)
		{
			this.ClearListenerOfList(ref this._nonSerializedListenerList, o);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001E82C File Offset: 0x0001CA2C
		private void ClearListenerOfList(ref MbEvent.EventHandlerRec list, object o)
		{
			MbEvent.EventHandlerRec eventHandlerRec = list;
			while (eventHandlerRec != null && eventHandlerRec.Owner != o)
			{
				eventHandlerRec = eventHandlerRec.Next;
			}
			if (eventHandlerRec == null)
			{
				return;
			}
			MbEvent.EventHandlerRec eventHandlerRec2 = list;
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

		// Token: 0x04000184 RID: 388
		private MbEvent.EventHandlerRec _nonSerializedListenerList;

		// Token: 0x02000506 RID: 1286
		internal class EventHandlerRec
		{
			// Token: 0x17000ECE RID: 3790
			// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x0017D73A File Offset: 0x0017B93A
			// (set) Token: 0x06004BD9 RID: 19417 RVA: 0x0017D742 File Offset: 0x0017B942
			internal Action Action { get; private set; }

			// Token: 0x17000ECF RID: 3791
			// (get) Token: 0x06004BDA RID: 19418 RVA: 0x0017D74B File Offset: 0x0017B94B
			// (set) Token: 0x06004BDB RID: 19419 RVA: 0x0017D753 File Offset: 0x0017B953
			internal object Owner { get; private set; }

			// Token: 0x06004BDC RID: 19420 RVA: 0x0017D75C File Offset: 0x0017B95C
			public EventHandlerRec(object owner, Action action)
			{
				this.Action = action;
				this.Owner = owner;
			}

			// Token: 0x0400159F RID: 5535
			public MbEvent.EventHandlerRec Next;
		}
	}
}
