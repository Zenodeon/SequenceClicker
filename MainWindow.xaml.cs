global using DebugLogger.Wpf;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SequenceClicker.API;
using SequenceClicker.View;
using SequenceClicker.Component;
using SequenceClicker.TouchSim;

namespace SequenceClicker
{
    public partial class MainWindow : Window
    {
        OverlayWindow overlayWindow;

        private IntPtr hwnd;

        public bool basicSeqRunning;

        private SeqFileData defaultFileData;
        private SeqFileData currentFileData;

        private KeyboardListener kListener = new KeyboardListener();

        public MainWindow()
        {
            DLog.Instantiate();

            InitializeComponent();

            LocalState.MainWindow = this;

            TouchInput.Initialize();

            overlayWindow = new OverlayWindow();
            LocalState.OverlayWindow = overlayWindow;

            FileDataSetup();

            SeqCtrl.OnActionRequest = SeqCtrlAction;
            MenuTab.OnActionRequest = MenuAction;

            BasicSeq.OnSequencerComplete += OnSequencerComplete;

            kListener.KeyStateChange += ListenForHotKeys;
        }

        private void FileDataSetup()
        {
            defaultFileData = new SeqFileData();
            currentFileData = new SeqFileData();

            BasicSequencerPanel.SaveData basicSeqSD = BasicSeq.GetSaveData();
            OverlayWindow.SaveData overlayWinSD = overlayWindow.GetSaveData();

            defaultFileData.basicSeqData = basicSeqSD;
            defaultFileData.overlayWindowData = overlayWinSD;

            currentFileData.basicSeqData = basicSeqSD;
            currentFileData.overlayWindowData = overlayWinSD;
        }

        private void SeqCtrlAction(SequenceController.StateAction action)
        {
            switch (action)
            {
                case SequenceController.StateAction.Start:
                    {
                        if (!basicSeqRunning)
                            StartBasicSequence();
                    }
                    break;

                case SequenceController.StateAction.Pause:
                    {
                        BasicSeq.PauseTask();
                        basicSeqRunning = false;
                    }
                    break;

                case SequenceController.StateAction.Stop:
                    {
                        StopBasicSequence();
                    }
                    break;

                case SequenceController.StateAction.Restart:
                    {
                        if (basicSeqRunning)
                            BasicSeq.StopTask(toggleLiveMode: false);
                        BasicSeq.BeginTask(toggleLiveMode: !basicSeqRunning);
                        basicSeqRunning = true;
                    }
                    break;
            }

            void StartBasicSequence()
            {
                //overlayWindow.IgnoreInput(true);
                BasicSeq.BeginTask(!basicSeqRunning);
                basicSeqRunning = true;
            }

            void StopBasicSequence()
            {
                BasicSeq.StopTask();
                //overlayWindow.IgnoreInput(false);
                basicSeqRunning = false;
            }
        }

        private void OnSequencerComplete(object? o, EventArgs e)
        {
            basicSeqRunning = false;
            SeqCtrl.ShowStartButton(true);
        }

        private void MenuAction(MenuTab.MenuTabAction menuTabAction)
        {
            switch (menuTabAction)
            {
                case MenuTab.MenuTabAction.OpenFile:
                    {
                        SeqFileData fileData = SeqFileData.CreateFromFile();
                        if (fileData != null)
                        {
                            currentFileData = fileData;
                            UpdateElementFromData();
                        }
                    }
                    break;

                case MenuTab.MenuTabAction.NewFile:
                    {
                        currentFileData = defaultFileData;
                        UpdateElementFromData();
                    }
                    break;

                case MenuTab.MenuTabAction.SaveFile:
                    {
                        UpdateDataFromElement();
                        currentFileData.SaveData();
                    }
                    break;

                case MenuTab.MenuTabAction.SaveAsFile:
                    {
                        UpdateDataFromElement();
                        currentFileData.SaveDataAs();
                    }
                    break;
            }
        }

        private void UpdateDataFromElement()
        {
            currentFileData.basicSeqData = BasicSeq.GetSaveData();
            currentFileData.overlayWindowData = overlayWindow.GetSaveData();
        }

        private void UpdateElementFromData()
        {
            BasicSeq.LoadSaveData(currentFileData.basicSeqData);
            overlayWindow.LoadSaveData(currentFileData.overlayWindowData);
        }

        private void ListenForHotKeys(object o, RawKeyEventArgs e)
        {

        }

    #region UI Interaction

        private void Window_GotTouchCapture(object sender, TouchEventArgs e)
        {

            //foreach(System.Windows.Input.TouchPoint tp in e.GetIntermediateTouchPoints(null))
            //{
            //    DLog.Log($"{tp.Position} : {tp.Action} : {tp.Size}");
            //}
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            kListener.Dispose();
            overlayWindow.Close();
        }

        private void MinWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ToggleMenuTab(object sender, RoutedEventArgs e)
        {
            MenuTab.ToggleVisibilty();
        }

        private void OverlayToggle(object sender, RoutedEventArgs e)
        {
            overlayWindow.ToggleOverlayVisiblity();
        }

        #endregion
    }
}
