using System;

namespace Client
{
    //My extended EventArgs class
    class SignalEventArgs : EventArgs
    {
        public SimplingPoint SimplingArgs { get; set; }

        public SignalEventArgs(SimplingPoint simplingArgs)
        {
            SimplingArgs = simplingArgs;
        }
    }
}
