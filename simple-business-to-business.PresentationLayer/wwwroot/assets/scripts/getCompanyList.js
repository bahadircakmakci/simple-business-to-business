$(document).ready(function () {
    var pageIndex = 1;
    loadCompanyResults(pageIndex, controllerName, actionName);
});

function loadCompanyResults(pageIndex, controllerName, actionName) {
    $.ajax({
        url: "/" + controllerName + "/" + actionName,
        type: "POST",
        async: true,
        dataType: "JSON",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {  pageIndex: pageIndex },
        success: function (result) {
            var html = "";
            if (result.lenght != 0) {
                $.each(result, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.TaxNumber+'</td>';
                    html += '<td>' + item.CompanyName + '</td>';
                    html += '<td>' + item.AccountingCode + '</td>';
                    html += '<td>' + item.City + '</td>';
                    html += '<td>' + item.State + '</td>';

                    html += '<td>' + '<a href="/Company/CompanyEdit/'+item.Id+'">Düzenle</a>' + '</td>';
                    html += '<td>' + '<a href="/Company/CompanyDelete/' + item.Id + '">Sil</a>' + '</td>';

                    html += '</tr>';
                });
                if (pageIndex == 1) {
                    $('#CompanysResult').html(html);
                }
                else {
                    $('#CompanysResult').append(html);
                }
            }
        },
        error: function (errormessage) {
            $('#CompanysResult').html('<tr><p class="text-center">There is no results found</p></tr>');
        }
    });
}