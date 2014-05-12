using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RisksApp
{
	public partial class SupportTableCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("SupportTableCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("SupportTableCell");

		public SupportTableCell () : base ()
		{

		}

		public SupportTableCell (IntPtr handle) : base (handle)
		{

		}

		public static SupportTableCell Create ()
		{
			return (SupportTableCell)Nib.Instantiate (null, null) [0];
		}

		public void Bind(int index, UIColor colour, String desc) {
			ContentIndexLabel.Text = index.ToString ();
			ContentColourView.BackgroundColor = colour;
			DescriptionLabel.Lines = 0;
			DescriptionLabel.Text = desc;
		}
	}
}

