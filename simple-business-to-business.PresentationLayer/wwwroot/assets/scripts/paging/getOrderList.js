$(document).ready(function () {

    Orderlist();
});

function Orderlist(pageIndex) {
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
                    html += '<td>' + new Date(item.CreateDate.replace(/(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3")) + '</td>';
                    html += '<td>' + parseFloat(parseFloat(item.SubTotal) + parseFloat(item.TotalTax)).toFixed(2) + '</td>';
                    html += '<td>' + '<a href="/Order/OrderDetails/' + item.Id + '">Detay</a>' + '</td>';
                     
                    html += '</tr>';
                });

                $('#OrderResult').html(html);
                var pagigng = "";
                pagigng += '<tr>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th colspan="2">';
                pagigng += '<button type="button" onclick="Orderlist(1)" class="btn btn-info ml-2"> İlk </button>'

                if (totalpage >= 3) {
                    if (pageIndex != 1) {

                        if (pageIndex == totalpage) {
                            for (var i = pageIndex - 2; i <= totalpage; i++) {

                                pagigng += '<button type="button" onclick="Orderlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }
                        else {
                            for (var i = pageIndex - 1; i <= (pageIndex + 1); i++) {

                                pagigng += '<button type="button" onclick="Orderlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }

                    }
                    else {
                        for (var i = pageIndex; i <= (pageIndex + 2); i++) {

                            pagigng += '<button type="button" onclick="Orderlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                        }
                    }


                }
                else {
                    for (var i = 1; i <= totalpage; i++) {

                        pagigng += '<button type="button" onclick="Orderlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'

                    }
                }

                pagigng += '<button type="button" onclick="Orderlist(' + totalpage + ')" class="btn btn-info ml-2"> Son </button>'
                pagigng += '</th>';

                pagigng += '</tr>';

                $('#PageOrderResult').html(pagigng);
            }
        },
        error: function (errormessage) {
            $('#OrderResult').html('<tr><p class="text-center">There is no results found</p></tr>');
        }
    });
}