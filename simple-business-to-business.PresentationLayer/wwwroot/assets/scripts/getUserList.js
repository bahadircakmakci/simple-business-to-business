$(document).ready(function () {
    var pageIndex = 1;
    loadUserResults(pageIndex, userName, controllerName, actionName);
});

function loadUserResults(pageIndex, userName, controllerName, actionName) {
    $.ajax({
        url: "/" + controllerName + "/" + actionName,
        type: "POST",
        async: true,
        dataType: "JSON",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: { userName: userName, pageIndex: pageIndex },
        success: function (result) {
            var html = "";
            if (result.lenght != 0) {
                $.each(result, function (key, item) {
                    html += '<tr><td><img src="' + item.ImagePath + '" class="img-circle" alt="User Image" /></td > <td><a href="/Profile/' + item.UserName + '">' + item.UserName + '</a></td></tr > ';
                });
                if (pageIndex == 1) {
                    $('#UsersResult').html(html);
                }
                else {
                    $('#UsersResult').append(html);
                }
            }
        },
        error: function (errormessage) {
            $('#UsersResult').html('<tr><p class="text-center">There is no results found</p></tr>');
        }
    });
}