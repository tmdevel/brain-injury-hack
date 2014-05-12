using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using RisksApp.Core;
using RisksApp.UI;

namespace RisksApp
{
	public class Application
	{
		private static HardCodedServiceLocator locator;

		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			ServiceLocator.SetLocatorProvider (() => locator ?? (locator = new HardCodedServiceLocator ()));

			Dispatcher.SetDispatcher (new UIDispatcher ());

			try {
				Database.EnsureDB();
				UIApplication.Main (args, null, "AppDelegate");
			}
			catch (Exception e) {
				Console.WriteLine (e.ToString ());
				throw;
			}
		}
	}
}
