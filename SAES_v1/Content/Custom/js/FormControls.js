//-----  Función de agregar error  ------//
function FromCtrl_ErrorForm(idElementForm, textoAlerta) {
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
function FromCtrl_ValidadoForm(idElementForm) {
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

function SetDateControl(idElementForm) {
    $('#' + idElementForm).datepicker({
        uiLibrary: 'bootstrap4',
        locale: 'es-es',
        format: 'dd/mm/yyyy'
    });
}