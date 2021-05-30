$(document).ready(function () {

    list();
});

function list(pageIndex) {
    if (pageIndex == null || pageIndex <= 0) {
        pageIndex = 1;
    }
    loadResults(pageIndex, controllerName, actionName, totalpage, basketcount);
}

function quantityUpdate(id) {
    var deger = $("#basketquantity_"+id).val();
    $.ajax({
        url: "/Basket/BasketQuantityUpdate",
        type: "POST",
        async: true,
        dataType: "JSON",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: { Id: id, Count: deger },
        success: function (result) {
            list();
        },
        error: {

        }



    });

    
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
                var totalprice = 0.0;
                $.each(result, function (key, item) {

                    var price = (parseFloat(item.Products.ListPrice) * parseFloat(item.BasketQuantity)).toFixed(2);
                    var vat = parseFloat((parseFloat(item.Products.Vat) / 100) + 1).toFixed(2);
                    var pricevat = parseFloat(price * vat).toFixed(2);
                    totalprice += pricevat;
                    html += '<tr>';
                    html += '<td><img src="' + item.Products.ProductPictures.ImagePath + '" class="img-circle img-md" alt="Product Image" /></td >';
                    html += '<td>' + item.Products.ProductCode + '</td>';
                    html += '<td>' + item.Products.ProductName + '</td>';
                    html += '<td>' + item.Products.Brands.BrandName + '</td>';
                    html += '<td><input onchange="quantityUpdate(' + item.Products.Id + ')" type="number" id="basketquantity_' + item.Products.Id + '" name="basketquantity_' + item.Products.Id + '" value="' + item.BasketQuantity + '" /></td>';
                    html += '<td>' + price + '</td>';
                    html += '<td>' + pricevat + '</td>';
                    html += '<td>' + '<a href="/Basket/BasketDelete/' + item.Id + '">Sil</a>' + '</td>';

                    html += '</tr>';
                });
                html += '<tr>';
                html += '<th></th>';
                html += '<th></th>';
                html += '<th></th>';
                html += '<th>Toplam Fiyat</th>';

                html += '<th colspan="4">' + parseFloat(totalprice).toFixed(2) + '</th>';

                html += '</tr>';
                $('#ProductResult').html(html);
                var pagigng = "";
                pagigng += '<tr>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
                pagigng += '<th colspan="4">';
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
                if (basketcount != null || basketcount > 0) {
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