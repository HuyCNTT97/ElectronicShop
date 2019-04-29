var feedback = {
    init:function () {
        feedback.registerEvent();
    },
    registerEvent:function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                data: { feedbackID:id },
                dataType: "json",
                type: "POST",
                url: "/Admin/FeedBack/ChangeStatus",
                success: function (object) {
                    if (object.status == true) {
                        btn.text("Kích Hoạt");
                    }
                    else btn.text("Đã Khóa");
                }
            })
        })
    }
}
feedback.init();