document.addEventListener('DOMContentLoaded', () => {
    // Get products for new page 
    function getProductsForPage(categoryId, pageNumber, pageSize) {
        fetch('Category/GetNewPage/', {
            method: 'GET',
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            },
            body: new URLSearchParams({ categoryId, pageNumber, pageSize })
        })
            .then(response => response.text())
            .then(html => {
                document.getElementById("product-row").innerHTML = html;
            })
            .catch(error => console.log('Error fetching updated products:', error));
    }

    document.querySelectorAll(".other-page-btn").forEach(btn => {
        btn.addEventListener('click', (e) => {
            e.preventDefault();
            let categoryId = parseInt(this.getAttribute('data-category-id'));
            let pageNumber = parseInt(this.getAttribute("data-page-number"));
            let pageSize = parseInt(this.getAttribute("data-page-size"));
            getProductsForPage(categoryId, pageNumber, pageSize);
        })
    })
})