document.addEventListener("DOMContentLoaded", function () {
    let report_selector = document.getElementById('report_type');

    report_selector.addEventListener('focus', function () {
        report_selector.selectedIndex = 0;
    });

    report_selector.addEventListener('change', function () {
        if (report_selector.value === 'Movimentação de estoque') {
            $('#movimentacaoDeEstoque').modal('show');
        }
        if (report_selector.value === 'Inventário') {
            $('#inventario').modal('show');
        }
    });

    
});

/**
 * Funções auxiliares.
 */

window.generateFormData = function (prefix, data) {
    const formData = [];

    for (let p of data) {
        formData[p] = document.getElementById(`${prefix}_${p}`).value;
    }
    return formData;
}

window.createElement = function (tag, options, value) {
    if (tag === 'text') {
        return document.createTextNode(value);
    }
    const el = document.createElement(tag);

    if (typeof value === 'string' || typeof value === 'number') {
        el.innerText = value;
    } else if (typeof value === 'object') {
        if (value instanceof Array) {
            el.replaceChildren(...value.filter((v) => v !== null && v !== undefined));
        } else {
            if (value !== null && value !== undefined) {
                el.replaceChildren(...[value]);
            }
        }
    }

    if (options !== null && typeof options === 'object') {
        if (options.hasOwnProperty('className')) {
            el.className = options['className'];
            delete options['className'];
        }

        if (options.hasOwnProperty('style')) {
            for (const [key, value] of Object.entries(options['style'])) {
                el.style[key] = value;
            }
            delete options['style'];
        }

        if (options.hasOwnProperty('data')) {
            for (const [key, value] of Object.entries(options['data'])) {
                el.dataset[key] = value;
            }
            delete options['data'];
        }

        if (options.hasOwnProperty('events')) {
            for (const [key, value] of Object.entries(options['events'])) {
                el.addEventListener(key, value);
            }
        }

        for (const [key, value] of Object.entries(options)) {
            el.setAttribute(key, value);
        }
    }
    return el;
}

window.formatDate = function (dateStr) {
    let dateTimeArr = dateStr.split('T');
    let dateArr = dateTimeArr[0].split('-');
    return `${dateArr[2]}/${dateArr[1]}/${dateArr[0]}`;
}