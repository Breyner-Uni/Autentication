# Authentication Service

This project is an authentication service designed to handle user login, registration, and session management. It provides a secure and scalable solution for managing user authentication in your applications.

## Features
- User registration and login
- Password hashing for security
- Token-based authentication
- Session management

## How to Use

1. Clone the repository:
    ```bash
    git clone <repository-url>
    cd Authentication
    ```

2. Install the required dependencies:
    ```bash
    pip install -r requirements.txt
    ```

3. Edit the .env File:
    Follow the .env.example and fill with your data

4. Access the service at `http://localhost:3000` (or the configured host and port).

## Requirements

Make sure you have Python 3.8+ installed. Install the dependencies using the following command:
```bash
pip install -r requirements.txt
```

### Environment Variables

To configure the application, you need to fill in the `.env` file with your own data. Follow these steps:

1. Visit the [Auth0 official page](https://auth0.com/) and log in to your account.
2. Navigate to your application settings in the Auth0 dashboard.
3. Locate the required credentials (e.g., Client ID, Client Secret, Domain, etc.).
4. Copy the credentials and paste them into the corresponding fields in the `.env` file.

Ensure that the `.env` file is properly configured before running the application. 


## Contributors
Breyner Felipe Meneses Muñoz
<a href="https://github.com/BreyMene/">
<img src="https://contrib.rocks/image?repo=BreyMene/BreyMene" alt="Breyner" />
</a>

Walter Ernesto Gutierres londoño
<a href="https://github.com/BreyMene/">
<img src="https://contrib.rocks/image?repo=Wgutierrezl/Wgutierrezl" alt="Walter" />
</a>
