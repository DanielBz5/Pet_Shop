
function ValorServico() {

    var nome = document.getElementById("ListaServicos").value;
    valor = nome - 1;
    if (valor == -1) {
        document.getElementById("CampoValor").innerHTML = "R$ 0.00";
    }
    else {
        document.getElementById("CampoValor").innerHTML = "R$ " + servicosData[valor] + ".00";
    }
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

function AvisoLoginTeste() {
    alert("PARA TESTE Usuarios: Admin ou Daniel/ senha:123");
}

//function SaveRout(rout) {
//    alert('Executando');
//    var url = '/Home/Login'; 

//    fetch(url, {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//        },
//        body: JSON.stringify({ rout: rout }),
//    })
//        .then(response => {
//            if (!response.ok) {
//                throw new Error('Erro na requisição.');
//            }
//            return response.json();
//        })
//        .then(data => {
//            // Lógica a ser executada em caso de sucesso
//            console.log(data);
//            alert('Sucesso');
//        })
//        .catch(error => {
//            // Lógica a ser executada em caso de erro
//            console.error('Erro:', error);
//            alert('Erro');
//        });
//}