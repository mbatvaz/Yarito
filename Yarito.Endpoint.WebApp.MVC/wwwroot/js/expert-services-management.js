/**
 * Expert Services Management JavaScript
 * مدیریت خدمات متخصص - انتخاب دسته‌بندی و سرویس با Select2
 */
(function () {
    'use strict';

    let selectedServices = new Map(); // Map of workId => {id, title, categoryTitle}
    let categoryMap = new Map(); // Map of categoryId => {id, works: []}

    /**
     * Parse JSON data from script tag
     */
    function parseJsonData(elementId) {
        const el = document.getElementById(elementId);
        if (!el) return [];

        const raw = (el.textContent || '').trim();
        if (!raw) return [];

        try {
            return JSON.parse(raw);
        } catch (err) {
            console.error('Error parsing ' + elementId + ':', err);
            return [];
        }
    }

    /**
     * Build category map from JSON data
     */
    function buildCategoryMap() {
        const data = parseJsonData('categoryWorksData');
        const map = new Map();

        if (Array.isArray(data)) {
            data.forEach(function (item) {
                if (!item || item.id === undefined) return;
                map.set(String(item.id), item);
            });
        }

        return map;
    }

    /**
     * Load current services from server data
     */
    function loadCurrentServices() {
        const data = parseJsonData('currentServicesData');

        if (Array.isArray(data)) {
            data.forEach(function (service) {
                if (!service || !service.id) return;
                selectedServices.set(String(service.id), {
                    id: service.id,
                    title: service.title,
                    categoryTitle: service.categoryTitle || ''
                });
            });
        }

        renderServicesChips();
    }

    /**
     * Render service chips in the container
     */
    function renderServicesChips() {
        const container = document.getElementById('currentServicesContainer');
        if (!container) return;

        container.innerHTML = '';

        if (selectedServices.size === 0) {
            container.innerHTML = '<p class="form-help" style="margin: 0;">هنوز خدمتی اضافه نشده است</p>';
            updateHiddenInputs();
            return;
        }

        selectedServices.forEach(function (service) {
            const chip = document.createElement('span');
            chip.className = 'service-chip service-chip--removable';
            chip.setAttribute('data-work-id', service.id);

            const textNode = document.createTextNode(service.title);
            chip.appendChild(textNode);

            const removeBtn = document.createElement('button');
            removeBtn.type = 'button';
            removeBtn.className = 'service-chip__remove';
            removeBtn.setAttribute('aria-label', 'حذف ' + service.title);
            removeBtn.textContent = '×';

            removeBtn.addEventListener('click', function () {
                selectedServices.delete(String(service.id));
                renderServicesChips();
            });

            chip.appendChild(removeBtn);
            container.appendChild(chip);
        });

        updateHiddenInputs();
    }

    /**
     * Update hidden inputs for form submission
     */
    function updateHiddenInputs() {
        // Remove existing hidden inputs
        const form = document.querySelector('form');
        const existingInputs = form.querySelectorAll('input[name="WorkIds"]');
        existingInputs.forEach(function (input) {
            input.remove();
        });

        // Add new hidden inputs for each work ID
        selectedServices.forEach(function (service) {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'WorkIds';
            input.value = service.id;
            form.appendChild(input);
        });
    }

    /**
     * Populate service dropdown based on selected category
     */
    function populateServiceSelect(categoryId) {
        const serviceSelect = document.getElementById('serviceId');
        if (!serviceSelect) return;

        // Clear current options
        serviceSelect.innerHTML = '';

        const emptyOption = document.createElement('option');
        emptyOption.value = '';
        emptyOption.textContent = categoryId ? 'انتخاب سرویس' : 'ابتدا دسته‌بندی را انتخاب کنید';
        serviceSelect.appendChild(emptyOption);

        if (!categoryId) {
            if (window.jQuery && window.jQuery.fn.select2) {
                window.jQuery(serviceSelect).val(null).trigger('change');
            }
            return;
        }

        const categoryData = categoryMap.get(String(categoryId));
        if (!categoryData || !Array.isArray(categoryData.works)) {
            if (window.jQuery && window.jQuery.fn.select2) {
                window.jQuery(serviceSelect).val(null).trigger('change');
            }
            return;
        }

        categoryData.works.forEach(function (work) {
            if (!work || !work.id) return;

            const option = document.createElement('option');
            option.value = work.id;
            option.textContent = work.title || '—';
            serviceSelect.appendChild(option);
        });

        if (window.jQuery && window.jQuery.fn.select2) {
            window.jQuery(serviceSelect).trigger('change');
        }
    }

    /**
     * Add selected service to list
     */
    function addServiceToList() {
        const categorySelect = document.getElementById('categoryId');
        const serviceSelect = document.getElementById('serviceId');

        if (!categorySelect || !serviceSelect) return;

        const categoryId = categorySelect.value;
        const serviceId = serviceSelect.value;

        if (!categoryId || !serviceId) {
            alert('لطفاً دسته‌بندی و سرویس را انتخاب کنید');
            return;
        }

        // Check if already added
        if (selectedServices.has(String(serviceId))) {
            alert('این خدمت قبلاً اضافه شده است');
            return;
        }

        // Get service details
        const categoryData = categoryMap.get(String(categoryId));
        if (!categoryData) return;

        const work = categoryData.works.find(function (w) {
            return String(w.id) === String(serviceId);
        });

        if (!work) return;

        const categoryOption = categorySelect.options[categorySelect.selectedIndex];
        const categoryTitle = categoryOption ? categoryOption.textContent : '';

        selectedServices.set(String(serviceId), {
            id: work.id,
            title: work.title,
            categoryTitle: categoryTitle
        });

        renderServicesChips();

        // Reset selects
        if (window.jQuery && window.jQuery.fn.select2) {
            window.jQuery(categorySelect).val(null).trigger('change');
            window.jQuery(serviceSelect).val(null).trigger('change');
        } else {
            categorySelect.value = '';
            serviceSelect.value = '';
        }

        populateServiceSelect(null);
    }

    /**
     * Focus select2 search field when opened
     */
    function focusSelect2Search() {
        var searchField = document.querySelector('.select2-container--open .select2-search--dropdown .select2-search__field');
        if (searchField) {
            searchField.style.display = 'block';
            setTimeout(function () {
                searchField.focus();
            }, 10);
        }
    }

    /**
     * Initialize select2 dropdowns
     */
    function initializeSelect2() {
        if (!window.jQuery || !window.jQuery.fn.select2) {
            console.log('Select2 not available');
            return;
        }

        var $ = window.jQuery;

        // Category dropdown
        var categorySelect = $('#categoryId');
        if (categorySelect.length) {
            categorySelect.select2({
                placeholder: 'انتخاب دسته‌بندی...',
                language: 'fa',
                allowClear: true,
                width: 'resolve',
                dir: 'rtl',
                minimumResultsForSearch: 0
            }).on('select2:open', focusSelect2Search);

            categorySelect.on('change', function () {
                populateServiceSelect(this.value);
            });
        }

        // Service dropdown
        var serviceSelect = $('#serviceId');
        if (serviceSelect.length) {
            serviceSelect.select2({
                placeholder: 'انتخاب سرویس...',
                language: 'fa',
                allowClear: true,
                width: 'resolve',
                dir: 'rtl',
                minimumResultsForSearch: 0
            }).on('select2:open', focusSelect2Search);
        }

        // City dropdown
        var citySelect = $('#citySelect');
        if (citySelect.length) {
            citySelect.select2({
                placeholder: 'انتخاب شهر...',
                language: 'fa',
                allowClear: true,
                width: 'resolve',
                dir: 'rtl',
                minimumResultsForSearch: 0
            }).on('select2:open', focusSelect2Search);
        }
    }

    /**
     * Document ready handler
     */
    document.addEventListener('DOMContentLoaded', function () {
        // Initialize data
        categoryMap = buildCategoryMap();
        loadCurrentServices();

        // Initialize select2
        initializeSelect2();

        // Add service button handler
        var addServiceBtn = document.getElementById('addServiceBtn');
        if (addServiceBtn) {
            addServiceBtn.addEventListener('click', addServiceToList);
        }
    });
})();
