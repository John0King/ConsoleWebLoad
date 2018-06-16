using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Outputers
{
    public interface IOutputer:IDisposable
    {
        void AddMessage(OutputMessage message);
        void StartOutput();
        void Stop();

        Task WaitAsync(CancellationToken cancellationToken = default);
            

    }
}
