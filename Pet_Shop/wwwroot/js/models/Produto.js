
export default class Produto {

    constructor(cod, nome, valor, quantidade, estoque_minimo, categoria, descricao, imagem) {
        this.cod = cod;
        this.nome = nome;
        this.valor = valor;
        this.quantidade = quantidade;
        this.estoque_minimo = estoque_minimo;
        this.categoria = categoria;
        this.descricao = descricao;
        this.imagem = imagem;  
    }

    get cod() {
        return this._cod;
    }
    set cod(novoCod) {
        this._cod = novoCod;
    }

    get nome() {
        return this._nome;
    }
    set nome(novoNome) {
        this._nome = novoNome;
    }

    get valor() {
        return this._valor;
    }
    set valor(novoValor) {
        this._valor = novoValor;
    }

    get quantidade() {
        return this._quantidade;
    }
    set quantidade(novoQuantidade) {
        this._quantidade = novoQuantidade;
    }

    get estoque_minimo() {
        return this._estoque_minimo;
    }
    set estoque_minimo(novoEstoque_minimoe) {
        this._estoque_minimo = novoEstoque_minimo;
    }

    get categoria() {
        return this._categoria;
    }
    set categoria(novoCategoria) {
        this._categoria = novoCategoria;
    }

    get descricao() {
        return this._descricao;
    }
    set descricao(novoDescricao) {
        this._descricao = novoDescricao;
    }

    get imagem() {
        return this._imagem;
    }
    set imagem(novoImagem) {
        this._imagem = novoImagem;
    }
}


