using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000361 RID: 865
	public class SoundPlayer : ScriptComponentBehavior
	{
		// Token: 0x0600318C RID: 12684 RVA: 0x000CA2B4 File Offset: 0x000C84B4
		private void ValidateSoundEvent()
		{
			if ((this.SoundEvent == null || !this.SoundEvent.IsValid) && this.SoundName.Length > 0)
			{
				if (this.SoundCode == -1)
				{
					this.SoundCode = SoundManager.GetEventGlobalIndex(this.SoundName);
				}
				this.SoundEvent = SoundEvent.CreateEvent(this.SoundCode, base.GameEntity.Scene);
			}
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000CA31D File Offset: 0x000C851D
		public void UpdatePlaying()
		{
			this.Playing = this.SoundEvent != null && this.SoundEvent.IsValid && this.SoundEvent.IsPlaying();
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000CA348 File Offset: 0x000C8548
		public void PlaySound()
		{
			if (this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid)
			{
				this.SoundEvent.SetPosition(base.GameEntity.GlobalPosition);
				this.SoundEvent.Play();
				this.Playing = true;
			}
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000CA39F File Offset: 0x000C859F
		public void ResumeSound()
		{
			if (this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid && this.SoundEvent.IsPaused())
			{
				this.SoundEvent.Resume();
				this.Playing = true;
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000CA3DE File Offset: 0x000C85DE
		public void PauseSound()
		{
			if (!this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid)
			{
				this.SoundEvent.Pause();
				this.Playing = false;
			}
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000CA410 File Offset: 0x000C8610
		public void StopSound()
		{
			if (!this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid)
			{
				this.SoundEvent.Stop();
				this.Playing = false;
			}
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000CA442 File Offset: 0x000C8642
		protected internal override void OnInit()
		{
			base.OnInit();
			MBDebug.Print("SoundPlayer : OnInit called.", 0, Debug.DebugColor.Yellow, 17592186044416UL);
			this.ValidateSoundEvent();
			if (this.AutoStart)
			{
				this.PlaySound();
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000CA480 File Offset: 0x000C8680
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000CA48A File Offset: 0x000C868A
		protected internal override void OnTick(float dt)
		{
			this.UpdatePlaying();
			if (!this.Playing && this.AutoLoop)
			{
				this.ValidateSoundEvent();
				this.PlaySound();
			}
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000CA4AE File Offset: 0x000C86AE
		protected internal override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x040014F1 RID: 5361
		private bool Playing;

		// Token: 0x040014F2 RID: 5362
		private int SoundCode = -1;

		// Token: 0x040014F3 RID: 5363
		private SoundEvent SoundEvent;

		// Token: 0x040014F4 RID: 5364
		public bool AutoLoop;

		// Token: 0x040014F5 RID: 5365
		public bool AutoStart;

		// Token: 0x040014F6 RID: 5366
		public string SoundName;
	}
}
