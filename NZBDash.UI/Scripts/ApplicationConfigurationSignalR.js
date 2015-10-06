$(function () {
    $('#testNzbGetConnection').prop("disabled", true);
    var hub = $.connection.applicationConfigurationHub;

    hub.client.failed = function (msg) {
        $.notify({
            // options
            message: msg
        }, {
            // settings
            newest_on_top: true,
            type: 'danger',
            placement: {
                from: "bottom",
                align: "right"
            }
        });
    };

    hub.client.message = function (msg) {
        $.notify({
            // options
            message: msg
        }, {
            // settings
            newest_on_top: true,
            type: 'success',
            placement: {
                from: "bottom",
                align: "right"
            }
        });
    };

    $.connection.hub.start().done(function () {
        $('#testNzbGetConnection').prop("disabled",false);

        $('#testNzbGetConnection').click(function () {
            var ip = $('#IpAddress').val();
            var pass = $('#Password').val();
            var user = $('#Username').val();
            var port = $('#Port').val();
            // string ipAddress, int port, string username, string password
            hub.server.testNzbGetConnection(ip, port, user, pass);
        });
    });
});