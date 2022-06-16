using AppHealth.Models;
using CreateGraphByPoints.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace AppHealth.ForWorkWithFiles
{
    internal class WorkForXml : IForWorkWithFiles
    {
        public void LoadFromFile(List<List<UserInfo>> param)
        {
            throw new NotImplementedException();
        }

        public async Task LoadInFile(ResultUser resultUser, UserInfo userInfo)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            using (XmlWriter writer = XmlWriter.Create(string.Format(@"{0}\SaveXml\user.xml", Environment.CurrentDirectory), xmlWriterSettings))
            {
                writer.WriteStartElement("User");
                writer.WriteElementString("Name", resultUser.User);
                writer.WriteElementString("Rank", userInfo.Rank.ToString());
                writer.WriteElementString("Status", userInfo.Status);
                writer.WriteElementString("AverageSteps", resultUser.AverageSteps.ToString());
                writer.WriteElementString("BestResult", resultUser.BestResult.ToString());
                writer.WriteElementString("WorstResult", resultUser.WorstResult.ToString());
                writer.WriteEndElement();
                await Task.Run(() => writer.Flush());
            }
        }
    }
}
