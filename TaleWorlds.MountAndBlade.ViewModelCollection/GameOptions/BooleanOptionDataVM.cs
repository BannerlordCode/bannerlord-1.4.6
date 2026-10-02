using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200006A RID: 106
	public class BooleanOptionDataVM : GenericOptionDataVM
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		public BooleanOptionDataVM(OptionsVM optionsVM, IBooleanOptionData option, TextObject name, TextObject description)
			: base(optionsVM, option, name, description, OptionsVM.OptionsDataType.BooleanOption)
		{
			this._booleanOptionData = option;
			this._initialValue = option.GetValue(false).Equals(1f);
			this.OptionValueAsBoolean = this._initialValue;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001C82A File Offset: 0x0001AA2A
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001C832 File Offset: 0x0001AA32
		[DataSourceProperty]
		public bool OptionValueAsBoolean
		{
			get
			{
				return this._optionValue;
			}
			set
			{
				if (value != this._optionValue)
				{
					this._optionValue = value;
					base.OnPropertyChangedWithValue(value, "OptionValueAsBoolean");
					this.UpdateValue();
				}
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001C858 File Offset: 0x0001AA58
		public override void UpdateValue()
		{
			this.Option.SetValue((float)(this.OptionValueAsBoolean ? 1 : 0));
			this.Option.Commit();
			this._optionsVM.SetConfig(this.Option, (float)(this.OptionValueAsBoolean ? 1 : 0));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001C8A6 File Offset: 0x0001AAA6
		public override void Cancel()
		{
			this.OptionValueAsBoolean = this._initialValue;
			this.UpdateValue();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001C8BA File Offset: 0x0001AABA
		public override void SetValue(float value)
		{
			this.OptionValueAsBoolean = (int)value == 1;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001C8C7 File Offset: 0x0001AAC7
		public override void ResetData()
		{
			this.OptionValueAsBoolean = (int)this.Option.GetDefaultValue() == 1;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001C8DE File Offset: 0x0001AADE
		public override bool IsChanged()
		{
			return this._initialValue != this.OptionValueAsBoolean;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
		public override void ApplyValue()
		{
			if (this._initialValue != this.OptionValueAsBoolean)
			{
				this._initialValue = this.OptionValueAsBoolean;
			}
		}

		// Token: 0x040003AB RID: 939
		private bool _initialValue;

		// Token: 0x040003AC RID: 940
		private readonly IBooleanOptionData _booleanOptionData;

		// Token: 0x040003AD RID: 941
		private bool _optionValue;
	}
}
