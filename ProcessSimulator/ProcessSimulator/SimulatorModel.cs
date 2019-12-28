using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    public class SimulatorModel
    {
        public int NumProcBigThanZero { get; set; }
        public int NumProc { get; set; }
        public Tuple<int, int>[] ProcessArrivingTime { get; set; }

        public SimulatorModel(int NumProcBigThanZero, Tuple<int, int>[] ProcessArrivingTime, int NumProc)
        {
            this.NumProcBigThanZero = NumProcBigThanZero;
            this.NumProc = NumProc;
            this.ProcessArrivingTime = ProcessArrivingTime;
        }


    }
}
