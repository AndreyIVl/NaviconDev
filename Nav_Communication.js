var Navicon = Navicon || {};

Navicon.navde_new_communication = (function()
{   
    var typeOnChange = function(context)
    {
        let formContext = context.getFormContext();
        let controlType = formContext.getAttribute("navde_new_type").getValue();
        if(controlType == "0")
        {
            formContext.getControl("navde_new_email").setVisible(false);
            formContext.getControl("navde_new_contactid").setVisible(true);
        }
        else 
        {
            formContext.getControl("navde_new_email").setVisible(false);
            formContext.getControl("navde_new_contactid").setVisible(true);
        }      
    }
    
    return{
        onSave : function(context)
        {
            let formContext = context.getFormContext();
            let typeAttribute = formContext.getAttribute("navde_new_type");
            typeAttribute.addOnChange(typeOnChange); 
        }
    }
})();