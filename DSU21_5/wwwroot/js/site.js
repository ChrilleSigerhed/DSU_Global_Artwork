jQueryAjaxUpdate = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (data) {
                    console.log("hej", $('#editBioModal'));
                    $(`#bio`).text(data);
                    $("#editBioModal .close").click()
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    return false;
}
