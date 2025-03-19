document.addEventListener('DOMContentLoaded', () => {
    function GetProducts(slug) {
        fetch("Category/GetProducts", {
            method: "GET",
            headers: {
                "X-Requested-With": "XMLHttpRequest",
            }
        })
            .then(response => response.json())
            .then(data => {
                
            })
    }
    
    function InsertProducts() {
        fetch("Category")
    }
})