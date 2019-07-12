var contact = {
    init: function () {
        contact.registerEvent();
    },
    registerEvent: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#txtName').val();
            var phone = $('#txtPhone').val();
            var address = $('#txtAddress').val();
            var email = $('#txtEmail').val();
            var content = $('#txtContent').val();
            $.ajax({
                url: "/Contact/Send",
                type: "POST",
                dataType: "json",
                data: {
                    txtName: name,
                    txtPhone: phone,
                    txtAddress: address,
                    txtEmail: email,
                    txtContent: content
                },
                success: function (respon) {
                    if (respon.status == true) {
                        alert("thành công");
                    }
                }
            });
        });
    }
};
contact.init();