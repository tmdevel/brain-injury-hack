﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RisksApp
{
	public partial class DirectoryViewController : UIViewController
	{
		public DirectoryViewController () : base ("DirectoryViewController", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void CloseDirectory () {
			this.DismissViewController(true, null);
		}
	}
}

