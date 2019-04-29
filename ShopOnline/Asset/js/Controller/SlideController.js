var slide = {
    init: function () {
        slide.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Slide/ChangeStatus",
                data: { SlideID: id },
                dataType: "json",
                type: "POST",
                success: function (ObjectJson) {
                    if (ObjectJson.status == true) btn.text("Kích Hoạt");
                    else btn.text("Đã Khóa");
                }
            })
        })
    }
}
slide.init();