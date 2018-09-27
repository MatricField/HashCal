using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace HashCal
{
    public sealed class HashCalculator :
        Dictionary<string, CalculatorEntry>
    {
        private CancellationTokenSource CancellationTokenSource;

        public async Task Compute(FileInfo fileInfo)
        {
            void UpdateHash(CalculatorEntry entry)
            {
                using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var func = entry.InitAlgorithm();
                    var result = func.ComputeHash(stream);
                    entry.HashResult = BitConverter.ToString(result).Replace("-", "");
                }
            }
            foreach (var entry in Values)
            {
                entry.HashResult = "";
            }
            Cancel();
            using (var source = new CancellationTokenSource())
            {
                CancellationTokenSource = source;
                var tasks = new List<Task>(Values.Count);
                var factory = new TaskFactory(source.Token);
                foreach (var entry in Values)
                {
                    if (entry.IsEnabled)
                        tasks.Add(factory.StartNew(() => UpdateHash(entry)));
                }
                foreach(var task in tasks)
                {
                    try
                    {
                        if (!task.IsCanceled)
                            await task;
                    }
                    catch (TaskCanceledException) { }
                }
            }
            CancellationTokenSource = null;

        }

        public async Task Compute(string str)
        {
            void UpdateHash(CalculatorEntry entry)
            {
                var func = entry.InitAlgorithm();
                var result = func.ComputeHash(Encoding.Default.GetBytes(str));
                entry.HashResult = BitConverter.ToString(result).Replace("-", "");
            }
            foreach(var entry in Values)
            {
                entry.HashResult = "";
            }
            Cancel();
            using (var source = new CancellationTokenSource())
            {
                CancellationTokenSource = source;
                var tasks = new List<Task>(Values.Count);
                var factory = new TaskFactory(source.Token);
                foreach (var entry in Values)
                {
                    if (entry.IsEnabled)
                        tasks.Add(factory.StartNew(() => UpdateHash(entry)));
                }
                foreach (var task in tasks)
                {
                    try
                    {
                        if (!task.IsCanceled)
                            await task;
                    }
                    catch (TaskCanceledException) { }
                }
            }
            CancellationTokenSource = null;
        }

        public void Cancel()
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
        }
    }
}
