
var listOfXbmc = [];
function addToXbmcHosts(ipaddress) {
    listOfXbmc.push(ipaddress);
}

function pingCallback(data, ipaddress) {
    $("#" + ipaddress).append('d')
    $("#" + ipaddress).val('d')
    //alert($('#'+ipaddress).val())
}
function xbmcCall(ipaddress, method, callBack) {

    var myUrl = 'http://' + ipaddress;
    myUrl = myUrl + '/jsonrpc?request={"jsonrpc":"2.0", "id":1, "method" : "' + method + '", "params":{}}';
    $.ajax({
        url: "http://localhost:8085/api/StatusApi/Proxy?url=" + encodeURIComponent(myUrl),
        type: 'GET',
        dataType: 'json',
        success: function (data, a, b) {
            pingCallback(data, ipaddress);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
}

function runXbmc() {
    for (var i = 0; i < listOfXbmc.length; i++) {
        //alert(listOfXbmc[i])
        //window.location(myUrl)
        xbmcCall(listOfXbmc[i], 'JSONRPC.Ping', pingCallback)
    }
}

var timeout = 200000;
function updateNetwork() {
    $.ajax({
        url: "http://localhost:8085/api/StatusApi/NetworkInfo",
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, xhr) {
            $('#ReceiveSpan').html('Recieving : ' + data.Recieved);
            $('#SendSpan').html('Sending : ' + data.Recieved);
            $('#TotalSpan').html('Total : ' + data.Recieved);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error in Operation');
        }
    });
}
function updateUptime() {
    $.ajax({
        url: "http://localhost:8085/api/StatusApi/UpTime",
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, xhr) {
            $('#UpTimeSpan').html('Up Time: ' + data);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error in Operation');
        }
    });
}
function updateData() {
    //updateNetwork();
    //updateUptime();

    setTimeout(updateData, timeout);
}
setTimeout(updateData, timeout);





