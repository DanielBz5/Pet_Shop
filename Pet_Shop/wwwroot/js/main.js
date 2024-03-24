
import * as servicosCtrl from './controllers/ServicosController.js';
import * as produtosCtrl from './controllers/ProdutoController.js';


//View Agendamentos - Atualiza Valor ao selecionar serviço
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('ListaServicos').addEventListener('change', servicosCtrl.ValorServico);
});


//Exibição Deleta Produto
document.getElementById('btn-deleta-produto').addEventListener('click', produtosCtrl.DeleteProduto);


var total = 0;
var itemsSelecionados = [];//matriz

function SelectServico(Cod, div) {
    var servico;
    Cod = Cod - 1;

    if (div.getAttribute('data-parametro') === 'false') {
        // Seleciona item
        div.classList.add('card-selecionado');
        div.setAttribute('data-parametro', 'true');

        servico = ListaValorOrcamento[Cod];
        total = total + servico;
        document.getElementById("total-orcamento").innerHTML = "Total: " + "R$ " + total + ",00";

        itemsSelecionados.push({ Cod: Cod, servico: ListaOrcamente[Cod], valor: servico });//adiciona na matriz

    } else {
        // Desseleciona item
        div.setAttribute('data-parametro', 'false');
        div.classList.remove('card-selecionado');

        servico = ListaValorOrcamento[Cod];
        total = total - servico;
        document.getElementById("total-orcamento").innerHTML = "Total: " + "R$ " + total + ",00";

        var indexToRemove = itemsSelecionados.findIndex(item => item.Cod === Cod);//procura na matriz
        if (indexToRemove !== -1) {
            itemsSelecionados.splice(indexToRemove, 1);//remove da matriz
        }
    }

    //exibe matriz
    var servicoSelect = document.getElementById("servico-select");
    servicoSelect.innerHTML = itemsSelecionados.map(item => item.servico + ": " + "R$ " + item.valor + ",00").join("<br>");
}





//Escolhe modelo do Relatorio

function selectModel() {

    var relatorio = document.getElementById('select-relatorio').value;

    if (relatorio === 'Produto') {
        document.getElementById('Rel-produto').style.display = 'flex';
        document.getElementById('Rel-estoque').style.display = 'none';
    } else if (relatorio === 'Estoque') {
        document.getElementById('Rel-estoque').style.display = 'flex';
        document.getElementById('Rel-produto').style.display = 'none';
    } else {
        document.getElementById('Rel-produto').style.display = 'none';
        document.getElementById('Rel-estoque').style.display = 'none';
    }
}



function AvisoLoginTeste() {
    alert("PARA TESTE Usuarios: Admin ou Daniel/ senha:123");
}

