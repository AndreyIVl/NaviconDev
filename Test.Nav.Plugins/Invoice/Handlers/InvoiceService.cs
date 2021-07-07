using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Test.Nav.Plugins.Invoice.Handlers
{
    class InvoiceService
    {
        private readonly IOrganizationService _service;
        public InvoiceService(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void UpdateInvoiceType(Entity invoice)
        {
            if (!invoice.Contains("navde_new_type"))
            {
                invoice["navde_new_type"] = new OptionSetValue(0);
                _service.Update(invoice);
            }
        }
        public void InvoiceDelete(Entity invoice)
        {
            var entityRefId = GetAgreementId(invoice);
            var agreements = GetAgreementEntityWithColumSet(entityRefId, "navde_new_summa");
            AgreementSumUpdate(agreements,invoice.GetAttributeValue<decimal>("navde_new_amount"));
            _service.Delete("navde_new_invoice", entityRefId);
        }
        private void AgreementSumUpdate(Entity agreement, decimal summa)
        {
            agreement["navde_new_summa"] = summa + agreement.GetAttributeValue<decimal>("navde_new_summa");
            _service.Update(agreement);
        }
        private Entity GetAgreementEntityById(Guid entityId)
        {
            return _service.Retrieve("navde_new_agreement", entityId,new ColumnSet(true));
        }
        private Entity GetAgreementEntityWithColumSet(Guid entityId,params string[] columset)
        {
            return _service.Retrieve("navde_new_agreement", entityId, new ColumnSet(columset));
        }
        private Guid GetAgreementId(Entity invoice)
        {
            return invoice.GetAttributeValue<EntityReference>("navde_new_dogovorid").Id;
        }
        public void CheckInvoiceType(Entity invoice)
        {
            if(invoice.GetAttributeValue<bool>("navde_new_type"))
            {
                var agrement = GetAgreementEntityById(GetAgreementId(invoice));
                if(agrement.GetAttributeValue<decimal>("navde_new_creditamount") >agrement.GetAttributeValue<decimal>("navde_new_fullcreditamount"))
                {
                    throw new InvalidPluginExecutionException("оплаченная сумма не может превышать максимальную сумму кредита");     
                }
                else
                {
                    agrement["navde_new_date"] = DateTime.UtcNow;
                    _service.Update(agrement);
                }
            }
        }
    }
}
