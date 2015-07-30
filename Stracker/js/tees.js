//function FillFacilities() {
//    $.ajax({
//        url: '/BusinessLayer/GetFacilities',
//        type: 'POST',
//        dataType: 'json',
//        contentType: 'application/json; charset=utf-8',
//        success: function (data) {
//            var ddlFacilities = $("#ddlFacilities");
//            ddlFacilities.empty();
//            $.each(data, function (i, facility) {
//                ddlFacilities.append('<option value="' + facility.FacilityId + '">' + facility.Name + '</option>');
//            });
//        },
//        error: function () {
//            alert('Failed to retrieve facilites.');
//        }
//    });
//}

function FillCourses() {
    $.ajax({
        url: '/BusinessLayer/GetCourses',
        type: 'POST',
        data: "{facilityId:" + $("#FacilityId").val() + "}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var ddlCourse = $("#CourseId");
            ddlCourse.empty();
            $.each(data, function (i, course) {
                ddlCourse.append('<option value="' + course.CourseId + '">' + course.Name + '</option>');
            });
        },
        error: function () {
            alert('Failed to retrieve courses.');
        }
    });
}

//function FillCourses() {
//    $.ajax({
//        url: '/BusinessLayer/GetCourses',
//        type: 'POST',
//        data: "{facilityId:" + $("#FacilityId").val() + "}",
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        success: function (data) {
//            var ddlCourse = $("#CourseId");
//            ddlCourse.empty();
//            $(result).each(function () {
//                $(document.createElement('option'))
//                    .attr('value', this.Id)
//                    .text(this.Value)
//                    .appendTo(ddlCourse);
//            });
//        },
//        error: function () {
//            alert('Failed to retrieve courses.');
//        }
//    });
//}


$(function () {
    $("#FacilityId").change(FillCourses);
    FillCourses();
});