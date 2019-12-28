using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Validations.IsRightNumberOfArguments(args.Length)) { return; }

            StreamReader InputFile;
            if (!Validations.IsFileExists(out InputFile, args[0])) { return; }

            if (InputFile == null) { return; }

            //Read the first line, that should present the number of processes in the input file
            string firstLine = InputFile.ReadLine();
            int amountOfProcesses = int.Parse(firstLine);
            List<Tuple<int, int>> ProcessArrivingTime = new List<Tuple<int, int>>();

            //Read all the lines with the details of the processes 
            string readLine;
            string[] processesArgs = new string[2];
            for (int i = 0; i < amountOfProcesses; i++)
            {
                readLine = InputFile.ReadLine();
                processesArgs = readLine.Split(',');
                if (int.Parse(processesArgs[1]) == 0) { continue; }
                ProcessArrivingTime.Add(new Tuple<int, int>(int.Parse(processesArgs[0]), int.Parse(processesArgs[1])));               
            }

            SimulatorModel simulatorModel = new SimulatorModel(ProcessArrivingTime.Count, ProcessArrivingTime.ToArray(), amountOfProcesses);
            List<Scheduler> schedulers = new List<Scheduler>();

            schedulers.Add(new FCFS());
            schedulers.Add(new LCFSNotPreemptive());
            schedulers.Add(new LCFSPreemptive());
            schedulers.Add(new RoundRobin());
            schedulers.Add(new SJF());

            foreach (var scheduler in schedulers)
            {
                scheduler.Schedule(simulatorModel);
            }

        }
    }
}
