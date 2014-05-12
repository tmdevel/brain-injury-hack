using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using RisksApp.Core;
using System.Collections.Generic;

namespace RisksApp.UI {
  public class ContactOption {
    public ContactOption(string value, ContactType type) {
      Value = value;
      Type = type;
    }

    public string Value { get; private set; }

    public ContactType Type { get; private set; }

    public void ExecuteValue(UIView currentView) {
      switch (Type) {
        case ContactType.Telephone:
          if (Phone.HasPhone())
            Phone.Call (Value, currentView);
          break;
        case ContactType.Email:
          UrlLauncher.OpenUrl (NSUrl.FromString ("mailto:?to=" + Value));
          break;
        case ContactType.Website:
          if(Value.StartsWith("www."))
            Value = string.Format("http://{0}", Value);
          UrlLauncher.OpenUrl (NSUrl.FromString (Value));
          break;
      }
    }
  }

  public enum ContactType {
    Address,
    Telephone,
    Email,
    Website,
    Fax,
  }
}

