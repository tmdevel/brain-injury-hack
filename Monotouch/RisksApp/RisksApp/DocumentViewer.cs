using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;

namespace RisksApp
{
	public partial class DocumentViewer : UIViewController
	{
		private String doc = String.Empty;

		public DocumentViewer () : base ("DocumentViewer", null)
		{

		}

		public DocumentViewer(String documentFile) : base ("DocumentViewer", null) {
			doc = documentFile;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Leaflet";

			// Perform any additional setup after loading the view, typically from a nib.
			String filename = String.Format ("Documents/{0}.pdf", doc);
			String path = Path.Combine (NSBundle.MainBundle.BundlePath, filename);
			//String path = NSBundle.MainBundle.PathForResource (doc, "pdf");
			NSUrl url = NSUrl.FromFilename (path);
			NSUrlRequest request = new NSUrlRequest (url);
			WebView.LoadRequest (request);
		}
	}
}

