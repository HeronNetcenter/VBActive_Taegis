# collector - PARSE DA SAÍDA collectors01_aaaammdd.json PARA UM FORMATO JSON DENTRO DO PADRÃO CORRETO
""" 
AUTOR: HERON JR
DATA:  27/01/23
ALT:   10/03/23
"""
import json

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que enviará o parâmetro para o Tenant ID
import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pTenantId')
args = parser.parse_args()

TENANT_ID = args.pTenantId

from datetime import datetime, timedelta
# yesterday = datetime.now() - timedelta(days=1)

with open('collectors01_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'r') as fcc_file:
    fcc_data = json.load(fcc_file)
    print(json.dumps(fcc_data, indent=4, sort_keys=True))
    arquivo = open('collectors02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'w')
    arquivo.write(json.dumps(fcc_data, indent=4, sort_keys=True))
    arquivo.close()