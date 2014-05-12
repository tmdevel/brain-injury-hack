using System;
using System.Collections.Generic;
using System.Linq;
using RisksApp.Core;
using System.Threading;
using MonoTouch.CoreLocation;
using System.Text;

namespace RisksApp.Services {
  public class OrganisationService : ServiceBase<Organisation>, IOrganisationService {
    public string selectStart = "SELECT DISTINCT(ORGANISATION.id), ORGANISATION.name, ORGANISATION.contact, ORGANISATION.address1, ORGANISATION.address2, " +
    "ORGANISATION.town, ORGANISATION.county, ORGANISATION.postcode, ORGANISATION.telephone, ORGANISATION.fax, ORGANISATION.email, " +
    "ORGANISATION.website, ORGANISATION.servicesAccessibleVia, ORGANISATION.overviewOfServices, ORGANISATION.organisationType, " +
    "ORGANISATION.numberOfPlaces, ORGANISATION.ageGroups, ORGANISATION.referral, ORGANISATION.fullCategories, ORGANISATION.mainCategories, " +
    "ORGANISATION.lon, ORGANISATION.lat, ORGANISATION.areaCovered, otherGeography ";

    //private string distanceQuery = " FROM ORGANISATION INNER JOIN CategoryOrganisation ON ORGANISATION.id = CategoryOrganisation.organisationId WHERE (? < lon AND lon <= ?) AND (? < lat AND lat <= ?)";
    private string catQuery = " FROM ORGANISATION INNER JOIN CategoryOrganisation ON ORGANISATION.id = CategoryOrganisation.organisationId WHERE CategoryOrganisation.categoryId {0} ";
    private string filterQuery = "AND (+ORGANISATION.name LIKE ?) ";
    private string filterFormat = "%{0}%";
    private string catList = " IN (1, 2, 3, 4, 5, 6, 7, 8)";
    private string distanceWhereQuery = "AND (? < lon AND lon <= ?) AND (? < lat AND lat <= ?)";

    public void GetOrganisations(Action<List<Organisation>> callback) {
      Logger.DebugLog("GetOrganisations");
      currentCallback = callback;
      DoLocationBasedSearch();
    }

    private Action<List<Organisation>> currentCallback;

    void InvokeCallBack(List<Organisation> orgs) {
      Logger.DebugLog("InvokeCallBack");
      var invoker = currentCallback;
      if (invoker != null) {
        invoker.Invoke (orgs);
      }
    }

    ILocationManager LocationManager {
      get { return ServiceLocator.Current.GetInstance<ILocationManager>(); }
    }

    void DoLocationBasedSearch() {
      if(LocationManager.IsLocationEnabled)
        LocationManager.Get(HandleLocationManagerhandleUpdatedLocation);
      else
        GetOrganisationsForCategory(AppConstants.DefaultLocation.Latitude, AppConstants.DefaultLocation.Longitude);
    }

    private void HandleLocationManagerhandleUpdatedLocation(LocationStatus status, Location location, CLLocation clLocation) {
      Logger.DebugLog("HandleLocationManagerhandleUpdatedLocation");
      if(status != LocationStatus.OK) {
        InvokeCallBack (new List<Organisation> ());
        return;
      }
      
      GetOrganisationsForCategory (location.Latitude, location.Longitude);   
    }

    public void GetOrganisationsForCategory(double latStart, double longStart) {
			int catId = -1;
		string filter = string.Empty;

      lock (Database.Instance) {
        StringBuilder sb = new StringBuilder();
        sb.Append(selectStart);

        List<object> args = new List<object>();
        sb.Append(string.Format(catQuery, catId != -1 ? "= ?" : catList));

        if(catId != -1) 
          args.Add(catId);

        if(!string.IsNullOrEmpty(filter)) {
          sb.Append(filterQuery);
          args.Add(string.Format(filterFormat, filter));
        }

        if(string.IsNullOrEmpty(filter)) {
          sb.Append(distanceWhereQuery);
          args.Add(longStart.GetNegBoundary());
          args.Add(longStart.GetPosBoundary());
          args.Add(latStart.GetNegBoundary());
          args.Add(latStart.GetPosBoundary());
        }

        List<Organisation> orgs = Database.Instance.Query<Organisation> (sb.ToString(), args.ToArray());
        InvokeCallBack (orgs);
      }
    }

    public void GetOrganisation(int orgId, Action<Organisation> callback) {
      lock (Database.Instance) {
        Organisation org = Database.Instance.Get<Organisation>(orgId);
        if (org == null)
          callback (null);
        callback (org); 
      }
    }
   
    
    protected override void InternalRefresh() {
      this.GetOrganisations((orgs) => {
        if (orgs == null) return;
        NotifyObserverItemsAdded (orgs);  
      });
    }
  }

  public static class LocationBoundary {
    public static int SearchRadius {
      get { 
				return 10;
//        int radius = Settings.Default.AsInt(SettingKeys.SearchRadiusKey, 10);
//        return radius == 0 ? 10 : radius; 
		}
    }
    
    public static double GetNegBoundary(this double startPoint) {
      return GetBoundary (startPoint, (-1  * SearchRadius)); 
    }
    
    public static double GetPosBoundary(this double startPoint) {
      return GetBoundary (startPoint, (1  * SearchRadius)); 
    }
    
    public const double oneMileApprox = 0.01449275;
    public static double GetBoundary(double start, int miles=10) {
      if (miles == 0)
        return start;
      return start + (miles * oneMileApprox);
    }
  }

  public interface IOrganisationService : IBaseService<Organisation> {
    void GetOrganisations(Action<List<Organisation>> callback);
    void GetOrganisation(int orgId, Action<Organisation> callback);
  }

  public interface IOrganisationServiceObserver : IServiceObserver<Organisation> {

  }
}

