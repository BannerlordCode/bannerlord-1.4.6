using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000072 RID: 114
	public class OptionGroupVM : ViewModel
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0001DE24 File Offset: 0x0001C024
		public OptionGroupVM(TextObject groupName, OptionsVM optionsBase, IEnumerable<IOptionData> optionsList)
		{
			this._groupName = groupName;
			this.Options = new MBBindingList<GenericOptionDataVM>();
			foreach (IOptionData optionData in optionsList)
			{
				this.Options.Add(optionsBase.GetOptionItem(optionData));
			}
			this.RefreshValues();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001DE98 File Offset: 0x0001C098
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._groupName.ToString();
			this.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		internal List<IOptionData> GetManagedOptions()
		{
			List<IOptionData> list = new List<IOptionData>();
			foreach (GenericOptionDataVM genericOptionDataVM in this.Options)
			{
				if (!genericOptionDataVM.IsNative)
				{
					list.Add(genericOptionDataVM.GetOptionData());
				}
			}
			return list;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001DF4C File Offset: 0x0001C14C
		internal bool IsChanged()
		{
			return this.Options.Any<GenericOptionDataVM>((GenericOptionDataVM o) => o.IsChanged());
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001DF78 File Offset: 0x0001C178
		internal void Cancel()
		{
			this.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM o)
			{
				o.Cancel();
			});
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		internal void InitializeDependentConfigs(Action<IOptionData, float> updateDependentConfigs)
		{
			this.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM o)
			{
				updateDependentConfigs(o.GetOptionData(), o.GetOptionData().GetValue(false));
			});
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001DFD5 File Offset: 0x0001C1D5
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001DFDD File Offset: 0x0001C1DD
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001E000 File Offset: 0x0001C200
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001E008 File Offset: 0x0001C208
		[DataSourceProperty]
		public MBBindingList<GenericOptionDataVM> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != this._options)
				{
					this._options = value;
					base.OnPropertyChangedWithValue<MBBindingList<GenericOptionDataVM>>(value, "Options");
				}
			}
		}

		// Token: 0x040003F2 RID: 1010
		private readonly TextObject _groupName;

		// Token: 0x040003F3 RID: 1011
		private const string ControllerIdentificationModifier = "_controller";

		// Token: 0x040003F4 RID: 1012
		private string _name;

		// Token: 0x040003F5 RID: 1013
		private MBBindingList<GenericOptionDataVM> _options;
	}
}
