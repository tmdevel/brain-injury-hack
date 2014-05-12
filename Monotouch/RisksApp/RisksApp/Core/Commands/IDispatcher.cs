using System;
namespace RisksApp.Core {
	public interface IDispatcher {
		void Invoke(Action action);
	}
}

