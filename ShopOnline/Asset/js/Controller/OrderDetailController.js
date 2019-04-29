var about = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('.btn-xuli').off('click').on('click', function (e) {
            e.preventDefault(); var btn = $(this);
            var orderID = btn.data('orderid');
            var productID = btn.data('productid');
            $.ajax({
                url: "/Admin/OrderDetail/ChangeStatusProcess",
                datatype: "json",
                data: { orderID:orderID,productID:productID},
                type: "post",
                success: function (object) {
                    if (object.status == true) {
                        btn.text("Đã xử lí");
                    }
                    else {
                        btn.text("Chưa xử lí");
                    }
                }
            })
           
        })
        $('.btn-giao').off('click').on('click', function (e) {
            e.preventDefault(); var btn = $(this);
            var orderID = btn.data('orderid');
            var productID = btn.data('productid');
            $.ajax({
                url: "/Admin/OrderDetail/ChangeStatusShipping",
                datatype: "json",
                data: { orderID: orderID, productID: productID },
                type: "post",
                success: function (object) {
                    if (object.status == true) {
                        btn.text("Đang giao");
                    }
                    else {
                        btn.text("Chưa giao");
                    }
                }
            })

        })
        $('.btn-thanhtoan').off('click').on('click', function (e) {
            e.preventDefault(); var btn = $(this);
            var orderID = btn.data('orderid');
            var productID = btn.data('productid');
            $.ajax({
                url: "/Admin/OrderDetail/ChangeStatusPayment",
                datatype: "json",
                data: { orderID: orderID, productID: productID },
                type: "post",
                success: function (object) {
                    if (object.status == true) {
                        btn.text("Đang nhận và thanh toans");
                    }
                    else {
                        btn.text("Chưa nhận");
                    }
                }
            })

        })
    }
}
about.init();