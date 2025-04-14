# ALERTAS - GERAÇÃO DO ARQUIVO CSV COM OS ALERTAS DO DIA ANTERIOR
""" 
=========================
PROGRAMA: alerts_par02.py
=========================
AUTOR: HERON JR
DATA:  31/01/23
ALT:   13/02/23
ALT:   31/03/23 - Parâmetros de seleção de cliente
ALT:   11/12/23 - Inclusão do campo ddata19 - Tactics & Technique Id (Enrichment Details)
ALT:   27/12/24 - Inclusão do campo ddata20 - Metadata_First_Resolved_At
 """
import json
import argparse

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pData1: data para a extração dos alertas
# O processo será automatizado pelo programa VB.Net de chamada a partir da data informada como parâmetro.
parser = argparse.ArgumentParser()
parser.add_argument('pData1')
parser.add_argument('pTenantId')
args = parser.parse_args()

# from datetime import datetime, timedelta
# yesterday = datetime.now() - timedelta(days=1)

# Arquivo de saída CSV
arquivo = open('alerts02_' + args.pTenantId + '_' + args.pData1.replace('-','') + '.csv', 'w', encoding="utf-8")
# Cabeçalho
arquivo.write("Alert_Num;Attack Technique Ids;Entities;Ent-Relationships;ID;Investigation Ids;Metadata-Confidence;Metadata-Created At;Metadata-Creator Detector Id;Metadata-Creator Detector Version;Metadata-Creator Rule Id;Metadata-Creator Rule Version;Metadata-Engine Name;Metadata-Severity;Metadata-Title;Sensor Types;Status;Suppressed;Suppressed Rules;Tactics;Technique_id;Metadata-First Resolved At")    

# Arquivo de leitura
with open('alerts01_' + args.pTenantId + '_' + args.pData1.replace('-','') + '.json', 'r') as json_str:
    data = json.load(json_str)
    
isNone = 0
notNone = 0
i = 0
total = len(data['data']['alertsServiceSearch']['alerts']['list'])
# total = 2

while i <= (total - 1):
    ddata01 = data['data']['alertsServiceSearch']['alerts']['list'][i]['attack_technique_ids']
    ddata02 = data['data']['alertsServiceSearch']['alerts']['list'][i]['entities']['entities']
    ddata03 = data['data']['alertsServiceSearch']['alerts']['list'][i]['entities']['relationships']
    ddata04 = data['data']['alertsServiceSearch']['alerts']['list'][i]['id']
    ddata05 = data['data']['alertsServiceSearch']['alerts']['list'][i]['investigation_ids']
    ddata06 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['confidence']

    try:
        ddata20 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['first_resolved_at']['seconds']
        if ddata20 is None:
            ddata20 = 0
    except (KeyError, TypeError, IndexError):
        ddata20 = 0

    ddata07 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['created_at']['seconds']
    ddata08 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['detector']['detector_id']
    ddata09 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['detector']['version']
    ddata10 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['rule']['rule_id']
    ddata11 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['creator']['rule']['version']
    ddata12 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['engine']['name']
    ddata13 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['severity']
    ddata14 = data['data']['alertsServiceSearch']['alerts']['list'][i]['metadata']['title']
    ddata15 = data['data']['alertsServiceSearch']['alerts']['list'][i]['sensor_types']
    ddata16 = data['data']['alertsServiceSearch']['alerts']['list'][i]['status']
    ddata17 = data['data']['alertsServiceSearch']['alerts']['list'][i]['suppressed']
    ddata18 = data['data']['alertsServiceSearch']['alerts']['list'][i]['suppression_rules']
    # O campo enrichment_details é uma lista com n dicionários 'mitre_attack_info' e cada um com um dicionário {'tactics': <valor>, 'technique_id': <valor>}
    ddata19 = data['data']['alertsServiceSearch']['alerts']['list'][i]['enrichment_details']
    i += 1
    
    # ddata02 - entities - Este campo é uma lista
    for j in range(len(ddata02)):
        if ';' in ddata02[j]:
            ddata02[j] = ddata02[j].replace(';', '~')

    # ddata03 - relationships - Este campo é uma lista
    #   Cada subcampo da lista ddata03 é um dicionário
    if isinstance(ddata03, type(None)):
        isNone+=1
    elif type(ddata03) is list:
        j = 0
        notNone+=1

        for j in range(len(ddata03)):
            strDic = str(ddata03[j])
            if ';' in strDic:
                strDic = strDic.replace(';', '~')
                ddata03[j] = eval(strDic)
    
    ddata14 =  ddata14.replace(';', ',')

    # print(f"Alert Num: {i}, \nattack_technique_ids: {ddata01}, \nEntities/Entities: {ddata02}, \nEntities/Relationships: {ddata03}, \nEntities/Id: {ddata04}, \nInvestigation_Ids: {ddata05}, \nMetadata/Confidence: {ddata06}, \nMetadata/Created at: {ddata07}, \nMetadata/Creator/Detector/Detector_Id: {ddata08}, \nMetadata/Creator/Detector/Version: {ddata09}, \nMetadata/Creator/Rule/Rule_Id: {ddata10}, \nMetadata/Creator/Rule/Version: {ddata11}, \nMetadata/Enginee/Name: {ddata12}, \nMetadata/Severity: {ddata13}, \nMetadata/Title: {ddata14}, \nSensor Types: {ddata15}, \nStatus: {ddata16}, \nSuppressed: {ddata17}, \nSuppression Rules: {ddata18}, \n====================================================================================================================================================================================================================")

    # Gravando o arquivo CSV
    arquivo.write(f"\n{i};{ddata01};{ddata02};{ddata03};{ddata04};{ddata05};{ddata06};{ddata07};{ddata08};{ddata09};{ddata10};{ddata11};{ddata12};{ddata13};{ddata14};{ddata15};{ddata16};{ddata17};{ddata18};{ddata19};{ddata20}")    # Linha

# print(f"\nTotal: {total}")
arquivo.close()