using System;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000157 RID: 343
	public abstract class CharacterCreationStageBaseVM : ViewModel
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x000764B8 File Offset: 0x000746B8
		protected CharacterCreationStageBaseVM(CharacterCreationManager characterCreationManager, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText)
		{
			this.CharacterCreationManager = characterCreationManager;
			this._affirmativeAction = affirmativeAction;
			this._negativeAction = negativeAction;
			this._affirmativeActionText = affirmativeActionText;
			this._negativeActionText = negativeActionText;
			TextObject affirmativeActionText2 = this._affirmativeActionText;
			this.NextStageText = ((affirmativeActionText2 != null) ? affirmativeActionText2.ToString() : null);
			TextObject negativeActionText2 = this._negativeActionText;
			this.PreviousStageText = ((negativeActionText2 != null) ? negativeActionText2.ToString() : null);
		}

		// Token: 0x06002054 RID: 8276
		public abstract void OnNextStage();

		// Token: 0x06002055 RID: 8277
		public abstract void OnPreviousStage();

		// Token: 0x06002056 RID: 8278
		public abstract bool CanAdvanceToNextStage();

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00076556 File Offset: 0x00074756
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0007655E File Offset: 0x0007475E
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00076581 File Offset: 0x00074781
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x00076589 File Offset: 0x00074789
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x000765AC File Offset: 0x000747AC
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x000765B4 File Offset: 0x000747B4
		[DataSourceProperty]
		public string SelectionText
		{
			get
			{
				return this._selectionText;
			}
			set
			{
				if (value != this._selectionText)
				{
					this._selectionText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectionText");
				}
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x000765D7 File Offset: 0x000747D7
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x000765DF File Offset: 0x000747DF
		[DataSourceProperty]
		public string NextStageText
		{
			get
			{
				return this._nextStageText;
			}
			set
			{
				if (value != this._nextStageText)
				{
					this._nextStageText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextStageText");
				}
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00076602 File Offset: 0x00074802
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x0007660A File Offset: 0x0007480A
		[DataSourceProperty]
		public string PreviousStageText
		{
			get
			{
				return this._previousStageText;
			}
			set
			{
				if (value != this._previousStageText)
				{
					this._previousStageText = value;
					base.OnPropertyChangedWithValue<string>(value, "PreviousStageText");
				}
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0007662D File Offset: 0x0007482D
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x00076635 File Offset: 0x00074835
		[DataSourceProperty]
		public int TotalStageCount
		{
			get
			{
				return this._totalStageCount;
			}
			set
			{
				if (value != this._totalStageCount)
				{
					this._totalStageCount = value;
					base.OnPropertyChangedWithValue(value, "TotalStageCount");
				}
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00076653 File Offset: 0x00074853
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x0007665B File Offset: 0x0007485B
		[DataSourceProperty]
		public int FurthestIndex
		{
			get
			{
				return this._furthestIndex;
			}
			set
			{
				if (value != this._furthestIndex)
				{
					this._furthestIndex = value;
					base.OnPropertyChangedWithValue(value, "FurthestIndex");
				}
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00076679 File Offset: 0x00074879
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x00076681 File Offset: 0x00074881
		[DataSourceProperty]
		public int CurrentStageIndex
		{
			get
			{
				return this._currentStageIndex;
			}
			set
			{
				if (value != this._currentStageIndex)
				{
					this._currentStageIndex = value;
					base.OnPropertyChangedWithValue(value, "CurrentStageIndex");
				}
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0007669F File Offset: 0x0007489F
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x000766A7 File Offset: 0x000748A7
		[DataSourceProperty]
		public bool AnyItemSelected
		{
			get
			{
				return this._anyItemSelected;
			}
			set
			{
				if (value != this._anyItemSelected)
				{
					this._anyItemSelected = value;
					base.OnPropertyChangedWithValue(value, "AnyItemSelected");
				}
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x000766C5 File Offset: 0x000748C5
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x000766CD File Offset: 0x000748CD
		[DataSourceProperty]
		public bool CanAdvance
		{
			get
			{
				return this._canAdvance;
			}
			set
			{
				if (value != this._canAdvance)
				{
					this._canAdvance = value;
					base.OnPropertyChangedWithValue(value, "CanAdvance");
				}
			}
		}

		// Token: 0x04000F08 RID: 3848
		protected readonly CharacterCreationManager CharacterCreationManager;

		// Token: 0x04000F09 RID: 3849
		protected readonly Action _affirmativeAction;

		// Token: 0x04000F0A RID: 3850
		protected readonly Action _negativeAction;

		// Token: 0x04000F0B RID: 3851
		protected readonly TextObject _affirmativeActionText;

		// Token: 0x04000F0C RID: 3852
		protected readonly TextObject _negativeActionText;

		// Token: 0x04000F0D RID: 3853
		private string _title = "";

		// Token: 0x04000F0E RID: 3854
		private string _description = "";

		// Token: 0x04000F0F RID: 3855
		private string _selectionText = "";

		// Token: 0x04000F10 RID: 3856
		private string _nextStageText;

		// Token: 0x04000F11 RID: 3857
		private string _previousStageText;

		// Token: 0x04000F12 RID: 3858
		private int _totalStageCount = -1;

		// Token: 0x04000F13 RID: 3859
		private int _currentStageIndex = -1;

		// Token: 0x04000F14 RID: 3860
		private int _furthestIndex = -1;

		// Token: 0x04000F15 RID: 3861
		private bool _anyItemSelected;

		// Token: 0x04000F16 RID: 3862
		private bool _canAdvance;
	}
}
