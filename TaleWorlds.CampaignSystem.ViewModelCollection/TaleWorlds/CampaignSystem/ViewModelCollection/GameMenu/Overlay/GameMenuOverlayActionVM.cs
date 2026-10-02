using System;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000B8 RID: 184
	public class GameMenuOverlayActionVM : StringItemWithEnabledAndHintVM
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x0004A224 File Offset: 0x00048424
		public GameMenuOverlayActionVM(Action<object> onExecute, string item, bool isEnabled, object identifier, TextObject hint = null)
			: base(onExecute, item, isEnabled, identifier, hint)
		{
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004A233 File Offset: 0x00048433
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0004A23B File Offset: 0x0004843B
		[DataSourceProperty]
		public bool IsHiglightEnabled
		{
			get
			{
				return this._isHiglightEnabled;
			}
			set
			{
				if (value != this._isHiglightEnabled)
				{
					this._isHiglightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHiglightEnabled");
				}
			}
		}

		// Token: 0x04000859 RID: 2137
		private bool _isHiglightEnabled;
	}
}
