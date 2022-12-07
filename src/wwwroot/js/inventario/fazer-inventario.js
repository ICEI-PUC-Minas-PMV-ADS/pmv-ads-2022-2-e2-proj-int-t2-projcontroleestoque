window.selectedId = null;
window.productList = window.sessionStorage.getItem('product_list') || {};

document.addEventListener("DOMContentLoaded", function () {
    const selecionarProdutosModal = document.getElementById("incluirProdutosModal");
    selecionarProdutosModal.addEventListener("click", function () {
        selectedId = null;
        initSelecionarProdutos();
    });

    const finalizarInventario = document.getElementById('finalizarInventario');
    finalizarInventario.addEventListener('click', function () {
        if (confirm('Você deseja finalizar este inventario(este procedimento não pode ser revertido)?')) {
            let action = fetch('/Inventario/Registrar', {
                method: 'POST',
                body: JSON.stringify(generateInventoryBody()),
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'Accept': 'application/json; charset=utf-8',
                }
            });

            action = action.then((response) => response.json());
            action = action.then((response) => {
                const { status, data } = response.value;

                if (status === 200) {
                    alert('O inventário foi finalizado e registrado com sucesso!');
                    window.location.href = '/Home/Index';
                }
            });
        }
    });
});

function initSelecionarProdutos() {
    let action = fetch("/Products/GetAll", {
        method: "GET",
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Accept': 'application/json; charset=utf-8',
        }
    });

    action = action.then((response) => response.json());
    action = action.then((response) => processSelecionarProdutoResponse(response));
    action = action.then(() => new bootstrap.Modal('#escolherProduto').show());
}

function processSelecionarProdutoResponse({ value: { status, data } }) {
    if (status === 200) {
        const { results, totalCount, count, limit, offset } = data;

        const containerBody = document.getElementById('products-body');
        const containerFoot = document.getElementById('products-foot');

        let selectedElement = null;

        const onClick = (event, target, id) => {
            if (selectedElement) {
                selectedElement.style.backgroundColor = 'white';
            }
            target.style.backgroundColor = 'yellow';

            window.selectedId = id;
            selectedElement = target;
        };

        /***
         * Pagination
         */

        const BACK_PAGE = -1;
        const NEXT_PAGE = 1;

        const hasPrev = offset > 0;
        const hasNext = offset + count < totalCount;

        const updateReports = (offset) => {
            const action = fetch(`/Products/GetAll?Offset=${offset}&Limit=${limit}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8',
                    'Accept': 'application/json; charset=utf-8',
                }
            });

            action
                .then((response) => response.json())
                .then((response) => processResponse(response));
        };

        const onPagClick = (enabled, index) => {
            if (enabled) {
                switch (index) {
                    case BACK_PAGE:
                        updateReports(Math.max(0, offset - limit));
                        break;

                    case NEXT_PAGE:
                        updateReports(offset + limit);
                        break;
                }
            }
        };

        containerBody.replaceChildren(
            ...results.map((r) => createElement('tr', { style: { backgroundColor: window.selectedId === r.id ? 'yellow' : 'white' }, events: { click: (event, target) => onClick(event, target, r.id) } }, [
                createElement('td', null, r.nome),
                createElement('td', null, r.descricao),
                createElement('td', null, r.quantidade),
                createElement('td', null, r.localizacao),
                createElement('td', null, r.tags),
                createElement('td', null, r.estrategiaConsumo),
                createElement('td', null, r.validade),
            ]))
        );

        containerFoot.replaceChildren(
            createElement('tr', { colspan: 7 }, [
                createElement('td', { className: 'd-flex justify-content-between', }, [
                    createElement('div', { className: 'me-4' }, [
                        createElement('ul', { className: 'c-pagination ' }, [
                            createElement('li', { className: (hasPrev ? 'enabled' : 'disabled'), events: { click: () => { onPagClick(hasPrev, BACK_PAGE); } } }, '<'),
                            createElement('li', { className: (hasNext ? 'enabled' : 'disabled'), events: { click: () => { onPagClick(hasNext, NEXT_PAGE); } } }, '>'),
                        ]),
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
        );
    }
}

function generateInventoryBody() {
    const mapObject = (invItem) => ({
        productId: invItem.productId,
        quantidade: invItem.quantidade,
        status: invItem.status,
        observacao: invItem.observacao,
    });

    return Object.keys(window.productList).map((key) => mapObject(window.productList[key]));
}