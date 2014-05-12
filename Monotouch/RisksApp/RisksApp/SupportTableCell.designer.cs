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
	[Register ("SupportTableCell")]
	partial class SupportTableCell
	{
		[Outlet]
		MonoTouch.UIKit.UIView ContentColourView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel ContentIndexLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DescriptionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentColourView != null) {
				ContentColourView.Dispose ();
				ContentColourView = null;
			}

			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}

			if (ContentIndexLabel != null) {
				ContentIndexLabel.Dispose ();
				ContentIndexLabel = null;
			}
		}
	}
}
