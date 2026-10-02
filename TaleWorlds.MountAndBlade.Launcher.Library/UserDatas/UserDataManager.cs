using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001E RID: 30
	public class UserDataManager
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005DD2 File Offset: 0x00003FD2
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005DDA File Offset: 0x00003FDA
		public UserData UserData { get; private set; }

		// Token: 0x06000134 RID: 308 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public UserDataManager()
		{
			this.UserData = new UserData();
			string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			text += "\\Mount and Blade II Bannerlord\\Configs\\";
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			this._filePath = text + "LauncherData.xml";
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005E50 File Offset: 0x00004050
		public bool HasUserData()
		{
			return File.Exists(this._filePath);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005E60 File Offset: 0x00004060
		public void LoadUserData()
		{
			if (!File.Exists(this._filePath))
			{
				return;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(this._filePath))
				{
					this.UserData = (UserData)xmlSerializer.Deserialize(xmlReader);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005EDC File Offset: 0x000040DC
		public void SaveUserData()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
			try
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(this._filePath, new XmlWriterSettings
				{
					Indent = true
				}))
				{
					xmlSerializer.Serialize(xmlWriter, this.UserData);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		// Token: 0x04000096 RID: 150
		private const string DataFolder = "\\Mount and Blade II Bannerlord\\Configs\\";

		// Token: 0x04000097 RID: 151
		private const string FileName = "LauncherData.xml";

		// Token: 0x04000098 RID: 152
		private readonly string _filePath;
	}
}
