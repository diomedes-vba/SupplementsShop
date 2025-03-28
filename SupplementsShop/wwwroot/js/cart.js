document.addEventListener("DOMContentLoaded", () => {
    // Add to cart func
    function addToCart(productId, quantity = 1) {
        fetch(`/Cart/AddToCart`, {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "X-Requested-With": "XMLHttpRequest"
            },
            body: new URLSearchParams({ productId, quantity })
        })
            .then(response => response.json())
            .then(data => {
                const modalTitle = document.getElementById('cartModalLabel');
                const modalBody = document.getElementById('cartModalBody');
                if (data.success) {
                    modalTitle.textContent = "Success";
                    modalBody.textContent = data.message;
                    updateCartBadge();
                } else {
                    modalTitle.textContent = "Error";
                    modalBody.textContent = data.message;
                }
                
                fetch('/Cart/RefreshCart', {
                    method: "GET",
                    headers: {
                        "X-Requested-With": "XMLHttpRequest"
                    }
                })
                    .then(response => response.text())
                    .then(html => {
                        document.getElementById('cartModalContainer').innerHTML = html;

                        const modalElement = document.getElementById('cartModal');
                        const modalInstance = new bootstrap.Modal(modalElement);
                        modalInstance.show();
                    });
            })
            .catch(error => console.error("Error: ", error));
    }
    
    document.querySelectorAll(".add-to-cart-btn").forEach(btn => {
        btn.addEventListener("click", function (event) {
            event.preventDefault();
            let productId = parseInt(this.getAttribute("data-product-id"));
            addToCart(productId);
        });
    })
    
    document.querySelectorAll(".add-to-cart-form").forEach(form => {
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
                if (data.cartCount > 0) {
                    cartBadge.textContent = data.cartCount;
                    cartBadge.style.display = "inline-block";
                } else {
                    cartBadge.style.display = "none";
                }
            })
            .catch(error => console.error("Error fetching cart count: ", error));
    }
    
    window.updateCartQuantity = function(productId, quantity) {
        fetch(`/Cart/UpdateItemQuantity`, {
            method: "POST",
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            },
            body: new URLSearchParams({ productId, quantity })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    console.log('Cart Updated')
                    
                    const itemTotalElement = document.getElementById('item-total-' + productId);
                    if (itemTotalElement) {
                        itemTotalElement.innerText = data.itemNewPrice +" $";
                    }
                    
                    const cartTotalElement = document.getElementById('cart-total');
                    if (cartTotalElement) {
                        cartTotalElement.innerText = "Total Price: $" + data.cartNewPrice;
                    }
                } else {
                    console.error('Failed to update quantity');
                }
            })
            .catch(error => console.error("Error updating cart quantity: ", error));
    }

    updateCartBadge()
})
