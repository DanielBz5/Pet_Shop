
export default class Servicos {

    constructor(cod, nome, valor, descricao) {
        this.cod = cod;
        this.nome = nome;
        this.valor = valor;
        this.descricao = descricao;
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

    get descricao() {
        return this._descricao;
    }
    set descricao(novoDescricao) {
        this._descricao = novoDescricao;
    }

}