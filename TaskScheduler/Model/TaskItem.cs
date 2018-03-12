using System;
using System.Collections.Generic;

namespace TaskScheduler.Model
{
    public class TaskItem : ObservObj, IEquatable<TaskItem>
    {
        #region Parameters
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
        #endregion

        #region Constructors
        public TaskItem(int id, string taskName, status status, DateTime trigger, string executablePath, string description)
        {
            ID = id;
            TaskName = taskName;
            Status = status;
            Trigger = trigger;
            ExecutablePath = executablePath;
            Description = description;
        }

        public TaskItem(TaskItem another)
        {
            if (another == null)
                return;

            ID = another.ID;
            TaskName = another.TaskName;
            Status = another.Status;
            Trigger = another.Trigger;
            ExecutablePath = another.ExecutablePath;
            Description = another.Description;
        }

        public TaskItem()
        {
            ID = -1;
            TaskName = "";
            Status = status.Paused;
            Trigger = DateTime.Now;
            ExecutablePath = "";
            Description = "";
        }
        #endregion

        public bool Equals(TaskItem other)
        {
            if (other == null)
                return false;

            return (this.ID == other.ID && this.TaskName.Equals(other.TaskName) && this.Status == other.Status && this.Trigger == other.Trigger && this.ExecutablePath.Equals(other.ExecutablePath) && this.Description.Equals(other.Description));
        }
    }

    public class TaskComparer : IEqualityComparer<TaskItem>
    {
        public bool Equals(TaskItem x, TaskItem y)
        {
            return (x.ID == y.ID && x.TaskName.Equals(y.TaskName) && x.Status == y.Status && x.Trigger == y.Trigger && x.ExecutablePath.Equals(y.ExecutablePath) && x.Description.Equals(y.Description));
        }

        public int GetHashCode(TaskItem obj) => obj.GetHashCode();
    }
}