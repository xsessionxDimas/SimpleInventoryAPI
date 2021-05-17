"use strict";

/* variables */
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
    getSupplierData();
    getComponentData();    
    $('#type').select2({
        width: 'resolve'
    });
    $('#podate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows
    });
    taxAndFeesEvent();
});

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
function addNewRow() {
    var index = $('#tblItems tbody tr').length;
    dataItem.push({
        new: true,
        deleted: false
    });

    var newRowStr = '<tr>';
    newRowStr += createComponentDropdown(index);
    newRowStr += `<td><input type="text" class="form-control" id="desc_${index}" disabled="disabled" /></td>`;
    newRowStr += `<td><input type="text" class="form-control numeric" onkeypress="return isNumber(event)" id="qty_${index}" /></td>`;
    newRowStr += `<td><input type="text" class="form-control numeric" onkeypress="return isNumber(event)" id="price_${index}" /></td>`;
    newRowStr += `<td><input type="text" class="form-control numeric" onkeypress="return isNumber(event)" id="discount_${index}" /></td>`;
    newRowStr += `<td><input type="text" class="form-control" id="total_${index}" disabled="disabled" /></td>`;
    newRowStr += `<td>
                    <a id="btnHide_${index}" href="javascript:void(0);" onclick="hideRow(${index})" class="btn btn-icon btn-danger btn-sm"><i class="flaticon2-cross"></i></a>
                  </td></tr>`;
    $(newRowStr).appendTo('#tblItems tbody');
    initDropdown(`#component_${index}`, dataComponent);
    componentChangeEvent(index);
    numericBlurEvent(index);
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
        Id                  : 0,
        PurchaseOrderNumber : $('#ponumber').val(),
        SupplierId          : $('#supplier').val(),
        OrderDate           : dateGenerator($('#podate').val()),
        Notes               : $('#notes').val(),
        SubTotal            : $('#subtotal').val().replaceAll(',', ''),
        Discount            : $('#discount').val() ? $('#discount').val().replaceAll(',', '') : 0,
        Tax                 : $('#tax').val() ? $('#tax').val().replaceAll(',', '') : 0,
        Additional          : $('#additional').val() ? $('#additional').val().replaceAll(',', '') : 0,
        GrandTotal          : $('#grandtotal').val().replaceAll(',', ''),
        User                : 'test-user'        
    };

    var items = [];
    dataItem.forEach((item, index) => {
        if (item.deleted) return;
        items.push({
            Id              : 0,
            PurchaseOrderId : 0,
            ComponentId     : $(`#component_${index}`).val(),
            Qty             : $(`#qty_${index}`).val().replaceAll(',', ''),
            Price           : $(`#price_${index}`).val().replaceAll(',', ''),
            Discount        : $(`#discount_${index}`).val() ? $(`#discount_${index}`).val().replaceAll(',', '') : 0,
            Total           : $(`#total_${index}`).val().replaceAll(',', '')
        })
    });

    data.Items = items;
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
        "url"       : HOST_URL + '/api/PurchaseOrder/Add',
        "method"    : "POST",
        "timeout"   : 0,
        "headers"   : {
            "Content-Type"  : "application/json",
            "Authorization" : `Bearer ${token}`
        },
        "data"      : JSON.stringify(data)
    };

    $.ajax(settings).done(function (response) {
        KTApp.unblock('#content');
        redirectBack();
    });
}