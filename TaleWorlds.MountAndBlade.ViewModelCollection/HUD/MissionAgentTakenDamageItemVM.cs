using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000051 RID: 81
	public class MissionAgentTakenDamageItemVM : ViewModel
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x000184CF File Offset: 0x000166CF
		public MissionAgentTakenDamageItemVM(Camera missionCamera, Vec3 affectorAgentPos, int damage, bool isRanged, Action<MissionAgentTakenDamageItemVM> onRemove)
		{
			this._affectorAgentPosition = affectorAgentPos;
			this.Damage = damage;
			this.IsRanged = isRanged;
			this._missionCamera = missionCamera;
			this._onRemove = onRemove;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000184FC File Offset: 0x000166FC
		internal void Update()
		{
			if (this.IsRanged)
			{
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				MBWindowManager.WorldToScreen(this._missionCamera, this._affectorAgentPosition, ref num, ref num2, ref num3);
				this.ScreenPosOfAffectorAgent = new Vec2(num, num2);
				this.IsBehind = num3 < 0f;
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00018556 File Offset: 0x00016756
		public void ExecuteRemove()
		{
			this._onRemove(this);
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00018564 File Offset: 0x00016764
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0001856C File Offset: 0x0001676C
		[DataSourceProperty]
		public int Damage
		{
			get
			{
				return this._damage;
			}
			set
			{
				if (value != this._damage)
				{
					this._damage = value;
					base.OnPropertyChangedWithValue(value, "Damage");
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001858A File Offset: 0x0001678A
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00018592 File Offset: 0x00016792
		[DataSourceProperty]
		public bool IsRanged
		{
			get
			{
				return this._isRanged;
			}
			set
			{
				if (value != this._isRanged)
				{
					this._isRanged = value;
					base.OnPropertyChangedWithValue(value, "IsRanged");
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000185B0 File Offset: 0x000167B0
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x000185B8 File Offset: 0x000167B8
		[DataSourceProperty]
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (value != this._isBehind)
				{
					this._isBehind = value;
					base.OnPropertyChangedWithValue(value, "IsBehind");
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000185D6 File Offset: 0x000167D6
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x000185DE File Offset: 0x000167DE
		[DataSourceProperty]
		public Vec2 ScreenPosOfAffectorAgent
		{
			get
			{
				return this._screenPosOfAffectorAgent;
			}
			set
			{
				if (value != this._screenPosOfAffectorAgent)
				{
					this._screenPosOfAffectorAgent = value;
					base.OnPropertyChangedWithValue(value, "ScreenPosOfAffectorAgent");
				}
			}
		}

		// Token: 0x040002F5 RID: 757
		private Action<MissionAgentTakenDamageItemVM> _onRemove;

		// Token: 0x040002F6 RID: 758
		private Vec3 _affectorAgentPosition;

		// Token: 0x040002F7 RID: 759
		private Camera _missionCamera;

		// Token: 0x040002F8 RID: 760
		private int _damage;

		// Token: 0x040002F9 RID: 761
		private bool _isBehind;

		// Token: 0x040002FA RID: 762
		private bool _isRanged;

		// Token: 0x040002FB RID: 763
		private Vec2 _screenPosOfAffectorAgent;
	}
}
