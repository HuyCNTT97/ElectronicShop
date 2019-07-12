var news = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/News/ChangeStatus",
                type: "POST",
                data: { newsid: id },
                dataType: 'json',
                success: function (news) {
                    if (news.status == true) {
                        btn.text('Kích Hoạt');
                    }
                    else { btn.text('Đã Khóa');}
                }
            })
        })
    }
}
news.init();