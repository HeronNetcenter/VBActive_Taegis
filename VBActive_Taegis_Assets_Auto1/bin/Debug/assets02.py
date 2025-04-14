# ASSETS - GERAÇÃO DO ARQUIVO CSV
""" 
AUTOR: HERON JR
DATA:  27/01/23
ALT:   27/01/23-17/04/23
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
yesterday = datetime.now() - timedelta(days=1)

# Arquivo de saída CSV
arquivo = open('assets02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.csv', 'w')
arquivo.write("Asset_Num;Endpoint Type;Host ID;Host Name;ID;OS Version;Sensor ID;Sensor Tenant;Sensor Version;osFamily")    # Cabeçalho

# Arquivo de leitura
with open('assets02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'r') as json_str:
    data = json.load(json_str)
    
i = 0
total = len(data['data']['allAssets']['assets'])

while i <= (total - 1):
    ddata1 = data['data']['allAssets']['assets'][i]['endpointType']
    ddata2 = data['data']['allAssets']['assets'][i]['hostId']
    ddata3 = data['data']['allAssets']['assets'][i]['hostnames'][0]['hostname']
    ddata4 = data['data']['allAssets']['assets'][i]['id']
    ddata5 = data['data']['allAssets']['assets'][i]['osVersion']
    ddata6 = data['data']['allAssets']['assets'][i]['sensorId']
    ddata7 = data['data']['allAssets']['assets'][i]['sensorTenant']
    ddata8 = data['data']['allAssets']['assets'][i]['sensorVersion']
    ddata9 = data['data']['allAssets']['assets'][i]['osFamily']
    i += 1
    print(f"Asset_Num: {i}, \nEndpoint Type: {ddata1}, \nHost ID: {ddata2}, \nHost Name: {ddata3}, \nID: {ddata4}, \nOS Version: {ddata5}, \nSensor ID: {ddata6}, \nSensor Tenant: {ddata7}, \nSensor Version: {ddata8}, \nOS Family: {ddata9}, \n=====================================================")
    # Gravando o arquivo CSV
    arquivo.write(f"\n{i};{ddata1};{ddata2};{ddata3};{ddata4};{ddata5};{ddata6};{ddata7};{ddata8};{ddata9}")    # Linha

print(f"\nTotal: {total}")
arquivo.close()