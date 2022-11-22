document.addEventListener("DOMContentLoaded", function () {
    const containter = document.getElementById("content");
    const modal = $('#movimentacaoDeEstoque');

    // Submit modal de movimentacaoDeEstoque

    let queryMovimentacaoDeEstoque = document.getElementById("movimentacaoDeEstoque_submit");
    queryMovimentacaoDeEstoque.addEventListener("click", function () {
        initMovimentacaoDeEstoqueData(containter, modal);
    });
});

/**
 * Movimentacao de estoque
 */

function initMovimentacaoDeEstoqueData(containter, modal) {
    const formData = generateFormData('movimentacaoDeEstoque', ['filtrarPorTipo', 'startDate', 'endDate', 'limitePorPagina']);

    const action = fetch('Reports/GenerateMovimentacaoEstoque', {
        method: 'POST',
        body: JSON.stringify({
            startDate: formData['startDate'],
            endDate: formData['endDate'],
            type: formData['filtrarPorTipo'],
            limit: formData['limitePorPagina']
        }),
        headers: {
            'Accepted': 'application/json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });

    action
        .then((response) => response.json())
        .then((response) => processMovimentacaoDeEstoqueData(containter, modal, response));
}

function processMovimentacaoDeEstoqueData(container, modal, response) {
    const { status, data } = response.value;
    const { count, totalCount, startDate, endDate, type, limit, offset } = data;

    const BACK_PAGE = -1;
    const NEXT_PAGE = 1;

    const hasPrev = offset > 0;
    const hasNext = offset + count < totalCount;

    const createTooltipTH = (value) => createElement('th', { data: { toggle: 'tooltip', placement: 'top' }, title: value }, value);
    const createTooltipTD = (value) => createElement('td', { data: { toggle: 'tooltip', placement: 'top' }, title: value }, value);

    const updateReports = (container, offset) => {
        const action = fetch('Reports/GenerateMovimentacaoEstoque', {
            method: 'POST',
            body: JSON.stringify({
                startDate,
                endDate,
                type,
                limit,
                offset
            }),
            headers: {
                'Accepted': 'application/json; charset=utf-8',
                'Content-Type': 'application/json; charset=utf-8'
            }
        });

        action
            .then((response) => response.json())
            .then((response) => processMovimentacaoDeEstoqueData(container, modal, response));
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
                                    createElement('li', { className: (hasPrev ? 'enabled' : 'disabled'), events: { click: () => { onPagClick(hasPrev, BACK_PAGE); } } }, '<'),
                                    createElement('li', { className: (hasNext ? 'enabled' : 'disabled'), events: { click: () => { onPagClick(hasNext, NEXT_PAGE); } } }, '>'),
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

        modal.modal('hide');
    } else {
        alert('Erro inesperado!');
    }
}
