using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RisksApp
{
	public partial class SupportViewController : UITableViewController
	{
		private const String cellIdentifier = "SupportTableCell";

		public SupportViewController () : base ("SupportViewController", null)
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
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new TableSource ();
		}

		public class TableSource : UITableViewSource {
			string[] tableItems = new string[] {
				"Information on Brain Injury", 
				"Arrival at Hospital",
				"Leaving Hospital",
				"Recovery and Early Rehab",
				"Rehabilitation",
				"Injury in Children and Young People",
				"Family Dynamics and Caring Responsibilities",
				"Support Services Available" };

			UIColor[] colours = new UIColor[] {
				UIColor.FromRGB(134, 20, 106),
				UIColor.FromRGB(18, 146, 209),
				UIColor.FromRGB(220, 93, 33),
				UIColor.FromRGB(205, 0, 106),
				UIColor.FromRGB(52, 151, 55),
				UIColor.FromRGB(7, 62, 92),
				UIColor.FromRGB(116, 63, 104),
				UIColor.FromRGB(98, 174, 52)
			};

			public override int RowsInSection (UITableView tableview, int section)
			{
				return 8;
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(cellIdentifier) as SupportTableCell;

				// if there are no cells to reuse, create a new one
				if (cell == null) {
					cell = new SupportTableCell ();
					var views = NSBundle.MainBundle.LoadNib("SupportTableCell", cell, null);
					cell = MonoTouch.ObjCRuntime.Runtime.GetNSObject( views.ValueAt(0) ) as SupportTableCell;
				}

				cell.Bind (colours[indexPath.Row], tableItems[indexPath.Row]);
				return cell;
			}

			public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				return 60;
			}
		}
	}
}

