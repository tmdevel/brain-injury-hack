
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using RisksApp.Core;
//using RisksApp.Commands;
using System.Drawing;
using RisksApp.Services;

namespace RisksApp.UI {
  public partial class MapViewController : UIViewController, IOrganisationServiceObserver {
    private IOrganisationService service;

    public MapViewController(IntPtr handle) : base(handle) {
      Initialize ();  
    }

    [Export("initWithCoder:")]
    public MapViewController(NSCoder coder) : base(coder) {
      Initialize ();
    }

    public MapViewController() : base("MapViewController", null) {
      Initialize ();
    }
      
    private ICommand<Organisation> detailCommand;
    private ICommand filterCommand;

    void Initialize() {
      this.service = ServiceLocator.Current.GetInstance<IOrganisationService> ();
//      this.detailCommand = new OrganisationDetailCommand (this);
//      this.filterCommand = new FilterCommand (this);
      service.Refresh ();
    }
    
//    private Dictionary<int, OrganisationMapAnnotation> items = new Dictionary<int, OrganisationMapAnnotation> ();
    
    public void BeginBusy() {
      RisksApp.Core.Dispatcher.Invoke (() => this.activityIndicator.StartAnimating ()); 
    }
    
    public void EndBusy() {
      RisksApp.Core.Dispatcher.Invoke (() => this.activityIndicator.StopAnimating ());
    }

    void RemoveAllAnnotations() {
      //Remove all added annotations
      mapView.RemoveAnnotations (mapView.Annotations);
    }

		partial void CloseDirectory (MonoTouch.UIKit.UIBarButtonItem sender)
		{
			this.DismissViewController(true, null);
		}

    public void ItemsUpdated(IList<Organisation> items) {
      var annotations = OrganisationMapAnnotationAdapter.Translate (items);
      RisksApp.Core.Dispatcher.Invoke (() => {
        RemoveAllAnnotations ();
        mapView.AddAnnotations (annotations.Values.ToArray ()); });  
    }
    
    private MKPinAnnotationView HandleGetViewForAnnotation(MKMapView mapView, NSObject annotation) {
      if (annotation is MKUserLocation)
        return null;
      
      OrganisationMapAnnotation reportAnnotation = annotation as OrganisationMapAnnotation;
      if (reportAnnotation == null)
        return null;
      
      OrganisationMapAnnotationView pinView = mapView.DequeueReusableAnnotation ("REPORT-PIN") as OrganisationMapAnnotationView;
      if (pinView == null) {
        pinView = new OrganisationMapAnnotationView (reportAnnotation, "REPORT-PIN");
      }
      pinView.Annotation = reportAnnotation;
      return pinView;
    }

    ILocationManager LocationManager {
      get { return ServiceLocator.Current.GetInstance<ILocationManager> (); }
    }

    public override void ViewDidLoad() {
      base.ViewDidLoad ();
      
      this.activityIndicator.HidesWhenStopped = true;
      
      this.locateButton.Clicked += HandleLocateButtonClicked;

      CenterOnUserLocation ();

      this.mapView.DidUpdateUserLocation += HandleMapViewDidUpdateUserLocation;
      this.mapView.GetViewForAnnotation = HandleGetViewForAnnotation;
      
      this.mapView.CalloutAccessoryControlTapped += delegate(object sender, MKMapViewAccessoryTappedEventArgs e) {
        OrganisationMapAnnotation annotation = e.View.Annotation as OrganisationMapAnnotation;
        if (annotation == null)
          return;
        Organisation provider = annotation.Provider;
        detailCommand.Execute (new CommandContext<Organisation> (provider));
      };

      this.service.AddObserver (this);
    }

    private void HandleLocateButtonClicked(object sender, EventArgs e) {
      CenterOnUserLocation ();
    }

    void CenterOnUserLocation() {
      if (!LocationManager.IsLocationEnabled || (mapView.UserLocation.Coordinate.Latitude == 0 && mapView.UserLocation.Coordinate.Longitude == 0))
        mapView.CenterCoordinate = AppConstants.DefaultLocation;
      else
        this.mapView.CenterCoordinate = this.mapView.UserLocation.Coordinate;
      
      MKCoordinateRegion region = new MKCoordinateRegion (this.mapView.CenterCoordinate, new MKCoordinateSpan (0.09, 0.09));
      this.mapView.SetRegion (region, true);
    }

    void HandleMapViewDidUpdateUserLocation(object sender, MKUserLocationEventArgs e) {
      this.mapView.DidUpdateUserLocation -= HandleMapViewDidUpdateUserLocation;

      CenterOnUserLocation ();
    }

    [Obsolete]
    public override void ViewDidUnload() {
      Logger.DebugLog (this.GetType ().ToString (), "ViewDidUnload");
      this.service.RemoveObserver (this);
      RemoveAllAnnotations();
      base.ViewDidUnload ();
    }

    partial void FilterClicked(MonoTouch.UIKit.UIBarButtonItem sender) {
      filterCommand.Execute ();
    }
  }
}


