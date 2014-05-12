using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace RisksApp.Core {
  public static class Phone {

    public static bool HasPhone() {
      return UIApplication.SharedApplication.CanOpenUrl(NSUrl.FromString("tel:11111"));
    }

    private static string cleanString(string phoneNumber) {
      phoneNumber = phoneNumber.Replace (" ", "");
      phoneNumber = phoneNumber.Replace ("(", "");
      phoneNumber = phoneNumber.Replace (")", "");
      phoneNumber = phoneNumber.Replace ("or", "/");
      phoneNumber = phoneNumber.Replace ("ext:", ",");
      phoneNumber = phoneNumber.Replace ("ext", ",");
      phoneNumber = phoneNumber.Replace ("ex", ",");
      return phoneNumber;
    }

    public static void Call(string phoneNumber, UIView view) {
//      if (string.IsNullOrEmpty (phoneNumber) || !HasPhone ())
//        return;
//
//      phoneNumber = cleanString(phoneNumber);
//      
//      string[] numbers = phoneNumber.Split('/');
//      PhoneNumberActionSheet.ShowNumberSelection(view, new List<string>(numbers));  
    }
    
    public static void Call(string phoneNumber) {
      if (string.IsNullOrEmpty(phoneNumber) || !HasPhone())
        return;

      phoneNumber = cleanString(phoneNumber);
      
      UrlLauncher.OpenUrl(NSUrl.FromString("tel:" + phoneNumber.Trim()));
    }
  }
}

