using AppHealth.Commands;
using AppHealth.ForWorkWithFiles;
using AppHealth.Models;
using Autofac;
using CreateGraphByPoints.Containers;
using CreateGraphByPoints.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AppHealth.ViewModels
{
    public class WorkFilesViewModel : BaseViewModel
    {
        private void LoadInFile(object param, IForWorkWithFiles WorkFile)
        {
            var DrawFuncVM = param as DrawFuncViewModel;
            if (DrawFuncVM.CurrentResultUser == null)
            {
                MessageBox.Show("The user is not selected from the list");
                return;
            }    
            var userInfo = DrawFuncVM.ListUserInfo.Last().FirstOrDefault(x => x.User == DrawFuncVM.CurrentResultUser.User);

            WorkFile.LoadInFile(DrawFuncVM.CurrentResultUser, userInfo);
        }

        private void LoadFromFile(object param, IForWorkWithFiles WorkFile)
        {
            var DrawFuncVM = param as DrawFuncViewModel;
            DrawFuncVM.ListUserInfo.Clear();
            WorkFile.LoadFromFile(DrawFuncVM.ListUserInfo);

            DrawFuncVM.ListUserInfo.ForEach(daysUserInfo =>
            {
                daysUserInfo.ForEach(UserInfo =>
                {
                    var resultUser = DrawFuncVM.ListResultUser.FirstOrDefault(x => x.User == UserInfo.User);
                    if (resultUser == null)
                    {
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                            (Action)delegate ()
                            {
                                DrawFuncVM.ListResultUser.Add(new ResultUser
                                {
                                    User = UserInfo.User,
                                    AverageSteps = UserInfo.Steps,
                                    BestResult = UserInfo.Steps,
                                    WorstResult = UserInfo.Steps
                                });
                            });
                    }
                    else
                    {
                        resultUser.AverageSteps += UserInfo.Steps;
                        if (UserInfo.Steps > resultUser.BestResult)
                            resultUser.BestResult = UserInfo.Steps;

                        if (UserInfo.Steps < resultUser.WorstResult)
                            resultUser.WorstResult = UserInfo.Steps;
                    }
                });
            });

            foreach (var resultUser in DrawFuncVM.ListResultUser)
            {
                resultUser.AverageSteps = resultUser.AverageSteps / DrawFuncVM.ListUserInfo.Count;
            }
            MessageBox.Show("The functions were successfully unloaded from the file." +
                "\nThe points of the blue function are now displayed");
        }

        #region Commands

        #region --- LoadInJsonFile ---

        public ICommand LoadInJsonFile
        {
            get
            {
                if (_cmdLoadInJsonFile == null)
                {
                    _cmdLoadInJsonFile = new RelayCommand(
                        param => LoadInFile(param, AutofacConfig.GetContainer.Resolve<WorkForJson>())
                        );
                }
                return _cmdLoadInJsonFile;
            }
        }
        private RelayCommand _cmdLoadInJsonFile;

        #endregion --- LoadInJsonFile ---

        #region --- LoadFromJsonFile ---

        public ICommand LoadFromJsonFile
        {
            get
            {
                if (_cmdLoadFromJsonFile == null)
                {
                    _cmdLoadFromJsonFile = new RelayCommand(
                        param => LoadFromFile(param, AutofacConfig.GetContainer.Resolve<WorkForJson>())
                        );
                }
                return _cmdLoadFromJsonFile;
            }
        }
        private RelayCommand _cmdLoadFromJsonFile;

        #endregion --- LoadFromJsonFile ---

        #endregion Commands
    }
}
