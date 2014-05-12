using System;
namespace RisksApp.Core {
	public interface ICommandContext<T> {
		T Value {get;}
	}
}

