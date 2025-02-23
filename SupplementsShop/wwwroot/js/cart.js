document.addEventListener("DOMContentLoaded", () => {
    // Add to cart func
    function addToCart(productId, quantity = 1) {
        fetch(`/Cart/AddToCart&productId=${productId}&quantity=${quantity}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "X-Requested-With": "XMLHttpRequest"
            }
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert(data.message);
                    updateCartBadge();
                } else {
                    alert("Error: " + data.message);
                }
            })
            .catch(error => console.error("Error: ", error));
    }
    
    document.querySelectorAll("add-to-cart-btn").forEach(btn => {
        button.addEventListener("click", function (event) {
            event.preventDefault();
            let productId = parseInt(this.getAttribute("data-product-id"));
            addToCart(productId);
        });
    })
    
    document.querySelectorAll("add-to-cart-form").forEach(form => {
        form.addEventListener("submit", function (event) {
            event.preventDefault();
            let productId = parseInt(this.querySelector("[name='productId']").value);
            let quantity = parseInt(this.querySelector("[name='quantity']").value);
            addToCart(productId, quantity);
        })
    })

    function updateCartBadge() {
        fetch('/Cart/GetCartCount', {
            method: "GET",
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            }
        })
            .then(response => response.json())
            .then(data => {
                const cartBadge = document.getElementById("cart-count");
                if (data.count > 0) {
                    cartBadge.textContent = data.count;
                    cartBadge.style.display = "inline-block";
                } else {
                    cartBadge.style.display = "none";
                }
            })
            .catch(error => console.error("Error fetching cart count: ", error));
    }

    updateCartBadge()
})