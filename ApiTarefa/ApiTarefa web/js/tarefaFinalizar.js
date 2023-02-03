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
        var botaoFinalizar = '<button class="btn btn-primary btn-sm me-2" onclick="finalizar(' + linha.identificadorTarefa + ')">Finalizar</button>';

        if (linha.email != localStorage.emailUsuario)
            return;
        if (linha.horarioFim != null)
            return;

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

        htmlTabela = htmlTabela + `<tr><td>${linha.identificadorTarefa}</td><td>${formatarData(linha.horarioInicio)}</td><td>${formatarData(linha.horarioFim)}</td><td>${linha.descricaoResumida}</td><td>${linha.descricaoLonga}</td><td>${linha.tipoTarefa}</td><td>${linha.razaoSocial}<td>${botaoFinalizar}</td>/tr>`
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

function finalizar(identificadorTarefa) {
    Swal.fire({
        title: 'Você quer finalizar essa tarefa?',
        showDenyButton: true,
        confirmButtonText: 'Sim',
        denyButtonText: `Não`,
    }).then((result) => {
        if (result.isConfirmed) {
            enviarFinalizacao(identificadorTarefa);
        } else if (result.isDenied) {
            Swal.fire('Nada foi alterado.', '', 'info')
        }
    });
}

function enviarFinalizacao(identificadorTarefa) {
    var rotaApi = '/tarefa/' + identificadorTarefa;

    $.ajax({
        url: urlBaseApi + rotaApi,
        method: 'PUT',
    }).done(function () {
        listarTarefas();
        Swal.fire('Tarefa finalizada com sucesso.', '', 'success');
    });
}
