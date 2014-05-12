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
			
			// Perform any additional setup after loading the view, typically from a nib.
            SlideInAnimation();
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

