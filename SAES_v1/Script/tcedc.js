$(document).ready(function () {
    //Initialize tooltips
    $('.nav-tabs > li a[title]').tooltip();

    //Wizard
    //$('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

    //    var $target = $(e.target);

    //    if ($target.parent().hasClass('disabled')) {
    //        return false;
    //    }
    //});

    //$(".next-step").click(function (e) {

    //    var $active = $('.wizard .nav-tabs li.active');
    //    $active.next().removeClass('disabled');
    //    nextTab($active);
    //    alert("ppaassoo");

    //});

    //$(".prev-step").click(function (e) {

    //    var $active = $('.wizard .nav-tabs li.active');
    //    prevTab($active);

    //});
});

//$("#ContentPlaceHolder1_paso2").click(function (e) {

//    var $active = $('.wizard .nav-tabs li.active');
//    $active.next().removeClass('disabled');
//    nextTab($active);
//    alert("ppaassoo");

//});


function Paso1() {

    $("#step3").removeClass("tab-pane active");
    $("#step3").addClass("tab-pane");
    $("#step2").removeClass("tab-pane active");
    $("#step2").addClass("tab-pane");
    $("#step1").removeClass("tab-pane");
    $("#step1").addClass("tab-pane active");


    $("#ContentPlaceHolder1_paso1").removeClass("disabled");
    $("#ContentPlaceHolder1_paso1").addClass("active");


    $("#ContentPlaceHolder1_paso2").removeClass("active");
    $("#ContentPlaceHolder1_paso2").addClass("disabled");

    $("#ContentPlaceHolder1_paso3").removeClass("active");
    $("#ContentPlaceHolder1_paso3").addClass("disabled");
}

function Paso2() {
    //alert("paso");
    $("#ContentPlaceHolder1_paso2").removeAttr("style");
    $("#ContentPlaceHolder1_paso2_enabled").removeAttr("style");
    $("#ContentPlaceHolder1_paso2_enabled").attr("style", "display: none;");


    $("#step3").removeClass("tab-pane active");
    $("#step3").addClass("tab-pane");
    $("#step1").removeClass("tab-pane active");
    $("#step1").addClass("tab-pane");
    $("#step2").addClass("tab-pane active");


    $("#ContentPlaceHolder1_paso2").removeClass("disabled");
    $("#ContentPlaceHolder1_paso2").addClass("active");

    $("#ContentPlaceHolder1_paso1").removeClass("active");
    $("#ContentPlaceHolder1_paso1").addClass("disabled");

    $("#ContentPlaceHolder1_paso3").removeClass("active");
    $("#ContentPlaceHolder1_paso3").addClass("disabled");
}

function Paso3() {
    //$("#ContentPlaceHolder1_paso3").removeClass("disabled");
    //$("#ContentPlaceHolder1_paso3").addClass("active");
    $("#ContentPlaceHolder1_paso3").removeAttr("style");
    $("#ContentPlaceHolder1_paso3_enabled").removeAttr("style");
    $("#ContentPlaceHolder1_paso3_enabled").attr("style", "display: none;");



    $("#step1").removeClass("tab-pane active");
    $("#step1").addClass("tab-pane");
    $("#step2").removeClass("tab-pane active");
    $("#step2").addClass("tab-pane");
    $("#step3").addClass("tab-pane active");

    $("#ContentPlaceHolder1_paso3").removeClass("disabled");
    $("#ContentPlaceHolder1_paso3").addClass("active");

    $("#ContentPlaceHolder1_paso1").removeClass("active");
    $("#ContentPlaceHolder1_paso1").addClass("disabled");

    $("#ContentPlaceHolder1_paso2").removeClass("active");
    $("#ContentPlaceHolder1_paso2").addClass("disabled");

}
function Pagar() {
    //alert("pagar");
    var $active = $('.wizard .nav-tabs li.active');
    $active.next().removeClass('disabled');
    nextTab($active);
}

$("#ContentPlaceHolder1_paso2").on("click", function () {
    alert("Handler for `click` called.");
});

 $(".next-step").click(function (e) {

        var $active = $('.wizard .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);
        alert("ppaassoo");

    });

function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}

function load_datatable() {
    let table_solicitudes = $("#ContentPlaceHolder1_GridSolicitudes").DataTable({
        language: {
            bProcessing: 'Procesando...',
            sLengthMenu: 'Mostrar _MENU_ registros',
            sZeroRecords: 'No se encontraron resultados',
            sEmptyTable: 'Ningún dato disponible en esta tabla',
            sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
            sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
            sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
            sInfoPostFix: '',
            sSearch: 'Buscar:',
            sUrl: '',
            sInfoThousands: '',
            sLoadingRecords: 'Cargando...',
            oPaginate: {
                sFirst: 'Primero',
                sLast: 'Último',
                sNext: 'Siguiente',
                sPrevious: 'Anterior'
            }
        },
        scrollResize: true,
        scrollY: '500px',
        scrollCollapse: true,
        order: [
            [0, "asc"]
        ],
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
        "autoWidth": true,
        dom: '<"top"if>rt<"bottom"lBp><"clear">',
        buttons: [

        ],
        stateSave: true
    });
}

function load_datatable_tedcu() {
    let table_solicitudes = $("#ContentPlaceHolder1_Gridtedcu").DataTable({
        language: {
            bProcessing: 'Procesando...',
            sLengthMenu: 'Mostrar _MENU_ registros',
            sZeroRecords: 'No se encontraron resultados',
            sEmptyTable: 'Ningún dato disponible en esta tabla',
            sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
            sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
            sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
            sInfoPostFix: '',
            sSearch: 'Buscar:',
            sUrl: '',
            sInfoThousands: '',
            sLoadingRecords: 'Cargando...',
            oPaginate: {
                sFirst: 'Primero',
                sLast: 'Último',
                sNext: 'Siguiente',
                sPrevious: 'Anterior'
            }
        },
        scrollResize: true,
        scrollY: '500px',
        scrollCollapse: true,
        order: [
            [0, "asc"]
        ],
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
        "autoWidth": true,
        dom: '<"top"if>rt<"bottom"lBp><"clear">',
        buttons: [

        ],
        stateSave: true
    });
}

function load_datatable_tcoco() {
    let table_solicitudes = $("#ContentPlaceHolder1_Gridtcoco").DataTable({
        language: {
            bProcessing: 'Procesando...',
            sLengthMenu: 'Mostrar _MENU_ registros',
            sZeroRecords: 'No se encontraron resultados',
            sEmptyTable: 'Ningún dato disponible en esta tabla',
            sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
            sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
            sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
            sInfoPostFix: '',
            sSearch: 'Buscar:',
            sUrl: '',
            sInfoThousands: '',
            sLoadingRecords: 'Cargando...',
            oPaginate: {
                sFirst: 'Primero',
                sLast: 'Último',
                sNext: 'Siguiente',
                sPrevious: 'Anterior'
            }
        },
        scrollResize: true,
        scrollY: '500px',
        scrollCollapse: true,
        order: [
            [0, "asc"]
        ],
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]],
        "autoWidth": true,
        dom: '<"top"if>rt<"bottom"lBp><"clear">',
        buttons: [

        ],
        stateSave: true
    });
}

function remove_class() {
    $('.selected_table').removeClass("selected_table")
}






    function error_consulta() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'error',
            html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Consulta Base de Datos</h2>'
        })
    }

    function error_transaccion() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'error',
            html: '<h2 class="swal2-title" id="swal2-title">ERROR -- Transacción de Base de Datos</h2>'
        })
    }

    function save() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'success',
            html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
        })
    }

    function update() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'success',
            html: '<h2 class="swal2-title" id="swal2-title">Se guardaron los datos exitosamente</h2>Favor de validar en el listado.'
        })
    }

    function error_saldo() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'error',
            html: '<h2 class="swal2-title" id="swal2-title">Importe no puede ser mayor a Saldo</h2>'
        })
    }

    function error_saldo_menor() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'error',
            html: '<h7 class="swal2-title" id="swal2-title">Importe no puede ser menor a Saldo, favor de hacer la modificación para que pueda realizar el pago</h7>'
        })
    }

    function error_numerico() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'success',
            html: '<h2 class="swal2-title" id="swal2-title">Importe ... debe ser un valor numérico !!!</h2>'
        })
    }

    function ImportePagar() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'error',
            html: '<h2 class="swal2-title" id="swal2-title">Se debe capturar Importe a pagar</h2>'
        })
    }

    function ImporteDiferente() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'error',
            html: '<h2 class="swal2-title" id="swal2-title">Total a Pagar / Total Medio Pago deben ser iguales!</h2>'
        })
    }

    function NoExist() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'success',
            html: '<h2 class="swal2-title" id="swal2-title">No existe Matrícula</h2>'
        })
    }

    function NoExistConcepto() {
        swal({
            allowEscapeKey: false,
            allowOutsideClick: false,
            type: 'success',
            html: '<h2 class="swal2-title" id="swal2-title">No existe Concepto Pago</h2>'
        })
    }

        //---- Concepto Pago ----//
    function validarConcepto(idEl) {
            const idElemento = idEl;
    let nombre = document.getElementById(idElemento).value;
    if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
        errorForm(idElemento, 'Ingresar Concepto Pago');
    return false;
            } else {
        validadoForm(idElemento);
            }
        }

    //---- Concepto Pago ----//
    function validarCantidad(idEl) {
            const idElemento = idEl;
    let nombre = document.getElementById(idElemento).value;
    if (nombre == null || nombre.length == 0 || /^\s+$/.test(nombre)) {
        errorForm(idElemento, 'Ingresar Importe Pago');
    return false;
            } else {
        validadoForm(idElemento);
            }
        }

    //-----  Función de agregar error  ------//
    function errorForm(idElementForm, textoAlerta) {
        let elemento = idElementForm;
    let textoInterno = textoAlerta;
    let elementoId = document.getElementById(elemento);
    elementoId.classList.add('error');
    elementoId.classList.remove('validado');
    elementoId.classList.remove('sinvalidar');
    errorId = 'error-' + String(elemento);
    let tieneError = document.getElementById(errorId);
    if (!tieneError) {
                const parrafo = document.createElement("p");
    const contParrafo = document.createTextNode(textoInterno);
    parrafo.appendChild(contParrafo);
    parrafo.classList.add('textoError');
    parrafo.id = 'error-' + String(elemento)
    //Depende de estructura de HTML
    elementoId.parentNode.appendChild(parrafo);
            }
        }

    //----- Función de remover error ------//
    function validadoForm(idElementForm) {
        let elemento = idElementForm;
    let elementoId = document.getElementById(elemento);
    elementoId.classList.remove('error');
    elementoId.classList.add('validado');
    elementoId.classList.remove('sinvalidar');
    errorId = 'error-' + String(elemento);
    let tieneError = document.getElementById(errorId);
    if (tieneError) {
        tieneError.remove();
            }
        }


    function Noexiste(idEl) {
            const idElemento = idEl;
    let nombre = document.getElementById(idElemento).value;

    errorForm(idElemento, 'No existe Clave');
    return false;
        }

    //---- Valida Campos Solicitud ----//
    function validar_campos_pago(e) {
        event.preventDefault(e);
    validarConcepto('ContentPlaceHolder1_txt_concepto');
    validarCantidad('ContentPlaceHolder1_TxtCantidad');
    return false;
        }

    function validarEntero_cantidad(idEl) {
            //intento convertir a entero.
            //si era un entero no le afecta, si no lo era lo intenta convertir
            const idElemento = idEl;
    valor = parseInt(idElemento)

    //Compruebo si es un valor numérico
    if (isNaN(valor)) {
        //entonces (no es numero) devuelvo el valor cadena vacia
        errorForm(idElemento, 'Monto fijo es valor numérico');
    return false
            } else {
        //En caso contrario (Si era un número) devuelvo el valor
        validadoForm(idElemento);
            }
        }

    function valida_cantidad(e) {
        validarEntero_cantidad('ContentPlaceHolder1_TxtCantidad');
        }

function error_gral(error) {
    swal({
        allowEscapeKey: false,
        allowOutsideClick: false,
        type: 'error',
        html: '<h2 class="swal2-title" id="swal2-title">ERROR --'+  error +'</h2>'
    })
}

function VerReciboPdf(campus, matricula, consecutivo) {
    //alert(consecutivo);
    //                //string ruta = "../Reports/VisualizadorCrystal.aspx?Tipo=RepRecibo&Valor1=" + Global.campus + "&Valor2=" + txt_matricula.Text + "&Valor3=55555";
    $('#precarga1').html('<div class="loading"><img src="https://sysweb.unach.mx/resources/imagenes/ajax_loader_gray_512.gif" alt="loading"  height="42" width="42"/><br/>Un momento, por favor...</div>');
    window.open('../Reports/VisualizadorCrystal.aspx?Tipo=RepRecibo&Valor1=' + campus + '&Valor2=' + matricula + '&Valor3=' + consecutivo, "miniContenedor");
    $('#precarga1').html('');
}