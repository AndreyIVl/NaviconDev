using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Test.Nav.Plugins.Contact.Handler
{
    class AgreementService
    {
        private readonly IOrganizationService _service;
        public AgreementService(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public void SetAgreementDate(Entity agrement)
        {
            QueryExpression query = new QueryExpression("contact");
            var link = query.AddLink("navde_new_agreement", "contactid", "navde_new_contact");
            var referencesId = agrement.GetAttributeValue<EntityReference>("navde_new_contact").Id;
            query.Criteria.AddCondition("contactid", ConditionOperator.Equal, referencesId);
            var rez = _service.RetrieveMultiple(query);
            if (rez.Entities.Count < 0)
            {
                var contactToUpdate = new Entity("contact", referencesId);
                contactToUpdate["navde_new_date"] = agrement["navde_new_date"];
                _service.Update(contactToUpdate);
            }
        }
    }
}
