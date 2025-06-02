document.addEventListener("DOMContentLoaded", function () {
    const sortOrderSelect = document.getElementById("sortOrder");
    const carCatalog = document.getElementById("car-catalog");
    const filterForm = document.getElementById("filter-form");
    const resetButton = document.getElementById("reset-button");

    const mileageSlider = document.getElementById("mileageRange");
    const mileageValueLabel = document.getElementById("mileageValue");
    const engineVolumeMax = document.getElementById("engineVolumeMax");

    const initialMileage = mileageSlider.max;
    const initialEngineVolume = engineVolumeMax.max;

    mileageSlider.addEventListener("input", () => {
        mileageValueLabel.textContent = mileageSlider.value;
    });

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
        const selectedFuels = getCheckedValues("fuelType");
        const selectedTransmissions = getCheckedValues("transmission");
        const maxMileage = parseInt(mileageSlider.value);
        const maxEngineVolume = parseFloat(engineVolumeMax.value);

        const cars = carCatalog.getElementsByClassName("car-info");

        for (let car of cars) {
            const matchesBrand = selectedBrands.length === 0 || selectedBrands.includes(car.dataset.brand);
            const matchesBody = selectedBodies.length === 0 || selectedBodies.includes(car.dataset.bodytype);
            const matchesColor = selectedColors.length === 0 || selectedColors.includes(car.dataset.color);
            const matchesFuel = selectedFuels.length === 0 || selectedFuels.includes(car.dataset.fueltype);
            const matchesTrans = selectedTransmissions.length === 0 || selectedTransmissions.includes(car.dataset.transmission);
            const matchesMileage = parseInt(car.dataset.mileage) <= maxMileage;
            const matchesEngine = parseFloat(car.dataset.enginevolume) <= maxEngineVolume;

            car.style.display = (matchesBrand && matchesBody && matchesColor && matchesFuel && matchesTrans && matchesMileage && matchesEngine)
                ? "block"
                : "none";
        }
    });

    // 👉 Додано: Скидання фільтрів
    resetButton.addEventListener("click", function () {
        // Знімаємо всі чекбокси
        filterForm.querySelectorAll("input[type='checkbox']").forEach(cb => cb.checked = false);

        // Скидаємо range і number inputs
        mileageSlider.value = initialMileage;
        engineVolumeMax.value = initialEngineVolume;

        // Оновлюємо відображення значення пробігу
        mileageValueLabel.textContent = initialMileage;

        // Показуємо всі машини
        Array.from(carCatalog.getElementsByClassName("car-info")).forEach(car => {
            car.style.display = "block";
        });
    });

    function getCheckedValues(name) {
        return Array.from(document.querySelectorAll(`input[name="${name}"]:checked`)).map(cb => cb.value);
    }
});
