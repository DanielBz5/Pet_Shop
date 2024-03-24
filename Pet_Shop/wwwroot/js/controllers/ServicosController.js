import Servicos from '../models/Servicos.js';

export function ValorServico() {
    var CodServico = document.getElementById("ListaServicos").value;

    const url = `BuscaServicos?CodServico=${CodServico}`;
    fetch(url)
        .then(response => response.json())
        .then(data => {
            const servicos = new Servicos(data.cod, data.nome, data.valor, data.descricao);
            document.getElementById("CampoValor").innerHTML = "R$ " + servicos.valor + ".00";
        })
        .catch(error => console.error('Erro ao buscar servicos:', error));
}

