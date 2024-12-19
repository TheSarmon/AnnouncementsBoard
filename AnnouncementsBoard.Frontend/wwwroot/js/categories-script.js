const categoryData = JSON.parse(document.getElementById("categoryData").textContent);

function populateSubcategories() {
    const category = document.getElementById("category").value;
    const subcategorySelect = document.getElementById("subcategory");

    subcategorySelect.innerHTML = '<option value="">-- Оберіть підкатегорію --</option>';
    subcategorySelect.disabled = !category;

    if (category) {
        categoryData[category]?.forEach(subcategory => {
            const option = document.createElement("option");
            option.value = subcategory;
            option.textContent = subcategory;
            subcategorySelect.appendChild(option);
        });
    }
}