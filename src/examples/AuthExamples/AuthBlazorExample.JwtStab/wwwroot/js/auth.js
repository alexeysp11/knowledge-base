function validateUser(login, password) {
    // Create request object.
    const request = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ login: login, password: password })
    };
    //document.cookie = 'authblazorexample-cookies=' + login;
    console.log(request.body);

    // Send HTTP request.
    fetch('/api/Auth/ValidateUser', request)
        .then(response => {
            if (response.ok) {
                // Get cookies from response.
                response.headers.forEach((value, name) => {
                    // Add cookies into browser.
                    console.log(name + '=' + value);
                    document.cookie = name + '=' + value; path='/';
                });
                // Redirect to root.
            } else {
                console.error('Authentication error!');
            }
        })
        .catch(error => {
            console.error('Error sending request:', error);
        });
}