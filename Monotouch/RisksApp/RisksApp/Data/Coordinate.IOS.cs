using System;
using MonoTouch.CoreLocation; 
namespace RisksApp {
	public partial struct Coordinate {
		public CLLocationCoordinate2D CLLocationCoordinate { 
			get {
				return Location.Coordinate;
			}
		}
				
		private CLLocation location;
		public CLLocation Location {
			get {
				return location ?? (location = new CLLocation(this.Latitude, this.Longitude));
			}
		}
	}
}

