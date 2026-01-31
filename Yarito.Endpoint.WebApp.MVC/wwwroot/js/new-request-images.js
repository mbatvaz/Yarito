(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        var input = document.getElementById('imageUpload');
        var previews = document.getElementById('uploadPreviews');

        if (!input || !previews) {
            return;
        }

        var maxFiles = 4;
        var buffer = [];
        var dataTransferSupported = true;

        try {
            new DataTransfer();
        } catch (err) {
            dataTransferSupported = false;
        }

        function createPreview(file) {
            var preview = document.createElement('div');
            preview.className = 'upload-preview';

            var img = document.createElement('img');
            img.src = URL.createObjectURL(file);
            img.alt = 'پیش‌نمایش';

            var overlay = document.createElement('div');
            overlay.className = 'upload-preview__overlay';

            var removeBtn = document.createElement('button');
            removeBtn.type = 'button';
            removeBtn.className = 'upload-preview__remove';
            removeBtn.title = 'حذف';
            removeBtn.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path></svg>';
            removeBtn.addEventListener('click', function () {
                clearAll();
            });

            overlay.appendChild(removeBtn);

            var name = document.createElement('span');
            name.className = 'upload-preview__name';
            name.textContent = file.name;

            preview.appendChild(img);
            preview.appendChild(overlay);
            preview.appendChild(name);

            return preview;
        }

        function renderPreviews() {
            previews.innerHTML = '';
            buffer.forEach(function (file) {
                previews.appendChild(createPreview(file));
            });
        }

        function syncInputFiles() {
            if (!dataTransferSupported) {
                return;
            }

            var dt = new DataTransfer();
            buffer.forEach(function (file) {
                dt.items.add(file);
            });

            input.files = dt.files;
        }

        function trimBuffer() {
            if (buffer.length > maxFiles) {
                buffer = buffer.slice(buffer.length - maxFiles);
            }
        }

        function addFiles(files) {
            buffer = buffer.concat(files);
            trimBuffer();
            syncInputFiles();
            renderPreviews();
        }

        function clearAll() {
            buffer = [];
            input.value = '';
            previews.innerHTML = '';
        }

        input.addEventListener('change', function () {
            var files = Array.from(input.files || []);
            if (files.length === 0) {
                return;
            }

            addFiles(files);
        });

        var form = input.closest('form');
        if (form) {
            form.addEventListener('submit', function () {
                trimBuffer();
                syncInputFiles();
            });
        }

        if (!dataTransferSupported) {
            console.warn('DataTransfer is not supported; file list will include only the last selection.');
        }
    });
})();
