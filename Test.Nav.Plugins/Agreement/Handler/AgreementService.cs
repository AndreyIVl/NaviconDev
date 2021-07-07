using Microsoft.Xrm.Sdk;
using System;

namespace Test.Nav.Plugins.Agreement.Handler
{
    class AgreementService
    {
        private readonly IOrganizationService _service;
        public AgreementService(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public void AgreementSetFact(Entity agreement)
        {
            if (agreement.GetAttributeValue<decimal>("navde_new_factsumma") == agreement.GetAttributeValue<decimal>("navde_new_creditamount"))
            {
                agreement["navde_new_fact"] = new OptionSetValue(1);
                _service.Update(agreement);
            }
        }
    }
}
