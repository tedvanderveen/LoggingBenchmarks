namespace LoggingBenchmarks
{
    public interface ILogging
    {
        void LogOnceNoParams();
        void LogOnceOneParam(string value1);
        void LogOnceOneParam(int value1);
        void LogOnceTwoParams(string value1, int value2);
    }
}