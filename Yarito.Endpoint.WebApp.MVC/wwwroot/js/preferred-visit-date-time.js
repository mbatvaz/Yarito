$('#visitFa').pDatepicker({
    format: 'YYYY/MM/DD HH:mm',
    timePicker: { enabled: true, second: { enabled: false } },

    altField: '#PreferredVisitDateTime',
    altFormat: 'YYYY-MM-DD HH:mm:ss' // میلادی برای Bind شدن به DateTime
});