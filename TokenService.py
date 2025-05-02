import json
import requests

CLIENT_ID="dr3Z9hDtGVq8UZyghU0Udd0TvLtwpcr9"
CLIENT_SECRET="fxLLHM8dDeVf2ExBaWGaOSjPRFdP6Lqjm-psDX8T1YcxJgeKzrc5KANFjDh74ThO"
AUDIENCE = "https://dev-kw0pbr6x4dqrvm5g.us.auth0.com/api/v2/"
GRANT_TYPE = "client_credentials"

token_url="https://dev-kw0pbr6x4dqrvm5g.us.auth0.com/oauth/token"


def get_access_token():
    token_data={
        "client_id":CLIENT_ID,
        "client_secret":CLIENT_SECRET,
        "audience":AUDIENCE,
        "grant_type":GRANT_TYPE
    }
    
    token_response= requests.post(token_url,json=token_data)
    
    if token_response.status_code==200:
        return token_response.json().get("access_token")
    else:
        print(f"Error al obtener el token: {token_response.status_code} - {token_response.text}")
        exit()
    