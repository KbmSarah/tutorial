using System;
using DevExpress.Mvvm;
using MvvmExam.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmExam.Common;
using System.Windows;

namespace MvvmExam.ViewModels
{
    public class UpperViewModel : ViewModelBase
    {
        #region Construction
        public UpperViewModel()
        {
            Person = new Person();
            _timeVM = new TimeViewModel();
        }
        #endregion

        #region Members
        private Person _person;
        private ObservableCollection<Person> _people;
        private ICommand _cmdShowMessageBox;
        private TimeViewModel _timeVM;
        #endregion

        #region Properties
        public Person Person
        {
            get { return _person; }
            set { base.SetValue(ref _person, value); }
        }
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { base.SetValue(ref _people, value); }
        }
        public TimeViewModel TimeVM
        {
            get { return _timeVM; }
            set { base.SetValue(ref _timeVM, value); }
        }
        #endregion

        #region Commands
        public ICommand CmdShowMessageBox
        {
            get
            {
                if (_cmdShowMessageBox == null)
                {
                    _cmdShowMessageBox = new RelayCommand(param => ExecCmdShowMessageBox());
                }
                return _cmdShowMessageBox;
            }
        }
        private void ExecCmdShowMessageBox()
        {
            if(Person.FirstName != null)
            {
                MessageBox.Show($"{Person.FirstName} {Person.LastName}");
                Person = new Person();
            }
        }
        #endregion
    }
}