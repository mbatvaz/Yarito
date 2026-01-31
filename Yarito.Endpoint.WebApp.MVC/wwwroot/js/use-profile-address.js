document.addEventListener('DOMContentLoaded', function () {
    const checkbox = document.getElementById('use-profile-address');
    const addressTextarea = document.getElementById('request-address');

    checkbox.addEventListener('change', function () {
        const isDisabled = this.checked;

        addressTextarea.disabled = isDisabled;
        addressTextarea.required = !isDisabled;

        if (isDisabled) {
            addressTextarea.style.backgroundColor = '#f5f5f5';
            addressTextarea.style.color = '#999';
            addressTextarea.style.borderColor = '#ddd';
        } else {
            addressTextarea.style.backgroundColor = '';
            addressTextarea.style.color = '';
            addressTextarea.style.borderColor = '';
        }
    });

    checkbox.dispatchEvent(new Event('change'));
});
