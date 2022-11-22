// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    $('#date').datepicker({
        uiLibrary: 'bootstrap4'
    });

    $('#hearingdate').datepicker({
        uiLibrary: 'bootstrap4'
    });

    $('#hearingtime').timepicker({
        uiLibrary: 'bootstrap4'
    });

    $('#result').DataTable();

    $(".btn-upload").click(function (e) {

        e.preventDefault();

        if ($("#courtcaseno").val() == '') {
            $("#courtcaseno").siblings().addClass("b");
            $("#courtcaseno").focus();
            return;
        } else {
            $("#courtcaseno").siblings().removeClass("b");
        }

        if ($("#uploaderemail").val() == '') {
            $("#uploaderemail").siblings().addClass("b");
            $("#uploaderemail").focus();
            return;
        } else {
            $("#uploaderemail").siblings().removeClass("b");
        }

        if ($("#jurisdiction").val() == '') {
            $("#jurisdiction").siblings().addClass("b");
            $("#jurisdiction").focus();
            return;
        } else {
            $("#jurisdiction").siblings().removeClass("b");
        }

        if ($("#chamberid").val() == '') {
            $("#chamberid").siblings().addClass("b");
            $("#chamberid").focus();
            return;
        } else {
            $("#chamberid").siblings().removeClass("b");
        }

        if ($("#hearingtype").val() == '') {
            $("#hearingtype").siblings().addClass("b");
            $("#hearingtype").focus();
            return;
        } else {
            $("#hearingtype").siblings().removeClass("b");
        }

        if ($("#firstname").val() == '') {
            $("#firstname").siblings().addClass("b");
            $("#firstname").focus();
            return;
        } else {
            $("#firstname").siblings().removeClass("b");
        }

        if ($("#lastname").val() == '') {
            $("#lastname").siblings().addClass("b");
            $("#lastname").focus();
            return;
        } else {
            $("#lastname").siblings().removeClass("b");
        }

        if ($("#hearingdate").val() == '') {
            $("#hearingdate").siblings().addClass("b");
            $("#hearingdate").focus();
            return;
        } else {
            $("#hearingdate").siblings().removeClass("b");
        }

        if ($("#hearingtime").val() == '') {
            $("#hearingtime").siblings().addClass("b");
            $("#hearingtime").focus();
            return;
        } else {
            $("#hearingtime").siblings().removeClass("b");
        }

        var params = {
            courtcaseno: $("#courtcaseno").val(),
            jurisdiction: $("#jurisdiction").val(),
            uploaderemail: $("#uploaderemail").val(),
            firstname: $("#firstname").val(),
            lastname: $("#lastname").val(),
            chamberid: $("#chamberid").val(),
            hearingdate: $("#hearingdate").val(),
            hearingtime: $("#hearingtime").val(),
            hearingtype: $("#hearingtype").val(),
        };

        if (params != null) {
            $.ajax({
                type: "POST",
                url: "/Agenda/Create",
                data: params,
                contentType: 'application/x-www-form-urlencoded',
                success: function (response) {
                    if (response != null) {
                        //$('#rsDlg').on('hide', function () {
                        window.location.href = "/Manage/CourtCase/AgendasByEmail?email=" + params.uploaderemail;
                        //});
                        $("#myModal").modal('hide');

                        $('.toast-body').text("Upload succesully.");
                        $('.toast').toast({ animation: true, delay: 2000 });
                        $('.toast').toast('show');
                    } else {
                        $("#myModal").modal("hide");
                        $('.toast-body').text("Upload failed.");
                        $('.toast').toast({ animation: true, delay: 2000 });
                        $('.toast').toast('show');
                    }
                },
                failure: function (response) {
                    $('.toast-body').text(response.responseText);
                    $('.toast').toast({ animation: true, delay: 2000 });
                    $('.toast').toast('show');
                },
                error: function (response) {
                    $('.toast-body').text(response.responseText);
                    $('.toast').toast({ animation: true, delay: 2000 });
                    $('.toast').toast('show');
                }
            });
        }
    });

    $("#date").change(function (e) {
        var tmp = $(e.target).val();
        if (tmp.includes("/")) {
            var arr = tmp.split("/");
            var newDate = arr[2] + "-" + arr[0] + "-" + arr[1];
            $("#date").val(newDate);
        }
    });

    $("#hearingdate").change(function (e) {
        var tmp = $(e.target).val();
        if (tmp.includes("/")) {
            var arr = tmp.split("/");
            var newDate = arr[2] + "-" + arr[0] + "-" + arr[1];
            $("#hearingdate").val(newDate);
        }
    });
    if (window.location.href.includes("Match") || window.location.href.includes("GetByCourtCase")) {
        getJuridictionList();
    }


});

function formatResultJuridiction(data) {
    if (!data.id) return data.message;
    var res = data.type_juridiction.split(' - ');
    var lib_juridiction = '<div> ' + data.jur_num + '-' + data.jur_annexe + ' ' + res[0] + ' ' + data.canton;
    if (data.division_id != '000') { lib_juridiction = lib_juridiction + ' AFDELING ' + data.division; }
    if (data.type_juridiction_id == 38) { lib_juridiction = lib_juridiction.replace('DIVISION', 'SIEGE'); lib_juridiction = lib_juridiction.replace('AFDELING', 'ZETEL'); }
    section = (res.length > 1) ? ', ' + res[1] : '';
    lib_juridiction = lib_juridiction + section
    lib_juridiction = lib_juridiction + '</div>'
    return lib_juridiction;
}
function formatSelectionJuridiction(data) {
    var lib_juridiction = '';
    if (!data.typeJuridiction) { return "Rechtscollege"; }
    var res = data.typeJuridiction.split(' - ');
    lib_juridiction = res[0] + ' ' + data.canton;
    if (data.divisionId != '000') { lib_juridiction = lib_juridiction + ' AFDELING ' + data.division; }
    if (data.typeJuridictionId == 38) {
        lib_juridiction = lib_juridiction.replace('DIVISION', 'SIEGE');
        lib_juridiction = lib_juridiction.replace('AFDELING', 'ZETEL');
    }
    var section = (res.length > 1) ? ', ' + res[1] : '';
    lib_juridiction = lib_juridiction + section;
    return lib_juridiction;
}
function formatNoResultJuridiction(term) {
    return '<div>No result for Juridiction ' + term + '</div>';
}
function compare(a, b) {
    if (a.last_nom < b.last_nom) {
        return -1;
    }
    if (a.last_nom > b.last_nom) {
        return 1;
    }
    return 0;
}

function approveAgenda(elem, e) {
    e.preventDefault();
    var count = 0;
    var modalConfirm = function (callback) {

        if (count == 0) {
            $("#approveModal").modal('show');
            count++;
        } else {
            return;
        }        

        $("#modal-btn-ok").on("click", function () {
            callback(true);
            $("#approveModal").modal('hide');
            return;
        });

        $("#modal-btn-cancel").on("click", function () {
            callback(false);
            $("#approveModal").modal('hide');
            return;
        });
    };

    modalConfirm(function (confirm) {
        if (confirm) {
            var caseno = $(elem).data("caseno");
            if (caseno != null) {
                $.ajax({
                    type: "GET",
                    url: "/Manage/CourtCase/Approve?caseno=" + caseno,
                    data: caseno,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != null) {
                            $('.toast-body').text(response.value);
                            $('.toast').toast({ animation: true, delay: 2000, fadeIn: true, fadeDelay: 2500, fadeOut: true });
                            $('.toast').toast('show');
                        } else {
                            $('.toast-body').text(response.value);
                            $('.toast').toast({ animation: true, delay: 2000 });
                            $('.toast').toast('show');
                        }
                    },
                    failure: function (response) {
                        $('.toast-body').text(response.responseText);
                        $('.toast').toast({ animation: true, delay: 2000 });
                        $('.toast').toast('show');
                    },
                    error: function (response) {
                        $('.toast-body').text(response.responseText);
                        $('.toast').toast({ animation: true, delay: 2000 });
                        $('.toast').toast('show');
                    }
                });
            }
        }
    });
    
}