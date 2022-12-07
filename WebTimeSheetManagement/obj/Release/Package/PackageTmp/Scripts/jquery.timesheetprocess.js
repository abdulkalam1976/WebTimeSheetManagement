function Approvetimesheet() {
    if (confirm('Sure to Approve?'))
    {
            var TimeSheetModel =
                {
                    TimeSheetID: $("#TimeSheetID").val(),
                    Comment: $("#CommentApprover").val(),
                };

            var url = '/ShowAllTimeSheet/Approval';
            $.post(url, { TimeSheetApproval: TimeSheetModel }, function (data) {
                if (data) {
                    alert("Timesheet Approved Successfully");
                    window.location.href = "/ShowAllTimeSheet/TimeSheet";
                    return true;
                }
                else {
                    alert("Something Went Wrong!");
                    return false;
                }
            });
    }
    else {
        return false;
    }
}

function Rejecttimesheet() {

    if (confirm('Sure to Reject?')) {
        if ($("#CommentApprover").val() == '') {
            alert("Please Enter Comment");
            return false;
        }
        else {

            var TimeSheetModel =
               {
                   TimeSheetID: $("#TimeSheetID").val(),
                   Comment: $("#CommentApprover").val(),
               };

            var url = '/ShowAllTimeSheet/Rejected';
            $.post(url, { TimeSheetApproval: TimeSheetModel }, function (data) {
                if (data) {
                    alert("Timesheet Rejected Successfully");
                    window.location.href = "/ShowAllTimeSheet/TimeSheet";
                    return true;
                }
                else {
                    alert("Something Went Wrong!");
                }
            });

        }
    }
    else {
        return false;
    }
}