
var oTables = {
    "polling": [        
    ]
};

function generic_datatable_load(gridId, ExportFileName = "SAES_Export") {
    oTables.polling.push(
    $("#ContentPlaceHolder1_" + gridId).DataTable({
    /*$("#" + gridId).DataTable({*/
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
            {
                title: ExportFileName,
                className: 'btn-dark',
                extend: 'excel',
                text: 'Exportar Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                title: ExportFileName,
                className: 'btn-dark',
                extend: 'pdfHtml5',
                text: 'Exportar PDF',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            }
        ],
        stateSave: true
    })
    );
    
}

function generic_datatable_destroy(gridId) {
    $("#ContentPlaceHolder1_" + gridId).DataTable().destroy();
}
function generic_datatable_remove_class(gridId,classCss) {
    $("#ContentPlaceHolder1_" + gridId).removeClass(classCss)
}