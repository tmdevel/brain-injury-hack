using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RisksApp
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		RisksAppViewController viewController;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(227, 6, 19);
            UINavigationBar.Appearance.BackgroundColor = UIColor.FromRGB(227, 6, 19);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes(){ TextColor = UIColor.White });

            UIToolbar.Appearance.TintColor = UIColor.White;
            UIToolbar.Appearance.BarTintColor = UIColor.FromRGB(227, 6, 19);
            UIToolbar.Appearance.BackgroundColor = UIColor.FromRGB(227, 6, 19);

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new RisksAppViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

