// ocultarElementos();
// function ocultarElementos() {
//     if (nivelAcesso == '1') {
//         $("#cardCadastroEmpresa").show();
//     }
// }

$(document).ready(function () {
    listarTarefas();
    $(".preloading").hide();
});

var urlBaseApi = "https://localhost:44375";
var tabelaTarefas;
function limparCorpoTabelaTarefas() {
    var componenteSelecionado = $('#tabelaTarefas tbody');
    componenteSelecionado.html('');
}

function listarTarefas() {
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

// function submeterFormulario() {
//     var isValid = $('#formTarefa').parsley().validate();
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
//         listarTarefas();
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
