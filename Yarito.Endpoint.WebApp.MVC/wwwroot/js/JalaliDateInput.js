// JalaliDateInput.js
(function () {
    // جلوگیری از init دوباره
    if (window.__jalaliPickerInited) return;
    window.__jalaliPickerInited = true;

    document.addEventListener("DOMContentLoaded", function () {
        const faInput = document.getElementById("visitFa");
        const hidden = document.getElementById("VisitDateTime");
        if (!faInput || !hidden) return;

        // minDate = فردا (شمسی), maxDate = یک ماه بعد (شمسی)
        const minJ = moment().locale("fa").add(1, "day").format("YYYY/MM/DD");
        const maxJ = moment().locale("fa").add(1, "month").format("YYYY/MM/DD");
        faInput.setAttribute("data-jdp-min-date", minJ);
        faInput.setAttribute("data-jdp-max-date", maxJ);

        jalaliDatepicker.startWatch({
            hasSecond: false,
            persianDigits: true,
            minDate: "attr",
            maxDate: "attr",
            useDropDownYears: false,
            hasSecond: false,
            showEmptyBtn: false,
            changeMonthRotateYear: true,
        });

        function faToEnDigits(str) {
            return (str || "")
                .replace(/[۰-۹]/g, d => "۰۱۲۳۴۵۶۷۸۹".indexOf(d))
                .replace(/[٠-٩]/g, d => "٠١٢٣٤٥٦٧٨٩".indexOf(d));
        }

        function syncToHidden(val) {
            val = faToEnDigits(val).trim();
            if (!val) { hidden.value = ""; return; }

            const m = moment.from(val, "fa", "YYYY/MM/DD HH:mm").locale("en");
            hidden.value = m.isValid() ? m.format("YYYY-MM-DDTHH:mm:00") : "";
        }

        faInput.addEventListener("input", e => syncToHidden(e.target.value));
        faInput.addEventListener("change", e => syncToHidden(e.target.value));
    });

})();
