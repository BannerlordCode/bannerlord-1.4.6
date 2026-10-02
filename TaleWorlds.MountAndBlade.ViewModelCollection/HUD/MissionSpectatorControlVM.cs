using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000058 RID: 88
	public class MissionSpectatorControlVM : ViewModel
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0001A5D6 File Offset: 0x000187D6
		public MissionSpectatorControlVM(Mission mission)
		{
			this._mission = mission;
			this.RefreshValues();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001A5FC File Offset: 0x000187FC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PrevCharacterText = new TextObject("{=BANC61K5}Previous Character", null).ToString();
			this.NextCharacterText = new TextObject("{=znKxunbQ}Next Character", null).ToString();
			this.TakeControlText = new TextObject("{=TGpbi44D}Take Control of Character", null).ToString();
			this.UpdateStatusText();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001A657 File Offset: 0x00018857
		public void OnSpectatedAgentFocusIn(Agent followedAgent)
		{
			MissionPeer missionPeer = followedAgent.MissionPeer;
			this.SpectatedAgentName = ((missionPeer != null) ? missionPeer.DisplayedName : null) ?? followedAgent.Name;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001A67B File Offset: 0x0001887B
		public void OnSpectatedAgentFocusOut(Agent followedAgent)
		{
			this.SpectatedAgentName = "";
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001A688 File Offset: 0x00018888
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM prevCharacterKey = this.PrevCharacterKey;
			if (prevCharacterKey != null)
			{
				prevCharacterKey.OnFinalize();
			}
			InputKeyItemVM nextCharacterKey = this.NextCharacterKey;
			if (nextCharacterKey != null)
			{
				nextCharacterKey.OnFinalize();
			}
			InputKeyItemVM takeControlKey = this.TakeControlKey;
			if (takeControlKey == null)
			{
				return;
			}
			takeControlKey.OnFinalize();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001A6C2 File Offset: 0x000188C2
		public void SetMainAgentStatus(bool isDead)
		{
			if (this._isMainHeroDead != isDead)
			{
				this._isMainHeroDead = isDead;
				this.UpdateStatusText();
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001A6DA File Offset: 0x000188DA
		private void UpdateStatusText()
		{
			if (this._isMainHeroDead)
			{
				this.StatusText = this._deadTextObject.ToString();
				return;
			}
			this.StatusText = string.Empty;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001A701 File Offset: 0x00018901
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001A709 File Offset: 0x00018909
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001A727 File Offset: 0x00018927
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001A72F File Offset: 0x0001892F
		[DataSourceProperty]
		public string PrevCharacterText
		{
			get
			{
				return this._prevCharacterText;
			}
			set
			{
				if (value != this._prevCharacterText)
				{
					this._prevCharacterText = value;
					base.OnPropertyChangedWithValue<string>(value, "PrevCharacterText");
				}
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001A752 File Offset: 0x00018952
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001A75A File Offset: 0x0001895A
		[DataSourceProperty]
		public string NextCharacterText
		{
			get
			{
				return this._nextCharacterText;
			}
			set
			{
				if (value != this._nextCharacterText)
				{
					this._nextCharacterText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextCharacterText");
				}
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001A77D File Offset: 0x0001897D
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001A785 File Offset: 0x00018985
		[DataSourceProperty]
		public string TakeControlText
		{
			get
			{
				return this._takeControlText;
			}
			set
			{
				if (value != this._takeControlText)
				{
					this._takeControlText = value;
					base.OnPropertyChangedWithValue<string>(value, "TakeControlText");
				}
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001A7A8 File Offset: 0x000189A8
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001A7B0 File Offset: 0x000189B0
		[DataSourceProperty]
		public string StatusText
		{
			get
			{
				return this._statusText;
			}
			set
			{
				if (value != this._statusText)
				{
					this._statusText = value;
					base.OnPropertyChangedWithValue<string>(value, "StatusText");
				}
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001A7D3 File Offset: 0x000189D3
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001A7DB File Offset: 0x000189DB
		[DataSourceProperty]
		public bool IsTakeControlRelevant
		{
			get
			{
				return this._isTakeControlRelevant;
			}
			set
			{
				if (value != this._isTakeControlRelevant)
				{
					this._isTakeControlRelevant = value;
					base.OnPropertyChangedWithValue(value, "IsTakeControlRelevant");
				}
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001A7F9 File Offset: 0x000189F9
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001A801 File Offset: 0x00018A01
		[DataSourceProperty]
		public bool IsTakeControlEnabled
		{
			get
			{
				return this._isTakeControlEnabled;
			}
			set
			{
				if (value != this._isTakeControlEnabled)
				{
					this._isTakeControlEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTakeControlEnabled");
				}
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001A81F File Offset: 0x00018A1F
		public void SetPrevCharacterInputKey(GameKey gameKey)
		{
			this.PrevCharacterKey = InputKeyItemVM.CreateFromGameKey(gameKey, false);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001A82E File Offset: 0x00018A2E
		public void SetNextCharacterInputKey(GameKey gameKey)
		{
			this.NextCharacterKey = InputKeyItemVM.CreateFromGameKey(gameKey, false);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001A83D File Offset: 0x00018A3D
		public void SetTakeControlInputKey(GameKey gameKey)
		{
			this.TakeControlKey = InputKeyItemVM.CreateFromGameKey(gameKey, false);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001A84C File Offset: 0x00018A4C
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001A854 File Offset: 0x00018A54
		[DataSourceProperty]
		public string SpectatedAgentName
		{
			get
			{
				return this._spectatedAgentName;
			}
			set
			{
				if (value != this._spectatedAgentName)
				{
					this._spectatedAgentName = value;
					base.OnPropertyChangedWithValue<string>(value, "SpectatedAgentName");
				}
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001A877 File Offset: 0x00018A77
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001A87F File Offset: 0x00018A7F
		[DataSourceProperty]
		public InputKeyItemVM PrevCharacterKey
		{
			get
			{
				return this._prevCharacterKey;
			}
			set
			{
				if (value != this._prevCharacterKey)
				{
					this._prevCharacterKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PrevCharacterKey");
				}
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001A89D File Offset: 0x00018A9D
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0001A8A5 File Offset: 0x00018AA5
		[DataSourceProperty]
		public InputKeyItemVM NextCharacterKey
		{
			get
			{
				return this._nextCharacterKey;
			}
			set
			{
				if (value != this._nextCharacterKey)
				{
					this._nextCharacterKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextCharacterKey");
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001A8C3 File Offset: 0x00018AC3
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001A8CB File Offset: 0x00018ACB
		[DataSourceProperty]
		public InputKeyItemVM TakeControlKey
		{
			get
			{
				return this._takeControlKey;
			}
			set
			{
				if (value != this._takeControlKey)
				{
					this._takeControlKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "TakeControlKey");
				}
			}
		}

		// Token: 0x04000334 RID: 820
		private readonly Mission _mission;

		// Token: 0x04000335 RID: 821
		private bool _isMainHeroDead;

		// Token: 0x04000336 RID: 822
		private readonly TextObject _deadTextObject = GameTexts.FindText("str_battle_hero_dead", null);

		// Token: 0x04000337 RID: 823
		private bool _isEnabled;

		// Token: 0x04000338 RID: 824
		private string _prevCharacterText;

		// Token: 0x04000339 RID: 825
		private string _nextCharacterText;

		// Token: 0x0400033A RID: 826
		private string _takeControlText;

		// Token: 0x0400033B RID: 827
		private string _statusText;

		// Token: 0x0400033C RID: 828
		private bool _isTakeControlRelevant;

		// Token: 0x0400033D RID: 829
		private bool _isTakeControlEnabled;

		// Token: 0x0400033E RID: 830
		private string _spectatedAgentName;

		// Token: 0x0400033F RID: 831
		private InputKeyItemVM _prevCharacterKey;

		// Token: 0x04000340 RID: 832
		private InputKeyItemVM _nextCharacterKey;

		// Token: 0x04000341 RID: 833
		private InputKeyItemVM _takeControlKey;
	}
}
