import json
from os import environ as env
from urllib.parse import quote_plus, urlencode

from UsuarioDTO import Usuario
from UpdateProfile import UpdateProfileUser
from GetUserInfo import get_user_metadata_from_auth0

from authlib.integrations.flask_client import OAuth
from dotenv import find_dotenv, load_dotenv
from flask import Flask, redirect, render_template, session, url_for, jsonify, request

ENV_FILE = find_dotenv()
if ENV_FILE:
    load_dotenv(ENV_FILE)

app = Flask(__name__)
app.secret_key = env.get("APP_SECRET_KEY")

oauth = OAuth(app)

oauth.register(
    "auth0",
    client_id=env.get("AUTH0_CLIENT_ID"),
    client_secret=env.get("AUTH0_CLIENT_SECRET"),
    client_kwargs={
        "scope": "openid profile email",
    },
    server_metadata_url=f'https://{env.get("AUTH0_DOMAIN")}/.well-known/openid-configuration',
)

@app.route("/")
def home():
    user_session = session.get("user")
    user_metadata = {}

    if user_session:
        user_id = user_session["userinfo"]["sub"]
        user_data = get_user_metadata_from_auth0(user_id)
        if user_data and "user_metadata" in user_data:
            user_metadata = user_data["user_metadata"]

    return render_template(
        "home.html",
        session=user_session,
        pretty=json.dumps(user_session, indent=4),
        metadata=user_metadata
    )

@app.route("/callback", methods=["GET", "POST"])
def callback():
    token = oauth.auth0.authorize_access_token()
    session["user"] = token
    return redirect("/")

@app.route("/login")
def login():
    return oauth.auth0.authorize_redirect(
        redirect_uri=url_for("callback", _external=True)
    )

@app.route("/logout")
def logout():
    session.clear()
    return redirect(
        "https://"
        + env.get("AUTH0_DOMAIN")
        + "/v2/logout?"
        + urlencode(
            {
                "returnTo": url_for("home", _external=True),
                "client_id": env.get("AUTH0_CLIENT_ID"),
            },
            quote_via=quote_plus,
        )
    )

@app.route("/updateprofile", methods=["PATCH"])
def update_profile():
    if 'user' not in session:
        return jsonify({'error': 'No autenticado'}), 401
    
    try:
        data = request.get_json()
        user_id = session['user']['userinfo']['sub']
        
        user = Usuario(
            direccion=data.get('direccion'),
            numerodocumento=data.get('numerodocumento'),
            telefono=data.get('telefono'),
            tipodocumento=data.get('tipodocumento')
        )
        
        usuario = UpdateProfileUser(user, user_id)
        return jsonify({'success': True})
    except Exception as e:
        import traceback
        traceback.print_exc()
        return jsonify({'error': str(e)}), 500

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=env.get("PORT", 3000))
