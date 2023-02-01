// ocultarElementosMarketing();
// function ocultarElementosMarketing() {
//     if (nivelAcesso == '1') {
//         $("#cardCadastroEmpresa").show();
//     }
// }

$(document).ready(function () {
    listarEmpresas();
    $(".preloading").hide();
});

var urlBaseApi = "https://localhost:44375";
var tabelaEmpresas;
function limparCorpoTabelaEmpresas() {
    var componenteSelecionado = $('#tabelaEmpresas tbody');
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
        htmlTabela = htmlTabela + `<tr><td>${formatarCnpj(linha.cnpj)}</td><td>${linha.razaoSocial}</td><td>${formatarData(linha.dataCadastro)}</td></tr>`
    });
    $('#tabelaEmpresas tbody').html(htmlTabela);
    if (tabelaEmpresas == undefined) {
        tabelaEmpresas = $('#tabelaEmpresas').DataTable({
            language: {
                url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/pt-BR.json'
            }
        });
    }
}

function obterValoresFormulario() {
    var cnpj = $("#inputCnpj").val();
    var razaoSocial = $("#inputRazaoSocial").val();

    var objeto = {
        cnpj: retirarMascaraCnpj(cnpj),
        razaoSocial: razaoSocial,
    };

    return objeto;
}

function enviarFormularioParaApi() {
    var rotaApi = "/empresa";

    var objeto = obterValoresFormulario();
    var objetoJson = JSON.stringify(objeto);

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'POST',
        data: objetoJson,
        contentType: "application/json"
    }).done(function () {
        limparDadosFormulario();
        listarEmpresas();
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Empresa cadastrada com sucesso!',
            showConfirmButton: false,
            timer: 1500
        });
    });
}

function limparDadosFormulario() {
    $('#formEmpresa').trigger("reset");
}

function submeterFormulario() {
    var isValid = $('#formEmpresa').parsley().validate();
    if (isValid)
        enviarFormularioParaApi();
}

