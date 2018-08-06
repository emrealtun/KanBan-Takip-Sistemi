var todoid = -1;

var modalYorumBodyId = "#modal_yorum_body";

$(function () {
    $('#modal_yorum').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        todoid = btn.data("todo-id");
        $(modalYorumBodyId).load("/Yorum/ShowTodoYorum/" + todoid);
    })
});

function yorumEdit(btn, e, yorumId, spanId) {

    var button = $(btn);
    var mode = button.data("edit-mode");

    if (e === "edit_click") {
        if (!mode) {
            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var btnSpan = button.find("span");
            btnSpan.removeClass("fa-edit");
            btnSpan.addClass("fa-thumbs-up");

            $(spanId).addClass("editable");
            $(spanId).attr("contenteditable", true);
            $(spanId).focus();


        }
        else {
            button.data("edit-mode", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success");
            var btnSpan = button.find("span");
            btnSpan.addClass("fa-edit");
            btnSpan.removeClass("fa-thumbs-up");

            $(spanId).removeClass("editable");
            $(spanId).attr("contenteditable", false);

            var txt = $(spanId).text();

            $.ajax({
                method: "POST",
                url: "/Yorum/Edit/" + yorumId,
                data: { text: txt }
            }).done(function (data) {
                if (data.result) {
                    //yorumlar tekrar yüklenir
                    $(modalYorumBodyId).load("/Yorum/ShowTodoYorum/" + todoid);
                }
                else {
                    alert("Yorum güncellenemedi");
                }
            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            });

        }

    }
    else if (e === "delete_click") {
        var dialog_res = confirm("Yorumu silmek istediğinizden emin misiniz?");

        if (!dialog_res) return false;

        $.ajax({
            method: "GET",
            url: "/Yorum/Delete/" + yorumId
        }).done(function (data) {
            if (data.result) {
                //yorumlar tekrar yüklenir

                $(modalYorumBodyId).load("/Yorum/ShowTodoYorum/" + todoid);
            }
            else {
                alert("Yorum silinemedi");
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        })
    }
    else if (e === "new_click") {

        var txt = $("#yeni_yorum_txt").val();
        $.ajax({
            method: "POST",
            url: "/Yorum/Create/",
            data: { "icerik": txt, "todoid": todoid }
        }).done(function (data) {
            if (data.result) {
                //yorumlar tekrar yüklenir
                $(modalYorumBodyId).load("/Yorum/ShowTodoYorum/" + todoid);
            }
            else {
                alert("Yorum yapılamadı");
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }

}