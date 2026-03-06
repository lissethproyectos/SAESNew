var root = am5.Root.new("chartdiv");

function Grafica1(TipoGrafica, Ciclo, Campus, Nivel) {
    //$("#precarga").show();

    graficasContext.ObtenerDatosGraficaAdeudos(TipoGrafica, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                self.listDatosAdeudos = graficasContext.listDatosAdeudos;
                am5.ready(function () {

                    // Create root element
                    // https://www.amcharts.com/docs/v5/getting-started/#Root_element
                    root.container.children.clear();


                    // Set themes
                    // https://www.amcharts.com/docs/v5/concepts/themes/
                    root.setThemes([
                        am5themes_Animated.new(root)
                    ]);


                    // Create chart
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/
                    var chart = root.container.children.push(am5xy.XYChart.new(root, {
                        panX: false,
                        panY: false,
                        wheelX: "panX",
                        wheelY: "zoomX",
                        layout: root.verticalLayout
                    }));

                    var colors = chart.get("colors");

                    var data = [];
                    for (var x = 0; x < self.listDatosAdeudos.length; x++) {
                        if (x === 0) {
                            data.push({
                                "campus": "",
                                "visits": 0
                            });
                        }
                        data.push({
                            "campus": self.listDatosAdeudos[x].Nombre,
                            "visits": parseInt(self.listDatosAdeudos[x].Porcentaje)
                        });
                    }

                    //var data = [{
                    //    campus: "",
                    //    visits: 0
                    //}, {
                    //    campus: "LOMAS VERDES",
                    //    visits: 76.32
                    //}, {
                    //    campus: "San Rafael",
                    //    visits: 70.00
                    //}];

                    prepareParetoData();

                    function prepareParetoData() {
                        var total = 0;

                        for (var i = 0; i < data.length; i++) {
                            var value = data[i].visits;
                            total += value;
                        }

                        var sum = 0;
                        for (var i = 0; i < data.length; i++) {
                            var value = data[i].visits;
                            sum += value;
                            data[i].pareto = sum / total * 100;
                        }
                    }



                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, {
                        minGridDistance: 30
                    })

                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        categoryField: "campus",
                        renderer: xRenderer
                    }));

                    xRenderer.grid.template.setAll({
                        location: 1
                    })

                    xRenderer.labels.template.setAll({
                        paddingTop: 20
                    });

                    xAxis.data.setAll(data);

                    var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                        renderer: am5xy.AxisRendererY.new(root, {
                            strokeOpacity: 0.1
                        })
                    }));

                    var paretoAxisRenderer = am5xy.AxisRendererY.new(root, { opposite: true });
                    var paretoAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                        renderer: paretoAxisRenderer,
                        min: 0,
                        max: 100,
                        strictMinMax: true
                    }));

                    paretoAxisRenderer.grid.template.set("forceHidden", true);
                    paretoAxis.set("numberFormat", "#'%");


                    // Add series
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
                    var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                        xAxis: xAxis,
                        yAxis: yAxis,
                        valueYField: "visits",
                        categoryXField: "campus"
                    }));

                    series.columns.template.setAll({
                        tooltipText: "{categoryX}: {valueY}",
                        tooltipY: 0,
                        strokeOpacity: 0,
                        cornerRadiusTL: 6,
                        cornerRadiusTR: 6
                    });

                    series.columns.template.adapters.add("fill", function (fill, target) {
                        return chart.get("colors").getIndex(series.dataItems.indexOf(target.dataItem));
                    })


                    // pareto series
                    var paretoSeries = chart.series.push(am5xy.LineSeries.new(root, {
                        xAxis: xAxis,
                        yAxis: paretoAxis,
                        valueYField: "pareto",
                        categoryXField: "campus",
                        stroke: root.interfaceColors.get("alternativeBackground"),
                        maskBullets: false
                    }));

                    paretoSeries.bullets.push(function () {
                        return am5.Bullet.new(root, {
                            locationY: 1,
                            sprite: am5.Circle.new(root, {
                                radius: 5,
                                fill: series.get("fill"),
                                stroke: root.interfaceColors.get("alternativeBackground"),
                            })

                        })
                    })



                    series.data.setAll(data);
                    paretoSeries.data.setAll(data);

                    // Make stuff animate on load
                    // https://www.amcharts.com/docs/v5/concepts/animations/
                    series.appear();
                    chart.appear(1000, 100);

                });
                //document.getElementById("ctl00_MainContent_lblNivel").innerHTML = "PAGOS: " + Total;
                //document.getElementById("ctl00_MainContent_lblNive2").innerHTML = "TOTAL: " + ImporteTotal.toLocaleString('es-MX');

                //$("#precarga").hide();
                break;
            case "notgp":
                document.getElementById("tituloChart").innerHTML = "Error al recuperar los datos, intentar más tarde."
                //$("#precarga").hide();
                break;
            default:
                break;
        }
    });
};
function Grafica2(TipoGrafica, Ciclo, Campus, Nivel) {
    graficasContext.ObtenerDatosGraficaAdeudos(TipoGrafica, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        root.container.children.clear();

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosAdeudos = graficasContext.listDatosAdeudos;
                am5.ready(function () {

                    // Create root element
                    // https://www.amcharts.com/docs/v5/getting-started/#Root_element

                    // Set themes
                    // https://www.amcharts.com/docs/v5/concepts/themes/
                    root.setThemes([
                        am5themes_Animated.new(root)
                    ]);


                    // Create chart
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/
                    var chart = root.container.children.push(am5xy.XYChart.new(root, {
                        panX: true,
                        panY: true,
                        wheelX: "panX",
                        wheelY: "zoomX",
                        pinchZoomX: true
                    }));

                    // Add cursor
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
                    var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
                    cursor.lineY.set("visible", false);


                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, { minGridDistance: 30 });
                    xRenderer.labels.template.setAll({
                        rotation: -90,
                        centerY: am5.p50,
                        centerX: am5.p100,
                        paddingRight: 15
                    });

                    xRenderer.grid.template.setAll({
                        location: 1
                    })

                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        maxDeviation: 0.3,
                        categoryField: "country",
                        renderer: xRenderer,
                        tooltip: am5.Tooltip.new(root, {})
                    }));

                    var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                        maxDeviation: 0.3,
                        renderer: am5xy.AxisRendererY.new(root, {
                            strokeOpacity: 0.1
                        }),
                        min: 0,
                        max: 100
                    }));

                    //yAxis.min = 1;
                    //yAxis.max = 100; 

                    // Create series
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
                    var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                        name: "Series 1",
                        xAxis: xAxis,
                        yAxis: yAxis,
                        valueYField: "value",
                        sequencedInterpolation: true,
                        categoryXField: "country",
                        tooltip: am5.Tooltip.new(root, {
                            labelText: "{valueY}"
                        })
                    }));

                    // Add Label bullet
                    //series.bullets.push(function () {
                    //    return am5.Bullet.new(root, {
                    //        locationY: 1,
                    //        sprite: am5.Label.new(root, {
                    //            text: "{valueYWorking.formatNumber('#.')}",
                    //            fill: root.interfaceColors.get("alternativeText"),
                    //            centerY: 0,
                    //            centerX: am5.p50,
                    //            populateText: true
                    //        })
                    //    });
                    //});

                    series.bullets.push(function (root) {
                        return am5.Bullet.new(root, {
                            locationX: 0.5,
                            locationY: 0.5,
                            sprite: am5.Label.new(root, {
                                text: "{valueY}" + "% Vencido",
                                centerX: am5.percent(50),
                                centerY: am5.percent(50),
                                populateText: true
                            })
                        });
                    });



                    series.columns.template.setAll({ cornerRadiusTL: 5, cornerRadiusTR: 5, strokeOpacity: 0 });
                    series.columns.template.adapters.add("fill", function (fill, target) {
                        return chart.get("colors").getIndex(series.columns.indexOf(target));
                    });

                    series.columns.template.adapters.add("stroke", function (stroke, target) {
                        return chart.get("colors").getIndex(series.columns.indexOf(target));
                    });



                    var data = [];
                    for (var x = 0; x < self.listDatosAdeudos.length; x++) {
                        //if (x === 0) {
                        //    data.push({
                        //        "country": "",
                        //        "value": 0
                        //    });
                        //}
                        data.push({
                            "country": self.listDatosAdeudos[x].Nombre,
                            "value": parseInt(self.listDatosAdeudos[x].Porcentaje)
                        });
                    }
                    xAxis.data.setAll(data);
                    series.data.setAll(data);


                    // Make stuff animate on load
                    // https://www.amcharts.com/docs/v5/concepts/animations/
                    series.appear(1000);
                    chart.appear(1000, 100);

                }); // end am5.ready()
                break;
            case "notgp":
                //$("#chartdiv").empty();
                document.getElementById("tituloChart").innerHTML = "Error al recuperar los datos, intentar más tarde."
                //$("#precarga").hide();
                break;
            default:
                break;
        }
    });
};
function GraficaPorcentaje(TipoGrafica, Ciclo, Campus, Nivel) {
    graficasContext.ObtenerDatosGraficaAdeudos(TipoGrafica, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        root.container.children.clear();

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosAdeudos = graficasContext.listDatosAdeudos;
                am5.ready(function () {


                    // Create root element
                    // https://www.amcharts.com/docs/v5/getting-started/#Root_element
                    root.container.children.clear();


                    // Set themes
                    // https://www.amcharts.com/docs/v5/concepts/themes/
                    root.setThemes([
                        am5themes_Animated.new(root)
                    ]);


                    // Create chart
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/
                    var chart = root.container.children.push(am5xy.XYChart.new(root, {
                        panX: false,
                        panY: false,
                        wheelX: "panX",
                        wheelY: "zoomX",
                        layout: root.verticalLayout
                    }));

                    // Add scrollbar
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/scrollbars/
                    chart.set("scrollbarX", am5.Scrollbar.new(root, {
                        orientation: "horizontal"
                    }));

                    //var data = [{
                    //    "year": "2021",
                    //    "europe": 2.5,
                    //    "namerica": 2.5,
                    //    "asia": 2.1,
                    //    "lamerica": 1,
                    //    "meast": 0.8,
                    //    "africa": 0.4
                    //}, {
                    //    "year": "2022",
                    //    "europe": 2.6,
                    //    "namerica": 2.7,
                    //    "asia": 2.2,
                    //    "lamerica": 0.5,
                    //    "meast": 0.4,
                    //    "africa": 0.3
                    //}, {
                    //    "year": "2023",
                    //    "europe": 2.8,
                    //    "namerica": 2.9,
                    //    "asia": 2.4,
                    //    "lamerica": 0.3,
                    //    "meast": 0.9,
                    //    "africa": 0.5
                    //}]

                    var data = [];
                    for (var x = 0; x < self.listDatosAdeudos.length; x++) {
                        //if (x === 0) {
                        //    data.push({
                        //        "country": "",
                        //        "value": 0
                        //    });
                        //}
                        data.push({
                            "Periodo": self.listDatosAdeudos[x].Nombre,
                            "Por_Cobrar": self.listDatosAdeudos[x].Por_Cobrar,
                            "namerica": self.listDatosAdeudos[x].Vencido


                        });
                    }

                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, {});
                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        categoryField: "Periodo",
                        renderer: xRenderer,
                        tooltip: am5.Tooltip.new(root, {})
                    }));

                    xRenderer.grid.template.setAll({
                        location: 1
                    })

                    xAxis.data.setAll(data);

                    var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                        min: 0,
                        max: 100,
                        numberFormat: "#'%'",
                        strictMinMax: true,
                        calculateTotals: true,
                        renderer: am5xy.AxisRendererY.new(root, {
                            strokeOpacity: 0.1
                        })
                    }));


                    // Add legend
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
                    var legend = chart.children.push(am5.Legend.new(root, {
                        centerX: am5.p50,
                        x: am5.p50
                    }));


                    // Add series
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
                    function makeSeries(name, fieldName) {
                        var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                            name: name,
                            stacked: true,
                            xAxis: xAxis,
                            yAxis: yAxis,
                            valueYField: fieldName,
                            valueYShow: "valueYTotalPercent",
                            categoryXField: "Periodo"
                        }));

                        series.columns.template.setAll({
                            tooltipText: "{name}, {categoryX}:{valueYTotalPercent.formatNumber('#.#')}%",
                            tooltipY: am5.percent(10)
                        });
                        series.data.setAll(data);

                        // Make stuff animate on load
                        // https://www.amcharts.com/docs/v5/concepts/animations/
                        series.appear();

                        series.bullets.push(function () {
                            return am5.Bullet.new(root, {
                                sprite: am5.Label.new(root, {
                                    text: "{valueYTotalPercent.formatNumber('#.#')}%",
                                    fill: root.interfaceColors.get("alternativeText"),
                                    centerY: am5.p50,
                                    centerX: am5.p50,
                                    populateText: true
                                })
                            });
                        });

                        legend.data.push(series);
                    }

                    makeSeries("Por_Cobrar", "Por_Cobrar");
                    makeSeries("Vencido", "Vencido");


                    // Make stuff animate on load
                    // https://www.amcharts.com/docs/v5/concepts/animations/
                    chart.appear(1000, 100);

                }); // end am5.ready()
                break;
            case "notgp":
                //$("#chartdiv").empty();
                document.getElementById("tituloChart").innerHTML = "Error al recuperar los datos, intentar más tarde."
                //$("#precarga").hide();
                break;
            default:
                break;
        }
    });
};

function Grafica11() {

    // Create root element
    // https://www.amcharts.com/docs/v5/getting-started/#Root_element
    root.container.children.clear();


    // Set themes
    // https://www.amcharts.com/docs/v5/concepts/themes/
    root.setThemes([
        am5themes_Animated.new(root)
    ]);


    // Create chart
    // https://www.amcharts.com/docs/v5/charts/xy-chart/
    var chart = root.container.children.push(am5xy.XYChart.new(root, {
        panX: true,
        panY: true,
        wheelX: "panX",
        wheelY: "zoomX",
        pinchZoomX: true
    }));

    // Add cursor
    // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
    var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
    cursor.lineY.set("visible", false);


    // Create axes
    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
    var xRenderer = am5xy.AxisRendererX.new(root, { minGridDistance: 30 });
    xRenderer.labels.template.setAll({
        rotation: -90,
        centerY: am5.p50,
        centerX: am5.p100,
        paddingRight: 15
    });

    xRenderer.grid.template.setAll({
        location: 1
    })

    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
        maxDeviation: 0.3,
        categoryField: "country",
        renderer: xRenderer,
        tooltip: am5.Tooltip.new(root, {})
    }));

    var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
        maxDeviation: 0.3,
        renderer: am5xy.AxisRendererY.new(root, {
            strokeOpacity: 0.1
        })
    }));


    // Create series
    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
    var series = chart.series.push(am5xy.ColumnSeries.new(root, {
        name: "Series 1",
        xAxis: xAxis,
        yAxis: yAxis,
        valueYField: "value",
        sequencedInterpolation: true,
        categoryXField: "country",
        tooltip: am5.Tooltip.new(root, {
            labelText: "{valueY}"
        })
    }));

    series.columns.template.setAll({ cornerRadiusTL: 5, cornerRadiusTR: 5, strokeOpacity: 0 });
    series.columns.template.adapters.add("fill", function (fill, target) {
        return chart.get("colors").getIndex(series.columns.indexOf(target));
    });

    series.columns.template.adapters.add("stroke", function (stroke, target) {
        return chart.get("colors").getIndex(series.columns.indexOf(target));
    });


    // Set data
    var data = [{
        country: "TI",
        value: 0
    }, {
        country: "LOMAS VERDES",
        value: 76.32
    }, {
        country: "San Rafael",
        value: 70
    }];

    xAxis.data.setAll(data);
    series.data.setAll(data);


    // Make stuff animate on load
    // https://www.amcharts.com/docs/v5/concepts/animations/
    series.appear(1000);
    chart.appear(1000, 100);

}; // end am5.ready()};
function Grafica23() {
    am5.ready(function () {

        // Create root element
        // https://www.amcharts.com/docs/v5/getting-started/#Root_element
        root.container.children.clear();


        // Set themes
        // https://www.amcharts.com/docs/v5/concepts/themes/
        root.setThemes([
            am5themes_Animated.new(root)
        ]);


        // Create chart
        // https://www.amcharts.com/docs/v5/charts/xy-chart/
        var chart = root.container.children.push(am5xy.XYChart.new(root, {
            panX: true,
            panY: true,
            wheelX: "panX",
            wheelY: "zoomX",
            pinchZoomX: true
        }));

        // Add cursor
        // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
        var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
        cursor.lineY.set("visible", false);


        // Create axes
        // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
        var xRenderer = am5xy.AxisRendererX.new(root, { minGridDistance: 30 });
        xRenderer.labels.template.setAll({
            rotation: -90,
            centerY: am5.p50,
            centerX: am5.p100,
            paddingRight: 15
        });

        xRenderer.grid.template.setAll({
            location: 1
        })

        var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
            maxDeviation: 0.3,
            categoryField: "country",
            renderer: xRenderer,
            tooltip: am5.Tooltip.new(root, {})
        }));

        var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            maxDeviation: 0.3,
            renderer: am5xy.AxisRendererY.new(root, {
                strokeOpacity: 0.1
            })
        }));


        // Create series
        // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
        var series = chart.series.push(am5xy.ColumnSeries.new(root, {
            name: "Series 1",
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            sequencedInterpolation: true,
            categoryXField: "country",
            tooltip: am5.Tooltip.new(root, {
                labelText: "{valueY}"
            })
        }));

        series.columns.template.setAll({ cornerRadiusTL: 5, cornerRadiusTR: 5, strokeOpacity: 0 });
        series.columns.template.adapters.add("fill", function (fill, target) {
            return chart.get("colors").getIndex(series.columns.indexOf(target));
        });

        series.columns.template.adapters.add("stroke", function (stroke, target) {
            return chart.get("colors").getIndex(series.columns.indexOf(target));
        });


        // Set data
        var data = [{
            country: "LICENCIATURA",
            value: 80
        }, {
            country: "ESPECIALIDAD",
            value: 20
        }, {
            country: "MAESTRIA",
            value: 50
        }, {
            country: "DOCTORADO",
            value: 25
        }];

        xAxis.data.setAll(data);
        series.data.setAll(data);


        // Make stuff animate on load
        // https://www.amcharts.com/docs/v5/concepts/animations/
        series.appear(1000);
        chart.appear(1000, 100);

    }); // end am5.ready()
};
function Grafica22() {
    am5.ready(function () {
        root.container.children.clear();
        // Create root element
        // https://www.amcharts.com/docs/v5/getting-started/#Root_element

        // Set themes
        // https://www.amcharts.com/docs/v5/concepts/themes/
        root.setThemes([
            am5themes_Animated.new(root)
        ]);

        // Create chart
        // https://www.amcharts.com/docs/v5/charts/xy-chart/
        var chart = root.container.children.push(
            am5xy.XYChart.new(root, {
                panX: false,
                panY: false,
                wheelX: "none",
                wheelY: "none"
            })
        );

        // Add cursor
        // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
        var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
        cursor.lineY.set("visible", false);

        // Create axes
        // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
        var xRenderer = am5xy.AxisRendererX.new(root, { minGridDistance: 30 });
        xRenderer.labels.template.setAll({ text: "{realName}" });

        var xAxis = chart.xAxes.push(
            am5xy.CategoryAxis.new(root, {
                maxDeviation: 0,
                categoryField: "category",
                renderer: xRenderer,
                tooltip: am5.Tooltip.new(root, {
                    labelText: "{realName}"
                })
            })
        );

        var yAxis = chart.yAxes.push(
            am5xy.ValueAxis.new(root, {
                maxDeviation: 0.3,
                renderer: am5xy.AxisRendererY.new(root, {})
            })
        );

        var yAxis2 = chart.yAxes.push(
            am5xy.ValueAxis.new(root, {
                maxDeviation: 0.3,
                syncWithAxis: yAxis,
                renderer: am5xy.AxisRendererY.new(root, { opposite: true })
            })
        );

        // Create series
        // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
        var series = chart.series.push(
            am5xy.ColumnSeries.new(root, {
                name: "Series 1",
                xAxis: xAxis,
                yAxis: yAxis2,
                valueYField: "value",
                sequencedInterpolation: true,
                categoryXField: "category",
                tooltip: am5.Tooltip.new(root, {
                    labelText: "{provider} {realName}: {valueY}"
                })
            })
        );

        series.columns.template.setAll({
            fillOpacity: 0.9,
            strokeOpacity: 0
        });
        series.columns.template.adapters.add("fill", (fill, target) => {
            return chart.get("colors").getIndex(series.columns.indexOf(target));
        });

        series.columns.template.adapters.add("stroke", (stroke, target) => {
            return chart.get("colors").getIndex(series.columns.indexOf(target));
        });

        var lineSeries = chart.series.push(
            am5xy.LineSeries.new(root, {
                name: "Series 2",
                xAxis: xAxis,
                yAxis: yAxis,
                valueYField: "quantity",
                sequencedInterpolation: true,
                stroke: chart.get("colors").getIndex(13),
                fill: chart.get("colors").getIndex(13),
                categoryXField: "category",
                tooltip: am5.Tooltip.new(root, {
                    labelText: "{valueY}"
                })
            })
        );

        lineSeries.strokes.template.set("strokeWidth", 2);

        lineSeries.bullets.push(function () {
            return am5.Bullet.new(root, {
                locationY: 1,
                locationX: undefined,
                sprite: am5.Circle.new(root, {
                    radius: 5,
                    fill: lineSeries.get("fill")
                })
            });
        });

        // when data validated, adjust location of data item based on count
        lineSeries.events.on("datavalidated", function () {
            am5.array.each(lineSeries.dataItems, function (dataItem) {
                // if count divides by two, location is 0 (on the grid)
                if (
                    dataItem.dataContext.count / 2 ==
                    Math.round(dataItem.dataContext.count / 2)
                ) {
                    dataItem.set("locationX", 0);
                }
                // otherwise location is 0.5 (middle)
                else {
                    dataItem.set("locationX", 0.5);
                }
            });
        });

        var chartData = [];

        // Set data
        var data = {
            "LICENCIATURA": {
                "Por_cobrar": 10000,
                "Vencido": 35
            },
            "ESPECIALIDAD": {
                "Por_cobrar": 1000,
                "Vencido": 350
            },
            "MAESTRIA": {
                "Por_cobrar": 90000,
                "Vencido": 500
            },
            "DOCTORADO": {
                "Por_cobrar": 99000,
                "Vencido": 780
            }
        };

        // process data ant prepare it for the chart
        for (var providerName in data) {
            var providerData = data[providerName];

            // add data of one provider to temp array
            var tempArray = [];
            var count = 0;
            // add items
            for (var itemName in providerData) {
                if (itemName != "quantity") {
                    count++;
                    // we generate unique category for each column (providerName + "_" + itemName) and store realName
                    tempArray.push({
                        category: providerName + "_" + itemName,
                        realName: itemName,
                        value: providerData[itemName],
                        provider: providerName
                    });
                }
            }
            // sort temp array
            tempArray.sort(function (a, b) {
                if (a.value > b.value) {
                    return 1;
                } else if (a.value < b.value) {
                    return -1;
                } else {
                    return 0;
                }
            });

            // add quantity and count to middle data item (line series uses it)
            var lineSeriesDataIndex = Math.floor(count / 2);
            tempArray[lineSeriesDataIndex].quantity = providerData.quantity;
            tempArray[lineSeriesDataIndex].count = count;
            // push to the final data
            am5.array.each(tempArray, function (item) {
                chartData.push(item);
            });

            // create range (the additional label at the bottom)

            var range = xAxis.makeDataItem({});
            xAxis.createAxisRange(range);

            range.set("category", tempArray[0].category);
            range.set("endCategory", tempArray[tempArray.length - 1].category);

            var label = range.get("label");

            label.setAll({
                text: tempArray[0].provider,
                dy: 30,
                fontWeight: "bold",
                tooltipText: tempArray[0].provider
            });

            var tick = range.get("tick");
            tick.setAll({ visible: true, strokeOpacity: 1, length: 50, location: 0 });

            var grid = range.get("grid");
            grid.setAll({ strokeOpacity: 1 });
        }

        // add range for the last grid
        var range = xAxis.makeDataItem({});
        xAxis.createAxisRange(range);
        range.set("category", chartData[chartData.length - 1].category);
        var tick = range.get("tick");
        tick.setAll({ visible: true, strokeOpacity: 1, length: 50, location: 1 });

        var grid = range.get("grid");
        grid.setAll({ strokeOpacity: 1, location: 1 });

        xAxis.data.setAll(chartData);
        series.data.setAll(chartData);
        lineSeries.data.setAll(chartData);

        // Make stuff animate on load
        // https://www.amcharts.com/docs/v5/concepts/animations/
        series.appear(1000);
        chart.appear(1000, 100);

    }); // end am5.ready()
};
function Grafica3() {
    am5.ready(function () {
        root.container.children.clear();


        // Create root element
        // https://www.amcharts.com/docs/v5/getting-started/#Root_element


        // Set themes
        // https://www.amcharts.com/docs/v5/concepts/themes/
        root.setThemes([
            am5themes_Animated.new(root)
        ]);


        // Create chart
        // https://www.amcharts.com/docs/v5/charts/xy-chart/
        var chart = root.container.children.push(am5xy.XYChart.new(root, {
            panX: false,
            panY: false,
            wheelX: "panX",
            wheelY: "zoomX",
            layout: root.verticalLayout
        }));



        //var data = [{
        //    "nivel": "LICENCIATURA",
        //    "por_cobrar": 87600.00,
        //    "vencido": 28680.00
        //}, {
        //    "nivel": "ESPECIALIDAD",
        //    "por_cobrar": 12000.00,
        //    "vencido": 4200.00
        //}, {
        //    "nivel": "MAESTRIA",
        //    "por_cobrar": 120000.00,
        //    "vencido": 60000
        //    }]

        var data = [];
        for (var x = 0; x < self.listDatosAdeudos.length; x++) {
            //if (x === 0) {
            //    data.push({
            //        "country": "",
            //        "value": 0
            //    });
            //}
            data.push({
                "nivel": self.listDatosAdeudos[x].Nombre,
                "por_cobrar": self.listDatosAdeudos[x].Por_Cobrar,
                "vencido": parseInt(self.listDatosAdeudos[x].Vencido)
            });
        }


        // Create axes
        // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
        var xRenderer = am5xy.AxisRendererX.new(root, {});
        var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
            categoryField: "nivel",
            renderer: xRenderer,
            tooltip: am5.Tooltip.new(root, {})
        }));

        xRenderer.grid.template.setAll({
            location: 1
        })

        xAxis.data.setAll(data);

        var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            min: 0,
            max: 100,
            numberFormat: "#'%'",
            strictMinMax: true,
            calculateTotals: true,
            renderer: am5xy.AxisRendererY.new(root, {
                strokeOpacity: 0.1
            })
        }));


        // Add legend
        // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
        var legend = chart.children.push(am5.Legend.new(root, {
            centerX: am5.p50,
            x: am5.p50
        }));


        // Add series
        // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
        function makeSeries(name, fieldName) {
            var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                name: name,
                stacked: true,
                xAxis: xAxis,
                yAxis: yAxis,
                valueYField: fieldName,
                valueYShow: "valueYTotalPercent",
                categoryXField: "nivel"
            }));

            series.columns.template.setAll({
                tooltipText: "{name}, {categoryX}:{valueYTotalPercent.formatNumber('#.#')}%",
                tooltipY: am5.percent(10)
            });
            series.data.setAll(data);

            // Make stuff animate on load
            // https://www.amcharts.com/docs/v5/concepts/animations/
            series.appear();

            series.bullets.push(function () {
                return am5.Bullet.new(root, {
                    sprite: am5.Label.new(root, {
                        text: "{valueYTotalPercent.formatNumber('#.#')}%",
                        fill: root.interfaceColors.get("alternativeText"),
                        centerY: am5.p50,
                        centerX: am5.p50,
                        populateText: true
                    })
                });
            });

            legend.data.push(series);
        }

        chart.get("colors").set("colors", [
            am5.color(0x68dc76),
            am5.color(0xd9d9d9)
        ]);

        makeSeries("Por Cobrar", "por_cobrar");
        makeSeries("Vencido", "vencido");

        // Make stuff animate on load
        // https://www.amcharts.com/docs/v5/concepts/animations/
        chart.appear(1000, 100);

    }); // end am5.ready()
};
function Grafica20() {
    am5.ready(function () {
        root.container.children.clear();


        // Create root element
        // https://www.amcharts.com/docs/v5/getting-started/#Root_element


        // Set themes
        // https://www.amcharts.com/docs/v5/concepts/themes/
        root.setThemes([
            am5themes_Animated.new(root)
        ]);


        // Create chart
        // https://www.amcharts.com/docs/v5/charts/xy-chart/
        var chart = root.container.children.push(am5xy.XYChart.new(root, {
            panX: false,
            panY: false,
            wheelX: "panX",
            wheelY: "zoomX",
            layout: root.verticalLayout
        }));


        // Add legend
        // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
        var legend = chart.children.push(am5.Legend.new(root, {
            centerX: am5.p50,
            x: am5.p50
        }))


        // Data
        var data = [{
            nivel: "LICENCIATURA",
            por_cobrar: 99600.00,
            vencido: 28680.00
        }, {
            nivel: "MAESTRIA",
            por_cobrar: 9960,
            vencido: 2868
        }, {
            nivel: "ESPECIALIDAD",
            por_cobrar: 30.1,
            vencido: 23.9
        }, {
            nivel: "DOCTORADO",
            por_cobrar: 29.5,
            vencido: 25.1
        }];


        // Create axes
        // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
        var yAxis = chart.yAxes.push(am5xy.CategoryAxis.new(root, {
            categoryField: "nivel",

            renderer: am5xy.AxisRendererY.new(root, {
                inversed: true,
                cellStartLocation: 0.1,
                cellEndLocation: 0.9
            })
        }));

        yAxis.data.setAll(data);

        var xAxis = chart.xAxes.push(am5xy.ValueAxis.new(root, {
            renderer: am5xy.AxisRendererX.new(root, {
                strokeOpacity: 0.1
            }),
            min: 0
        }));


        // Add series
        // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
        function createSeries(field, name) {
            var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                name: name,
                xAxis: xAxis,
                yAxis: yAxis,
                valueXField: field,
                categoryYField: "nivel",
                sequencedInterpolation: true,
                tooltip: am5.Tooltip.new(root, {
                    pointerOrientation: "horizontal",
                    labelText: "[bold]{name}[/]\n{categoryY}: {valueX}"
                })
            }));

            series.columns.template.setAll({
                height: am5.p100,
                strokeOpacity: 0
            });


            series.bullets.push(function () {
                return am5.Bullet.new(root, {
                    locationX: 1,
                    locationY: 0.5,
                    sprite: am5.Label.new(root, {
                        centerY: am5.p50,
                        text: "{valueX}",
                        populateText: true
                    })
                });
            });

            series.bullets.push(function () {
                return am5.Bullet.new(root, {
                    locationX: 1,
                    locationY: 0.5,
                    sprite: am5.Label.new(root, {
                        centerX: am5.p100,
                        centerY: am5.p50,
                        text: "{name}",
                        fill: am5.color(0xffffff),
                        populateText: true
                    })
                });
            });

            series.data.setAll(data);
            series.appear();

            return series;
        }

        createSeries("por_cobrar", "Por Cobrar");
        createSeries("vencido", "Vencido");


        // Add legend
        // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
        var legend = chart.children.push(am5.Legend.new(root, {
            centerX: am5.p50,
            x: am5.p50
        }));

        legend.data.setAll(chart.series.values);


        // Add cursor
        // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
        var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {
            behavior: "zoomY"
        }));
        cursor.lineY.set("forceHidden", true);
        cursor.lineX.set("forceHidden", true);


        // Make stuff animate on load
        // https://www.amcharts.com/docs/v5/concepts/animations/
        chart.appear(1000, 100);

    }); // end am5.ready()
};
function Grafica24() {
    am5.ready(function () {

        // Create root element
        // https://www.amcharts.com/docs/v5/getting-started/#Root_element
        root.container.children.clear();


        // Set themes
        // https://www.amcharts.com/docs/v5/concepts/themes/
        root.setThemes([
            am5themes_Animated.new(root)
        ]);


        // Create chart
        // https://www.amcharts.com/docs/v5/charts/xy-chart/
        var chart = root.container.children.push(am5xy.XYChart.new(root, {
            panX: false,
            panY: false,
            wheelX: "panX",
            wheelY: "zoomX",
            layout: root.verticalLayout
        }));


        // Add legend
        // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
        var legend = chart.children.push(am5.Legend.new(root, {
            centerX: am5.percent(50),
            x: am5.percent(50),
            layout: am5.GridLayout.new(root, {
                maxColumns: 3,
                fixedWidthGrid: true
            })
        }));


        var data = [{
            "year": "LICENCIATURA",
            "Por_Cobrar": 100000,
            "Vencido": 5000
        }, {
            "year": "ESPECIALIDAD",
            "Por_Cobrar": 50000,
            "Vencido": 1000
        }, {
            "year": "MAESTRIA",
            "Por_Cobrar": 40000,
            "Vencido": 10000
        }, {
            "year": "DOCTORADO",
            "Por_Cobrar": 100000,
            "Vencido": 10000
        }]


        // Create axes
        // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
        var xRenderer = am5xy.AxisRendererX.new(root, {
            cellStartLocation: 0.1,
            cellEndLocation: 0.9
        })

        var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
            categoryField: "year",
            renderer: xRenderer,
            tooltip: am5.Tooltip.new(root, {})
        }));

        xRenderer.grid.template.setAll({
            location: 1
        })

        xAxis.data.setAll(data);

        var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            renderer: am5xy.AxisRendererY.new(root, {
                strokeOpacity: 0.1
            })
        }));


        // Add series
        // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
        function makeSeries(name, fieldName) {
            var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                name: name,
                xAxis: xAxis,
                yAxis: yAxis,
                valueYField: fieldName,
                categoryXField: "year"
            }));

            series.columns.template.setAll({
                tooltipText: "{name}, {categoryX}:{valueY}",
                width: am5.percent(90),
                tooltipY: 0,
                strokeOpacity: 0
            });

            series.data.setAll(data);

            // Make stuff animate on load
            // https://www.amcharts.com/docs/v5/concepts/animations/
            series.appear();

            series.bullets.push(function () {
                return am5.Bullet.new(root, {
                    locationY: 0,
                    sprite: am5.Label.new(root, {
                        text: "{valueY}",
                        fill: root.interfaceColors.get("alternativeText"),
                        centerY: 0,
                        centerX: am5.p50,
                        populateText: true
                    })
                });
            });

            legend.data.push(series);
        }

        makeSeries("Por Cobrar", "Por_Cobrar");
        makeSeries("Vencido", "Vencido");


        // Make stuff animate on load
        // https://www.amcharts.com/docs/v5/concepts/animations/
        chart.appear(1000, 100);

    }); // end am5.ready()
};
function Grafica30() {
    am5.ready(function () {

        root.container.children.clear();

        document.getElementById('tituloChart').innerHTML = 'Saldo vencido por carrera';

        // Set themes
        // https://www.amcharts.com/docs/v5/concepts/themes/
        root.setThemes([
            am5themes_Animated.new(root)
        ]);


        // Create chart
        // https://www.amcharts.com/docs/v5/charts/xy-chart/
        var chart = root.container.children.push(am5xy.XYChart.new(root, {
            panX: true,
            panY: true,
            wheelX: "panX",
            wheelY: "zoomX",
            pinchZoomX: true
        }));



        // Add cursor
        // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
        var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
        cursor.lineY.set("visible", false);


        // Create axes
        // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
        var xRenderer = am5xy.AxisRendererX.new(root, { minGridDistance: 30 });
        xRenderer.labels.template.setAll({
            rotation: -90,
            centerY: am5.p50,
            centerX: am5.p100,
            paddingRight: 15
        });

        xRenderer.grid.template.setAll({
            location: 1
        })

        var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
            maxDeviation: 0.3,
            categoryField: "country",
            renderer: xRenderer,
            tooltip: am5.Tooltip.new(root, {})
        }));

        var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
            maxDeviation: 0.3,
            renderer: am5xy.AxisRendererY.new(root, {
                strokeOpacity: 0.1
            })
        }));


        // Create series
        // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
        var series = chart.series.push(am5xy.ColumnSeries.new(root, {
            name: "Series 1",
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            sequencedInterpolation: true,
            categoryXField: "country",
            tooltip: am5.Tooltip.new(root, {
                labelText: "{valueY}"
            })
        }));

        series.columns.template.setAll({ cornerRadiusTL: 5, cornerRadiusTR: 5, strokeOpacity: 0 });
        series.columns.template.adapters.add("fill", function (fill, target) {
            return chart.get("colors").getIndex(series.columns.indexOf(target));
        });

        series.columns.template.adapters.add("stroke", function (stroke, target) {
            return chart.get("colors").getIndex(series.columns.indexOf(target));
        });


        // Set data
        var data = [{
            country: "SISTEMAS",
            value: 30
        }, {
            country: "CONTABILIDAD",
            value: 40
        }, {
            country: "ADMINISTRACION",
            value: 50
        }, {
            country: "TURISMO",
            value: 25
        }];

        xAxis.data.setAll(data);
        series.data.setAll(data);


        // Make stuff animate on load
        // https://www.amcharts.com/docs/v5/concepts/animations/
        series.appear(1000);
        chart.appear(1000, 100);

    }); // end am5.ready()
}