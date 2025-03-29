function bindPageButtons() {
        document.querySelectorAll(".other-page-btn").forEach(btn => {
            btn.addEventListener('click', function (event) {
                event.preventDefault();
                let categoryId = parseInt(this.getAttribute("data-category-id"));
                let pageIndex = parseInt(this.getAttribute("data-page-number")) - 1;
                let pageSize = parseInt(this.getAttribute("data-page-size"));
                getProductsForPage(categoryId, pageIndex, pageSize);
            });
        });
    }
    
document.addEventListener('DOMContentLoaded', () => {
    bindPageButtons();
})

function getProductsForPage(categoryId, pageIndex, pageSize) {
    const url = `/Category/GetNewPage?categoryId=${categoryId}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
    fetch(url, {
        method: 'GET',
        headers: {
            "X-Requested-With": "XMLHttpRequest"
        }
    })
        .then(response => response.text())
        .then(html => {
            document.getElementById("category-page").innerHTML = html;
            bindPageButtons();
        })
        .catch(error => console.log('Error fetching updated products:', error));
}