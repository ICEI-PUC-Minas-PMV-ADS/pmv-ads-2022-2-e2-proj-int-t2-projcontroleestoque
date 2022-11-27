document.addEventListener('DOMContentLoaded', function () {
    const incluirProduto = document.getElementById('escolherProduto_submit');
    incluirProduto.addEventListener('click', function () {
        if (window.selectedId === null) {
            alert('Você deve selecionar um produto.');
            return;
        }

        /*
        if (window.productList.hasOwnProperty(window.selectedId)) {
            if (!confirm('Este produto já foi registrado.')) {
                return;
            }
        }
        */

        if (window.selectedId !== null && window.selectedId !== undefined) {
            initCriarItemDoInventario();
        }
    });
});

function initCriarItemDoInventario() {
    let action = fetch(`/Products/Get?productId=${window.selectedId}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Accept': 'application/json; charset=utf-8',
        },
    });

    action = action.then((response) => response.json());
    action = action.then((response) => processCriarItemDoInventario(response));
}

function processCriarItemDoInventario({ value: { status, data } }) {
    if (status === 200) {
        const { result } = data;
        const container = document.getElementById('criarItemDoInventarioContainer');
        
        container.replaceChildren(
            createElement('h5', null, 'Registrar no inventário'),
            createElement('hr'),
            createElement('div', { className: 'row' }, [
                createInput('itemInventario_Quantidade', 'number', 'Quantidade atual', { min: 0 }, ""),
                createSelect('itemInventario_Status', 'Status', [
                    { value: 'Está em conformidade', text: 'Está em conformidade' },
                    { value: 'Não está em conformidade', text: 'Não está em conformidade' },
                ]),
                createArea('itemInventario_Observacao', 'Observação', { containerClassName: 'mb-3 col-12' }),                
            ]),
            createElement('h5', null, 'Dados do produto'),
            createElement('hr'),
            createElement('div', { className: 'row' }, [
                createInput('product_Id', 'hidden', null, null, result.id),
                createInput('product_Nome', 'text', 'Produto', { readOnly: true }, result.nome),
                createInput('product_Descricao', 'text', 'Descrição', { readOnly: true }, result.descricao),
                createInput('product_Quantidade', 'text', 'Quantidade', { readOnly: true }, result.quantidade),
                createInput('product_Fornecedor', 'text', 'Fornecedor', { readOnly: true }, result.fornecedor?.nome),
                createInput('product_Localizacao', 'text', 'Localização', { readOnly: true }, result.localizacao),
                createInput('product_Tags', 'text', 'Tags', { readOnly: true }, result.tags),
                createInput('product_EstrategiaConsumo', 'text', 'Estrategia de consumo', { readOnly: true }, result.estrategiaConsumo),
                createInput('product_Validade', 'text', 'Validade', { readOnly: true }, formatDate(result.validade)),
            ]),
        );

        $('#escolherProduto').modal('hide');
        $('#registrarNoInventario').modal('show');
    }
}

function createArea(id, label, options) {
    const className = options?.containerClassName ? options?.containerClassName : 'mb-3 col-6';

    return createElement('div', { className }, [
        createElement('label', { for: id, className: 'form-label' }, label),
        createElement('textarea', { id, className: 'form-control' })
    ]);
}

function createInput(id, type, label, options, value) {
    const className = options?.containerClassName ? options?.containerClassName : 'mb-3 col-6';

    return type === 'hidden'
        ? createElement('input', { id, type, value })
        : createElement('div', { className }, [
            createElement('label', { for: id, className: 'form-label' }, label),
            createElement('input', { id, type, value, className: 'form-control', min: options?.min, readOnly: options?.readOnly })]);
}

function createSelect(id, label, items) {
    return createElement('div', { className: 'col-6 mb-3' }, [
        createElement('label', { for: id, className: 'form-label' }, label),
        createElement('select', { id, className: 'form-select' }, [
            createElement('option', { selected: true, disabled: true }, '-'),
            ...items?.map((item) => createElement('option', { value: item.value }, item.text))
        ])
    ]);
}