using System;

namespace RisksApp.Utilities {
  public static class FormatExtensions {
//    private static String DistanceFormatKM = Locale.GetText ("{0:0.##} km");
//    private static String DistanceFormatM = Locale.GetText ("{0:0.##} m");
    private static String DistanceFormatMiles = Locale.GetText ("{0:0.##} Miles");

    public static String ToDistanceString(this double distance) {
      if (distance <= 0)
        return String.Empty;

      return String.Format (DistanceFormatMiles, ConvertMetersToMiles(distance));
    }

    public static double ConvertMetersToMiles(double meters) {
      return (meters / 1609.344);
    }

  }
}
