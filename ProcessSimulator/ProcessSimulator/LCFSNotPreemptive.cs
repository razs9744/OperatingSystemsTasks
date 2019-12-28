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
            int totalRunningTime = 0;
            int waitingTime = 0;
            Array.Sort(simulator.ProcessArrivingTime, (x, y) => x.Item1.CompareTo(y.Item1));
            List<Tuple<int, int>> processesList = simulator.ProcessArrivingTime.ToList();

            Tuple<int, int> currentProcess;
            while (processesList.Count != 0)
            {
                //     if(!processesList.Contains(process)) { continue; }
                currentProcess = processesList.FirstOrDefault();
                turnAroundTime += currentProcess.Item2 + waitingTime;
                totalRunningTime += currentProcess.Item2;
                processesList.Remove(currentProcess);
                var findNextProcess = from nextProcess in processesList
                                      where nextProcess != null &&
                                      nextProcess.Item1 <= totalRunningTime
                                      select nextProcess;
                processesList.Remove(findNextProcess);
            }

        }

       
    }
}
