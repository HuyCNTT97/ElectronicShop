var about = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('.btn-link-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/About/ChangeStatus/",
                dataType: "json",
                data: { aboutid: id },
                type: "POST",
                success: function (object) {
                    if (object.status == true) {
                        btn.text("Kích Hoạt");
                    }
                    else {
                        btn.text("Đã Khóa");
                    }
                }
            })
        })
    }
}
about.init();