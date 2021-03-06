﻿using Robot;
using System;
using System.ComponentModel;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Viewer.ViewModel
{
    public class RobotViewModel : INotifyPropertyChanged
    {
        // Shape (position and orientation and sensor ranges, sensor values)
        private readonly LineAndWallDetectorRobot robot;

        public LineAndWallDetectorRobot Robot => robot;

        public double X => robot.Location.X;
        public double Y => robot.Location.Y;
        public Single Orientation => Convert.ToSingle(robot.Orientation);

        public string LeftDistanceText => $"Left: {(int)robot.LeftWallSensor.GetDistance()}";
        public string RightDistanceText => $"Right: {(int)robot.RightWallSensor.GetDistance()}";

        public BitmapImage Image;
        public Vector3 ImageCenterPoint;
        public Vector3 ImageCenterTranslation;

        private DispatcherTimer timer;

        public RobotViewModel(LineAndWallDetectorRobot robot)
        {
            this.robot = robot;
            this.Image = new BitmapImage
            {
                UriSource = new Uri(@"ms-appx:///Assets/Robot.png")
            };
            ImageCenterPoint = new Vector3(16.0F, 25.0F, 0.0F);
            ImageCenterTranslation = new Vector3(-16.0F, -25.0F, 0.0F);

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            timer.Tick += Timer_Tick;
        }

        public void StartMonitoringModelProperties()
        {
            timer.Start();
        }

        #region INCP triggered by timer
        // Now we use a timer to trigger the updates, not a full INCP chain starting from the model.
        private void Timer_Tick(object sender, object e)
        {
            // Note: Left and right distances will be fully re-evaluated everytime!
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.X)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Y)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Orientation)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LeftDistanceText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.RightDistanceText)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void TriggerSimulationTick()
        {
            // Note: called by the UI timer to trigger simulation ticks.
            robot.Environment.Tick();
        }
        #endregion
    }
}
