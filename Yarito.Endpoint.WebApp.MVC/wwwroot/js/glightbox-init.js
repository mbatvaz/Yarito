document.addEventListener("DOMContentLoaded", function() {
    if (typeof GLightbox !== 'undefined') {
        const lightbox = GLightbox({
            selector: '.glightbox',
            touchNavigation: true,
            loop: true,
            autoplayVideos: false,
            closeButton: true,
            closeOnOutsideClick: true,
            keyboardNavigation: true,
            draggable: true,
            zoomable: true,
            preload: true
        });
    }
});
