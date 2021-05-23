$(document).ready(function () {

    list();
});

function list(pageIndex) {
    if (pageIndex == null || pageIndex <= 0) {
        pageIndex = 1;
    }
    loadResults(pageIndex, controllerName, actionName, totalpage, basketcount);
}

function loadResults(pageIndex, controllerName, actionName, totalpage, basketcount) {
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
                    html += '<td><img src="' + item.ProductPictures.ImagePath + '" class="img-circle img-md" alt="Product Image" /></td >';
                    html += '<td>' + item.ProductCode + '</td>';
                    html += '<td>' + item.ProductName + '</td>';
                    html += '<td>' + item.Brands.BrandName + '</td>';
                    html += '<td>' + item.ListPrice + '</td>';
                    html += '<td>' + item.Quantity + '</td>';
                    html += '<td>' + '<form action = "BasketAdd" method = "post">     <a href="/Product/BasketAdd/' + item.Id + '">Sepete Ekle</a>' + '</td>';
                    html += '<td>' + '<a href="/Product/ProductEdit/' + item.Id + '">Düzenle</a>' + '</td>';
                    html += '<td>' + '<a href="/Product/ProductDelete/' + item.Id + '">Sil</a>' + '</td>';

                    html += '</tr>';
                });

                $('#ProductResult').html(html);
                var pagigng = "";
                pagigng += '<tr>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th colspan="5">';
                pagigng += '<button type="button" onclick="list(1)" class="btn btn-info ml-2"> İlk </button>'

                if (totalpage >= 3) {
                    if (pageIndex != 1) {

                        if (pageIndex == totalpage) {
                            for (var i = pageIndex - 2; i <= totalpage; i++) {

                                pagigng += '<button type="button" onclick="list(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }
                        else {
                            for (var i = pageIndex - 1; i <= (pageIndex + 1); i++) {

                                pagigng += '<button type="button" onclick="list(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }

                    }
                    else {
                        for (var i = pageIndex; i <= (pageIndex + 2); i++) {

                            pagigng += '<button type="button" onclick="list(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                        }
                    }


                }
                else {
                    for (var i = 1; i <= totalpage; i++) {

                        pagigng += '<button type="button" onclick="list(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'

                    }
                }

                pagigng += '<button type="button" onclick="list(' + totalpage + ')" class="btn btn-info ml-2"> Son </button>'
                pagigng += '</th>';

                pagigng += '</tr>';

                $('#PageProductResult').html(pagigng);
                if (basketcount != null || basketcount>0) {
                    $('#basketCount').html(basketcount);
                }
                else {
                    $('#basketCount').html("0");
                }
            }


        },
        error: function (errormessage) {
            $('#ProdutResult').html('<tr><p class="text-center">There is no results found</p></tr>');
        }
    });
}