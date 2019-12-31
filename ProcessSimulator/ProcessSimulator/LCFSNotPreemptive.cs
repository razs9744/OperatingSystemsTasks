using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    class LCFSNotPreemptive : Scheduler
    {
        public override void Schedule(SimulatorModel simulator)
        {
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => x.Item1.CompareTo(y.Item1));

            if (simulator.ProcessArrivingTime.Length == 0) { return; }

            int totalRunningTime = simulator.ProcessArrivingTime[0].Item1;
            int waitingTime = 0;
            List<Tuple<int, int>> processesList = simulator.ProcessArrivingTime.ToList();
            List<Tuple<int, int>> arrivingProcessesList = processesList;

            Tuple<int, int> currentProcess;
            while (processesList.Count != 0)
            {
                if (arrivingProcessesList.Count != 0)
                {
                    currentProcess = arrivingProcessesList.FirstOrDefault();
                }
                else
                {
                    currentProcess = processesList.FirstOrDefault();
                }
                turnAroundTime += currentProcess.Item2 + waitingTime;
                totalRunningTime += currentProcess.Item2;
                processesList.Remove(currentProcess);

                arrivingProcessesList = (from nextProcess in processesList
                                         where nextProcess != null &&
                                         nextProcess.Item1 <= totalRunningTime
                                         select nextProcess).ToList();

                if (arrivingProcessesList.Count == 0) { continue; }

                arrivingProcessesList.Sort((x, y) => y.Item1.CompareTo(x.Item1));

                waitingTime = totalRunningTime - arrivingProcessesList.FirstOrDefault().Item1;

                if (waitingTime < 0)
                    waitingTime = 0;
            }
            this.PrintResult("LCFS Not Preemptive", turnAroundTime, simulator.NumProc);
        }
    }
}
