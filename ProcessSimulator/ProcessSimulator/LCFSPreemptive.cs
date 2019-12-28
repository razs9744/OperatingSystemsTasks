using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    public class LCFSPreemptive : Scheduler
    {
        public override void Schedule(SimulatorModel simulator)
        {
            int lastBurstTime = 0;
            //sort in descending order 
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => y.Item1.CompareTo(x.Item1));

            for (int i = 0; i < simulator.NumProcBigThanZero; i++)
            {
                if (i > 0) { lastBurstTime += simulator.ProcessArrivingTime[i - 1].Item2; }

                turnAroundTime += lastBurstTime + simulator.ProcessArrivingTime[i].Item2;
            }
            this.PrintResult("LCFS Preemptive", turnAroundTime, simulator.NumProc);
        }
    }
}
