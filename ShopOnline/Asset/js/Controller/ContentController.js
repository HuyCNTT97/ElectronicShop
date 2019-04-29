var content = {
    init: function () {
        content.registerEvent();
    },
    registerEvent: function () {
        $('.btn-link-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Content/ChangeStatus",
                type: "POST",
                dataType: "json",
                data: { ContentID: id },
                success: function (ObjectJson) {
                    if (ObjectJson.status == true) btn.text("Kích Hoạt");
                    if (ObjectJson.status == false) btn.text("Đã Khóa");
                }
            });
        });
    }
}
content.init();