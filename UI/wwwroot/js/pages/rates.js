"use strict";

var ratesfunc = function () {
    var isNew           = true;
    var itemId          = 0;
    var dataJSONArray   = [];
    var table;
    var numberWithCommas = function (str) {
        return str.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
    var createTable = function () {
        if (table) {
            table.destroy();
        }
        table = $('#kt_datatable').KTDatatable({
            // datasource definition
            data: {
                type     : 'local',
                source   : dataJSONArray,
                pageSize : 10,

            },
            // layout definition
            layout: {
                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false // display/hide footer
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },

            // columns definition
            columns: [
                {
                    field: 'currency',
                    title: 'Currency',
                }, {
                    field: 'rate',
                    title: 'Rate',
                    template: function (row) {
                        return numberWithCommas(row.rate.toString());
                    }
                }, {
                    field       : 'Actions',
                    title       : 'Actions',
                    sortable    : false,
                    width       : 125,
                    autoHide    : false,
                    overflow    : 'visible',
                    template    : function (row) {
                        return '\
                        <button onclick="ratesfunc.item(' + row.id + ');" class="btn btn-sm btn-clean btn-icon mr-2" title="Edit details">\
                            <span class="svg-icon svg-icon-md">\
                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">\
                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">\
                                        <rect x="0" y="0" width="24" height="24"/>\
                                        <path d="M8,17.9148182 L8,5.96685884 C8,5.56391781 8.16211443,5.17792052 8.44982609,4.89581508 L10.965708,2.42895648 C11.5426798,1.86322723 12.4640974,1.85620921 13.0496196,2.41308426 L15.5337377,4.77566479 C15.8314604,5.0588212 16,5.45170806 16,5.86258077 L16,17.9148182 C16,18.7432453 15.3284271,19.4148182 14.5,19.4148182 L9.5,19.4148182 C8.67157288,19.4148182 8,18.7432453 8,17.9148182 Z" fill="#000000" fill-rule="nonzero"\ transform="translate(12.000000, 10.707409) rotate(-135.000000) translate(-12.000000, -10.707409) "/>\
                                        <rect fill="#000000" opacity="0.3" x="5" y="20" width="15" height="2" rx="1"/>\
                                    </g>\
                                </svg>\
                            </span>\
                        </button>';
                    },
                }],

        });
    }
    var generateTable   = function () {
        /* api call here */
        var settings = {
            "url"       : HOST_URL + '/api/Currency/GetList',
            "method"    : "GET",
            "timeout"   : 0,
            "headers"   : {
                "Content-Type"  : "application/json",
                "Authorization" : `Bearer ${token}`
            },
        };

        $.ajax(settings).done(function (response) {
            dataJSONArray = response;
            createTable();
        });        
    }
    var getItem  = function (id) {
        var item = dataJSONArray.filter(row => row.id == id)[0];
        $("#currency").val(item.currency);
        $("#rates").val(numberWithCommas(item.rate));        
        /* show modal */
        $('#exampleModalCenter').modal('show');
        itemId = id;
    }
    
    var updateRate = function () {
        var data = JSON.stringify({
            "Id"       : itemId,
            "Currency" : $("#currency").val(),
            "Rate"     : $("#rates").val().replaceAll(',', ''),            
            "User"     : "Dimas"
        })
        /* api call here */
        var settings = {
            "url"       : HOST_URL + '/api/Currency/Update',
            "method"    : "PUT",
            "timeout"   : 0,
            "headers"   : {
                "Content-Type"  : "application/json",
                "Authorization" : `Bearer ${token}`
            },
            "data"      : data
        };

        $.ajax(settings).done(function (response) {
            dataJSONArray = response;
            isNew         = true;
            itemId        = 0;
            generateTable();
            clearForm();
        });        
    }
    var clearForm       = function (){
        $("#currency").val('');
        $("#rates").val('');
        $('#exampleModalCenter').modal('hide');
    }
    var registerEvent   = function () {
        $("#btnSave").click(function () {
            /* loader */
            updateRate();
        });

        $('.numeric').on('focus', function () {
            var value = $(this).val();
            if (value) {
                $(this).val(value.replaceAll(',', ''));
            }
        });

        $('.numeric').on('blur', function () {
            var value = $(this).val();
            if (value) {
                $(this).val(numberWithCommas(value));
            }
        });
    }
    
    return {
        // public functions
        init: function () {
            generateTable();
            registerEvent();
        },
        item: function (id) {
            getItem(id);
        }
    };
}();


jQuery(document).ready(function () {
    ratesfunc.init();
});