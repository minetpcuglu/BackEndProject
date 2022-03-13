function Delete(id) {
    var tr = $(event.currentTarget.parentElement.parentElement);
    debugger;
    if (confirm("Silmek istediğinize emin misiniz ?")) {
        $.post("/Hobby/DeleteHobby/" + id, function (response) {
            toastr.success(response.message, "SuccessAlert", { timeOut: 5000, "closeButton": true, "progressBar": true });
            tr.remove();
        });
    }
};