using System;
using DevExpress.Mvvm;

namespace MvvmExam.ViewModels
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        #region Construction
        public MainViewModel()
        {
            UpperVM = new UpperViewModel();
            LowerVM = new LowerViewModel();
        }
        #endregion

        #region Members
        private UpperViewModel _upperVM;
        private LowerViewModel _lowerVM;
        #endregion

        #region Properties
        public UpperViewModel UpperVM
        {
            get { return _upperVM; }
            set { base.SetValue(ref _upperVM, value); }
        }
        public LowerViewModel LowerVM
        {
            get { return _lowerVM; }
            set { base.SetValue(ref _lowerVM, value); }
        }
        #endregion

        #region Methods
        public void Dispose() { }
        #endregion
    }
}