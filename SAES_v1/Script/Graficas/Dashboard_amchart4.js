function Inicializar() {
    $("#precarga").show();
};
function GraficaPronosticoIngreso(Tipo, Turno, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    $("#chartdiv").empty();
    document.getElementById("tituloChart2").innerHTML = "";
    graficasContext.ObtenerDatosGraficaPronosticoIngreso(Tipo, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        $("#chartdiv2").empty();

        //if (Tipo === "GRAFICA_1")
        //    $("#chartdiv2").show();
        //else
        //    $("#chartdiv2").hide();

        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronostico = graficasContext.listDatosPronostico;
                am4core.ready(function () {
                    var chart = am4core.create("chartdiv", am4charts.XYChart);
                    // Themes begin
                    am4core.useTheme(am4themes_animated);
                    // Themes end

                    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

                    var datos = [];
                    for (var x = 0; x < self.listDatosPronostico.length; x++) {
                        datos.push({
                            "category": self.listDatosPronostico[x].Campus,
                            "value1": self.listDatosPronostico[x].Pronostico - self.listDatosPronostico[x].Registrados,
                            "value2": self.listDatosPronostico[x].Registrados,
                            "value3": self.listDatosPronostico[x].Pronostico,
                            "value4": self.listDatosPronostico[x].Clave


                        });

                    };

                    chart.data = datos;


                    chart.colors.step = 2;
                    chart.padding(30, 30, 10, 30);
                    chart.legend = new am4charts.Legend();


                    chart.colors.list = [
                        am4core.color("#e54454"),
                        am4core.color("#17a2b8")
                    ];

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
                    series1.name = "Pronóstico";
                    series1.dataFields.categoryX = "category";
                    series1.dataFields.valueY = "value1";
                    series1.dataFields.valueYShow = "totalPercent";
                    series1.dataItems.template.locations.categoryX = 0.5;
                    series1.stacked = true;
                    series1.tooltip.pointerOrientation = "vertical";

                    //chart.get("colors").set("colors", [
                    //    am5.color(0x68dc76),
                    //    am5.color(0xd9d9d9)
                    //]);



                    var bullet1 = series1.bullets.push(new am4charts.LabelBullet());
                    bullet1.interactionsEnabled = false;
                    bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}% (Pronóstico: {value3})";
                    bullet1.label.fill = am4core.color("#ffffff");
                    bullet1.locationY = 0.5;






                    var series2 = chart.series.push(new am4charts.ColumnSeries());
                    series2.columns.template.width = am4core.percent(80);
                    series2.columns.template.tooltipText =
                        "{name}: {valueY.totalPercent.formatNumber('#.00')}%";
                    series2.columns.template.events.on("hit", function (ev) {
                        var cve_campus = "";
                        cve_campus = ev.target._dataItem.dataContext["value4"];
                        var desc_campus = ev.target._dataItem.dataContext["category"];
                        document.getElementById("tituloChart2").innerHTML = "Estatus de NI del campus " + desc_campus;
                        var periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;// $('#<%= ddl_periodo.ClientID %>').val();
                        var turno = document.getElementById("ContentPlaceHolder1_ddl_turno").value;// $('#<%= ddl_periodo.ClientID %>').val();
                        var nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;

                        if (Tipo === 'GRAFICA_2') {
                            cve_campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
                            ObtenerGraficaPie(5, turno, periodo, cve_campus, '', '');
                        }
                        else {
                            ObtenerGraficaPie(5, turno, periodo, cve_campus, '', '');
                        }


                    });
                    series2.name = "Registros NI";
                    series2.dataFields.categoryX = "category";
                    series2.dataFields.valueY = "value2";
                    series2.dataFields.valueYShow = "totalPercent";
                    series2.dataItems.template.locations.categoryX = 0.5;
                    series2.stacked = true;
                    series2.tooltip.pointerOrientation = "vertical";

                    var bullet2 = series2.bullets.push(new am4charts.LabelBullet());
                    bullet2.interactionsEnabled = false;
                    bullet2.label.text = "{valueY.totalPercent.formatNumber('#.00')}% (Registros de NI: {value2} )";
                    bullet2.locationY = 0.5;
                    bullet2.label.fill = am4core.color("#ffffff");



                    /* if (Tipo === "GRAFICA_1") {*/
                    var periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
                    var turno = document.getElementById("ContentPlaceHolder1_ddl_turno").value;
                    var nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
                    var campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;

                    if (Tipo === "GRAFICA_1") {
                        document.getElementById("tituloChart2").innerHTML = "Estatus de NI del campus " + self.listDatosPronostico[0].Campus;
                        ObtenerGraficaPie(5, turno, periodo, self.listDatosPronostico[0].Clave, "0", "");
                    }
                    else if (Tipo === "GRAFICA_2") {
                        document.getElementById("tituloChart2").innerHTML = "Estatus de NI del nivel " + self.listDatosPronostico[0].Campus;
                        ObtenerGraficaPie(5, turno, periodo, campus, "", "");
                    }
                    else if (Tipo === "GRAFICA_3") {
                        document.getElementById("tituloChart2").innerHTML = "Estatus de NI del nivel " + self.listDatosPronostico[0].Campus;
                        ObtenerGraficaPie(5, turno, periodo, campus, nivel, "");
                    }
                        //                        ObtenerGraficaPie(5, turno, periodo, campus, self.listDatosPronostico[0].Clave_Nivel, "");

                    else if (Tipo === "GRAFICA_4") {
                        document.getElementById("tituloChart2").innerHTML = "Estatus de NI de la carrera " + self.listDatosPronostico[0].Campus;
                        ObtenerGraficaPie(5, turno, periodo, campus, nivel, self.listDatosPronostico[0].Campus);
                    }
                    //}
                    //else {
                    //    $("#tituloChart2").empty();
                    //}

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
var ObtenerGraficaPie = (Tipo, Turno, Ciclo, Campus, Nivel, Programa) => {
    $("#precarga").show();
    $("#chartdiv2").empty();
    graficasContext.ObtenerDatosGraficaPronosticoIngreso(Tipo, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                //document.getElementById("tituloChart2").innerHTML = "Aspirantes requeridos por turno";
                self.listDatosPronostico = graficasContext.listDatosPronostico;

                am4core.ready(function () {

                    // Themes begin
                    am4core.useTheme(am4themes_animated);
                    // Themes end

                    // Create chart instance
                    var chart = am4core.create("chartdiv2", am4charts.PieChart);



                    var datos = [];
                    chart.legend = new am4charts.Legend();
                    chart.legend.position = "bottom";
                    chart.legend.scrollable = true;


                    for (var x = 0; x < self.listDatosPronostico.length; x++) {
                        datos.push({
                            "Turno": self.listDatosPronostico[x].Turno,
                            "Pronostico": self.listDatosPronostico[x].Pronostico

                        });
                    };

                    chart.data = datos;



                    // Add and configure Series
                    var pieSeries = chart.series.push(new am4charts.PieSeries());
                    pieSeries.dataFields.value = "Pronostico";
                    pieSeries.dataFields.category = "Turno";
                    pieSeries.slices.template.stroke = am4core.color("#fff");
                    pieSeries.slices.template.strokeOpacity = 1;
                    pieSeries.labels.template.radius = am4core.percent(-50);
                    pieSeries.ticks.template.disabled = true;
                    pieSeries.alignLabels = false;
                    pieSeries.colors.list = [
                        am4core.color("#1b5a9d"), //En proceso
                        am4core.color("#146D8B"), //Iniciada
                        am4core.color("#00FFFF"), //Terminada
                        am4core.color("#FF9671"),
                        am4core.color("#FFC75F"),
                        am4core.color("#F9F871"),
                    ];

                    // This creates initial animation
                    pieSeries.hiddenState.properties.opacity = 1;
                    pieSeries.hiddenState.properties.endAngle = -45;
                    pieSeries.hiddenState.properties.startAngle = -45;



                    chart.hiddenState.properties.radius = am4core.percent(0);


                });
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
}
function GraficaPronosticoReIngreso2(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaPronosticoReIngreso(TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronosticoRI = graficasContext.listDatosPronosticoRI;
                am4core.ready(function () {
                    var chart = am4core.create("chartdiv", am4charts.XYChart);

                    // Themes begin
                    am4core.useTheme(am4themes_animated);



                    chart.hiddenState.properties.opacity = 0; // this makes initial fade in effect

                    //chart.data = [
                    //    {
                    //        country: "USA",
                    //        visits: 3025
                    //    },
                    //    {
                    //        country: "China",
                    //        visits: 1882
                    //    },


                    var datos = [];
                    for (var x = 0; x < self.listDatosPronosticoRI.length; x++) {
                        datos.push({
                            "Clave": self.listDatosPronosticoRI[x].Campus,
                            "Porcentaje": parseInt(self.listDatosPronosticoRI[x].Porcentaje),
                            "Total": parseInt(self.listDatosPronosticoRI[x].Pronostico),
                            "Reingresados": parseInt(self.listDatosPronosticoRI[x].Registrados)
                        });

                    };

                    chart.data = datos;

                    chart.padding(40, 40, 0, 0);
                    chart.maskBullets = false; // allow bullets to go out of plot area

                    var text = chart.plotContainer.createChild(am4core.Label);
                    text.y = 92;
                    text.x = am4core.percent(100);
                    text.horizontalCenter = "right";
                    text.zIndex = 100;
                    text.fillOpacity = 0.7;

                    // category axis
                    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
                    categoryAxis.dataFields.category = "Clave";
                    categoryAxis.renderer.grid.template.disabled = true;
                    categoryAxis.renderer.minGridDistance = 50;


                    // value axis
                    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
                    // we set fixed min/max and strictMinMax to true, as otherwise value axis will adjust min/max while dragging and it won't look smooth
                    valueAxis.strictMinMax = true;
                    valueAxis.min = 0;
                    valueAxis.max = 100;
                    valueAxis.renderer.minWidth = 60;

                    // series
                    var series = chart.series.push(new am4charts.ColumnSeries());
                    series.dataFields.categoryX = "Clave";
                    series.dataFields.valueY = "Porcentaje";
                    series.tooltip.pointerOrientation = "vertical";
                    series.tooltip.dy = -8;
                    series.sequencedInterpolation = true;
                    series.defaultState.interpolationDuration = 1500;
                    series.columns.template.strokeOpacity = 0;

                    // label bullet
                    var labelBullet = new am4charts.LabelBullet();
                    series.bullets.push(labelBullet);
                    labelBullet.label.text = "{valueY.value.formatNumber('#.')}" + " %";
                    labelBullet.strokeOpacity = 0;
                    labelBullet.stroke = am4core.color("#dadada");
                    labelBullet.dy = -20;


                    // series bullet
                    var bullet = series.bullets.create();
                    bullet.stroke = am4core.color("#ffffff");
                    bullet.strokeWidth = 3;
                    bullet.opacity = 1; // initially invisible
                    bullet.defaultState.properties.opacity = 1;
                    // resize cursor when over
                    bullet.cursorOverStyle = am4core.MouseCursorStyle.verticalResize;
                    bullet.draggable = true;

                    // create hover state
                    var hoverState = bullet.states.create("hover");
                    hoverState.properties.opacity = 1; // visible when hovered

                    // add circle sprite to bullet
                    var circle = bullet.createChild(am4core.Circle);
                    circle.radius = 8;

                    // while dragging
                    bullet.events.on("drag", event => {
                        handleDrag(event);
                    });

                    bullet.events.on("dragstop", event => {
                        handleDrag(event);
                        var dataItem = event.target.dataItem;
                        dataItem.column.isHover = false;
                        event.target.isHover = false;
                    });

                    function handleDrag(event) {
                        var dataItem = event.target.dataItem;
                        // convert coordinate to value
                        var value = valueAxis.yToValue(event.target.pixelY);
                        // set new value
                        dataItem.valueY = value;
                        // make column hover
                        dataItem.column.isHover = true;
                        // hide tooltip not to interrupt
                        dataItem.column.hideTooltip(0);
                        // make bullet hovered (as it might hide if mouse moves away)
                        event.target.isHover = true;
                    }

                    // column template
                    var columnTemplate = series.columns.template;
                    columnTemplate.column.cornerRadiusTopLeft = 8;
                    columnTemplate.column.cornerRadiusTopRight = 8;
                    columnTemplate.column.fillOpacity = 0.8;
                    columnTemplate.tooltipText = "Pronostico: " + "{Total}" + ", Reingresados:" + "{Reingresados}"; //"Total";
                    columnTemplate.tooltipY = 0; // otherwise will point to middle of the column


                    // hover state
                    var columnHoverState = columnTemplate.column.states.create("hover");
                    columnHoverState.properties.fillOpacity = 1;
                    // you can change any property on hover state and it will be animated
                    columnHoverState.properties.cornerRadiusTopLeft = 35;
                    columnHoverState.properties.cornerRadiusTopRight = 35;

                    // show bullet when hovered
                    columnTemplate.events.on("over", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);
                        itemBullet.isHover = true;
                    });

                    // hide bullet when mouse is out
                    columnTemplate.events.on("out", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);
                        itemBullet.isHover = false;
                    });

                    // start dragging bullet even if we hit on column not just a bullet, this will make it more friendly for touch devices
                    columnTemplate.events.on("down", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);
                        itemBullet.dragStart(event.pointer);
                    });

                    // when columns position changes, adjust minX/maxX of bullets so that we could only dragg vertically
                    columnTemplate.events.on("positionchanged", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);

                        var column = dataItem.column;
                        itemBullet.minX = column.pixelX + column.pixelWidth / 2;
                        itemBullet.maxX = itemBullet.minX;
                        itemBullet.minY = 0;
                        itemBullet.maxY = chart.seriesContainer.pixelHeight;
                    });

                    // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set
                    columnTemplate.adapter.add("fill", (fill, target) => {
                        return chart.colors.getIndex(target.dataItem.index).saturate(0.3);
                    });

                    bullet.adapter.add("fill", (fill, target) => {
                        return chart.colors.getIndex(target.dataItem.index).saturate(0.3);
                    });




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
function GraficaPronosticoReIngreso(Tipo, Turno, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    $("#chartdiv").empty();
    document.getElementById("tituloChart2").innerHTML = "";
    graficasContext.ObtenerDatosGraficaPronosticoReIngreso(Tipo, Turno, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosPronosticoRI = graficasContext.listDatosPronosticoRI;
                am4core.ready(function () {
                    var chart = am4core.create("chartdiv", am4charts.XYChart);
                    // Themes begin
                    am4core.useTheme(am4themes_animated);
                    // Themes end

                    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

                    var datos = [];
                    for (var x = 0; x < self.listDatosPronosticoRI.length; x++) {
                        datos.push({
                            "category": self.listDatosPronosticoRI[x].Campus,
                            "value1": self.listDatosPronosticoRI[x].Pronostico,
                            "value2": self.listDatosPronosticoRI[x].Registrados
                        });

                    };

                    chart.data = datos;


                    chart.colors.step = 2;
                    chart.padding(30, 30, 10, 30);
                    chart.legend = new am4charts.Legend();


                    chart.colors.list = [
                        am4core.color("#e54454"),
                        am4core.color("#17a2b8")
                    ];

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
                        "{name}: {value1} ({valueY.totalPercent.formatNumber('#.00')}%)";

                    series1.name = "Pronóstico";
                    series1.dataFields.categoryX = "category";
                    series1.dataFields.valueY = "value1";
                    series1.dataFields.valueYShow = "totalPercent";
                    series1.dataItems.template.locations.categoryX = 0.5;
                    series1.stacked = true;
                    series1.tooltip.pointerOrientation = "vertical";


                    var bullet1 = series1.bullets.push(new am4charts.LabelBullet());
                    bullet1.interactionsEnabled = false;
                    bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value} Pronóstico)";
                    bullet1.label.fill = am4core.color("#ffffff");
                    bullet1.locationY = 0.5;



                    var series2 = chart.series.push(new am4charts.ColumnSeries());
                    series2.columns.template.width = am4core.percent(80);
                    series2.columns.template.tooltipText =
                        "{name}: {value2} ({valueY.totalPercent.formatNumber('#.00')}%)";
                    series2.name = "Registros RI";
                    series2.dataFields.categoryX = "category";
                    series2.dataFields.valueY = "value2";
                    series2.dataFields.valueYShow = "totalPercent";
                    series2.dataItems.template.locations.categoryX = 0.5;
                    series2.stacked = true;
                    series2.tooltip.pointerOrientation = "vertical";

                    var bullet2 = series2.bullets.push(new am4charts.LabelBullet());
                    bullet2.interactionsEnabled = false;
                    bullet2.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value2} Registros RI)";
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
function GraficaBecas(TipoGrafica, Clasificacion, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    graficasContext.ObtenerDatosGraficaBecas(TipoGrafica, Clasificacion, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosBecas = graficasContext.listDatosBecas;
                am4core.ready(function () {
                    var chart = am4core.create("chartdiv", am4charts.XYChart);

                    // Themes begin
                    am4core.useTheme(am4themes_animated);



                    chart.hiddenState.properties.opacity = 0; // this makes initial fade in effect

                    //chart.data = [
                    //    {
                    //        country: "USA",
                    //        visits: 3025
                    //    },
                    //    {
                    //        country: "China",
                    //        visits: 1882
                    //    },


                    var datos = [];
                    for (var x = 0; x < self.listDatosBecas.length; x++) {
                        datos.push({
                            "Clave": self.listDatosBecas[x].Clave,
                            "Total_Becas": parseInt(self.listDatosBecas[x].Total_Becas)
                        });

                    };

                    chart.data = datos;

                    chart.padding(40, 40, 0, 0);
                    chart.maskBullets = false; // allow bullets to go out of plot area

                    var text = chart.plotContainer.createChild(am4core.Label);
                    text.y = 92;
                    text.x = am4core.percent(100);
                    text.horizontalCenter = "right";
                    text.zIndex = 100;
                    text.fillOpacity = 0.7;

                    // category axis
                    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
                    categoryAxis.dataFields.category = "Clave";
                    categoryAxis.renderer.grid.template.disabled = true;
                    categoryAxis.renderer.minGridDistance = 50;


                    // value axis
                    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
                    // we set fixed min/max and strictMinMax to true, as otherwise value axis will adjust min/max while dragging and it won't look smooth
                    valueAxis.strictMinMax = true;
                    valueAxis.min = 0;
                    //valueAxis.max = 100;
                    valueAxis.renderer.minWidth = 60;

                    // series
                    var series = chart.series.push(new am4charts.ColumnSeries());
                    series.dataFields.categoryX = "Clave";
                    series.dataFields.valueY = "Total_Becas";
                    series.tooltip.pointerOrientation = "vertical";
                    series.tooltip.dy = -8;
                    series.sequencedInterpolation = true;
                    series.defaultState.interpolationDuration = 1500;
                    series.columns.template.strokeOpacity = 0;

                    // label bullet
                    var labelBullet = new am4charts.LabelBullet();
                    series.bullets.push(labelBullet);
                    labelBullet.label.text = "$" + "{valueY.value.formatNumber('#.')}";
                    labelBullet.strokeOpacity = 0;
                    labelBullet.stroke = am4core.color("#dadada");
                    labelBullet.dy = -20;


                    // series bullet
                    var bullet = series.bullets.create();
                    bullet.stroke = am4core.color("#ffffff");
                    bullet.strokeWidth = 3;
                    bullet.opacity = 1; // initially invisible
                    bullet.defaultState.properties.opacity = 1;
                    // resize cursor when over
                    bullet.cursorOverStyle = am4core.MouseCursorStyle.verticalResize;
                    bullet.draggable = true;

                    // create hover state
                    var hoverState = bullet.states.create("hover");
                    hoverState.properties.opacity = 1; // visible when hovered

                    // add circle sprite to bullet
                    var circle = bullet.createChild(am4core.Circle);
                    circle.radius = 8;

                    // while dragging
                    bullet.events.on("drag", event => {
                        handleDrag(event);
                    });

                    bullet.events.on("dragstop", event => {
                        handleDrag(event);
                        var dataItem = event.target.dataItem;
                        dataItem.column.isHover = false;
                        event.target.isHover = false;
                    });

                    function handleDrag(event) {
                        var dataItem = event.target.dataItem;
                        // convert coordinate to value
                        var value = valueAxis.yToValue(event.target.pixelY);
                        // set new value
                        dataItem.valueY = value;
                        // make column hover
                        dataItem.column.isHover = true;
                        // hide tooltip not to interrupt
                        dataItem.column.hideTooltip(0);
                        // make bullet hovered (as it might hide if mouse moves away)
                        event.target.isHover = true;
                    }

                    // column template
                    var columnTemplate = series.columns.template;
                    columnTemplate.column.cornerRadiusTopLeft = 8;
                    columnTemplate.column.cornerRadiusTopRight = 8;
                    columnTemplate.column.fillOpacity = 0.8;
                    columnTemplate.tooltipText = "{Total_Becas}"; //"Total";
                    columnTemplate.tooltipY = 0; // otherwise will point to middle of the column


                    // hover state
                    var columnHoverState = columnTemplate.column.states.create("hover");
                    columnHoverState.properties.fillOpacity = 1;
                    // you can change any property on hover state and it will be animated
                    columnHoverState.properties.cornerRadiusTopLeft = 35;
                    columnHoverState.properties.cornerRadiusTopRight = 35;

                    // show bullet when hovered
                    columnTemplate.events.on("over", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);
                        itemBullet.isHover = true;
                    });

                    // hide bullet when mouse is out
                    columnTemplate.events.on("out", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);
                        itemBullet.isHover = false;
                    });

                    // start dragging bullet even if we hit on column not just a bullet, this will make it more friendly for touch devices
                    columnTemplate.events.on("down", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);
                        itemBullet.dragStart(event.pointer);
                    });

                    // when columns position changes, adjust minX/maxX of bullets so that we could only dragg vertically
                    columnTemplate.events.on("positionchanged", event => {
                        var dataItem = event.target.dataItem;
                        var itemBullet = dataItem.bullets.getKey(bullet.uid);

                        var column = dataItem.column;
                        itemBullet.minX = column.pixelX + column.pixelWidth / 2;
                        itemBullet.maxX = itemBullet.minX;
                        itemBullet.minY = 0;
                        itemBullet.maxY = chart.seriesContainer.pixelHeight;
                    });

                    // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set
                    columnTemplate.adapter.add("fill", (fill, target) => {
                        return chart.colors.getIndex(target.dataItem.index).saturate(0.3);
                    });

                    bullet.adapter.add("fill", (fill, target) => {
                        return chart.colors.getIndex(target.dataItem.index).saturate(0.3);
                    });




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
function GraficaCXC(TipoGrafica, TipoPago, Ciclo, Campus, Nivel, Programa) {
    $("#precarga").show();
    $("#chartdiv").empty();

    graficasContext.ObtenerDatosGraficaCXC(TipoGrafica, TipoPago, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;
        document.getElementById("tituloChart2").innerHTML = "";
        switch (resp.ressult) {
            case "tgp":
                document.getElementById("tituloChart").innerHTML = "";
                self.listDatosCXC = graficasContext.listDatosCXC;
                am4core.ready(function () {
                    var chart = am4core.create("chartdiv", am4charts.XYChart);
                    // Themes begin
                    am4core.useTheme(am4themes_animated);
                    // Themes end

                    chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

                    var datos = [];
                    for (var x = 0; x < self.listDatosCXC.length; x++) {
                        datos.push({
                            "category": self.listDatosCXC[x].Clave,
                            "value1": self.listDatosCXC[x].Falta_por_Cobrar,
                            "value2": self.listDatosCXC[x].Pagado,
                            "value3": self.listDatosCXC[x].CXC_Neto,
                            "value4": self.listDatosCXC[x].Campus
                        });

                    };

                    chart.data = datos;


                    chart.colors.step = 2;
                    chart.padding(30, 30, 10, 30);
                    chart.legend = new am4charts.Legend();


                    chart.colors.list = [
                        am4core.color("#e54454"),
                        am4core.color("#17a2b8")
                    ];

                    var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
                    categoryAxis.dataFields.category = "category";
                    categoryAxis.renderer.grid.template.location = 0;

                    var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
                    valueAxis.min = 0;
                    valueAxis.max = 100;
                    valueAxis.strictMinMax = true;
                    valueAxis.calculateTotals = true;




                    var series1 = chart.series.push(new am4charts.ColumnSeries());
                    series1.columns.template.width = am4core.percent(80);
                    series1.columns.template.tooltipText =
                        "{name}: {value3} (100%)";
                    series1.name = "Cuenta por Cobrar";
                    series1.dataFields.categoryX = "category";
                    series1.dataFields.valueY = "value1";
                    series1.dataFields.valueYShow = "totalPercent";
                    series1.dataItems.template.locations.categoryX = 0.5;
                    series1.stacked = true;
                    series1.tooltip.pointerOrientation = "vertical";
                    series1.columns.template.events.on("hit", function (ev) {
                        
                        //var Ciclo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;// $('#<%= ddl_periodo.ClientID %>').val();
                        var TipoPago = document.getElementById("ContentPlaceHolder1_ddl_tipo").value;// $('#<%= ddl_periodo.ClientID %>').val();
                        var Nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
                        var campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;

                        //ObtenerGraficaPieCXC("GRAFICA_PASTEL", TipoPago, Ciclo, cve_campus, Nivel, '');


                        if (TipoGrafica === "GRAFICA_1") {
                            var Cve_Campus = "";
                            Cve_Campus = ev.target._dataItem.dataContext["value4"];
                            document.getElementById("tituloChart2").innerHTML = "CXC Neto del campus " + ev.target._dataItem.dataContext["category"];

                            ObtenerGraficaPieCXC("GRAFICA_PASTEL1", TipoPago, Ciclo, Cve_Campus, Nivel, '');

                        }
                        else if (TipoGrafica === "GRAFICA_2") {
                            var campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
                            document.getElementById("tituloChart2").innerHTML = "CXC Neto del nivel " + ev.target._dataItem.dataContext["category"];


                            ObtenerGraficaPieCXC("GRAFICA_PASTEL2", TipoPago, Ciclo, campus, '', '');
                        }
                        else if (TipoGrafica === "GRAFICA_3") {
                            document.getElementById("tituloChart2").innerHTML = "CXC Neto del programa " + ev.target._dataItem.dataContext["category"];
                            ObtenerGraficaPieCXC("GRAFICA_PASTEL3", TipoPago, Ciclo, campus, nivel, ev.target._dataItem.dataContext["category"]);
                        }
                    });

                    var bullet1 = series1.bullets.push(new am4charts.LabelBullet());
                    bullet1.interactionsEnabled = false;
                    bullet1.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value} Cuenta por Cobrar)";


                    bullet1.label.fill = am4core.color("#ffffff");
                    bullet1.locationY = 0.5;



                    var series2 = chart.series.push(new am4charts.ColumnSeries());
                    series2.columns.template.width = am4core.percent(80);
                    series2.columns.template.tooltipText =
                        "{name}: {value2} ({valueY.totalPercent.formatNumber('#.00')}%)";
                    series2.name = "Pagado";
                    series2.dataFields.categoryX = "category";
                    series2.dataFields.valueY = "value2";
                    series2.dataFields.valueYShow = "totalPercent";
                    series2.dataItems.template.locations.categoryX = 0.5;
                    series2.stacked = true;
                    series2.tooltip.pointerOrientation = "vertical";
                 


                    var bullet2 = series2.bullets.push(new am4charts.LabelBullet());
                    bullet2.interactionsEnabled = false;
                    bullet2.label.text = "{valueY.totalPercent.formatNumber('#.00')}% ({value2} Pagado)";
                    bullet2.locationY = 0.5;
                    bullet2.label.fill = am4core.color("#ffffff");


                    var TipoPago = document.getElementById("ContentPlaceHolder1_ddl_tipo").value;// $('#<%= ddl_periodo.ClientID %>').val();
                    var Ciclo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;// $('#<%= ddl_periodo.ClientID %>').val();
                    //var cve_campus = "{value4}";
                    var periodo = document.getElementById("ContentPlaceHolder1_ddl_periodo").value;
                    var nivel = document.getElementById("ContentPlaceHolder1_ddl_nivel").value;
                    var campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;

                    if (TipoGrafica === "GRAFICA_1") {
                        var Cve_Campus = self.listDatosCXC[0].Campus;
                        var Desc_Campus = self.listDatosCXC[0].Clave;

                        document.getElementById("tituloChart2").innerHTML = "CXC Neto del campus " + Desc_Campus;
                        ObtenerGraficaPieCXC("GRAFICA_PASTEL1", TipoPago, Ciclo, Cve_Campus, Nivel, '');
                    }
                    else if (TipoGrafica === "GRAFICA_2") {
                        var campus = document.getElementById("ContentPlaceHolder1_ddl_campus").value;
                        var Desc_Nivel = self.listDatosCXC[0].Clave;
                        document.getElementById("tituloChart2").innerHTML = "CXC Neto del nivel " + Desc_Nivel;
                        ObtenerGraficaPieCXC("GRAFICA_PASTEL2", TipoPago, Ciclo, campus, '', '');
                    }
                    else if (TipoGrafica === "GRAFICA_3") {
                        var Cve_Programa = self.listDatosCXC[0].Clave;
                        var Desc_Nivel = self.listDatosCXC[0].Clave;
                        document.getElementById("tituloChart2").innerHTML = "CXC Neto del programa " + Cve_Programa;
                        ObtenerGraficaPieCXC("GRAFICA_PASTEL3", TipoPago, Ciclo, campus, nivel, Cve_Programa);
                    }

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
var ObtenerGraficaPieCXC = (TipoGrafica, TipoPago, Ciclo, Campus, Nivel, Programa) => {
    //$("#precarga").show();
    $("#chartdiv2").empty();
    graficasContext.ObtenerDatosGraficaCXC(TipoGrafica, TipoPago, Ciclo, Campus, Nivel, Programa, function (resp) {
        Total = 0;
        ImporteTotal = 0;

        switch (resp.ressult) {
            case "tgp":
                self.listDatos = graficasContext.listDatosCXC;
                if (self.listDatos.length > 0) {
                    document.getElementById("chartdiv2").innerHTML = "";
                    am4core.ready(function () {

                        // Themes begin
                        am4core.useTheme(am4themes_animated);
                        // Themes end

                        // Create chart instance
                        var chart = am4core.create("chartdiv2", am4charts.PieChart);



                        var datos = [];
                        chart.legend = new am4charts.Legend();
                        chart.legend.position = "bottom";
                        chart.legend.scrollable = true;


                        for (var x = 0; x < self.listDatos.length; x++) {
                            datos.push({
                                "category": self.listDatosCXC[x].Clave,
                                "value1": self.listDatosCXC[x].Falta_por_Cobrar,
                                "value2": self.listDatosCXC[x].Pagado,
                                "value3": self.listDatosCXC[x].CXC_Neto

                            });
                        };


                        chart.data = datos;



                        // Add and configure Series
                        var pieSeries = chart.series.push(new am4charts.PieSeries());
                        pieSeries.dataFields.value = "value3";
                        pieSeries.dataFields.category = "category";
                        pieSeries.dataFields.name = "value3";

                        pieSeries.alignLabels = false;
                        pieSeries.labels.template.fontSize = 11;

                  
                   
                        //pieSeries.labels.template.bent = true;                        //pieSeries.colors.list = [
                        //    am4core.color("#1b5a9d"), //En proceso
                        //    am4core.color("#146D8B"), //Iniciada
                        //    am4core.color("#00FFFF"), //Terminada
                        //    am4core.color("#FF9671"),
                        //    am4core.color("#FFC75F"),
                        //    am4core.color("#F9F871"),
                        //];

                       

                        // This creates initial animation
                        pieSeries.hiddenState.properties.opacity = 1;
                        pieSeries.hiddenState.properties.endAngle = -45;
                        pieSeries.hiddenState.properties.startAngle = -45;



                        chart.hiddenState.properties.radius = am4core.percent(0);


                    });
                }
                else {
                    document.getElementById("chartdiv2").innerHTML = "Sin datos";
                }
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
}


