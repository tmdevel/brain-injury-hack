using System;

namespace RisksApp.Core {
  public static class ServiceLocator {  
    private static ServiceLocatorProvider currentProvider;

    public static IServiceLocator Current {
      get { return currentProvider ();}
    }

    public static void SetLocatorProvider(ServiceLocatorProvider newProvider) {
      currentProvider = newProvider;
    }
  }
}

