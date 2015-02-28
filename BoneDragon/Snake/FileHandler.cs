using System;
using System.Xml;
using System.IO;

namespace Snake
{
    class FileHandler
    {
        private string _fileName;

        public FileHandler(string fileName)
        {
            this._fileName = fileName;
            if (!File.Exists(fileName))
            {
                CreateFile();
            }
        }

        private void CreateFile()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);
            XmlElement rootNode = doc.CreateElement("Players");
            doc.AppendChild(rootNode);
            doc.Save(this._fileName);
        }

        public void SaveUserScore(string userName, int score)
        {
            XmlDocument doc = new XmlDocument();
            // TODO: Check if the passed score > bottom-most node score and append if so
            doc.Load(this._fileName);

            XmlNode node = doc.CreateNode(XmlNodeType.Element, "Player", null);

            XmlNode nodeName = doc.CreateElement("Name");
            nodeName.InnerText = userName;

            XmlNode nodeScore = doc.CreateElement("Score");
            nodeScore.InnerText = score.ToString();

            XmlNode nodeTime = doc.CreateElement("Time");
            nodeTime.InnerText = DateTime.Now.ToString();

            node.AppendChild(nodeName);
            node.AppendChild(nodeScore);
            node.AppendChild(nodeTime);

            doc.DocumentElement.AppendChild(node);

            // TODO: Linq child nodes sorting by score.
            doc.Save(this._fileName);
        }

        // Sample implementation. TODO: Integrate with the high scores menu function.
        public void PrintScores()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this._fileName);

            XmlNodeList nodeList = doc.GetElementsByTagName("Player");
            foreach (XmlNode node in nodeList)
            {
                XmlElement playerNode = (XmlElement)node;

                string playerName = playerNode.GetElementsByTagName("Name")[0].InnerText;
                string playerScore = playerNode.GetElementsByTagName("Score")[0].InnerText;
                string time = playerNode.GetElementsByTagName("Time")[0].InnerText;
                Console.WriteLine("Username: {0} Score: {1} Date: {2}", playerName, playerScore, time);
            }
        }

        public void GetScoreByUsername(string userName)
        {
            // TODO
        }
    }
}