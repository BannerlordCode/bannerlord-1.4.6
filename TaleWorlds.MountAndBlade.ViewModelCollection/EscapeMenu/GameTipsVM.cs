using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu
{
	// Token: 0x02000081 RID: 129
	public class GameTipsVM : ViewModel
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002697F File Offset: 0x00024B7F
		public GameTipsVM(bool isAutoChangeEnabled, bool navigationButtonsEnabled)
		{
			this._navigationButtonsEnabled = navigationButtonsEnabled;
			this._isAutoChangeEnabled = isAutoChangeEnabled;
			this.RefreshValues();
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000269A8 File Offset: 0x00024BA8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._allTips = new MBList<string>();
			this.GameTipTitle = GameTexts.FindText("str_game_tip_title", null).ToString();
			float num = 0.8f;
			string keyHyperlinkText = HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 4), num);
			GameTexts.SetVariable("LEAVE_AREA_KEY", keyHyperlinkText);
			string keyHyperlinkText2 = HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 5), num);
			GameTexts.SetVariable("MISSION_INDICATORS_KEY", keyHyperlinkText2);
			GameTexts.SetVariable("EXTEND_KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("MapHotKeyCategory", "MapFollowModifier"), num));
			GameTexts.SetVariable("ENCYCLOPEDIA_SHORTCUT", HyperlinkTexts.GetKeyHyperlinkText("RightMouseButton", num));
			if (Input.IsMouseActive)
			{
				foreach (TextObject textObject in GameTexts.FindAllTextVariations("str_game_tip_pc"))
				{
					this._allTips.Add(textObject.ToString());
				}
			}
			foreach (TextObject textObject2 in GameTexts.FindAllTextVariations("str_game_tip"))
			{
				this._allTips.Add(textObject2.ToString());
			}
			this.NavigationButtonsEnabled = this._allTips.Count > 1;
			this.CurrentTip = ((this._allTips.Count == 0) ? string.Empty : this._allTips.GetRandomElement<string>());
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00026B2C File Offset: 0x00024D2C
		public void ExecutePreviousTip()
		{
			this._currentTipIndex--;
			if (this._currentTipIndex < 0)
			{
				this._currentTipIndex = this._allTips.Count - 1;
			}
			this.CurrentTip = this._allTips[this._currentTipIndex];
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00026B7A File Offset: 0x00024D7A
		public void ExecuteNextTip()
		{
			this._currentTipIndex = (this._currentTipIndex + 1) % this._allTips.Count;
			this.CurrentTip = this._allTips[this._currentTipIndex];
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00026BAD File Offset: 0x00024DAD
		public void OnTick(float dt)
		{
			if (this._isAutoChangeEnabled)
			{
				this._totalDt += dt;
				if (this._totalDt > this._tipTimeInterval)
				{
					this.ExecuteNextTip();
					this._totalDt = 0f;
				}
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00026BE4 File Offset: 0x00024DE4
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00026BEC File Offset: 0x00024DEC
		[DataSourceProperty]
		public string CurrentTip
		{
			get
			{
				return this._currentTip;
			}
			set
			{
				if (value != this._currentTip)
				{
					this._currentTip = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentTip");
				}
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00026C0F File Offset: 0x00024E0F
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x00026C17 File Offset: 0x00024E17
		[DataSourceProperty]
		public string GameTipTitle
		{
			get
			{
				return this._gameTipTitle;
			}
			set
			{
				if (value != this._gameTipTitle)
				{
					this._gameTipTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "GameTipTitle");
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00026C3A File Offset: 0x00024E3A
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00026C42 File Offset: 0x00024E42
		[DataSourceProperty]
		public bool NavigationButtonsEnabled
		{
			get
			{
				return this._navigationButtonsEnabled;
			}
			set
			{
				if (value != this._navigationButtonsEnabled)
				{
					this._navigationButtonsEnabled = value;
					base.OnPropertyChangedWithValue(value, "NavigationButtonsEnabled");
				}
			}
		}

		// Token: 0x040004E2 RID: 1250
		private MBList<string> _allTips;

		// Token: 0x040004E3 RID: 1251
		private readonly float _tipTimeInterval = 5f;

		// Token: 0x040004E4 RID: 1252
		private readonly bool _isAutoChangeEnabled;

		// Token: 0x040004E5 RID: 1253
		private int _currentTipIndex;

		// Token: 0x040004E6 RID: 1254
		private float _totalDt;

		// Token: 0x040004E7 RID: 1255
		private string _currentTip;

		// Token: 0x040004E8 RID: 1256
		private string _gameTipTitle;

		// Token: 0x040004E9 RID: 1257
		private bool _navigationButtonsEnabled;
	}
}
