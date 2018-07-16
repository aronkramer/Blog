$(() => {
    $("#commenters-name").on('keyup', function () {
        let a = allow();
        $("#CommentButton").prop('disabled', !a);
    });

    function allow() {
        const name = $("#commenters-name").val();
        return name;
    }
});