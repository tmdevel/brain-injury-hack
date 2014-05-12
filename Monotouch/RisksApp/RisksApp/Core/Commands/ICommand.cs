using System;
namespace RisksApp.Core {
	public interface ICommand<T> {
		string Text {get; set;}
		bool CanExecute {get;}
		void Execute(ICommandContext<T> context);
	}

  public interface ICommand {
    string Text {get; set;}
    bool CanExecute {get;}
    void Execute();
  }
}

