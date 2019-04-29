var product = {
    init: function(){
        product.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var productID = btn.data('id');
            $.ajax({
                url: "/Admin/Product/ChangeStatus",
                type: "POST",
                dataType: "json",
                data: { productID: productID },
                success: function (ObjectJson) {
                    if (ObjectJson.status == true) {
                        btn.text("Kích Hoạt");
                    }
                    else btn.text("Đã Khóa");
                }
            })
        })
    }

}
product.init();