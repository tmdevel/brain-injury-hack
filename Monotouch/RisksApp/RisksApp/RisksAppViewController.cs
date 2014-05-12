using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace RisksApp
{
	public partial class RisksAppViewController : UIPageViewController
	{
        public RisksAppViewController () : base ( 
            UIPageViewControllerTransitionStyle.Scroll,
            UIPageViewControllerNavigationOrientation.Horizontal)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			var pageDataSource = new NavigationPageDataSource ();
			DataSource = pageDataSource;
			SetViewControllers (new UIViewController[] { pageDataSource.controllers [0] }, UIPageViewControllerNavigationDirection.Forward, true, (e) => {});

			//UIPageViewControllerTransitionStyle = UIPageViewControllerTransitionStyle.Scroll;


		}

		partial void ShowDirectory () {
			var directoryView = new DirectoryViewController();
			directoryView.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
			PresentViewController(directoryView, true, null);
		}
	}

	public class NavigationPageDataSource : UIPageViewControllerDataSource {
		public List<UIViewController> controllers;

		public NavigationPageDataSource() {
			controllers = new List<UIViewController> ();

			controllers.Add (new HomeViewController());
			controllers.Add (new DDViewController ());
			controllers.Add (new SymptomsViewController ());
			controllers.Add (new SeekMedicalViewController ());
			controllers.Add (new SupportViewController ());
		} 

		public override int GetPresentationCount (UIPageViewController pageViewController) {
			return controllers.Count;
		}

		public override int GetPresentationIndex (UIPageViewController pageViewController) {
			return 0;
		}

		public override UIViewController GetNextViewController (UIPageViewController pageViewController, UIViewController referenceViewController) {
			int index = controllers.IndexOf (referenceViewController);
			return index == 4 ? controllers [0] : controllers [index + 1];

		}
			
		public override UIViewController GetPreviousViewController (UIPageViewController pageViewController, UIViewController referenceViewController) {
			int index = controllers.IndexOf (referenceViewController);
			return index == 0 ? controllers [controllers.Count - 1] : controllers [index - 1];
		}

	}
}

