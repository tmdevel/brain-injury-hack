using System;
using MonoTouch.CoreLocation;

namespace RisksApp {
  public static class AppConstants {
    public static double DefaultLong = -5.929935;
    public static double DefaultLat = 54.59713;
    public static CLLocationCoordinate2D DefaultLocation {
      get {  return  new CLLocationCoordinate2D(DefaultLat, DefaultLong);  }
    }

  }
}

