using BikeShop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Specialized;

namespace BikeShop.ViewModel
{
    public class ViewModel : ViewModelBase
    {

        private Student student;
        private ObservableCollection<Student> students;
        private ICommand submitCommand;

        public Student Student
        {
            get { return student; }
            set
            {
                student = value;
                Notify("Student");
            }
        }

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                Notify("Students");
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                if(submitCommand==null)
                {
                    submitCommand = new RelayCommand(param => Submit());
                }
                return submitCommand;
            }
        }

        public ViewModel()
        {
            Student = new Student();
            Students = new ObservableCollection<Student>();
            Students.CollectionChanged += new NotifyCollectionChangedEventHandler(Students_CollectionChanged);
        }

        private void Students_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Notify("Students");
        }
        private void Submit()
        {
            Student.JoiningDate = DateTime.Today.Date;
            Students.Add(Student);
            Student = new Student();
        }
    }
}
