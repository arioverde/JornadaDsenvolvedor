$(document).ready(function () {
    listarTarefas();
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

    var filtroColaborador = $('#selectColaborador').val();
    if (filtroColaborador != "")
        filtroColaborador = $('#selectColaborador :selected').text();

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

function listarTarefas() {
    var rotaApi = "/tarefa";

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (resultado) {
        popularSelectColaboradores(resultado)
    }).fail(function (err, errr, errrr) {
    });
}

function construirTabela(linhas) {

    var tempoTotalEmMinutos = 0;
    var htmlTabela = '';
    $(linhas).each(function (index, linha) {

        var horas = parseInt((linha.tempoTarefa).slice(0, 2));
        var minutos = parseInt((linha.tempoTarefa).slice(3, 5));

        tempoTotalEmMinutos += (horas * 60) + minutos;
        if (linha.tipoTarefa == 1)
            linha.tipoTarefa = 'Reuni??o'
        if (linha.tipoTarefa == 2)
            linha.tipoTarefa = 'Quebra de contexto'
        if (linha.tipoTarefa == 3)
            linha.tipoTarefa = 'Tarefa'

        htmlTabela = htmlTabela + `<tr><td>${linha.identificadorTarefa}</td><td>${formatarData(linha.horarioInicio)}</td><td>${formatarData(linha.horarioFim)}</td><td>${linha.descricaoResumida}</td><td>${linha.descricaoLonga}</td><td>${linha.tipoTarefa}</td><td>${linha.razaoSocial}</td><td>${linha.nomeColaborador}</td></tr>`
    });

    $('#totalHoras').val(converterMinutos(tempoTotalEmMinutos));

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

function popularSelectColaboradores(colaboradores) {

    var nomeColaboradores = new Array();
    $(colaboradores).each(function (index, colaborador) {

        if (nomeColaboradores.includes(colaborador.nomeColaborador)) {
            return;
        }
        else
            nomeColaboradores.push(colaborador.nomeColaborador);
    });

    if (nomeColaboradores != null) {
        var selectbox = $('#selectColaborador');
        $.each(nomeColaboradores, function (i, d) {
            $('<option>').text(d).appendTo(selectbox);
        });
    }
}

function converterMinutos(tempoTotalEmMinutos) {
    var horas = Math.floor(tempoTotalEmMinutos / 60);
    var min = tempoTotalEmMinutos % 60;
    var textoHoras = (`00${horas}`).slice(-2);
    var textoMinutos = (`00${min}`).slice(-2);

    return `${textoHoras}:${textoMinutos}`;
};


