using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Test.Nav.Plugins.Communication.Handler
{
    class CommunicationService
    {
        private readonly IOrganizationService _service;
        public CommunicationService(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public void CheckDoubleRecords(Entity communication)
        {
            QueryExpression query = new QueryExpression("navde_new_communication");
            var communicatonEntities = _service.RetrieveMultiple(query);
            foreach (var item in communicatonEntities.Entities)
            {
                if(!haveDoblecate(communication, item))
                {
                    throw new InvalidPluginExecutionException();
                }
                else
                {
                    _service.Create(communication);
                }
            }
        }
        private bool haveDoblecate(Entity first, Entity second)
        {
            if (first.GetAttributeValue<bool>("navde_nav_main") != second.GetAttributeValue<bool>("navde_nav_main")
                && first.GetAttributeValue<bool>("navde_new_type") != second.GetAttributeValue<bool>("navde_new_type"))
            {
                return false;
            }
            else return true;
        }
    }
}
