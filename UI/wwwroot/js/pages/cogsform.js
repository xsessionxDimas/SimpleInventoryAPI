"use strict";

/* variables */
var dataItem      = [];
var dataProduct   = [];
var dataComponent = [];

$(function () {
    getProductData();
    getComponentData();    
    $('#type').select2({
        width: 'resolve'
    });
});

/* get datasource */
function getProductData() {
    return $.ajax({
        url         : HOST_URL + '/api/Product/GetAsDataSource',
        type        : 'GET',
        contentType : 'application/json',
        async       : true,
        dataType    : 'json',
        headers     : {
            'Authorization': `Bearer ${token}`
        },
        success: function (result) {
            dataProduct = jsonToArray(result);
            initDropdown('#product', dataProduct);
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
        placeholder: 'Select options...',
        data: dataSource,
        width: 'resolve'
    });
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
    newRowStr += `<td><input type="text" class="form-control numeric" onkeypress="return isNumber(event)" id="usage_${index}" /></td>`;
    newRowStr += `<td><input type="text" class="form-control numeric" onkeypress="return isNumber(event)" id="cost_${index}" /></td>`;
    newRowStr += `<td><input type="text" class="form-control numeric" onkeypress="return isNumber(event)" id="freight_${index}" /></td>`;
    newRowStr += `<td><input type="text" class="form-control numeric" id="total_${index}" disabled="disabled" /></td>`;
    newRowStr += `<td><input type="text" class="form-control" id="notes_${index}" /></td>`;
    newRowStr += `<td>
                    <a id="btnHide_${index}" href="javascript:void(0);" onclick="hideRow(${index})" class="btn btn-icon btn-danger btn-sm"><i class="flaticon2-cross"></i></a>
                  </td></tr>`;
    $(newRowStr).appendTo('#tblItems tbody');
    initDropdown(`#component_${index}`, dataComponent);
    numericBlurEvent(index);
}

function createComponentDropdown(index) {
    return `<td><select class="form-control select2" id="component_${index}"></select>`;
}

function hideRow(index) {
    $(`#tblItems tbody tr:eq(${index})`).addClass('hide');
    var item     = dataItem[index];
    item.deleted = true;
}

function numericBlurEvent(index) {
    $(`#usage_${index}`).on('blur', function () {
        var value = $(this).val();
        if (value) {
            var cost    = $(`#cost_${index}`).val() ? Number($(`#cost_${index}`).val().replaceAll(',', '')) : 0;
            var freight = $(`#freight_${index}`).val() ? Number($(`#freight_${index}`).val().replaceAll(',', '')) : 0;
            var total   = Number(value.replaceAll(',', '')) * (cost + freight);
            $(this).val(numberWithCommas(value));
            $(`#total_${index}`).val(numberWithCommas(total));
        }
    });

    $(`#cost_${index}`).on('blur', function () {
        var value = $(this).val();
        if (value) {
            var usage   = $(`#usage_${index}`).val() ? Number($(`#usage_${index}`).val().replaceAll(',', '')) : 0;
            var freight = $(`#freight_${index}`).val() ? Number($(`#freight_${index}`).val().replaceAll(',', '')) : 0;
            var total   = (Number(value.replaceAll(',', '')) + freight) * usage;
            $(this).val(numberWithCommas(value));
            $(`#total_${index}`).val(numberWithCommas(total));
        }
    });

    $(`#freight_${index}`).on('blur', function () {
        var value = $(this).val();
        if (value) {
            var cost = $(`#cost_${index}`).val() ? Number($(`#cost_${index}`).val().replaceAll(',', '')) : 0;
            var usage = $(`#usage_${index}`).val() ? Number($(`#usage_${index}`).val().replaceAll(',', '')) : 0;
            var total = (Number(value.replaceAll(',', '')) + cost) * usage;
            $(this).val(numberWithCommas(value));
            $(`#total_${index}`).val(numberWithCommas(total));
        }
    });

    $('.numeric').on('focus', function () {
        var value = $(this).val();
        if (value) {
            $(this).val(value.replaceAll(',',''));
        }
    });
}


/* populate data */
function populateData() {
    var data = {
        Id        : 0,
        ProductId : $('#product').val(),
        Type      : $('#type').val(),
        User      : 'test-user'
    };

    var items = [];
    dataItem.forEach((item, index) => {
        if (item.deleted) return;
        items.push({
            Id              : 0,
            HeaderId        : 0,
            ComponentId     : $(`#component_${index}`).val(),
            Usage           : $(`#usage_${index}`).val().replaceAll(',', ''),
            CostPerUnit     : $(`#cost_${index}`).val().replaceAll(',', ''),
            FreightPerUnit  : $(`#freight_${index}`).val() ? $(`#freight_${index}`).val().replaceAll(',', '') : null,
            Total           : $(`#total_${index}`).val().replaceAll(',', ''),
            Notes           : $(`#notes_${index}`).val()
        })
    });

    data.Items = items;
    return data;
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
        "url"       : HOST_URL + '/api/ProductComponent/Add',
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
    });
}