$("#formularioLogin").submit(function (e) {
    e.preventDefault();


    var inputsFormulario = $('#formularioLogin').find(':input');

    var objeto = {
        email: inputsFormulario.eq(0).val(),
        senha: inputsFormulario.eq(1).val()
    };
    var objetoJson = JSON.stringify(objeto);

    $.ajax({
        url: 'https://localhost:44375/Autorizacao',
        method: 'POST',
        data: objetoJson,
        contentType: 'application/json',
        dataType: 'json'
    }).done(function (resposta) {
        SalvarDadosLogin(resposta);
        if (localStorage.nivelAcesso == 1) {
            window.location.href = "tarefa.html";
        }
        else {
            window.location.href = "empresa.html";
        }

    }).fail(function (err, errr, errrr) {
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Usuário ou senha inválida!',
            showConfirmButton: false,
            timer: 1500
        });
    });
});

function SalvarDadosLogin(dadosToken) {
    localStorage.setItem('bearer', dadosToken.bearer);
    localStorage.setItem('nomeUsuario', dadosToken.nomeUsuario);
    localStorage.setItem('nivelAcesso', dadosToken.nivelAcesso);
    localStorage.setItem('emailUsuario', dadosToken.emailUsuario);
}
