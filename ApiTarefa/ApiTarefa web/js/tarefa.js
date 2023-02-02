// ocultarElementos();
// function ocultarElementos() {
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
        var botaoSelecionar = '<button class="btn btn-primary btn-sm me-2" onclick="selecionar(' + linha.cnpj + ')">Selecionar</button>';

        htmlTabela = htmlTabela + `<tr><td>${formatarCnpj(linha.cnpj)}</td><td>${linha.razaoSocial}</td><td>${botaoSelecionar}</td>/tr>`
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
    var descricaoResumida = $("#inputDescricaoResumida").val();
    var descricaoLonga = $("#inputDescricaoLonga").val();
    var tipoTarefa = parseInt($("#inputTipoTarefa option:selected").val());
    var email = localStorage.getItem('emailUsuario');
    var cnpj = $("#inputCnpj").val();

    var objeto = {
        descricaoResumida: descricaoResumida,
        descricaoLonga: descricaoLonga,
        tipoTarefa: tipoTarefa,
        email: email,
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
        listarEmpresas();
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

// function submeterFormulario() {
//     var isValid = $('#formEmpresa').parsley().validate();
//     if (isValid)
//         enviarFormularioParaApi();
// }

// function excluir(cnpj) {
//     Swal.fire({
//         title: 'Você quer excluir esse cliente?',
//         showDenyButton: true,
//         confirmButtonText: 'Sim',
//         denyButtonText: `Não`,
//     }).then((result) => {
//         if (result.isConfirmed) {
//             enviarExclusao(cnpj);
//         } else if (result.isDenied) {
//             Swal.fire('Nada foi alterado.', '', 'info')
//         }
//     });
// }

// function enviarExclusao(cnpj) {
//     var rotaApi = '/empresa/' + cnpj;

//     $.ajax({
//         url: urlBaseApi + rotaApi,
//         method: 'DELETE',
//     }).done(function () {
//         listarEmpresas();
//         Swal.fire('Cliente excluido com sucesso.', '', 'success');
//     });
// }

function selecionar(cnpj) {
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

// function botaoCancelar() {

//     limparDadosFormulario();
//     var isEdicao = $("#inputCnpj").is(":disabled");

//     if (isEdicao) {
//         voltarEstadoInsercaoFormulario();
//     } else {
//         limparDadosFormulario();
//     }
// }

// function voltarEstadoInsercaoFormulario() {
//     limparDadosFormulario();
//     $("#inputCnpj").prop("disabled", false);
// }
