using System;
using Microsoft.Win32;
using GalaSoft.MvvmLight;
using TaskScheduler.Model;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace TaskScheduler.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Parameters
        private ObservableCollection<TaskItem> _tasks;
        public ObservableCollection<TaskItem> Tasks
        {
            get { return _tasks; }
            set { Set(ref _tasks, value); }
        }

        private TaskItem _buffTask;
        public TaskItem BuffTask
        {
            get { return _buffTask; }
            set { Set(ref _buffTask, value); }
        }

        private TaskItem selectedTask;

        #region Commands
        public RelayCommand<TaskItem> SelectionChangedCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand OpenPathDialogCommand { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }
        #endregion
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            //Tasks = getDataFromXml();
            //tmp Data
            Tasks.Add(new TaskItem(1, "Test", status.WaitForTrigger, System.DateTime.Now, @"notepad++.exe", "Some test description ..."));
            Tasks.Add(new TaskItem(2, "Second Test", status.Paused, System.DateTime.Now, @"C:\Program Files\Notepad++\notepad++.exe", "Second some test description ..."));
            Tasks.Add(new TaskItem(3, "Test #3", status.Completed, System.DateTime.Now, @"C:\Program Files\Notepad++\notepad++.exe", "Some test description for test #3 ..."));
            //~tmp Data

            if (Tasks.Count >= 1)
                BuffTask = new TaskItem(Tasks[0]);

            SelectionChangedCommand = new RelayCommand<TaskItem>(SelectionChanged);
            OpenPathDialogCommand = new RelayCommand(openPathDialog);
            SaveChangesCommand = new RelayCommand(saveChanges);
            CloseCommand = new RelayCommand(close);
        }

        private void SelectionChanged(TaskItem selected)
        {
            selectedTask = selected;
            if (selected != null)
                BuffTask = new TaskItem(selectedTask);
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

        private void saveChanges()
        {
            if (selectedTask.Equals(BuffTask))
                return;

            Tasks[Tasks.IndexOf(selectedTask)] = new TaskItem(BuffTask);
        }

        private void close() => Environment.Exit(0);

        public override void Cleanup()
        {
            // Clean up if needed
            base.Cleanup();
        }
    }
}