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


// Animación de Loader
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('a.nav-link, a.dropdown-item').forEach(link => {
        link.addEventListener('click', function (e) {
            if (link.classList.contains('dropdown-toggle')) {
                // Si es el toggle del dropdown, no mostrar loader
                return;
            }
            // Para cualquier otro link, mostrar loader
            document.getElementById('loader').style.display = 'flex';
        });
    });
});



// Cargar categorías desde el archivo JSON
document.addEventListener("DOMContentLoaded", function () {
    fetch('/js/categoria.json')
        .then(response => {
            if (!response.ok) {
                throw new Error('No se pudo cargar el archivo JSON');
            }
            return response.json();
        })
        .then(categorias => {
            const select = document.getElementById("select-categoria-todos");

            const optionTodos = document.createElement("option");
            optionTodos.value = "todos";
            optionTodos.textContent = "Todos";
            select.appendChild(optionTodos);

            categorias.forEach(cat => {
                const option = document.createElement("option");
                option.value = cat.value;
                option.textContent = cat.text;
                select.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error cargando categorías:", error);
        });
});

document.addEventListener("DOMContentLoaded", function () {
    fetch('/js/categoria.json')
        .then(response => {
            if (!response.ok) {
                throw new Error('No se pudo cargar el archivo JSON');
            }
            return response.json();
        })
        .then(categorias => {
            const select = document.getElementById("select-categoria");

            categorias.forEach(cat => {
                const option = document.createElement("option");
                option.value = cat.value;
                option.textContent = cat.text;
                select.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error cargando categorías:", error);
        });
});


