using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000E9 RID: 233
	public class EncyclopediaShipSlotVM : ViewModel
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x00055632 File Offset: 0x00053832
		public EncyclopediaShipSlotVM(string slotId, bool isAvailable)
		{
			this.SlotTypeId = slotId;
			this.IsAvailable = isAvailable;
			this.RefreshValues();
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0005564E File Offset: 0x0005384E
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = GameTexts.FindText("str_ship_slot_type", this.SlotTypeId).ToString();
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00055671 File Offset: 0x00053871
		// (set) Token: 0x060015B0 RID: 5552 RVA: 0x00055679 File Offset: 0x00053879
		[DataSourceProperty]
		public string SlotTypeId
		{
			get
			{
				return this._slotTypeId;
			}
			set
			{
				if (value != this._slotTypeId)
				{
					this._slotTypeId = value;
					base.OnPropertyChangedWithValue<string>(value, "SlotTypeId");
				}
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0005569C File Offset: 0x0005389C
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000556A4 File Offset: 0x000538A4
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

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000556C7 File Offset: 0x000538C7
		// (set) Token: 0x060015B4 RID: 5556 RVA: 0x000556CF File Offset: 0x000538CF
		[DataSourceProperty]
		public bool IsAvailable
		{
			get
			{
				return this._isAvailable;
			}
			set
			{
				if (value != this._isAvailable)
				{
					this._isAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsAvailable");
				}
			}
		}

		// Token: 0x040009DC RID: 2524
		private string _slotTypeId;

		// Token: 0x040009DD RID: 2525
		private string _name;

		// Token: 0x040009DE RID: 2526
		private bool _isAvailable;
	}
}
