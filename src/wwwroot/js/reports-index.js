document.addEventListener("DOMContentLoaded", function () {
    let containter = document.getElementById("content");

    let queryMovimentacaoDeEstoque = document.getElementById("reports_submit");
    queryMovimentacaoDeEstoque.addEventListener("click", function () {
        initReports(containter);
    });

    let report_selector = document.getElementById('report_type');

    report_selector.addEventListener('focus', function () {
        report_selector.selectedIndex = 0;
    });

    report_selector.addEventListener('change', function () {
        if (report_selector.value === 'Movimentação de estoque') {
            $('#movimentacaoDeEstoque').modal('show');
        }
    });

    // Inicia variaveis globais.
    window.offset = 0;
});

/**
 * Funções auxiliares.
 */

function generateFormData(prefix, data) {
    const formData = [];

    for (let p of data) {
        formData[p] = document.getElementById(`${prefix}_${p}`).value;
    }
    return formData;
}

function generateUrl(url, params) {
    let generatedUrl = url + "?";

    for (const [key, value] of Object.entries(params)) {
        if (typeof value === 'number') {
            generatedUrl += `${key}=${value}&`;
        }
        else if (typeof value === 'string' && value.length > 0) {
            generatedUrl += `${key}=${value}&`;
        }
    }
    return generatedUrl.slice(0, -1);;
}

function createElement(tag, options, value) {    
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

function formatDate(dateStr) {
    let dateTimeArr = dateStr.split('T');
    let dateArr = dateTimeArr[0].split('-');
    return `${dateArr[2]}/${dateArr[1]}/${dateArr[0]}`;
}

function processReportsData(container, response) {
    const { status, data } = response.value;
    const { count, totalCount, startDate, endDate, type, limit, offset } = data;

    const BACK_PAGE = -1;
    const NEXT_PAGE = 1;

    const hasPrev = offset > 0;
    const hasNext = offset + count < totalCount;

    const createTooltipTH = (value) => createElement('th', { data: { toggle: 'tooltip', placement: 'top' }, title: value }, value);
    const createTooltipTD = (value) => createElement('td', { data: { toggle: 'tooltip', placement: 'top' }, title: value }, value);

    const updateReports = (container, offset) => {
        const action = fetch(generateUrl('Reports/GenerateMovimentacaoEstoque', {
            startDate, endDate, type, limit, offset
        }), {
            headers: {
                'Accepted': 'application/json; charset=utf-8',
            }
        });

        action
            .then((response) => response.json())
            .then((response) => processReportsData(container, response));
    };

    const onPagClick = (enabled, index) => {
        if (enabled) {
            switch (index) {
                case BACK_PAGE:
                    updateReports(container, Math.max(0, offset - limit));
                    break;

                case NEXT_PAGE:
                    updateReports(container, offset + limit);
                    break;
            }
        }
    };

    if (status === 200) {
        container.replaceChildren(
            createElement('table', { className: 'table table-hover table-sm', style: { backgroundColor: 'white', borderRadius: '0.25rem' } }, [
                createElement('thead', null, [
                    createElement('tr', null, [
                        createTooltipTH("Solicitado Por"),
                        createTooltipTH("Registrado Por"),
                        createTooltipTH("Tipo de movimentação"),
                        createTooltipTH("Motivo"),
                        createTooltipTH("Produto"),
                        createTooltipTH("Quantidade"),
                        createTooltipTH("Data"),
                    ])
                ]),
                createElement('tbody', null, data['results'].map((record) => createElement('tr', null, [
                    createTooltipTD(record['solicitadoPor']?.['nome']),
                    createTooltipTD(record['registradoPor']?.['nome']),
                    createTooltipTD(record['tipo']),
                    createTooltipTD(record['motivo']),
                    createTooltipTD(record['produto']['nome']),
                    createTooltipTD(record['quantidade']),
                    createTooltipTD(formatDate(record['dataMovimento'])),
                ]))),
                createElement('tfoot', null, [
                    createElement('tr', null, [
                        createElement('td', { colspan: '7' }, [
                            createElement('div', { className: 'd-flex justify-content-between' }, [
                                createElement('ul', { className: 'c-pagination ' }, [
                                    createElement('li', { className: (hasPrev ? 'enabled' : 'disabled'), events: { click: () => { onPagClick(hasPrev, BACK_PAGE); } } },  '<'),
                                    createElement('li', { className: (hasNext ? 'enabled' : 'disabled'), events: { click: () => { onPagClick(hasNext, NEXT_PAGE); } } },  '>'),
                                ]),
                                createElement('div', { className: 'me-4' }, [
                                    createElement('text', null, 'Exibindo '),
                                    createElement('b', null, offset + 1),
                                    createElement('text', null, ' a '),
                                    createElement('b', null, count + offset),
                                    createElement('text', null, ' registros de '),
                                    createElement('b', null, totalCount),
                                    createElement('text', null, ' resultados'),
                                ])
                            ])
                        ])
                    ])
                ])
            ])
        );

        $('#movimentacaoDeEstoque').modal('hide');
    } else {
        alert('Erro inesperado!');
    }
    // Exibindo <b>@(offset + 1)</b> a <b>@(Math.Min(pageCount, totalCount) + offset)</b> registros de <b>@totalCount</b> resultados
}

function initReports(containter) {
    const formData = generateFormData('reports', ['filtrarPorTipo', 'startDate', 'endDate', 'limitePorPagina']);

    const action = fetch(generateUrl('Reports/GenerateMovimentacaoEstoque', {
        startDate: formData['startDate'],
        endDate: formData['endDate'],
        type: formData['filtrarPorTipo'],
        limit: formData['limitePorPagina']
    }), {
        headers: {
            'Accepted': 'application/json; charset=utf-8',
        }
    });

    action
        .then((response) => response.json())
        .then((response) => processReportsData(containter, response));
}