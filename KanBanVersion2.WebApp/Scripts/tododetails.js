$(function () {
    $('#modal_tododetay').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        todoid = btn.data("todo-id");
        $('#modal_tododetay_body').load("/Todo/GetTodoDetails/" + todoid);
    })
});