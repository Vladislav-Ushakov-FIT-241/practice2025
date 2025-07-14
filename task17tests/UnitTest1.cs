using Xunit;
using task17;
using System.Threading;

public class ServerThreadTests
{
    [Fact]
    public async Task HardStop_ImmediatelyTerminates()
    {
        var server = new ServerThread();
        bool longCommandCompleted = false;
        var longCommandSignal = new ManualResetEventSlim();

        try
        {
            server.PostCommand(new ActionCommand(() =>
            {
                Thread.Sleep(1000);
                longCommandCompleted = true;
                longCommandSignal.Set();
            }));

            server.PostCommand(new HardStopCommand(server));

            bool signalReceived = longCommandSignal.Wait(500);

            Assert.False(signalReceived, "HardStop должен прервать выполнение до завершени€ команды");
            Assert.False(longCommandCompleted);
        }
        finally
        {
            server.Dispose();
        }
    }

    [Fact]
    public void SoftStop_WaitsForCompletion()
    {
        var server = new ServerThread();
        bool commandExecuted = false;
        var signal = new ManualResetEventSlim();

        try
        {
            server.PostCommand(new ActionCommand(() =>
            {
                commandExecuted = true;
                signal.Set();
            }));

            server.PostCommand(new SoftStopCommand(server));

            Assert.True(signal.Wait(1000), " оманда не выполнилась в течение таймаута");
            Assert.True(commandExecuted);
        }
        finally
        {
            server.Dispose();
        }
    }

    [Fact]
    public void StopCommands_RequireCorrectThread()
    {
        using var server = new ServerThread();

        Assert.Throws<InvalidOperationException>(() => new HardStopCommand(server).Execute());
        Assert.Throws<InvalidOperationException>(() => new SoftStopCommand(server).Execute());
    }

    private class ActionCommand : ICommand
    {
        private readonly Action _action;
        public ActionCommand(Action action) => _action = action;
        public void Execute() => _action();
    }
}