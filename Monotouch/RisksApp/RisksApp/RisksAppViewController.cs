using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using RisksApp.UI;

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
			pageDataSource.PageChanged += OnPageChanged;
			pagingViewController.DidFinishAnimating += (object sender, UIPageViewFinishedAnimationEventArgs e) =>  {

			};


			AddChildViewController (pagingViewController);
			pagingViewController.DidMoveToParentViewController(this);
			View.AddSubview(pagingViewController.View);

			pageDots = new UIPageControl (new RectangleF(0, 10, 320, 20));

			pageDots.Pages = 5;
			pageDots.CurrentPage = 0;
	

			UIToolbar toolBar = new UIToolbar (new RectangleF (0, 568 - 40, 320, 40));
			toolBar.Translucent = false;
			toolBar.TintColor = UIColor.White;
			toolBar.BarTintColor = UIColor.FromRGB (227, 6, 19);
			toolBar.BackgroundColor = UIColor.FromRGB (227, 6, 19);

			toolBar.AddSubview (pageDots);
			toolBar.BringSubviewToFront (pageDots);

			directoryButton = new UIBarButtonItem (UIImage.FromBundle ("Images/directory"), UIBarButtonItemStyle.Plain, (s,e) => ShowDirectory ());

			toolBar.SetItems ( new UIBarButtonItem[] { directoryButton }, true);

			View.AddSubview (toolBar);
		}

		void OnPageChanged (int pageIndex) {
			pageDots.CurrentPage = pageIndex;
		}

		partial void ShowDirectory () {

			var directoryView = new MapViewController();
				//var directoryView = new DirectoryViewController();
			directoryView.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
			PresentViewController(directoryView, true, null);
		}
	}

	public class NavigationPageDataSource : UIPageViewControllerDataSource {
		public List<UIViewController> controllers;
		public event Action<int> PageChanged;

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
			OnPageChanged (index);

			index = index == 4 ? 0 : index + 1;

			return controllers[index];
		}
			
		public override UIViewController GetPreviousViewController (UIPageViewController pageViewController, UIViewController referenceViewController) {
			int index = controllers.IndexOf (referenceViewController);
			OnPageChanged (index);

			if (index == 0) 
				return controllers[4];
			return controllers[index-1];
		}

		private void OnPageChanged(int pageIndex) {
			var invoker = PageChanged;
			if (invoker != null)
				invoker (pageIndex);
		}
	}
}

