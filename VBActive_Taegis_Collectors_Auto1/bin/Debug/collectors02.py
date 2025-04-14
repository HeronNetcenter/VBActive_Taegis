# COLLECTORS - GERAÇÃO DO ARQUIVO CSV
""" 
AUTOR: HERON JR
DATA:  10/03/23
ALT:   19/04/23
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
arquivo = open('collectors02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.csv', 'w')
arquivo.write("Collector Num;Cluster Type;Created At;Deployments;Description;Health;ID;Name;DHCP;DNS;Hostnamr;NTP;Proxy;Role;Status;Type;Updated At")    # Cabeçalho

# Arquivo de leitura
with open('collectors02_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'r') as json_str:
    data = json.load(json_str)
    
i = 0
total = len(data['data']['getAllClusters'])

while i <= (total - 1):
    ddata1 = data['data']['getAllClusters'][i]['clusterType']
    ddata2 = data['data']['getAllClusters'][i]['createdAt']
    ddata3 = data['data']['getAllClusters'][i]['deployments']
    ddata4 = data['data']['getAllClusters'][i]['description']
    ddata5 = data['data']['getAllClusters'][i]['health']
    ddata6 = data['data']['getAllClusters'][i]['id']
    ddata7 = data['data']['getAllClusters'][i]['name']
    ddata8 = data['data']['getAllClusters'][i]['network']['dhcp']
    ddata9 = data['data']['getAllClusters'][i]['network']['dns']
    ddata10 = data['data']['getAllClusters'][i]['network']['hostname']
    ddata11 = data['data']['getAllClusters'][i]['network']['ntp']
    ddata12 = data['data']['getAllClusters'][i]['network']['proxy']
    ddata13 = data['data']['getAllClusters'][i]['role']
    ddata14 = data['data']['getAllClusters'][i]['status']
    ddata15 = data['data']['getAllClusters'][i]['type']
    ddata16 = data['data']['getAllClusters'][i]['updatedAt']

    i += 1
    print(f"Collector_Num: {i}, \nCluster Type: {ddata1}, \nCreated At: {ddata2}, \nDeployments: {ddata3}, \nDescription: {ddata4}, \nHealth: {ddata5}, \nId: {ddata6}, \nName: {ddata7}, \nDHCP: {ddata8}, \nDNS: {ddata9}, \nHostname: {ddata10}, \nntp: {ddata11}, \nProxy: {ddata12}, \nRole: {ddata13}, \nStatus: {ddata14}, \nType: {ddata15}, \nUpdated At: {ddata16} ,\n=====================================================")
    # Gravando o arquivo CSV
    arquivo.write(f"\n{i};{ddata1};{ddata2};{ddata3};{ddata4};{ddata5};{ddata6};{ddata7};{ddata8};{ddata9};{ddata10};{ddata11};{ddata12};{ddata13};{ddata14};{ddata15};{ddata16}")    # Linha

print(f"\nTotal: {total}")
arquivo.close()