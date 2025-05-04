import TokenService as T
import UsuarioDTO as user
import requests
from os import environ as env
from dotenv import find_dotenv, load_dotenv

# Cargar variables del entorno
ENV_FILE = find_dotenv()
if ENV_FILE:
    load_dotenv(ENV_FILE)

class UpdateProfileUser:
    def __init__(self, usuario, user_id):
        self.usuario = usuario
        self.user_id = user_id
        self.update_user()

    def update_user(self):
        token = T.get_access_token()

        headers = {
            "Authorization": f"Bearer {token}",
            "Content-Type": "application/json"
        }

        body = {
            "user_metadata": {
                "direccion": self.usuario.direccion,
                "numerodocumento": self.usuario.numerodocumento,
                "telefono": self.usuario.telefono,
                "tipodocumento": self.usuario.tipodocumento
            }
        }

        url = f"https://{env.get('AUTH0_DOMAIN')}/api/v2/users/{self.user_id}"
        update_response = requests.patch(url, headers=headers, json=body)

        print(update_response.json())

        update_response.raise_for_status()
