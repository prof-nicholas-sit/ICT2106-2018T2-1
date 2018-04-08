function resetAccountPasswords(emails) {
    $.ajax({
        type: "POST", 
        url: "/Account/resetAccount",
        data: emails,
        success: function (data) {
            alert("account password resetted");
        },
        error: function () {

        }
    });
}

function deleteAccounts(emails) {
    $.ajax({
        type: "POST", 
        url: "/Account/deleteAccount",
        data: emails,
        success: function (data) {
            alert("account deleted");
        },
        error: function () {

        }
    });
}


function getUserProfile(email) {
    showUserProfile({});
    $.ajax({
        type: "POST", 
        url: "/Account/getUserProfile",
        data: { "email": email },
        success: function (data) {
            showUserProfile(data)
        },
        error: function () {

        }
    });
}

function deleteMultiple() {
    selectedUsersEmails = getSelectedEmails();
    if (confirm("You are about to delete the following users: \n" + selectedUsersEmails)) {
        $.ajax({
            type: "POST",
            url: "/Account/deleteAccounts",
            data: { "emails": selectedUsersEmails },
            success: function (data) {
                alert("accounts deleted");
                location.reload();
            },
            error: function () {

            }
        });
    } 
}

function resetMultiple() {
    selectedUsersEmails = getSelectedEmails();
    if (confirm("You are about to reset the accounts of the following users: \n" + selectedUsersEmails)) {
        $.ajax({
            type: "POST",
            url: "/Account/resetAccounts",
            data: { "emails": selectedUsersEmails },
            success: function (data) {
                alert("accounts passwords resetted");
            },
            error: function () {

            }
        });
    } 
}

function getSelectedEmails() {
    var selectedUsers = $(".userCheckbox:checked");
    var selectedUsersEmails = [];
    for (var i = 0; i < selectedUsers.length; i++) {
        selectedUsersEmails.push($(selectedUsers[i]).val());
    }
    return selectedUsersEmails;
}

function showUserProfile(userDetails) {
    var birthdayRaw = new Date(userDetails["birthday"]);
    var birthday = birthdayRaw.getDay() + "-" + birthdayRaw.getMonth() + "-" + birthdayRaw.getFullYear();

    $(".modal-display").css("display", "block");
    $("#modal-user-name").html(userDetails["name"]);
    $("#modal-user-title").html(userDetails["title"]);
    $("#modal-user-email").html(userDetails["email"]);
    $("#modal-user-birthday").html(birthday);
    $("#modal-user-bio").html(userDetails["bio"]);
    $("#modal-user-img").attr("src", userDetails["img"]);
}

function closeProfileModal() {
    $(".modal-display").css("display", "none");

    $("#modal-user-name").html("");
    $("#modal-user-title").html("");
    $("#modal-user-email").html("");
    $("#modal-user-birthday").html("");
    $("#modal-user-bio").html("");
    $("#modal-user-img").attr("src", "");
}

function search() {
    $("#search").submit();

}