$(document).ready(function () {
    $("#initialStocks").click(function() {
        $.post("/maintain/InitialAllStocksHistory");
    });
});