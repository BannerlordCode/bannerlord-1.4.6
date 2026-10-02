using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x0200001B RID: 27
	public class PartyPlayerNameplateVM : PartyNameplateVM
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000B80B File Offset: 0x00009A0B
		public PartyPlayerNameplateVM()
		{
			this.IsMainParty = true;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B828 File Offset: 0x00009A28
		public void InitializePlayerNameplate(Action resetCamera)
		{
			this._isPartyHeroVisualDirty = true;
			this._resetCamera = resetCamera;
			bool flag;
			if (this.IsMainParty && base.Party.LeaderHero == null)
			{
				Hero mainHero = Hero.MainHero;
				flag = mainHero != null && mainHero.IsAlive;
			}
			else
			{
				flag = false;
			}
			this._isPrisonerBind = flag;
			this.MainHeroVisual = new CharacterImageIdentifierVM(CampaignUIHelper.GetCharacterCode(Hero.MainHero.CharacterObject, false));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B88D File Offset: 0x00009A8D
		public override void Clear()
		{
			base.Clear();
			base.IsInSettlement = true;
			base.IsVisibleOnMap = false;
			this.MainHeroVisual = null;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public override void RefreshDynamicProperties(bool forceUpdate)
		{
			base.RefreshDynamicProperties(forceUpdate);
			if ((this.IsMainParty && MathF.Abs(Hero.MainHero.Age - this._latestMainHeroAge) >= 1f) || forceUpdate)
			{
				this._latestMainHeroAge = Hero.MainHero.Age;
				this._isPartyHeroVisualDirty = true;
			}
			if (this._isPartyHeroVisualDirty || forceUpdate)
			{
				this._mainHeroVisualBind = new CharacterImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(Hero.MainHero.CharacterObject, false));
				this._isPartyHeroVisualDirty = false;
			}
			bool flag;
			if (this.IsMainParty && base.Party.LeaderHero == null)
			{
				Hero mainHero = Hero.MainHero;
				flag = mainHero != null && mainHero.IsAlive;
			}
			else
			{
				flag = false;
			}
			this._isPrisonerBind = flag;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B961 File Offset: 0x00009B61
		public override void RefreshBinding()
		{
			base.RefreshBinding();
			this.IsPrisoner = this._isPrisonerBind;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B978 File Offset: 0x00009B78
		public override void RefreshPosition()
		{
			Vec3 vec = (base.Party.Position + base.Party.EventPositionAdder).AsVec3();
			Vec3 vec2 = vec + new Vec3(0f, 0f, 0.8f, -1f);
			this._latestX = 0f;
			this._latestY = 0f;
			this._latestW = 0f;
			MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, vec, ref this._latestX, ref this._latestY, ref this._latestW);
			this._partyPositionBind = new Vec2(this._latestX, this._latestY);
			this._isHighBind = this._mapCamera.Position.Distance(vec) >= 110f;
			this._isBehindBind = this._latestW < 0f;
			MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, vec2, ref this._latestX, ref this._latestY, ref this._latestW);
			this._headPositionBind = new Vec2(this._latestX, this._latestY);
			base.DistanceToCamera = vec.Distance(this._mapCamera.Position);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		public void ExecuteSetCameraPosition()
		{
			this._resetCamera();
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000BAB2 File Offset: 0x00009CB2
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000BABA File Offset: 0x00009CBA
		[DataSourceProperty]
		public bool IsMainParty
		{
			get
			{
				return this._isMainParty;
			}
			set
			{
				if (value != this._isMainParty)
				{
					this._isMainParty = value;
					base.OnPropertyChangedWithValue(value, "IsMainParty");
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		[DataSourceProperty]
		public bool IsPrisoner
		{
			get
			{
				return this._isPrisoner;
			}
			set
			{
				if (value != this._isPrisoner)
				{
					this._isPrisoner = value;
					base.OnPropertyChangedWithValue(value, "IsPrisoner");
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000BAFE File Offset: 0x00009CFE
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000BB06 File Offset: 0x00009D06
		[DataSourceProperty]
		public CharacterImageIdentifierVM MainHeroVisual
		{
			get
			{
				return this._mainHeroVisual;
			}
			set
			{
				if (value != this._mainHeroVisual)
				{
					this._mainHeroVisual = value;
					base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "MainHeroVisual");
				}
			}
		}

		// Token: 0x0400014E RID: 334
		private float _latestMainHeroAge = -1f;

		// Token: 0x0400014F RID: 335
		private bool _isPartyHeroVisualDirty;

		// Token: 0x04000150 RID: 336
		private Action _resetCamera;

		// Token: 0x04000151 RID: 337
		private CharacterImageIdentifierVM _mainHeroVisualBind;

		// Token: 0x04000152 RID: 338
		private bool _isPrisonerBind;

		// Token: 0x04000153 RID: 339
		private bool _isMainParty;

		// Token: 0x04000154 RID: 340
		private bool _isPrisoner;

		// Token: 0x04000155 RID: 341
		private CharacterImageIdentifierVM _mainHeroVisual;
	}
}
