var cart = {
    init: function () {
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: "/removeall-gio-hang",
                data: {},
                type: "POST",
                dataType: "json",
                success: function (respon) {
                    if (respon.status==true) window.location.href = "/Cart";
                }
            });
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan-gio-hang";
        });
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            btn = $(this);
            var productID = btn.data('id');
            $.ajax({
                url: "remove-gio-hang",
                data: { productID: productID },
                dataType: "json",
                type: "POST",
                success: function (respon) {
                    if (respon.status == true) window.location.href = "Cart";
                }
            });
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartlist = [];
            $.each(listProduct, function (i, item) {

                cartlist.push({
                    Quantity: $(item).val(),
                    Product: {
                        ProductID:$(item).data('id')
                    }
                });
            });
            $.ajax({
                url: "gio-hang",
                data: { CartModel: JSON.stringify(cartlist) },
                dataType: "json",
                type: "POST",
                success: function (respon) {
                    if (respon.status == true) {
                        window.location.href = "/Cart";
                    }
                }
                
            });
        });

    }
};
cart.init();