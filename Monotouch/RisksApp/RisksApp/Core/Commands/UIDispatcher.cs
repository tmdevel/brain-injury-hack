using System;
using MonoTouch.Foundation;
using RisksApp.Core;

namespace RisksApp.UI {
	public class UIDispatcher : IDispatcher {
		private NSObject invoker =  new NSObject();
		
		public UIDispatcher () {
		}
		
		public void Invoke (Action action) {
			invoker.BeginInvokeOnMainThread(()=>action());	
		}		
	}
}

