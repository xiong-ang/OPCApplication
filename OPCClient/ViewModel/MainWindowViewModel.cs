using Client.Properties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;


namespace Client
{
    class MainWindowViewModel : ViewModelBase
    {
        //Constructor，Constructor Injection is used to decouple ViewModel and Model.
        public MainWindowViewModel(SignalBase inputSignal)
        {
            //Initialize Property            
            signalSimplingPoint = new SimplingPoint(DateTime.Now, 0.0, 0.0, 0.0);

            Signal_Y1_Input = true;
            Signal_Y2_Input = true;
            Signal_Y3_Input = true;

            samplingPeriodItem = new ObservableCollection<string>() { "1s", "800ms", "600ms", "400ms", "200ms" };
            samplingPeriodArray = new double[]{ 1.0, 0.8, 0.6, 0.4, 0.2 };
            samplingPeriodIndex = 3;

            statusTip = "Ready";

            saveEnabled = false;

            historyData = new List<SimplingPoint>();

            isStarted = false;

            Start_StopCommand = new RelayCommand(Start_Stop);
            SaveCommand = new RelayCommand(SaveHistoryData);

            //Update View based on the signal
            signal = inputSignal;
            signal.TiggerTimer.Interval = TimeSpan.FromSeconds(0.5);
            signal.Changed += UpdateData;
        }

        //Signal source object
        private SignalBase signal;
               
        //Simpling point
        private SimplingPoint signalSimplingPoint;
        public SimplingPoint SignalSimplingPoint
        {
            get { return signalSimplingPoint; }
            private set 
            { 
                signalSimplingPoint = value;
                RaisePropertyChanged("SignalSimplingPoint");
            }
        }
        

        // Input the signals or not
        public bool Signal_Y1_Input { private get; set; }
        public bool Signal_Y2_Input { private get; set; }        
        public bool Signal_Y3_Input { private get; set; }

        //Optional sampling period item list
        private double[] samplingPeriodArray;
        private ObservableCollection<string> samplingPeriodItem;
        public ObservableCollection<string> SamplingPeriodItem
        {
            get { return samplingPeriodItem; }
            private set 
            {
                samplingPeriodItem = value;
                RaisePropertyChanged("SamplingPeriodItem");
            }
        }

        //Selected sampling period index
        private int samplingPeriodIndex;
        public int SamplingPeriodIndex
        {
            private get { return samplingPeriodIndex; }
            set 
            {
                samplingPeriodIndex = value;
                RaisePropertyChanged("SamplingPeriodIndex");
            }
        }
        
        //Status Tips
        private string statusTip;

        public string StatusTip
        {
            get { return statusTip; }
            set 
            {
                statusTip = value;
                RaisePropertyChanged("StatusTip");
            }
        }


        //Whether the history data could be saved
        private bool saveEnabled;
        public bool SaveEnabled
        {
            get { return saveEnabled; }
            private set 
            { 
                saveEnabled = value;
                RaisePropertyChanged("SaveEnabled");
            }
        }
        
        //History data list
        private List<SimplingPoint> historyData;

        //Whether the present is started
        private bool isStarted;
        public bool IsStarted
        {
            get { return isStarted; }
            private set
            {
                isStarted = value;
                RaisePropertyChanged("IsStarted");
            }
        }

        //Start button DelegateCommand
        public RelayCommand Start_StopCommand { get; private set; }

        //Save button DelegateCommand
        public RelayCommand SaveCommand { get; private set; }       

        //Update View
        private void UpdateData(object sender, EventArgs e)
        {
            if (sender == null || e == null) 
                return;

            //Change simpling period
            signal.TiggerTimer.Interval = TimeSpan.FromSeconds(samplingPeriodArray[SamplingPeriodIndex]);
                    
            //Change signals properity
            SignalEventArgs signalArgs = e as SignalEventArgs;

            if (!Signal_Y1_Input)
                signalArgs.SimplingArgs.Signal_Y1 = 0;
            if (!Signal_Y2_Input)
                signalArgs.SimplingArgs.Signal_Y2 = 0;
            if (!Signal_Y3_Input)
                signalArgs.SimplingArgs.Signal_Y3 = 0;

            SignalSimplingPoint = signalArgs.SimplingArgs;


            //Save 10 thousand updated sampling points at most
            historyData.Add(SignalSimplingPoint);
            if (historyData.Count > maxSaveDataCount)
                historyData.RemoveAt(0);
        }

               
        //Start or stop signal presentation
        private void Start_Stop()
        {
            if (!SaveEnabled)
                SaveEnabled = true;

            IsStarted = !IsStarted;

            if (IsStarted)
                StatusTip = Resources.TIP_START;
            else
                StatusTip = Resources.TIP_STOP;

            signal.TiggerTimer.IsEnabled = !signal.TiggerTimer.IsEnabled;           
        }

        //Save 10 thousand updated sampling points at most after simpling ends
        private const int maxSaveDataCount = 10000;
        private void SaveHistoryData()
        {
            //set the save button disabled.
            SaveEnabled = false;

            StatusTip = Resources.TIP_SAVINGDATA;

            //Create a new thread to save the data
            //Main thread writes historyData and new thread reads historyData, so no thread synchronization is needed
            var saveThread = new Thread(new ThreadStart(
                //Save data thread function
                () => 
                {
                    Stream myStream;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "CSV(Comma-Separated Valuesfiles) (*.csv)|*.csv|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        //Handle IOException
                        try
                        {
                            myStream = saveFileDialog.OpenFile();
                        }
                        catch (IOException e)
                        {

                            StatusTip = Resources.TIP_ERROR + e.Message;

                            //set the save button available.
                            SaveEnabled = true;

                            return;
                        }

                        if (myStream != null)
                        {
                            StreamWriter sw = new StreamWriter(myStream);

                            // Write file
                            sw.WriteLine("Time,Signal Y1,Signal Y2,Signal Y3");
                            foreach (var d in historyData)
                                sw.WriteLine("{0},{1},{2},{3}", d.SignalTime.ToString("yyyy-MM-dd HH:mm:ss:fff"), d.Signal_Y1, d.Signal_Y2, d.Signal_Y3);

                            sw.Close();

                            StatusTip = Resources.TIP_DATASAVED;
                        }
                    }else
                        StatusTip = Resources.TIP_CANCEL;

                    //set the save button available.
                    SaveEnabled = true;
                }
                ));
            saveThread.IsBackground = true;
            saveThread.Start();            
        }         
    } 
}
