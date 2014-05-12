using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading;
using RisksApp.Core;

namespace RisksApp.Services {
  public abstract class ServiceBase<T> : IBaseService<T> {
    private readonly IList<IServiceObserver<T>> observers=new List<IServiceObserver<T>>();
    private readonly List<T> currentItems = new List<T>();
    private int busyCount = 0;  
    
    private bool Busy {
      get {
        return busyCount != 0;
      }
      set {       
        lock (observers) {
          Logger.DebugLog(this.GetType().ToString(), "SetBusy", value.ToString());
          bool previously = Busy;
          
          if (value) busyCount++; else busyCount--;
          if (busyCount<0) busyCount = 0;
          if (previously == Busy) return;
          
          foreach (var observer in observers) {
            if (Busy)
              observer.BeginBusy();
            else 
              observer.EndBusy();
          }
        }
      }
    }
    
    public void Refresh() {
      if (this.Busy) return;
      Logger.DebugLog(this.GetType().ToString(), "Refresh");
      ThreadPool.QueueUserWorkItem((s)=>{
        this.Busy = true;
        try {
          InternalRefresh();
        }
        finally {
          this.Busy = false;
        }
      });
    }

    protected abstract void InternalRefresh();

    private List<T> CurrentItems {
      get {  return this.currentItems; }
    }
    
    protected void NotifyObserverItemsAdded(IList<T> items) {
      Logger.DebugLog(this.GetType().ToString(), "NotifyObserverItemsAdded");
      lock (observers) {   
        this.CurrentItems.Clear();
        this.CurrentItems.AddRange(items);
        
        foreach (var observer in observers) {
          observer.ItemsUpdated(items);
        }
      }
    }
    
    public void AddObserver(IServiceObserver<T> observer) {
      if (observer == null)
        throw new ArgumentNullException("observer");
      
      Logger.DebugLog(this.GetType().ToString(), "AddObserver");
      lock (observers) {
        this.observers.Add(observer);
        Logger.DebugLog(this.GetType().ToString(), "AddObserver", "ItemsAdded");
        observer.ItemsUpdated(this.CurrentItems);
        if (Busy) {
          Logger.DebugLog(this.GetType().ToString(), "AddObserver", "BeginBusy");
          observer.BeginBusy();
        }
      }
      
    }
    
    public void RemoveObserver(IServiceObserver<T> observer) {
      if (observer == null)
        throw new ArgumentNullException("observer");
      
      Logger.DebugLog(this.GetType().ToString(), "RemoveObserver");
      lock (observers) {
        if (observers.Contains(observer))
          observers.Remove(observer);
      }
    }
  }

  public interface IBaseService<T> {
    void Refresh();
    void AddObserver(IServiceObserver<T> observer);
    void RemoveObserver(IServiceObserver<T> observer);
  }
  
  public interface IServiceObserver<T> {
    void BeginBusy();
    void EndBusy();
    void ItemsUpdated(IList<T> items);
  }
}
