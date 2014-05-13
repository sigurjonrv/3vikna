/*function getAll() {
    $.ajax({
        type: "GET",
        url: "/Home/GetUpvotes",
        dataType: "JSON",
        success:
            function (comments) {
                for (var i = 0; i < comments.length; i++) {
                    comments[i].CommentDate = ConvertStringToJSDate(comments[i].CommentDate);
                    var template = '<li class="list-group-item" id="listi-item' + i + '"><span class="glyphicon glyphicon-user"></span><span class="text-primary" id="a">' + comments[i].Username + '</span><span>' + " " + comments[i].CommentText + '</span><p id="commentTEXT">' + comments[i].CommentDate + '<a  data-control="userBtn" class="like_takki" href="#" onclick="javascript: return addLike(' + comments[i].ID + ')">Like <span class="glyphicon glyphicon-thumbs-up"></span></a></p><ul id="list-unstyled"></ul></li>';
                    var tempName = comments[i].CommentText;
                    $("#listi").append(template);
                    getLikes(comments[i].ID);

                }

            }
    });
}*/
$(document).ready(function () {
    getAll();
    $("#butid").click(function (evt) {
        var comment = document.getElementById('CommentText').value;
        $.trim(comment);
        $.ajax({
            type: "POST",
            url: "/Home/Index",
            data: { "CommentText": comment },
            dataType: "JSON",
            success: function () {
                remove()
                getAll()
            }
        });
        evt.preventDefault();
    })
});

function remove() {
    $(".list-group").empty();
}

function ConvertStringToJSDate(dt) {
    var dtE = /^\/Date\((-?[0-9]+)\)\/$/.exec(dt);
    if (dtE) {
        var dt = new Date(parseInt(dtE[1], 10));
        return dt;
    } return null;
}
function addLike(id) {
    $.ajax({
        type: "POST",
        url: "/Home/AddLike",
        data: { "id": id },
        dataType: "JSON",
        success:
            function (id) {
                getUpvotes(id);
            }
    });
}
function getUpvotes(id) {
    $.ajax({
        type: "GET",
        url: "/Home/GetUpvotes",
        data: { "id": id },
        dataType: "JSON",
        success:
            function (id) {
                for (var i = 0; i < id.length; i++) {
                    var template = id[i].UsernameWhoLiked;
                    var tala = id[i].commentID - 1;
                    $("#listi-item" + tala).append("<ol>" + template + " liked this</ol>");
                }
            }
    });
}