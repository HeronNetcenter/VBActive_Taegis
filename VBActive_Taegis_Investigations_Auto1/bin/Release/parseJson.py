# INVESTIGATIONS - PARSE ARQUIVO investigations01_aaaammdd.json para investigations01__parse_aaaammdd.json
#   O arquivo gerado pela Query da Taegis não vem no formato JSON correto e não pode ser lido pelo desserializador.

""" 
AUTOR: HERON JR
DATA:  31/01/23
ALT:   06/02/23-02/03/23-18/04/23
"""
import json

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que enviará o parâmetro para o campo Tenant ID
import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pTenantId')
args = parser.parse_args()

TENANT_ID = args.pTenantId

from datetime import datetime, timedelta
# yesterday = datetime.now() - timedelta(days=1)

with open('investigations01_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'r') as fcc_file:
    fcc_data = json.load(fcc_file)
    json_object = json.loads(fcc_data)
    json_formatted_str = json.dumps(json_object, indent=2)
    print(json_formatted_str)

    # Grava o arquivo de saída "pretty json" no formato correto para o desserializador.
    arquivo = open('investigations02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'w')
    arquivo.write(json.dumps(json_object, indent=2))
    arquivo.close()
    