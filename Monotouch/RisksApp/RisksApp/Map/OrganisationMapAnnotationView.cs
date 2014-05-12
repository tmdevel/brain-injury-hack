using System;
using MonoTouch.MapKit;
using MonoTouch.UIKit;
using RisksApp.Core;

namespace RisksApp.UI {
	public class OrganisationMapAnnotationView : MKPinAnnotationView {
		
		public OrganisationMapAnnotationView(IntPtr handle): base(handle) {
			Initialize();
		}
		
		public OrganisationMapAnnotationView (OrganisationMapAnnotation annotation, string identifier): base(annotation, identifier) {
			Initialize();
		}
		
		private void Initialize() {
			RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
			AnimatesDrop = false;
			CanShowCallout = true;	
		}
			
		public override bool Selected {
			get {
				return base.Selected;
			}
			set {
				if (base.Selected==value) return;
				base.Selected = value;
				if (Selected) {
					OrganisationMapAnnotation s = Annotation as OrganisationMapAnnotation;
					if (s == null) return;
				}
			}
		}
	}
}

