document.addEventListener('DOMContentLoaded', () => {
    const filtro = document.getElementById('select-categoria');
    const cartas = document.querySelectorAll('.carta-rodeo');

    filtro.addEventListener('change', () => {
        const categoriaSeleccionada = filtro.value.toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "");
    
        cartas.forEach(carta => {
            const categoriaCarta = carta.dataset.categoria.toLowerCase().normalize("NFD").replace(/[\u0300-\u036f]/g, "");
    
            if (categoriaSeleccionada === 'todos' || categoriaCarta === categoriaSeleccionada) {
                carta.style.display = 'block';
            } else {
                carta.style.display = 'none';
            }
        });
    });
    
});

document.addEventListener("DOMContentLoaded", function () {
    const cartas = document.querySelectorAll(".carta-rodeo");

    cartas.forEach(carta => {
        carta.addEventListener("click", function () {
            // Opcional: quitar "activa" de todas las demás
            cartas.forEach(c => c.classList.remove("activa"));
            
            // Agregar clase activa a la seleccionada
            this.classList.add("activa");
        });
    });
});