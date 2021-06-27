var Navicon = Navicon || {};

Navicon.navde_new_contract = (function()
{
    var setCreditTabVisible = function(context, visible)
    {
        let formContext = context.getFormContext();
        formContext.ui.tabs.get("Credit").setVisible(visible);
    }
    
    //скрывает вкладку кредиты и все поля, кроме: номер, дата договора, контакт, модель
    //1 задание
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
    //показывает вкладку кредиты при установлении значений полей контакт и автомобиль.
    //2
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
    //делает поля доступными при выборе кредитной программы
    //3
    var creditidOnChange = function()
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
        if(formContext.getAttribute("navde_new_name").getValue())
        {      
            nameAttribute = formContext.getAttribute("navde_new_name").getValue().toString(); 
            let newName = nameAttribute.replace(/^[0-9]+$/, 'g'); 
            formContext.getAttribute("navde_new_name").setValue(newName); 
        }
    }
    return{
        onLoad : function(context)
        {
            let formContext = context.getFormContext(); 
            let nameAttribute = formContext.getAttribute("navde_new_name");
            nameAttribute.addOnChange(validateContractName); 
        }
    }
})();