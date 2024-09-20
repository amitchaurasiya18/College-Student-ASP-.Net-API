const loginURL = "http://localhost:5155/api/College/Login";

document.querySelector('#login-form').addEventListener('submit', async function(event) {
    event.preventDefault(); // Prevent the default form submission behavior

    const email = document.querySelector('#college-email').value;
    const password = document.querySelector('#college-password').value;

    const response = await fetch(loginURL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            CollegeEmail: email,
            Password: password
        })
    });

    if (response.ok) {
        const token = await response.text();
        localStorage.setItem('jwtToken', token);
        if (token != "Not a Valid User") {
            window.location.href = '/CollegeStudentPortal/collegeDashboard.html';
        } else {
            alert("College Not Found!!")
        }
    } else {
        alert('Invalid email or password');
    }
});