using System;
using DevExpress.Mvvm;
using System.Windows.Input;
using MvvmExam.Common;
using MvvmExam.Models;

namespace MvvmExam.ViewModels
{
    public class LowerViewModel : ViewModelBase
    {
        #region Members
        private bool _toggleChecked;
        private RadioSelection _radioSelection;
        private int _sliderValue;
        #endregion

        #region Properties
        public bool ToggleChecked
        {
            get { return _toggleChecked; }
            set { base.SetValue(ref _toggleChecked, value); }
        }
        public RadioSelection RadioSelection
        {
            get { return _radioSelection; }
            set { base.SetValue(ref _radioSelection, value); }
        }
        public int SliderValue
        {
            get { return _sliderValue; }
            set { base.SetValue(ref _sliderValue, value); }
        }
        #endregion
    }
}