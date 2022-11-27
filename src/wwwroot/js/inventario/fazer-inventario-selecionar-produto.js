document.addEventListener("DOMContentLoaded", function () {
    const cadastrarItemInventario = document.getElementById('cadastrarItemInventario_submit');
    cadastrarItemInventario.addEventListener('click', function () {
        const itemInventoryFields = generateFormDataWithFields('itemInventario', [
            'Quantidade',
            'Status',
            'Observacao',
        ]);

        const productFields = generateFormData('product', [
            'Id',
            'Nome',
            'Descricao',
            'Quantidade',
            'Fornecedor',
            'Localizacao',
            'Tags',
            'EstrategiaConsumo',
            'Validade'
        ]);

        const validateField = (field) => (field.value === null || field.value === undefined || field.value.trim().length === 0);

        itemInventoryFields['Quantidade'].el.style.border = '1px solid #ced4da';
        if (validateField(itemInventoryFields['Quantidade'])) {
            alert('Todos os campos obrigatorios devem ser prenchido!');
            fields['Quantidade'].el.style.border = '1px solid red';
            return;
        }

        itemInventoryFields['Status'].el.style.border = '1px solid #ced4da';
        if (validateField(itemInventoryFields['Status'])) {
            alert('Todos os campos obrigatorios devem ser prenchido!');
            fields['Status'].el.style.border = '1px solid red';
            return;
        }

        if (productList.hasOwnProperty(selectedId)) {
            if (!confirm('Este item ja foi cadastrado, deseja substituir o cadastro anterior?')) {
                $('#registrarNoInventario').modal('hide');
                return;
            }
        }

        window.productList[selectedId] = {
            productId: productFields['Id'],
            productNome: productFields['Nome'],
            productDescricao: productFields['Descricao'],
            productQuantidade: productFields['Quantidade'],
            productFornecedor: productFields['Fornecedor'],
            productLocalizacao: productFields['Localizacao'],
            productTags: productFields['Tags'],
            productEstrategiaConsumo: productFields['EstrategiaConsumo'],
            productValidade: productFields['Validade'],
            quantidade: itemInventoryFields['Quantidade'].value,
            status: itemInventoryFields['Status'].value,
            observacao: itemInventoryFields['Observacao'].value,
        };

        // window.sessionStorage.setItem('product_list', window.productList);

        updateProductTable();

        alert("Registro efetuado com sucesso!");
        $('#registrarNoInventario').modal('hide');
    });
});

function updateProductTable() {
    const container = document.getElementById('inventoryItemList');
    const checkStatus = (status) => status === 'Está em conformidade' ? 'green' : 'red';

    const mapObject = (invItem) => createElement('tr', {}, [
        createElement('td', {}, invItem.productNome),        
        createElement('td', {}, invItem.productFornecedor),
        createElement('td', {}, invItem.productQuantidade),
        createElement('td', {}, invItem.quantidade),
        createElement('td', { style: { color: checkStatus(invItem.status) } }, invItem.status),
    ]);

    container.replaceChildren(...Object.keys(window.productList).map((key) => mapObject(window.productList[key])));
}