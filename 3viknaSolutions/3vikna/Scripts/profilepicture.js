function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#profileimage')
                .attr('src', e.target.result)
                .width(170)
                .height(220);
        };

        reader.readAsDataURL(input.files[0]);
    }
}