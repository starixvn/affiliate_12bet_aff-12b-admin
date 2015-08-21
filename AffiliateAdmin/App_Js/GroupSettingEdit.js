
window.onload = Initialize;

var StringBuilder = function() {
    var buffer = [];
    if (arguments.length === 1 && typeof arguments[0] === "string")
        buffer.push(arguments[0]);
    this.append = function(str) {
        buffer.push(str);
    };
    this.clear = function() {
        buffer = null;
        buffer = [];
    };
    this.insert = function(index, str) {
        if (buffer[index]) { buffer[index] = str; }
        else { return false; }
    };
    this.replace = function(find, replace) {
        var length = buffer.length;
        for (var i = 0; i < length; i++) {
            var exp = new RegExp(find, 'gm');
            buffer[i] = buffer[i].replace(exp, replace);
        }
    };
    this.remove = function() {
        if (typeof arguments[0] === "string") {
            var length = buffer.length;
            for (var i = 0; i < length; i++) {
                if (arguments[0] === buffer[i]) { buffer.splice(i, 1); }
            }
        }
        else {
            var index = arguments[0], length = (arguments.length > 1) ? arguments[1] : 1;
            buffer.splice(index, length);
        }
    };
    this.toString = function() {
        return buffer.join("");
    };
};

    
    
    function Initialize(){
        //Add Event Handler for each control
        $("chkAll").observe('click', SwitchAllSportsStatus);
        $("btnSave").observe('click', SaveSetting);
       
             
        for(var i=0;i<GroupSetting.sportSetting.length;i++){
            //SportIcon             
             $("icon_enable" + GroupSetting.sportSetting[i].id).observe('click', ChangeSportStatus);                   
             $("collapse_expand" + GroupSetting.sportSetting[i].id).observe('click', SwitchSportTab);      
             //Copy All Checkbox
             $("chkCopy" + GroupSetting.sportSetting[i].id).observe('click', CopySetting);      
             //Add Tier button
             $("btnAddTier" + GroupSetting.sportSetting[i].id).observe('click', AddTier);                            
             //Deduction Event Handler
             for(var j=0;j<GroupSetting.sportSetting[i].deductions.length;j++){
                $("deduction_" + GroupSetting.sportSetting[i].id + "_" + GroupSetting.sportSetting[i].deductions[j].id).observe('click', ChangeDeductionStatus);                   
             }
             //Deduction Event Handler for each control in each Tier
             for(var k=1;k<=GroupSetting.sportSetting[i].minAmounts.length;k++){
                 AddEventHandlerForOneTierRow(GroupSetting.sportSetting[i].id,k);        
             }
             
        }  
        //Disable check box copy when have only one sport enabled
        var enabledSportID=-1,count=0;
        for(var i=0;i<GroupSetting.sportSetting.length;i++){
              if(GroupSetting.sportSetting[i].isEnabled){
                count++;
                enabledSportID=GroupSetting.sportSetting[i].id;
              }
         }  
         if(count==1){
            $("chkCopy" + enabledSportID).className=CheckboxStatus.OffDisable; 
            GroupSetting.copySportID=-1;
        }
        //Active all sport for Default
        if (!GroupSetting.isEdited) {
            fireEvent($("chkAll"), 'click');
        }
        if ($("txtGroupName") != null && $("txtGroupName") != 'undefined') {
            $("txtGroupName").observe('blur', GroupNameChanged);
            $("txtDescription").observe('blur', DescriptionChanged);
            if ($("txtGroupName").disabled == "") {
                $("txtGroupName").focus();
            }
            else {
                $("txtDescription").focus();
            }
        } 
     }   
    
    
    function GroupNameChanged(event){
        GroupSetting.groupName=trim(this.value);
    }
    function DescriptionChanged(event){
        GroupSetting.description=trim(this.value);
    }
    function SaveSetting(event){
        if (IsValidSetting()) {
            
            if (GroupSetting.isEdited) {  
                var alertText=new StringBuilder("Are you sure you want to update ");
                if (parent.ibcWnd.titleText.indexOf('Edit Commission Group for') >= 0) {
                    alertText.append(parent.ibcWnd.titleText.substring('Edit Commission Group for '.length, parent.ibcWnd.titleText.length));
                    alertText.append("'s commission group ?");
                }
                else
                    alertText.append('the group "' + GroupSetting.groupName + '" ?');   
                if(!confirm(alertText))
                    return;
            }
            DelayReloadPage1();
            
            var ajax = new Ajax.Request("../Handler/CommGroupUpd.ashx", {
                method: 'post',
                parameters: { params: Object.toJSON(GroupSetting) },
                onComplete: function(data) {
                    var result = data.responseText;
                    if (result != "")
                        alert(result);
                    else {
                        if (!GroupSetting.isApproved) {
                            if (parent.location.href.indexOf("GroupSetting.aspx") > 1 || parent.location.href.indexOf("AffiliateList.aspx")>1) {
                            parent.location.href = parent.location.href.replace(/#/, "");                                 
                            }else
                            parent.parent.location.href = parent.parent.location.href.replace(/#/, ""); 
                        }
                    }
                    if (GroupSetting.isApproved) {                        
                        parent.parent.location.href = parent.parent.location.href.replace(/#/, ""); 
                    }
                    HideDelayReloadPage();
                }
            });                         
           }
    }
  
function fireEvent(element,event) {
	
	if (element.fireEvent) { 
		var e = document.createEventObject();
		element.fireEvent('on'+ event, e);
	}
	else { 
		var e = document.createEvent('MouseEvents');
		e.initEvent(event, true, true);
		element.dispatchEvent(e);
	}
}


  
    function AddEventHandlerForOneTierRow(sportID,tierID){
        $("txtMinAmount_" + sportID + "_" + tierID).observe('blur', MinBlur);                
        $("txtMinAmount_" + sportID + "_" + tierID).observe('keypress', IsNumericKey);   
        $("txtMinAmount_" + sportID + "_" + tierID).observe('change', FixNumberAmount);   
        $("txtMinAmount_" + sportID + "_" + tierID).observe('keyup', FixNumberAmount);                     
           
        $("txtMaxAmount_" + sportID + "_" + tierID).observe('blur', MaxAmountBlur);   
        $("txtMaxAmount_" + sportID + "_" + tierID).observe('keypress', IsNumericKey);   
        $("txtMaxAmount_" + sportID + "_" + tierID).observe('change', FixNumberAmount);   
        $("txtMaxAmount_" + sportID + "_" + tierID).observe('keyup', FixNumberAmount);   
        
        $("txtMinQM_" + sportID + "_" + tierID).observe('blur', MinBlur);                
        $("txtMinQM_" + sportID + "_" + tierID).observe('keypress', IsNumericKey);   
        $("txtMinQM_" + sportID + "_" + tierID).observe('change', FixNumberQM);   
        $("txtMinQM_" + sportID + "_" + tierID).observe('keyup', FixNumberQM);   
        
        $("txtMaxQM_" + sportID + "_" + tierID).observe('blur', MaxQMBlur);   
        $("txtMaxQM_" + sportID + "_" + tierID).observe('keypress', IsNumericKey);   
        $("txtMaxQM_" + sportID + "_" + tierID).observe('change', FixNumberQM);   
        $("txtMaxQM_" + sportID + "_" + tierID).observe('keyup', FixNumberQM);   
        
        $("slCommission_" + sportID + "_" + tierID).observe('change', CommissionChange);
        
         $("chkMaxAmount_" + sportID + "_" + tierID).observe('click', SetLastTier);
         $("chkMaxQM_" + sportID + "_" + tierID).observe('click', SetLastTier);                
         $("btnDelete_" + sportID + "_" + tierID).observe('click', DeleteTier);
    }
    function DeleteTier(event){
         var arr=this.id.split("_");  
         if(this.className==IconDelete.Enabled){
                       
            //Enable previous Delete Tier if Tier>1
            if(arr[2]>1){
                $("btnDelete_" + arr[1] + "_" + (arr[2]-1)).className=IconDelete.Enabled;
                $("chkMaxAmount_" + arr[1] + "_" + (arr[2]-1)).className=CheckboxStatus.OffEnable;
                $("chkMaxQM_" + arr[1] + "_" + (arr[2]-1)).className=CheckboxStatus.OffEnable;
            }
            //Enable button Add Tier
            $("btnAddTier" + arr[1]).className=IconAddTier.Enabled;
            
             //delete this Tier
             var sportSetting=GetSportSetting(arr[1]);
             sportSetting.minAmounts.splice(arr[2]-1,1);
             sportSetting.minAmounts.splice(arr[2]-1,1);
             sportSetting.maxAmounts.splice(arr[2]-1,1);
             sportSetting.minQMs.splice(arr[2]-1,1);
             sportSetting.maxQMs.splice(arr[2]-1,1);
             sportSetting.commissions.splice(arr[2]-1,1);             
             
            var table=$("tblTier" + arr[1]);
            table.deleteRow(arr[2]);  
            var rowCount= table.rows.length;  
            if(arr[2]==1)
            {                
                var sXmlBody = "<table><tbody id='tierList" + arr[1] + "'>" + noInfo + "</tbody></table>";
                var tempDiv = document.createElement("DIV");
                tempDiv.innerHTML = sXmlBody;
               $("tblTier" + arr[1]).removeChild($("tierList" +  arr[1]));
               $("tblTier" + arr[1]).appendChild(tempDiv.firstChild.firstChild);
            }
         }
          
    }
    function SetLastTier(event){      
       var arr=this.id.split("_");
       var sportSetting = GetSportSetting(arr[1]);            
       
       if(this.className==CheckboxStatus.OnEnable){ //This Is Last Tier so uncheck it
            var maxAmount=sportSetting.minAmounts[arr[2]-1];
            var maxQM=sportSetting.minQMs[arr[2]-1];
            $("txtMaxAmount_" + arr[1] + "_" + arr[2]).disabled="";
            $("txtMaxAmount_" + arr[1] + "_" + arr[2]).value=FormatNumber(maxAmount);
            $("txtMaxQM_" + arr[1] + "_" + arr[2]).disabled="";
            $("txtMaxQM_" + arr[1] + "_" + arr[2]).value=FormatNumber(maxQM);
            $("chkMaxAmount_" + arr[1] + "_" + arr[2]).className=CheckboxStatus.OffEnable;
            $("chkMaxQM_" + arr[1] + "_" + arr[2]).className=CheckboxStatus.OffEnable;
             if(maxAmount>=maximumAmountNumber-1 || maxQM>=maximumQMNumber-1)
             {    
                     $("btnAddTier" + arr[1]).className=IconAddTier.Disabled;                      
             }
             else{
                    $("btnAddTier" + arr[1]).className=IconAddTier.Enabled;                       
              }
            
            sportSetting.maxAmounts[arr[2]-1]=maxAmount;
            sportSetting.maxQMs[arr[2]-1]=maxQM;
            
       }
       else if(this.className==CheckboxStatus.OffEnable) { //Check it as Lastier
            $("txtMaxAmount_" + arr[1] + "_" + arr[2]).disabled = "disabled";
            $("txtMaxQM_" + arr[1] + "_" + arr[2]).disabled = "disabled";
            $("txtMaxAmount_" + arr[1] + "_" + arr[2]).value="∞";            
            $("txtMaxQM_" + arr[1] + "_" + arr[2]).value="∞";
            $("chkMaxAmount_" + arr[1] + "_" + arr[2]).className=CheckboxStatus.OnEnable;
            $("chkMaxQM_" + arr[1] + "_" + arr[2]).className=CheckboxStatus.OnEnable;
            $("btnAddTier" + arr[1]).className=IconAddTier.Disabled;
           
            sportSetting.maxAmounts[arr[2]-1]=maximumAmountNumber;
            sportSetting.maxQMs[arr[2]-1]=maximumQMNumber;
       }
       
    
    }
   

    function AddTier(event){
        if(this.className==IconAddTier.Enabled){
            var sportID=GetSportIDFromControlID(this,"btnAddTier");
            var sportSetting=GetSportSetting(sportID);  
            var noTier=sportSetting.commissions.length;
            var minAmount,maxAmount,minQM,maxQM,commission,newTierID;
            var sXmlBody=new StringBuilder("<table><tbody>")
            sXmlBody.append(TierRowTpl);
            sXmlBody.append("</tbody></table>");
            sXmlBody.replace("{SportID_val}",sportID);
            sXmlBody.replace("{disabled_txtMaxAmount_val}","");
            sXmlBody.replace("{disabled_txtMaxQM_val}","");
            if(noTier==0){
                minAmount=maxAmount=minQM=maxQM=0;
                commission="0.00";
                newTierID=1;
                sXmlBody.replace("{disabled_txtMinAmount_val}","");                
                sXmlBody.replace("{disabled_txtMinQM_val}","");    
                 $("tblTier" + sportID).deleteRow(1);              
            }
            else{
                minAmount=sportSetting.maxAmounts[noTier-1] + 1;                
                maxAmount=minAmount;
                minQM=sportSetting.maxQMs[noTier-1] + 1;                
                maxQM=minQM;
                if(minAmount>=maximumAmountNumber-1 || minQM>=maximumQMNumber-1)
                {    
                     $("btnAddTier" + sportID).className=IconAddTier.Disabled;                      
                }
                commission=sportSetting.commissions[noTier-1];
                newTierID=noTier+1;
                sXmlBody.replace("{disabled_txtMinAmount_val}","disabled");                
                sXmlBody.replace("{disabled_txtMinQM_val}","disabled");
                //Disable Delete button for previous Tier            
                $("btnDelete_" + sportID + "_" + noTier).className=IconDelete.Disabled;
                $("chkMaxAmount_" + sportID + "_" + noTier).className=CheckboxStatus.OffDisable;
                $("chkMaxQM_" +sportID + "_" + noTier).className=CheckboxStatus.OffDisable;
            }
            
            sXmlBody.replace("{tierID_val}",newTierID);
            sXmlBody.replace("{txtMinAmount_val}",FormatNumber(minAmount));
            sXmlBody.replace("{txtMaxAmount_val}",FormatNumber(maxAmount));
            sXmlBody.replace("{txtMinQM_val}",FormatNumber(minQM));
            sXmlBody.replace("{txtMaxQM_val}",FormatNumber(maxQM));
            sXmlBody.replace("{Commission_val}",commission);
            sXmlBody.replace("{chkMaxClass_val}", CheckboxStatus.OffEnable);
            
            sXmlBody.replace("{icon_delete_status_val}",IconDelete.Enabled);           
            
            var tempDiv = document.createElement("DIV");                
            tempDiv.innerHTML = sXmlBody.toString();
            $("tblTier" + sportID).appendChild(tempDiv.firstChild.firstChild);
            delete tempDiv;
            delete sXmlBody;
           
            SetDropDownList($("slCommission_" + sportID + "_" + newTierID),commission); 
            //Cai Event Handler cho cac control
            AddEventHandlerForOneTierRow(sportID,newTierID);
            //Update data
            sportSetting.minAmounts.push(minAmount);
            sportSetting.maxAmounts.push(maxAmount);
            sportSetting.minQMs.push(minQM);
            sportSetting.maxQMs.push(maxQM);
            sportSetting.commissions.push(commission);    
            
        }
    }
    
    function IsValidSportSetting(SportID){
           /*sport duoc enabled phai co it nhat 1 tier, neu copy duoc check thi chi check copysport, tier cuoi cung co cac gia tri max= ∞ */ 
           if(GroupSetting.copySportID!=-1){
               SportID=GroupSetting.copySportID;
           }
           var sportSetting=GetSportSetting(SportID);           
           if(sportSetting.commissions.length>0) {

               if (sportSetting.commissions.length > 50) {
                   alert('Invalid setting for ' + sportSetting.sportName + '. Maximum of 50 tiers are allowed.');
                   return false;     
               }
             //Check the last tier for maxAmount and maxQM
             if(sportSetting.maxAmounts[sportSetting.commissions.length-1] < maximumAmountNumber ||  sportSetting.maxQMs[sportSetting.commissions.length.length-1]<maximumQMNumber){
                alert('Invalid setting for ' +sportSetting.sportName + '. Last Tier Setting must have maximum value=∞.');
             return false;     
             }
           }
           else{
             alert('Invalid setting for ' +sportSetting.sportName + '. Tier Setting must have at least 1 tier.');
             return false;
           }
           
           return true;
    }
    function IsValidSetting(){         
        if(trim(GroupSetting.groupName)=="")
        {
            alert("Group Name cannot be empty. Please check!");
             $("txtGroupName").focus();
            return false;
        }       
        var count=0;        
        for(var i=0;i<GroupSetting.sportSetting.length;i++){
            if(GroupSetting.sportSetting[i].isEnabled)
            {
                count++;
                if(!IsValidSportSetting(GroupSetting.sportSetting[i].id))
                    return false;
            }
        }
        if(count==0){
            alert('Oops, you must enable at least 1 sport.');
            return false;
        }
        return true;
    }
    
    function SetDropDownList(elementRef, valueToSetTo)
    {
        var isFound = false;

         for (var i=0; i<elementRef.options.length; i++)
         {
          if ( elementRef.options[i].value == valueToSetTo )
          {
           elementRef.options[i].selected = true;
           isFound = true;
            return; 
          }
         }         
         if (isFound == false )
          elementRef.options[0].selected = true;
    }

    function CommissionChange(event){        
        //Gia tri thay doi phai luon nho hon bang gia tri cua tier truoc do        
        
        var previousCommissionVal=0;
        var arr=this.id.split("_");
        var sportSetting=GetSportSetting(arr[1]);        
        var index=arr[2]-1;
        var comm = this.options[this.selectedIndex].value;
        /*alert(this.options.length);*/
        if(index>0)
        {          
            previousCommissionVal=sportSetting.commissions[index-1];                       
        }       
        if(comm< previousCommissionVal)
        {
            SetDropDownList(this,previousCommissionVal);            
            comm=previousCommissionVal;
        }
        
        //Neu gia tri thay doi lon hon gia tri cua cac tier sau thi cac tier sau se mang gia tri nay
       
        for(var k=index +1 ; k<sportSetting.commissions.length;k++){                     
           if(sportSetting.commissions[k]<comm){
           
                SetDropDownList($("slCommission_" +  arr[1] + "_" + (k+1) ),comm);
                sportSetting.commissions[k]=comm;
           }
           
        }
        sportSetting.commissions[index]=comm;
       
    }
    function ChangeDeductionStatus(event){
        var arr=this.id.split("_");
        var isEnabled=false;
        if(this.className==CheckboxStatus.OffEnable){
            this.className=CheckboxStatus.OnEnable;
            isEnabled=true;
        }
        else
        {
            this.className=CheckboxStatus.OffEnable;
        }
        var sportSetting=GetSportSetting(arr[1]);
        for(var j=0;j<sportSetting.deductions.length;j++){
            if(sportSetting.deductions[j].id==arr[2]){
                sportSetting.deductions[j].isCheck=isEnabled;                
                return;
            }
        }
        
    }
    function CopySetting(event){
        //Control phai duoc enable
        if(this.className==CheckboxStatus.OnEnable) //Khong copy setting, tra lai trang thai cho cac sport enable
        {
            this.className=CheckboxStatus.OffEnable;
            var sportID=GroupSetting.copySportID;
            GroupSetting.copySportID=-1;
            //cai lai event handler cho cac tab khac, icon khac ma co sport enable
            //Thay doi mau cac sport
            for(i=0;i<GroupSetting.sportSetting.length;i++){
              if(sportID!=GroupSetting.sportSetting[i].id && GroupSetting.sportSetting[i].isEnabled){
                $("icon_enable" + GroupSetting.sportSetting[i].id).observe('click', ChangeSportStatus);                   
                $("collapse_expand" + GroupSetting.sportSetting[i].id).observe('click', SwitchSportTab);
                $("chkCopy" + GroupSetting.sportSetting[i].id).className=CheckboxStatus.OffEnable;
                 $("sport_color" + GroupSetting.sportSetting[i].id).className=SportStatusColor.Enabled;
              }
            }
            
        }
        else if(this.className==CheckboxStatus.OffEnable) //Copy Setting, remove copy cu neu co, va cac sport enable o tinh trang khong edit duoc
        {
            this.className=CheckboxStatus.OnEnable;
            GroupSetting.copySportID=GetSportIDFromControlID(this,"chkCopy");      
            //Expand Current Sport Tab
            $("tab_content" + GroupSetting.copySportID).style.display=SportTabSwitch.Expand;
            $("collapse_expand" + GroupSetting.copySportID).className=SportTabSwitchIcon.Expand; 
            for(i=0;i<GroupSetting.sportSetting.length;i++){
              if(GroupSetting.copySportID!=GroupSetting.sportSetting[i].id && GroupSetting.sportSetting[i].isEnabled){
                $("icon_enable" + GroupSetting.sportSetting[i].id).observe('click', DoNothing);                   
                $("collapse_expand" + GroupSetting.sportSetting[i].id).observe('click', DoNothing);
                $("chkCopy" + GroupSetting.sportSetting[i].id).className=CheckboxStatus.OffDisable;
                 $("sport_color" + GroupSetting.sportSetting[i].id).className=SportStatusColor.Disabled;
                 $("tab_content" + GroupSetting.sportSetting[i].id).style.display=SportTabSwitch.Collapse;
                  $("collapse_expand" + GroupSetting.sportSetting[i].id).className=SportTabSwitchIcon.Collapse; 
              }
            }      
         }         
    }
    function DoNothing(event){
     Event.stop(event);
    }
    function GetSportIDFromControlID(control,name){
        return control.id.substring(name.length,control.id.length);
    }
    function ChangeSportStatus(event){        
        var sportID=GetSportIDFromControlID(this,"icon_enable");
        Event.stop(event);        
        var sportSetting=GetSportSetting(sportID);
        if(sportSetting!=null){
            if(sportSetting.isEnabled){ //Disable This Sport
                    if(confirm("Are you sure you want to remove " + sportSetting.sportName + " from the group?")){
                              var tabContent=$("tab_content" + sportID);
                              tabContent.style.display=SportTabSwitch.Collapse;   
                              $("collapse_expand" + sportID).className=SportTabSwitchIcon.Collapse;
                               $("sport_color" + sportID).className=SportStatusColor.Disabled;
                               this.className=IconSportStatus.Disabled;                       
                              sportSetting.isEnabled=false;                    
                               this.title=EnableSportExplainTpl.replace(/{SportName_val}/, sportSetting.sportName);
                               $("chkCopy" + sportID).className=CheckboxStatus.OffDisable;
                                $("chkAll").className=CheckboxStatus.OffEnable;
                                //This Sport Is for Copy Setting --> Reenable other Copy Checkbox and Tab
                              if(GroupSetting.copySportID==sportID){
                                 for(var i=0;i<GroupSetting.sportSetting.length;i++){
                                      if(sportID!=GroupSetting.sportSetting[i].id && GroupSetting.sportSetting[i].isEnabled){
                                        $("icon_enable" + GroupSetting.sportSetting[i].id).observe('click', ChangeSportStatus);                   
                                        $("collapse_expand" + GroupSetting.sportSetting[i].id).observe('click', SwitchSportTab);
                                        $("chkCopy" + GroupSetting.sportSetting[i].id).className=CheckboxStatus.OffEnable;
                                         $("sport_color" + GroupSetting.sportSetting[i].id).className=SportStatusColor.Enabled;
                                      }
                                  }
                                  GroupSetting.copySportID=-1;
                                }
                                //Disable check box copy when have only one sport enabled
                                var enabledSportID=-1,count=0;
                                for(var i=0;i<GroupSetting.sportSetting.length;i++){
                                      if(GroupSetting.sportSetting[i].isEnabled){
                                        count++;
                                        enabledSportID=GroupSetting.sportSetting[i].id;
                                      }
                                 }  
                                 if(count==1){
                                    $("chkCopy" + enabledSportID).className=CheckboxStatus.OffDisable; 
                                    GroupSetting.copySportID=-1;
                                 }  
                       }
                     
                    
                }
                else //Enable this sport
                {
                    sportSetting.isEnabled=true;                    
                    this.className=IconSportStatus.Enabled;   
                    this.title= DisableSportExplainTpl.replace(/{SportName_val}/, sportSetting.sportName);    
                    if(GroupSetting.copySportID==-1){                                   
                        
                        $("sport_color" + sportID).className=SportStatusColor.Enabled;
                        var tabContent=$("tab_content" + sportID);
                         tabContent.style.display=SportTabSwitch.Expand;   
                         $("collapse_expand" + sportID).className=SportTabSwitchIcon.Expand; 
                         //Enable other checkbox copy when this sport is enabled                   
                         var count=0;
                         for(var i=0;i<GroupSetting.sportSetting.length;i++){
                              if(GroupSetting.sportSetting[i].isEnabled){
                                count++;                                        
                              }
                           }  
                           if(count>1){
                                for(var i=0;i<GroupSetting.sportSetting.length;i++){
                                      if(GroupSetting.sportSetting[i].isEnabled){
                                            $("chkCopy" + GroupSetting.sportSetting[i].id).className=CheckboxStatus.OffEnable;                                     
                                       }
                                 }  
                           }
                           else{
                                $("chkCopy" + sportID).className=CheckboxStatus.OffDisable; 
                           }
                     }   
                     
                    if(IsAllSportEnabled()){
                         $("chkAll").className=CheckboxStatus.OnEnable;
                    }
                }
         }
      
        
    }
    function SwitchSportTab(event){          
        var sportID=GetSportIDFromControlID(this,"collapse_expand");
        var sportSetting=GetSportSetting(sportID);
        if((GroupSetting.copySportID ==sportID || GroupSetting.copySportID==-1) && sportSetting!=null && sportSetting.isEnabled){
                    var tabContent=$("tab_content" + sportID);
                    if(tabContent.style.display==SportTabSwitch.Collapse){
                       tabContent.style.display=SportTabSwitch.Expand;
                       this.className=SportTabSwitchIcon.Expand;
                    }
                    else
                    {
                        this.className=SportTabSwitchIcon.Collapse;
                        tabContent.style.display=SportTabSwitch.Collapse;
                    }
        }        
    }
    function GetSportSetting(sportID){
         for(i=0;i<GroupSetting.sportSetting.length;i++){
             if(GroupSetting.sportSetting[i].id==sportID){            
                return GroupSetting.sportSetting[i];
             }
         }
         return null;
    }
    function IsAllSportEnabled(){
         var count=0;
         for(var i=0;i<GroupSetting.sportSetting.length;i++){
                if(GroupSetting.sportSetting[i].isEnabled){
                    count++;
                }
            }
            return (GroupSetting.sportSetting.length==count);
    }
    function SwitchAllSportsStatus(event){
        //Disable all sports
        if(this.className==CheckboxStatus.OnEnable){ 
            var i;
            for(i=0;i<GroupSetting.sportSetting.length;i++){
                if(GroupSetting.sportSetting[i].isEnabled){                    
                    fireEvent($("icon_enable" + GroupSetting.sportSetting[i].id),"click");
                }
            }
            var count=0;
            for(i=0;i<GroupSetting.sportSetting.length;i++){
                if(!GroupSetting.sportSetting[i].isEnabled){
                    count++;
                }
            }
            if(count==GroupSetting.sportSetting.length){
            this.className=CheckboxStatus.OffEnable;
            
            }
        }
        else{ //Enable all sports
            var i;
            for(i=0;i<GroupSetting.sportSetting.length;i++){
                if(!GroupSetting.sportSetting[i].isEnabled){
                    fireEvent($("icon_enable" + GroupSetting.sportSetting[i].id),"click");
                }
            }
             var count=0;
            for(i=0;i<GroupSetting.sportSetting.length;i++){
                if(!GroupSetting.sportSetting[i].isEnabled){
                    count++;
                }
            }
            if(count==GroupSetting.sportSetting.length){
            this.className=CheckboxStatus.OnEnable;
            
            }
        }
    }
function FixNumberAmount(event){
    FixNumber(this,maximumAmountNumber);    
}
function FixNumberQM(event){
    FixNumber(this,maximumQMNumber);    
}
function ReverseString(text){
var splitext = text.split("");
var revertext = splitext.reverse();
var reversed = revertext.join("");
return reversed;
}

function FixNumber(obj,max){         
     var result='';    
    for(var i=0;i<obj.value.length;i++){
        if(!isNaN(obj.value.charAt(i)))
            result += obj.value.charAt(i);
    }
     obj.value = eval(result);
    var arr=obj.id.split("_");    
    var sportSetting=GetSportSetting(arr[1]); 
   var m=max-1 - (sportSetting.commissions.length-arr[2]);
    if(eval(obj.value) > m)
            obj.value= m;   
    obj.value=FormatNumber(obj.value);
                
}
function FormatNumber(value){
    //Them dau ,
    if(value=='undefined'){
        return "";
    }
    var result='';
    result += value;
    var t='';
    if(result.length>0){
        var count=0;
        for(var k=result.length-1;k>=0;k--){
            t+= result.charAt(k);
            count ++;
            if(count != result.length && count%3 ==0)
              t+=',';
        }        
    }         
   return ReverseString(t);  
}
function IsNumericKey(e)
{
    e =(e) ?e :window.event;
	var key =(e.keyCode) ?e.keyCode :e.which;
    if(key ==8 || key ==9 || key == 13 || key==17 || key==18 || (key>=37 && key<=40) || key==46 || (key >= 48 && key <= 57)  ) 
        bol = true;
    else 
        bol = false;
    
    return bol;
}  

function trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}
function ltrim(stringToTrim) {
	return stringToTrim.replace(/^\s+/,"");
}
function rtrim(stringToTrim) {
	return stringToTrim.replace(/\s+$/,"");
}


function MinBlur(event)
{
    //Kiem tra gia tri phai co neu khong gan no bang 0 hoac day la tier dau tien thi gia tri luon bang 0  
    var arr=this.id.split("_");    
    if(trim(this.value)==""|| arr[2]==1)
    {
        this.value=0;
    }
    var sportSetting=GetSportSetting(arr[1]); 
    if(this.id.indexOf("txtMinAmount")>-1){
        sportSetting.minAmounts[arr[2]-1]=ToFloat(this.value);
    }
    else{
        sportSetting.minQMs[arr[2]-1]=ToInt32(this.value);
    }    
}
function MaxAmountBlur(event)
{
  //Gia tri MaxAmount phai luon lon hon hoac bang gia tri Min Amount va nho hon gia tri maximum
  var arr=this.id.split("_");    
   var sportSetting=GetSportSetting(arr[1]);  
   if(trim(this.value)==""){
        this.value=FormatNumber(sportSetting.maxAmounts[arr[2]-1]);
        return;
   }
  
  if(ToFloat(this.value)== sportSetting.maxAmounts[arr[2]-1])
      return;
  var minAmount=sportSetting.minAmounts[arr[2]-1]; 
  if(ToFloat(this.value)<minAmount || ToFloat(this.value)>=maximumAmountNumber){
     this.value=FormatNumber(minAmount);
  }     
  sportSetting.maxAmounts[arr[2]-1]=ToFloat(this.value);
  //Tang gia tri cua min Amount sau len 1 neu co
  var previousMaxAmount=sportSetting.maxAmounts[arr[2]-1];
  
  for(var k=parseInt(arr[2]);k<sportSetting.maxAmounts.length;k++){    
    if(sportSetting.minAmounts[k]!= (previousMaxAmount +1)){
        sportSetting.minAmounts[k]=++previousMaxAmount;        
        var tierID=k+1;           
        //alert(previousMaxAmount);
        $("txtMinAmount_" + arr[1] + "_" + tierID).value=FormatNumber(sportSetting.minAmounts[k]);
        if(sportSetting.maxAmounts[k]!= maximumAmountNumber && sportSetting.maxAmounts[k] <  sportSetting.minAmounts[k] ){
            sportSetting.maxAmounts[k]=previousMaxAmount; 
            $("txtMaxAmount_" + arr[1] + "_" +  tierID).value=FormatNumber(sportSetting.maxAmounts[k]);
        }  
        else
        {
            return;
        }     
        previousMaxAmount= sportSetting.maxAmounts[k];        
    }
  }
   if(sportSetting.minAmounts[sportSetting.maxAmounts.length-1]>=maximumAmountNumber-1)
  {
      $("btnAddTier" + arr[1]).className=IconAddTier.Disabled;   
  }
}

function MaxQMBlur(event)
{
  //Gia tri MaxQM phai luon lon hon hoac bang gia tri Min QM va nho hon gia tri maximum
  var arr=this.id.split("_");    
   var sportSetting=GetSportSetting(arr[1]);  
   if(trim(this.value)==""){
        this.value=FormatNumber(sportSetting.maxQMs[arr[2]-1]);
        return;
   }
  
  if(ToInt32(this.value)== sportSetting.maxQMs[arr[2]-1])
      return;
  var minQM=sportSetting.minQMs[arr[2]-1]; 
  if(ToInt32(this.value)<minQM || ToInt32(this.value)>=maximumQMNumber){
     this.value=FormatNumber(minQM);
  }     
  sportSetting.maxQMs[arr[2]-1]=ToInt32(this.value);
  //Tang gia tri cua min Amount sau len 1 neu co
  var previousMaxQM=sportSetting.maxQMs[arr[2]-1];
  
  for(var k=parseInt(arr[2]);k<sportSetting.maxQMs.length;k++){    
    if(sportSetting.minQMs[k]!= (previousMaxQM +1)){
        sportSetting.minQMs[k]=++previousMaxQM;        
        var tierID=k+1;           
        //alert(previousMaxAmount);
        $("txtMinQM_" + arr[1] + "_" + tierID).value=FormatNumber(sportSetting.minQMs[k]);
        if(sportSetting.maxQMs[k]!= maximumQMNumber && sportSetting.maxQMs[k] <  sportSetting.minQMs[k] ){
            sportSetting.maxQMs[k]=previousMaxQM; 
            $("txtMaxQM_" + arr[1] + "_" +  tierID).value=FormatNumber(sportSetting.maxQMs[k]);
        }  
        else
        {
            return;
        }     
        previousMaxQM= sportSetting.maxQMs[k];        
    }
  }
   if(sportSetting.minQMs[sportSetting.maxQMs.length-1]>=maximumQMNumber-1)
  {
      $("btnAddTier" + arr[1]).className=IconAddTier.Disabled;   
  }
}
function ToInt32(value){
     return parseInt(ReplaceAll(value,',',''));   
}
function ToFloat(value){
     return parseFloat(ReplaceAll(value,',',''));   
}

function ReplaceAll(str,findStr,replacingStr){         
    return str.replace(eval('/' +findStr +'/g'), replacingStr);
}