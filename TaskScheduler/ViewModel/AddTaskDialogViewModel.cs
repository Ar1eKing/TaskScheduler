using Microsoft.Win32;
using GalaSoft.MvvmLight;
using TaskScheduler.Model;
using TaskScheduler.Views;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace TaskScheduler.ViewModel
{
    public class AddTaskDialogViewModel : ViewModelBase
    {
        #region Parameters
        private ObservableCollection<TaskItem> tasks;        
        private AddTaskDialog addTaskDialog;
        private TaskItem _buffTask;
        public TaskItem BuffTask
        {
            get { return _buffTask; }
            set { Set(ref _buffTask, value); }
        }

        public RelayCommand AddTaskCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand OpenPathDialogCommand { get; set; }
        public RelayCommand OpenAddDialogCommand { get; set; }
        #endregion

        public AddTaskDialogViewModel(ref ObservableCollection<TaskItem> tasks)
        {
            this.tasks = new ObservableCollection<TaskItem>();
            this.tasks = tasks;
            BuffTask = new TaskItem();

            OpenAddDialogCommand = new RelayCommand(openAddDialog);
            AddTaskCommand = new RelayCommand(addTask);
            CloseCommand = new RelayCommand(close);
            OpenPathDialogCommand = new RelayCommand(openPathDialog);
        }

        private void openAddDialog()
        {
            addTaskDialog = new AddTaskDialog();
            addTaskDialog.Show();
        }

        private void openPathDialog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Program File (.exe)|*.exe|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            bool? clickedOK = openFileDialog1.ShowDialog();
            if (clickedOK == true)
                BuffTask.ExecutablePath = openFileDialog1.FileName;
        }

        private void addTask()
        {
            BuffTask.ID = tasks.Count + 1;
            tasks.Add(BuffTask);
            XmlDataProvider.addTaskToData(BuffTask);
            addTaskDialog.Close();
        }

        private void close() =>
            addTaskDialog.Close();
    }
}