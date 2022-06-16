using AppHealth.Models;
using CreateGraphByPoints.Interfaces;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace AppHealth.ForWorkWithFiles
{
    public class WorkForJson : IForWorkWithFiles
    {
        public void LoadFromFile(List<List<UserInfo>> param)
        {
            var files = Directory.GetFiles(string.Format(@"{0}\MonthlyStatistics", Environment.CurrentDirectory));
            Array.Sort(files, delegate (string a, string b) {
                var first = int.Parse(Path.GetFileNameWithoutExtension(a).Remove(0, 3));
                var second = int.Parse(Path.GetFileNameWithoutExtension(b).Remove(0, 3));
                if (first < second)
                    return -1;
                else if (first > second)
                    return +1;
                return 0;
            });
            
            Array.ForEach(files, file =>
            {
                using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
                {
                    List<UserInfo> person = JsonSerializer.Deserialize<List<UserInfo>>(fs);
                    param.Add(person);
                }
            });
        }

        public async Task LoadInFile(ResultUser resultUser, UserInfo userInfo)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            
            using (FileStream fs = new FileStream(string.Format(@"{0}\SaveJson\user.json", Environment.CurrentDirectory), FileMode.OpenOrCreate))
            {
                var user = new { 
                    User = resultUser.User,
                    AverageSteps = resultUser.AverageSteps,
                    BestResult = resultUser.BestResult,
                    WorstResult = resultUser.WorstResult,
                    Rank = userInfo.Rank,
                    Status=userInfo.Status};
                await JsonSerializer.SerializeAsync(fs, user, jsonSerializerOptions);
            }
        }
    }
}
