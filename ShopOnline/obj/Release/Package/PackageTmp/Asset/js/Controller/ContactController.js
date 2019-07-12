var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('.btn-link').off('click').on('click',  function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                data: { contactid: id },
                dataType: "json",
                type: "POST",
                url: "/Admin/Contact/ChangeStatus",
                success:  function (object) {
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
contact.init();