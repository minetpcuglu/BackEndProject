function Delete(id) {
    var tr = $(event.currentTarget.parentElement.parentElement);
    debugger;
    if (confirm("Silmek istediğinize emin misiniz ?")) {
        $.post("/Education/DeleteEducation/" + id, function (response) {
            toastr.error(response.message, "SuccessAlert", { timeOut: 5000, "closeButton": true, "progressBar": true });
            tr.remove();
        });
    }
};

