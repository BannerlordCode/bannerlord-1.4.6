using System;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200008C RID: 140
	public struct MeetingSceneData
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00053605 File Offset: 0x00051805
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x0005360D File Offset: 0x0005180D
		public string SceneID { get; private set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00053616 File Offset: 0x00051816
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x0005361E File Offset: 0x0005181E
		public string CultureString { get; private set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00053627 File Offset: 0x00051827
		public CultureObject Culture
		{
			get
			{
				return MBObjectManager.Instance.GetObject<CultureObject>(this.CultureString);
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00053639 File Offset: 0x00051839
		public MeetingSceneData(string sceneID, string cultureString)
		{
			this.SceneID = sceneID;
			this.CultureString = cultureString;
		}
	}
}
