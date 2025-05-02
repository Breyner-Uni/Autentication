import TokenService as T
import UsuarioDTO as user
import requests



def UpdateProfileUser(user,user_id):
    update_url = f"https://dev-kw0pbr6x4dqrvm5g.us.auth0.com/api/v2/users/{user_id}"
    user_data={
        "user_metadata":{
            "direccion":user.direccion,
            "numerodocumento":user.numerodocumento,
            "telefono":user.telefono,
            "tipodocumento":user.tipodocumento
        }
    }
    
    token=T.get_access_token()
    
    headers = {
    "Authorization": f"Bearer {token}",
    "Content-Type": "application/json"
    }
    
    update_response= requests.patch(update_url,headers=headers,json=user_data)
    
    if update_response.status_code==200:
        print("Usuario actualizado correctamente")
        print(update_response.json)
        
    else:
        print(f"Error al actualizar el usuario: {update_response.status_code} - {update_response.text}")
    
    
    
