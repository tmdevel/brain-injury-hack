using System;
namespace RisksApp.Core {
	public abstract class Command<T> : ICommand<T> {
		public Command () {
		}
		
		public string Text {get;set;}
		public bool CanExecute {get {return GetCanExecute();}}
		public virtual void Execute(ICommandContext<T> context) {
			if (CanExecute)
				DoExecute(context);
		}
		
		protected abstract void DoExecute(ICommandContext<T> context);
		protected virtual bool GetCanExecute() {
			return true;
		}
	}

  public abstract class Command : ICommand {
    public Command () {
    }
    
    public string Text {get;set;}
    public bool CanExecute {get {return GetCanExecute();}}
    public virtual void Execute() {
      if (CanExecute)
        DoExecute();
    }
    
    protected abstract void DoExecute();
    protected virtual bool GetCanExecute() {
      return true;
    }
  }
}

