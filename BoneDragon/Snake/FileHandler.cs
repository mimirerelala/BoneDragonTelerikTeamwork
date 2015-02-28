using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Snake
{
    class FileHandler
    {
        private string _fileName;
        private const byte MAX_HIGHSCORE_ENTRIES = 10;

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
            XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement("Players"));

            xDoc.Save(this._fileName);
        }

        public void SaveUserScore(string userName, int score)
        {
            XElement root = XElement.Load(this._fileName);

            XElement lastPlayerNode = (XElement)root.LastNode;
            int lastPlayerScore = 0;
            try
            {
                lastPlayerScore = int.Parse(lastPlayerNode.Element("Score").Value);
            }
            catch (NullReferenceException) { }
            finally
            {
                if (score > lastPlayerScore)
                {
                    if (root.Elements("Player").Count() == MAX_HIGHSCORE_ENTRIES)
                    {
                        root.Elements("Player").Last().Remove();
                    }
                    root.Add(new XElement("Player",
                                            new XElement("Username", userName),
                                            new XElement("Score", score),
                                            new XElement("Date", DateTime.Now.ToString())
                                         )
                              );
                    var sortedScores = root.Elements("Player")
                                          .OrderByDescending(xScore => (int)xScore.Element("Score"))
                                          .ToArray();
                    root.RemoveAll();
                    foreach (XElement sortedScore in sortedScores)
                    {
                        root.Add(sortedScore);
                    }
                    root.Save(this._fileName);
                }
            }
        }

        // TODO: Integrate this with the high scores menu function.
        public void PrintScores()
        {
            XElement root = XElement.Load(this._fileName);
            IEnumerable<XElement> players = root.Elements();
            Console.WriteLine("High Scores");
            foreach (var player in players)
            {
                Console.Write("Username: {0} ", player.Element("Username").Value);
                Console.Write("Score: {0} ", player.Element("Score").Value);
                Console.Write("Date: {0} ", player.Element("Date").Value);
                Console.WriteLine();
            }
        }

        public bool IsUsernamePresent(string userName)
        {
            XElement root = XElement.Load(this._fileName);
            return root.Elements("Player").Where(x => x.Element("Username").Value == userName).Any();
        }

    }
}
