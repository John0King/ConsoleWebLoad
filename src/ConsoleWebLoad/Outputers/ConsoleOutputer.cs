using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWebLoad.Outputers
{
    public class ConsoleOutputer : IOutputer
    {
        private readonly ConcurrentQueue<OutputMessage> _messagePool = new ConcurrentQueue<OutputMessage>();
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private bool _isEnd = false;
        private Task _t;

        public void AddMessage(OutputMessage message)
        {
            _messagePool.Enqueue(message);
        }

        public void Dispose()
        {
            _messagePool.Clear();
        }

        public void StartOutput()
        {
            _isEnd = false;
            _t = Task.Run(async () =>
            {
                while (true)
                {
                    if (_tokenSource.IsCancellationRequested)
                    {
                        throw new TaskCanceledException();
                    }
                    if(!await WriteMessageAsync())
                    {
                        if (!_isEnd)
                        {
                            await Task.Delay(10, _tokenSource.Token);
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                }
            }, _tokenSource.Token);
        }

        private ValueTask<bool> WriteMessageAsync()
        {
            if (_messagePool.TryDequeue(out var message))
            {
                if (message == null)
                {
                    return new ValueTask<bool>(true);
                }
                switch (message.Leve)
                {
                    case MessageLeve.Info:
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.Write(message.Category);
                            Console.ResetColor();
                            Console.WriteLine(message.Message);
                            break;
                        }
                    case MessageLeve.Warning:
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(message.Category);
                            Console.ResetColor();
                            Console.WriteLine(message.Message);
                            break;
                        }
                    case MessageLeve.Error:
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(message.Category);
                            Console.ResetColor();
                            Console.WriteLine(message.Message);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                return new ValueTask<bool>(true);
            }
            return new ValueTask<bool>(false);
        }

        public void Stop()
        {
            Console.ResetColor();
            _tokenSource?.Cancel();
        }
        public async ValueTask WaitAsync(CancellationToken cancellationToken = default)
        {
            _isEnd = true;
            cancellationToken.Register(() =>
            {
                _tokenSource.Cancel();
            });
            await _t;
        }
    }
}
