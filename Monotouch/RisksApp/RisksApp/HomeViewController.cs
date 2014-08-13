using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RisksApp
{
	public partial class HomeViewController : UIViewController
	{
		public HomeViewController () : base ("HomeViewController", null)
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
				((UIImageView)ImageView).Image = UIImage.FromBundle ("Images/home-586h");
			else	
				((UIImageView)ImageView).Image = UIImage.FromBundle ("Images/home");
			// Perform any additional setup after loading the view, typically from a nib.
            SlideInAnimation();
		}

		public override void ViewWillLayoutSubviews ()
		{	ImageView.Frame = new RectangleF (new PointF (0, 0), new SizeF(ImageView.Bounds.Size.Width, UIScreen.MainScreen.Bounds.Height-40));
			GroupView.Frame = new RectangleF (0, UIScreen.MainScreen.Bounds.Height - ((UIDevice.CurrentDevice.CheckSystemVersion (7, 0) ? 40 : 60) + GroupView.Bounds.Height), GroupView.Bounds.Width, GroupView.Bounds.Height);
			base.ViewWillLayoutSubviews ();
		}

        private void SlideInAnimation() {
            Animate(.3f, RestLabel);
            Animate(.4f, InformLabel);
            Animate(.5f, SignsLabel);
            Animate(.6f, KnowLabel);
            Animate(.7f, SeekLabel);
        }

        public void Animate(float duration, UIView view) {
            view.Frame = new RectangleF(new PointF(view.Frame.Location.X - 100, view.Frame.Location.Y), view.Frame.Size);
            UIView.Animate(duration, 0f, UIViewAnimationOptions.CurveEaseOut, () => {
                view.Frame = new RectangleF(new PointF(view.Frame.Location.X + 100, view.Frame.Location.Y), 
                    view.Frame.Size);
            }, ()=>{});
        }
	}
}

