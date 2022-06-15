namespace AppHealth.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DrawFuncViewModel _drawFuncVM;

        private WorkFilesViewModel _workFilesVM;

        public WorkFilesViewModel WorkFilesVM
        {
            get => _workFilesVM;
            set
            {
                _workFilesVM = value;
                OnPropertyChanged();
            }
        }

        public DrawFuncViewModel DrawFuncVM
        {
            get => _drawFuncVM;
            set
            {
                _drawFuncVM = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(DrawFuncViewModel drawFuncVM, WorkFilesViewModel workFilesVM)
        {
            _drawFuncVM = drawFuncVM;
            _workFilesVM = workFilesVM;
        }
    }
}
