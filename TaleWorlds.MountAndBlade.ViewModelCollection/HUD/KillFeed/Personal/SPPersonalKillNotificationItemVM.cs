using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.Personal
{
	// Token: 0x0200005D RID: 93
	public class SPPersonalKillNotificationItemVM : ViewModel
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001ADA1 File Offset: 0x00018FA1
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0001ADA9 File Offset: 0x00018FA9
		private SPPersonalKillNotificationItemVM.ItemTypes ItemTypeAsEnum
		{
			get
			{
				return this._itemTypeAsEnum;
			}
			set
			{
				this._itemType = (int)value;
				this._itemTypeAsEnum = value;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001ADBC File Offset: 0x00018FBC
		public SPPersonalKillNotificationItemVM(int damageAmount, bool isMountDamage, bool isFriendlyFire, bool isHeadshot, string killedAgentName, bool isUnconscious, Action<SPPersonalKillNotificationItemVM> onRemoveItem)
		{
			this._onRemoveItem = onRemoveItem;
			this.Amount = damageAmount;
			if (isFriendlyFire)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.FriendlyFireKill;
				this.Message = killedAgentName;
				return;
			}
			if (isMountDamage)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.MountDamage;
				this.Message = GameTexts.FindText("str_damage_delivered_message", null).ToString();
				return;
			}
			this.ItemTypeAsEnum = (isUnconscious ? (isHeadshot ? SPPersonalKillNotificationItemVM.ItemTypes.MakeUnconsciousHeadshot : SPPersonalKillNotificationItemVM.ItemTypes.MakeUnconscious) : (isHeadshot ? SPPersonalKillNotificationItemVM.ItemTypes.NormalKillHeadshot : SPPersonalKillNotificationItemVM.ItemTypes.NormalKill));
			this.Message = killedAgentName;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001AE38 File Offset: 0x00019038
		public SPPersonalKillNotificationItemVM(int amount, bool isMountDamage, bool isFriendlyFire, string killedAgentName, Action<SPPersonalKillNotificationItemVM> onRemoveItem)
		{
			this._onRemoveItem = onRemoveItem;
			this.Amount = amount;
			if (isFriendlyFire)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.FriendlyFireDamage;
				this.Message = killedAgentName;
				return;
			}
			if (isMountDamage)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.MountDamage;
				this.Message = GameTexts.FindText("str_damage_delivered_message", null).ToString();
				return;
			}
			this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.NormalDamage;
			this.Message = GameTexts.FindText("str_damage_delivered_message", null).ToString();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001AEAB File Offset: 0x000190AB
		public SPPersonalKillNotificationItemVM(string victimAgentName, Action<SPPersonalKillNotificationItemVM> onRemoveItem)
		{
			this._onRemoveItem = onRemoveItem;
			this.Amount = -1;
			this.Message = victimAgentName;
			this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.Message;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001AED0 File Offset: 0x000190D0
		public void ExecuteRemove()
		{
			this._onRemoveItem(this);
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001AEDE File Offset: 0x000190DE
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x0001AEE6 File Offset: 0x000190E6
		[DataSourceProperty]
		public string VictimType
		{
			get
			{
				return this._victimType;
			}
			set
			{
				if (value != this._victimType)
				{
					this._victimType = value;
					base.OnPropertyChangedWithValue<string>(value, "VictimType");
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001AF09 File Offset: 0x00019109
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0001AF11 File Offset: 0x00019111
		[DataSourceProperty]
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				if (value != this._message)
				{
					this._message = value;
					base.OnPropertyChangedWithValue<string>(value, "Message");
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001AF34 File Offset: 0x00019134
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0001AF3C File Offset: 0x0001913C
		[DataSourceProperty]
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (value != this._itemType)
				{
					this._itemType = value;
					base.OnPropertyChangedWithValue(value, "ItemType");
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001AF5A File Offset: 0x0001915A
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x0001AF62 File Offset: 0x00019162
		[DataSourceProperty]
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if (value != this._amount)
				{
					this._amount = value;
					base.OnPropertyChangedWithValue(value, "Amount");
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001AF80 File Offset: 0x00019180
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x0001AF88 File Offset: 0x00019188
		[DataSourceProperty]
		public bool IsPaused
		{
			get
			{
				return this._isPaused;
			}
			set
			{
				if (value != this._isPaused)
				{
					this._isPaused = value;
					base.OnPropertyChangedWithValue(value, "IsPaused");
				}
			}
		}

		// Token: 0x04000353 RID: 851
		private Action<SPPersonalKillNotificationItemVM> _onRemoveItem;

		// Token: 0x04000354 RID: 852
		private SPPersonalKillNotificationItemVM.ItemTypes _itemTypeAsEnum;

		// Token: 0x04000355 RID: 853
		private string _message;

		// Token: 0x04000356 RID: 854
		private string _victimType;

		// Token: 0x04000357 RID: 855
		private int _amount;

		// Token: 0x04000358 RID: 856
		private int _itemType;

		// Token: 0x04000359 RID: 857
		private bool _isPaused;

		// Token: 0x020000F6 RID: 246
		private enum ItemTypes
		{
			// Token: 0x0400067B RID: 1659
			NormalDamage,
			// Token: 0x0400067C RID: 1660
			FriendlyFireDamage,
			// Token: 0x0400067D RID: 1661
			FriendlyFireKill,
			// Token: 0x0400067E RID: 1662
			MountDamage,
			// Token: 0x0400067F RID: 1663
			NormalKill,
			// Token: 0x04000680 RID: 1664
			Assist,
			// Token: 0x04000681 RID: 1665
			MakeUnconscious,
			// Token: 0x04000682 RID: 1666
			NormalKillHeadshot,
			// Token: 0x04000683 RID: 1667
			MakeUnconsciousHeadshot,
			// Token: 0x04000684 RID: 1668
			Message
		}
	}
}
