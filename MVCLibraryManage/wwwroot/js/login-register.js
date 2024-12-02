function register() {
    document.getElementById("login").style.left = "-400px";
    document.getElementById("register").style.left = "50px";
    document.getElementById("switch-btn").style.left = "50%";
}

function login() {
    document.getElementById("login").style.left = "50px";
    document.getElementById("register").style.left = "450px";
    document.getElementById("switch-btn").style.left = "0";
}

function validateFilledEmail() {
    var email = $("#register-email"); // Use correct ID
    return email.val().trim() !== "";
}

function validateFilledNewPass() {
    var pass = $("#register-pass"); // Use correct ID
    return pass.val().trim() !== "";
}

function validateFilledConfirmPass() {
    var confirmPass = $("#register-confirmpass"); // Use correct ID
    return confirmPass.val().trim() !== "";
}

function checkValidate() {
    let isCheck = true;

    if (!validateFilledEmail()) {
        $('#error').text('* Vui lòng nhập email!');
        isCheck = false;
    } else if (!validateFilledNewPass()) {
        $('#error').text('* Vui lòng nhập mật khẩu!');
        isCheck = false;
    } else if (!validateFilledConfirmPass()) {
        $('#error').text('* Vui lòng xác nhận lại mật khẩu!');
        isCheck = false;
    } else if ($("#register-pass").val() !== $("#register-confirmpass").val()) {
        $('#error').text('* Không khớp với mật khẩu. Vui lòng nhập lại!');
        isCheck = false;
    } else {
        $('#error').text(''); // Clear error if all checks pass
    }

    return isCheck;
}

function validateForm(event) {
    // Perform validation and only prevent form submission if validation fails
    if (!checkValidate()) {
        event.preventDefault(); // Stop form submission if validation fails
        return false;           // Ensure it returns false to prevent submission
    }
    // No need for additional event handling here if validation passes
    return true; // Allow form submission if validation passes
}

// Utility functions for validation
function isName(name) {
    return /^([a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+)((\s{1}[a-vxyỳọáầảấờễàạằệếýộậốũứĩõúữịỗìềểẩớặòùồợãụủíỹắẫựỉỏừỷởóéửỵẳẹèẽổẵẻỡơôưăêâđ]+){1,})$/.test(name.toLowerCase());
}

function isEmail(email) {
    return /^[a-z0-9](\.?[a-z0-9]){4,}@gmail\.com$/.test(email); // Updated email regex
}

function isPhone(number) {
    return /^0[35789][0-9]{8}\b/.test(number);
}

window.onload = function () {
    login();
};
