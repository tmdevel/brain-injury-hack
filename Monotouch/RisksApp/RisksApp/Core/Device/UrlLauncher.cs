using System;
using MonoTouch.Foundation;
using RisksApp.Core;
using MonoTouch.UIKit;

namespace RisksApp {
  public static class UrlLauncher {
    public static void OpenUrl(NSUrl url) {
      try {
        UIApplication.SharedApplication.OpenUrl (url);
      }
      catch(Exception ex) {
				Logger.DebugLog ("Unable to open url." + ex.Message);
      }
    }
  }
}

