function bindPageButtons() {
    document.querySelectorAll(".other-page-btn").forEach(btn => {
        btn.addEventListener('click', function (event) {
            event.preventDefault();
            let searchString = this.getAttribute("data-search-term");
            let pageIndex = parseInt(this.getAttribute("data-page-number")) - 1;
            let pageSize = parseInt(this.getAttribute("data-page-size"));
            console.log(searchString, pageIndex, pageSize);
            getProductsForPage(searchString, pageIndex, pageSize);
        });
    });

    document.querySelectorAll(".previous-page-btn").forEach(btn => {
        btn.addEventListener('click', function (event) {
            event.preventDefault();
            let searchString = this.getAttribute("data-search-term");
            let pageIndex = parseInt(this.getAttribute("data-current-page")) - 1;
            let pageSize = parseInt(this.getAttribute("data-page-size"));

            getProductsForPage(searchString, pageIndex, pageSize);
        })
    })

    document.querySelectorAll(".next-page-btn").forEach(btn => {
        btn.addEventListener('click', function (event) {
            event.preventDefault();
            let searchString = this.getAttribute("data-search-term");
            let pageIndex = parseInt(this.getAttribute("data-current-page")) + 1;
            let pageSize = parseInt(this.getAttribute("data-page-size"));

            getProductsForPage(searchString, pageIndex, pageSize);
        })
    })
}

document.addEventListener('DOMContentLoaded', () => {
    bindPageButtons();
})

function getProductsForPage(searchString, pageIndex, pageSize) {
    const url = `/Product/GetNewSearchPage?searchString=${searchString}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
    fetch(url, {
        method: 'GET',
        headers: {
            "X-Requested-With": "XMLHttpRequest"
        }
    })
        .then(response => response.text())
        .then(html => {
            document.getElementById("search-page").innerHTML = html;
            bindPageButtons();
        })
        .catch(error => console.log('Error fetching updated products:', error));
}