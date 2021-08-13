function changeIsAdmin(id, obj) {

    var isAdmin = $(obj).val();

    $.ajax({
        type: 'POST',
        url: "/Admin/ChangeIsAdmin/" + id,
        data: JSON.stringify({
            IsAdmin: isAdmin
        }),
        contentType: "application/json"
    }).done(function (response) {
        console.log(response);
    });
}