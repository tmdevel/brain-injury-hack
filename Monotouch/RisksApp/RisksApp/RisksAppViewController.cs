using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace RisksApp
{
	public partial class RisksAppViewController : UIViewController
	{
		private UIBarButtonItem directoryButton;
		private UIPageControl pageDots;

        public RisksAppViewController () : base ()
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


			var pagingViewController = new UIPageViewController(
				UIPageViewControllerTransitionStyle.Scroll,
				UIPageViewControllerNavigationOrientation.Horizontal,
				UIPageViewControllerSpineLocation.Min);	
			pagingViewController.View.Frame = new RectangleF(0, 0, View.Bounds.Width, 568);


			var pageDataSource = new NavigationPageDataSource ();
			pagingViewController.DataSource = pageDataSource;
			pagingViewController.SetViewControllers (new UIViewController[] { pageDataSource.controllers [0] }, UIPageViewControllerNavigationDirection.Forward, true, (e) => {});

			AddChildViewController (pagingViewController);
			pagingViewController.DidMoveToParentViewController(this);
			View.AddSubview(pagingViewController.View);

		    pageDots = new UIPageControl (new RectangleF(10, 0, 320, 20));

			pageDots.Pages = 5;
			pageDots.CurrentPage = 0;

			UIToolbar toolBar = new UIToolbar (new RectangleF (0, 568 - 37, 320, 37));
			toolBar.Translucent = false;
			toolBar.BarTintColor = UIColor.FromRGB (227, 6, 19 );
			toolBar.BackgroundColor = UIColor.FromRGB (227, 6, 19 );

			toolBar.AddSubview (pageDots);
			toolBar.BringSubviewToFront (pageDots);

			directoryButton = new UIBarButtonItem (UIImage.FromBundle ("Images/directory"), UIBarButtonItemStyle.Plain, (s,e) => ShowDirectory ());

			toolBar.SetItems ( new UIBarButtonItem[] { directoryButton }, true);

			View.AddSubview (toolBar);

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
			controllers.Add (new UINavigationController(new SupportViewController ()));
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

