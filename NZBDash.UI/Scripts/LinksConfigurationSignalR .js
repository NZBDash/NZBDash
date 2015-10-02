$(function () {

    var hub = $.connection.linksConfigurationHub;

    hub.client.success = function (msg) {
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
    };

    hub.client.error = function (msg) {
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

    hub.client.remove = function (id) {
        var idName = "#"+id + "panel";
        $(idName).slideUp();
    };

    hub.client.addLink = function (id) {
        $.ajax('/LinksConfiguration/GetLink?id=' + id).success(function (data) {
            $('#Links').append(data);
        });
    };


    $.connection.hub.start().done(function () {
        console.log("Connected");
        $('#addLink').click(function () {
            var name = $('#LinkName').val();
            var endpoint = $('#LinkEndpoint').val();
            hub.server.addLink(name, endpoint);
        });
        $('#Links').on("click", ".remove", function (e) {
            hub.server.removeLink(e.target.id);
        });
    });
});