using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using TaskScheduler.Model;

namespace TaskScheduler.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
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

        #region Commands
        public RelayCommand<TaskItem> SelectionChangedCommand { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            Tasks.Add(new TaskItem(1, "Test", status.WaitForTrigger, System.DateTime.Now, @"notepad++.exe", "Some test description ..."));
            Tasks.Add(new TaskItem(2, "Second Test", status.Paused, System.DateTime.Now, @"C:\Program Files\Notepad++\notepad++.exe", "Second some test description ..."));
            Tasks.Add(new TaskItem(3, "Test #3", status.Completed, System.DateTime.Now, @"C:\Program Files\Notepad++\notepad++.exe", "Some test description for test #3 ..."));

            if (Tasks.Count >= 1)
                BuffTask = new TaskItem(Tasks[0]);

            SelectionChangedCommand = new RelayCommand<TaskItem>(SelectionChanged);
        }

        private void SelectionChanged(TaskItem buffTask)
        {
            BuffTask = new TaskItem(buffTask);
        }

        public override void Cleanup()
        {
            // Clean up if needed
            base.Cleanup();
        }
    }
}