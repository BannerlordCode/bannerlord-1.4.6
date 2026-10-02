using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Map.Cheat
{
	// Token: 0x02000050 RID: 80
	public abstract class CheatItemBaseVM : ViewModel
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00012D07 File Offset: 0x00010F07
		public CheatItemBaseVM()
		{
		}

		// Token: 0x060004EB RID: 1259
		public abstract void ExecuteAction();

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00012D0F File Offset: 0x00010F0F
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00012D17 File Offset: 0x00010F17
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

		// Token: 0x0400026D RID: 621
		private string _name;
	}
}
