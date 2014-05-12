using System;

namespace RisksApp.Core {
  public class Location {
    public Location(string provider, double latitude, double longitude, double altitude, double accuracy, DateTime timestamp) {
      this.Provider = provider;
      this.Latitude = latitude;
      this.Longitude = longitude;
      this.Altitude = altitude;
      this.Accuracy = accuracy;
      this.Timestamp = timestamp;
    }
    
    public string Provider {get; private set;}
    public double Latitude {get; private set;}
    public double Longitude {get; private set;}
    public double Altitude {get; private set;}
    public double Accuracy {get; private set;}
    public DateTime Timestamp {get; private set;}
    
    public override string ToString ()
    {
      return string.Format ("[Location: Provider={0}, Latitude={1}, Longitude={2}, Altitude={3}, Accuracy={4}, Timestamp={5}]", Provider, Latitude, Longitude, Altitude, Accuracy, Timestamp);
    }
  }
}

