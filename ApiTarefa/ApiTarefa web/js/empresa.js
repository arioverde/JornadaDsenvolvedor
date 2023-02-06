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
        var botaoAlterar = '<button class="btn btn-primary btn-sm me-2" onclick="alterar(' + linha.cnpj + ')">Alterar</button>';
        var botaoExcluir = '<button class="btn btn-danger btn-sm" onclick="excluir(' + linha.cnpj + ')">Excluir</button>';

        htmlTabela = htmlTabela + `<tr><td>${formatarCnpj(linha.cnpj)}</td><td>${linha.razaoSocial}</td><td>${formatarData(linha.dataCadastro)}</td><td>${botaoAlterar + botaoExcluir}</td>/tr>`
    });
    $('#tabelaEmpresas tbody').html(htmlTabela);
    if (tabelaEmpresas == undefined) {
        tabelaEmpresas = $('#tabelaEmpresas').DataTable({
            language: {
                url: 'dist/datatables/i18n.json'
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
    var rotaApi = '/empresa';

    var objeto = obterValoresFormulario();
    var json = JSON.stringify(objeto);
    var isEdicao = $("#inputCnpj").is(":disabled");

    if (isEdicao) {
        $.ajax({
            url: urlBaseApi + rotaApi,
            method: 'PUT',
            data: json,
            contentType: 'application/json'
        }).done(function () {
            voltarEstadoInsercaoFormulario();
            listarEmpresas();
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cliente alterado com sucesso.',
                showConfirmButton: false,
                timer: 1500
            });
        });
    } else {
        $.ajax({
            url: urlBaseApi + rotaApi,
            method: 'POST',
            data: json,
            contentType: 'application/json'
        }).done(function () {
            limparDadosFormulario();
            listarEmpresas();
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Cliente adicionado com sucesso.',
                showConfirmButton: false,
                timer: 1500
            });
        });
    }
}

function limparDadosFormulario() {
    $('#formEmpresa').trigger("reset");
}

function submeterFormulario() {
    var isValid = $('#formEmpresa').parsley().validate();
    if (isValid)
        enviarFormularioParaApi();
}

function excluir(cnpj) {
    Swal.fire({
        title: 'Você quer excluir esse cliente?',
        showDenyButton: true,
        confirmButtonText: 'Sim',
        denyButtonText: `Não`,
    }).then((result) => {
        if (result.isConfirmed) {
            enviarExclusao(cnpj);
        } else if (result.isDenied) {
            Swal.fire('Nada foi alterado.', '', 'info')
        }
    });
}

function enviarExclusao(cnpj) {
    var rotaApi = '/empresa/' + cnpj;

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'DELETE',
    }).done(function () {
        listarEmpresas();
        Swal.fire('Cliente excluido com sucesso.', '', 'success');
    });
}

function alterar(cnpj) {
    var rotaApi = '/empresa/' + cnpj;

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'GET',
        dataType: "json"
    }).done(function (resultado) {
        $("#inputCnpj").val(formatarCnpj(resultado.cnpj));
        $("#inputRazaoSocial").val(resultado.razaoSocial);

        $("#inputCnpj").prop("disabled", true);
    });
}

function botaoCancelar() {
    var isEdicao = $("#inputCnpj").is(":disabled");

    if (isEdicao) {
        voltarEstadoInsercaoFormulario();
    } else {
        limparDadosFormulario();
    }
}

function voltarEstadoInsercaoFormulario() {
    limparDadosFormulario();
    $("#inputCnpj").prop("disabled", false);
}
