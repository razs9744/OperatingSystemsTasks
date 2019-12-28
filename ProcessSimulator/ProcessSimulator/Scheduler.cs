using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    public abstract class Scheduler
    {
        protected double turnAroundTime = 0;

        public abstract void Schedule(SimulatorModel simulator);
        protected void PrintResult(string algoName,double totalTurnAroundTime, int numberOfProcesses)
        {
            Console.WriteLine($"{algoName}: mean turnaround= {totalTurnAroundTime/numberOfProcesses}");
        }
    }
}
