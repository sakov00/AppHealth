using AppHealth.Commands;
using AppHealth.Models;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace AppHealth.ViewModels
{
    public class DrawFuncViewModel : BaseViewModel
    {
        private ObservableCollection<ResultUser> _listResultUser;

        private List<List<UserInfo>> _listUserInfo;

        private ResultUser _currentResultUser;

        private ChartValues<ObservablePoint> _observablePoints;

        private CartesianMapper<ObservablePoint> _mapper;

        public ObservableCollection<ResultUser> ListResultUser
        {
            get => _listResultUser;
            set
            {
                _listResultUser = value;
                OnPropertyChanged();
            }
        }

        public List<List<UserInfo>> ListUserInfo
        {
            get => _listUserInfo;
            set
            {
                _listUserInfo = value;
                OnPropertyChanged();
            }
        }

        public ResultUser CurrentResultUser
        {
            get => _currentResultUser;
            set
            {
                _currentResultUser = value;
                OnPropertyChanged();
            }
        }

        public ChartValues<ObservablePoint> ObservablePoints
        {
            get => _observablePoints;
            set
            {
                _observablePoints = value;
                OnPropertyChanged();
            }
        }

        public CartesianMapper<ObservablePoint> Mapper
        {
            get => _mapper;
            set
            {
                _mapper = value;
                OnPropertyChanged();
            }
        }

        public DrawFuncViewModel()
        {
            ObservablePoints = new ChartValues<ObservablePoint>();
            ListResultUser = new ObservableCollection<ResultUser>();
            ListUserInfo = new List<List<UserInfo>>();
        }

        #region Commands

        public ICommand SelectResultUser
        {
            get
            {
                if (_cmdSelectResultUser == null)
                {
                    _cmdSelectResultUser = new RelayCommand(
                        param => SelectItem(param)
                        );
                }
                return _cmdSelectResultUser;
            }
        }

        private RelayCommand _cmdSelectResultUser;

        private void SelectItem(object param)
        {
            var selectItem = param as ResultUser;
            CurrentResultUser = selectItem;
            ObservablePoints.Clear();
            for(int i = 0; i < ListUserInfo.Count;i++)
            { 
                var userInfo = ListUserInfo[i].FirstOrDefault(x=> x.User == selectItem.User);
                if (userInfo != null)
                {
                    ObservablePoints.Add(new ObservablePoint { X = i, Y = userInfo.Steps });
                }
            }
            Mapper = Mappers.Xy<ObservablePoint>().X(x => x.X).Y(y => y.Y)
                .Fill(item =>
                {
                    if (item.Y == selectItem.BestResult)
                        return new SolidColorBrush(Color.FromRgb(0, 255, 34));
                    else if (item.Y == selectItem.WorstResult)
                        return new SolidColorBrush(Color.FromRgb(238, 83, 80));
                    else
                        return null;
                })
                .Stroke(item =>
                {
                    if (item.Y == selectItem.BestResult)
                        return new SolidColorBrush(Color.FromRgb(0, 255, 34));
                    else if (item.Y == selectItem.WorstResult)
                        return new SolidColorBrush(Color.FromRgb(238, 83, 80));
                    else
                        return null;
                });
        }

        #endregion Commands
    }
}
