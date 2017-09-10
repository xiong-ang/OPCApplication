using System;
using System.Windows.Threading;

namespace Client
{
    //Template method Pattern
    abstract class SignalBase
    {
        //Signal changed CRL event
        public event EventHandler Changed;

        public DispatcherTimer TiggerTimer { get; set; }

        public SignalBase()
        {
            TiggerTimer = new DispatcherTimer();
            TiggerTimer.Tick += new EventHandler(RaiseChanged);
        }

        protected void RaiseChanged(object sender, EventArgs e)
        {
            if (sender == null || e == null) return;

            //Trigger the signal change event
            SignalEventArgs signalArgs = Update();//Obtain signals
            Changed.Invoke(this, signalArgs);
        }

        //Obtain signals by different ways 
        abstract protected SignalEventArgs Update();       

    }
}
