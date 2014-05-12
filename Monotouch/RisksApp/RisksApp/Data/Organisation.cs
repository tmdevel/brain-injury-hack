using System;
using System.Drawing;
using SQLite;
using System.Text;
using RisksApp.Utils;

namespace RisksApp {
  public class Organisation {
    public Organisation () {
    }

    [PrimaryKey]
    [Indexed]
    public int id { get; set; }

    public string  hashString;
    public string hash {
      get {
        if(string.IsNullOrEmpty(hashString)) {
          StringBuilder sb = new StringBuilder();
          sb.Append(name);
          sb.Append(address1);
          hashString = HashUtils.GetHashString(sb.ToString());
        }
        return hashString;
      }
      set {
        hashString = value;
      }
    }  

    [Indexed]
    public string name { get; set; }

    public string contact { get; set; }

    public string address1 { get; set; }

    public string address2 { get; set; }

    public string town { get; set; }

    public string county { get; set; }

    public string postcode { get; set; }

    public string telephone { get; set; }

    public string fax { get; set; }

    public string email { get; set; }

    public string website { get; set; }

    public string servicesAccessibleVia { get; set; }

    public string overviewOfServices { get; set; }

    public string organisationType { get; set; }

    public string numberOfPlaces { get; set; }

    public string ageGroups { get; set; }

    public string referral { get; set; }

    public string fullCategories { get; set; }

    public string mainCategories { get; set; }

    public double lon { get; set; }

    public double lat { get; set; }

    public string areaCovered { get; set; }

    public string otherGeography { get; set; }

    private double latitude;
    private double longitude;
    private Coordinate coordinate;
    private bool isCoordinateDirty = true;
       
    [Ignore]
    public double Latitude {
      get { 
        if(latitude == 0)
          latitude = lat;
        return latitude; 
      } 
      set { 
        latitude = value;
        this.isCoordinateDirty = true;
      }
    }

    [Ignore]
    public double Longitude {
      get { 
        if(longitude == 0)
          longitude = lon;
        return longitude; 
      } 
      set { 
        longitude = value; 
        this.isCoordinateDirty = true;
      }
    }
        
    [Ignore]
    public double Distance { get; set; }

    [Ignore]
    public bool HasLocation {
      get {  return !(lat == 0 && lon == 0); }
    }

    [Ignore]
    public Coordinate Coordinate {
      get {
        if (!isCoordinateDirty)
          return coordinate;
        coordinate = new Coordinate (this.Latitude, this.Longitude);
        this.isCoordinateDirty = false;
        return coordinate;
      }
    }

//    [Ignore]
//    public SizeF CellSize { get; set; }

    [Ignore]
    public string  DisplayServices {
      get { return overviewOfServices; }
    }

    [Ignore]
    public string DisplayCategories {
      get { return string.IsNullOrEmpty(mainCategories) ? "" : mainCategories.Replace(";", Environment.NewLine); }
    }

    [Ignore]
    public string DisplayAgeGroups {
      get { return string.IsNullOrEmpty(ageGroups) ? "" : ageGroups.Replace(";", Environment.NewLine); }
    }

    private string displayAddress = string.Empty;
    [Ignore]
    public string DisplayAddress {
      get {
        if(string.IsNullOrEmpty(displayAddress)) {
          StringBuilder sb = new StringBuilder();
          if(!string.IsNullOrEmpty(contact))
            sb.AppendLine(contact);
          
          if(!string.IsNullOrEmpty(address1))
            sb.AppendLine(address1);
          
          if(!string.IsNullOrEmpty(address2))
            sb.AppendLine(address2);
          
          if(!string.IsNullOrEmpty(town))
            sb.AppendLine(town);

          if(!string.IsNullOrEmpty(county))
            sb.AppendLine(county);
          
          if(!string.IsNullOrEmpty(postcode))
            sb.AppendLine(postcode);
          
          displayAddress = sb.ToString();
        }
        return displayAddress;
      }
    }
  }
}

