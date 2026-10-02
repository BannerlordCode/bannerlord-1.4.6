using System;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036F RID: 879
	public interface IFocusable
	{
		// Token: 0x0600324D RID: 12877
		void OnFocusGain(Agent userAgent);

		// Token: 0x0600324E RID: 12878
		void OnFocusLose(Agent userAgent);

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600324F RID: 12879
		FocusableObjectType FocusableObjectType { get; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003250 RID: 12880
		bool IsFocusable { get; }

		// Token: 0x06003251 RID: 12881
		TextObject GetInfoTextForBeingNotInteractable(Agent userAgent);

		// Token: 0x06003252 RID: 12882
		TextObject GetDescriptionText(WeakGameEntity gameEntity);
	}
}
