$(document).ready(function () {
    listarEmpresas();
    $(".preloading").hide();
});

var urlBaseApi = "https://localhost:44375";

function listarEmpresas() {
    var rotaApi = "/empresa";

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (resultado) {
        popularSelectEmpresas(resultado);
    }).fail(function (err, errr, errrr) {
    });
}
function obterValoresFormulario() {
    var filtroTarefas = parseInt($('#selectPeriodoTarefas').val());

    var filtroCliente = $('#selectCliente').val();
    if (filtroCliente != "")
        filtroCliente = $('#selectCliente :selected').text();

    var filtroColaborador = localStorage.getItem('nomeUsuario');

    if (filtroTarefas == 0) {
        if (filtroCliente == "" && filtroColaborador == "") {
            return;
        }
    }

    enviarFormularioParaApi(filtroTarefas, filtroCliente, filtroColaborador);
}

function enviarFormularioParaApi(filtroTarefas, filtroCliente, filtroColaborador) {
    var rotaApi = '/tarefa';

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json",
        data: { tarefasPorPeriodo: filtroTarefas, razaoSocial: filtroCliente, nomeColaborador: filtroColaborador },
    }).done(function (resultado) {
        construirTabela(resultado);
    }).fail(function (err, errr, errrr) {
    });
}

function construirTabela(linhas) {

    var htmlTabela = '';
    $(linhas).each(function (index, linha) {

        if (linha.tipoTarefa == 1)
            linha.tipoTarefa = 'Reunião'
        if (linha.tipoTarefa == 2)
            linha.tipoTarefa = 'Quebra de contexto'
        if (linha.tipoTarefa == 3)
            linha.tipoTarefa = 'Tarefa'

        htmlTabela = htmlTabela + `<tr><td>${linha.identificadorTarefa}</td><td>${formatarData(linha.horarioInicio)}</td><td>${formatarData(linha.horarioFim)}</td><td>${linha.descricaoResumida}</td><td>${linha.descricaoLonga}</td><td>${linha.tipoTarefa}</td><td>${linha.razaoSocial}</td></tr>`
    });

    $('#tabelaTarefas tbody').html(htmlTabela);
    if (tabelaTarefas == undefined) {
        tabelaTarefas = $('#tabelaTarefas').DataTable({
            language: {
                url: 'dist/datatables/i18n.json'
            }
        });
    }
}

function popularSelectEmpresas(selectEmpresas) {

    if (selectEmpresas != null) {
        var selectbox = $('#selectCliente');
        $.each(selectEmpresas, function (i, d) {
            $('<option>').text(d.razaoSocial).appendTo(selectbox);
        });
    }
}
