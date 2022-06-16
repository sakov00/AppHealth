using AppHealth.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateGraphByPoints.Interfaces
{
    public interface IForWorkWithFiles
    {
        Task LoadInFile(ResultUser resultUser, UserInfo userInfo);

        void LoadFromFile(List<List<UserInfo>> param);
    }
}
