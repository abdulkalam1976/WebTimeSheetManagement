$(function () {
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        debugger;
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});


function bindForm(dialog) {
    $('form', dialog).submit(function (e) {
        e.preventDefault();
        //var formdata = new FormData($('#formmd').get(0));
        var formdata;
        if ($("#formmd").length == 0) {
            formdata = new FormData($('form').get(0));
        }
        else {
            formdata = new FormData($('#formmd').get(0));
        }

        var fileInput = document.getElementById('UploadFileName');
        if (fileInput != null)
            {
            //Iterating through each files selected in fileInput
            for (i = 0; i < fileInput.files.length; i++) {
                //Appending each file to FormData object
                formdata.append(fileInput.files[i].name, fileInput.files[i]);
            }
        }
        if ($('form').valid()) {
            $.ajax({
                url: this.action,
                type: this.method,
                contentType: false,
                processData: false,
                data: formdata,
                success: function (result) {
                    if (result.success) {
                        debugger
                        $('#myModal').modal('hide');
                        $("#mriskval").val($("#riskval").val());

                    } else {
                        $('#myModalContent').html(result);
                        bindForm();
                    }
                }
            });
        }
        return false;
    });
}