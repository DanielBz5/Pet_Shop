

export function SelectModel() {

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