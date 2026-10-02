using System;
using TaleWorlds.MountAndBlade.ViewModelCollection.Order.Visual;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x0200001B RID: 27
	public struct MissionOrderCallbacks
	{
		// Token: 0x04000114 RID: 276
		public MissionOrderCallbacks.OnRefreshVisualsDelegate RefreshVisuals;

		// Token: 0x04000115 RID: 277
		public MissionOrderCallbacks.OnToggleActivateOrderStateDelegate OnActivateToggleOrder;

		// Token: 0x04000116 RID: 278
		public MissionOrderCallbacks.OnToggleActivateOrderStateDelegate OnDeactivateToggleOrder;

		// Token: 0x04000117 RID: 279
		public MissionOrderCallbacks.OnTransferTroopsFinishedDelegate OnTransferTroopsFinished;

		// Token: 0x04000118 RID: 280
		public MissionOrderCallbacks.OnBeforeOrderDelegate OnBeforeOrder;

		// Token: 0x04000119 RID: 281
		public Action<bool> ToggleMissionInputs;

		// Token: 0x0400011A RID: 282
		public MissionOrderCallbacks.ToggleOrderPositionVisibilityDelegate SetSuspendTroopPlacer;

		// Token: 0x0400011B RID: 283
		public MissionOrderCallbacks.GetOrderExecutionParametersDelegate GetVisualOrderExecutionParameters;

		// Token: 0x020000BA RID: 186
		// (Invoke) Token: 0x06000C13 RID: 3091
		public delegate void OnRefreshVisualsDelegate();

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x06000C17 RID: 3095
		public delegate void OnToggleActivateOrderStateDelegate();

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x06000C1B RID: 3099
		public delegate void OnTransferTroopsFinishedDelegate();

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x06000C1F RID: 3103
		public delegate void OnBeforeOrderDelegate();

		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x06000C23 RID: 3107
		public delegate void ToggleOrderPositionVisibilityDelegate(bool value);

		// Token: 0x020000BF RID: 191
		// (Invoke) Token: 0x06000C27 RID: 3111
		public delegate VisualOrderExecutionParameters GetOrderExecutionParametersDelegate();
	}
}
