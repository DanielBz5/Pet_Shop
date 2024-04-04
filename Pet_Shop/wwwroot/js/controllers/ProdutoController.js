import Produto from '../models/Produto.js';

let valorBase = 0;

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
    document.getElementById('cod-mostra').innerHTML = produto.cod;
    document.getElementById('nome-mostra').innerHTML = produto.nome;
    document.getElementById('descri-mostra').innerHTML = produto.descricao;
    document.getElementById('valor-mostra').innerHTML = "R$ " + produto.valor + ".00";
    document.getElementById('categ-mostra').innerHTML = produto.categoria;

    valorBase = produto.valor;
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
    //soma quantidade
    const spanQtd = document.getElementById('qtd');
    let qtd = parseInt(spanQtd.textContent);
    qtd = qtd + 1;
    spanQtd.innerHTML = " " + qtd;

    UpdateValorQtd(qtd);
}

export function SubQtd() {
    //Subtrai quantidade
    const spanQtd = document.getElementById('qtd');
    let qtd = parseInt(spanQtd.textContent);
    if (qtd >= 2) {
        qtd = qtd - 1;
        spanQtd.innerHTML = " " + qtd;

        UpdateValorQtd(qtd);
    }     
}

function UpdateValorQtd(qtd) {
    //atualiza valor
    let valor = valorBase * qtd;
    document.getElementById('valor-mostra').innerHTML = "R$ " + valor.toFixed(2);
}

export function AddCarrinho() {
    var produto = new Produto({
        cod: document.getElementById('cod-mostra').textContent,
        nome: document.getElementById('nome-mostra').textContent,
        descricao: document.getElementById('descri-mostra').textContent,
        valor: document.getElementById('valor-mostra').textContent,
        quantidade: document.getElementById('qtd').textContent,
        imagem: document.getElementById('img-mostra').src
    });
    SaveLocalStorage(produto);

    document.getElementById('mostra-produto').style.display = 'none'

    LoadCarrinho();
}

function SaveLocalStorage(produto) {
    let carrinho = JSON.parse(localStorage.getItem('carrinho')) || [];//carrega carrinho
    carrinho.forEach(item => {
        if (item._cod == produto.cod) {
            MessageBox('Atenção', 'O produto já foi Incluido no Carrinho.')
        }
    });
    carrinho.push(produto);//add produto no array
    localStorage.setItem('carrinho', JSON.stringify(carrinho));//set novo carrinho com produto novo
}

export function LoadCarrinho() {
    document.getElementById('carrinho').style.display = 'Flex'
    document.getElementById('overlay').style.display = 'block';
    var itemsCarrinho = JSON.parse(localStorage.getItem('carrinho')) || [];
    const itemModelo = document.querySelector('.item-carrinho');

    itemsCarrinho.forEach(produto => {
        const novoItem = itemModelo.cloneNode(true);//clona

        novoItem.style.display = 'Flex';
        novoItem.setAttribute('class', 'card clone-item');
        novoItem.querySelector('.card-img-top').setAttribute('src', produto._imagem);
        novoItem.querySelector('.btn-remove-carrinho').setAttribute('id', `btn-remove-${produto._cod}`);
        novoItem.querySelector('.card-text:nth-of-type(1)').textContent = produto._nome;
        novoItem.querySelector('.card-text:nth-of-type(2)').textContent = produto._descricao;
        novoItem.querySelector('.row-text-item:nth-of-type(2) .card-text:nth-of-type(1)').textContent = produto._valor;
        novoItem.querySelector('.row-text-item:nth-of-type(2) .card-text:nth-of-type(2)').textContent = "Quantidade:"+produto._quantidade;

        document.querySelector('.carrinho-items').appendChild(novoItem);//insere no html
    });
}

export function RemoveLocalStorage(codRemove) {
    const codigo = parseInt(codRemove.substring(11));

    if (!isNaN(codigo)) {
        let carrinho = JSON.parse(localStorage.getItem('carrinho')) || [];
        carrinho = carrinho.filter(produto => produto.Cod !== codigo);
        localStorage.removeItem('carrinho', JSON.stringify(carrinho));

        document.querySelector('.clone-item').remove();

        LoadCarrinho();
    } else {
        console.error('Código inválido:', codRemove);
    }
}

export function FecharCarrinho() {//erro ao abrir, fechar e abrir de novo
    document.getElementById('carrinho').style.display = 'none'
    document.getElementById('overlay').style.display = 'none';
    document.querySelector('.clone-item').remove();
}

function MessageBox(titulo, mensagem) {;//colocar tratamento de exeção
    const url = `MessageBox?titulo=${titulo}&mensagem=${mensagem}`;
    fetch(url)
        .then(response => response.json())
        .then(data => {

        })
        .catch(error => console.error('Erro:', error));

    window.location.href = "/Shared/MessageBox.cshtml";
}



