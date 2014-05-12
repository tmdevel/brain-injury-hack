using System;
namespace RisksApp.Core {
	public static class Dispatcher {		
		public static IDispatcher Current {
			get; private set;
		}
		
		public static void SetDispatcher(IDispatcher dispatcher) {
			Current = dispatcher;	
		}
		
		public static void Invoke(Action action) {
			if (action == null) return;
			Current.Invoke(action);
		}
		
		public static void Invoke<T>(Action<T> action, T param1) {
			if (action == null) return;
			Current.Invoke(()=>action(param1));
		}
		
		public static void Invoke<T1, T2>(Action<T1, T2> action, T1 param1, T2 param2) {
			if (action == null) return;
			Current.Invoke(()=>action(param1, param2));
		}
	}
}

