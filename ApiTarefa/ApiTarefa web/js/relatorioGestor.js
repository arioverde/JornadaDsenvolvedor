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

function aplicarFiltros() {

    var htmlTabelaFiltro = "";
    //var filtroTarefas = parseInt($('#selectPeriodoTarefas').val());
    var filtroCliente = $('#selectCliente :selected').text();
    var filtroColaborador = $('#selectColaborador :selected').text();

    var tarefasFiltradas = new Array();

    $(linhasFiltro).each(function (index, linhaFiltro) {
        if (linhaFiltro.razaoSocial == filtroCliente)
            tarefasFiltradas.push(linhaFiltro)
        else return;

    });

    $(tarefasFiltradas).each(function (index2, linhaFiltro) {
        if (linhaFiltro.nomeColaborador != filtroColaborador) {
            tarefasFiltradas.remove(linhaFiltro)
        }
    });

    $(tarefasFiltradas).each(function (index3, linhaFiltro) {
        htmlTabelaFiltro = htmlTabelaFiltro + `<tr><td>${tarefasFiltradas.identificadorTarefa}</td><td>${formatarData(tarefasFiltradas.horarioInicio)}</td><td>${formatarData(linhaFiltro.horarioFim)}</td><td>${linhaFiltro.descricaoResumida}</td><td>${linhaFiltro.descricaoLonga}</td><td>${linhaFiltro.tipoTarefa}</td><td>${linhaFiltro.razaoSocial}</td><td>${linhaFiltro.nomeColaborador}</td></tr>`
    });

    $('#tabelaTarefas tbody').html(htmlTabelaFiltro);
    if (tabelaTarefas == undefined) {
        tabelaTarefas = $('#tabelaTarefas').DataTable({
            language: {
                url: 'dist/datatables/i18n.json'
            }
        });
    }
}

function popularSelectEmpresas(empresas) {

    if (empresas != null) {
        var selectbox = $('#selectCliente');
        $.each(empresas, function (i, d) {
            $('<option>').text(d.razaoSocial).appendTo(selectbox);
        });
    }
}

function listarTarefas() {
    var rotaApi = "/tarefa";

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (resultado) {
        // var global para uso nos filtros
        linhasFiltro = resultado;
        construirTabela(resultado);
    }).fail(function (err, errr, errrr) {
    });
}

function construirTabela(linhas) {

    var htmlTabela = '';
    var nomeColaboradores = new Array();
    $(linhas).each(function (index, linha) {

        if (linha.descricaoResumida == 'string')
            linha.descricaoResumida = '';
        if (linha.descricaoLonga == 'string')
            linha.descricaoLonga = '';
        if (linha.tipoTarefa == 1)
            linha.tipoTarefa = 'Reunião'
        if (linha.tipoTarefa == 2)
            linha.tipoTarefa = 'Quebra de contexto'
        if (linha.tipoTarefa == 3)
            linha.tipoTarefa = 'Tarefa'

        htmlTabela = htmlTabela + `<tr><td>${linha.identificadorTarefa}</td><td>${formatarData(linha.horarioInicio)}</td><td>${formatarData(linha.horarioFim)}</td><td>${linha.descricaoResumida}</td><td>${linha.descricaoLonga}</td><td>${linha.tipoTarefa}</td><td>${linha.razaoSocial}</td><td>${linha.nomeColaborador}</td></tr>`

        if (nomeColaboradores.includes(linha.nomeColaborador)) {
            return;
        }
        else
            nomeColaboradores.push(linha.nomeColaborador);

    });

    $('#tabelaTarefas tbody').html(htmlTabela);
    if (tabelaTarefas == undefined) {
        tabelaTarefas = $('#tabelaTarefas').DataTable({
            language: {
                url: 'dist/datatables/i18n.json'
            }
        });
    }

    popularSelectColaboradores(nomeColaboradores)
}

function popularSelectColaboradores(nomeColaboradores) {

    if (nomeColaboradores != null) {
        var selectbox = $('#selectColaborador');
        $.each(nomeColaboradores, function (i, d) {
            $('<option>').text(d).appendTo(selectbox);
        });
    }
}
