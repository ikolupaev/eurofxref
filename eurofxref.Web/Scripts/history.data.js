var seriesData = [];
var graph;

function currencyOnChange() {
    $.ajax("/home/historydata/" + $("#HistoryCurrency").val()).done(updateGraph);
}

function updateGraph(data) {

    for (var i = 0; i < data.length; i++) {
        seriesData[i] = data[i];
    }

    if (graph === undefined) {
        createGraph();
    }

    graph.update();
}

function createGraph() {

    graph = new Rickshaw.Graph({
        element: document.getElementById("chart"),
        height: 300,
        renderer: 'line',
        series: [
            {
                color: "#30c020",
                data: seriesData,
                name: 'Exchange rate'
            }
        ]
    });

    var hoverDetail = new Rickshaw.Graph.HoverDetail({
        graph: graph,
        formatter: function (series, x, y) {
            return new Date(x).toUTCString() + "- EUR/" + $("#HistoryCurrency").val() + " - " + y;
        }
    });

    graph.render();
}

$(function () {
    currencyOnChange();
    $("#HistoryCurrency").change(currencyOnChange);
});
