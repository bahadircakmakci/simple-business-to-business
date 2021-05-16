$(document).ready(function () {
    Userlist();
});

function Userlist(pageIndex) {
    if (pageIndex == null || pageIndex <= 0) {
        pageIndex = 1;
    }
    loadUserResults(pageIndex, controllerName, actionName, totalpage);
}

function loadUserResults(pageIndex, controllerName, actionName, totalpage) {
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
           
                    html += '<td>' + item.UserName + '</td>';
                    html += '<td>' + item.Email + '</td>';
                    if (item.Companies == null) {
                        html += '<td>Firma İle Eşleştirirniz...</td>';
                    }
                    else {
                        html += '<td>' + item.Companies.CompanyName + '</td>';
                    }
                    
                    if (item.Status == 1) html += '<td>Aktif</td>'; 
                    else html += '<td>Pasif</td>';
               

                    html += '<td>' + '<a href="/Home/UserAccountEdit/' + item.Id + '">Düzenle</a>' + '</td>';
                    html += '<td>' + '<a href="/Home/UserAccountDelete/' + item.Id + '">Sil</a>' + '</td>';

                    html += '</tr>';
                });

                $('#UserResult').html(html);
                var pagigng = "";
                pagigng += '<tr>';
                pagigng += '<th></th>';
                pagigng += '<th></th>';
              
                pagigng += '<th colspan="4">';
                pagigng += '<button type="button" onclick="Userlist(1)" class="btn btn-info ml-2"> İlk </button>'

                if (totalpage >= 3) {
                    if (pageIndex != 1) {

                        if (pageIndex == totalpage) {
                            for (var i = pageIndex - 2; i <= totalpage; i++) {

                                pagigng += '<button type="button" onclick="Userlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }
                        else {
                            for (var i = pageIndex - 1; i <= (pageIndex + 1); i++) {

                                pagigng += '<button type="button" onclick="Userlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                            }
                        }

                    }
                    else {
                        for (var i = pageIndex; i <= (pageIndex + 2); i++) {

                            pagigng += '<button type="button" onclick="Userlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'
                        }
                    }


                }
                else {
                    for (var i = 1; i <= totalpage; i++) {

                        pagigng += '<button type="button" onclick="Userlist(' + i + ')" class="btn btn-info ml-2"> ' + i + ' </button>'

                    }
                }

                pagigng += '<button type="button" onclick="Userlist(' + totalpage + ')" class="btn btn-info ml-2"> Son </button>'
                pagigng += '</th>';

                pagigng += '</tr>';

                $('#PageUserResult').html(pagigng);
            }
        },
        error: function (errormessage) {
            $('#UserResult').html('<tr><p class="text-center">There is no results found</p></tr>');
        }
    });
}