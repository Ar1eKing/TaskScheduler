using System;

namespace TaskScheduler.Model
{
    class TaskItem : ObservObj
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set { Set(ref _taskName, value); }
        }

        private status _status;
        public status Status
        {
            get { return _status; }
            set { Set(ref _status, value); }
        }

        private DateTime _trigger;
        public DateTime Trigger
        {
            get { return _trigger; }
            set { Set(ref _trigger, value); }
        }

        private string _executablePath;
        public string ExecutablePath
        {
            get { return _executablePath; }
            set { Set(ref _executablePath, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }
    }

    enum status
    {
        WaitForTrigger,
        Completed,
        Paused
    }
}