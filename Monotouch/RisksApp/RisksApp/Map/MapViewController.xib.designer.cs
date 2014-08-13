// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace RisksApp.UI
{
	[Register ("MapViewController")]
	partial class MapViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView activityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem locateButton { get; set; }

		[Outlet]
		MonoTouch.MapKit.MKMapView mapView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UINavigationBar navigationBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UINavigationItem navItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem newSpotButton { get; set; }

		[Action ("CloseDirectory:")]
		partial void CloseDirectory (MonoTouch.UIKit.UIBarButtonItem sender);

		[Action ("FilterClicked:")]
		partial void FilterClicked (MonoTouch.UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (navigationBar != null) {
				navigationBar.Dispose ();
				navigationBar = null;
			}

			if (activityIndicator != null) {
				activityIndicator.Dispose ();
				activityIndicator = null;
			}

			if (locateButton != null) {
				locateButton.Dispose ();
				locateButton = null;
			}

			if (mapView != null) {
				mapView.Dispose ();
				mapView = null;
			}

			if (navItem != null) {
				navItem.Dispose ();
				navItem = null;
			}

			if (newSpotButton != null) {
				newSpotButton.Dispose ();
				newSpotButton = null;
			}
		}
	}
}
