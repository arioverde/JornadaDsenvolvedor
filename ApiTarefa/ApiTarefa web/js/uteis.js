$(document).ready(function () {
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });

    $.ajaxSetup({
        headers: { 'Authorization': 'Bearer ' + localStorage.getItem('bearer') },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 400) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: jqXHR.responseText,
                })
            } else if (jqXHR.status == 401) {
                Swal.fire({
                    icon: 'infor',
                    title: 'Oops...',
                    text: 'As suas credenciais expiraram. Faça o login novamente.',
                }).then((result) => {
                    window.location.href = "index.html";
                });
            } else if (jqXHR.status == 403) {
                Swal.fire({
                    icon: 'infor',
                    title: 'Acesso Negado',
                    text: 'Você não tem permissão para acessar esse recurso.',
                }).then((result) => {
                    limparDadosFormulario();
                });
            } else if (jqXHR.status == 0) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'O nosso sistema ou sua internet está indisponível, tente mais tarde.',
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: '',
                });
            }
        }
    });
});

function formatarData(dataString) {
    if (dataString == null) {
        return "00:00";
    }
    var dataOriginal = new Date(dataString);
    var dia = adicionaZeroDatas(dataOriginal.getDate().toString());
    var mes = adicionaZeroDatas((dataOriginal.getMonth() + 1).toString());
    var ano = dataOriginal.getFullYear().toString();
    var hora = adicionaZeroHora(dataOriginal.getHours().toString());
    var min = adicionaZeroDatas(dataOriginal.getMinutes().toString());
    var dataFormatada = `${dia}/${mes}/${ano} ${hora}:${min}`;
    return dataFormatada;
}

function adicionaZeroDatas(numero) {
    if (numero <= 9)
        return '0' + numero;
    else
        return numero;
}

function adicionaZeroHora(numero) {
    if (numero <= 9)
        return '0' + numero;
    else
        return numero;
}

function formatarCnpj(cnpjString) {

    var num1 = cnpjString.slice(0, 2);
    var num2 = cnpjString.slice(2, 5);
    var num3 = cnpjString.slice(5, 8);
    var num4 = cnpjString.slice(8, 12);
    var digitoVerificador = cnpjString.slice(12, 14);
    var cnpjFormatado = `${num1}.${num2}.${num3}/${num4}-${digitoVerificador}`;

    return cnpjFormatado;
}

function retirarMascaraCnpj(cnpjMascara) {
    return cnpjMascara.replace(/\./g, '').replace(/\//g, '').replace(/\-/g, '');
}

var nivelAcesso = localStorage.getItem('nivelAcesso');
var identificadorTarefaAlterar;
