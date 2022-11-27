document.addEventListener("DOMContentLoaded", function () {
    const form_periodico_semanal = document.getElementById('periodico_semanal');
    const form_periodico_mensal = document.getElementById('periodico_mensal');
    const form_extraordinario = document.getElementById('extraordinario');
    const container_botao = document.getElementById('container_botao');
    const selector = document.getElementById('inventario_type');

    initInventarioMensal();

    selector.addEventListener('change', function () {
        const selected_option = selector.value;

        switch (selected_option) {
            case 'Periodico semanal':
                form_periodico_semanal.style.display = 'block';
                form_periodico_mensal.style.display = 'none';
                form_extraordinario.style.display = 'none';
                container_botao.style.display = 'block';
                break;

            case 'Periodico mensal':
                form_periodico_semanal.style.display = 'none';
                form_periodico_mensal.style.display = 'block';
                form_extraordinario.style.display = 'none';
                container_botao.style.display = 'block';
                break;

            case 'Extraordinario':
                form_periodico_semanal.style.display = 'none';
                form_periodico_mensal.style.display = 'none';
                form_extraordinario.style.display = 'block';
                container_botao.style.display = 'block';
                break;
        }
    });

    const confirmButton = document.getElementById('confirm_button');

    confirmButton.addEventListener('click', function () {
        const selected_option = selector.value;

        switch (selected_option) {
            case 'Periodico semanal': return sendPeriodicoSemanal();
            case 'Periodico mensal':  return sendPeriodicoMensal();
            case 'Extraordinario':    return sendExtraordinario();
        }
    });
});

function initInventarioMensal() {
    const selector = document.getElementById('inventory_date_periodico_mensal');

    // Inicia 
    for (let i = 1; i < 31; i++) {
        const child = document.createElement('option');

        child.value = i;
        child.innerText = i;

        selector.appendChild(child);
    }
}

function sendPeriodicoSemanal() {
    const value = document.getElementById('inventory_date_periodico_semanal').value;

    const body = {
        tipo: 'periodico_semanal',
        semanal_value: value,
    };

    let action = fetch('Inventario/SchedulyInventory', {
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
            'Content-type': 'application/json; charset=utf-8',
            'Accept': 'application/json; charset=utf-8',
        }
    });

    action = action.then((response) => response.json());
    action.then((response) => {
        const { status, data } = response.value;

        console.log(data);

        if (status == 200) {

        }
    });
}

function sendPeriodicoMensal() {
    const value = document.getElementById('inventory_date_periodico_mensal').value;
    const proximoDiaUtil = document.getElementById('inventory_date_periodico_mensal_primeiro_dia_util').checked;

    const body = {
        tipo: 'periodico_mensal',
        mensal_value: value,
        proximo_dia_util: proximoDiaUtil,
    };

    let action = fetch('Inventario/SchedulyInventory', {
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
            'Content-type': 'application/json; charset=utf-8',
            'Accept': 'application/json; charset=utf-8',
        }
    });

    action = action.then((response) => response.json());
    action.then((response) => {
        const { status, data } = response.value;

        console.log(data);

        if (status == 200) {

        }
    });
}

function sendExtraordinario() {
    const value = document.getElementById('inventory_date_extraordinario').value;

    const body = {
        tipo: 'extraordinario',
        extraordinario_value: value,
    };

    let action = fetch('Inventario/SchedulyInventory', {
        method: 'POST',
        body: JSON.stringify(body),
        headers: {
            'Content-type': 'application/json; charset=utf-8',
            'Accept': 'application/json; charset=utf-8',
        }
    });

    action = action.then((response) => response.json());
    action.then((response) => {
        const { status, data } = response.value;

        console.log(data);

        if (status == 200) {

        }
    });
}