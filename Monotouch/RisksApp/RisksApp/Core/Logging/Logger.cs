using System;
using System.Text;

namespace RisksApp.Core {
  public static class Logger {
    public static void DebugLog(string s) {
#if DEBUG
      Console.WriteLine(s);
#endif
    }

    public static void DebugLog(params string[] args) {
#if DEBUG
      StringBuilder sb=new StringBuilder();
      foreach (var s in args) {
        sb.Append(s);
        sb.Append("."); 
      }
      DebugLog(sb.ToString());
#endif
    }
  }
}

