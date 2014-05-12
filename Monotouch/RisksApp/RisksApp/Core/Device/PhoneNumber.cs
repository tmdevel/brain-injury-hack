using System;
using System.Collections.Generic;

namespace RisksApp {
  public class PhoneNumber {
    public string Type { get; set; }
    public string Number { get; set; }
    
    public static void AddNumber(List<PhoneNumber> phoneNumbers, string key, string number) {
      if (!string.IsNullOrEmpty(number)) 
        phoneNumbers.Add(new PhoneNumber { Type = key,  Number = number });
    }
  }
}

