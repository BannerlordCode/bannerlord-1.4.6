using System;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200011F RID: 287
	public class ClanCardSelectionPopupItemVM : ViewModel
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0006342A File Offset: 0x0006162A
		public object Identifier { get; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00063432 File Offset: 0x00061632
		public TextObject ActionResultText { get; }

		// Token: 0x06001A3D RID: 6717 RVA: 0x0006343C File Offset: 0x0006163C
		public ClanCardSelectionPopupItemVM(in ClanCardSelectionItemInfo info, Action<ClanCardSelectionPopupItemVM> onSelected)
		{
			this.Identifier = info.Identifier;
			this._onSelected = onSelected;
			this.ActionResultText = info.ActionResult;
			this._titleText = info.Title;
			this._disabledReasonText = info.DisabledReason;
			this._specialActionText = info.SpecialActionText;
			this.DisabledHint = new HintViewModel();
			this.Properties = new MBBindingList<ClanCardSelectionPopupItemPropertyVM>();
			if (info.Properties != null)
			{
				foreach (ClanCardSelectionItemPropertyInfo clanCardSelectionItemPropertyInfo in info.Properties)
				{
					this.Properties.Add(new ClanCardSelectionPopupItemPropertyVM(in clanCardSelectionItemPropertyInfo));
				}
			}
			this.IsDisabled = info.IsDisabled;
			this.IsSpecialActionItem = info.IsSpecialActionItem;
			this.HasSprite = !string.IsNullOrEmpty(info.SpriteName);
			this.HasImage = info.Image != null;
			this.SpriteType = info.SpriteType.ToString();
			this.SpriteName = info.SpriteName ?? string.Empty;
			this.SpriteLabel = info.SpriteLabel ?? string.Empty;
			this.Image = new GenericImageIdentifierVM(info.Image);
			this.RefreshValues();
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00063590 File Offset: 0x00061790
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject titleText = this._titleText;
			this.Title = ((titleText != null) ? titleText.ToString() : null) ?? string.Empty;
			TextObject specialActionText = this._specialActionText;
			this.SpecialAction = ((specialActionText != null) ? specialActionText.ToString() : null) ?? string.Empty;
			this.DisabledHint.HintText = (this.IsDisabled ? this._disabledReasonText : TextObject.GetEmpty());
			this.Properties.ApplyActionOnAllItems(delegate(ClanCardSelectionPopupItemPropertyVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0006362F File Offset: 0x0006182F
		public void ExecuteSelect()
		{
			Action<ClanCardSelectionPopupItemVM> onSelected = this._onSelected;
			if (onSelected == null)
			{
				return;
			}
			onSelected(this);
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00063642 File Offset: 0x00061842
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x0006364A File Offset: 0x0006184A
		[DataSourceProperty]
		public ImageIdentifierVM Image
		{
			get
			{
				return this._image;
			}
			set
			{
				if (value != this._image)
				{
					this._image = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Image");
				}
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00063668 File Offset: 0x00061868
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00063670 File Offset: 0x00061870
		[DataSourceProperty]
		public MBBindingList<ClanCardSelectionPopupItemPropertyVM> Properties
		{
			get
			{
				return this._properties;
			}
			set
			{
				if (value != this._properties)
				{
					this._properties = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanCardSelectionPopupItemPropertyVM>>(value, "Properties");
				}
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0006368E File Offset: 0x0006188E
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00063696 File Offset: 0x00061896
		[DataSourceProperty]
		public HintViewModel DisabledHint
		{
			get
			{
				return this._disabledHint;
			}
			set
			{
				if (value != this._disabledHint)
				{
					this._disabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledHint");
				}
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x000636B4 File Offset: 0x000618B4
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x000636BC File Offset: 0x000618BC
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

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x000636DF File Offset: 0x000618DF
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x000636E7 File Offset: 0x000618E7
		[DataSourceProperty]
		public string SpriteType
		{
			get
			{
				return this._spriteType;
			}
			set
			{
				if (value != this._spriteType)
				{
					this._spriteType = value;
					base.OnPropertyChangedWithValue<string>(value, "SpriteType");
				}
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0006370A File Offset: 0x0006190A
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00063712 File Offset: 0x00061912
		[DataSourceProperty]
		public string SpriteName
		{
			get
			{
				return this._spriteName;
			}
			set
			{
				if (value != this._spriteName)
				{
					this._spriteName = value;
					base.OnPropertyChangedWithValue<string>(value, "SpriteName");
				}
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00063735 File Offset: 0x00061935
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x0006373D File Offset: 0x0006193D
		[DataSourceProperty]
		public string SpriteLabel
		{
			get
			{
				return this._spriteLabel;
			}
			set
			{
				if (value != this._spriteLabel)
				{
					this._spriteLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "SpriteLabel");
				}
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00063760 File Offset: 0x00061960
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x00063768 File Offset: 0x00061968
		[DataSourceProperty]
		public string SpecialAction
		{
			get
			{
				return this._specialAction;
			}
			set
			{
				if (value != this._specialAction)
				{
					this._specialAction = value;
					base.OnPropertyChangedWithValue<string>(value, "SpecialAction");
				}
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0006378B File Offset: 0x0006198B
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00063793 File Offset: 0x00061993
		[DataSourceProperty]
		public bool HasImage
		{
			get
			{
				return this._hasImage;
			}
			set
			{
				if (value != this._hasImage)
				{
					this._hasImage = value;
					base.OnPropertyChangedWithValue(value, "HasImage");
				}
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000637B1 File Offset: 0x000619B1
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x000637B9 File Offset: 0x000619B9
		[DataSourceProperty]
		public bool HasSprite
		{
			get
			{
				return this._hasSprite;
			}
			set
			{
				if (value != this._hasSprite)
				{
					this._hasSprite = value;
					base.OnPropertyChangedWithValue(value, "HasSprite");
				}
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000637D7 File Offset: 0x000619D7
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x000637DF File Offset: 0x000619DF
		[DataSourceProperty]
		public bool IsSpecialActionItem
		{
			get
			{
				return this._isSpecialActionItem;
			}
			set
			{
				if (value != this._isSpecialActionItem)
				{
					this._isSpecialActionItem = value;
					base.OnPropertyChangedWithValue(value, "IsSpecialActionItem");
				}
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000637FD File Offset: 0x000619FD
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00063805 File Offset: 0x00061A05
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00063823 File Offset: 0x00061A23
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x0006382B File Offset: 0x00061A2B
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x04000C22 RID: 3106
		private readonly TextObject _titleText;

		// Token: 0x04000C23 RID: 3107
		private readonly TextObject _disabledReasonText;

		// Token: 0x04000C24 RID: 3108
		private readonly TextObject _specialActionText;

		// Token: 0x04000C25 RID: 3109
		private readonly Action<ClanCardSelectionPopupItemVM> _onSelected;

		// Token: 0x04000C26 RID: 3110
		private ImageIdentifierVM _image;

		// Token: 0x04000C27 RID: 3111
		private MBBindingList<ClanCardSelectionPopupItemPropertyVM> _properties;

		// Token: 0x04000C28 RID: 3112
		private HintViewModel _disabledHint;

		// Token: 0x04000C29 RID: 3113
		private string _title;

		// Token: 0x04000C2A RID: 3114
		private string _spriteType;

		// Token: 0x04000C2B RID: 3115
		private string _spriteName;

		// Token: 0x04000C2C RID: 3116
		private string _spriteLabel;

		// Token: 0x04000C2D RID: 3117
		private string _specialAction;

		// Token: 0x04000C2E RID: 3118
		private bool _hasImage;

		// Token: 0x04000C2F RID: 3119
		private bool _hasSprite;

		// Token: 0x04000C30 RID: 3120
		private bool _isSpecialActionItem;

		// Token: 0x04000C31 RID: 3121
		private bool _isDisabled;

		// Token: 0x04000C32 RID: 3122
		private bool _isSelected;
	}
}
