var user={
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-link-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data("id");
            console.log(btn);
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (ObjectJson) {
                    if (ObjectJson.status == true) {
                        btn.text("Kích Hoạt");
                    }
                    else {
                        btn.text("Đã Khóa");
                    }
                }
            });
        });
    }
}
user.init();