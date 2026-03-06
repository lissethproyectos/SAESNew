function alert_error(message) {
    swal({
        allowEscapeKey: false,
        allowOutsideClick: false,
        type: 'error',
        title: 'Error',
        text: (message != undefined && message != "") ? message : "En consulta a Base de Datos.",
    })
}

function alert_warning(message) {
    swal({
        allowEscapeKey: false,
        allowOutsideClick: false,
        type: 'warning',
        title: 'Alerta',
        text: message,
    })
}

function alert_info(message) {
    swal({
        allowEscapeKey: false,
        allowOutsideClick: false,
        type: 'info',
        title: 'Información',
        text: message,
    })
}

function alert_success(message) {
    swal({
        allowEscapeKey: false,
        allowOutsideClick: false,
        type: 'success',
        title: 'Éxito',
        text: (message != undefined && message != "") ? message : "Se guardaron los datos exitosamente.",
    })
}