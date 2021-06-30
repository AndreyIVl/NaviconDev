var Navicon = Navicon || {};
Navicon.navde_new_contract = (function(){

    function checkUserRole() {
        var userRoles = Xrm.Utility.getGlobalContext().userSettings.roles;
        userRoles.forEach(role => {
            if(role.name=="System Administrator")
            {
                return true;
            }
            
        });
        return false;
    }
    return{

        onLoad: function(context)
        { 
            formContext = context.getFormContext();
            if(formContext.ui.getFormType==2 &&  checkUserRole())
            {
                context.getEventArgs().preventDefault();
            }
        }
    }
})();
