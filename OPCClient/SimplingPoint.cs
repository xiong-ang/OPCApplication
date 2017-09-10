using System;
namespace Client
{
    public class SimplingPoint
    {
        public SimplingPoint(DateTime time, double y1, double y2, double y3)
        {
            SignalTime = time;
            Signal_Y1 = y1;
            Signal_Y2 = y2;
            Signal_Y3 = y3;
        }

        public DateTime SignalTime { get; set; }
        public double Signal_Y1 { get; set; }
        public double Signal_Y2 { get; set; }
        public double Signal_Y3 { get; set; }      
    }
}
