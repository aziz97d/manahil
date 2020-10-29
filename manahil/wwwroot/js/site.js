

showInPopup = (url, title,dpsAccountId) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal #depositAccountD').val(dpsAccountId)
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#editPartial').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = url => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: url,
                
                contentType: false,
                processData: true,
                success: function (res) {
                    $('#editPartial').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

showInPopupDetails = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal .modal-dialog').addClass('modal-lg');
            $('#form-modal').modal('show');
        }
    })
}


viewApprovalProject = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

//jQueryAjaxPost = form => {
//    try {

//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new form(form),
//            contentType: false,
//            processData: false,
//            success: function (res) {
//                if (res.isValid) {
//                    $('#editPartial').html(res.html);

//                    $('#form-modal .modal-body').html('');
//                    $('#form-modal .modal-title').html('');
//                    $('#form-modal').modal('hide');
//                }
//                else {
//                    $('#form-modal .modal-body').html(res.html);
//                }
//            }
//        })

//    } catch (e) {
//        console.log(e);
//    }
//}


//(function () {
//    var placeholderelement = $('#placeholderhere');
//    $('data-toggle="ajax-modal').click(function (event) {
//        var url = $(this).data('url');
//        $.get(url).done(function (data) {
//            placeholderelement.html(data);
//            placeholderelement.find('.modal').modal('show');
//        })
//    })
//})