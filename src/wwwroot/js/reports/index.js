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