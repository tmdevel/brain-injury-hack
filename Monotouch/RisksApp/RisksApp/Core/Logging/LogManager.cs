using System;
namespace TotalMobile.Infrastructure.Logging {
  public static class LogManager {
    private static readonly ILog DebugLog = new DebugLog();
    public static Func<Type, ILog> Get = type => DebugLog;
  }
}

