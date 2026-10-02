using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Core
{
	// Token: 0x02000056 RID: 86
	public class DummyCommunicator : ICommunicator
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00018325 File Offset: 0x00016525
		public VirtualPlayer VirtualPlayer { get; }

		// Token: 0x060006ED RID: 1773 RVA: 0x0001832D File Offset: 0x0001652D
		public void OnSynchronizeComponentTo(VirtualPlayer peer, PeerComponent component)
		{
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001832F File Offset: 0x0001652F
		public void OnAddComponent(PeerComponent component)
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00018331 File Offset: 0x00016531
		public void OnRemoveComponent(PeerComponent component)
		{
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00018333 File Offset: 0x00016533
		public bool IsNetworkActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00018336 File Offset: 0x00016536
		public bool IsConnectionActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00018339 File Offset: 0x00016539
		public bool IsServerPeer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001833C File Offset: 0x0001653C
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001833F File Offset: 0x0001653F
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00018341 File Offset: 0x00016541
		private DummyCommunicator(int index, string name)
		{
			this.VirtualPlayer = new VirtualPlayer(index, name, PlayerId.Empty, this);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001835C File Offset: 0x0001655C
		public static DummyCommunicator CreateAsServer(int index, string name)
		{
			return new DummyCommunicator(index, name);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00018365 File Offset: 0x00016565
		public static DummyCommunicator CreateAsClient(string name, int index)
		{
			return new DummyCommunicator(index, name);
		}
	}
}
