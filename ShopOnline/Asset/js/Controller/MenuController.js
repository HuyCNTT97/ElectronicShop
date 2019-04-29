var menu = {
    init: function () {
        menu.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').off('click').on('click',function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                data: { menuID: id },
                dataType: "json",
                url: "/Admin/Menu/ChangeStatus",
                type:"POST",
                success: function (getobject) {
                    if (getobject.status == true) btn.text("Kích Hoạt");
                    else btn.text("Đã Khóa");
                }
            })
        })
    }
}
menu.init();