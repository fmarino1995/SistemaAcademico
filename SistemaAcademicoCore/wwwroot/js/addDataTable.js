function AddDataTable(table, searchble) {
    $(document).ready(function () {
        $(table).dataTable({
            "retrieve" : true,
            "searching": searchble,
            "ordering": false,
            "pagingType": "full_numbers",
            "iDisplayLength": 50,
            "language": {
                "lengthMenu": "Mostrar _MENU_ registros",
                "emptyTable": "Nenhum registro disponível na tabela",
                "infoFiltered": "(Filtrando de _MAX_ total de registros)",
                "zeroRecords": "Nenhum registro encontrado",
                "search": "Pesquisar",
                "infoEmpty": "Mostrando 0 até 0 de 0 registros",
                "info": "Mostrando _START_ até _END_ de _TOTAL_ registros",
                "paginate": {
                    "first": "Primeira página",
                    "last": "Última página ",
                    "next": "Próximo",
                    "previous": "Anterior"
                }
            }
        });
    });
}