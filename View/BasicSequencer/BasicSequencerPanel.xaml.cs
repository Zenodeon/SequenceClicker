﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SequenceClicker;
using SequenceClicker.View.BasicSequencer.Component;

namespace SequenceClicker.View
{
    /// <summary>
    /// Interaction logic for BasicSequencerPanel.xaml
    /// </summary>
    public partial class BasicSequencerPanel : UserControl
    {
        private ScrollViewer taskViewer { get; set; }

        ActiveTaskTab<ContentPresenter> activeTasks = new ActiveTaskTab<ContentPresenter>();

        private int currentTaskID;

        public BasicSequencerPanel()
        {
            InitializeComponent();

            TaskControl.ApplyTemplate();
            taskViewer = (ScrollViewer)TaskControl.Template.FindName("TaskViewer", TaskControl);

            activeTasks.Initialize();
            TaskControl.ItemsSource = activeTasks;

            activeTasks.Add(CreateTaskTab());
        }

        public TaskTab CreateTaskTab()
        {
            ContentPresenter frame = new ContentPresenter();
            TaskTab task = new TaskTab(this);

            task.frame = frame;

            frame.Content = task;

            return task;
        }

        public void BeginTask()
        {
            ToggleLiveMode(true);

            currentTaskID = 0;
            ExecuteCurrentTask();
        }

        public void StopTask()
        {
            ToggleLiveMode(false);

            activeTasks.tabs[currentTaskID].StopTask();
            //DLog.Log("TODO : Pause/Stop Delay");
        }

        public void ExecuteCurrentTask()
        {
            activeTasks.tabs[currentTaskID].RunTask(ExecuteNextTask);
        }

        private void ExecuteNextTask()
        {
            if (currentTaskID + 1 > activeTasks.Count - 1)
            {
                SequenceCompleted();
                return;
            }

            currentTaskID++;
            ExecuteCurrentTask();
        }

        private void ToggleLiveMode(bool state)
        {
            foreach (TaskTab tab in activeTasks.tabs)
                tab.LiveMode(state);
        }

        private void SequenceCompleted()
        {

        }

        public void ExecuteTabAction(TaskTab tab, TaskTabControl.TTAction ttAction)
        {
            int tabIndex = activeTasks.IndexOf(tab);
            int aboveIndex = tabIndex - 1;
            int belowIndex = tabIndex + 1;

            switch (ttAction)
            {
                case TaskTabControl.TTAction.MoveUp:
                    {
                        activeTasks.SwitchItem(tabIndex, aboveIndex);
                    }
                    break;

                case TaskTabControl.TTAction.RemoveSelf:
                    {
                        if (activeTasks.Count > 1)
                            activeTasks.Remove(tab);
                    }
                    break;

                case TaskTabControl.TTAction.Add:
                    {
                        activeTasks.Insert(belowIndex, CreateTaskTab());
                    }
                    break;

                case TaskTabControl.TTAction.DuplicateSelf:
                    {
                        TaskTab dupTaskTab = CreateTaskTab();
                        dupTaskTab.LoadSaveData(tab.GetSaveData());
                        activeTasks.Insert(belowIndex, dupTaskTab);
                    }
                    break;

                case TaskTabControl.TTAction.MoveDown:
                    {
                        activeTasks.SwitchItem(tabIndex, belowIndex);
                    }
                    break;
            }
        }
    }
}
