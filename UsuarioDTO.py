from dataclasses import dataclass

@dataclass
class Usuario():
    direccion:str=None
    numerodocumento:int=None
    telefono:str=None
    tipodocumento:str=None