$(function () {
    var resetMessage = function () {
        $('#s-message').closest('.alert').hide();
        $('#e-message').closest('.alert').hide();
    }
    var showMessage = function (message, type) {
        resetMessage();
        if (type === 'success') {
            $('#s-message').text(message);
            $('#s-message').closest('.alert').show();
        } else {
            $('#e-message').text(message);
            $('#e-message').closest('.alert').show();
        }
    }

    $('#btnLogin').on('click', function (e) {
        $.blockUI()
        var hdUrlPrefix = $('#hdUrlPrefix').val();
        resetMessage();
        e.preventDefault();

        var $form = $('#loginFrm');
        $.validator.unobtrusive.parse($form);
        if ($form.valid()) {
            $('#btnLogin').prop("disabled", true);
            $.ajax({
                url: hdUrlPrefix + '/Auth/Login',
                data: $form.serialize(),
                type: 'POST',
                success: function (data) {
                    if (data.desp && data.desp !== null) {
                        showMessage(data.desp, 'error');
                        $('#btnLogin').prop("disabled", false);
                        //$('#message').text(data.desp);
                        //$('#message').closest('.alert').show().removeClass('alert-success').removeClass('alert-danger');
                        //$('#message').closest('.alert').addClass(data.flag === 0 ? 'alert-success' : 'alert-danger');

                        //$('#lblTitle').html('LOGIN');
                    }
                    else if (data.Url) {
                        window.location.href = data.Url;
                    }
                    else {
                        if (data.Error) {
                            $('#lblTitle').html(data.Error.toUpperCase() + ' <br/> RESET YOUR PASSWORD');
                        }
                        $('#formLogin').hide();
                        $('#formRecover').hide();
                        $('#formExpired').show();
                    }
                    $('#btnLogin').prop("disabled", false);
                    $.unblockUI();
                },
                error: function () {
                    $('#btnLogin').prop("disabled", false);
                    $.unblockUI();
                }
            })
        } else {
            $('#btnLogin').prop("disabled", false);
            $.unblockUI();
        }
    })

    $('#btnForgotPassword').on('click', function (e) {
        e.preventDefault();
        $('#formLogin').hide();
        $('#formRecover').show();
        $('#formSetpassword').hide();
        $('#EmailAddress').val('');
        $('#VerificationCode').val('');
        $('#NewPassword').val('');
        $('#ConfirmPassword').val('');
        //$('#LoginID').val();
        //$('#Password').val();
        resetMessage();
    });

    $('#btnRemembersPassword').on('click', function (e) {
        e.preventDefault();
        $('#formLogin').show();
        $('#formRecover').hide();
        $('#formSetpassword').hide();
        //$('#message').closest('.alert').hide().removeClass('alert-success').removeClass('alert-danger');
        resetMessage();
    });

    $('.btnBackToLogin').on('click', function (e) {
        e.preventDefault();
        $('#formLogin').show();
        $('#formRecover').hide();
        $('#formSetpassword').hide();
        resetMessage();
    });

    $('#btnSubmitEmailPassword').on('click', function (e) {
        $.blockUI()
        var hdUrlPrefix = $('#hdUrlPrefix').val();
        resetMessage();
        e.preventDefault();

        var $form = $('#recoverFrm');
        $.validator.unobtrusive.parse($form);
        if ($form.valid()) {
            $.ajax({
                url: hdUrlPrefix + '/Auth/ForgotPassword',
                data: $form.serialize(),
                type: 'POST',
                success: function (data) {
                    if (data.ResponseCode === 0) {
                        // show new password form to reset
                        $('#formLogin').hide();
                        $('#formRecover').hide();
                        $('#formSetpassword').show();
                    } else {
                        showMessage(data.ResponseDesc, 'error');
                    }
                    $.unblockUI();
                },
                error: function () {
                    $.unblockUI();
                }
            })
        } else {
            $.unblockUI();
        }
    })

    $('#btnResetNow').on('click', function (e) {
        $.blockUI()
        var hdUrlPrefix = $('#hdUrlPrefix').val();
        resetMessage();
        e.preventDefault();
        var $form = $('#resetpwdFrm');
        $.validator.unobtrusive.parse($form);
        if ($form.valid()) {
            $.ajax({
                url: hdUrlPrefix + '/Auth/ResetPassword',
                data: $form.serialize() + '&EmailAddress=' + $('#EmailAddress').val(),
                type: 'POST',
                success: function (data) {
                    if (data.ResponseCode === 0) {
                        // reset success
                        $('#formLogin').show();
                        $('#formRecover').hide();
                        $('#formSetpassword').hide();
                        showMessage('Reset password is successful! You may now login with your new password.', 'success');
                    } else {
                        showMessage(data.ResponseDesc, 'error');
                    }
                    $.unblockUI();
                },
                error: function () {
                    $.unblockUI();
                }
            })
        }
        $.unblockUI();
    })
});