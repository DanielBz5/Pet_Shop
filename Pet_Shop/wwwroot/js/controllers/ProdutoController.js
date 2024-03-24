/*import Produto from '../models/Produto';*/

export function DeleteProduto() {
    var formDelete = document.getElementById('form-deleta-prod')
    formDelete.style.display = 'flex'
    var formGerencia = document.querySelectorAll('.form-gerencia')
    formGerencia.style.display = 'none'
}