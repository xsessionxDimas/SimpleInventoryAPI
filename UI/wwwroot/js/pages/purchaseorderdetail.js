"use strict";

/* variables */
var dataHeader    = {};
var dataItem      = [];
var dataSupplier  = [];
var dataComponent = [];

var arrows;
if (KTUtil.isRTL()) {
    arrows = {
        leftArrow: '<i class="la la-angle-right"></i>',
        rightArrow: '<i class="la la-angle-left"></i>'
    }
} else {
    arrows = {
        leftArrow: '<i class="la la-angle-left"></i>',
        rightArrow: '<i class="la la-angle-right"></i>'
    }
}

$(function () {
    var supplierPromise = new Promise((resolve) => {
        resolve(getSupplierData())
    });
    var componentPromise = new Promise((resolve) => {
        resolve(getComponentData());
    });
    var headerPromise = new Promise((resolve) => {
        resolve(getHeaderData());
    });

    var itemPromise = new Promise((resolve) => {
        resolve(getDetailData());
    });

    Promise.all([supplierPromise, componentPromise, headerPromise, itemPromise]).then(() => {
        var selectedSupplier = dataSupplier.filter(row => row.text == dataHeader[3])[0].id;
        $('#supplier').val(selectedSupplier).trigger('change');
        $('#ponumber').val(dataHeader[2]);
        var poDate     = new Date(dataHeader[5]);
        var dateString = ("0" + (poDate.getMonth() + 1)) + "/" + (("0" + poDate.getDate()).slice(-2)) + "/" + poDate.getFullYear();
        $('#podate').val(`${dateString}`);
        $('#notes').val(dataHeader[6]);
        $('#subtotal').val(numberWithCommas(dataHeader[7].toString()));
        $('#discount').val(numberWithCommas(dataHeader[8].toString()));
        $('#tax').val(numberWithCommas(dataHeader[9].toString()));
        $('#additional').val(numberWithCommas(dataHeader[10].toString()));
        $('#grandtotal').val(numberWithCommas(dataHeader[11].toString()));
        $('#podate').datepicker({
            rtl            : KTUtil.isRTL(),
            todayHighlight : true,
            orientation    : "bottom left",
            templates      : arrows
        });
        dataItem.forEach(item => {
            renderRow(item);
        });
        if (!dataHeader[4]) {
            $('#btnApply').hide();
        }
    });      
    taxAndFeesEvent();
});

function getQueryString(key) {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(key);
}

function getHeaderData() {
    return $.ajax({
        url         : HOST_URL + '/api/PurchaseOrder/Detail?id=' + getQueryString('id'),
        type        : 'GET',
        contentType : 'application/json',
        async       : true,
        dataType    : 'json',
        headers     : {
            'Authorization': `Bearer ${token}`
        },
        success: function (result) {
            dataHeader = jsonToArray(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function getDetailData() {
    return $.ajax({
        url         : HOST_URL + '/api/PurchaseOrder/GetListById?id=' + getQueryString('id'),
        type        : 'GET',
        contentType : 'application/json',
        async       : true,
        dataType    : 'json',
        headers     : {
            'Authorization': `Bearer ${token}`
        },
        success: function (result) {
            dataItem = jsonToArray(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

/* get datasource */
function getSupplierData() {
    return $.ajax({
        url         : HOST_URL + '/api/Supplier/GetAsDataSource',
        type        : 'GET',
        contentType : 'application/json',
        async       : true,
        dataType    : 'json',
        headers     : {
            'Authorization': `Bearer ${token}`
        },
        success: function (result) {
            dataSupplier = jsonToArray(result);
            initDropdown('#supplier', dataSupplier);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function getComponentData() {
    return $.ajax({
        url         : HOST_URL + '/api/Component/GetAsDataSource',
        type        : 'GET',
        contentType : 'application/json',
        async       : true,
        dataType    : 'json',
        headers     : {
            'Authorization': `Bearer ${token}`
        },
        success: function (result) {
            dataComponent = jsonToArray(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function numberWithCommas(str) {
    return str.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function isNumber(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function jsonToArray(json) {
    var result = [];
    var keys   = Object.keys(json);
    keys.forEach(key => {
        result.push(json[key]);
    });
    return result;
}

function initDropdown(selector, dataSource) {
    $(selector).select2({
        placeholder : 'Select options...',
        data        : dataSource,
        width       : 'resolve'
    });
    $(selector).val(null).trigger('change')
}

/* tables function */
function renderRow(item) {
    var newRowStr = '<tr>';
    newRowStr += `<td>${item.partNumber}</td>`;
    newRowStr += `<td>${item.partDesc}</td>`;
    newRowStr += `<td>${item.qty}</td>`;
    newRowStr += `<td>${numberWithCommas(item.price)}</td >`;
    newRowStr += `<td>${numberWithCommas(item.discount)}</td>`;
    newRowStr += `<td>${numberWithCommas(item.total)}</td></tr>`; 
    $(newRowStr).appendTo('#tblItems tbody');
}

function createComponentDropdown(index) {
    return `<td><select class="form-control select2" id="component_${index}"></select>`;
}

function hideRow(index) {
    $(`#tblItems tbody tr:eq(${index})`).addClass('hide');
    var item     = dataItem[index];
    item.deleted = true;
    calculateCostSummary();
}

function componentChangeEvent(index) {
    $(`#component_${index}`).on('change', function () {
        var value = $(this).val();
        if (value) {
            /* find in datasource */
            var item = dataComponent.filter(x => x.id == Number(value))[0];
            if (item) {
                $(`#desc_${index}`).val(item.info);
            }
        }
    });
}

function taxAndFeesEvent() {
    $('#tax').on('blur', function () {
        var value = $(this).val();
        if (value) {            
            $(this).val(numberWithCommas(value));
            calculateCostSummary();
        }
    });
    $('#additional').on('blur', function () {
        var value = $(this).val();
        if (value) {
            $(this).val(numberWithCommas(value));
            calculateCostSummary();
        }
    });
}

function numericBlurEvent(index) {
    $(`#qty_${index}`).on('blur', function () {
        var value = $(this).val();
        if (value) {
            var price    = $(`#price_${index}`).val() ? Number($(`#price_${index}`).val().replaceAll(',', '')) : 0;
            var discount = $(`#discount_${index}`).val() ? Number($(`#discount_${index}`).val().replaceAll(',', '')) : 0;
            var total    = Number(value.replaceAll(',', '')) * price - discount ;
            $(this).val(numberWithCommas(value));
            $(`#total_${index}`).val(numberWithCommas(total));
            calculateCostSummary();
        }        
    });

    $(`#price_${index}`).on('blur', function () {
        var value = $(this).val();
        if (value) {
            var qty      = $(`#qty_${index}`).val() ? Number($(`#qty_${index}`).val().replaceAll(',', '')) : 0;
            var discount = $(`#discount_${index}`).val() ? Number($(`#discount_${index}`).val().replaceAll(',', '')) : 0;
            var total    = (Number(value.replaceAll(',', ''))) * qty - discount;
            $(this).val(numberWithCommas(value));
            $(`#total_${index}`).val(numberWithCommas(total));
            calculateCostSummary();
        }
    });

    $(`#discount_${index}`).on('blur', function () {
        var value = $(this).val();
        if (value) {
            var price = $(`#price_${index}`).val() ? Number($(`#price_${index}`).val().replaceAll(',', '')) : 0;
            var qty   = $(`#qty_${index}`).val() ? Number($(`#qty_${index}`).val().replaceAll(',', '')) : 0;
            var total = price * qty - (Number(value.replaceAll(',', '')));
            $(this).val(numberWithCommas(value));
            $(`#total_${index}`).val(numberWithCommas(total));
            calculateCostSummary();
        }
    });

    $('.numeric').on('focus', function () {
        var value = $(this).val();
        if (value) {
            $(this).val(value.replaceAll(',',''));
        }
    });
}

function calculateCostSummary() {
    var subTotal   = 0;
    var discount   = 0;
    var tax        = 0;
    var addFee     = 0;
    var grandTotal = 0;
    dataItem.forEach((row, index) => {
        if (row.deleted) return;
        subTotal += $(`#total_${index}`).val() ? Number($(`#total_${index}`).val().replaceAll(',', '')) : 0;
        discount += $(`#discount_${index}`).val() ? Number($(`#discount_${index}`).val().replaceAll(',', '')) : 0;
    });
    $('#subtotal').val(numberWithCommas(subTotal));
    $('#discount').val(numberWithCommas(discount));
    tax    += $('#tax').val() ? Number($('#tax').val().replaceAll(',', '')) : 0;
    tax     = subTotal * (tax / 100);
    addFee += $('#additional').val() ? Number($('#additional').val().replaceAll(',', '')) : 0;
    grandTotal = subTotal - (discount + tax + addFee);
    $('#grandtotal').val(numberWithCommas(grandTotal));    
}

function dateGenerator(strDate) {
    var year   = strDate.split('/')[2];
    var month = strDate.split('/')[0];
    var day   = strDate.split('/')[1];
    return new Date(year, month - 1, day);
}

/* populate data */
function populateData() {
    var data = {
        Id   : Number(getQueryString('id')),
        User : 'test-user'        
    };
    return data;
}

function redirectBack() {
    window.location.href = "/PurchaseOrders"
}

function submitData() {
    var data = populateData();

    KTApp.block('#content', {
        overlayColor : '#000000',
        type         : 'v2',
        state        : 'danger',
        message      : 'Please wait...'
    });

    var settings = {
        "url"       : HOST_URL + '/api/PurchaseOrder/Apply',
        "method"    : "PUT",
        "timeout"   : 0,
        "headers"   : {
            "Content-Type"  : "application/json",
            "Authorization" : `Bearer ${token}`
        },
        "data"      : JSON.stringify(data)
    };

    $.ajax(settings).done(function () {
        KTApp.unblock('#content');
        redirectBack();
    });
}