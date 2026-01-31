(function () {
    'use strict';

    function normalizeNumber(value) {
        if (value === null || value === undefined) {
            return null;
        }

        var str = String(value).trim();
        if (!str) {
            return null;
        }

        str = str.replace(/[۰-۹]/g, function (d) { return '۰۱۲۳۴۵۶۷۸۹'.indexOf(d); })
            .replace(/[٠-٩]/g, function (d) { return '٠١٢٣٤٥٦٧٨٩'.indexOf(d); })
            .replace(/[,٬\s]/g, '');

        var num = Number(str);
        return Number.isNaN(num) ? null : num;
    }

    function formatPrice(value) {
        var numberValue = normalizeNumber(value);
        if (numberValue === null) {
            return '—';
        }

        return numberValue.toLocaleString('fa-IR') + ' تومان';
    }

    function parseWorks(option) {
        if (!option) {
            return [];
        }

        var data = option.getAttribute('data-works');
        if (!data) {
            return [];
        }

        data = decodeHtml(data);

        try {
            return JSON.parse(data);
        } catch (err) {
            return [];
        }
    }

    function decodeHtml(value) {
        if (!value) {
            return '';
        }

        var textarea = document.createElement('textarea');
        textarea.innerHTML = value;
        return textarea.value;
    }

    function readCategoryWorksData() {
        var dataEl = document.getElementById('categoryWorksData');
        if (!dataEl) {
            return [];
        }

        var raw = (dataEl.textContent || '').trim();
        if (!raw) {
            return [];
        }

        try {
            return JSON.parse(raw);
        } catch (err) {
            return [];
        }
    }

    function buildCategoryMap(categorySelect) {
        var data = readCategoryWorksData();
        var map = new Map();

        if (Array.isArray(data) && data.length > 0) {
            data.forEach(function (item) {
                if (!item || item.id === undefined || item.id === null) {
                    return;
                }
                map.set(String(item.id), item);
            });
        } else if (categorySelect) {
            Array.from(categorySelect.options).forEach(function (option) {
                if (!option || !option.value) {
                    return;
                }

                var works = parseWorks(option);
                if (!Array.isArray(works) || works.length === 0) {
                    return;
                }

                map.set(String(option.value), { id: option.value, works: works });
            });
        }

        return map;
    }

    function buildServicePriceMap(categoryMap) {
        var map = new Map();

        if (!categoryMap) {
            return map;
        }

        categoryMap.forEach(function (category) {
            if (!category || !Array.isArray(category.works)) {
                return;
            }

            category.works.forEach(function (work) {
                if (!work) {
                    return;
                }

                var id = work.Id !== undefined ? work.Id : work.id;
                var basePrice = work.BasePrice !== undefined ? work.BasePrice : work.basePrice;

                if (id === undefined || id === null || basePrice === undefined || basePrice === null || basePrice === '') {
                    return;
                }

                map.set(String(id), basePrice);
            });
        });

        return map;
    }

    function focusSelect2Search() {
        var searchField = document.querySelector('.select2-container--open .select2-search--dropdown .select2-search__field');
        if (searchField) {
            searchField.style.display = 'block';
            setTimeout(function () {
                searchField.focus();
            }, 10);
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        var categorySelect = document.getElementById('request-category');
        var serviceSelect = document.getElementById('request-service');
        var defaultPriceEl = document.getElementById('defaultServicePrice');

        if (!categorySelect || !serviceSelect) {
            return;
        }

        var categoryMap = buildCategoryMap(categorySelect);
        var servicePriceMap = buildServicePriceMap(categoryMap);

        var $ = window.jQuery;
        if ($ && $.fn && $.fn.select2) {
            $(categorySelect).select2({
                placeholder: categorySelect.getAttribute('data-placeholder') || 'انتخاب دسته‌بندی...',
                language: 'fa',
                allowClear: true,
                width: 'resolve',
                dir: 'rtl',
                minimumResultsForSearch: 0,
                dropdownParent: $(document.body)
            }).on('select2:open', focusSelect2Search);

            $(serviceSelect).select2({
                placeholder: serviceSelect.getAttribute('data-placeholder') || 'انتخاب سرویس...',
                language: 'fa',
                allowClear: true,
                width: 'resolve',
                dir: 'rtl',
                minimumResultsForSearch: 0,
                dropdownParent: $(document.body)
            }).on('select2:open', focusSelect2Search);
        }

        function getSelectedWorkId() {
            return serviceSelect.getAttribute('data-selected-work');
        }

        function getSelectedCategoryOption() {
            return categorySelect.options[categorySelect.selectedIndex] || null;
        }

        function getCategoryData(option) {
            if (!option || !option.value) {
                return null;
            }

            var key = String(option.value);
            return categoryMap.get(key) || null;
        }

        function setDefaultPrice(value) {
            if (!defaultPriceEl) {
                return;
            }

            defaultPriceEl.textContent = formatPrice(value);
        }

        function setDefaultPriceFromCategory(option) {
            if (!option) {
                setDefaultPrice('');
                return;
            }

            // Until a service is selected, don't show any default price.
            setDefaultPrice('');
        }

        function handleServiceSelection() {
            var selectedOption = serviceSelect.options[serviceSelect.selectedIndex];
            if (!selectedOption || !selectedOption.value) {
                setDefaultPrice('');
                return;
            }

            var selectedId = String(selectedOption.value);
            if (servicePriceMap.has(selectedId)) {
                setDefaultPrice(servicePriceMap.get(selectedId));
                return;
            }

            var basePrice = selectedOption.getAttribute('data-base-price');
            if (basePrice !== null && basePrice !== undefined && basePrice !== '') {
                setDefaultPrice(basePrice);
                return;
            }

            var categoryData = getCategoryData(getSelectedCategoryOption());
            if (categoryData && Array.isArray(categoryData.works)) {
                var match = categoryData.works.find(function (work) {
                    var id = work.Id !== undefined ? work.Id : work.id;
                    return String(id) === selectedId;
                });

                if (match) {
                    var matchPrice = match.BasePrice !== undefined ? match.BasePrice : match.basePrice;
                    setDefaultPrice(matchPrice);
                    return;
                }
            }

            setDefaultPrice('');
        }

        function populateServices(option) {
            var categoryData = getCategoryData(option);
            var works = categoryData && Array.isArray(categoryData.works)
                ? categoryData.works
                : parseWorks(option);
            var placeholderText = serviceSelect.getAttribute('data-placeholder-empty') || 'ابتدا دسته‌بندی را انتخاب کنید...';
            var selectedWorkId = getSelectedWorkId();

            serviceSelect.innerHTML = '';
            serviceSelect.appendChild(new Option(placeholderText, '', true, false));

            if (!Array.isArray(works) || works.length === 0) {
                if ($ && $.fn && $.fn.select2) {
                    $(serviceSelect).val(null).trigger('change');
                } else {
                    serviceSelect.value = '';
                }
                return;
            }

            works.forEach(function (work) {
                var id = work.Id !== undefined ? work.Id : work.id;
                var title = work.Title !== undefined ? work.Title : work.title;
                var basePrice = work.BasePrice !== undefined ? work.BasePrice : work.basePrice;
                if (!id) {
                    return;
                }

                var isSelected = selectedWorkId && String(id) === String(selectedWorkId);
                var optionEl = new Option(title || '—', id, isSelected, isSelected);
                if (basePrice !== undefined && basePrice !== null && basePrice !== '') {
                    optionEl.setAttribute('data-base-price', basePrice);
                }
                serviceSelect.appendChild(optionEl);
            });

            if ($ && $.fn && $.fn.select2) {
                $(serviceSelect).trigger('change');
            }
        }

        function handleCategoryChange() {
            var categoryOption = getSelectedCategoryOption();
            populateServices(categoryOption);
            setDefaultPriceFromCategory(categoryOption);
        }

        function resetSelectedWork() {
            serviceSelect.setAttribute('data-selected-work', '');
        }

        categorySelect.addEventListener('change', function () {
            resetSelectedWork();
            handleCategoryChange();
        });

        if ($ && $.fn && $.fn.select2) {
            $(categorySelect).on('select2:select select2:clear', function () {
                resetSelectedWork();
                handleCategoryChange();
            });
        }

        serviceSelect.addEventListener('change', handleServiceSelection);
        if ($ && $.fn && $.fn.select2) {
            $(serviceSelect).on('select2:select select2:clear', handleServiceSelection);
        }

        handleCategoryChange();

        if (serviceSelect.value) {
            var initialOption = serviceSelect.options[serviceSelect.selectedIndex];
            if (initialOption && initialOption.value) {
                handleServiceSelection();
            }
        }
    });
})();
