using System;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Linq;

namespace RisksApp.Core {
  public class LocationManager : ILocationManager {
    private bool updating;
    private CLLocation bestLocation;
    private List<Action<LocationStatus, Location, CLLocation>> callbacks = new List<Action<LocationStatus, Location, CLLocation>>();
    
    private NSTimer timer;
    private CLLocationManager locationManager;
    private int timeout;
    private int maxAge;
    private float accuracy;
    
    public LocationManager(int timeout, int maxAge, float accuracy) {
      this.timeout = timeout;
      this.maxAge = maxAge;
      this.accuracy = accuracy;
      
      // CLLocationManager.Ctor needs to be invoked on the main thread 
      // otherwise it goes mental.
      NSRunLoop.Main.InvokeOnMainThread(()=>{
        locationManager = new CLLocationManager();
        locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
        locationManager.DistanceFilter = 10f;
        locationManager.Failed += HandleFailed;
        if (UIDevice.CurrentDevice.CheckSystemVersion(6,0)) 
          locationManager.LocationsUpdated += HandleLocationsUpdated;
        else 
          locationManager.UpdatedLocation += HandleUpdatedLocation;
      });
    }
    
    public void Get(Action<LocationStatus, Location, CLLocation> callback) {
      NSRunLoop.Main.InvokeOnMainThread(()=>{
        Log("==================================================");
        Log("START: Get");
        if (callback == null)
          throw new ArgumentNullException("callback");
        
        if (!IsLocationEnabled || CLLocationManager.Status == CLAuthorizationStatus.Denied || CLLocationManager.Status == CLAuthorizationStatus.Restricted) {
          callback(LocationStatus.Disabled, null, null);
          return;
        }
        
        AddCallback(callback);
      });
    }
    
    public bool IsLocationEnabled {
      get {
        return CLLocationManager.LocationServicesEnabled;
      }
    }
    
    public bool IsGettingLocation { 
      get{
        return updating || callbacks.Count > 0;
      } 
    }
    
    private void HandleFailed(Object sender, NSErrorEventArgs e) {
      updating = false;
      if (e.Error.Code == (int)CLError.LocationUnknown) return;
      UpdateCallbacks((e.Error.Code == (int)CLError.Denied) ? LocationStatus.Disabled : LocationStatus.Failed, null, null);
    }
    
    private Location ToLocation(CLLocation clLocation) {
      if (clLocation == null)
        return null;
      
      return new Location((clLocation.HorizontalAccuracy < 50) ? "gps" : "network", clLocation.Coordinate.Latitude, clLocation.Coordinate.Longitude, clLocation.Altitude, clLocation.HorizontalAccuracy, clLocation.Timestamp);
    }
    
    
    private void HandleLocationsUpdated (object sender, CLLocationsUpdatedEventArgs e) {
      CLLocation location = e.Locations.LastOrDefault();
      if (location == null) return;
      
      HandleUpdatedLocation(sender, new CLLocationUpdatedEventArgs(location, location));
    }
    
    private void HandleUpdatedLocation(object sender, CLLocationUpdatedEventArgs e) {
      Log("START: HandleUpdatedLocation");
      if (!updating) return;
      
      Log("BEFORE: Accuracy Invalid Check");
      // Make sure its accurate.
      if (e.NewLocation.HorizontalAccuracy < 0) return;
      
      Log("BEFORE: Age Check");
      // Make sure its a recent update
      DateTime locationTime = e.NewLocation.Timestamp;
      if (DateTime.Now.Subtract(locationTime.ToLocalTime()).TotalSeconds > this.maxAge) return;
      
      Log("BEFORE: Best Check");
      if (bestLocation == null || bestLocation.HorizontalAccuracy > e.NewLocation.HorizontalAccuracy) {
        Log("IN: Best Check");
        bestLocation = e.NewLocation;
        
        Log("BEFORE: Meets Accuracy setting");
        if (bestLocation.HorizontalAccuracy <= this.accuracy) {
          Log("IN: Meets Accuracy setting");
          UpdateCallbacks(LocationStatus.OK, ToLocation(bestLocation), bestLocation);
        }
      }
    }
    
    private void UpdateCallbacks(LocationStatus status, Location location, CLLocation clLocation) {
      Log("DONE: UpdateCallbacks " + status.ToString());
      lock (callbacks) {
        this.timer.Invalidate();
        bestLocation = null;
        updating = false;
        locationManager.StopUpdatingLocation();
        var c = callbacks;
        foreach (Action<LocationStatus, Location, CLLocation> callback in c) {
          callback(status, location, clLocation);
        }
        c.Clear();
        callbacks.Clear();
      }
    }
    
    [System.Diagnostics.Conditional("DEBUG")]
    private void Log(string s) {
      Logger.DebugLog(s);
    }
    
    private void AddCallback(Action<LocationStatus, Location, CLLocation> callback) {
      lock (callbacks) {
        callbacks.Add(callback);  
        if (!updating) {
          updating = true; 
          if (timer != null) {
            timer.Invalidate();
          }
          locationManager.StartUpdatingLocation();
          timer = NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(this.timeout), delegate {
            Log("KILLED");
            CLLocation local = bestLocation;
            CLAuthorizationStatus authStatus = CLLocationManager.Status;
            
            LocationStatus status = (authStatus != CLAuthorizationStatus.Authorized) ? LocationStatus.Disabled : (local==null) ? LocationStatus.Timeout : LocationStatus.OK;
            
            UpdateCallbacks(status, ToLocation(local), local);
          });
        }
      }
    }
  }
}

