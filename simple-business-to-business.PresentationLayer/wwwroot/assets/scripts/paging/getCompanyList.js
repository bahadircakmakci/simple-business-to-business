$(document).ready(function () {

    Companylist();
});

function Companylist(pageIndex) {
    if (pageIndex == null || pageIndex <= 0) {
        pageIndex = 1;
    }
    loadCompanyResults(pageIndex, controllerName, actionName, totalpage);
}

function loadCompanyResults(pageIndex, controllerName, actionName, totalpage) {
    $.ajax({
        url: "/" + controllerName + "/" + actionName,
        type: "POST",
        async: true,
        dataType: "JSON",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: { pageIndex: pageIndex },
        success: function (result) {
            var html = "";
            if (result.lenght != 0) {
                $.each(result, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.Id + '</td>';
                    html += '<td>' + item.TaxNumber + '</td>';
                    html += '<td>' + item.CompanyName + '</td>';
                    html += '<td>' + item.AccountingCode + '</td>';
                    html += '<td>' + item.City + '</td>';
                    html += '<td>' + item.State + '</td>';

                    html += '<td>' + '<a href="/Company/CompanyEdit/' + item.Id + '">Düzenle</a>' + '</td>';
                    html += '<td>' + '<a href="/Company/CompanyDelete/' + item.Id + '">Sil</a>' + '</td>';

                    html += '</tr>';
                });

                $('#CompanysResult').html(html);
                var pagigng = "";
                pagigng += '<tr>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th colspan="4">';
                pagigng += '<button type="button" onclick="Companylist(1)" class="btn btn-info ml-2"> İlk </button>'
                 
                if (totalpage >= 3) {
                    if (pageIndex != 1) {

                        if (pageIndex == totalpage) {
                            for (var i = pageIndex - 2; i <= totalpage; i++) {

                                pagigng += '<button type="button" onclick="Companylist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }
                        else {
                            for (var i = pageIndex - 1; i <= (pageIndex + 1); i++) {

                                pagigng += '<button type="button" onclick="Companylist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }
                        
                    }
                    else {
                        for (var i = pageIndex; i <= (pageIndex + 2); i++) {

                            pagigng += '<button type="button" onclick="Companylist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                        }
                    }
                        
                         
                }
                else {
                    for (var i = 1; i <= totalpage; i++) {

                        pagigng += '<button type="button" onclick="Companylist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'

                    }
                }
                 
                pagigng += '<button type="button" onclick="Companylist(' + totalpage + ')" class="btn btn-info ml-2"> Son </button>'
                pagigng += '</th>';

                pagigng += '</tr>';

                $('#PageCompanysResult').html(pagigng);
            }
        },
        error: function (errormessage) {
            $('#CompanysResult').html('<tr><p class="text-center">There is no results found</p></tr>');
        }
    });
}