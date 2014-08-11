// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace RisksApp
{
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIView GroupView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView ImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView InformLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView InfoView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView KnowLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView RestLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView SeekLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView SignsLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (GroupView != null) {
				GroupView.Dispose ();
				GroupView = null;
			}

			if (InformLabel != null) {
				InformLabel.Dispose ();
				InformLabel = null;
			}

			if (InfoView != null) {
				InfoView.Dispose ();
				InfoView = null;
			}

			if (KnowLabel != null) {
				KnowLabel.Dispose ();
				KnowLabel = null;
			}

			if (RestLabel != null) {
				RestLabel.Dispose ();
				RestLabel = null;
			}

			if (SeekLabel != null) {
				SeekLabel.Dispose ();
				SeekLabel = null;
			}

			if (SignsLabel != null) {
				SignsLabel.Dispose ();
				SignsLabel = null;
			}
		}
	}
}
