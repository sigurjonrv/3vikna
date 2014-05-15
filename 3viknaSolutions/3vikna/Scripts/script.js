function Alert() 
{
    $.ajax({
        type: "POST",
        url: "/Home/Alert()",
        data: { "name": name, "UserName": UserName}, 
        dataType: "JSON", 
        success:  
            function (name, UserName) {
                if (name == UserName) {
                    alert("KE");
                }
            }});
}