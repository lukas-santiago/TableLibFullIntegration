import jQuery from 'jquery'; window.$ = jQuery;

import * as bootstrap from 'bootstrap'
import ScrollReveal from 'scrollreveal';
import dt from "datatables.net";
var dt = dt(window, $);

ScrollReveal().reveal('.ScrollReveal-1', {
    delay: 500,
    distance: '5px',
});

$(function () {
    $('#example').DataTable({
        serverSide: true,
        ordering: true,
        orderMulti: true,
        processing: true,
        scrollX: true,
        responsive: true,
        ajax: {
            url: 'https://localhost:7259/',
            type: 'POST',
            datatype: "JSON",
            data: function (d) {
                // note: d is created by datatable, the structure of d is the same with DataTableParameters model above
                console.log(d);
                return JSON.stringify(d);
            },
            complete: async function (response) {
                console.log(await response.then())
            }
        },
        rowId: 'customerNumber',
        columns: [
            { data: 'customerNumber' },
            { data: 'customerName' },
            { data: 'contactLastName' },
            { data: 'contactFirstName' },
            { data: 'phone' },
            { data: 'addressLine1' },
            { data: 'addressLine2' },
            { data: 'city' },
            { data: 'state' },
            { data: 'postalCode' },
            { data: 'country' },
            { data: 'salesRepEmployeeNumber' },
            { data: 'creditLimit' },
        ],
    });

    $('.ScrollReveal-1').removeClass('invisible');
});