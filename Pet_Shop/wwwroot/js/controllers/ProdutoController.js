/*import Produto from '../models/Produto';*/

export function DeleteProduto() {
    const overlay = document.getElementById('overlay');
    const msgbox = document.getElementById('msgbox-deleta-prod');
    overlay.style.display = 'block';
    msgbox.style.display = 'block';
}