using System;
using MonoTouch.MapKit;

namespace RisksApp.UI {
  public class OrganisationMapAnnotation: MKAnnotation {
    private Organisation provider;

    public OrganisationMapAnnotation(Organisation provider) {
      if (provider == null)
        throw new ArgumentNullException ("provider");
      this.provider = provider;
    }
    
    public override MonoTouch.CoreLocation.CLLocationCoordinate2D Coordinate {
      get { return provider.Coordinate.CLLocationCoordinate;} 
      set { 
        // provider.Coordinate.CLLocationCoordinate = value;
      }
    }

    public override string Title { get { return provider.name; } }

    public override string Subtitle { get { return provider.contact; } }
    
    public Organisation Provider { get { return this.provider; } }
  }
}

