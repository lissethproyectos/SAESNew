var graficasContext = {

    listDatosAdeudos: [],
    listDatosPronostico: [],
    listDatosPronosticoRI: [],
    ciclos: [],
    escuelas: [],
    listDatosBecas: [],
    listDatosCXC: [],
    ObtenerDatosGraficaAdeudos: function (TipoGrafica, Ciclo, Campus, Nivel, callBackResult) {
        var self = this;
        self.listDatosAdeudos.length = 0;
        $.ajax({
            type: "GET",
            cache: false,
            url: "http://148.222.48.34/PracticasApis/Dashboard1/Adeudos",
            data: { Tipo_Grafica: TipoGrafica, Periodo: Ciclo, Campus: Campus, Nivel: Nivel },
            success: function (resp) {
                if (resp.error === false) {
                    if (TipoGrafica == "3") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosAdeudos.push({
                                Nombre: resp.resultado[i].Clave, Porcentaje: resp.resultado[i].Porcentaje, Por_Cobrar: resp.resultado[i].Por_Cobrar, Clave: resp.resultado[i].Clave, Vencido: resp.resultado[i].Vencido
                            });
                        }
                    }
                    else {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosAdeudos.push({
                                Nombre: resp.resultado[i].Nombre, Porcentaje: resp.resultado[i].Porcentaje, Por_Cobrar: resp.resultado[i].Por_Cobrar, Clave: resp.resultado[i].Clave, Vencido: resp.resultado[i].Vencido
                            });
                        }
                    }
                    callBackResult({ ressult: 'tgp' });
                }
                else {
                    //$("#loading").hide();
                    callBackResult({ ressult: 'notgp', message: resp });
                }
            },
            error: function (ex) {
                if (callBackResult !== undefined) {
                    callBackResult({ ressult: 'notgp', message: ex.statusText });
                }
            }
        });
    },
    ObtenerDatosGraficaPronosticoIngreso: function (Tipo, Turno, Ciclo, Campus, Nivel, Programa, callBackResult) {
        var self = this;
        self.listDatosPronostico.length = 0;
        $.ajax({
            type: "GET",
            cache: false,
            url: "http://148.222.48.34/PracticasApis/Dashboard1/PronosticoIngreso",
            data: { Tipo: Tipo, Turno: Turno, Periodo: Ciclo, Campus: Campus, Nivel: Nivel, Programa: Programa },
            success: function (resp) {
                self.listDatosPronostico.length = 0;

                if (resp.error === false) {
                    if (Tipo == "GRAFICA_2") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronostico.push({
                                Campus: resp.resultado[i].Clave, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registrados, Clave: resp.resultado[i].Clave
                            });
                        }
                    }
                    else if (Tipo == "GRAFICA_3") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronostico.push({
                                Campus: resp.resultado[i].Campus, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registrados
                            });
                        }
                    }
                    else if (Tipo == "5") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronostico.push({
                                Turno: resp.resultado[i].Turno, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registrados
                            });
                        }
                    }
                    else {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronostico.push({
                                Campus: resp.resultado[i].Campus, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registrados, Clave: resp.resultado[i].Clave
                            });
                        }
                    }
                    callBackResult({ ressult: 'tgp' });
                }
                else {
                    //$("#loading").hide();
                    callBackResult({ ressult: 'notgp', message: resp.mensaje_error });
                }
            },
            error: function (ex) {
                if (callBackResult !== undefined) {
                    callBackResult({ ressult: 'notgp', message: ex.statusText });
                }
            }
        });
    },
    ObtenerDatosGraficaPronosticoReIngreso: function (TipoGrafica, Turno, Ciclo, Campus, Nivel, Programa, callBackResult) {
        var self = this;
        self.listDatosPronosticoRI.length = 0;
        $.ajax({
            type: "GET",
            cache: false,
            url: "http://148.222.48.34/PracticasApis/Dashboard1/PronosticoReIngreso",
            data: { Tipo: TipoGrafica, Turno: Turno, Periodo: Ciclo, Campus: Campus, Nivel: Nivel, Programa: Programa },
            success: function (resp) {
                if (resp.error === false) {
                    if (TipoGrafica == "GRAFICA_2") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronosticoRI.push({
                                Campus: resp.resultado[i].Nivel, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registros_RI, Clave: resp.resultado[i].Clave, Porcentaje: resp.resultado[i].Porcentaje
                            });
                        }
                    }
                    else if (TipoGrafica == "GRAFICA_3") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronosticoRI.push({
                                Campus: resp.resultado[i].Programa, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registros_RI, Clave: resp.resultado[i].Clave, Porcentaje: resp.resultado[i].Porcentaje
                            });
                        }
                    }
                    else {
                        self.listDatosPronosticoRI.length = 0;
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosPronosticoRI.push({
                                Campus: resp.resultado[i].Campus, Pronostico: resp.resultado[i].Pronostico_Total, Registrados: resp.resultado[i].Registros_RI, Porcentaje: resp.resultado[i].Porcentaje
                            });
                        }
                    }
                    callBackResult({ ressult: 'tgp' });
                }
                else {
                    //$("#loading").hide();
                    callBackResult({ ressult: 'notgp', message: resp.mensaje_error });
                }
            },
            error: function (ex) {
                if (callBackResult !== undefined) {
                    callBackResult({ ressult: 'notgp', message: ex.statusText });
                }
            }
        });
    },
    ObtenerDatosGraficaBecas: function (TipoGrafica, Clasificacion, Ciclo, Campus, Nivel, Programa, callBackResult) {
        var self = this;
        self.listDatosBecas.length = 0;
        $.ajax({
            type: "GET",
            cache: false,
            url: "http://148.222.48.34/PracticasApis/Dashboard1/Becas",
            data: { Tipo: TipoGrafica, Clasificacion: Clasificacion, Periodo: Ciclo, Campus: Campus, Nivel: Nivel, Programa: Programa },
            success: function (resp) {
                if (resp.error === false) {
                    if (TipoGrafica == "GRAFICA_3") {
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosBecas.push({
                                Clave: resp.resultado[i].Clave, Total_Becas: resp.resultado[i].Total_Becas, Programa: resp.resultado[i].Registros_RI, Clave: resp.resultado[i].Clave, Porcentaje: resp.resultado[i].Porcentaje
                            });
                        }
                    }
                    else {
                        self.listDatosBecas.length = 0;
                        for (var i = 0; i < resp.resultado.length; i++) {
                            self.listDatosBecas.push({
                                Clave: resp.resultado[i].Clave, Total_Becas: resp.resultado[i].Total_Becas
                            });
                        }
                    }
                    callBackResult({ ressult: 'tgp' });
                }
                else {
                    //$("#loading").hide();
                    callBackResult({ ressult: 'notgp', message: resp.mensaje_error });
                }
            },
            error: function (ex) {
                if (callBackResult !== undefined) {
                    callBackResult({ ressult: 'notgp', message: ex.statusText });
                }
            }
        });
    },
    ObtenerDatosGraficaCXC: function (TipoGrafica, Tipo_Pago, Ciclo, Campus, Nivel, Programa, callBackResult) {
        var self = this;
        self.listDatosCXC.length = 0;
        $.ajax({
            type: "GET",
            cache: false,
            url: "http://148.222.48.34/PracticasApis/Dashboard1/CuentasPorCobrar",
            data: { Tipo: TipoGrafica, Tipo_Pago: Tipo_Pago, Periodo: Ciclo, Campus: Campus, Nivel: Nivel, Programa: Programa },
            success: function (resp) {
                if (resp.error === false) {
                    self.listDatosCXC.length = 0;
                    for (var i = 0; i < resp.resultado.length; i++) {
                        self.listDatosCXC.push({
                            Clave: resp.resultado[i].Clave, Clave2: resp.resultado[i].Clave2, CXC_Neto: resp.resultado[i].Cta_por_Cobrar, Falta_por_Cobrar: resp.resultado[i].Falta_por_Cobrar, Pagado: resp.resultado[i].Pagado, Campus: resp.resultado[i].Desc_Clave
                        });
                    }
                    callBackResult({ ressult: 'tgp' });
                }
                else {
                    //$("#loading").hide();
                    callBackResult({ ressult: 'notgp', message: resp.mensaje_error });
                }
            },
            error: function (ex) {
                if (callBackResult !== undefined) {
                    callBackResult({ ressult: 'notgp', message: ex.statusText });
                }
            }
        });
    },

};