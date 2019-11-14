using System;
using System.Diagnostics.Tracing;

namespace LoggingBenchmarks
{
    [EventSource(Name = "LoggingBenchmarks")]
    public sealed class CustomEventSource : EventSource, ILogging
    {
        public static CustomEventSource Log = new CustomEventSource();
        private const int AcceptedId = 1;

        [Event(AcceptedId, Level = EventLevel.Informational)]
        public unsafe void LogOnceOneParam(int value1)
        {
            const int SizeData = 1;
            EventData* dataDesc = stackalloc EventSource.EventData[SizeData];
            dataDesc[0].DataPointer = (IntPtr) (&value1);
            dataDesc[0].Size = sizeof(int);

            WriteEventCore(AcceptedId, SizeData, dataDesc);
        }

        [Event(AcceptedId, Level = EventLevel.Informational)]
        public unsafe void LogOnceTwoParams(string value1, int value2)
        {
            fixed (char* arg1Ptr = value1)
            {
                const int SizeData = 2;
                EventData* dataDesc = stackalloc EventSource.EventData[SizeData];
                dataDesc[0].DataPointer = (IntPtr)(arg1Ptr);
                dataDesc[0].Size = (value1.Length + 1) * sizeof(char);
                dataDesc[1].DataPointer = (IntPtr)(&value2);
                dataDesc[1].Size = sizeof(int);

                WriteEventCore(AcceptedId, SizeData, dataDesc);
            }
        }

        public void LogOnceNoParams()
        {
            throw new NotImplementedException();
        }

        public void LogOnceOneParam(string value1)
        {
            throw new NotImplementedException();
        }
    }
}