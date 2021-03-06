﻿$(function () {

    // Don't allow browser caching of forms
    $.ajaxSetup({ cache: false });

    // Wire up the click event of any current or future dialog links
    $('.dialogLink').on('click', function () {
        var element = $(this);

        // Retrieve values from the HTML5 data attributes of the link        
        var dialogTitle = element.attr('data-dialog-title');
        var updateTargetId = '#' + element.attr('data-update-target-id');
        var updateUrl = element.attr('data-update-url');

        // Generate a unique id for the dialog div
        var dialogId = 'uniqueName-' + Math.floor(Math.random() * 1000)
        var dialogDiv = "<div id='" + dialogId + "'></div>";

        // Load the form into the dialog div
        $(dialogDiv).load(this.href, function () {

            $('.ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-dialog-buttons.ui-resizable').remove();
            $('.ui-widget-overlay.ui-front').remove();

            var modalDialog = $(this).dialog({
                modal: true,
                draggable: false,
                closeOnEscape: false,
                title: dialogTitle,
                width: "500px",
                buttons: {
                    Close: function () {
                        modalDialog.dialog('close');
                        return false;
                    }
                },
                open: function () {
                    $(this).parent().children().children(".ui-dialog-titlebar-close").hide();                    
                    var link = '<a id="dialog-div-close" href="javascript:void(0);" style="float:right;text-decoration:none;" class="btn btn-primary" title="Close">X</a>';
                    $(this).parent().find(".ui-dialog-titlebar").append(link);
                    $('#dialog-div-close').on('click', function () {
                        $('.ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-dialog-buttons.ui-resizable').remove();
                        $('.ui-widget-overlay.ui-front').remove();
                    });
                    return false;
                },
                close: function () {
                    $('.ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-dialog-buttons.ui-resizable').remove();
                    $('.ui-widget-overlay.ui-front').remove();
                    return false;
                }
            });

            ////hide close button
            //$("button[title=Close]").hide();

            // Enable client side validation
            $.validator.unobtrusive.parse(this);

            // Setup the ajax submit logic
            wireUpForm(this, updateTargetId, updateUrl);
        });

        return false;
    });
});

function wireUpForm(dialog, updateTargetId, updateUrl) {
    $('form', dialog).submit(function () {

        // Do not submit if the form
        // does not pass client side validation
        if (!$(this).valid())
            return false;

        // Client side validation passed, submit the form
        // using the jQuery.ajax form
        var msgLabel = $("<label class='msgLabel'></label>");
        var count = 0;

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                count += 1;
                if (count < 2) {
                    // Check whether the post was successful
                    if (result.success) {
                        //show msg
                        var ele = $(dialog).find('.msgLabel');
                        if (ele) {
                            $(ele).remove();
                        }
                        $(msgLabel).addClass("text-success");
                        $(msgLabel).html(result.msg);
                        $(dialog).html(msgLabel);

                        // Reload the updated data in the target div
                        //$(updateTargetId).load(updateUrl);
                    }
                    else {
                        // Reload the dialog to show model errors
                        var ele = $(dialog).find('.msgLabel');
                        if (ele) {
                            $(ele).remove();
                        }
                        $(msgLabel).html(result.msg);
                        $(msgLabel).removeClass("text-success");
                        $(msgLabel).addClass("text-danger");
                        $(dialog).html(msgLabel);

                        // Enable client side validation
                        $.validator.unobtrusive.parse(dialog);

                        // Setup the ajax submit logic
                        wireUpForm(dialog, updateTargetId, updateUrl);
                    }
                }
            }
        });
        return false;
    });
}