import json
import requests
from os import environ as env
from dotenv import find_dotenv, load_dotenv

# Cargar variables de entorno desde .env
ENV_FILE = find_dotenv()
if ENV_FILE:
    load_dotenv(ENV_FILE)

def get_access_token():
    token_url = f"https://{env.get('AUTH0_DOMAIN')}/oauth/token"

    token_data = {
        "client_id": env.get("AUTH0_CLIENT_ID"),
        "client_secret": env.get("AUTH0_CLIENT_SECRET"),
        "audience": f"https://{env.get('AUTH0_DOMAIN')}/api/v2/",
        "grant_type": "client_credentials"
    }

    token_response = requests.post(token_url, json=token_data)

    if token_response.status_code == 200:
        return token_response.json().get("access_token")
    else:
        print(f"Error al obtener el token: {token_response.status_code} - {token_response.text}")
        exit()
