using AppHealth.Models;
using System.Collections.Generic;

namespace CreateGraphByPoints.Interfaces
{
    public interface IForWorkWithFiles
    {
        void LoadInFile(ResultUser resultUser, UserInfo userInfo);

        void LoadFromFile(List<List<UserInfo>> param);
    }
}
