"use strict";

var alertFunction = function () {
    var successAlert = function (message, cb) {
        Swal.fire({
            title : "Good job!",
            text  : "message",
            icon  : "success",
            buttonsStyling: false,
        });
    }   

    return {
        success: function (message, cb) {
            Swal.fire({
                title          : "Good job!",
                text           : message,
                icon           : "success",
                buttonsStyling : false,
            });
        },

        error: function (message, cb) {

        },

        confirmation: function (message, cb) {

        }
    }
}
