document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".add-to-cart-btn").forEach(button => {
        button.addEventListener("click", function (event) {
            event.preventDefault(); // Prevent full page reload
            
            let productId = parseInt(this.getAttribute("data-product-id"));
            
            fetch(`/Cart/AddToCart?productId=${productId}`, {
                method: "POST",
                headers : {
                    "X-Requested-With": "XMLHttpRequest"
                },
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
        });
    });

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
});

