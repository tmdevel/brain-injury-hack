using System;
using System.Diagnostics;

namespace TotalMobile.Infrastructure.Logging {
  public class DebugLog : ILog {
    public void Info(string format, params object[] args) {
      Debug.WriteLine(String.Format(format, args), "INFO");
    }

    public void Warn(string format, params object[] args) {
      Debug.WriteLine(String.Format(format, args), "WARN");
    }

    public void Error(Exception exception) {
      Debug.WriteLine(exception.ToString(), "ERROR");
    }

    public void Error(Exception exception, string format, params object[] args) {
      Debug.WriteLine(String.Format("{0} - {1}", String.Format(format, args), exception.ToString()), "ERROR");
    }
  }
}

