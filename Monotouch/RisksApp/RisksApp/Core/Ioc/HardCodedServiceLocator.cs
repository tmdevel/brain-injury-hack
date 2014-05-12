using System;
using System.Collections.Generic;
using RisksApp.Core;
using RisksApp.Services;

namespace RisksApp {
	public class HardCodedServiceLocator : IServiceLocator {
		private IOrganisationService orgService;
    	private ILocationManager locManager;
		
		public HardCodedServiceLocator () {
		}
	
		public object GetInstance (Type serviceType) {
			throw new NotImplementedException ();
		}

		public object GetInstance (Type serviceType, string key) {
			throw new NotImplementedException ();
		}

		public IEnumerable<object> GetAllInstances (Type serviceType){
			throw new NotImplementedException ();
		}

		public TService GetInstance<TService> (){
      		if (typeof(TService) == typeof(ILocationManager)) 
        		return (TService) (this.locManager ?? (this.locManager = new LocationManager(30, 180, 100)));
			if (typeof(TService) == typeof(IOrganisationService))
				return (TService)(this.orgService ?? (this.orgService = new OrganisationService()));
			throw new Exception("No Type Registered");
		}

		public TService GetInstance<TService> (string key){
			throw new NotImplementedException ();
		}

		public IEnumerable<TService> GetAllInstances<TService> (){
			throw new NotImplementedException ();
		}
		public object GetService (Type serviceType){
			throw new NotImplementedException ();
		}
	}
}

