using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036E RID: 878
	public class DuelZoneLandmark : ScriptComponentBehavior, IFocusable
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x000CD360 File Offset: 0x000CB560
		public FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.None;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000CD363 File Offset: 0x000CB563
		public virtual bool IsFocusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000CD366 File Offset: 0x000CB566
		public void OnFocusGain(Agent userAgent)
		{
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000CD368 File Offset: 0x000CB568
		public void OnFocusLose(Agent userAgent)
		{
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000CD36A File Offset: 0x000CB56A
		public TextObject GetInfoTextForBeingNotInteractable(Agent userAgent)
		{
			return null;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000CD36D File Offset: 0x000CB56D
		public TextObject GetDescriptionText(WeakGameEntity gameEntity)
		{
			return null;
		}

		// Token: 0x04001544 RID: 5444
		public TroopType ZoneTroopType;
	}
}
