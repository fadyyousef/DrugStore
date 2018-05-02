var registerObj = {

    toggleActive: function () {
        var isChecked = document.getElementById("chkActive").checked;
        if (isChecked) {
            var elem = $("#chkActive").parents(".input-group").children().find('.fa.fa-times');
            $(elem).css({ "color": "#3b5997" });
            $(elem).removeClass('fa-times');
            $(elem).addClass('fa-check');
        }
        else {
            var elem = $("#chkActive").parents(".input-group").children().find('.fa.fa-check');
            $(elem).css("color", "#ff0000");
            $(elem).addClass('fa-times');
            $(elem).removeClass('fa-check');
        }
    },

    togglePassword: function () {
        $('#txtPassword + .fa').on('click', function () {
            $("#txtPassword + .fa")
                .mousedown(function (e) {
                    $(this).addClass('fa-eye-slash');
                    $(this).removeClass("fa-eye");
                    var firstElem = $("#txtPassword").siblings().children('.fa.fa-lock');
                    var secondElem = $("#txtConfirmPassword").siblings().children('.fa.fa-lock');

                    $(firstElem).css({ "color": "#ff0000" });
                    $(secondElem).css({ "color": "#ff0000" });

                    $(firstElem).removeClass('fa-lock');
                    $(secondElem).removeClass('fa-lock');

                    $(firstElem).addClass('fa-lock-open');
                    $(secondElem).addClass('fa-lock-open');

                    $("#txtPassword").attr("type", "text");
                    $("#txtConfirmPassword").attr("type", "text");
                })
                .mouseup(function (e) {
                    $(this).addClass("fa-eye");
                    $(this).removeClass('fa-eye-slash');
                    var firstElem = $("#txtPassword").siblings().children('.fa.fa-lock-open');
                    var secondElem = $("#txtConfirmPassword").siblings().children('.fa.fa-lock-open');

                    $(firstElem).css({ "color": "#3b5997" });
                    $(secondElem).css({ "color": "#3b5997" });

                    $(firstElem).removeClass('fa-lock-open');
                    $(secondElem).removeClass('fa-lock-open');

                    $(firstElem).addClass('fa-lock');
                    $(secondElem).addClass('fa-lock');

                    $("#txtPassword").attr("type", "password");
                    $("#txtConfirmPassword").attr("type", "password");
                })
                .mouseout(function (e) {
                    $(this).addClass("fa-eye");
                    $(this).removeClass('fa-eye-slash');
                    var firstElem = $("#txtPassword").siblings().children('.fa.fa-lock-open');
                    var secondElem = $("#txtConfirmPassword").siblings().children('.fa.fa-lock-open');

                    $(firstElem).css({ "color": "#3b5997" });
                    $(secondElem).css({ "color": "#3b5997" });

                    $(firstElem).removeClass('fa-lock-open');
                    $(secondElem).removeClass('fa-lock-open');

                    $(firstElem).addClass('fa-lock');
                    $(secondElem).addClass('fa-lock');

                    $("#txtPassword").attr("type", "password");
                    $("#txtConfirmPassword").attr("type", "password");
                });
        });
        $('#txtPassword + .fa').click();
    },

    validatePhone: function (elem) {
        $("#" + elem).on("keydown keyup change paste input", function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
        $("#" + elem).on("contextmenu", function (e) {
            e.preventDefault();
        });
    },

    validatePassword: function () {
        $("#txtConfirmPassword").on("keydown keyup change paste input", function (e) {
            var password = $("#txtPassword").val();
            var confirmPassword = $("#txtConfirmPassword").val();

            // Check for equality with the password inputs
            if (password != confirmPassword) {
                $("#ErrConfirmPassword").html("Passwords do not match!");
            }
            else {
                $("#ErrConfirmPassword").html("");
            }
        })
    },

    validatePage: function () {
        $(".text-danger").each(function () {
            if ($(this).text() != "") {
                $(this).val($(this).text());
                $(this).html($(this).text());
                $(this).siblings().addClass('has-error');
            }
        });

        var isValid = false;
        $("input.form-control").on("click", function (e) {
            if ($(this).hasClass("input-validation-error")) {
                console.log(this);
                return false;
            }
        });
        $("input.form-control").on("change paste input", function (e) {
            $("input.form-control").each(function () {
                if (this.value.length > 2) {
                    isValid = true;
                }
                else {
                    isValid = false;
                }
            });

            if (isValid) {
                $("input[value='Register']").attr("disabled", false);
                $("input[value='Register']").removeClass("btn-default");
                $("input[value='Register']").addClass("btn-primary");
            }
            else {
                $("input[value='Register']").attr("disabled", "disabled");
                $("input[value='Register']").addClass("btn-default");
                $("input[value='Register']").removeClass("btn-primary");
            }
        });
    },

    reset: function () {
        $("#btnReset").click(function () {
            $("input.form-control").each(function () {
                this.value = "";
                $(this).parents().removeClass('has-error');
            });
            $(".text-danger").each(function () {
                $(this).text("");
            });
        });
    },

    toggleLogin: function () {
        $("#txtPassword + .fa")
            .mousedown(function (e) {
                $(this).addClass('fa-eye-slash');
                $(this).removeClass("fa-eye");
                var firstElem = $("#txtPassword").siblings().children('.fa.fa-lock');
                $(firstElem).css({ "color": "#ff0000" });
                $(firstElem).removeClass('fa-lock');
                $(firstElem).addClass('fa-lock-open');
                $("#txtPassword").attr("type", "text");
                $("#txtConfirmPassword").attr("type", "text");
            })
            .mouseup(function (e) {
                $(this).addClass("fa-eye");
                $(this).removeClass('fa-eye-slash');
                var firstElem = $("#txtPassword").siblings().children('.fa.fa-lock-open');
                $(firstElem).css({ "color": "#3b5997" });
                $(firstElem).removeClass('fa-lock-open');
                $(firstElem).addClass('fa-lock');
                $("#txtPassword").attr("type", "password");
                $("#txtConfirmPassword").attr("type", "password");
            })
            .mouseout(function (e) {
                $(this).addClass("fa-eye");
                $(this).removeClass('fa-eye-slash');
                var firstElem = $("#txtPassword").siblings().children('.fa.fa-lock-open');
                $(firstElem).css({ "color": "#3b5997" });
                $(firstElem).removeClass('fa-lock-open');
                $(firstElem).addClass('fa-lock');
                $("#txtPassword").attr("type", "password");
                $("#txtConfirmPassword").attr("type", "password");
            });
    },

    toggleAccordion: function () {
        $(".panel-collapse").collapse("hide");
        $(".panelLink").children('span').css({ "color": "#4cae4c" });

        $(".panel-collapse").on("show.bs.collapse", function () {
            var id = "#" + this.id;
            $(id).siblings().children().find('span').removeClass("fa-plus").addClass("fa-minus");
            $(id).siblings().children().find('span').css({ "color": "#ac2925" });
        });
        $(".panel-collapse").on("hide.bs.collapse", function () {
            var id = "#" + this.id;
            $(id).siblings().children().find('span').removeClass("fa-minus").addClass("fa-plus");
            $(id).siblings().children().find('span').css({ "color": "#4cae4c" });
        });
    },

    validateDecimals: function (elem) {
        $("#" + elem).keydown(function (event) {
            if (event.shiftKey == true) {
                event.preventDefault();
            }
            if ((event.keyCode >= 48 && event.keyCode <= 57) ||
                (event.keyCode >= 96 && event.keyCode <= 105) ||
                event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 37 ||
                event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

            } else {
                event.preventDefault();
            }
            if ($(this).val().indexOf('.') !== -1 && event.keyCode == 190) {
                event.preventDefault();
            }
        });
    },
}