function Delete(id) {
    if (confirm("Silmek istediğinize emin misiniz ?")) {
        $.ajax({
            type: "POST",
            url: "/Hobby/DeleteHobby/" + id,
            contentType: "application/json; charset=utf-8",

            dataType: "json",
            success: function (func) {

                alert("Yazar Silme işlemi başarılı bir şekilde gerçekleştirildi");
            }

        });
    }
};