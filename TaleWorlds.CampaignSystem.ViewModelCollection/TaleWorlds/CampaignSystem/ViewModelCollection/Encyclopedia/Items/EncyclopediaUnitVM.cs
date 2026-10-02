using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000EF RID: 239
	public class EncyclopediaUnitVM : ViewModel
	{
		// Token: 0x060015E4 RID: 5604 RVA: 0x00055E84 File Offset: 0x00054084
		public EncyclopediaUnitVM(CharacterObject character, bool isActive)
		{
			if (character != null)
			{
				CharacterCode characterCode = CharacterCode.CreateFrom(character);
				this.ImageIdentifier = new CharacterImageIdentifierVM(characterCode);
				this._character = character;
				this.IsActiveUnit = isActive;
				this.TierIconData = CampaignUIHelper.GetCharacterTierData(character, true);
				this.TypeIconData = CampaignUIHelper.GetCharacterTypeData(character, true);
			}
			else
			{
				this.IsActiveUnit = false;
			}
			this.RefreshValues();
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00055EE4 File Offset: 0x000540E4
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._character != null)
			{
				this.NameText = this._character.Name.ToString();
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00055F0A File Offset: 0x0005410A
		public void ExecuteLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._character.EncyclopediaLink);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00055F26 File Offset: 0x00054126
		public virtual void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(CharacterObject), new object[] { this._character });
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00055F46 File Offset: 0x00054146
		public virtual void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00055F4D File Offset: 0x0005414D
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x00055F55 File Offset: 0x00054155
		[DataSourceProperty]
		public bool IsActiveUnit
		{
			get
			{
				return this._isActiveUnit;
			}
			set
			{
				if (value != this._isActiveUnit)
				{
					this._isActiveUnit = value;
					base.OnPropertyChangedWithValue(value, "IsActiveUnit");
				}
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x00055F73 File Offset: 0x00054173
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x00055F7B File Offset: 0x0005417B
		[DataSourceProperty]
		public CharacterImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00055F99 File Offset: 0x00054199
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x00055FA1 File Offset: 0x000541A1
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00055FC4 File Offset: 0x000541C4
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x00055FCC File Offset: 0x000541CC
		[DataSourceProperty]
		public StringItemWithHintVM TierIconData
		{
			get
			{
				return this._tierIconData;
			}
			set
			{
				if (value != this._tierIconData)
				{
					this._tierIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TierIconData");
				}
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00055FEA File Offset: 0x000541EA
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x00055FF2 File Offset: 0x000541F2
		[DataSourceProperty]
		public StringItemWithHintVM TypeIconData
		{
			get
			{
				return this._typeIconData;
			}
			set
			{
				if (value != this._typeIconData)
				{
					this._typeIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TypeIconData");
				}
			}
		}

		// Token: 0x040009F4 RID: 2548
		private CharacterObject _character;

		// Token: 0x040009F5 RID: 2549
		private CharacterImageIdentifierVM _imageIdentifier;

		// Token: 0x040009F6 RID: 2550
		private string _nameText;

		// Token: 0x040009F7 RID: 2551
		private bool _isActiveUnit;

		// Token: 0x040009F8 RID: 2552
		private StringItemWithHintVM _tierIconData;

		// Token: 0x040009F9 RID: 2553
		private StringItemWithHintVM _typeIconData;
	}
}
