$(document).ready(function () {
    listarEmpresas();
    $(".preloading").hide();


});

var urlBaseApi = "https://localhost:44375";
var tabelaTarefas;
function limparCorpoTabelaTarefas() {
    var componenteSelecionado = $('#tabelaTarefas tbody');
    componenteSelecionado.html('');
}

function listarEmpresas() {
    var rotaApi = "/empresa";

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (resultado) {
        construirTabela(resultado);
    }).fail(function (err, errr, errrr) {
    });
}

function construirTabela(linhas) {

    var htmlTabela = '';

    $(linhas).each(function (index, linha) {
        var botaoSelecionar = '<button class="btn btn-primary btn-sm me-2" onclick="selecionar(' + linha.cnpj + ')">Selecionar</button>';

        htmlTabela = htmlTabela + `<tr><td>${formatarCnpj(linha.cnpj)}</td><td>${linha.razaoSocial}</td><td>${botaoSelecionar}</td>/tr>`
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

function obterValoresFormulario() {
    var descricaoResumida = $("#inputDescricaoResumida").val();
    var descricaoLonga = $("#inputDescricaoLonga").val();
    var tipoTarefa = parseInt($("#inputTipoTarefa option:selected").val());
    var email = localStorage.getItem('emailUsuario');
    var cnpj = $("#inputCnpj").val();
    var razaoSocial = $("#inputRazaoSocial").val();

    var objeto = {
        descricaoResumida: descricaoResumida,
        descricaoLonga: descricaoLonga,
        tipoTarefa: tipoTarefa,
        email: email,
        razaoSocial: razaoSocial,
        cnpj: retirarMascaraCnpj(cnpj)
    };

    return objeto;
}

function enviarFormularioParaApi() {
    var rotaApi = '/tarefa';

    var objeto = obterValoresFormulario();
    var json = JSON.stringify(objeto);

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'POST',
        data: json,
        contentType: 'application/json'
    }).done(function () {
        limparDadosFormulario();
        listarTarefas();
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Tarefa adicionada com sucesso.',
            showConfirmButton: false,
            timer: 1500
        });
    });
}

function limparDadosFormulario() {
    $('#formTarefa').trigger("reset");
}

function selecionar(cnpj) {

    var rotaApi = '/tarefa';
    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (linhas) {
        $(linhas).each(function (index, linha) {

            if ((linha.email == localStorage.emailUsuario) & (linha.horarioFim == null)) {
                Swal.fire({
                    icon: 'infor',
                    title: 'Oops...',
                    text: 'Existe tarefa sem finalizar. Você será redirecionado para a página de finalização de tarefas.',
                }).then((result) => {
                    window.location.href = "tarefaFinalizar.html";
                });
            }
        });
    })

    var rotaApi = '/empresa/' + cnpj;
    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (resultado) {
        $("#inputCnpj").val(formatarCnpj(resultado.cnpj));
        $("#inputRazaoSocial").val(resultado.razaoSocial);

        $("#inputCnpj").prop("disabled", true);
        $("#inputRazaoSocial").prop("disabled", true);
    });
}
