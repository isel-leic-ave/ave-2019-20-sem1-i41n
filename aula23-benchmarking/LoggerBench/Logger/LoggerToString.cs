using System;
using System.Text;

namespace Logger
{
    public class LoggerToString : ILoggerPrinter
    {
        StringBuilder buffer = new StringBuilder();

        public void Write(string str)
        {
            buffer.Append(str);
        }

        public void WriteLine(string str)
        {
            Write(str + "\n");
        }

        public override string ToString()
        {
            return buffer.ToString();
        }
    }
}