

function FillCourses() {
    $.ajax({
        url: '/BusinessLayer/GetCourses',
        type: 'POST',
        data: "{facilityId:" + $("#FacilityId").val() + "}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var ddlCourse = $("#CourseId");
            var ddlTee = $("#TeeId");
            ddlCourse.empty();
            ddlTee.empty();
            $.each(data, function (i, course) {
                ddlCourse.append('<option value="' + course.CourseId + '">' + course.Name + '</option>');
            });

            FillTees();
        },
        error: function () {
            alert('Failed to retrieve courses.');
        }
    });
}

function FillTees() {
    if ($("#FacilityId").val() !== '' ||
        $("#CourseId").val() !== '') {
        $.ajax({
            url: '/BusinessLayer/GetTees',
            type: 'POST',
            data: "{facilityId:" + $("#FacilityId").val() + ", courseId:" + $("#CourseId").val() + "}",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                var ddlTee = $("#TeeId");
                ddlTee.empty();
                $.each(data, function(i, tee) {
                    ddlTee.append('<option value="' + tee.TeeId + '">' + tee.Name + '</option>');
                });
            },
            error: function() {
                alert('Failed to retrieve tees.');
            }
        });
    } else {
        alert('Please fill in Facility and Course');
    }

}


$(function () {
    $("#FacilityId").change(FillCourses);
    FillCourses();

    $("#CourseId").change(FillTees);
});