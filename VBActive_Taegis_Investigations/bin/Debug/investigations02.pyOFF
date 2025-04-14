# LISTA INVESTIGAÇÕES
""" 
AUTOR: HERON JR
DATA:  06/02/23
ALT:   28/02/23-02/03/23-18/04/23-10/10/23
 """
import json

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que enviará o parâmetro para o campo Tenant ID no Python
import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pTenantId')
args = parser.parse_args()

TENANT_ID = args.pTenantId

from datetime import datetime, timedelta
# yesterday = datetime.now() - timedelta(days=1)

# Investigações Todas - Arquivo de saída CSV
arquivo = open('investigations02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.csv', 'w')
arquivo.write("Investigation_Num;Id;Archived at;Created at;Created by;Description;Priority;Status;Type;Updated at;Service Desk Id;Short Id")    # Cabeçalho

# Arquivo de leitura
with open('investigations02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'r') as json_str:
    data = json.load(json_str)
    
i = 0
total = len(data['data']['allInvestigations'])
# total = 5
print(f"\nTotal: {total}")

while i <= (total - 1):
    ddata01 = data['data']['allInvestigations'][i]['id']
    ddata02 = data['data']['allInvestigations'][i]['archived_at']
    ddata03 = data['data']['allInvestigations'][i]['created_at']
    ddata04 = data['data']['allInvestigations'][i]['created_by']
    ddata05 = data['data']['allInvestigations'][i]['description']
    ddata06 = data['data']['allInvestigations'][i]['priority']
    ddata07 = data['data']['allInvestigations'][i]['status']
    ddata08 = data['data']['allInvestigations'][i]['type']
    ddata09 = data['data']['allInvestigations'][i]['updated_at']
    ddata10 = data['data']['allInvestigations'][i]['service_desk_id']
    ddata11 = data['data']['allInvestigations'][i]['shortId']
    i += 1
    ddata05 = ddata05.replace('"', '')
    ddata05 = ddata05.replace("'","")
    print(f"Investigation Num: {i}, \nId: {ddata01}, \nArchived at: {ddata02}, \nCreated at: {ddata03}, \nCreated by: {ddata04}, \nDescription: {ddata05}, \nPriority: {ddata06}, \nStatus: {ddata07}, \nType: {ddata08},\nUpdated at: {ddata09},\nService Desk Id: {ddata10},\nShort Id: {ddata11},\n====================================================================================================================================================================================================================")

    # Gravando o arquivo CSV
    arquivo.write(f"\n{i};{ddata01};{ddata02};{ddata03};{ddata04};{ddata05};{ddata06};{ddata07};{ddata08};{ddata09};{ddata10};{ddata11}") # Linha

print(f"\nTotal: {total}")
arquivo.close()
