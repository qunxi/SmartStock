$(document).ready(function () {
    $("#initialStocks").click(function() {
        $.post("/maintain/InitialAllStocksHistory");
    });

    $("#updatemissing").click(function () {
        $.post("/maintain/UpdateLoggingData");
    });
});