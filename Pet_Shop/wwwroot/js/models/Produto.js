
export default class Produto {

    constructor(produto) {
        this.cod = produto.cod || null;//garanti que tds propriedades tenha um valor msm que null
        this.nome = produto.nome || null;
        this.valor = produto.valor || null;
        this.quantidade = produto.quantidade || null;
        this.estoque_minimo = produto.estoque_minimo || null;
        this.categoria = produto.categoria || null;
        this.descricao = produto.descricao || null;
        this.imagem = produto.imagem || null;
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
    set estoque_minimo(novoEstoque_minimo) {
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


