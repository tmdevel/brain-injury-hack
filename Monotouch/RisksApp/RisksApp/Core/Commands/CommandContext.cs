using System;
namespace RisksApp.Core {
	public class CommandContext<T> : ICommandContext<T> {
		public CommandContext (T newValue) {
			this.Value = newValue;
		}
		
		public T Value {get; private set;}
	}
}

