const collegeDetailsURL = "http://localhost:5155/api/College/Details";

async function getCollegeDetails() {
    const token = localStorage.getItem('jwtToken');
    if (token) {
        try {
            const response = await fetch(collegeDetailsURL, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const collegeDetails = await response.json();
                document.querySelector('#welcome-message').textContent = `Welcome, ${collegeDetails.collegeName}`;
                return collegeDetails;
            } else {
                alert('Failed to fetch college details');
            }
        } catch (error) {
            console.error('Error:', error);
            alert('An error occurred while fetching college details');
        }
    } else {
        alert('College Logged Out');
        window.location.href = '/CollegeStudentPortal/login.html';
    }
}

async function getCollegeSummary() {
    const token = localStorage.getItem('jwtToken');
    if (token) {
        try {
            const response = await fetch('http://localhost:5155/api/Student', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const students = await response.json();
                const branchCounts = students.reduce((acc, student) => {
                    const branch = student.studentBranch;
                    if (branch) {
                        acc[branch] = (acc[branch] || 0) + 1;
                    }
                    return acc;
                }, {});

                const branchSummary = Object.entries(branchCounts)
                    .map(([branch, count]) => `<li>${branch}: ${count} students</li>`)
                    .join('');

                const summaryContainer = document.querySelector('.college-summary-container');
                summaryContainer.innerHTML = `
                    <p>Total Students in College: ${students.length}</p>
                    <h4>Branch-wise Count:</h4>
                    <ul>${branchSummary}</ul>
                `;
            } else {
                console.log('Failed to fetch college summary');
            }
        } catch (error) {
            console.error('Error:', error);
            alert('An error occurred while fetching college summary');
        }
    } else {
        alert('UnAuthorized College');
        window.location.href = '/CollegeStudentPortal/login.html';
    }
}

document.addEventListener('DOMContentLoaded', getCollegeDetails);
document.addEventListener('DOMContentLoaded', getCollegeSummary);

document.querySelector('.add-student').addEventListener('click', () => {
    document.querySelector('#add-student-container').classList.toggle('hidden');
    hideOtherContainers('add-student');
});

document.querySelector('.get-all-students').addEventListener('click', () => {
    document.querySelector('#get-all-students-container').classList.toggle('hidden');
    hideOtherContainers('get-all-students');
    getAllStudents();
});

document.querySelector('.get-student-by-roll').addEventListener('click', () => {
    document.querySelector('#get-student-by-roll-container').classList.toggle('hidden');
    hideOtherContainers('get-student-by-roll');
});

document.querySelector('.delete-student').addEventListener('click', () => {
    document.querySelector('#delete-student-container').classList.toggle('hidden');
    hideOtherContainers('delete-student');
});

document.querySelector('.logout').addEventListener('click', () => {
    localStorage.removeItem('jwtToken');
    window.location.href = '/CollegeStudentPortal/login.html';
});

function hideOtherContainers(visibleContainer) {
    const containers = ['add-student', 'get-all-students', 'get-student-by-roll', 'delete-student'];
    containers.filter(container => container !== visibleContainer)
        .forEach(container => document.querySelector(`#${container}-container`).classList.add('hidden'));
}

document.querySelector('#add-student-form').addEventListener('submit', async(e) => {
    e.preventDefault();
    const token = localStorage.getItem('jwtToken');
    const collegeDetails = await getCollegeDetails();

    const student = {
        StudentName: document.querySelector('#student-name').value,
        StudentRollNo: document.querySelector('#student-rollno').value,
        StudentAge: document.querySelector('#student-age').value,
        StudentBranch: document.querySelector('#student-branch').value,
        DateOfAdmission: document.querySelector('#date-of-admission').value,
        StudentEmail: document.querySelector('#student-email').value,
        CollegeId: collegeDetails.collegeId
    };

    try {
        const response = await fetch('http://localhost:5155/api/Student', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(student)
        });

        if (response.ok) {
            alert('Student added successfully');
        } else {
            alert('Failed to add student');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while adding student');
    }
});

async function getAllStudents() {
    const token = localStorage.getItem('jwtToken');
    try {
        const response = await fetch('http://localhost:5155/api/Student', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            const students = await response.json();
            const tableBody = document.querySelector('#get-all-students-container tbody');
            tableBody.innerHTML = students.map(student => `
                <tr>
                    <td>${student.id}</td>
                    <td>${student.studentName}</td>
                    <td>${student.studentRollNo}</td>
                    <td>${student.studentAge}</td>
                    <td>${student.studentBranch}</td>
                    <td>${student.dateOfAdmission}</td>
                    <td>${student.studentEmail}</td>
                    <td>${student.collegeId}</td>
                </tr>
            `).join('');
        } else {
            alert('Failed to fetch students');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while fetching students');
    }
}

document.querySelector('#get-student-by-roll-form').addEventListener('submit', async(e) => {
    e.preventDefault();
    const token = localStorage.getItem('jwtToken');
    const rollNo = document.querySelector('#student-rollno-get').value;

    try {
        const response = await fetch(`http://localhost:5155/api/Student/${rollNo}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            const student = await response.json();
            const tableBody = document.querySelector('#get-student-by-roll-container tbody');
            tableBody.innerHTML = `
                <tr>
                    <td>${student.id}</td>
                    <td>${student.studentName}</td>
                    <td>${student.studentRollNo}</td>
                    <td>${student.studentAge}</td>
                    <td>${student.studentBranch}</td>
                    <td>${student.dateOfAdmission}</td>
                    <td>${student.studentEmail}</td>
                    <td>${student.collegeId}</td>
                </tr>
            `;
        } else {
            alert('Failed to fetch student');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while fetching student');
    }
});

document.querySelector('#delete-student-form').addEventListener('submit', async(e) => {
    e.preventDefault();
    const token = localStorage.getItem('jwtToken');
    const studentId = document.querySelector('#student-id-delete').value;

    try {
        const response = await fetch(`http://localhost:5155/api/Student/${studentId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert('Student deleted successfully');
        } else {
            alert('Failed to delete student');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while deleting student');
    }
});