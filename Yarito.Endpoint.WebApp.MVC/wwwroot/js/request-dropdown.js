document.querySelectorAll('.status-dropdown__toggle').forEach(function (toggle) {
    toggle.addEventListener('click', function (e) {
        e.stopPropagation();
        const dropdown = this.closest('.status-dropdown');
        document.querySelectorAll('.status-dropdown.is-open').forEach(function (d) {
            if (d !== dropdown) d.classList.remove('is-open');
        });
        dropdown.classList.toggle('is-open');
    });
});

document.addEventListener('click', function () {
    document.querySelectorAll('.status-dropdown.is-open').forEach(function (d) {
        d.classList.remove('is-open');
    });
});