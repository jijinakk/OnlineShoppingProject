function addToCart(productId) {
    console.log("Adding product with ID: " + productId);

    $.ajax({
        url: '/Product/AddProductToCart',
        type: 'POST',
        data: { productId: productId },
        success: function (response) {
            console.log("AJAX success response:", response);
            if (response.success) {
                $('#cart-item-count').text(response.cartItemCount);
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
    }
   