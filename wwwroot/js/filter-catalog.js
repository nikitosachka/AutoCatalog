document.addEventListener("DOMContentLoaded", function () {
    const sortOrderSelect = document.getElementById("sortOrder");
    const carCatalog = document.getElementById("car-catalog");
    const filterForm = document.getElementById("filter-form");

    // Сортування
    sortOrderSelect.addEventListener("change", function () {
        const sortOrder = this.value;
        const cars = Array.from(carCatalog.getElementsByClassName("car-info"));

        cars.sort((a, b) => {
            let comparison = 0;

            const getTitle = el => el.querySelector("h1, h3")?.textContent ?? "";

            switch (sortOrder) {
                case "brand":
                    comparison = getTitle(a).localeCompare(getTitle(b));
                    break;
                case "brand_desc":
                    comparison = getTitle(b).localeCompare(getTitle(a));
                    break;
                case "year":
                    comparison = parseInt(a.dataset.year) - parseInt(b.dataset.year);
                    break;
                case "year_desc":
                    comparison = parseInt(b.dataset.year) - parseInt(a.dataset.year);
                    break;
                case "price":
                    comparison = parseFloat(a.dataset.price) - parseFloat(b.dataset.price);
                    break;
                case "price_desc":
                    comparison = parseFloat(b.dataset.price) - parseFloat(a.dataset.price);
                    break;
            }

            return comparison;
        });

        cars.forEach(car => carCatalog.appendChild(car));
    });

    // Фільтрація
    filterForm.addEventListener("submit", function (e) {
        e.preventDefault();

        const selectedBrands = getCheckedValues("brand");
        const selectedBodies = getCheckedValues("bodyType");
        const selectedColors = getCheckedValues("color");

        const cars = carCatalog.getElementsByClassName("car-info");

        for (let car of cars) {
            const matchesBrand = selectedBrands.length === 0 || selectedBrands.includes(car.dataset.brand);
            const matchesBody = selectedBodies.length === 0 || selectedBodies.includes(car.dataset.bodytype);
            const matchesColor = selectedColors.length === 0 || selectedColors.includes(car.dataset.color);

            if (matchesBrand && matchesBody && matchesColor) {
                car.style.display = "block";
            } else {
                car.style.display = "none";
            }
        }
    });

    function getCheckedValues(name) {
        return Array.from(document.querySelectorAll(`input[name="${name}"]:checked`)).map(cb => cb.value);
    }
});
