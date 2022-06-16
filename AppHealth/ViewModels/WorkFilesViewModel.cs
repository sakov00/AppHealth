using AppHealth.Commands;
using AppHealth.ForWorkWithFiles;
using AppHealth.Models;
using Autofac;
using CreateGraphByPoints.Containers;
using CreateGraphByPoints.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AppHealth.ViewModels
{
    public class WorkFilesViewModel : BaseViewModel
    {
        private async Task LoadInFile(object param, IForWorkWithFiles WorkFile)
        {
            var DrawFuncVM = param as DrawFuncViewModel;
            if (DrawFuncVM.CurrentResultUser == null)
            {
                MessageBox.Show("The user is not selected from the list");
                return;
            }    
            var userInfo = DrawFuncVM.ListUserInfo.Last().FirstOrDefault(x => x.User == DrawFuncVM.CurrentResultUser.User);

            await WorkFile.LoadInFile(DrawFuncVM.CurrentResultUser, userInfo);
            MessageBox.Show("The users were successfully loaded in the file.");
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

                        if (UserInfo.Steps < resultUser.WorstResult && UserInfo.Steps != 0)
                            resultUser.WorstResult = UserInfo.Steps;
                    }
                });
            });

            foreach (var resultUser in DrawFuncVM.ListResultUser)
            {
                resultUser.AverageSteps = resultUser.AverageSteps / DrawFuncVM.ListUserInfo.Count;
            }
            MessageBox.Show("The users were successfully unloaded from the file.");
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
                        async param => await Task.Run(()=> LoadInFile(param, AutofacConfig.GetContainer.Resolve<WorkForJson>()))
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

        #region --- LoadInXmlFile ---
        public ICommand LoadInXmlFile
        {
            get
            {
                if (_cmdLoadInXmlFile == null)
                {
                    _cmdLoadInXmlFile = new RelayCommand(
                        async param => await Task.Run(() => LoadInFile(param, AutofacConfig.GetContainer.Resolve<WorkForXml>()))
                        );
                }
                return _cmdLoadInXmlFile;
            }
        }
        private RelayCommand _cmdLoadInXmlFile;

        #endregion --- LoadInExcelFile ---

        #region --- LoadFromXmlFile ---

        public ICommand LoadFromXmlFile
        {
            get
            {
                if (_cmdLoadFromXmlFile == null)
                {
                    _cmdLoadFromXmlFile = new RelayCommand(
                        param => LoadFromFile(param, AutofacConfig.GetContainer.Resolve<WorkForXml>())
                        );
                }
                return _cmdLoadFromXmlFile;
            }
        }
        private RelayCommand _cmdLoadFromXmlFile;

        #endregion --- LoadFromXmlFile ---

        #endregion Commands
    }
}
