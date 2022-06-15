using AppHealth.Models;
using LiveCharts;
using LiveCharts.Defaults;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CreateGraphByPoints.Interfaces
{
    public interface IForWorkWithFiles
    {
        void LoadInFile(ResultUser resultUser, UserInfo userInfo);

        void LoadFromFile(List<List<UserInfo>> param);
    }
}
