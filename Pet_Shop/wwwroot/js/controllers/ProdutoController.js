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
                MessageBox('Atenção', 'Erro ao carregar produto');
            }
            return response.json();
        })
        .then(data => {
            const produto = new Produto(data); 
            return produto; 
        })
        .catch(error => {
            MessageBox('Atenção', 'Erro ao carregar produto');
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
    let exist = false;
    carrinho.forEach(item => {
        if (item._cod == produto.cod) {
            exist = true;
        }
    });

    if (exist == false) {
        carrinho.push(produto);//add produto no array
        localStorage.setItem('carrinho', JSON.stringify(carrinho));//set novo carrinho com produto novo
    }
    else {
        MessageBox('Atenção', 'O produto já foi Incluido no Carrinho.')
    }
}

export function LoadCarrinho() {
    document.getElementById('carrinho').style.display = 'Flex'
    document.getElementById('overlay').style.display = 'block';
    var itemsCarrinho = JSON.parse(localStorage.getItem('carrinho')) || [];
    const itemModelo = document.querySelector('.item-carrinho');
    let totalCarrinho = 0;

    if (itemsCarrinho.length === 0) {
        document.getElementById('valor-carrinho').textContent = "Carrinho Vazio";
        document.getElementById('continuar-pedido').style.display = 'none';
    }
    else {
        document.getElementById('continuar-pedido').style.display = 'flex';
        itemsCarrinho.forEach(produto => {
            const novoItem = itemModelo.cloneNode(true);//clona

            novoItem.style.display = 'Flex';
            novoItem.setAttribute('class', 'card clone-item');
            novoItem.querySelector('.card-img-top').setAttribute('src', produto._imagem);
            novoItem.querySelector('.btn-remove-carrinho').setAttribute('id', `btn-remove-${produto._cod}`);
            novoItem.querySelector('.card-text:nth-of-type(1)').textContent = produto._nome;
            novoItem.querySelector('.card-text:nth-of-type(2)').textContent = produto._descricao;
            novoItem.querySelector('.row-text-item:nth-of-type(2) .card-text:nth-of-type(1)').textContent = produto._valor;
            novoItem.querySelector('.row-text-item:nth-of-type(2) .card-text:nth-of-type(2)').textContent = "Quantidade:" + produto._quantidade;

            document.querySelector('.carrinho-items').appendChild(novoItem);//insere no html

            totalCarrinho = totalCarrinho + parseInt((produto._valor).substring(2));
        });

        document.getElementById('valor-carrinho').textContent = "R$ " + totalCarrinho + ".00";
    }
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
        MessageBox('Atenção', 'Erro ao remover produto.');
    }
}

export function FecharCarrinho() { 
    document.getElementById('carrinho').style.display = 'none'
    document.getElementById('overlay').style.display = 'none';
    document.querySelector('.clone-item').remove();
}

function MessageBox(titulo, mensagem) {
    const url = `MessageBox?titulo=${encodeURIComponent(titulo)}&mensagem=${encodeURIComponent(mensagem)}`;
    window.location.href = url;
}

export function EnviaPedido() {
    var carrinho = JSON.parse(localStorage.getItem('carrinho')) || [];
    var produtos = [];

    carrinho.forEach(item => {
        var produto = new Produto({//monta modelo convertido
            cod: parseInt(item._cod), 
            nome: item._nome,
            valor: parseFloat(item._valor.replace('R$ ', '')),
            quantidade: parseInt(item._quantidade.trim()), 
            estoque_minimo: 0, 
            categoria: item._categoria,
            descricao: item._descricao,
            imagem: ""
        });

        for (const key in produto) {//converte nome atributos
            if (Object.prototype.hasOwnProperty.call(produto, key)) {
                const newKey = key.replace(/^_/, '').charAt(0).toUpperCase() + key.slice(2);
                produto[newKey] = produto[key];
                if (newKey !== key) delete produto[key];
            }
        }

        produtos.push(produto);
    })

    fetch('RecebeCarrinho', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(produtos)
    })
        .then(response => {
            if (response.ok) {
                window.location.href = 'GeraPedido';
            } else {
                MessageBox('Atenção', 'Erro ao carregar carrinho.');
            }
        })
        .catch(error => {
            MessageBox('Atenção', 'Erro ao montar pedido.');
        });
}

export function CopiaChave() {
    const chave = document.getElementById('chave-pagamento').value;
    CopiaAreaDeTransferencia(chave);
    
}

function CopiaAreaDeTransferencia(texto) {
    
    var textareaTemp = document.createElement("textarea");
    textareaTemp.value = texto;

    document.body.appendChild(textareaTemp);

    // Define o foco no textarea
    textareaTemp.focus();

    // Seleciona todo o texto dentro do textarea
    textareaTemp.setSelectionRange(0, texto.length);

    // Copia o texto selecionado
    document.execCommand("copy");

    document.body.removeChild(textareaTemp);
}