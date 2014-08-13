using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RisksApp
{
	public partial class SymptomsViewController : UIViewController
	{
		public SymptomsViewController () : base ("SymptomsViewController", null)
		{
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


			if (UIScreen.MainScreen.Bounds.Height == 568)
				((UIImageView)ImageView).Image = UIImage.FromBundle ("Images/symptoms-586h");
			else	
				((UIImageView)ImageView).Image = UIImage.FromBundle ("Images/symptoms");
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

