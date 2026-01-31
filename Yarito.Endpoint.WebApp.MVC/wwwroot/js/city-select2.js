$(function () {
    // Use class for all city selects
    $('select[id="city"]').select2({
        placeholder: 'انتخاب شهر',
        language: "fa",
        allowClear: true,
        width: 'resolve',
        dir: "rtl",
        minimumResultsForSearch: 0,
        dropdownParent: $(document.body)
    });

    // Focus search input on open
    $('select[id="city"]').on('select2:open', function () {
        const $search = $('.select2-container--open .select2-search--dropdown .select2-search__field');
        $search.css('display', 'block');
        setTimeout(function () { $search.focus(); }, 10);
    });
});