$(function () {

    var hub = $.connection.applicationConfigurationHub;

    hub.client.removeSuccess = function (id, msg) {
        $.notify({
            // options
            message: msg
        }, {
            // settings
            newest_on_top: true,
            type: 'info',
            placement: {
                from: "bottom",
                align: "right"
            }
        });
        $("#" + id + "panel").slideUp();
    };

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

    hub.client.appMessage = function (msg, id) {
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
        $.ajax('/ApplicationConfiguration/GetApplicationConfiguration?id=' + id).success(function (data) {
            $('#applications').append(data);
        });
    };


    $.connection.hub.start().done(function () {
        $('#applications').on("click", ".remove", function (e) {
            hub.server.deleteApplication(e.target.id);
        });

        $('#addApp').click(function (e) {
            var appId = $('#Applications').val();
            var api = $('#ApiKey').val();
            var ip = $('#IpAddress').val();
            var pass = $('#Password').val();
            var user = $('#Username').val();

            hub.server.addApplication(appId, api, ip, pass, user);
        });

        function updateApp(model) {
            hub.server.addApplication(model);
        }

        $('#testConnection').click(function() {
            var appId = $('#Applications').val();
            var api = $('#ApiKey').val();
            var ip = $('#IpAddress').val();
            var pass = $('#Password').val();
            var user = $('#Username').val();

            hub.server.testConnection(appId, api, ip, pass, user);
        });
    });
});