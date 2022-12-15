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

using SequenceClicker.Component;
using SequenceClicker.TouchSim;

namespace SequenceClicker.View.BasicSequencer.Component
{
    /// <summary>
    /// Interaction logic for TaskTab.xaml
    /// </summary>
    public partial class TaskTab : UserControl
    {
        public ContentPresenter frame { get; set; }
        public int taskIndex { get; set; }

        private List<IDelay> delayModules = new List<IDelay>();

        private ScreenPoint sPoint;

        private bool stopTask = false;

        private DelayControl currentDelay;

        private BasicSequencerPanel sequencerPanel;

        public TaskTab(BasicSequencerPanel sequencerPanel)
        {
            InitializeComponent();

            this.sequencerPanel = sequencerPanel;

            AddDelayModules();

            LeftTTCtrl.OnActionRequest = OnActionClicked;
            RightTTCtrl.OnActionRequest = OnActionClicked;
        }

        private void OnActionClicked(TaskTabControl.TTAction ttAction)
        {
            sequencerPanel.ExecuteTabAction(this, ttAction);
        }

        private void AddDelayModules()
        {
            delayModules.Add(SDelay);
            delayModules.Add(HDelay);
        }

        public void LiveMode(bool state)
        {
            foreach (IDelay module in delayModules)
                module.LiveMode(state);
        }

        private void TaskActive(bool state)
        {
            TTIndicatorLeft.FadeProperty(OpacityProperty, state ? 1 : 0.4f, 150);
            TTIndicatorRight.FadeProperty(OpacityProperty, state ? 1 : 0.4f, 150);
        }

        public void RunTask(Action callback = null)
        {
            TaskActive(true);

            RunSubTask(TaskType.PressInput);

            void RunSubTask(TaskType task)
            {
                switch (task)
                {
                    case TaskType.PressInput:

                        Action onStartDelay = () => PressInput(() => RunSubTask(TaskType.HoldInput));
                        currentDelay = SDelay.Delay(null, onStartDelay);

                        break;

                    case TaskType.HoldInput:

                        Action onHoldDelay = () => ReleaseInput(() => RunSubTask(TaskType.TaskCompleted));
                        currentDelay = HDelay.Delay(UpdateInput, onHoldDelay);

                        break;

                    case TaskType.TaskCompleted:

                        currentDelay = null;

                        TaskActive(false);

                        if (!stopTask)
                            callback?.Invoke();

                        break;
                }
            }
        }

        public void StopTask()
        {
            stopTask = true;

            if (currentDelay != null)
                currentDelay.StopDelay();
        }

        public void PressInput(Action onPress)
        {
            int id = KSelect.GetPointID();
            sPoint = LocalState.OverlayWindow.GetPoint(id);

            if (sPoint.id != -1)
            {
                TouchInput.SetTouchPoint(id, sPoint.targetPoint);
                TouchInput.ExecuteTouchAction(TouchInput.TouchAction.Touch);
            }
            else
            {
                DLog.Log($"ID : {sPoint.id} Does not Exist");
            }

            onPress?.Invoke();
        }

        public void UpdateInput()
        {
            if (sPoint.id != -1)
            {
                TouchInput.ExecuteTouchAction(TouchInput.TouchAction.Update);
            }
            else
            {
                DLog.Log($"ID : {sPoint.id} Does not Exist");
            }
        }

        public void ReleaseInput(Action onRelease)
        {
            if (sPoint.id != -1)
            {
                TouchInput.ExecuteTouchAction(TouchInput.TouchAction.End);
            }
            else
            {
                DLog.Log($"ID : {sPoint.id} Does not Exist");
            }

            onRelease?.Invoke();
        }

        private enum TaskType
        {
            PressInput,
            HoldInput,
            TaskCompleted
        }

        private void ToggleLeftTTCtrl(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton != MouseButtonState.Pressed)
                return;

            ToggleElementVisibility(TTIndicatorLeft);
            ToggleElementVisibility(LeftTTCtrl);

            TTIndicatorRight.Visibility = Visibility.Visible;
            RightTTCtrl.Visibility = Visibility.Collapsed;
        }

        private void ToggleRightTTCtrl(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton != MouseButtonState.Pressed)
                return;

            ToggleElementVisibility(TTIndicatorRight);
            ToggleElementVisibility(RightTTCtrl);

            TTIndicatorLeft.Visibility = Visibility.Visible;
            LeftTTCtrl.Visibility = Visibility.Collapsed;
        }

        private void ToggleElementVisibility(UIElement element)
        {
            element.Visibility = element.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible; ;
        }

        public SaveData GetSaveData()
        {
            return new SaveData(KSelect, SDelay, HDelay);
        }

        public void LoadSaveData(SaveData data)
        {
            KSelect.LoadSaveData(data.kSelect);

            SDelay.LoadSaveData(data.sDelaySD);
            HDelay.LoadSaveData(data.hDelaySD);
        }

        public struct SaveData
        {
            public KeySelectControl.SaveData kSelect;

            public DelayControl.SaveData sDelaySD;
            public DelayControl.SaveData hDelaySD;

            public SaveData(KeySelectControl kSelect, DelayControl sDelay, DelayControl hDelay)
            {
                this.kSelect = kSelect.GetSaveData(); ;

                sDelaySD = sDelay.GetSaveData();
                hDelaySD = hDelay.GetSaveData();
            }
        }
    }
}
