var order = {
    init: function () {
        order.registerEvent();
    },
    registerEvent: function () {
        $('.btn-link-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data("id");
            $.ajax({
                data: { orderID: id },
                dataType: "json",
                type: "POST",
                url: "/Admin/Order/ChangeStatus",
                success: function (respon) {
                    if (respon.status == true) btn.text("Kích Hoạt");
                    else btn.text("Đã Khóa");
                }
            });
        });
    }
}
order.init();