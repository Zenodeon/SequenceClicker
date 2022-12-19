using System;
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

        private int viewTaskID = 2;
        private int viewTaskIDOffset;

        public EventHandler OnSequencerComplete;

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

        public void BeginTask(bool toggleLiveMode = true)
        {
            if (toggleLiveMode)
                ToggleLiveMode(true);

            if (currentTaskID == 0)
            {
                //viewTaskIDOffset = viewTaskID;
                ExecuteCurrentTask();
            }
            else
                ExecuteNextTask();
        }

        public void PauseTask()
        {
            StopCurrentTask();
        }

        public void StopTask(bool toggleLiveMode = true)
        {
            if (toggleLiveMode)
                ToggleLiveMode(false);

            StopCurrentTask();

            currentTaskID = 0;
            viewTaskIDOffset = 0;
        }

        private void StopCurrentTask()
        {
            if (activeTasks.ContainsIndex(currentTaskID))
                activeTasks.tabs[currentTaskID].StopTask();
        }

        public void ExecuteCurrentTask()
        {
            AlignScrollView();
            activeTasks.tabs[currentTaskID].RunTask(ExecuteNextTask);
        }

        private void AlignScrollView(bool incrementToTargetOffset = true)
        {
            if (incrementToTargetOffset)
                if (currentTaskID != 0)
                    if (currentTaskID <= viewTaskID)
                        if (activeTasks.ContainsIndex(currentTaskID))
                            viewTaskIDOffset++;

            if (activeTasks.ContainsIndex(currentTaskID + viewTaskIDOffset))
                activeTasks[currentTaskID + viewTaskIDOffset].BringIntoView();
            else
            {
                viewTaskIDOffset--;
                AlignScrollView(incrementToTargetOffset: false);
            }
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
            OnSequencerComplete?.Invoke(this, null);
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

        public SaveData GetSaveData()
        {
            return new SaveData(activeTasks.tabs);
        }

        public void LoadSaveData(SaveData data)
        {
            activeTasks.Clear();

            foreach(TaskTab.SaveData saveData in data.taskTabsSD)
            {
                TaskTab tab = CreateTaskTab();
                tab.LoadSaveData(saveData);
                activeTasks.Add(tab);
            }
        }

        public struct SaveData
        {
            public List<TaskTab.SaveData> taskTabsSD;

            public SaveData(List<TaskTab> taskTabs)
            {
                taskTabsSD = new List<TaskTab.SaveData>();
                foreach (TaskTab taskTab in taskTabs)
                    taskTabsSD.Add(taskTab.GetSaveData());
            }
        }
    }
}
