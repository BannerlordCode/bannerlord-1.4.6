using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Helpers;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Barter
{
	// Token: 0x0200015A RID: 346
	public class BarterVM : ViewModel
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x00076DEC File Offset: 0x00074FEC
		public BarterVM(BarterData args)
		{
			this._barterData = args;
			if (this._barterData.OtherHero == Hero.MainHero)
			{
				this._otherParty = this._barterData.OffererParty;
				this._otherCharacter = this._barterData.OffererHero.CharacterObject ?? CampaignUIHelper.GetVisualPartyLeader(this._otherParty);
			}
			else if (this._barterData.OtherHero != null)
			{
				this._otherCharacter = this._barterData.OtherHero.CharacterObject;
				this.LeftMaxGold = this._otherCharacter.HeroObject.Gold;
			}
			else
			{
				this._otherParty = this._barterData.OtherParty;
				this._otherCharacter = CampaignUIHelper.GetVisualPartyLeader(this._otherParty);
				this.LeftMaxGold = this._otherParty.MobileParty.PartyTradeGold;
			}
			this._barter = Campaign.Current.BarterManager;
			this._isPlayerOfferer = this._barterData.OffererHero == Hero.MainHero;
			this.AutoBalanceHint = new HintViewModel();
			this.LeftFiefList = new MBBindingList<BarterItemVM>();
			this.RightFiefList = new MBBindingList<BarterItemVM>();
			this.LeftPrisonerList = new MBBindingList<BarterItemVM>();
			this.RightPrisonerList = new MBBindingList<BarterItemVM>();
			this.LeftItemList = new MBBindingList<BarterItemVM>();
			this.RightItemList = new MBBindingList<BarterItemVM>();
			this.LeftOtherList = new MBBindingList<BarterItemVM>();
			this.RightOtherList = new MBBindingList<BarterItemVM>();
			this.LeftDiplomaticList = new MBBindingList<BarterItemVM>();
			this.RightDiplomaticList = new MBBindingList<BarterItemVM>();
			this.LeftGoldList = new MBBindingList<BarterItemVM>();
			this.RightGoldList = new MBBindingList<BarterItemVM>();
			this._leftList = new Dictionary<BarterGroup, MBBindingList<BarterItemVM>>();
			this._rightList = new Dictionary<BarterGroup, MBBindingList<BarterItemVM>>();
			this._barterList = new List<Dictionary<BarterGroup, MBBindingList<BarterItemVM>>>();
			this._offerList = new List<MBBindingList<BarterItemVM>>();
			this.LeftOfferList = new MBBindingList<BarterItemVM>();
			this.RightOfferList = new MBBindingList<BarterItemVM>();
			this.InitBarterList(this._barterData);
			this.OnInitialized();
			this.RightMaxGold = Hero.MainHero.Gold;
			this.LeftHero = new HeroVM(this._otherCharacter.HeroObject, false);
			this.RightHero = new HeroVM(Hero.MainHero, false);
			this.SendOffer();
			this.InitializationIsOver = true;
			this.RefreshValues();
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00077028 File Offset: 0x00075228
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.InitializeStaticContent();
			this.LeftNameLbl = this._otherCharacter.Name.ToString();
			this.RightNameLbl = Hero.MainHero.Name.ToString();
			this.LeftFiefList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightFiefList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftPrisonerList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightPrisonerList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftItemList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightItemList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftOtherList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightOtherList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftDiplomaticList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightDiplomaticList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.LeftGoldList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
			this.RightGoldList.ApplyActionOnAllItems(delegate(BarterItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00077264 File Offset: 0x00075464
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
			this.CancelInputKey.OnFinalize();
			this.ResetInputKey.OnFinalize();
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00077290 File Offset: 0x00075490
		private void InitBarterList(BarterData args)
		{
			this._leftList.Add(args.GetBarterGroup<FiefBarterGroup>(), this.LeftFiefList);
			this._leftList.Add(args.GetBarterGroup<PrisonerBarterGroup>(), this.LeftPrisonerList);
			this._leftList.Add(args.GetBarterGroup<ItemBarterGroup>(), this.LeftItemList);
			this._leftList.Add(args.GetBarterGroup<OtherBarterGroup>(), this.LeftOtherList);
			this._leftList.Add(args.GetBarterGroup<GoldBarterGroup>(), this.LeftGoldList);
			this._rightList.Add(args.GetBarterGroup<FiefBarterGroup>(), this.RightFiefList);
			this._rightList.Add(args.GetBarterGroup<PrisonerBarterGroup>(), this.RightPrisonerList);
			this._rightList.Add(args.GetBarterGroup<ItemBarterGroup>(), this.RightItemList);
			this._rightList.Add(args.GetBarterGroup<OtherBarterGroup>(), this.RightOtherList);
			this._rightList.Add(args.GetBarterGroup<GoldBarterGroup>(), this.RightGoldList);
			this._barterList.Add(this._leftList);
			this._barterList.Add(this._rightList);
			this._offerList.Add(this.LeftOfferList);
			this._offerList.Add(this.RightOfferList);
			if (this._barterData.ContextInitializer != null)
			{
				foreach (Barterable barterable in this._barterData.GetBarterables())
				{
					if (barterable.IsContextDependent && this._barterData.ContextInitializer(barterable, this._barterData, null))
					{
						this.ChangeBarterableIsOffered(barterable, true);
					}
				}
			}
			foreach (Barterable barterable2 in args.GetBarterables())
			{
				if (!barterable2.IsOffered && !barterable2.IsContextDependent)
				{
					this._barterList[(barterable2.OriginalOwner == Hero.MainHero) ? 1 : 0][barterable2.Group].Add(new BarterItemVM(barterable2, new BarterItemVM.BarterTransferEventDelegate(this.TransferItem), new Action(this.OnOfferedAmountChange), false));
				}
				else
				{
					BarterItemVM barterItemVM = new BarterItemVM(barterable2, new BarterItemVM.BarterTransferEventDelegate(this.TransferItem), new Action(this.OnOfferedAmountChange), barterable2.IsContextDependent);
					this._offerList[(barterable2.OriginalOwner == Hero.MainHero) ? 1 : 0].Add(barterItemVM);
					this.RefreshCompatibility(barterItemVM, true);
				}
			}
			this._barterData.GetBarterables().Find((Barterable t) => t.Group.GetType() == typeof(GoldBarterGroup) && t.OriginalOwner == Hero.MainHero);
			this._barterData.GetBarterables().Find((Barterable t) => (t.Group.GetType() == typeof(GoldBarterGroup) && this._barterData.OffererHero == Hero.MainHero && t.OriginalOwner == this._barterData.OtherHero) || (this._barterData.OtherHero == Hero.MainHero && t.OriginalOwner == this._barterData.OffererHero));
			this.RefreshOfferLabel();
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00077584 File Offset: 0x00075784
		private void ChangeBarterableIsOffered(Barterable barterable, bool newState)
		{
			if (barterable.IsOffered != newState)
			{
				barterable.SetIsOffered(newState);
				this.OnTransferItem(barterable, true);
				foreach (Barterable barterable2 in barterable.LinkedBarterables)
				{
					this.OnTransferItem(barterable2, true);
				}
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000775F0 File Offset: 0x000757F0
		public void OnInitialized()
		{
			BarterManager barterManager = Campaign.Current.BarterManager;
			barterManager.Closed = (BarterManager.BarterCloseEventDelegate)Delegate.Combine(barterManager.Closed, new BarterManager.BarterCloseEventDelegate(this.OnClosed));
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0007761D File Offset: 0x0007581D
		private void OnClosed()
		{
			BarterManager barterManager = Campaign.Current.BarterManager;
			barterManager.Closed = (BarterManager.BarterCloseEventDelegate)Delegate.Remove(barterManager.Closed, new BarterManager.BarterCloseEventDelegate(this.OnClosed));
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0007764A File Offset: 0x0007584A
		public void ExecuteTransferAllLeftFief()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<FiefBarterGroup>());
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00077663 File Offset: 0x00075863
		public void ExecuteAutoBalance()
		{
			this.AutoBalanceAdd();
			this.AutoBalanceRemove();
			this.AutoBalanceAdd();
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00077678 File Offset: 0x00075878
		private void AutoBalanceRemove()
		{
			if ((int)Campaign.Current.BarterManager.GetOfferValue(this._otherCharacter.HeroObject, this._otherParty, this._barterData.OffererParty, this._barterData.GetOfferedBarterables()) > 0)
			{
				List<ValueTuple<Barterable, int>> list = BarterHelper.GetAutoBalanceBarterablesToRemove(this._barterData, this.OtherFaction, Clan.PlayerClan.MapFaction, Hero.MainHero).ToList<ValueTuple<Barterable, int>>();
				List<ValueTuple<BarterItemVM, int>> list2 = new List<ValueTuple<BarterItemVM, int>>();
				this.GetBarterItems(this.RightGoldList, list, list2);
				this.GetBarterItems(this.RightItemList, list, list2);
				this.GetBarterItems(this.RightPrisonerList, list, list2);
				this.GetBarterItems(this.RightFiefList, list, list2);
				foreach (ValueTuple<BarterItemVM, int> valueTuple in list2)
				{
					BarterItemVM item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					this.OfferItemRemove(item, item2);
				}
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00077778 File Offset: 0x00075978
		private void AutoBalanceAdd()
		{
			if ((int)Campaign.Current.BarterManager.GetOfferValue(this._otherCharacter.HeroObject, this._otherParty, this._barterData.OffererParty, this._barterData.GetOfferedBarterables()) < 0)
			{
				List<ValueTuple<Barterable, int>> list = BarterHelper.GetAutoBalanceBarterablesAdd(this._barterData, this.OtherFaction, Clan.PlayerClan.MapFaction, Hero.MainHero, 1f).ToList<ValueTuple<Barterable, int>>();
				List<ValueTuple<BarterItemVM, int>> list2 = new List<ValueTuple<BarterItemVM, int>>();
				this.GetBarterItems(this.RightGoldList, list, list2);
				this.GetBarterItems(this.RightItemList, list, list2);
				this.GetBarterItems(this.RightPrisonerList, list, list2);
				this.GetBarterItems(this.RightFiefList, list, list2);
				foreach (ValueTuple<BarterItemVM, int> valueTuple in list2)
				{
					BarterItemVM item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					if (item2 > 0)
					{
						this.OfferItemAdd(item, item2);
					}
				}
			}
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00077880 File Offset: 0x00075A80
		private void GetBarterItems(MBBindingList<BarterItemVM> itemList, [TupleElementNames(new string[] { "barterable", "count" })] List<ValueTuple<Barterable, int>> newBarterables, List<ValueTuple<BarterItemVM, int>> barterItems)
		{
			foreach (BarterItemVM barterItemVM in itemList)
			{
				foreach (ValueTuple<Barterable, int> valueTuple in newBarterables)
				{
					Barterable item = valueTuple.Item1;
					int item2 = valueTuple.Item2;
					if (item == barterItemVM.Barterable)
					{
						barterItems.Add(new ValueTuple<BarterItemVM, int>(barterItemVM, item2));
					}
				}
			}
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0007791C File Offset: 0x00075B1C
		public void ExecuteTransferAllLeftItem()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<ItemBarterGroup>());
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00077935 File Offset: 0x00075B35
		public void ExecuteTransferAllLeftPrisoner()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<PrisonerBarterGroup>());
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0007794E File Offset: 0x00075B4E
		public void ExecuteTransferAllLeftOther()
		{
			this.ExecuteTransferAll(this._otherCharacter, this._barterData.GetBarterGroup<OtherBarterGroup>());
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00077967 File Offset: 0x00075B67
		public void ExecuteTransferAllRightFief()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<FiefBarterGroup>());
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0007797F File Offset: 0x00075B7F
		public void ExecuteTransferAllRightItem()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<ItemBarterGroup>());
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00077997 File Offset: 0x00075B97
		public void ExecuteTransferAllRightPrisoner()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<PrisonerBarterGroup>());
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000779AF File Offset: 0x00075BAF
		public void ExecuteTransferAllRightOther()
		{
			this.ExecuteTransferAll(CharacterObject.PlayerCharacter, this._barterData.GetBarterGroup<OtherBarterGroup>());
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000779C8 File Offset: 0x00075BC8
		private void ExecuteTransferAll(CharacterObject fromCharacter, BarterGroup barterGroup)
		{
			if (barterGroup != null)
			{
				foreach (BarterItemVM barterItemVM in new List<BarterItemVM>(this._barterList[(fromCharacter == CharacterObject.PlayerCharacter) ? 1 : 0][barterGroup].Where<BarterItemVM>((BarterItemVM barterItem) => !barterItem.Barterable.IsOffered)))
				{
					this.TransferItem(barterItemVM, true);
				}
				foreach (BarterItemVM barterItemVM2 in this._barterList[(fromCharacter == CharacterObject.PlayerCharacter) ? 1 : 0][barterGroup])
				{
					barterItemVM2.CurrentOfferedAmount = barterItemVM2.TotalItemCount;
				}
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00077AB8 File Offset: 0x00075CB8
		private void SendOffer()
		{
			this.IsOfferDisabled = !this.IsCurrentOfferAcceptable() || (this.LeftOfferList.Count == 0 && this.RightOfferList.Count == 0);
			this.RefreshResultBar();
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00077AEF File Offset: 0x00075CEF
		private bool IsCurrentOfferAcceptable()
		{
			return Campaign.Current.BarterManager.IsOfferAcceptable(this._barterData, this._otherCharacter.HeroObject, this._otherParty);
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00077B18 File Offset: 0x00075D18
		private IFaction OtherFaction
		{
			get
			{
				if (!this._otherCharacter.IsHero)
				{
					return this._otherParty.MapFaction;
				}
				return this._otherCharacter.HeroObject.Clan;
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00077B50 File Offset: 0x00075D50
		private void RefreshResultBar()
		{
			long num = 0L;
			long num2 = 0L;
			IFaction otherFaction = this.OtherFaction;
			foreach (BarterItemVM barterItemVM in this.LeftOfferList)
			{
				int valueForFaction = barterItemVM.Barterable.GetValueForFaction(otherFaction);
				if (valueForFaction < 0)
				{
					num2 += (long)valueForFaction;
				}
				else
				{
					num += (long)valueForFaction;
				}
			}
			foreach (BarterItemVM barterItemVM2 in this.RightOfferList)
			{
				int valueForFaction2 = barterItemVM2.Barterable.GetValueForFaction(otherFaction);
				if (valueForFaction2 < 0)
				{
					num2 += (long)valueForFaction2;
				}
				else
				{
					num += (long)valueForFaction2;
				}
			}
			double num3 = (double)MathF.Max(0f, (float)num);
			double num4 = (double)MathF.Max(1f, (float)(-(float)num2));
			this.ResultBarOtherPercentage = MathF.Round(num3 / num4 * 100.0);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00077C58 File Offset: 0x00075E58
		private void ExecuteTransferAllGoldLeft()
		{
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00077C5A File Offset: 0x00075E5A
		private void ExecuteTransferAllGoldRight()
		{
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00077C5C File Offset: 0x00075E5C
		public void ExecuteOffer()
		{
			Campaign.Current.BarterManager.ApplyAndFinalizePlayerBarter(this._barterData.OffererHero, this._barterData.OtherHero, this._barterData);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00077C89 File Offset: 0x00075E89
		public void ExecuteCancel()
		{
			Campaign.Current.BarterManager.CancelAndFinalizePlayerBarter(this._barterData.OffererHero, this._barterData.OtherHero, this._barterData);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00077CB8 File Offset: 0x00075EB8
		public void ExecuteReset()
		{
			this.LeftFiefList.Clear();
			this.RightFiefList.Clear();
			this.LeftPrisonerList.Clear();
			this.RightPrisonerList.Clear();
			this.LeftItemList.Clear();
			this.RightItemList.Clear();
			this.LeftOtherList.Clear();
			this.RightOtherList.Clear();
			this.LeftDiplomaticList.Clear();
			this.RightDiplomaticList.Clear();
			this.LeftGoldList.Clear();
			this.RightGoldList.Clear();
			this._leftList.Clear();
			this._rightList.Clear();
			this._barterList.Clear();
			this.LeftOfferList.Clear();
			this.RightOfferList.Clear();
			this._offerList.Clear();
			foreach (Barterable barterable in this._barterData.GetBarterables())
			{
				if (barterable.IsOffered)
				{
					this.ChangeBarterableIsOffered(barterable, false);
				}
			}
			this.InitBarterList(this._barterData);
			this.SendOffer();
			this.InitializationIsOver = true;
			this.RefreshValues();
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00077E00 File Offset: 0x00076000
		private void TransferItem(BarterItemVM item, bool offerAll)
		{
			this.ChangeBarterableIsOffered(item.Barterable, !item.IsOffered);
			if (offerAll)
			{
				item.CurrentOfferedAmount = item.TotalItemCount;
			}
			this.SendOffer();
			this.RefreshOfferLabel();
			this.RefreshCompatibility(item, item.IsOffered);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00077E40 File Offset: 0x00076040
		private void OfferItemAdd(BarterItemVM barterItemVM, int count)
		{
			this.ChangeBarterableIsOffered(barterItemVM.Barterable, true);
			barterItemVM.CurrentOfferedAmount = (int)MathF.Clamp((float)(barterItemVM.CurrentOfferedAmount + count), 0f, (float)barterItemVM.TotalItemCount);
			this.SendOffer();
			this.RefreshOfferLabel();
			this.RefreshCompatibility(barterItemVM, barterItemVM.IsOffered);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00077E94 File Offset: 0x00076094
		private void OfferItemRemove(BarterItemVM barterItemVM, int count)
		{
			if (barterItemVM.CurrentOfferedAmount <= count)
			{
				this.ChangeBarterableIsOffered(barterItemVM.Barterable, false);
			}
			else
			{
				barterItemVM.CurrentOfferedAmount = (int)MathF.Clamp((float)(barterItemVM.CurrentOfferedAmount - count), 0f, (float)barterItemVM.TotalItemCount);
			}
			this.SendOffer();
			this.RefreshOfferLabel();
			this.RefreshCompatibility(barterItemVM, barterItemVM.IsOffered);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00077EF4 File Offset: 0x000760F4
		public void OnTransferItem(Barterable barter, bool isTransferrable)
		{
			int num = ((barter.OriginalOwner == Hero.MainHero) ? 1 : 0);
			if (!this._barterList.IsEmpty<Dictionary<BarterGroup, MBBindingList<BarterItemVM>>>())
			{
				BarterItemVM barterItemVM = this._barterList[num][barter.Group].FirstOrDefault<BarterItemVM>((BarterItemVM i) => i.Barterable == barter);
				if (barterItemVM == null && !this._offerList.IsEmpty<MBBindingList<BarterItemVM>>())
				{
					barterItemVM = this._offerList[num].FirstOrDefault<BarterItemVM>((BarterItemVM i) => i.Barterable == barter);
				}
				if (barterItemVM != null)
				{
					barterItemVM.IsOffered = barter.IsOffered;
					barterItemVM.IsItemTransferrable = isTransferrable;
					if (barterItemVM.IsOffered)
					{
						this._offerList[num].Add(barterItemVM);
						if (barterItemVM.IsMultiple)
						{
							barterItemVM.CurrentOfferedAmount = 1;
							return;
						}
					}
					else
					{
						this._offerList[num].Remove(barterItemVM);
						if (barterItemVM.IsMultiple)
						{
							barterItemVM.CurrentOfferedAmount = 1;
						}
					}
				}
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00077FF8 File Offset: 0x000761F8
		private void OnOfferedAmountChange()
		{
			this.SendOffer();
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00078000 File Offset: 0x00076200
		private void RefreshOfferLabel()
		{
			if (this.LeftOfferList.Any<BarterItemVM>((BarterItemVM x) => x.Barterable.GetValueForFaction(this.OtherFaction) < 0) || this.RightOfferList.Any<BarterItemVM>((BarterItemVM x) => x.Barterable.GetValueForFaction(this.OtherFaction) < 0))
			{
				this.OfferLbl = GameTexts.FindText("str_offer", null).ToString();
				return;
			}
			this.OfferLbl = GameTexts.FindText("str_gift", null).ToString();
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0007806C File Offset: 0x0007626C
		private void RefreshCompatibility(BarterItemVM lastTransferredItem, bool gotOffered)
		{
			Action<BarterItemVM> <>9__0;
			foreach (MBBindingList<BarterItemVM> mbbindingList in this._leftList.Values)
			{
				List<BarterItemVM> list = mbbindingList.ToList<BarterItemVM>();
				Action<BarterItemVM> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(BarterItemVM b)
					{
						b.RefreshCompabilityWithItem(lastTransferredItem, gotOffered);
					});
				}
				list.ForEach(action);
			}
			Action<BarterItemVM> <>9__1;
			foreach (MBBindingList<BarterItemVM> mbbindingList2 in this._rightList.Values)
			{
				List<BarterItemVM> list2 = mbbindingList2.ToList<BarterItemVM>();
				Action<BarterItemVM> action2;
				if ((action2 = <>9__1) == null)
				{
					action2 = (<>9__1 = delegate(BarterItemVM b)
					{
						b.RefreshCompabilityWithItem(lastTransferredItem, gotOffered);
					});
				}
				list2.ForEach(action2);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x00078164 File Offset: 0x00076364
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x0007816C File Offset: 0x0007636C
		[DataSourceProperty]
		public string FiefLbl
		{
			get
			{
				return this._fiefLbl;
			}
			set
			{
				if (value != this._fiefLbl)
				{
					this._fiefLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "FiefLbl");
				}
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0007818F File Offset: 0x0007638F
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x00078197 File Offset: 0x00076397
		[DataSourceProperty]
		public string PrisonerLbl
		{
			get
			{
				return this._prisonerLbl;
			}
			set
			{
				if (value != this._prisonerLbl)
				{
					this._prisonerLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "PrisonerLbl");
				}
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x000781BA File Offset: 0x000763BA
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x000781C2 File Offset: 0x000763C2
		[DataSourceProperty]
		public string ItemLbl
		{
			get
			{
				return this._itemLbl;
			}
			set
			{
				if (value != this._itemLbl)
				{
					this._itemLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ItemLbl");
				}
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000781E5 File Offset: 0x000763E5
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x000781ED File Offset: 0x000763ED
		[DataSourceProperty]
		public string OtherLbl
		{
			get
			{
				return this._otherLbl;
			}
			set
			{
				if (value != this._otherLbl)
				{
					this._otherLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "OtherLbl");
				}
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x00078210 File Offset: 0x00076410
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x00078218 File Offset: 0x00076418
		[DataSourceProperty]
		public string CancelLbl
		{
			get
			{
				return this._cancelLbl;
			}
			set
			{
				if (value != this._cancelLbl)
				{
					this._cancelLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelLbl");
				}
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0007823B File Offset: 0x0007643B
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x00078243 File Offset: 0x00076443
		[DataSourceProperty]
		public string ResetLbl
		{
			get
			{
				return this._resetLbl;
			}
			set
			{
				if (value != this._resetLbl)
				{
					this._resetLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ResetLbl");
				}
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00078266 File Offset: 0x00076466
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x0007826E File Offset: 0x0007646E
		[DataSourceProperty]
		public string OfferLbl
		{
			get
			{
				return this._offerLbl;
			}
			set
			{
				if (value != this._offerLbl)
				{
					this._offerLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "OfferLbl");
				}
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x00078291 File Offset: 0x00076491
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x00078299 File Offset: 0x00076499
		[DataSourceProperty]
		public string DiplomaticLbl
		{
			get
			{
				return this._diplomaticLbl;
			}
			set
			{
				if (value != this._diplomaticLbl)
				{
					this._diplomaticLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DiplomaticLbl");
				}
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x000782BC File Offset: 0x000764BC
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x000782C4 File Offset: 0x000764C4
		[DataSourceProperty]
		public HintViewModel AutoBalanceHint
		{
			get
			{
				return this._autoBalanceHint;
			}
			set
			{
				if (value != this._autoBalanceHint)
				{
					this._autoBalanceHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AutoBalanceHint");
				}
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000782E2 File Offset: 0x000764E2
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x000782EA File Offset: 0x000764EA
		[DataSourceProperty]
		public HeroVM LeftHero
		{
			get
			{
				return this._leftHero;
			}
			set
			{
				if (value != this._leftHero)
				{
					this._leftHero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "LeftHero");
				}
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00078308 File Offset: 0x00076508
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x00078310 File Offset: 0x00076510
		[DataSourceProperty]
		public HeroVM RightHero
		{
			get
			{
				return this._rightHero;
			}
			set
			{
				if (value != this._rightHero)
				{
					this._rightHero = value;
					base.OnPropertyChangedWithValue<HeroVM>(value, "RightHero");
				}
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0007832E File Offset: 0x0007652E
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x00078336 File Offset: 0x00076536
		[DataSourceProperty]
		public bool IsOfferDisabled
		{
			get
			{
				return this._isOfferDisabled;
			}
			set
			{
				if (value != this._isOfferDisabled)
				{
					this._isOfferDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsOfferDisabled");
				}
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x00078354 File Offset: 0x00076554
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0007835C File Offset: 0x0007655C
		[DataSourceProperty]
		public int LeftMaxGold
		{
			get
			{
				return this._leftMaxGold;
			}
			set
			{
				if (value != this._leftMaxGold)
				{
					this._leftMaxGold = value;
					base.OnPropertyChangedWithValue(value, "LeftMaxGold");
				}
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0007837A File Offset: 0x0007657A
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x00078382 File Offset: 0x00076582
		[DataSourceProperty]
		public int RightMaxGold
		{
			get
			{
				return this._rightMaxGold;
			}
			set
			{
				if (value != this._rightMaxGold)
				{
					this._rightMaxGold = value;
					base.OnPropertyChangedWithValue(value, "RightMaxGold");
				}
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x000783A0 File Offset: 0x000765A0
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x000783A8 File Offset: 0x000765A8
		[DataSourceProperty]
		public string LeftNameLbl
		{
			get
			{
				return this._leftNameLbl;
			}
			set
			{
				if (value != this._leftNameLbl)
				{
					this._leftNameLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "LeftNameLbl");
				}
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x000783CB File Offset: 0x000765CB
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x000783D3 File Offset: 0x000765D3
		[DataSourceProperty]
		public string RightNameLbl
		{
			get
			{
				return this._rightNameLbl;
			}
			set
			{
				if (value != this._rightNameLbl)
				{
					this._rightNameLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "RightNameLbl");
				}
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000783F6 File Offset: 0x000765F6
		// (set) Token: 0x060020E6 RID: 8422 RVA: 0x000783FE File Offset: 0x000765FE
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftFiefList
		{
			get
			{
				return this._leftFiefList;
			}
			set
			{
				if (value != this._leftFiefList)
				{
					this._leftFiefList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftFiefList");
				}
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0007841C File Offset: 0x0007661C
		// (set) Token: 0x060020E8 RID: 8424 RVA: 0x00078424 File Offset: 0x00076624
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightFiefList
		{
			get
			{
				return this._rightFiefList;
			}
			set
			{
				if (value != this._rightFiefList)
				{
					this._rightFiefList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightFiefList");
				}
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x00078442 File Offset: 0x00076642
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x0007844A File Offset: 0x0007664A
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftPrisonerList
		{
			get
			{
				return this._leftPrisonerList;
			}
			set
			{
				if (value != this._leftPrisonerList)
				{
					this._leftPrisonerList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftPrisonerList");
				}
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x00078468 File Offset: 0x00076668
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x00078470 File Offset: 0x00076670
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightPrisonerList
		{
			get
			{
				return this._rightPrisonerList;
			}
			set
			{
				if (value != this._rightPrisonerList)
				{
					this._rightPrisonerList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightPrisonerList");
				}
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0007848E File Offset: 0x0007668E
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x00078496 File Offset: 0x00076696
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftItemList
		{
			get
			{
				return this._leftItemList;
			}
			set
			{
				if (value != this._leftItemList)
				{
					this._leftItemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftItemList");
				}
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x000784B4 File Offset: 0x000766B4
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x000784BC File Offset: 0x000766BC
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightItemList
		{
			get
			{
				return this._rightItemList;
			}
			set
			{
				if (value != this._rightItemList)
				{
					this._rightItemList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightItemList");
				}
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000784DA File Offset: 0x000766DA
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x000784E2 File Offset: 0x000766E2
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftOtherList
		{
			get
			{
				return this._leftOtherList;
			}
			set
			{
				if (value != this._leftOtherList)
				{
					this._leftOtherList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftOtherList");
				}
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x00078500 File Offset: 0x00076700
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x00078508 File Offset: 0x00076708
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightOtherList
		{
			get
			{
				return this._rightOtherList;
			}
			set
			{
				if (value != this._rightOtherList)
				{
					this._rightOtherList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightOtherList");
				}
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00078526 File Offset: 0x00076726
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x0007852E File Offset: 0x0007672E
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftDiplomaticList
		{
			get
			{
				return this._leftDiplomaticList;
			}
			set
			{
				if (value != this._leftDiplomaticList)
				{
					this._leftDiplomaticList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftDiplomaticList");
				}
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0007854C File Offset: 0x0007674C
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x00078554 File Offset: 0x00076754
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightDiplomaticList
		{
			get
			{
				return this._rightDiplomaticList;
			}
			set
			{
				if (value != this._rightDiplomaticList)
				{
					this._rightDiplomaticList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightDiplomaticList");
				}
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00078572 File Offset: 0x00076772
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0007857A File Offset: 0x0007677A
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftOfferList
		{
			get
			{
				return this._leftOfferList;
			}
			set
			{
				if (value != this._leftOfferList)
				{
					this._leftOfferList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftOfferList");
				}
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x00078598 File Offset: 0x00076798
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x000785A0 File Offset: 0x000767A0
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightOfferList
		{
			get
			{
				return this._rightOfferList;
			}
			set
			{
				if (value != this._rightOfferList)
				{
					this._rightOfferList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightOfferList");
				}
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x000785BE File Offset: 0x000767BE
		// (set) Token: 0x060020FE RID: 8446 RVA: 0x000785C6 File Offset: 0x000767C6
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> RightGoldList
		{
			get
			{
				return this._rightGoldList;
			}
			set
			{
				if (value != this._rightGoldList)
				{
					this._rightGoldList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "RightGoldList");
				}
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x000785E4 File Offset: 0x000767E4
		// (set) Token: 0x06002100 RID: 8448 RVA: 0x000785EC File Offset: 0x000767EC
		[DataSourceProperty]
		public MBBindingList<BarterItemVM> LeftGoldList
		{
			get
			{
				return this._leftGoldList;
			}
			set
			{
				if (value != this._leftGoldList)
				{
					this._leftGoldList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BarterItemVM>>(value, "LeftGoldList");
				}
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x0007860A File Offset: 0x0007680A
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x00078612 File Offset: 0x00076812
		[DataSourceProperty]
		public bool InitializationIsOver
		{
			get
			{
				return this._initializationIsOver;
			}
			set
			{
				this._initializationIsOver = value;
				base.OnPropertyChangedWithValue(value, "InitializationIsOver");
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00078627 File Offset: 0x00076827
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x0007862F File Offset: 0x0007682F
		[DataSourceProperty]
		public int ResultBarOtherPercentage
		{
			get
			{
				return this._resultBarOtherPercentage;
			}
			set
			{
				this._resultBarOtherPercentage = value;
				base.OnPropertyChangedWithValue(value, "ResultBarOtherPercentage");
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00078644 File Offset: 0x00076844
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x0007864C File Offset: 0x0007684C
		[DataSourceProperty]
		public int ResultBarOffererPercentage
		{
			get
			{
				return this._resultBarOffererPercentage;
			}
			set
			{
				this._resultBarOffererPercentage = value;
				base.OnPropertyChangedWithValue(value, "ResultBarOffererPercentage");
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00078661 File Offset: 0x00076861
		public void SetResetInputKey(HotKey hotkey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00078670 File Offset: 0x00076870
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0007867F File Offset: 0x0007687F
		public void SetCancelInputKey(HotKey hotkey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x0007868E File Offset: 0x0007688E
		// (set) Token: 0x0600210B RID: 8459 RVA: 0x00078696 File Offset: 0x00076896
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000786B4 File Offset: 0x000768B4
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x000786BC File Offset: 0x000768BC
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x000786DA File Offset: 0x000768DA
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x000786E2 File Offset: 0x000768E2
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00078700 File Offset: 0x00076900
		public void InitializeStaticContent()
		{
			this.FiefLbl = GameTexts.FindText("str_fiefs", null).ToString();
			this.PrisonerLbl = GameTexts.FindText("str_prisoner_tag_name", null).ToString();
			this.ItemLbl = GameTexts.FindText("str_item_tag_name", null).ToString();
			this.OtherLbl = GameTexts.FindText("str_other", null).ToString();
			this.CancelLbl = GameTexts.FindText("str_cancel", null).ToString();
			this.ResetLbl = GameTexts.FindText("str_reset", null).ToString();
			this.DiplomaticLbl = GameTexts.FindText("str_diplomatic_group", null).ToString();
			this.AutoBalanceHint.HintText = new TextObject("{=Ve5jkJqf}Auto Offer", null);
		}

		// Token: 0x04000F30 RID: 3888
		private readonly List<Dictionary<BarterGroup, MBBindingList<BarterItemVM>>> _barterList;

		// Token: 0x04000F31 RID: 3889
		private readonly List<MBBindingList<BarterItemVM>> _offerList;

		// Token: 0x04000F32 RID: 3890
		private readonly Dictionary<BarterGroup, MBBindingList<BarterItemVM>> _leftList;

		// Token: 0x04000F33 RID: 3891
		private readonly Dictionary<BarterGroup, MBBindingList<BarterItemVM>> _rightList;

		// Token: 0x04000F34 RID: 3892
		private readonly bool _isPlayerOfferer;

		// Token: 0x04000F35 RID: 3893
		private readonly BarterManager _barter;

		// Token: 0x04000F36 RID: 3894
		private readonly CharacterObject _otherCharacter;

		// Token: 0x04000F37 RID: 3895
		private readonly PartyBase _otherParty;

		// Token: 0x04000F38 RID: 3896
		private readonly BarterData _barterData;

		// Token: 0x04000F39 RID: 3897
		private string _fiefLbl;

		// Token: 0x04000F3A RID: 3898
		private string _prisonerLbl;

		// Token: 0x04000F3B RID: 3899
		private string _itemLbl;

		// Token: 0x04000F3C RID: 3900
		private string _otherLbl;

		// Token: 0x04000F3D RID: 3901
		private string _cancelLbl;

		// Token: 0x04000F3E RID: 3902
		private string _resetLbl;

		// Token: 0x04000F3F RID: 3903
		private string _offerLbl;

		// Token: 0x04000F40 RID: 3904
		private string _diplomaticLbl;

		// Token: 0x04000F41 RID: 3905
		private HintViewModel _autoBalanceHint;

		// Token: 0x04000F42 RID: 3906
		private HeroVM _leftHero;

		// Token: 0x04000F43 RID: 3907
		private HeroVM _rightHero;

		// Token: 0x04000F44 RID: 3908
		private string _leftNameLbl;

		// Token: 0x04000F45 RID: 3909
		private string _rightNameLbl;

		// Token: 0x04000F46 RID: 3910
		private MBBindingList<BarterItemVM> _leftFiefList;

		// Token: 0x04000F47 RID: 3911
		private MBBindingList<BarterItemVM> _rightFiefList;

		// Token: 0x04000F48 RID: 3912
		private MBBindingList<BarterItemVM> _leftPrisonerList;

		// Token: 0x04000F49 RID: 3913
		private MBBindingList<BarterItemVM> _rightPrisonerList;

		// Token: 0x04000F4A RID: 3914
		private MBBindingList<BarterItemVM> _leftItemList;

		// Token: 0x04000F4B RID: 3915
		private MBBindingList<BarterItemVM> _rightItemList;

		// Token: 0x04000F4C RID: 3916
		private MBBindingList<BarterItemVM> _leftOtherList;

		// Token: 0x04000F4D RID: 3917
		private MBBindingList<BarterItemVM> _rightOtherList;

		// Token: 0x04000F4E RID: 3918
		private MBBindingList<BarterItemVM> _leftDiplomaticList;

		// Token: 0x04000F4F RID: 3919
		private MBBindingList<BarterItemVM> _rightDiplomaticList;

		// Token: 0x04000F50 RID: 3920
		private MBBindingList<BarterItemVM> _leftGoldList;

		// Token: 0x04000F51 RID: 3921
		private MBBindingList<BarterItemVM> _rightGoldList;

		// Token: 0x04000F52 RID: 3922
		private MBBindingList<BarterItemVM> _leftOfferList;

		// Token: 0x04000F53 RID: 3923
		private MBBindingList<BarterItemVM> _rightOfferList;

		// Token: 0x04000F54 RID: 3924
		private int _leftMaxGold;

		// Token: 0x04000F55 RID: 3925
		private int _rightMaxGold;

		// Token: 0x04000F56 RID: 3926
		private bool _initializationIsOver;

		// Token: 0x04000F57 RID: 3927
		private bool _isOfferDisabled;

		// Token: 0x04000F58 RID: 3928
		private int _resultBarOffererPercentage = -1;

		// Token: 0x04000F59 RID: 3929
		private int _resultBarOtherPercentage = -1;

		// Token: 0x04000F5A RID: 3930
		private InputKeyItemVM _resetInputKey;

		// Token: 0x04000F5B RID: 3931
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000F5C RID: 3932
		private InputKeyItemVM _cancelInputKey;
	}
}
