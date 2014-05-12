using System;
using MonoTouch.CoreLocation;

namespace RisksApp.Core {
  public interface ILocationManager {
    bool IsLocationEnabled {get;}
    bool IsGettingLocation {get;}
    void Get(Action<LocationStatus, Location, CLLocation> callback);
  }
}

