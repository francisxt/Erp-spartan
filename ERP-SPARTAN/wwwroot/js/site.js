// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(() => {
    $('.data-table').DataTable({
        pageLength: 50,
        "language": {
            "info": "Mostrando _START_ de _TOTAL_ resultados",
            "zeroRecords": "No encontramos resultados",
            "infoEmpty": "No hay datos",
            "search": "Buscar",
            "lengthMenu": "Mostrar _MENU_ resultados",
            "paginate": {
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        responsive: true
    });

    //Get all actives alerts
    fetch('/Alert/Quantity').then((result) => result.json()).then((response) => $('#qyt-alert').text(response));

});



$('#phoneNumber').mask('000-000-0000');
$('.decimal').mask("#,##0.00", { reverse: true });


/**
 * Muestra o oculta un elemento 
 * @param {any} elementId id del elemento
 */
const showOrHideElement = (elementId) => {
    var element = $(`#${elementId}`);
    if (element.is(':visible')) {
        element.hide();
    } else {
        element.show();
    }
};

