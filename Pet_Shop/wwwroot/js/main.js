
import * as servicosCtrl from './controllers/ServicosController.js';
import * as produtosCtrl from './controllers/ProdutoController.js';


//View Agendamentos - Atualiza Valor ao selecionar serviço
const selectServico = document.getElementById('ListaServicos');
if (selectServico) {
    selectServico.addEventListener('change', servicosCtrl.ValorServico);
}

//Exibição Deleta Produto
const bntDelete = document.getElementById('btn-deleta-produto');
if (bntDelete) {
    bntDelete.addEventListener('click', produtosCtrl.DeleteProduto);
}


//-----------------------------------------------------------Shop  
//Mostra Produto
const buttonsCompra = document.querySelectorAll('.btn-comprar');
buttonsCompra.forEach(button => {
    button.addEventListener('click', produtosCtrl.MostraProduto);
});

const btnMais = document.getElementById('btn-mais');
if (btnMais) {
    btnMais.addEventListener('click', produtosCtrl.SomaQtd)
}
const btnMenos = document.getElementById('btn-menos');
if (btnMenos) {
    btnMenos.addEventListener('click', produtosCtrl.SubQtd)
}

const btnAddCarrinho = document.getElementById('btn-add-carrinho')
if (btnAddCarrinho) {
    btnAddCarrinho.addEventListener('click', produtosCtrl.AddCarrinho)
}


//Carrinho
const openCarrinho = document.getElementById('open-carrinho')
if (openCarrinho) {
    openCarrinho.addEventListener('click', produtosCtrl.LoadCarrinho)
}

document.body.addEventListener('click', function (event) {
    if (event.target.classList.contains('btn-remove-carrinho')) {
        const codRemove = event.target.id;
        produtosCtrl.RemoveLocalStorage(codRemove);
    }
});

const fecharCarrinho = document.getElementById('btn-fechar-carrinho')
if (fecharCarrinho) {
    fecharCarrinho.addEventListener('click', produtosCtrl.FecharCarrinho)
}

const ContinuaPedido = document.getElementById('continuar-pedido')
if (ContinuaPedido) {
    ContinuaPedido.addEventListener('click', produtosCtrl.EnviaPedido)
}

//ResultPedido
const btncopy = document.getElementById('copy-pix')
if (btncopy) {
    btncopy.addEventListener('click', produtosCtrl.CopiaChave)
}


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

