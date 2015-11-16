
var gridster;
gridster = $(".gridster ul").gridster({
    widget_margins: [10, 10],
    widget_base_dimensions: [140, 140],

    serialize_params: function ($w, wgd) {
        var x = $($w).wrap('<p/>').parent().html();
        $($w).unwrap();
        return {
            id: $($w).attr('id'),
            col: wgd.col,
            row: wgd.row,
            size_x: wgd.size_x,
            size_y: wgd.size_y,
            htmlContent: x
        };
    }
}).data('gridster');


function resizeWidgets(down, right,wIndex) {
    gridster.resize_widget(gridster.$widgets.eq(wIndex), right, down);
}



    var hub = $.connection.dashboardHub;

    $('#Edit').click(function () {
        $('#save').show();
        $('#Edit').hide();
        gridster.enable();
    });

    //hub.client.updateGrid = function (grid) {

    //    gridster.remove_all_widgets();

    //    var serialization = Gridster.sort_by_row_and_col_asc(grid);
    //    $.each(serialization, function () {
    //        gridster.add_widget(this.htmlContent, this.size_x, this.size_y, this.col, this.row);
    //    });
    //};

    //hub.client.loadDefaultGrid = function () {

    //};

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



    $('#save').click(function () {
        var s = gridster.serialize();
        console.log(s);
        hub.server.saveGridPosition(s);

        $('#save').hide();
        $('#Edit').show();
        gridster.disable();
    });

    $.connection.hub.start().done(function () {
        console.log("connected");
        hub.server.getGrid();
        gridster.disable();

    });
