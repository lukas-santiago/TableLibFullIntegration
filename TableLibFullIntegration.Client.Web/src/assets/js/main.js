// Import all of Bootstrap's JS
import * as bootstrap from 'bootstrap'
import jQuery from 'jquery';
// export for others scripts to use
window.$ = jQuery;

import dt from "datatables.net";
var dt = dt(window, $);

$(document).ready(function () {
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
});