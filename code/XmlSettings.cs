using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace VoiPop
{
	public static class XmlSettings
	{
		static readonly XmlDocument xmlDocument = new XmlDocument();
		
		string commonConfigPath = Application.CommonAppDataPath;
    commonConfigPath = commonConfigPath.Substring(0, commonConfigPath.LastIndexOf('\\'));
		
		static readonly string documentPath = Path.Combine(commonConfigPath, "settings.xml");

		static XmlSettings()
		{
			try { xmlDocument.Load(documentPath); }
			catch { xmlDocument.LoadXml("<settings></settings>"); }
		}

		public static int GetSetting(string xPath, int defaultValue)
		{ return Convert.ToInt16(GetSetting(xPath, Convert.ToString(defaultValue))); }

		public static void PutSetting(string xPath, int value)
		{ PutSetting(xPath, Convert.ToString(value)); }

		public static string GetSetting(string xPath, string defaultValue)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("settings/" + xPath);
			if (xmlNode != null) { return xmlNode.InnerText; }
			else { return defaultValue; }
		}

		public static void PutSetting(string xPath, string value)
		{
			XmlNode xmlNode = xmlDocument.SelectSingleNode("settings/" + xPath);
			if (xmlNode == null) { xmlNode = createMissingNode("settings/" + xPath); }
			xmlNode.InnerText = value;
			xmlDocument.Save(documentPath);
		}

		private static XmlNode createMissingNode(string xPath)
		{
			string[] xPathSections = xPath.Split('/');
			string currentXPath = "";
			XmlNode testNode = null;
			XmlNode currentNode = xmlDocument.SelectSingleNode("settings");
			foreach (string xPathSection in xPathSections)
			{
				currentXPath += xPathSection;
				testNode = xmlDocument.SelectSingleNode(currentXPath);
				if (testNode == null)
				{
					currentNode.InnerXml += "<" +
											xPathSection + "></" +
											xPathSection + ">";
				}
				currentNode = xmlDocument.SelectSingleNode(currentXPath);
				currentXPath += "/";
			}
			return currentNode;
		}
	}
}
