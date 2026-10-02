using System;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000240 RID: 576
	public class InitialStateOption
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00074B72 File Offset: 0x00072D72
		// (set) Token: 0x06002136 RID: 8502 RVA: 0x00074B7A File Offset: 0x00072D7A
		public int OrderIndex { get; private set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00074B83 File Offset: 0x00072D83
		// (set) Token: 0x06002138 RID: 8504 RVA: 0x00074B8B File Offset: 0x00072D8B
		public TextObject Name { get; private set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00074B94 File Offset: 0x00072D94
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x00074B9C File Offset: 0x00072D9C
		public string Id { get; private set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x00074BA5 File Offset: 0x00072DA5
		// (set) Token: 0x0600213C RID: 8508 RVA: 0x00074BAD File Offset: 0x00072DAD
		public Func<bool> IsHidden { get; private set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x00074BB6 File Offset: 0x00072DB6
		// (set) Token: 0x0600213E RID: 8510 RVA: 0x00074BBE File Offset: 0x00072DBE
		public Func<ValueTuple<bool, TextObject>> IsDisabledAndReason { get; private set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00074BC7 File Offset: 0x00072DC7
		// (set) Token: 0x06002140 RID: 8512 RVA: 0x00074BCF File Offset: 0x00072DCF
		public TextObject EnabledHint { get; private set; }

		// Token: 0x06002141 RID: 8513 RVA: 0x00074BD8 File Offset: 0x00072DD8
		public InitialStateOption(string id, TextObject name, int orderIndex, Action action, Func<ValueTuple<bool, TextObject>> isDisabledAndReason, TextObject enabledHint = null, Func<bool> isHidden = null)
		{
			this.Name = name;
			this.Id = id;
			this.OrderIndex = orderIndex;
			this._action = action;
			this.IsHidden = isHidden;
			this.IsDisabledAndReason = isDisabledAndReason;
			this.EnabledHint = enabledHint;
			TextObject item = this.IsDisabledAndReason().Item2;
			string.IsNullOrEmpty((item != null) ? item.ToString() : null);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00074C42 File Offset: 0x00072E42
		public void DoAction()
		{
			Action action = this._action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x04000CC0 RID: 3264
		private Action _action;
	}
}
