//type: 'success', 'warning', 'error'

function showNotification(type, title, message) {
    var container = document.getElementById('notification-container');
    if (!container) return;

    var notification = document.createElement('div');
    notification.className = 'notification notification--' + type;
    notification.innerHTML =
        '<div class="notification__content">' +
        '<h4 class="notification__title">' + title + '</h4>' +
        '<p class="notification__message">' + message + '</p>' +
        '</div>' +
        '<div class="notification__progress"></div>';

    container.appendChild(notification);

    setTimeout(function () {
        notification.classList.add('hiding');
        setTimeout(function () {
            notification.remove();
        }, 300);
    }, 5000);
}

function showWarning(title, message, duration) {
    return showNotification('warning', title, message, duration);
}

function showError(title, message, duration) {
    return showNotification('error', title, message, duration);
}