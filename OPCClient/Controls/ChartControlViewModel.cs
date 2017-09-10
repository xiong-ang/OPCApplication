using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Client
{
    enum ChartMessageTokens
    {
        SimplingPointChangedFromView,
        SimplingPointChangedFromViewModel
    }

    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public MeasureModel()
        {
        }
        public MeasureModel(DateTime dateTime, double value)
        {
            DateTime = dateTime;
            Value = value;
        }    
    }

    class ChartControlViewModel:ViewModelBase
    {
        public ChartControlViewModel()
        {
            //Configure LiveCharts to handle MeasureModel class
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //Save the mapper config globally.
            Charting.For<MeasureModel>(mapper);


            //Initialize Property
            ThisSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = Properties.Resources.SINUSOIDAL_SIGNAL,
                    Values = new ChartValues<MeasureModel> {},
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize=6
                },
                new LineSeries
                {
                    Title = Properties.Resources.SQUARE_SIGNAL,
                    Values = new ChartValues<MeasureModel> {},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize=6,
                    LineSmoothness=0.0
                },
                new LineSeries
                {
                    Title = Properties.Resources.TRIANGLE_SIGNAL,
                    Values = new ChartValues<MeasureModel> {},
                    PointGeometry = DefaultGeometries.Triangle,
                    PointGeometrySize=6,
                    LineSmoothness=0.0
                }
            };

            DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

            simplingDurationItem =new int[]{ 5, 10, 20 };
            SimplingDurationItem = new ObservableCollection<string> { "5s", "10s", "20s" };
            simplingDurationIndex = 2;


            //Animation thread
            var animationThread = new Thread(new ThreadStart(
                //Animation thread function
                //Change axis show limit to generate animation
                //Update the show limit every 200ms
                () =>
                {
                    while (true)
                    {
                        AxisMax = DateTime.Now.Ticks;
                        AxisMin = DateTime.Now.Ticks - TimeSpan.FromSeconds(simplingDurationItem[SimplingDurationIndex]).Ticks;
                        Thread.Sleep(500);
                    }
                }
                ));
            animationThread.IsBackground = true;
            //Start animation thread
            animationThread.Start();


            //Registe message from view
            Messenger.Default.Register<SimplingPoint>(this, ChartMessageTokens.SimplingPointChangedFromView, (msg) =>
            {
                ThisSimplingPoint = msg;
            });           
        }

        //Changable signal values
        private SimplingPoint thisSimplingPoint;
        private SimplingPoint ThisSimplingPoint
        {
            get { return thisSimplingPoint; }
            set 
            {
                thisSimplingPoint = value;
                UpdateData();
            }
        }        

        //Signal values collection
        public SeriesCollection ThisSeriesCollection { get; set; }

        //lets set how to display the X Labels
        public Func<double, string> DateTimeFormatter { get; set; }

        //Simpling point item list
        private int[] simplingDurationItem;
        public ObservableCollection<string> SimplingDurationItem { get; set; }     
        
        //The displayed number of Simpling point
        private int simplingDurationIndex;
        public int SimplingDurationIndex
        {
            get { return simplingDurationIndex; }
            set 
            {
                simplingDurationIndex = value;
                RaisePropertyChanged("SimplingDurationIndex");
            }
        }

        //Showed max limit
        private double axisMax;       
        public double AxisMax
        {
            get { return axisMax; }
            set
            {
                axisMax = value;
                RaisePropertyChanged("AxisMax");
            }
        }
        //Showed min limit
        private double axisMin;
        public double AxisMin
        {
            get { return axisMin; }
            set
            {
                axisMin = value;
                RaisePropertyChanged("AxisMin");
            }
        }

        //The last time of updating show limit
        private DateTime lastUpdateTime = DateTime.MinValue;   

        //The saved simpling point count
        private const int savedSimplingCount = 100;

        //Update chart data
        private void UpdateData()
        {
            DateTime signalTime = ThisSimplingPoint.SignalTime;          

            if (signalTime.Subtract(lastUpdateTime).TotalMilliseconds < 2000)
            //Add signal values
            {
                ThisSeriesCollection[0].Values.Add(new MeasureModel(signalTime, ThisSimplingPoint.Signal_Y1));
                ThisSeriesCollection[1].Values.Add(new MeasureModel(signalTime, ThisSimplingPoint.Signal_Y2));
                ThisSeriesCollection[2].Values.Add(new MeasureModel(signalTime, ThisSimplingPoint.Signal_Y3));                
            }
            else
            //Remove (0.0,0.0,0.0) at the first time
            //Add the missing point if simpling is resumed
            {
                ThisSeriesCollection[0].Values.Clear();
                ThisSeriesCollection[1].Values.Clear();
                ThisSeriesCollection[2].Values.Clear();
                /*  
                ThisSeriesCollection[0].Values.Add(new MeasureModel(signalTime, double.NaN));
                ThisSeriesCollection[1].Values.Add(new MeasureModel(signalTime, double.NaN));
                ThisSeriesCollection[2].Values.Add(new MeasureModel(signalTime, double.NaN));
                */            
            }
            lastUpdateTime = signalTime;
            
            //Only use the last (savedSimplingCount) values            
            if (ThisSeriesCollection[0].Values.Count > savedSimplingCount)
            {
                    ThisSeriesCollection[0].Values.RemoveAt(0);
                    ThisSeriesCollection[1].Values.RemoveAt(0);
                    ThisSeriesCollection[2].Values.RemoveAt(0);
            }  
        }    
    }
}
