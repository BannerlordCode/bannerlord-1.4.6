using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000012 RID: 18
	public class ResultData
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000045E6 File Offset: 0x000027E6
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000045EE File Offset: 0x000027EE
		public string Errors { get; set; } = "";

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000045F7 File Offset: 0x000027F7
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000045FF File Offset: 0x000027FF
		public List<DLLResult> DLLs { get; set; } = new List<DLLResult>();

		// Token: 0x0600009F RID: 159 RVA: 0x00004608 File Offset: 0x00002808
		public void AddDLLResult(string dllName, bool isSafe, string information)
		{
			this.DLLs.Add(new DLLResult(dllName, isSafe, information));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004620 File Offset: 0x00002820
		public override string ToString()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ResultData));
			string text;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, this);
				text = stringWriter.ToString();
			}
			return text;
		}
	}
}
