using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.Repository;
using Kotak.WebAPI.Resolver;
using Unity;
using Unity.Lifetime;

namespace Kotak.WebAPI
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			var container = new UnityContainer();
			container.RegisterType<IRepository<AdminEntity>, AdminRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleRepository<RoleEntity>, RoleRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepository<BranchEntity>, BranchRepository>(new HierarchicalLifetimeManager());
            // container.RegisterType<IRepository<DepartmentEntity>, DepartmentRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IMappingchkRepository<BranchMappingEntity>, BranchMappingRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IBranchInward<BranchInwardEntity>, BranchInwardRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IMappingchkRepository<DataUploadEntity>, DataUplaodRepository>(new HierarchicalLifetimeManager());
             
            container.RegisterType<IStatus<StatusEntity>, StatusRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDepartmentMaster<DepartmentMasterEntity>, DepartmentMasterRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUser<UserLoginEntity>, UserLoginRepository>(new HierarchicalLifetimeManager()); 
            container.RegisterType<IAvansePickupRequest<AvansePickupRequestEntity>,AvansePickupRequestRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmailNotification<EmailNotificationEntity>, EmailNotificationRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDocument<DocumentEntity>, DocumentRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRetrival<RetrivalEntity>, RetrivalRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IReport<ReportEntity>, ReportRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<Iinventory<InventoryEntity>, InventoryRepository>(new HierarchicalLifetimeManager());


            //  container.RegisterType<IMail<MailEntity>, MailRepository>(new HierarchicalLifetimeManager()); 

            //  container.RegisterType<IRepository<TemplateconfigEntity>, TemplateconfigRepository>(new HierarchicalLifetimeManager());

            //    container.RegisterType<IMappingchkRepository<DepartmentEntity>, DepartmentMappingRepository>(new HierarchicalLifetimeManager());


            //   container.RegisterType<InwardRepository<InwardEntity>, InwardRepository>(new HierarchicalLifetimeManager());


            container.RegisterType<IWarehouse<WarehouseEntity>, WarehouseRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IRetrival<RetrivalEntity>, RetrivalRepository>(new HierarchicalLifetimeManager());


            container.RegisterType<ISearch<SearchEntity>, SearchRepository>(new HierarchicalLifetimeManager());

            //  container.RegisterType<IDashboard<DashboardEntity>, DashboardRepository>(new HierarchicalLifetimeManager());

            //container.RegisterType<IRepository<ContactCommentEntity>, ContactCommentsRepository > (new HierarchicalLifetimeManager());
            //container.RegisterType<IRepository<ContactSubscribeEntity>, ContactSubscribeRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepository<HomeBannerEntity>, HomeBannersRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepository<ParticipahteSurveyEntity>, ParticipateSurveyRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepository<ResourceDocumentationEntity>, ResourcesDocumentationRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepository<ResourcesNewsEntity>, ResourcesNewsRepository>(new HierarchicalLifetimeManager());
            ///  container.RegisterType<IRepository<EmailUsEntity>, EmailRepository>(new HierarchicalLifetimeManager());


            config.DependencyResolver = new UnityResolver(container);
			config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));

            //var cors = new EnableCorsAttribute("https://dms.conceptlab.in", headers: "*", methods: "*", exposedHeaders: "X-My-Header");
            var cors = new EnableCorsAttribute("*", "*", "*");
            
            config.EnableCors(cors);
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
