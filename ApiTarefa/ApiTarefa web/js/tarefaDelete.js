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
    var rotaApi = "/tarefa";

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
        var botaoAlterar = '<button class="btn btn-primary btn-sm me-2" onclick="alterar(' + linha.identificadorTarefa + ')">Alterar</button>';
        var botaoExcluir = '<button class="btn btn-danger btn-sm" onclick="excluir(' + linha.identificadorTarefa + ')">Excluir</button>';

        htmlTabela = htmlTabela + `<tr><td>${linha.identificadorTarefa}</td><td>${linha.horarioInicio}</td><td>${linha.horarioFim}</td><td>${linha.descricaoResumida}</td><td>${linha.descricaoLonga}</td><td>${linha.tipoTarefa}</td><td>${formatarCnpj(linha.cnpj)}<td>${botaoAlterar + botaoExcluir}</td>/tr>`
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
    var cnpj = $("#inputCnpj").val();
    var razaoSocial = $("#inputRazaoSocial").val();
    var descricaoResumida = $("#inputDescricaoResumida").val();
    var descricaoLonga = $("#inputDescricaoLonga").val();

    var objeto = {
        cnpj: retirarMascaraCnpj(cnpj),
        razaoSocial: razaoSocial,
    };

    return objeto;
}

function enviarFormularioParaApi() {
    var rotaApi = '/tarefa';

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
            listarTarefas();
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Tarefa alterada com sucesso.',
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
}

function limparDadosFormulario() {
    $('#formTarefa').trigger("reset");
}

function submeterFormulario() {
    var isValid = $('#formTarefa').parsley().validate();
    if (isValid)
        enviarFormularioParaApi();
}

function excluir(identificadorTarefa) {
    Swal.fire({
        title: 'Você quer excluir essa tarefa?',
        showDenyButton: true,
        confirmButtonText: 'Sim',
        denyButtonText: `Não`,
    }).then((result) => {
        if (result.isConfirmed) {
            enviarExclusao(identificadorTarefa);
        } else if (result.isDenied) {
            Swal.fire('Nada foi alterado.', '', 'info')
        }
    });
}

function enviarExclusao(identificadorTarefa) {
    var rotaApi = '/tarefa/' + identificadorTarefa;

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'DELETE',
    }).done(function () {
        listarTarefas();
        Swal.fire('Tarefa excluida com sucesso.', '', 'success');
    });
}

function alterar(identificadorTarefa) {
    var rotaApi = '/tarefa/' + identificadorTarefa;

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
