$(document).ready(function () {
    $(document).on("click", "#btnExtractVariables", pg.btnExtractVariables_click);
    $(document).on("click", "#btnReplaceVariables", pg.btnReplaceVariables_click);

    $(document).ajaxStart(function () {
        $("#waiter").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#waiter").css("display", "none");
    });
});

var pg = {
    extractVariablesAction: 'Home/ExtractVariables',
    replaceVariablesAction: 'Home/ReplaceVariables',
    minExpressionLength: 3,
    tooShortMessage: '*Please enter more longer expression',

    btnExtractVariables_click: function () {
        pg.validateLength();

        $.ajax({
                method: "POST",
                url: pg.extractVariablesAction,
            data: { expression: $('#inpExpression').val() }
                })
                .done(function (data) {
                    $('#divVariables').html('');
                    for (var el in data) {
                        $('#divVariables')
                            .append("<span>" + data[el] + "</span>:&nbsp;<input id='" + data[el] + "' /><br /><br />" );
                    }
               
                $('#divReplace').show();
        });
    },

    btnReplaceVariables_click: function () {
        var variables = [];
        $('#divVariables :input').each(function (index) {
            variables.push({ key: $(this).attr('id'), value: $(this).val() });
        });

        $.ajax({
                method: "POST",
                url: pg.replaceVariablesAction,
                data: { variables: variables }
            })
            .done(function (data) {
                pg.showMessage(data);
            });
    },

    validateLength: function() {
        if ($('#inpExpression').val().length < pg.minExpressionLength) {
            pg.showMessage(pg.tooShortMessage);
            return;
        } else {
            pg.hideMessage();
        }
    },

    showMessage: function(message) {
        $('#divMessage').text(message);
        $('#divMessage').show();
    },

    hideMessage: function () {
        $('#divMessage').text('');
        $('#divMessage').hide();
    }
}
