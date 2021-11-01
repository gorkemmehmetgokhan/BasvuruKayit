$(document).ready(function () {

    $("#btnReport").click(function () {
        ReportManager.GenerateReport();
    });
});


var ReportManager = {
    GenerateReport: function () {
        var jsonParam = "";
        var serviceUrl = "../Profil/GetStudentReport";
        ReportManager.GetReport(serviceUrl, jsonParam, onFailed);

        function onFailed(error) {
            alert(error);
        }

    },
    GetReport: function (serviceUrl, jsonParams, errorCallback) {
        jQuery.ajax({
            url: serviceUrl,
            async: false,
            type: "POST",
            data: "{" + jsonParams + "}",
            contentType: "application/json;charset=utf-8",
            success: function () {
                window.open('../Reports/TranskriptBelge.aspx', '_newtab');
            },
            error: errorCallback
        });


    }

};

var ReportHelper = {

};