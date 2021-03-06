﻿using System;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
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

        public AddTaskDialogViewModel addTaskDialogViewModel { get; set; }

        #region Commands
        public RelayCommand<TaskItem> SelectionChangedCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand OpenPathDialogCommand { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand<TaskItem> DeleteTaskCommand { get; set; }
        #endregion
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>(XmlDataProvider.getDataFromFile());

            if (Tasks.Count >= 1)
                BuffTask = new TaskItem(Tasks[0]);

            addTaskDialogViewModel = new AddTaskDialogViewModel(ref _tasks);

            SelectionChangedCommand = new RelayCommand<TaskItem>(SelectionChanged);
            OpenPathDialogCommand = new RelayCommand(openPathDialog);
            SaveChangesCommand = new RelayCommand(saveChanges);
            DeleteTaskCommand = new RelayCommand<TaskItem>(deleteTask);
            CloseCommand = new RelayCommand(close);

            Thread wacher = new Thread(TaskWacher);
            wacher.Start();
        }

        private void TaskWacher()
        {
            while (true)
            {
                foreach(var tsk in Tasks)
                {
                    if (tsk.Trigger < DateTime.Now && tsk.Status == status.WaitForTrigger)
                    {
                        Process.Start(tsk.ExecutablePath);
                        tsk.Status = status.Completed;
                    }                        
                }

                Thread.Sleep(1000);
            }
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

            XmlDataProvider.redactTaskInData(BuffTask);
            Tasks[Tasks.IndexOf(selectedTask)] = new TaskItem(BuffTask);
        }

        private void deleteTask(TaskItem selected)
        {
            Tasks.Remove(selected);
            XmlDataProvider.deleteTaskInData(selected.ID);
            BuffTask = new TaskItem();
        }

        private void close()
        {
            XmlDataProvider.saveChanges();
            Environment.Exit(0);
        }

        public override void Cleanup()
        {
            // Clean up if needed
            base.Cleanup();
        }
    }
}