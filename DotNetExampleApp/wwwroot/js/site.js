var currentStudentId = -1; //will be -1 for new Student entries (this is handled by the controller)

$('document').ready(() => {
    //create/edit student event
    $('#saveChangesBtn').click(() => {
        if (validateForm() === true) {
            $.ajax({
                type: "POST",
                url: "AddEditStudent",
                dataType: "JSON",
                data: {
                    'Id': currentStudentId,
                    'FName': $('#FName').val().trim(),
                    'LName': $('#LName').val().trim(),
                    'CurrGrade': $('#CurrGrade').val().trim(),
                    'Age': $('#Age').val().trim()
                },
                success: reloadPage
            });
        }
    });

    //modal close event
    $('#addEditStudentMdl').on('hidden.bs.modal', function (e) {
        clearModal();
    });

    //add new student click event. prep the modal then display it
    $('#addStudentBtn').click(() => {
        $('#addEditModalTitle').html("Add New Student");
        $('#saveChangesBtn').html("Create");
        currentStudentId = -1;

        $('#addEditStudentMdl').modal('show');
    });
});

//edit action links
function showEditModal(id, fname, lname, grade, age) {  
    //update header and button
    $('#addEditModalTitle').html("Edit Student");
    $('#saveChangesBtn').html("Save");
    currentStudentId = id;

    //populate fields 
    $('#FName').val(fname);
    $('#LName').val(lname);
    $('#CurrGrade').val(grade);
    $('#Age').val(age);

    $('#addEditStudentMdl').modal('show');
}

//delete action links
function deleteStudent(id) {
    $.ajax({
        type: "GET",
        url: "DeleteStudent",
        dataType: "text",
        data: { 'studentId': id },
        success: reloadPage
    });
}

//reset add/edit modal state
function clearModal() {
    //first name 
    $('#FName').val("");
    $('#FName').removeClass("invalidInput");
    $('#fNameError').attr("hidden", true);

    //last name 
    $('#LName').val("");
    $('#LName').removeClass("invalidInput");
    $('#lNameError').attr("hidden", true);

    //grade 
    $('#CurrGrade').val("");
    $('#CurrGrade').removeClass("invalidInput");
    $('#gradeError').attr("hidden", true);

    //age
    $('#Age').val("");
    $('#Age').removeClass("invalidInput");
    $('#ageError').attr("hidden", true);
}

//custom form validation. HTML's sucks.
function validateForm() {
    var valid = true;

    //first name
    if ($('#FName').val().trim() == "") {
        valid = false;
        $('#FName').addClass("invalidInput");
        $('#fNameError').attr("hidden", false);
    }

    else {
        $('#FName').removeClass("invalidInput");
        $('#fNameError').attr("hidden", true);
    }

    //last name
    if ($('#LName').val().trim() == "") {
        valid = false;
        $('#LName').addClass("invalidInput");
        $('#lNameError').attr("hidden", false);
    }

    else {
        $('#LName').removeClass("invalidInput");
        $('#lNameError').attr("hidden", true);
    }

    //grade
    if ($('#CurrGrade').val().trim() == "" || isNaN($('#CurrGrade').val() || $('#CurrGrade').val() > 100)) {
        valid = false;
        $('#CurrGrade').addClass("invalidInput");
        $('#gradeError').attr("hidden", false);
    }

    else {
        $('#CurrGrade').removeClass("invalidInput");
        $('#gradeError').attr("hidden", true);
    }

    //age
    if ($('#Age').val().trim() == "" || isNaN($('#Age').val() || $('#Age').val() > 130)) {
        valid = false;
        $('#Age').addClass("invalidInput");
        $('#ageError').attr("hidden", false);
    }

    else {
        $('#Age').removeClass("invalidInput");
        $('#ageError').attr("hidden", true);
    }

    return valid;
}

function reloadPage() {    
    location.reload();
}