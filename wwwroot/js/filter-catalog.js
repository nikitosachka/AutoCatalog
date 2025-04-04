document.addEventListener("DOMContentLoaded", function() {
    const sortOrderSelect = document.getElementById("sortOrder");
    const carCatalog = document.getElementById("car-catalog");

    sortOrderSelect.addEventListener("change", function() {
        const sortOrder = this.value;
        const cars = Array.from(carCatalog.getElementsByClassName("car-info"));

        cars.sort((a, b) => {
            let comparison = 0;

            switch (sortOrder) {
                case "brand":
                    comparison = a.querySelector("h1").textContent.localeCompare(b.querySelector("h1").textContent);
                    break;
                case "brand_desc":
                    comparison = b.querySelector("h1").textContent.localeCompare(a.querySelector("h1").textContent);
                    break;
                case "year":
                    comparison = parseInt(a.dataset.year) - parseInt(b.dataset.year);
                    break;
                case "year_desc":
                    comparison = parseInt(b.dataset.year) - parseInt(a.dataset.year);
                    break;
                case "price":
                    comparison = parseFloat(a.querySelector(".price").textContent.replace("$", "").replace(",", "")) - parseFloat(b.querySelector(".price").textContent.replace("$", "").replace(",", ""));
                    break;
                case "price_desc":
                    comparison = parseFloat(b.querySelector(".price").textContent.replace("$", "").replace(",", "")) - parseFloat(a.querySelector(".price").textContent.replace("$", "").replace(",", ""));
                    break;
            }

            return comparison;
        });

        cars.forEach(car => carCatalog.appendChild(car));
    });
});
