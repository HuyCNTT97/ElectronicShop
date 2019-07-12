var productcategory = {
    init: function () {
        productcategory.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                data: { productCategoryID: id },
                dataType: "json",
                type: "POST",
                url: "/Admin/ProductCategory/ChangeStatus",
                success: function (objectProductCategory) {
                    if (objectProductCategory.status == true) {
                        btn.text("Kích Hoạt");
                    }
                    else btn.text("Đã Khóa");
                }
            })
        })
    }
}
productcategory.init();