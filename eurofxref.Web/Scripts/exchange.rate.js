var seriesData = [];
var graph;

function convertorOnChange() {
    $.ajax("/home/ExchangeRate/" + $("#From").val() + "/" + $("#To").val()).done(updateRate);
}

function updateRate(data) {

    $("#latestRate").text(data);
}

$(function () {
    convertorOnChange();

    var selectors = $("#convertor select");

    selectors.prepend($("<option>", { text: "EUR" }));
    selectors.val("EUR");
    selectors.change(convertorOnChange);
});
