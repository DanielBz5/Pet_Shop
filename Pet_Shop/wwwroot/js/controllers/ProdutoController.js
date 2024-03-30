import Produto from '../models/Produto.js';

export function DeleteProduto() {
    const overlay = document.getElementById('overlay');
    const msgbox = document.getElementById('msgbox-deleta-prod');
    overlay.style.display = 'block';
    msgbox.style.display = 'block';
}

export async function MostraProduto(event) {
    const codProduto = event.target.querySelector('#Cod-Produto').textContent;
    var produto = new Produto(await BuscaProduto(codProduto));

    document.getElementById('mostra-produto').style.display = 'flex';
    document.getElementById('overlay').style.display = 'block';
    document.getElementById('img-mostra').src = `data:image/png;base64,${produto.imagem}`;
    document.getElementById('nome-mostra').innerHTML = produto.nome
    document.getElementById('descri-mostra').innerHTML = produto.descricao
    document.getElementById('valor-mostra').innerHTML = "R$ " + produto.valor + ".00";
    document.getElementById('categ-mostra').innerHTML = produto.categoria
 
}

function BuscaProduto(codProduto) {
    const url = `BuscaProduto?codProduto=${codProduto}`;
    return fetch(url) 
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao buscar produto');
            }
            return response.json();
        })
        .then(data => {
            const produto = new Produto(data); 
            return produto; 
        })
        .catch(error => {
            console.error('Erro ao buscar produto:', error);
            throw error; 
        });
}

export function SomaQtd() {
    const spanQtd = document.getElementById('qtd');
    let qtd = parseInt(spanQtd.textContent);
    qtd = qtd + 1;
    spanQtd.innerHTML = " " + qtd;
}

export function SubQtd() {
    const spanQtd = document.getElementById('qtd');
    let qtd = parseInt(spanQtd.textContent);
    if (qtd >= 2) {
        qtd = qtd - 1;
        spanQtd.innerHTML = " "+qtd;
    }     
}