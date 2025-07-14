using System;
using System.Collections.Concurrent;
using System.Threading;

namespace task17
{
    public interface ICommand
    {
        void Execute();
    }

    public sealed class ServerThread : IDisposable
    {
        private readonly Thread _workerThread;
        private readonly BlockingCollection<ICommand> _commandQueue = new();
        private readonly int _workerThreadId;
        private bool _disposed = false;
        private readonly CancellationTokenSource _cts = new();

        public ServerThread()
        {
            _workerThread = new Thread(WorkerMethod)
            {
                IsBackground = true
            };
            _workerThread.Start();
            _workerThreadId = _workerThread.ManagedThreadId;
        }

        private void WorkerMethod()
        {
            try
            {
                foreach (var command in _commandQueue.GetConsumingEnumerable(_cts.Token))
                {
                    try
                    {
                        command.Execute();
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(command, ex);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                _commandQueue.Dispose();
            }
        }


        public void PostCommand(ICommand command)
        {
            if (!_commandQueue.IsAddingCompleted)
            {
                try
                {
                    _commandQueue.Add(command);
                }
                catch (InvalidOperationException)
                {

                }
            }
        }

        public int GetWorkerThreadId()
        {
            return _workerThreadId;
        }

        internal void InternalInterruptWorker()
        {
            _workerThread.Interrupt();
        }

        internal void InternalCompleteAddingQueue()
        {
            _commandQueue.CompleteAdding();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                _cts.Cancel();
                _commandQueue.CompleteAdding();
                _workerThread.Join(500);
            }
            finally
            {
                _cts.Dispose();
                _commandQueue.Dispose();
            }
        }
    }

    public static class ExceptionHandler
    {
        public static void Handle(ICommand command, Exception ex)
        {

        }
    }

    public abstract class StopCommandBase : ICommand
    {
        protected readonly ServerThread _targetServerThread;
        protected readonly int _expectedThreadId;

        public StopCommandBase(ServerThread targetServerThread)
        {
            _targetServerThread = targetServerThread ?? throw new ArgumentNullException(nameof(targetServerThread));
            _expectedThreadId = _targetServerThread.GetWorkerThreadId();
        }

        public void Execute()
        {
            if (Thread.CurrentThread.ManagedThreadId != _expectedThreadId)
            {
                throw new InvalidOperationException($"Команда '{GetType().Name}' запущена не в том потоке");
            }
            DoStopAction();
        }

        protected abstract void DoStopAction();
    }

    public class HardStopCommand : StopCommandBase
    {
        public HardStopCommand(ServerThread targetServerThread) : base(targetServerThread) { }

        protected override void DoStopAction()
        {
            _targetServerThread.InternalInterruptWorker();
        }
    }

    public class SoftStopCommand : StopCommandBase
    {
        public SoftStopCommand(ServerThread targetServerThread) : base(targetServerThread) { }

        protected override void DoStopAction()
        {
            _targetServerThread.InternalCompleteAddingQueue();
        }
    }
}
