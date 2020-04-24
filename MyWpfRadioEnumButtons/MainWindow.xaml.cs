using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace MyWpfRadioEnumButtons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum OperationMode
        {
            Bounds = 1,
            OutBounds,
            Dynamic
        }

        #region INotify Changed Properties  
        private OperationMode activOperationMode;
        public OperationMode ActivOperationMode
        {
            get { return activOperationMode; }
            set
            {
                SetField(ref activOperationMode, value, nameof(ActivOperationMode));
                OnPropertyChanged("IsBounds");
                OnPropertyChanged("IsOutBounds");
                OnPropertyChanged("IsDynamic");
            }
        }
        public bool IsBounds
        {
            get { return ActivOperationMode == OperationMode.Bounds; }
            set { ActivOperationMode = value ? OperationMode.Bounds : ActivOperationMode; }
        }
        public bool IsOutBounds
        {
            get { return ActivOperationMode == OperationMode.OutBounds; }
            set { ActivOperationMode = value ? OperationMode.OutBounds : ActivOperationMode; }
        }
        public bool IsDynamic
        {
            get { return ActivOperationMode == OperationMode.Dynamic; }
            set { ActivOperationMode = value ? OperationMode.Dynamic : ActivOperationMode; }
        }

        // Template for a new INotify Changed Property
        // for using CTRL-R-R
        private string xxx;
        public string Xxx
        {
            get { return xxx; }
            set { SetField(ref xxx, value, nameof(Xxx)); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_Bounds_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Bounds_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format(""));
            ActivOperationMode = OperationMode.Bounds;
        }

        /// <summary>
        /// Button_OutBounds_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OutBounds_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("Button_OutBounds_Click"));
            ActivOperationMode = OperationMode.OutBounds;
        }

        /// <summary>
        /// Button_Dynamic_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Dynamic_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("Button_Dynamic_Click"));
            ActivOperationMode = OperationMode.Dynamic;
        }

        /// <summary>
        /// Button_Reset_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("Button_Reset_Click"));
            Properties.Settings.Default.Reload();
            LoadFromSettings();
        }

        /// <summary>
        /// Button_Reload_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Reload_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("Button_Reload_Click"));
            Properties.Settings.Default.Reset();
            LoadFromSettings();
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("Button_Close_Click"));
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Window_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(String.Format("Window_Loaded"));
            LoadFromSettings();
        }

        /// <summary>
        /// Window_Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Debug.WriteLine(String.Format("Window_Closing"));
            SaveToSettings();
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// LoadFromSettings
        /// </summary>
        private void LoadFromSettings()
        {
            ActivOperationMode = (OperationMode)Properties.Settings.Default.OperationMode;
            Debug.WriteLine(String.Format("Load ActivOperationMode={0}", ActivOperationMode));
        }

        /// <summary>
        /// SaveToSettings
        /// </summary>
        private void SaveToSettings()
        {
            Properties.Settings.Default.OperationMode = (int)ActivOperationMode;
            Debug.WriteLine(String.Format("Save ActivOperationMode={0}", ActivOperationMode));

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }
}
