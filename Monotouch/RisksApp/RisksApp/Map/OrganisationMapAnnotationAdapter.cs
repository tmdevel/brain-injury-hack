using System;
using System.Collections.Generic;

namespace RisksApp.UI {
  public static class OrganisationMapAnnotationAdapter {   
    public static Dictionary<int, OrganisationMapAnnotation> Translate(IList<Organisation> providers) {
      var annotations = new Dictionary<int, OrganisationMapAnnotation> (providers.Count);
      foreach (Organisation provider in providers) {
        if(provider.lat != 0 && provider.lon != 0)
          annotations[provider.id] = new OrganisationMapAnnotation (provider);
      }
      return annotations;
    }
  }
}

