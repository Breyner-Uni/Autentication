import requests
from os import environ as env
from dotenv import find_dotenv, load_dotenv
from TokenService import get_access_token

# Cargar variables de entorno desde .env
ENV_FILE = find_dotenv()
if ENV_FILE:
    load_dotenv(ENV_FILE)

def get_user_metadata_from_auth0(user_id):
    token = get_access_token()
    headers = {
        "Authorization": f"Bearer {token}"
    }

    auth0_domain = env.get("AUTH0_DOMAIN")
    if not auth0_domain:
        print("ERROR: AUTH0_DOMAIN no está definido en el .env")
        return None

    url = f"https://{auth0_domain}/api/v2/users/{user_id}"
    
    response = requests.get(url, headers=headers)
    
    if response.status_code == 200:
        return response.json()
    else:
        print(f"Error al obtener datos del usuario: {response.status_code} - {response.text}")
        return None