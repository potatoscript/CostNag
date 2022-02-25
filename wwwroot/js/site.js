/*******
 * you need to switch over the following setting for localhost and company server doing deploring your project
 * 
 * 2. Client.BaseAddress in Helper.cs
 *******/
var _url = "/";          //if your app upload outside Default Web site - for my pc
//var _url = "/costnag/";  //if your app upload under Default Web site - for company
var _url2 = "/costdata/";  //if your app upload under Default Web site - for company


var tooling_no = 0;

jQuery(document).ready(function () {
    window.onresize = setWindow;
    setWindow();


        jQuery('#table_data td')
            .click(function (event) {//for multiple select on process

                var RI = jQuery(this).parent().parent().children().index(this.parentNode);
                var CI = jQuery(this).parent().children().index(this);

                var table = document.getElementById("table_data");

                //window.location.href = _url + "Home/Read?id=" + table.rows[RI].cells[9].innerText;
                if(CI!=10)
                window.open(_url2 + "Home/Read?id=" + table.rows[RI].cells[9].innerText, '_blank');

            })
 

    jQuery("#btn_searchcode").click(function () {
        window.location.href = _url + "Home/Index?" +
            "search_code=" + document.getElementById("search_code").value;
    });

});


function AddNew() {
    window.open(_url2 + "Home/", '_blank');
}

function jQueryAjaxPost(form, page) {
    try {
        jQuery.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    var p = _url + page;
                    if (page == "ProcessMaster/Index") {
                        //document.getElementById("RefreshProcessMaster").click();
                        refreshProcessMaster();
                    } 
                    if (page == "Home/Index") {
                      
                        RefreshData();
                    }
                    
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (e) {
        console.log(e);
    }
    // to prevent default form submit event
    return false;
}

function setWindow() {

    document.getElementById('div_body').style.width = (window.innerWidth - 80) + 'px';
    document.getElementById('div_body').style.height = (window.innerHeight - 105) + 'px';

}

function delete_data(id) {
    
    var confirm_delete = confirm("Delete Data?");
    if (confirm_delete) {
        jQuery.ajax({
            type: "POST",
            url: _url + 'Home/Delete',
            data: jQuery.param({
                Id: id,
                confirm: true
            }),
            success: function (res) {
                //alert("Data Deleted");
                window.location.href = _url;
            }
        });
    }

}

function RefreshData() {
    
    var url = _url + "Home";
    window.location.href = url;
}



