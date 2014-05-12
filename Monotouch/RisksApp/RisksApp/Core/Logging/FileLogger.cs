using System;
using System.IO;

namespace TotalMobile.Infrastructure.Logging {
  public class FileLogger: ILog {
    public FileLogger() {
    }

    public void Info(string format, params object[] args) {
      // throw new System.NotImplementedException ();
    }

    public void Warn(string format, params object[] args) {
      //throw new System.NotImplementedException ();
    }

    public void Error(System.Exception exception) {
      LogManager.Get(this.GetType()).Error(exception);
      string RootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..");
      File.WriteAllText(Path.Combine(RootDirectory , "Documents/"+"crash.txt"), exception.ToString());
    }

    public void Error(System.Exception exception, string format, params object[] args) {
      //throw new System.NotImplementedException ();
    }
  }
}

