var Navicon = Navicon || {};

Navicon.navde_new_contract = (function()
{
    var autoToCreditMapping = function(context)
    {
        formContext = context.getFormContext();
        autoId = formContext.getAttribute("navde_new_auto").getValue();
        alert(autoId);
        if(!autoId)
        {
            let fetchXML = [
                "<fetch top='50'>",
                "  <entity name='navde_new_auto'>",
                "    <link-entity name='navde_navde_new_credit_navde_new_auto' from='navde_new_autoid' to='navde_new_autoid' link-type='inner'>",
                "      <link-entity name='navde_new_credit' from='navde_new_creditid' to='navde_new_creditid' link-type='inner' intersect='true' />",
                "         <filter type='and'>",
                "           <condition attribute='navde_new_auto' operator='eq' value='" + autoId + "'/>",
                "         </filter>",
                "    </link-entity>",
                "  </entity>",
                "</fetch>",
            ].join("");

            let layoutXml = "<grid name='resultset' jump='navde_new_datestart' select='1' icon='1' preview='1'>" +
            "<row name='result' id='navde_new_creditid'>" +
            "<cell name='navde_new_name' width='150' />" +            
            "</row>" +       
            "</grid>";
        }
        formContext.getControl("navde_new_creditid").addCustomView(
            "00000000-0000-0000-0000-000000000001", "navde_new_agreement", "Lookup", fetchXml, layoutXml, true
        );
    }
    
    var setCreditTabVisible = function(context, visible)
    {
        let formContext = context.getFormContext();
        formContext.ui.tabs.get("Credit").setVisible(visible);
    }
    
    var contractHideAttribute = function(context)
    {
        setCreditTabVisible(context,false);
    }

    var setVisibleCreditFields = function(context, action)
    {
        let formContext = context.getFormContext();
        formContext.getControl("navde_new_fact").setVisible(action);
        formContext.getControl("navde_new_summa").setVisible(action);
        formContext.getControl("navde_new_creditid").setVisible(action);
        formContext.ui.get("Credit").setVisible(true);
    }
    
    var contactOrAutoOnChange = function(context)
    {
        let formContext = context.getFormContext();
        let contactrAttribute = formContext.getAttribute("navde_new_contact").getValue();
        let autoAtrtribute = formContext.getAttribute("navde_new_autoid").getValue();
        if(!contactrAttribute && !autoAtrtribute)
        {
            setCreditTabVisible(context,true);
        }
    }

    var creaditSetDisable = function(context,visible)
    {
        let formContext = context.getFormContext();

        formContext.getControl("val_creditamount").setDisabled(visible);
        formContext.getControl("val_creditperiod").setDisabled(visible);
        formContext.getControl("val_factsumma").setDisabled(visible);
        formContext.getControl("val_initialfee").setDisabled(visible);
        formContext.getControl("val_paymentplandate").setDisabled(visible);
        formContext.getControl("val_fullcreditamount").setDisabled(visible);
    }  
    
    var creditidOnChange = function(context)
    {
        let formContext = context.getFormContext();
        let creaditidAttribute = formContext.getAttribute("navde_new_creditid").getValue();
        if(!creaditidAttribute)
        {
            creaditSetDisable(context,true);
        } 
    }    

    return{
        onLoad: function(context)
        { 
            let formContext = context.getFormContext();
            //let autoId = formContext.getAttribute("navde_new_auto").getValue();
            //alert(autoId.getValue());
            formContext.getControl("navde_new_creditid").addPreSearch(autoToCreditMapping);
            setCreditTabVisible(context,false);
            contractHideAttribute(context, true)
            
            let contactAttribute = formContext.getAttribute("navde_new_contact");
            let autoAttribute = formContext.getAttribute("navde_new_autoid");
            let creditidAttribute = formContext.getAttribute("navde_new_creditid");
            
            contactAttribute.addOnChange(contactOrAutoOnChange);
            autoAttribute.addOnChange(contactOrAutoOnChange);
            creditidAttribute.addOnChange(creditidOnChange);

        }
    }
})();
Navicon.six_task = (function()
{
    var validateContractName = function(context)
    {
        let nameAttribute;
        let formContext = context.getFormContext();
        if(formContext.getAttribute("navde_name").getValue())
        {      
            nameAttribute = formContext.getAttribute("navde_name").getValue().toString(); 
            let newName = nameAttribute.replace(/^[0-9]+$/, 'g'); 
            formContext.getAttribute("navde_name").setValue(newName); 
        }
    }
    return{
        onLoad : function(context)
        {
            let formContext = context.getFormContext(); 
            let nameAttribute = formContext.getAttribute("navde_name");
            nameAttribute.addOnChange(validateContractName); 
        }
    }
})();
Navicon.credit_check_time = (function()
{
    var checkCrediteDate = function(context)
    {
        let formContext = context.getFormContext();
        Xrm.WebApi.retrieveRecord("navde_new_credit", formContext.getAttribute("navde_new_creditid").getValue(), "?$select=navde_new_dateend").then(
            function success(result)
            {
                if (!result.navde_new_dateend) 
                {
                    let dateEndCredit = new Date(result.navde_new_dateend);
                    let tooday = new Date();
                    if(tooday > dateEndCredit)
                    {
                        context.getEventArgs().preventDefault();
                        alert("срок кредита вышел!");
                    }
                }
            },
            function (error) 
            {
                console.error(error.message);
            }
        )
    }
    return{
        onLoad : function(context)
        {
            let formContext = context.getFormContext();
            let criditidAtrribute = formContext.getAttribute("navde_new_creditid").getValue();
            criditidAtrribute.addOnChange(checkCrediteDate);
        }
    }
})();
Navicon.auto_set_recommendet_price = (function()
{
    var checkCrediteDate = function(context)
    {
        let formContext = context.getFormContext();
        Xrm.WebApi.retrieveRecord("navde_new_auto", formContext.getAttribute("navde_new_auto").getValue(), "?$select=navde_new_used,navde_new_amount,navde_new_modelid").then(
            function success(result)
            {
                if (result.navde_new_used) 
                {
                    formContext.getAttribute("navde_new_summa").setValue(result.navde_new_amount);
                }
                else
                {	
                                        
                    Xrm.WebApi.retrieveRecord("navde_models", result.navde_new_modelid, "?$select=navde_new_amount").then(
                        function success(result)
                        {
                            formContext.getAttribute("navde_new_summa").setValue(result.navde_recommendedamount);
                        },
                        function (error)
                        {
                            console.error(error.message);
                        }
                    );
                } 
            },
            function (error) 
            {
                console.error(error.message);
            }
        )
    }
    return{
        onLoad : function(context)
        {
            let formContext = context.getFormContext();
            let criditidAtrribute = formContext.getAttribute("navde_new_creditid").getValue();
            criditidAtrribute.addOnChange(checkCrediteDate);
        }
    }
})();
