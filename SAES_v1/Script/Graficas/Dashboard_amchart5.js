var root = am5.Root.new("chartdiv");

function GraficaAdeudos(TipoGrafica, Ciclo, Campus, Nivel) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaAdeudos(TipoGrafica, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosAdeudos = graficasContext.listDatosAdeudos;
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
                        panX: true,
                        panY: true,
                        wheelX: "panX",
                        wheelY: "zoomX",
                        pinchZoomX: true
                    }));

                    //// Add cursor
                    //// https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
                    //var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
                    //cursor.lineY.set("visible", false);


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
                        categoryXField: "country"
                    }));

                    series.columns.template.setAll({
                        tooltipText: "Vencido: {value2.formatNumber('$#,###.00')}"
                    });



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
                        data.push({
                            "country": self.listDatosAdeudos[x].Nombre,
                            "value": parseInt(self.listDatosAdeudos[x].Porcentaje),
                            "value2": self.listDatosAdeudos[x].Vencido
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
                //document.getElementById("tituloChart").innerHTML = resp.message;
                document.getElementById("tituloChart").innerHTML = "Error al recuperar los datos, intentar más tarde."

                //$("#precarga").hide();
                break;
            default:
                break;
        };
        $("#precarga").hide();
    });
};
function GraficaPronosticoIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronostico = graficasContext.listDatosPronostico;
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
                    //    "year": "2021",
                    //    "europe": 2.5,
                    //    "namerica": 2.5
                    //}, {
                    //    "year": "2022",
                    //    "europe": 2.6,
                    //    "namerica": 2.7
                    //}, {
                    //    "year": "2023",
                    //    "europe": 2.8,
                    //    "namerica": 2.9
                    //}]


                    var data = [];
                    for (var x = 0; x < self.listDatosPronostico.length; x++) {
                        data.push({
                            "Campus": self.listDatosPronostico[x].Campus,
                            "Pronostico": self.listDatosPronostico[x].Pronostico,
                            "Registrados": self.listDatosPronostico[x].Registrados
                        });
                    }

                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, {});
                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        categoryField: "Campus",
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
                            categoryXField: "Campus"
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

                    makeSeries("Pronostico", "Pronostico");
                    makeSeries("Registrados", "Registrados");



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
        };
        $("#precarga").hide();

    });
};
function GraficaPronosticoIngreso2(TipoGrafica, Turno, Ciclo, Campus, Nivel) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronostico = graficasContext.listDatosPronostico;
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
                    var legend = chart.children.push(
                        am5.Legend.new(root, {
                            centerX: am5.p50,
                            x: am5.p50
                        })
                    );

                    var data = [];
                    for (var x = 0; x < self.listDatosPronostico.length; x++) {
                        data.push({
                            "Campus": self.listDatosPronostico[x].Campus,
                            "Pronostico": self.listDatosPronostico[x].Pronostico,
                            "Registrados": self.listDatosPronostico[x].Registrados
                        });
                    }
                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, {
                        cellStartLocation: 0.1,
                        cellEndLocation: 0.9
                    })

                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        categoryField: "Campus",
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
                            categoryXField: "Campus"
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

                    makeSeries("Pronostico", "Pronostico");
                    makeSeries("Registrados", "Registrados");

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
        };
        $("#precarga").hide();

    });
};
function GraficaPronosticoIngreso3(TipoGrafica, Turno, Ciclo, Campus, Nivel) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoIngreso(Turno, TipoGrafica, Ciclo, Campus, Nivel, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronostico = graficasContext.listDatosPronostico;
                am4core.ready(function () {

                    // Themes begin
                    am4core.useTheme(am4themes_animated);
                    // Themes end

                    var chart = am4core.create("chartdiv", am4charts.XYChart);
                    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in
                                       

                    var datos = [];
                    for (var x = 0; x < self.listDatosPronostico.length; x++) {
                        datos.push({
                            "category": self.listDatosPronostico[x].Campus,
                            "value1": self.listDatosPronostico[x].Pronostico,
                            "value2": self.listDatosPronostico[x].Registrados
                        });
                    };

                    chart.data = datos;


                    chart.colors.step = 2;
                    chart.padding(30, 30, 10, 30);
                    chart.legend = new am4charts.Legend();

                    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
                    categoryAxis.dataFields.category = "category";
                    categoryAxis.renderer.grid.template.location = 0;

                    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
                    valueAxis.min = 0;
                    valueAxis.max = 100;
                    valueAxis.strictMinMax = true;
                    valueAxis.calculateTotals = true;
                    valueAxis.renderer.minWidth = 50;


                    var series1 = chart.series.push(new am4charts.ColumnSeries());
                    series1.columns.template.width = am4core.percent(80);
                    series1.columns.template.tooltipText =
                        "{name}: {valueY.totalPercent.formatNumber('#.00')}%";
                    series1.name = "Pronostico";
                    series1.dataFields.categoryX = "category";
                    series1.dataFields.valueY = "value1";
                    series1.dataFields.valueYShow = "totalPercent";
                    series1.dataItems.template.locations.categoryX = 0.5;
                    series1.stacked = true;
                    series1.tooltip.pointerOrientation = "vertical";

                    var bullet1 = series1.bullets.push(new am4charts.LabelBullet());
                    bullet1.interactionsEnabled = false;
                    bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value1} requeridos)";
                    bullet1.label.fill = am4core.color("#ffffff");
                    bullet1.locationY = 0.5;

                    var series2 = chart.series.push(new am4charts.ColumnSeries());
                    series2.columns.template.width = am4core.percent(80);
                    series2.columns.template.tooltipText =
                        "{name}: {valueY.totalPercent.formatNumber('#.00')}%";
                    series2.name = "Registrados";
                    series2.dataFields.categoryX = "category";
                    series2.dataFields.valueY = "value2";
                    series2.dataFields.valueYShow = "totalPercent";
                    series2.dataItems.template.locations.categoryX = 0.5;
                    series2.stacked = true;
                    series2.tooltip.pointerOrientation = "vertical";

                    var bullet2 = series2.bullets.push(new am4charts.LabelBullet());
                    bullet2.interactionsEnabled = false;
                    bullet2.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value2} registrados)";
                    bullet2.locationY = 0.5;
                    bullet2.label.fill = am4core.color("#ffffff");




                }); // end am4core.ready()

                break;
            case "notgp":
                //$("#chartdiv").empty();
                document.getElementById("tituloChart").innerHTML = "Error al recuperar los datos, intentar más tarde."
                //$("#precarga").hide();
                break;
            default:
                break;
        };
        $("#precarga").hide();

    });
};
function GraficaPronosticoReIngreso2(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoReIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronosticoRI = graficasContext.listDatosPronosticoRI;
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
                            }),
                            min: 0,
                            max:100
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
                      


                        var data = [];
                        for (var x = 0; x < self.listDatosPronosticoRI.length; x++) {
                            data.push({
                                "country": self.listDatosPronosticoRI[x].Campus,
                                "value": parseInt(self.listDatosPronosticoRI[x].Porcentaje)
                            });
                        }
                        xAxis.data.setAll(data);
                        series.data.setAll(data);


                        // Make stuff animate on load
                        // https://www.amcharts.com/docs/v5/concepts/animations/
                        series.appear(1000);
                        chart.appear(1000, 100);

                        xAxis.data.setAll(data);
                        series.data.setAll(data);


                        // Make stuff animate on load
                        // https://www.amcharts.com/docs/v5/concepts/animations/
                        series.appear(1000);
                        chart.appear(1000, 100);

                    }); // end am5.read
                break;
            case "notgp":
                //$("#chartdiv").empty();
                document.getElementById("tituloChart").innerHTML = "Error al recuperar los datos, intentar más tarde."
                //$("#precarga").hide();
                break;
            default:
                break;
        };
        $("#precarga").hide();

    });
};
function GraficaPronosticoReIngreso3(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoReIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronosticoRI = graficasContext.listDatosPronosticoRI;
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
                    var legend = chart.children.push(
                        am5.Legend.new(root, {
                            centerX: am5.p50,
                            x: am5.p50
                        })
                    );

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
                    for (var x = 0; x < self.listDatosPronosticoRI.length; x++) {
                        data.push({
                            "Clave": self.listDatosPronosticoRI[x].Campus,
                            "Pronostico": parseInt(self.listDatosPronosticoRI[x].Pronostico),
                            "Reingreso": parseInt(self.listDatosPronosticoRI[x].Registrados)
                        });
                    }

                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, {
                        cellStartLocation: 0.1,
                        cellEndLocation: 0.9
                    })

                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        categoryField: "Clave",
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
                            categoryXField: "Clave"
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

                    makeSeries("Pronostico", "Pronostico");
                    makeSeries("Reingreso", "Reingreso");

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
        };
        $("#precarga").hide();

    });
};
function GraficaPronosticoReIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoReIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronosticoRI = graficasContext.listDatosPronosticoRI;
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
                    //    "year": "2021",
                    //    "europe": 2.5,
                    //    "namerica": 2.5
                    //}, {
                    //    "year": "2022",
                    //    "europe": 2.6,
                    //    "namerica": 2.7
                    //}, {
                    //    "year": "2023",
                    //    "europe": 2.8,
                    //    "namerica": 2.9
                    //}]


                    //var data = [];
                    //for (var x = 0; x < self.listDatosPronostico.length; x++) {
                    //    data.push({
                    //        "Campus": self.listDatosPronostico[x].Campus,
                    //        "Pronostico": self.listDatosPronostico[x].Pronostico,
                    //        "Registrados": self.listDatosPronostico[x].Registrados
                    //    });
                    //}


                    var data = [];
                    for (var x = 0; x < self.listDatosPronosticoRI.length; x++) {
                        data.push({
                            "Clave": self.listDatosPronosticoRI[x].Campus,
                            "Pronostico": parseInt(self.listDatosPronosticoRI[x].Pronostico),
                            "Reingreso": parseInt(self.listDatosPronosticoRI[x].Registrados)
                        });
                    }

                    // Create axes
                    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
                    var xRenderer = am5xy.AxisRendererX.new(root, {});
                    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                        categoryField: "Clave",
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
                            categoryXField: "Clave"
                        }));

                        series.columns.template.setAll({
                            tooltipText: "{name}, {categoryX}:{Pronostico}",
                            tooltipY: am5.percent(10)
                        });
                        series.data.setAll(data);





                        //var bullet1 = series1.bullets.push(new am4charts.LabelBullet());
                        //bullet1.interactionsEnabled = false;
                        //bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value3} Pronóstico)";
                        //bullet1.label.fill = am4core.color("#ffffff");
                        //bullet1.locationY = 0.5;


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

                    var xRenderer = am5xy.AxisRendererX.new(root, {});

                    var bullet1 = series1.bullets.push(new am5charts.LabelBullet());
                    bullet1.interactionsEnabled = false;
                    bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value3} Pronóstico)";
                    bullet1.label.fill = am4core.color("#ffffff");
                    bullet1.locationY = 0.5;

                    var bullet2 = series2.bullets.push(new am5charts.LabelBullet());
                    bullet2.interactionsEnabled = false;
                    bullet2.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value2} Registros NI)";
                    bullet2.locationY = 0.5;
                    bullet2.label.fill = am4core.color("#ffffff");



                    makeSeries("Pronostico", "Pronostico");
                    makeSeries("Reingreso", "Reingreso");



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
        };
        $("#precarga").hide();

    });
};