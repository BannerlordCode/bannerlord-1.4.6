using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x02000022 RID: 34
	public class LauncherHintTriggerWidget : Widget
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00006335 File Offset: 0x00004535
		public LauncherHintTriggerWidget(UIContext context)
			: base(context)
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000633E File Offset: 0x0000453E
		protected override void OnConnectedToRoot()
		{
			base.ParentWidget.EventFire += this.ParentWidgetEventFired;
			base.OnConnectedToRoot();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000635D File Offset: 0x0000455D
		protected override void OnDisconnectedFromRoot()
		{
			base.ParentWidget.EventFire -= this.ParentWidgetEventFired;
			base.OnDisconnectedFromRoot();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000637C File Offset: 0x0000457C
		private void ParentWidgetEventFired(Widget widget, string eventName, object[] args)
		{
			if (base.IsVisible)
			{
				if (eventName == "HoverBegin")
				{
					base.EventFired("HoverBegin", Array.Empty<object>());
					return;
				}
				if (eventName == "HoverEnd")
				{
					base.EventFired("HoverEnd", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000063CC File Offset: 0x000045CC
		protected override bool OnPreviewMousePressed()
		{
			return false;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000063CF File Offset: 0x000045CF
		protected override bool OnPreviewDragBegin()
		{
			return false;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000063D2 File Offset: 0x000045D2
		protected override bool OnPreviewDrop()
		{
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000063D5 File Offset: 0x000045D5
		protected override bool OnPreviewMouseScroll()
		{
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000063D8 File Offset: 0x000045D8
		protected override bool OnPreviewMouseReleased()
		{
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000063DB File Offset: 0x000045DB
		protected override bool OnPreviewMouseMove()
		{
			return true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000063DE File Offset: 0x000045DE
		protected override bool OnPreviewDragHover()
		{
			return false;
		}
	}
}
