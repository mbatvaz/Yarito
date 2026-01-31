(function () {
    'use strict';

    function normalizeNumber(value) {
        if (value === null || value === undefined) {
            return '';
        }

        var str = String(value).trim();
        if (!str) {
            return '';
        }

        str = str.replace(/[۰-۹]/g, function (d) { return '۰۱۲۳۴۵۶۷۸۹'.indexOf(d); })
            .replace(/[٠-٩]/g, function (d) { return '٠١٢٣٤٥٦٧٨٩'.indexOf(d); })
            .replace(/[,٬\s]/g, '');

        return str;
    }

    document.addEventListener('DOMContentLoaded', function () {
        var form = document.querySelector('form.wizard-form');
        var priceInput = document.getElementById('request-price');

        if (!form || !priceInput) {
            return;
        }

        form.addEventListener('submit', function () {
            var normalized = normalizeNumber(priceInput.value);
            priceInput.value = normalized;
        });
    });
})();
